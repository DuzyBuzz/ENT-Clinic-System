//using MySql.Data.MySqlClient;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Drawing.Printing;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Windows.Forms;
////using PdfSharp.Pdf;
////using PdfSharp.Drawing;
////using ClosedXML.Excel;

//namespace ENT_Clinic_System.Helpers
//{
//    /// <summary>
//    /// Simple, copy/paste-ready ReportHelper.
//    /// Call GenerateReport(...) then ShowPreview().
//    /// </summary>
//    public static class ReportHelper
//    {
//        // Printing state
//        private static PrintDocument _printDocument;
//        private static DataTable _currentReport;
//        private static int _currentPrintRow = 0;
//        private static int _currentPage = 1;
//        private static int _totalPages = 0;
//        private static bool _isMeasuring = false;

//        // Options stored for printing/export
//        private static string _reportTitle = "";
//        private static string _reportSubtitle = "";
//        private static DateTime? _dateFrom = null;
//        private static DateTime? _dateTo = null;
//        private static bool _showPageNumbers = true;
//        private static Image _headerImage = null;
//        private static string _rowNumberHeader = "#";
//        private static bool _includeRowNumbers = false;
//        private static List<string> _totalColumns = new List<string>();
//        private static string _groupByColumn = null; // optional grouping column

//        // Keep numeric grand totals computed ahead for footer or last row
//        private static Dictionary<string, decimal> _grandTotals = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
//        private static Image _headerLeftImage;
//        private static Image _headerRightImage;

//        public static void SetHeaderImages(Image leftImage, Image rightImage)
//        {
//            _headerLeftImage = leftImage;
//            _headerRightImage = rightImage;
//        }

//        // -----------------------
//        // Reusable convenience overload
//        // -----------------------
//        /// <summary>
//        /// Convenience overload: filtersOrZone may be:
//        /// - null
//        /// - Dictionary<string, object>
//        /// - string (treated as Zone = value)
//        /// - anonymous object (public properties converted to dictionary)
//        /// Calls the existing simplified GenerateReport that takes Dictionary filters.
//        /// </summary>
//        public static void GenerateReport(
//           string tableName,
//           List<string> displayColumns,
//           Dictionary<string, object> filters = null,
//           string reportTitle = "",
//           string reportSubtitle = "",
//           bool showRowNumbers = false,
//           bool landscape = true,
//           string groupBy = null,
//           List<string> totalColumns = null
//       )
//        {
//            _reportTitle = reportTitle;
//            _reportSubtitle = reportSubtitle ?? "";
//            _dateFrom = null;
//            _dateTo = null;
//            _showPageNumbers = true;
//            _includeRowNumbers = showRowNumbers;
//            _rowNumberHeader = "#";
//            _totalColumns = totalColumns ?? new List<string>();
//            _groupByColumn = groupBy;
//            _headerImage = null;

//            // Build base SQL
//            string cols = string.Join(", ", displayColumns);
//            string query = $"SELECT {cols} FROM {tableName}";

//            // Build filter WHERE clause if needed
//            // NOTE: we generate safe parameter names (@p0, @p1, ...) so filter keys may be
//            // expressions like "MONTH(Collection_Date)" or simple column names like "Zone".
//            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>(); // (originalKey, paramName)
//            if (filters != null && filters.Count > 0)
//            {
//                List<string> conds = new List<string>();
//                int i = 0;
//                foreach (var kv in filters)
//                {
//                    // create a safe parameter name
//                    string paramName = $"p{i++}";        // will become @p0, @p1, ...
//                                                         // left side uses the raw key (so expressions like MONTH(col) are allowed)
//                    conds.Add($"{kv.Key} = @{paramName}");
//                    paramList.Add(new KeyValuePair<string, string>(paramName, kv.Value?.ToString() ?? ""));
//                }
//                query += " WHERE " + string.Join(" AND ", conds);
//            }

//            // Add optional group ordering
//            if (!string.IsNullOrWhiteSpace(_groupByColumn))
//                query += $" ORDER BY {_groupByColumn}";

//            DataTable dt = new DataTable();
//            using (var conn = DBConfig.GetConnection())
//            using (var cmd = new MySqlCommand(query, conn))
//            using (var adapter = new MySqlDataAdapter(cmd))
//            {
//                // add parameters using the safe names created above
//                // we iterate over filters again to get actual values (preserve original order)
//                if (filters != null && filters.Count > 0)
//                {
//                    int idx = 0;
//                    foreach (var kv in filters)
//                    {
//                        string paramName = $"@p{idx++}";
//                        // add parameter using the original value (not the stringified one)
//                        cmd.Parameters.AddWithValue(paramName, kv.Value ?? DBNull.Value);
//                    }
//                }
//                conn.Open();
//                adapter.Fill(dt);

