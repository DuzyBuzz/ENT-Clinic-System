using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace ENT_Clinic_System.Helpers
{
    internal static class ReportHelper
    {
        private static PrintDocument _printDocument;
        private static DataTable _currentReport;
        private static int _currentPrintRow = 0;
        private static string _reportTitle = "";
        private static DateTime? _dateFrom = null;  // NEW
        private static DateTime? _dateTo = null;    // NEW
        private static int _currentPage = 1;
        private static int _totalPages = 0;
        private static bool _isMeasuring = false; // used internally for pre-measurement
        public static void GenerateReport(
            string tableName,
            string dateColumn,
            DateTime? dateFrom,
            DateTime? dateTo,
            List<string> displayColumns,
            List<string> hiddenColumns,
            string reportTitle = ""
        )
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name required.");
            if (displayColumns == null || displayColumns.Count == 0)
                throw new ArgumentException("At least one display column required.");

            _reportTitle = reportTitle;
            _dateFrom = dateFrom;
            _dateTo = dateTo;

            string columns = string.Join(", ", displayColumns);
            string query = $"SELECT {columns} FROM {tableName}";

            bool useDateFilter = !string.IsNullOrWhiteSpace(dateColumn) && (dateFrom.HasValue || dateTo.HasValue);
            if (useDateFilter)
            {
                List<string> conditions = new List<string>();
                if (dateFrom.HasValue)
                    conditions.Add($"{dateColumn} >= @DateFrom");
                if (dateTo.HasValue)
                    conditions.Add($"{dateColumn} <= @DateTo");
                query += " WHERE " + string.Join(" AND ", conditions);
            }

            DataTable dt = new DataTable();
            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand(query, conn))
            using (var adapter = new MySqlDataAdapter(cmd))
            {
                if (useDateFilter)
                {
                    if (dateFrom.HasValue)
                        cmd.Parameters.AddWithValue("@DateFrom", dateFrom.Value);
                    if (dateTo.HasValue)
                        cmd.Parameters.AddWithValue("@DateTo", dateTo.Value);
                }
                conn.Open();
                adapter.Fill(dt);
            }

            // Totals row
            if (dt.Rows.Count > 0)
            {
                DataRow totalRow = dt.NewRow();
                foreach (var col in displayColumns)
                {
                    if (dt.Columns.Contains(col) &&
                        (dt.Columns[col].DataType == typeof(int) ||
                         dt.Columns[col].DataType == typeof(decimal) ||
                         dt.Columns[col].DataType == typeof(double)))
                    {
                        decimal sum = 0;
                        foreach (DataRow r in dt.Rows)
                            if (r[col] != DBNull.Value)
                                sum += Convert.ToDecimal(r[col]);
                        totalRow[col] = sum;
                    }
                    else if (dt.Columns.Contains(col))
                        totalRow[col] = DBNull.Value;
                }
                dt.Rows.Add(totalRow);
            }

            // Remove hidden columns
            if (hiddenColumns != null)
            {
                foreach (var col in hiddenColumns)
                {
                    if (dt.Columns.Contains(col))
                        dt.Columns.Remove(col);
                }
            }

            _currentReport = dt.Copy();
            _currentPrintRow = 0;
            _printDocument = new PrintDocument();
            _printDocument.DefaultPageSettings.Landscape = true;
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        public static void ShowPreview()
        {
            if (_printDocument == null || _currentReport == null)
            {
                MessageBox.Show("No report loaded to print.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = _printDocument,
                Width = 900,
                Height = 700
            };

            preview.Shown += delegate
            {
                ToolStrip tool = preview.Controls.OfType<ToolStrip>().FirstOrDefault();
                if (tool != null)
                {
                    foreach (ToolStripItem item in tool.Items)
                    {
                        if (item is ToolStripButton btn && btn.ToolTipText.ToLower().Contains("print"))
                            btn.Visible = false;
                    }

                    ToolStripButton customPrint = new ToolStripButton("Print");
                    customPrint.Click += delegate
                    {
                        using (PrintDialog dlg = new PrintDialog { Document = _printDocument })
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                                _printDocument.Print();
                        }
                    };
                    tool.Items.Insert(0, customPrint);
                }
            };

            preview.ShowDialog();
        }

        /// <summary>
        /// Handles printing each page — draws headers, alternating rows, and "Page X of Y".
        /// Includes accurate page calculation and column divider lines.
        /// </summary>
        private static void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_currentReport == null) return;

            // Recalculate total pages if not done yet
            if (!_isMeasuring && _totalPages == 0)
                CalculateTotalPages((PrintDocument)sender);

            int startX = e.PageBounds.Left + 10;
            int startY = e.PageBounds.Top + 10;
            int bottomMargin = e.PageBounds.Bottom - 40;
            int offsetY = 0;
            int rowHeight = 0;

            using (Font font = new Font("Arial", 6))
            using (Font headerFont = new Font("Arial", 8, FontStyle.Bold))
            using (Font titleFont = new Font("Arial", 12, FontStyle.Bold))
            using (Font dateFont = new Font("Arial", 9, FontStyle.Italic))
            using (Font pageFont = new Font("Arial", 8, FontStyle.Italic))
            {
                // === Report Title (only on first page) ===
                if (_currentPrintRow == 0 && !_isMeasuring)
                {
                    if (!string.IsNullOrWhiteSpace(_reportTitle))
                    {
                        SizeF titleSize = e.Graphics.MeasureString(_reportTitle, titleFont);
                        e.Graphics.DrawString(_reportTitle, titleFont, Brushes.Black, startX, startY);
                        offsetY += (int)titleSize.Height + 5;
                    }

                    if (_dateFrom.HasValue || _dateTo.HasValue)
                    {
                        string dateRange = "";
                        if (_dateFrom.HasValue && _dateTo.HasValue)
                            dateRange = $"From: {_dateFrom.Value:yyyy-MM-dd}  To: {_dateTo.Value:yyyy-MM-dd}";
                        else if (_dateFrom.HasValue)
                            dateRange = $"From: {_dateFrom.Value:yyyy-MM-dd}";
                        else if (_dateTo.HasValue)
                            dateRange = $"To: {_dateTo.Value:yyyy-MM-dd}";

                        SizeF dateSize = e.Graphics.MeasureString(dateRange, dateFont);
                        float rightX = e.PageBounds.Right - 10 - dateSize.Width;
                        e.Graphics.DrawString(dateRange, dateFont, Brushes.Black, rightX, startY);
                    }

                    offsetY += 20;
                }

                // === Column Headers (draw every page) ===
                float colX = startX;
                int colCount = _currentReport.Columns.Count;
                float colWidth = (e.PageBounds.Width - 40) / (float)colCount;
                rowHeight = 0;

                foreach (DataColumn col in _currentReport.Columns)
                {
                    string headerText = col.ColumnName.Replace("_", " ");
                    SizeF sz = e.Graphics.MeasureString(headerText, headerFont, (int)colWidth);
                    rowHeight = Math.Max(rowHeight, (int)sz.Height + 6);
                }

                // Header background
                e.Graphics.FillRectangle(Brushes.WhiteSmoke, startX - 2, startY + offsetY, colWidth * colCount, rowHeight);
                e.Graphics.DrawRectangle(Pens.Black, startX - 2, startY + offsetY, colWidth * colCount, rowHeight);

                // Header text + column divider lines
                colX = startX;
                foreach (DataColumn col in _currentReport.Columns)
                {
                    string headerText = col.ColumnName.Replace("_", " ");
                    e.Graphics.DrawString(headerText, headerFont, Brushes.Black,
                        new RectangleF(colX, startY + offsetY, colWidth, rowHeight));

                    // vertical divider line
                    e.Graphics.DrawLine(Pens.Black, colX, startY + offsetY, colX, startY + offsetY + rowHeight);
                    colX += colWidth;
                }
                e.Graphics.DrawLine(Pens.Black, colX, startY + offsetY, colX, startY + offsetY + rowHeight);
                offsetY += rowHeight;

                // === Print Data Rows ===
                while (_currentPrintRow < _currentReport.Rows.Count)
                {
                    DataRow row = _currentReport.Rows[_currentPrintRow];
                    float colX2 = startX;
                    rowHeight = 0;

                    // Alternating row background
                    Brush rowBrush = (_currentPrintRow % 2 == 0) ? Brushes.White : Brushes.LightGray;
                    if (_currentPrintRow == _currentReport.Rows.Count - 1)
                        rowBrush = Brushes.LightYellow;

                    foreach (DataColumn col in _currentReport.Columns)
                    {
                        SizeF sz = e.Graphics.MeasureString(row[col].ToString(), font, (int)colWidth);
                        rowHeight = Math.Max(rowHeight, (int)sz.Height + 5);
                    }

                    // Row background + outer border
                    e.Graphics.FillRectangle(rowBrush, startX - 2, startY + offsetY, colWidth * colCount, rowHeight);
                    e.Graphics.DrawRectangle(Pens.Black, startX - 2, startY + offsetY, colWidth * colCount, rowHeight);

                    // Cell text + vertical lines
                    colX2 = startX;
                    foreach (DataColumn col in _currentReport.Columns)
                    {
                        e.Graphics.DrawString(row[col].ToString(), font, Brushes.Black,
                            new RectangleF(colX2 + 2, startY + offsetY + 2, colWidth - 4, rowHeight - 4));
                        e.Graphics.DrawLine(Pens.Black, colX2, startY + offsetY, colX2, startY + offsetY + rowHeight);
                        colX2 += colWidth;
                    }
                    e.Graphics.DrawLine(Pens.Black, colX2, startY + offsetY, colX2, startY + offsetY + rowHeight);

                    offsetY += rowHeight;
                    _currentPrintRow++;

                    // Page break detection
                    if (startY + offsetY > bottomMargin)
                    {
                        if (!_isMeasuring)
                        {
                            string pageText = $"Page {_currentPage} of {_totalPages}";
                            SizeF pageSize = e.Graphics.MeasureString(pageText, pageFont);
                            float rightX = e.PageBounds.Right - 10 - pageSize.Width;
                            e.Graphics.DrawString(pageText, pageFont, Brushes.Black, rightX, e.PageBounds.Bottom - 25);
                        }

                        _currentPage++;
                        e.HasMorePages = true;
                        return;
                    }
                }

                // === Footer: Last Page ===
                if (!_isMeasuring)
                {
                    string pageText = $"Page {_currentPage} of {_totalPages}";
                    SizeF pageSize = e.Graphics.MeasureString(pageText, pageFont);
                    float rightX = e.PageBounds.Right - 10 - pageSize.Width;
                    e.Graphics.DrawString(pageText, pageFont, Brushes.Black, rightX, e.PageBounds.Bottom - 25);
                }
            }

            _currentPrintRow = 0;
            _currentPage = 1;
            e.HasMorePages = false;
        }

        /// <summary>
        /// Calculates total number of pages before printing (needed for "Page X of Y").
        /// More accurate page counting.
        /// </summary>
        private static void CalculateTotalPages(PrintDocument doc)
        {
            _isMeasuring = true;
            _totalPages = 0;
            _currentPrintRow = 0;

            using (var bmp = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(bmp))
            {
                bool morePages = true;
                while (morePages)
                {
                    _totalPages++;
                    PrintPageEventArgs fakeArgs = new PrintPageEventArgs(g, new Rectangle(0, 0, 800, 600),
                        new Rectangle(0, 0, 800, 600), doc.DefaultPageSettings);
                    fakeArgs.HasMorePages = false;
                    PrintDocument_PrintPage(doc, fakeArgs);
                    morePages = fakeArgs.HasMorePages;
                }
            }

            _isMeasuring = false;
            _currentPrintRow = 0;
            _currentPage = 1;
        }

    }

}
