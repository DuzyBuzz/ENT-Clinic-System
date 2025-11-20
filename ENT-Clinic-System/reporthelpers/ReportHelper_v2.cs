using ClosedXML.Excel;
using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ENT_Clinic_System.Helpers;
//using PdfSharp.Pdf;
//using PdfSharp.Drawing;
//using ClosedXML.Excel;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// Simple, copy/paste-ready ReportHelper.
    /// Call GenerateReport(...) then ShowPreview().
    /// You can call SetColumnWidths(...) to provide column widths (pixels) or percentages before printing.
    /// </summary>
    public static class ReportHelper_v2
    {
        // Printing state
        private static PrintDocument _printDocument;
        private static DataTable _currentReport;
        private static int _currentPrintRow = 0;
        private static int _currentPage = 1;
        private static int _totalPages = 0;
        private static bool _isMeasuring = false;

        // Options stored for printing/export
        private static string _reportTitle = "";
        private static string _reportSubtitle = "";
        private static DateTime? _dateFrom = null;
        private static DateTime? _dateTo = null;
        private static bool _showPageNumbers = true;
        private static Image _headerImage = null;
        private static string _rowNumberHeader = "#";
        private static bool _includeRowNumbers = false;
        private static List<string> _totalColumns = new List<string>();
        private static string _groupByColumn = null; // optional grouping column

        // Keep numeric grand totals computed ahead for footer or last row
        private static Dictionary<string, decimal> _grandTotals = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        private static Image _headerLeftImage;
        private static Image _headerRightImage;

        // Column width specification (optional). Keys: column names. Values: pixels or percent depending on flag.
        private static Dictionary<string, float> _columnWidthsSpec = null;
        private static bool _columnWidthsArePercent = false;

        public static void SetHeaderImages(Image leftImage, Image rightImage)
        {
            _headerLeftImage = leftImage;
            _headerRightImage = rightImage;
        }

        /// <summary>
        /// Set column widths to use when printing.
        /// widths: dictionary mapping columnName -> size.
        /// If arePercent == true, values are treated as percentages (0..100) of usable table width.
        /// If arePercent == false, values are treated as absolute pixel widths.
        /// Example: SetColumnWidths(new Dictionary{ {"Account_No", 100}, {"Total", 60} }, false);
        /// </summary>
        public static void SetColumnWidths(Dictionary<string, float> widths, bool arePercent = false)
        {
            if (widths == null)
            {
                _columnWidthsSpec = null;
                _columnWidthsArePercent = false;
                return;
            }

            _columnWidthsSpec = new Dictionary<string, float>(widths, StringComparer.OrdinalIgnoreCase);
            _columnWidthsArePercent = arePercent;
        }

        // -----------------------
        // Reusable convenience overload
        // -----------------------
        /// <summary>
        /// Convenience overload: filtersOrZone may be:
        /// - null
        /// - Dictionary<string, object>
        /// - string (treated as Zone = value)
        /// - anonymous object (public properties converted to dictionary)
        /// Calls the existing simplified GenerateReport that takes Dictionary filters.
        /// Optional parameters columnWidths and columnWidthsArePercent let you pass widths directly.
        /// </summary>
        public static void GenerateReport(
           string tableName,
           List<string> displayColumns,
           Dictionary<string, object> filters = null,
           string reportTitle = "",
           string reportSubtitle = "",
           bool showRowNumbers = false,
           bool landscape = true,
           string groupBy = null,
           List<string> totalColumns = null,
           Dictionary<string, float> columnWidths = null,
           bool columnWidthsArePercent = false
       )
        {
            // store options
            _reportTitle = reportTitle;
            _reportSubtitle = reportSubtitle ?? "";
            _dateFrom = null;
            _dateTo = null;
            _showPageNumbers = true;
            _includeRowNumbers = showRowNumbers;
            _rowNumberHeader = "#";
            _totalColumns = totalColumns ?? new List<string>();
            _groupByColumn = groupBy;
            _headerImage = null;

            // If caller passed column widths inline, apply them:
            if (columnWidths != null)
                SetColumnWidths(columnWidths, columnWidthsArePercent);

            // Build base SQL
            string cols = string.Join(", ", displayColumns);
            string query = $"SELECT {cols} FROM {tableName}";

            // Build filter WHERE clause if needed
            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
            if (filters != null && filters.Count > 0)
            {
                List<string> conds = new List<string>();
                int i = 0;
                foreach (var kv in filters)
                {
                    string paramName = $"p{i++}";
                    conds.Add($"{kv.Key} = @{paramName}");
                    paramList.Add(new KeyValuePair<string, string>(paramName, kv.Value?.ToString() ?? ""));
                }
                query += " WHERE " + string.Join(" AND ", conds);
            }

            // Add optional group ordering
            if (!string.IsNullOrWhiteSpace(_groupByColumn))
                query += $" ORDER BY {_groupByColumn}";

            DataTable dt = new DataTable();
            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand(query, conn))
            using (var adapter = new MySqlDataAdapter(cmd))
            {
                if (filters != null && filters.Count > 0)
                {
                    int idx = 0;
                    foreach (var kv in filters)
                    {
                        string paramName = $"@p{idx++}";
                        cmd.Parameters.AddWithValue(paramName, kv.Value ?? DBNull.Value);
                    }
                }
                conn.Open();
                adapter.Fill(dt);

                // --- Format DateTime columns to date-only strings safely ---
                List<DataColumn> dateCols = dt.Columns.Cast<DataColumn>()
                                                     .Where(c => c.DataType == typeof(DateTime))
                                                     .ToList();

                foreach (DataColumn col in dateCols)
                {
                    string newColName = col.ColumnName + "_str";
                    DataColumn newCol = new DataColumn(newColName, typeof(string));
                    dt.Columns.Add(newCol);

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row[col] != DBNull.Value)
                            row[newCol] = ((DateTime)row[col]).ToString("yyyy-MM-dd");
                        else
                            row[newCol] = "";
                    }

                    int ordinal = col.Ordinal;
                    dt.Columns.Remove(col);
                    newCol.ColumnName = col.ColumnName;
                    newCol.SetOrdinal(ordinal);
                }
            }

            // Add row numbers
            if (showRowNumbers)
            {
                if (!dt.Columns.Contains(_rowNumberHeader))
                {
                    DataColumn rc = new DataColumn(_rowNumberHeader, typeof(string));
                    dt.Columns.Add(rc);
                    rc.SetOrdinal(0);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                    dt.Rows[i][_rowNumberHeader] = (i + 1).ToString();
            }

            // set current report
            _currentReport = dt.Copy();

            // ------------------------------
            // PRECOMPUTE GRAND TOTALS
            // (sums entire dataset; change later if you want visible-only totals)
            // ------------------------------
            _grandTotals = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
            if (_totalColumns != null && _totalColumns.Count > 0)
            {
                foreach (var colName in _totalColumns)
                {
                    decimal sum = 0m;
                    if (_currentReport.Columns.Contains(colName))
                    {
                        foreach (DataRow r in _currentReport.Rows)
                        {
                            if (r[colName] != DBNull.Value && decimal.TryParse(r[colName].ToString(), out decimal v))
                                sum += v;
                        }
                    }
                    _grandTotals[colName] = sum;
                }
            }

            _currentPrintRow = 0;
            _currentPage = 1;
            _totalPages = 0;
            _isMeasuring = false;

            _printDocument = new PrintDocument();
            // Force long bond paper
            SetLongBondPaper(_printDocument);
            CalculateTotalPages(_printDocument);
            _printDocument.DefaultPageSettings.Landscape = landscape;

            // Compute total pages BEFORE preview/printing
            CalculateTotalPages(_printDocument);
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        /// <summary>
        /// Shows a PrintPreviewDialog with custom buttons:
        /// - Print (shows PrintDialog to let user select a printer)
        /// - Export PDF (requires PdfSharp or similar)
        /// - Export Excel (requires ClosedXML)
        /// </summary>
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
                Width = 1000,
                Height = 750,
                Icon = Properties.Resources.IGW_Logo
            };

            preview.Shown += (s, e) =>
            {
                var tool = preview.Controls.OfType<ToolStrip>().FirstOrDefault();
                if (tool != null)
                {
                    // Hide default print button
                    foreach (ToolStripItem item in tool.Items)
                    {
                        if (item is ToolStripButton btn && btn.ToolTipText != null && btn.ToolTipText.ToLower().Contains("print"))
                            btn.Visible = false;
                    }

                    ToolStripButton MakeButton(string text, Image icon, string tooltip)
                    {
                        var btn = new ToolStripButton
                        {
                            Text = text,
                            Image = icon,
                            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                            ImageAlign = ContentAlignment.MiddleLeft,
                            TextAlign = ContentAlignment.MiddleRight,
                            ToolTipText = tooltip,
                            Margin = new Padding(3, 1, 3, 2),
                            ForeColor = Color.Black
                        };
                        return btn;
                    }

                    Image printIcon = (Properties.Resources.print is Icon) ? ((Icon)Properties.Resources.print).ToBitmap() : Properties.Resources.print_image;
                    Image pdfIcon = (Properties.Resources.pdf is Icon) ? ((Icon)Properties.Resources.pdf).ToBitmap() : Properties.Resources.pdf_image;
                    Image excelIcon = (Properties.Resources.excel is Icon) ? ((Icon)Properties.Resources.excel).ToBitmap() : Properties.Resources.excel_image;

                    var printBtn = MakeButton("Print", printIcon, "Select printer and print report");
                    printBtn.Click += (ss, ee) =>
                    {
                        _currentPrintRow = 0;
                        _currentPage = 1;
                        _isMeasuring = false;

                        using (var pd = new PrintDialog { Document = _printDocument })
                        {

                            if (pd.ShowDialog() == DialogResult.OK)
                            {
                                _printDocument.PrinterSettings = pd.PrinterSettings;
                                _printDocument.Print();
                            }
                        }
                    };


                    var pdfBtn = MakeButton("Print to PDF", pdfIcon, "Save report as PDF using Microsoft Print to PDF");
                    pdfBtn.Click += (ss, ee) =>
                    {
                        using (var sfd = new SaveFileDialog
                        {
                            Filter = "PDF file (*.pdf)|*.pdf",
                            FileName = "report.pdf",
                            Title = "Save PDF File"
                        })
                        {
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    // Reset state
                                    _currentPrintRow = 0;
                                    _currentPage = 1;
                                    _isMeasuring = false;

                                    using (PrintDocument pd = new PrintDocument())
                                    {
                                        pd.DocumentName = "Report";
                                        pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                                        pd.PrinterSettings.PrintToFile = true;
                                        pd.PrinterSettings.PrintFileName = sfd.FileName;
                                        pd.PrintPage += PrintDocument_PrintPage;
                                        pd.Print();
                                        MessageBox.Show("PDF successfully created.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("PDF printing failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    };


                    var xlsBtn = MakeButton("Export Excel", excelIcon, "Export report to Excel");
                    xlsBtn.Click += (ss, ee) =>
                    {
                        using (var sfd = new SaveFileDialog { Filter = "Excel file (*.xlsx)|*.xlsx", FileName = "report.xlsx" })
                        {
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    ExportToExcel(sfd.FileName);
                                    MessageBox.Show("Excel exported successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Excel export failed: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    };

                    tool.Items.Insert(0, printBtn);
                    tool.Items.Insert(1, pdfBtn);
                    tool.Items.Insert(2, xlsBtn);
                }
            };

            preview.ShowDialog();
        }

        public static void ExportToExcel(string filePath)
        {
            if (_currentReport == null)
                throw new InvalidOperationException("No report data found to export.");

            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.Worksheets.Add("Report");
                ws.Cell(1, 1).InsertTable(_currentReport, "Report", true);
                ws.Columns().AdjustToContents();

                try { ws.Column(1).Width = 6.5; } catch { }

                workbook.SaveAs(filePath);
            }
        }

        // ----------------------
        // Printing: keep your dgv-like layout, with small defensive fixes
        // ----------------------
        private static void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_currentReport == null) return;

            int margin = 10;
            int startX = e.PageBounds.Left + margin;
            int startY = e.PageBounds.Top + margin;
            int footerHeight = -130; // positive footer height in px
            int offsetY = 0;
            int rowHeight = 0;

            using (Font font = new Font("Arial", 7))
            using (Font headerFont = new Font("Arial", 7, FontStyle.Bold))
            using (Font titleFont = new Font("Arial", 12, FontStyle.Bold))
            using (Font dateFont = new Font("Arial", 8, FontStyle.Italic))
            using (Font pageFont = new Font("Arial", 8, FontStyle.Italic))
            {
                Graphics g = e.Graphics;


                Action drawFooter = () =>
                {
                    if (_isMeasuring) return; // skip during measuring


                    // Position footer near the bottom of page, but within margin bounds
                    float footerY = e.MarginBounds.Bottom - footerHeight + 28f; // 10px padding from bottom

                    RectangleF footerRect = new RectangleF(
                        e.MarginBounds.Left,
                        footerY,
                        e.MarginBounds.Width,
                        footerHeight
                    );

                    // Draw footer background
                    g.FillRectangle(Brushes.White, footerRect);

                    // Left: printed date-time
                    string printDate = "Printed: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    SizeF footerDateSize = g.MeasureString(printDate, pageFont);
                    float dateY = footerRect.Top + (footerRect.Height - footerDateSize.Height) / 2f;
                    g.DrawString(printDate, pageFont, Brushes.Black, footerRect.Left - 90, dateY);



                    // --- Right-aligned: page X of Y ---
                    string pageText = (_totalPages > 0) ? $"Page {_currentPage} of {_totalPages}" : $"Page {_currentPage}";
                    SizeF footerPageSize = g.MeasureString(pageText, pageFont);
                    float pageX = footerRect.Right - footerPageSize.Width + 90; // right-aligned to margin
                    float pageY = footerRect.Top + (footerRect.Height - footerPageSize.Height) / 2f;
                    g.DrawString(pageText, pageFont, Brushes.Black, pageX, pageY);
                };

                // Helper: draw footer for current page (only when not measuring)
                // Only draw if not measuring
                if (!_isMeasuring)
                {
                    drawFooter(); // draw footer on actual print pass
                                  // draw rows, headers, totals, etc.
                }
                else
                {
                    // Only calculate row heights and page breaks
                    // increment _currentPrintRow as usual
                }
                // === Header images and title ===
                int headerImgSize = 60;
                if (_headerLeftImage != null)
                    g.DrawImage(_headerLeftImage, e.MarginBounds.Left + 100, e.MarginBounds.Top - 90, 50, 50);

                if (_headerRightImage != null)
                    g.DrawImage(_headerRightImage, e.MarginBounds.Right - 150, e.MarginBounds.Top - 90, 50, 50);

                if (!string.IsNullOrWhiteSpace(_reportTitle))
                {
                    StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
                    float midX = e.PageBounds.Left + e.PageBounds.Width / 2f;
                    g.DrawString(_reportTitle, titleFont, Brushes.Black, new PointF(midX, startY + 18), center);
                }

                if (!string.IsNullOrWhiteSpace(_reportSubtitle))
                {
                    StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
                    float midX = e.PageBounds.Left + e.PageBounds.Width / 2f;
                    g.DrawString(_reportSubtitle, dateFont, Brushes.Black, new PointF(midX, startY + 34), center);
                }

                offsetY += headerImgSize + 8;

                // === Date range display ===
                if (_dateFrom.HasValue || _dateTo.HasValue)
                {
                    string dateRange = _dateFrom.HasValue && _dateTo.HasValue
                        ? $"From: {_dateFrom.Value:yyyy-MM-dd}  To: {_dateTo.Value:yyyy-MM-dd}"
                        : (_dateFrom.HasValue ? $"{_dateFrom.Value:yyyy-MM-dd}" : $"{_dateTo.Value:yyyy-MM-dd}");

                    SizeF dateSize = g.MeasureString(dateRange, dateFont);
                    float rightX = e.PageBounds.Right - margin - dateSize.Width;
                    g.DrawString(dateRange, dateFont, Brushes.Black, rightX, startY + offsetY - 6);
                    offsetY += 18;
                }

                // === Column headers ===
                int colCount = _currentReport.Columns.Count;
                if (colCount == 0) return;

                float[] colWidths = new float[colCount];
                int usableWidth = Math.Max(100, e.PageBounds.Width - margin * 4);
                float fixedRowNumWidth = 40f;
                float minColWidth = 40f;

                // compute column widths (keeps your existing spec logic)
                if (_columnWidthsSpec != null && _columnWidthsSpec.Count > 0)
                {
                    float sumSpecified = 0f;
                    for (int i = 0; i < colCount; i++)
                    {
                        string colName = _currentReport.Columns[i].ColumnName;
                        if (_columnWidthsSpec.TryGetValue(colName, out float specVal))
                        {
                            colWidths[i] = _columnWidthsArePercent ? Math.Max(0f, specVal) * usableWidth / 100f : Math.Max(0f, specVal);
                            sumSpecified += colWidths[i];
                        }
                        else colWidths[i] = 0f;
                    }

                    if (_includeRowNumbers && colCount > 0 && colWidths[0] <= 0f)
                    {
                        colWidths[0] = fixedRowNumWidth;
                        sumSpecified += colWidths[0];
                    }

                    int unspecified = colWidths.Count(w => w <= 0f);
                    float remainingWidth = usableWidth - sumSpecified;
                    if (remainingWidth < 0) remainingWidth = 0;

                    if (unspecified > 0)
                    {
                        float perCol = Math.Max(minColWidth, remainingWidth / unspecified);
                        for (int i = 0; i < colCount; i++) if (colWidths[i] <= 0f) colWidths[i] = perCol;
                    }
                    else
                    {
                        float totalSpec = colWidths.Sum();
                        if (totalSpec > usableWidth && totalSpec > 0)
                        {
                            float scale = usableWidth / totalSpec;
                            for (int i = 0; i < colCount; i++) colWidths[i] *= scale;
                        }
                    }

                    for (int i = 0; i < colCount; i++) colWidths[i] = Math.Max(minColWidth, colWidths[i]);
                }
                else
                {
                    if (_includeRowNumbers)
                    {
                        if (colCount == 1) colWidths[0] = Math.Max(fixedRowNumWidth, usableWidth);
                        else
                        {
                            float remaining = usableWidth - fixedRowNumWidth;
                            float minRemainingTotal = (colCount - 1) * minColWidth;
                            if (remaining < minRemainingTotal) remaining = minRemainingTotal;
                            colWidths[0] = fixedRowNumWidth;
                            float otherWidth = remaining / (float)(colCount - 1);
                            for (int i = 1; i < colCount; i++) colWidths[i] = Math.Max(minColWidth, otherWidth);
                        }
                    }
                    else
                    {
                        float evenWidth = usableWidth / (float)colCount;
                        for (int i = 0; i < colCount; i++) colWidths[i] = Math.Max(50f, evenWidth);
                    }
                }

                // Compute header row height
                rowHeight = 0;
                for (int i = 0; i < colCount; i++)
                {
                    string headerText = _currentReport.Columns[i].ColumnName.Replace("_", " ");
                    int maxW = Math.Max(1, (int)Math.Floor(colWidths[i]));
                    SizeF sz = g.MeasureString(headerText, headerFont, maxW);
                    rowHeight = Math.Max(rowHeight, (int)sz.Height + 6);
                }

                // Draw header background & headers
                float totalTableWidth = colWidths.Sum();
                g.FillRectangle(Brushes.WhiteSmoke, startX - 2, startY + offsetY, totalTableWidth, rowHeight);
                g.DrawRectangle(Pens.Black, startX - 2, startY + offsetY, totalTableWidth, rowHeight);

                float colX = startX;
                for (int i = 0; i < colCount; i++)
                {
                    string headerText = _currentReport.Columns[i].ColumnName.Replace("_", " ");
                    g.DrawString(headerText, headerFont, Brushes.Black, new RectangleF(colX, startY + offsetY, colWidths[i], rowHeight));
                    g.DrawLine(Pens.Black, colX, startY + offsetY, colX, startY + offsetY + rowHeight);
                    colX += colWidths[i];
                }
                g.DrawLine(Pens.Black, colX, startY + offsetY, colX, startY + offsetY + rowHeight);
                offsetY += rowHeight;

                // === Rows & grouping ===
                object prevGroupValue = null;
                Dictionary<string, decimal> currentGroupTotals = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

                // Reserve space for footer + potential grand total block so rows don't overlap final totals.
                int reservedBottomSpace = footerHeight + 60; // 60px for grand total area (tweak if needed)
                int bottomMargin = e.MarginBounds.Bottom - reservedBottomSpace;

                while (_currentPrintRow < _currentReport.Rows.Count)
                {
                    DataRow row = _currentReport.Rows[_currentPrintRow];

                    // --- Group header check (if needed) ---
                    bool didDrawGroupHeader = false;
                    if (!string.IsNullOrWhiteSpace(_groupByColumn) && _currentReport.Columns.Contains(_groupByColumn))
                    {
                        object groupValue = row[_groupByColumn];
                        bool groupChanged = (prevGroupValue == null && groupValue != null) || (prevGroupValue != null && !prevGroupValue.Equals(groupValue));
                        if (groupChanged)
                        {
                            int gh = Math.Max(18, rowHeight);
                            // check fit for group header
                            if (startY + offsetY + gh > bottomMargin)
                            {
                                // before returning, draw footer for this page
                                drawFooter();
                                if (!_isMeasuring) _currentPage++;
                                e.HasMorePages = true;
                                return;
                            }

                            RectangleF ghRect = new RectangleF(startX - 2, startY + offsetY, totalTableWidth, gh);
                            g.FillRectangle(Brushes.LightGray, ghRect);
                            g.DrawRectangle(Pens.Black, Rectangle.Round(ghRect));
                            string gLabel = _groupByColumn + ": " + (groupValue?.ToString() ?? "(null)");
                            g.DrawString(gLabel, headerFont, Brushes.Black, new RectangleF(startX + 4, startY + offsetY + 2, totalTableWidth - 8, gh - 4));
                            offsetY += gh;
                            prevGroupValue = groupValue;
                            didDrawGroupHeader = true;
                        }
                    }

                    // --- measure row height ---
                    rowHeight = 0;
                    for (int c = 0; c < colCount; c++)
                    {
                        string txt = (row[c] == DBNull.Value) ? "" : row[c].ToString();
                        int maxW = Math.Max(1, (int)colWidths[c] - 6);
                        SizeF m = g.MeasureString(txt, font, maxW);
                        rowHeight = Math.Max(rowHeight, (int)m.Height + 6);
                    }
                    rowHeight = Math.Max(18, rowHeight);

                    // Check if row fits (consider reservedBottomSpace)
                    if (startY + offsetY + rowHeight > bottomMargin)
                    {
                        // Draw footer for this page before ending it
                        drawFooter();
                        if (!_isMeasuring) _currentPage++;
                        e.HasMorePages = true;
                        return;
                    }

                    // --- Draw row (only once) ---
                    Brush rowBrush = (_currentPrintRow % 2 == 0) ? Brushes.White : Brushes.LightGray;
                    RectangleF rowRect = new RectangleF(startX - 2, startY + offsetY, totalTableWidth, rowHeight);
                    g.FillRectangle(rowBrush, rowRect);
                    g.DrawRectangle(Pens.Black, Rectangle.Round(rowRect));

                    colX = startX;
                    for (int c = 0; c < colCount; c++)
                    {
                        string txt = (row[c] == DBNull.Value) ? "" : row[c].ToString();
                        RectangleF cellRect = new RectangleF(colX + 2, startY + offsetY + 2, colWidths[c] - 4, rowHeight - 4);
                        g.DrawString(txt, font, Brushes.Black, cellRect);
                        g.DrawLine(Pens.Black, colX, startY + offsetY, colX, startY + offsetY + rowHeight);

                        var colName = _currentReport.Columns[c].ColumnName;
                        if (_totalColumns.Contains(colName, StringComparer.OrdinalIgnoreCase) && decimal.TryParse(txt, out decimal val))
                        {
                            if (!currentGroupTotals.ContainsKey(colName)) currentGroupTotals[colName] = 0;
                            currentGroupTotals[colName] += val;
                        }

                        colX += colWidths[c];
                    }
                    g.DrawLine(Pens.Black, colX, startY + offsetY, colX, startY + offsetY + rowHeight);

                    // row consumed
                    offsetY += rowHeight;
                    _currentPrintRow++;
                } // end while rows

                // --- If we reached here, all rows were drawn on this page (or previous pages) ---
                // Draw any pending group subtotal (if grouping used)
                if (!string.IsNullOrWhiteSpace(_groupByColumn) && currentGroupTotals.Count > 0)
                {
                    int grpRowH = Math.Max(18, rowHeight);
                    if (startY + offsetY + grpRowH > e.MarginBounds.Bottom - footerHeight)
                    {
                        // draw footer for this page before starting next
                        drawFooter();
                        if (!_isMeasuring) _currentPage++;
                        e.HasMorePages = true;
                        return;
                    }

                    RectangleF grpRect = new RectangleF(startX - 2, startY + offsetY, totalTableWidth, grpRowH);
                    g.FillRectangle(Brushes.LightYellow, grpRect);
                    g.DrawRectangle(Pens.Black, Rectangle.Round(grpRect));
                    string label = $"Subtotal for {prevGroupValue}";
                    g.DrawString(label, headerFont, Brushes.Black, new RectangleF(startX + 2, startY + offsetY + 2, colWidths.Take(2).Sum(), grpRowH - 4));
                    for (int c = 0; c < colCount; c++)
                    {
                        var colName = _currentReport.Columns[c].ColumnName;
                        if (currentGroupTotals.ContainsKey(colName))
                        {
                            string val = currentGroupTotals[colName].ToString("N2");
                            float xPos = startX + colWidths.Take(c).Sum();
                            g.DrawString(val, font, Brushes.Black, new RectangleF(xPos + 2, startY + offsetY + 2, colWidths[c] - 4, grpRowH - 4));
                        }
                    }
                    offsetY += grpRowH;
                    currentGroupTotals.Clear();
                }

                // --- GRAND TOTAL (only after all rows are printed) ---
                if (_grandTotals != null && _grandTotals.Count > 0 && _totalColumns != null && _totalColumns.Count > 0)
                {
                    int grandBlockHeight = 28;
                    if (startY + offsetY + grandBlockHeight + footerHeight > e.MarginBounds.Bottom)
                    {
                        // draw footer for this page before continuing on next
                        drawFooter();
                        if (!_isMeasuring) _currentPage++;
                        e.HasMorePages = true;
                        return;
                    }

                    offsetY += 10;
                    float totalY = startY + offsetY;
                    g.DrawLine(Pens.Black, startX - 2, totalY, startX - 2 + totalTableWidth, totalY);
                    totalY += 4;

                    using (Font totalLabelFont = new Font("Arial", 8, FontStyle.Bold))
                    {
                        float labelWidth = (colCount >= 2) ? (colWidths[0] + colWidths[1]) : colWidths[0];
                        g.FillRectangle(Brushes.Beige, startX - 2, totalY, totalTableWidth, 20);
                        g.DrawRectangle(Pens.Black, startX - 2, totalY, totalTableWidth, 20);

                        g.DrawString("GRAND TOTAL", totalLabelFont, Brushes.Black, new RectangleF(startX + 4, totalY + 2, labelWidth - 8, 16));

                        colX = startX;
                        for (int c = 0; c < colCount; c++)
                        {
                            string colName = _currentReport.Columns[c].ColumnName;
                            if (_totalColumns.Contains(colName, StringComparer.OrdinalIgnoreCase) &&
                                _grandTotals.TryGetValue(colName, out decimal totalVal))
                            {
                                string val = (totalVal == Math.Floor(totalVal)) ? ((long)totalVal).ToString("N0") : totalVal.ToString("N2");
                                SizeF sz = g.MeasureString(val, font);
                                float cellRight = startX + colWidths.Take(c + 1).Sum();
                                float xRightAligned = cellRight - sz.Width - 4;
                                g.DrawString(val, font, Brushes.Black, xRightAligned, totalY + 2);
                            }
                            colX += colWidths[c];
                        }
                    }
                    offsetY += 25;
                }

                // --- Footer for final segment on this page ---
                drawFooter();

                // finalise page
                e.HasMorePages = false;
            } // using fonts
        }




        /// <summary>
        /// Measures pages in a safe loop to compute _totalPages
        /// </summary>
        private static void CalculateTotalPages(PrintDocument doc)
        {
            if (_currentReport == null) { _totalPages = 0; return; }

            _isMeasuring = true;
            _currentPrintRow = 0;
            _totalPages = 0;

            // Use actual page size from the PrintDocument
            Rectangle pageBounds = doc.DefaultPageSettings.Bounds;
            Rectangle marginBounds = new Rectangle(
                pageBounds.Left + doc.DefaultPageSettings.Margins.Left,
                pageBounds.Top + doc.DefaultPageSettings.Margins.Top,
                pageBounds.Width - doc.DefaultPageSettings.Margins.Left - doc.DefaultPageSettings.Margins.Right,
                pageBounds.Height - doc.DefaultPageSettings.Margins.Top - doc.DefaultPageSettings.Margins.Bottom
            );

            using (var bmp = new Bitmap(pageBounds.Width, pageBounds.Height))
            using (var g = Graphics.FromImage(bmp))
            {
                bool morePages = true;
                int safety = 0;

                while (morePages && safety++ < 2000)
                {
                    _totalPages++;

                    var fakeArgs = new PrintPageEventArgs(
                        g,
                        marginBounds,
                        pageBounds,
                        doc.DefaultPageSettings
                    )
                    { HasMorePages = false };

                    // PrintDocument_PrintPage will measure rows but skip drawing because _isMeasuring = true
                    PrintDocument_PrintPage(doc, fakeArgs);
                    morePages = fakeArgs.HasMorePages;
                }
            }

            _isMeasuring = false;
            _currentPrintRow = 0;
            _currentPage = 1;
        }

        public static void SetLongBondPaper(PrintDocument doc)
        {
            if (doc == null) return;

            // Long bond paper (typical 8.5" x 13") in hundredths of an inch
            int widthHundredths = (int)(8.5 * 100);   // 8.5 inches
            int heightHundredths = (int)(13 * 100);   // 13 inches

            PaperSize longBond = new PaperSize("Long Bond", widthHundredths, heightHundredths);

            // Assign custom paper size
            doc.DefaultPageSettings.PaperSize = longBond;

            // Optional: set landscape
            doc.DefaultPageSettings.Landscape = false; // portrait
        }


        /// <summary>
        /// Renders all pages to bitmaps (used by PDF exporter).
        /// </summary>
        private static List<Bitmap> RenderAllPagesToBitmaps()
        {
            if (_currentReport == null) throw new InvalidOperationException("No report prepared.");

            var doc = _printDocument ?? new PrintDocument();
            doc.DefaultPageSettings.Landscape = true;

            var pages = new List<Bitmap>();
            using (var dummy = new Bitmap(1200, 1600))
            using (var g = Graphics.FromImage(dummy))
            {
                bool more = true;
                _isMeasuring = false; _currentPrintRow = 0; _currentPage = 1;
                int safety = 0;
                while (more && safety++ < 2000)
                {
                    var pageBmp = new Bitmap(dummy.Width, dummy.Height, PixelFormat.Format32bppArgb);
                    using (var pg = Graphics.FromImage(pageBmp))
                    {
                        var fakeArgs = new PrintPageEventArgs(pg, new Rectangle(0, 0, pageBmp.Width, pageBmp.Height), new Rectangle(0, 0, pageBmp.Width, pageBmp.Height), doc.DefaultPageSettings);
                        fakeArgs.HasMorePages = false;
                        PrintDocument_PrintPage(doc, fakeArgs);
                        more = fakeArgs.HasMorePages;
                    }
                    pages.Add(pageBmp);
                    if (more) _currentPage++;
                }
            }

            _currentPrintRow = 0; _currentPage = 1;
            return pages;
        }

        // Utilities
        private static bool IsNumericType(Type t)
        {
            return t == typeof(int) || t == typeof(long) || t == typeof(float) || t == typeof(double) || t == typeof(decimal);
        }
    }
}