//                // --- Format DateTime columns to date-only strings safely ---
//                List<DataColumn> dateCols = dt.Columns.Cast<DataColumn>()
//                                                     .Where(c => c.DataType == typeof(DateTime))
//                                                     .ToList();

//                foreach (DataColumn col in dateCols)
//                {
//                    string newColName = col.ColumnName + "_str";
//                    DataColumn newCol = new DataColumn(newColName, typeof(string));
//                    dt.Columns.Add(newCol);

//                    foreach (DataRow row in dt.Rows)
//                    {
//                        if (row[col] != DBNull.Value)
//                            row[newCol] = ((DateTime)row[col]).ToString("yyyy-MM-dd");
//                        else
//                            row[newCol] = "";
//                    }

//                    int ordinal = col.Ordinal;
//                    dt.Columns.Remove(col);
//                    newCol.ColumnName = col.ColumnName;
//                    newCol.SetOrdinal(ordinal);
//                }
//            }

//            // Add row numbers
//            if (showRowNumbers)
//            {
//                if (!dt.Columns.Contains(_rowNumberHeader))
//                {
//                    DataColumn rc = new DataColumn(_rowNumberHeader, typeof(string));
//                    dt.Columns.Add(rc);
//                    rc.SetOrdinal(0);
//                }
//                for (int i = 0; i < dt.Rows.Count; i++)
//                    dt.Rows[i][_rowNumberHeader] = (i + 1).ToString();
//            }

//            // set current report
//            _currentReport = dt.Copy();

//            // ------------------------------
//            // PRECOMPUTE GRAND TOTALS (FIX)
//            // ------------------------------
//            _grandTotals = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
//            if (_totalColumns != null && _totalColumns.Count > 0)
//            {
//                foreach (var colName in _totalColumns)
//                {
//                    decimal sum = 0m;
//                    if (_currentReport.Columns.Contains(colName))
//                    {
//                        foreach (DataRow r in _currentReport.Rows)
//                        {
//                            if (r[colName] != DBNull.Value && decimal.TryParse(r[colName].ToString(), out decimal v))
//                                sum += v;
//                        }
//                    }
//                    _grandTotals[colName] = sum;
//                }
//            }

//            _currentPrintRow = 0;
//            _currentPage = 1;
//            _totalPages = 0;
//            _isMeasuring = false;

//            _printDocument = new PrintDocument();
//            _printDocument.DefaultPageSettings.Landscape = landscape;

//            // Compute total pages BEFORE preview/printing
//            CalculateTotalPages(_printDocument);
//            _printDocument.PrintPage += PrintDocument_PrintPage;
//        }



//        /// <summary>
//        /// Shows a PrintPreviewDialog with custom buttons:
//        /// - Print (shows PrintDialog to let user select a printer)
//        /// - Export PDF (requires PdfSharp or similar - helpful error if missing)
//        /// - Export Excel (requires ClosedXML - helpful error if missing)
//        /// </summary>
//        public static void ShowPreview()
//        {
//            if (_printDocument == null || _currentReport == null)
//            {
//                MessageBox.Show("No report loaded to print.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }


//            // === Create preview dialog ===
//            PrintPreviewDialog preview = new PrintPreviewDialog
//            {
//                Document = _printDocument,
//                Width = 1000,
//                Height = 750,
//                Icon = Properties.Resources.IGW_Logo
//            };


//            preview.Shown += (s, e) =>
//            {
//                var tool = preview.Controls.OfType<ToolStrip>().FirstOrDefault();
//                if (tool != null)
//                {
//                    // Hide default print button
//                    foreach (ToolStripItem item in tool.Items)
//                    {
//                        if (item is ToolStripButton btn && btn.ToolTipText != null && btn.ToolTipText.ToLower().Contains("print"))
//                            btn.Visible = false;
//                    }

//                    // === Helper to make styled buttons ===
//                    ToolStripButton MakeButton(string text, Image icon, string tooltip)
//                    {
//                        var btn = new ToolStripButton
//                        {
//                            Text = text,
//                            Image = icon,
//                            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
//                            ImageAlign = ContentAlignment.MiddleLeft,
//                            TextAlign = ContentAlignment.MiddleRight,
//                            ToolTipText = tooltip,
//                            Margin = new Padding(3, 1, 3, 2),
//                            ForeColor = Color.Black
//                        };
//                        return btn;
//                    }

//                    // === Safely load icons (convert .ico -> Bitmap) ===
//                    Image printIcon = (Properties.Resources.print is Icon) ? ((Icon)Properties.Resources.print).ToBitmap() : Properties.Resources.print_image;
//                    Image pdfIcon = (Properties.Resources.pdf is Icon) ? ((Icon)Properties.Resources.pdf).ToBitmap() : Properties.Resources.pdf_image;
//                    Image excelIcon = (Properties.Resources.excel is Icon) ? ((Icon)Properties.Resources.excel).ToBitmap() : Properties.Resources.excel_image;

//                    // === Print Button ===
//                    var printBtn = MakeButton("Print", printIcon, "Select printer and print report");
//                    printBtn.Click += (ss, ee) =>
//                    {
//                        using (var pd = new PrintDialog { Document = _printDocument })
//                        {
//                            if (pd.ShowDialog() == DialogResult.OK)
//                            {
//                                _printDocument.PrinterSettings = pd.PrinterSettings;
//                                _printDocument.Print();
//                            }
//                        }
//                    };

//                    // === Print to PDF Button ===
//                    var pdfBtn = MakeButton("Print to PDF", pdfIcon, "Save report as PDF using Microsoft Print to PDF");
//                    pdfBtn.Click += (ss, ee) =>
//                    {
//                        using (var sfd = new SaveFileDialog
//                        {
//                            Filter = "PDF file (*.pdf)|*.pdf",
//                            FileName = "report.pdf",
//                            Title = "Save PDF File"
//                        })
//                        {
//                            if (sfd.ShowDialog() == DialogResult.OK)
//                            {
//                                try
//                                {
//                                    // Create a PrintDocument with the "Microsoft Print to PDF" printer
//                                    using (PrintDocument pd = new PrintDocument())
//                                    {
//                                        pd.DocumentName = "Report";
//                                        pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
//                                        pd.PrinterSettings.PrintToFile = true;
//                                        pd.PrinterSettings.PrintFileName = sfd.FileName;

//                                        // Use your same print logic (page setup, handlers, etc.)
//                                        pd.PrintPage += PrintDocument_PrintPage; // same event you use for preview
//                                        pd.Print();

//                                        MessageBox.Show("PDF successfully created.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                                    }
//                                }
//                                catch (Exception ex)
//                                {
//                                    MessageBox.Show("PDF printing failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                }
//                            }
//                        }
//                    };


//                    // === Export Excel Button ===
//                    var xlsBtn = MakeButton("Export Excel", excelIcon, "Export report to Excel");
//                    xlsBtn.Click += (ss, ee) =>
//                    {
//                        using (var sfd = new SaveFileDialog { Filter = "Excel file (*.xlsx)|*.xlsx", FileName = "report.xlsx" })
//                        {
//                            if (sfd.ShowDialog() == DialogResult.OK)
//                            {
//                                try
//                                {
//                                    ExportToExcel(sfd.FileName);
//                                    MessageBox.Show("Excel exported successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                                }
//                                catch (Exception ex)
//                                {
//                                    MessageBox.Show("Excel export failed: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                                }
//                            }
//                        }
//                    };

//                    // Insert buttons
//                    tool.Items.Insert(0, printBtn);
//                    tool.Items.Insert(1, pdfBtn);
//                    tool.Items.Insert(2, xlsBtn);
//                }
//            };

//            preview.ShowDialog();
//        }

//        public static void ExportToExcel(string filePath)
//        {
//            if (_currentReport == null)
//                throw new InvalidOperationException("No report data found to export.");

//            using (var workbook = new XLWorkbook())
//            {
//                var ws = workbook.Worksheets.Add("Report");
//                ws.Cell(1, 1).InsertTable(_currentReport, "Report", true);
//                ws.Columns().AdjustToContents();

//                // approximate narrow first column width so the '#' column appears small in Excel as well
//                try
//                {
//                    ws.Column(1).Width = 6.5; // tweak if needed
//                }
//                catch { }

//                workbook.SaveAs(filePath);
//            }
//        }

//        // ----------------------
//        // Printing: keep your dgv-like layout, with small defensive fixes
//        // ----------------------
//        private static void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
//        {
//            if (_currentReport == null) return;

//            int margin = 10;
//            int startX = e.PageBounds.Left + margin;
//            int startY = e.PageBounds.Top + margin;
//            int footerHeight = 20;
//            int offsetY = 0;
//            int rowHeight = 0;

//            using (Font font = new Font("Arial", 7))
//            using (Font headerFont = new Font("Arial", 8, FontStyle.Bold))
//            using (Font titleFont = new Font("Arial", 12, FontStyle.Bold))
//            using (Font dateFont = new Font("Arial", 8, FontStyle.Italic))
//            using (Font pageFont = new Font("Arial", 8, FontStyle.Italic))
//            {
//                Graphics g = e.Graphics;

//                // === Header images and title ===
//                int headerImgSize = 60;
//                if (_headerLeftImage != null)
//                    g.DrawImage(_headerLeftImage, e.MarginBounds.Left + 100, e.MarginBounds.Top - 90, 50, 50);

//                if (_headerRightImage != null)
//                    g.DrawImage(_headerRightImage, e.MarginBounds.Right - 150, e.MarginBounds.Top - 90, 50, 50);

//                if (!string.IsNullOrWhiteSpace(_reportTitle))
//                {
//                    StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
//                    float midX = e.PageBounds.Left + e.PageBounds.Width / 2f;
//                    g.DrawString(_reportTitle, titleFont, Brushes.Black, new PointF(midX, startY + 18), center);
//                }

//                if (!string.IsNullOrWhiteSpace(_reportSubtitle))
//                {
//                    StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
//                    float midX = e.PageBounds.Left + e.PageBounds.Width / 2f;
//                    g.DrawString(_reportSubtitle, dateFont, Brushes.Black, new PointF(midX, startY + 34), center);
//                }

//                offsetY += headerImgSize + 8;

//                // === Date range display ===
//                if (_dateFrom.HasValue || _dateTo.HasValue)
//                {
//                    string dateRange = _dateFrom.HasValue && _dateTo.HasValue
//                        ? $"From: {_dateFrom.Value:yyyy-MM-dd}  To: {_dateTo.Value:yyyy-MM-dd}"
//                        : (_dateFrom.HasValue ? $"{_dateFrom.Value:yyyy-MM-dd}" : $"{_dateTo.Value:yyyy-MM-dd}");

//                    SizeF dateSize = g.MeasureString(dateRange, dateFont);
//                    float rightX = e.PageBounds.Right - margin - dateSize.Width;
//                    g.DrawString(dateRange, dateFont, Brushes.Black, rightX, startY + offsetY - 6);
//                    offsetY += 18;
//                }

//                // === Column headers ===
//                int colCount = _currentReport.Columns.Count;
//                if (colCount == 0) return;

//                float[] colWidths = new float[colCount];
//                int usableWidth = Math.Max(100, e.PageBounds.Width - margin * 4);
//                float fixedRowNumWidth = 40f;

//                if (_includeRowNumbers)
//                {
//                    if (colCount == 1)
//                        colWidths[0] = Math.Max(fixedRowNumWidth, usableWidth);
//                    else
//                    {
//                        float remaining = usableWidth - fixedRowNumWidth;
//                        float minRemainingTotal = (colCount - 1) * 40f;
//                        if (remaining < minRemainingTotal) remaining = minRemainingTotal;
//                        colWidths[0] = fixedRowNumWidth;
//                        float otherWidth = remaining / (float)(colCount - 1);
//                        for (int i = 1; i < colCount; i++)
//                            colWidths[i] = Math.Max(40f, otherWidth);
//                    }
//                }
//                else
//                {
//                    float evenWidth = usableWidth / (float)colCount;
//                    for (int i = 0; i < colCount; i++)
//                        colWidths[i] = Math.Max(50f, evenWidth);
//                }

//                // Compute header row height
//                rowHeight = 0;
//                for (int i = 0; i < colCount; i++)
//                {
//                    string headerText = _currentReport.Columns[i].ColumnName.Replace("_", " ");
//                    SizeF sz = g.MeasureString(headerText, headerFont, (int)Math.Floor(colWidths[i]));
//                    rowHeight = Math.Max(rowHeight, (int)sz.Height + 6);
//                }

//                // Draw header background
//                float totalTableWidth = colWidths.Sum();
//                g.FillRectangle(Brushes.WhiteSmoke, startX - 2, startY + offsetY, totalTableWidth, rowHeight);
//                g.DrawRectangle(Pens.Black, startX - 2, startY + offsetY, totalTableWidth, rowHeight);

//                // Draw column headers
//                float colX = startX;
//                for (int i = 0; i < colCount; i++)
//                {
//                    string headerText = _currentReport.Columns[i].ColumnName.Replace("_", " ");
//                    g.DrawString(headerText, headerFont, Brushes.Black, new RectangleF(colX, startY + offsetY, colWidths[i], rowHeight));
//                    g.DrawLine(Pens.Black, colX, startY + offsetY, colX, startY + offsetY + rowHeight);
//                    colX += colWidths[i];
//                }
//                g.DrawLine(Pens.Black, colX, startY + offsetY, colX, startY + offsetY + rowHeight);
//                offsetY += rowHeight;

//                // === Rows & grouping ===
//                object prevGroupValue = null;
//                Dictionary<string, decimal> currentGroupTotals = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

//                int bottomMargin = e.MarginBounds.Bottom - footerHeight - 5;

//                while (_currentPrintRow < _currentReport.Rows.Count)
//                {
//                    DataRow row = _currentReport.Rows[_currentPrintRow];

//                    // --- Group header & totals ---
//                    if (!string.IsNullOrWhiteSpace(_groupByColumn) && _currentReport.Columns.Contains(_groupByColumn))
//                    {
//                        object groupValue = row[_groupByColumn];
//                        bool groupChanged = (prevGroupValue == null && groupValue != null) || (prevGroupValue != null && !prevGroupValue.Equals(groupValue));

//                        if (groupChanged)
//                        {
//                            if (prevGroupValue != null && currentGroupTotals.Count > 0)
//                            {
//                                // Draw group subtotal
//                                int grpRowH = Math.Max(18, rowHeight);
//                                RectangleF grpRect = new RectangleF(startX - 2, startY + offsetY, totalTableWidth, grpRowH);
//                                g.FillRectangle(Brushes.LightYellow, grpRect);
//                                g.DrawRectangle(Pens.Black, Rectangle.Round(grpRect));

//                                string label = $"Subtotal for {prevGroupValue}";
//                                g.DrawString(label, headerFont, Brushes.Black, new RectangleF(startX + 2, startY + offsetY + 2, colWidths.Take(2).Sum(), grpRowH - 4));

//                                for (int c = 0; c < colCount; c++)
//                                {
//                                    var colName = _currentReport.Columns[c].ColumnName;
//                                    if (currentGroupTotals.ContainsKey(colName))
//                                    {
//                                        string val = currentGroupTotals[colName].ToString("N2");
//                                        float xPos = startX + colWidths.Take(c).Sum();
//                                        g.DrawString(val, font, Brushes.Black, new RectangleF(xPos + 2, startY + offsetY + 2, colWidths[c] - 4, grpRowH - 4));
//                                    }
//                                }

//                                offsetY += grpRowH;
//                                currentGroupTotals.Clear();
//                            }

//                            // Draw new group header
//                            int gh = Math.Max(18, rowHeight);
//                            RectangleF ghRect = new RectangleF(startX - 2, startY + offsetY, totalTableWidth, gh);
//                            g.FillRectangle(Brushes.LightGray, ghRect);
//                            g.DrawRectangle(Pens.Black, Rectangle.Round(ghRect));
//                            string gLabel = _groupByColumn + ": " + (groupValue?.ToString() ?? "(null)");
//                            g.DrawString(gLabel, headerFont, Brushes.Black, new RectangleF(startX + 4, startY + offsetY + 2, totalTableWidth - 8, gh - 4));
//                            offsetY += gh;

//                            prevGroupValue = groupValue;
//                        }
//                    }

//                    // --- Row height calculation ---
//                    rowHeight = 0;
//                    for (int c = 0; c < colCount; c++)
//                    {
//                        string txt = (row[c] == DBNull.Value) ? "" : row[c].ToString();
//                        SizeF m = g.MeasureString(txt, font, (int)colWidths[c] - 6);
//                        rowHeight = Math.Max(rowHeight, (int)m.Height + 6);
//                    }
//                    rowHeight = Math.Max(18, rowHeight);

//                    // --- Row background ---
//                    Brush rowBrush = (_currentPrintRow % 2 == 0) ? Brushes.White : Brushes.LightGray;
//                    RectangleF rowRect = new RectangleF(startX - 2, startY + offsetY, totalTableWidth, rowHeight);
//                    g.FillRectangle(rowBrush, rowRect);
//                    g.DrawRectangle(Pens.Black, Rectangle.Round(rowRect));

//                    // --- Draw cells ---
//                    colX = startX;
//                    for (int c = 0; c < colCount; c++)
//                    {
//                        string txt = (row[c] == DBNull.Value) ? "" : row[c].ToString();
//                        RectangleF cellRect = new RectangleF(colX + 2, startY + offsetY + 2, colWidths[c] - 4, rowHeight - 4);
//                        g.DrawString(txt, font, Brushes.Black, cellRect);
//                        g.DrawLine(Pens.Black, colX, startY + offsetY, colX, startY + offsetY + rowHeight);

//                        // Accumulate group totals
//                        var colName = _currentReport.Columns[c].ColumnName;
//                        if (_totalColumns.Contains(colName, StringComparer.OrdinalIgnoreCase) && decimal.TryParse(txt, out decimal val))
//                        {
//                            if (!currentGroupTotals.ContainsKey(colName)) currentGroupTotals[colName] = 0;
//                            currentGroupTotals[colName] += val;
//                        }

//                        colX += colWidths[c];
//                    }
//                    g.DrawLine(Pens.Black, colX, startY + offsetY, colX, startY + offsetY + rowHeight);

//                    offsetY += rowHeight;
//                    _currentPrintRow++;

//                    // --- Page break detection ---
//                    if (startY + offsetY > bottomMargin)
//                    {
//                        if (!_isMeasuring) _currentPage++;
//                        e.HasMorePages = true;
//                        return;
//                    }
//                }

//                // --- Remaining group totals ---
//                if (!string.IsNullOrWhiteSpace(_groupByColumn) && currentGroupTotals.Count > 0)
//                {
//                    int grpRowH = Math.Max(18, rowHeight);
//                    RectangleF grpRect = new RectangleF(startX - 2, startY + offsetY, totalTableWidth, grpRowH);
//                    g.FillRectangle(Brushes.LightYellow, grpRect);
//                    g.DrawRectangle(Pens.Black, Rectangle.Round(grpRect));

//                    string label = $"Subtotal for {prevGroupValue}";
//                    g.DrawString(label, headerFont, Brushes.Black, new RectangleF(startX + 2, startY + offsetY + 2, colWidths.Take(2).Sum(), grpRowH - 4));

//                    for (int c = 0; c < colCount; c++)
//                    {
//                        var colName = _currentReport.Columns[c].ColumnName;
//                        if (currentGroupTotals.ContainsKey(colName))
//                        {
//                            string val = currentGroupTotals[colName].ToString("N2");
//                            float xPos = startX + colWidths.Take(c).Sum();
//                            g.DrawString(val, font, Brushes.Black, new RectangleF(xPos + 2, startY + offsetY + 2, colWidths[c] - 4, grpRowH - 4));
//                        }
//                    }
//                    offsetY += grpRowH;
//                    currentGroupTotals.Clear();
//                }

//                // === GRAND TOTAL ROW ===
//                if (_grandTotals != null && _grandTotals.Count > 0 && _totalColumns != null && _totalColumns.Count > 0)
//                {
//                    offsetY += 10;
//                    float totalY = startY + offsetY;
//                    g.DrawLine(Pens.Black, startX - 2, totalY, startX - 2 + totalTableWidth, totalY);
//                    totalY += 4;

//                    using (Font totalLabelFont = new Font("Arial", 8, FontStyle.Bold))
//                    {
//                        float labelWidth = (colCount >= 2) ? (colWidths[0] + colWidths[1]) : colWidths[0];
//                        g.FillRectangle(Brushes.Beige, startX - 2, totalY, totalTableWidth, 20);
//                        g.DrawRectangle(Pens.Black, startX - 2, totalY, totalTableWidth, 20);

//                        g.DrawString("GRAND TOTAL", totalLabelFont, Brushes.Black, new RectangleF(startX + 4, totalY + 2, labelWidth - 8, 16));

//                        colX = startX;
//                        for (int c = 0; c < colCount; c++)
//                        {
//                            string colName = _currentReport.Columns[c].ColumnName;
//                            if (_totalColumns.Contains(colName, StringComparer.OrdinalIgnoreCase) &&
//                                _grandTotals.TryGetValue(colName, out decimal totalVal))
//                            {
//                                string val = (totalVal == Math.Floor(totalVal)) ? ((long)totalVal).ToString("N0") : totalVal.ToString("N2");
//                                SizeF sz = g.MeasureString(val, font);
//                                float cellRight = startX + colWidths.Take(c + 1).Sum();
//                                float xRightAligned = cellRight - sz.Width - 4;
//                                g.DrawString(val, font, Brushes.Black, xRightAligned, totalY + 2);
//                            }
//                            colX += colWidths[c];
//                        }
//                    }
//                    offsetY += 25;
//                }

//                // === Footer ===
//                if (!_isMeasuring)
//                {
//                    RectangleF footerRect = new RectangleF(
//                        e.PageBounds.Left + 5,
//                        e.PageBounds.Bottom - footerHeight - 5,
//                        e.PageBounds.Width - 10,
//                        footerHeight
//                    );
//                    g.FillRectangle(Brushes.White, footerRect);
//                    g.DrawString("Printed: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), pageFont, Brushes.Black, footerRect.Left + 2, footerRect.Top + 2);

//                    if (_showPageNumbers)
//                    {
//                        string pageText = $"Page {_currentPage}";
//                        SizeF pageSize = g.MeasureString(pageText, pageFont);
//                        g.DrawString(pageText, pageFont, Brushes.Black, footerRect.Right - pageSize.Width - 2, footerRect.Top + 2);
//                    }
//                }

//                e.HasMorePages = false;
//            }
//        }


//        /// <summary>
//        /// Measures pages in a safe loop to compute _totalPages
//        /// </summary>
//        private static void CalculateTotalPages(PrintDocument doc)
//        {
//            _isMeasuring = true;
//            _totalPages = 0;
//            _currentPrintRow = 0;

//            using (var bmp = new Bitmap(1200, 1600))
//            using (var g = Graphics.FromImage(bmp))
//            {
//                bool morePages = true;
//                int safety = 0;
//                while (morePages && safety++ < 2000)
//                {
//                    _totalPages++;
//                    var fakeArgs = new PrintPageEventArgs(g, new Rectangle(0, 0, bmp.Width, bmp.Height), new Rectangle(0, 0, bmp.Width, bmp.Height), doc.DefaultPageSettings);
//                    fakeArgs.HasMorePages = false;
//                    PrintDocument_PrintPage(doc, fakeArgs);
//                    morePages = fakeArgs.HasMorePages;
//                }
//            }

//            _isMeasuring = false;
//            _currentPrintRow = 0;
//            _currentPage = 1;
//        }

//        /// <summary>
//        /// Renders all pages to bitmaps (used by PDF exporter).
//        /// </summary>
//        private static List<Bitmap> RenderAllPagesToBitmaps()
//        {
//            if (_currentReport == null) throw new InvalidOperationException("No report prepared.");

//            var doc = _printDocument ?? new PrintDocument();
//            doc.DefaultPageSettings.Landscape = true;

//            var pages = new List<Bitmap>();
//            using (var dummy = new Bitmap(1200, 1600))
//            using (var g = Graphics.FromImage(dummy))
//            {
//                bool more = true;
//                _isMeasuring = false; _currentPrintRow = 0; _currentPage = 1;
//                int safety = 0;
//                while (more && safety++ < 2000)
//                {
//                    var pageBmp = new Bitmap(dummy.Width, dummy.Height, PixelFormat.Format32bppArgb);
//                    using (var pg = Graphics.FromImage(pageBmp))
//                    {
//                        var fakeArgs = new PrintPageEventArgs(pg, new Rectangle(0, 0, pageBmp.Width, pageBmp.Height), new Rectangle(0, 0, pageBmp.Width, pageBmp.Height), doc.DefaultPageSettings);
//                        fakeArgs.HasMorePages = false;
//                        PrintDocument_PrintPage(doc, fakeArgs);
//                        more = fakeArgs.HasMorePages;
//                    }
//                    pages.Add(pageBmp);
//                    if (more) _currentPage++;
//                }
//            }

//            _currentPrintRow = 0; _currentPage = 1;
//            return pages;
//        }

//        // Utilities
//        private static bool IsNumericType(Type t)
//        {
//            return t == typeof(int) || t == typeof(long) || t == typeof(float) || t == typeof(double) || t == typeof(decimal);
//        }
//    }
//}
