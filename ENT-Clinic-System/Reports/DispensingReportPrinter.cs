using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Reports
{
    internal class DispensingReportPrinter
    {
        private DateTime DateFrom;
        private DateTime DateTo;
        private DataTable _data;
        private PrintDocument _printDocument;
        private int _currentRow = 0;

        // Adjusted column widths
        private int[] colWidths = { 70, 60, 120, 80, 150, 100, 60, 60, 60, 50, 60 };
        private string[] colNames = { "Date", "Invoice#", "Customer", "Prescription#", "Item Name", "Brand", "Strength", "Dosage", "Category", "Qty", "Total" };
        private int rowHeight = 20;

        public DispensingReportPrinter()
        {
            if (!ShowParameterForm()) return;
            LoadData();
            InitializePrinting();
            ShowPreview();
        }

        private bool ShowParameterForm()
        {
            using (Form form = new Form())
            {
                form.Text = "Dispensing Report Parameters";
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Width = 300;
                form.Height = 180;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                Label lblFrom = new Label() { Text = "From Date:", Left = 20, Top = 20, AutoSize = true };
                DateTimePicker dtpFrom = new DateTimePicker() { Left = 100, Top = 16, Width = 150, Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddDays(-7) };
                Label lblTo = new Label() { Text = "To Date:", Left = 20, Top = 60, AutoSize = true };
                DateTimePicker dtpTo = new DateTimePicker() { Left = 100, Top = 56, Width = 150, Format = DateTimePickerFormat.Short, Value = DateTime.Today };

                Button btnOk = new Button() { Text = "OK", Left = 40, Width = 80, Top = 100, DialogResult = DialogResult.OK };
                Button btnCancel = new Button() { Text = "Cancel", Left = 160, Width = 80, Top = 100, DialogResult = DialogResult.Cancel };

                form.Controls.Add(lblFrom);
                form.Controls.Add(dtpFrom);
                form.Controls.Add(lblTo);
                form.Controls.Add(dtpTo);
                form.Controls.Add(btnOk);
                form.Controls.Add(btnCancel);

                form.AcceptButton = btnOk;
                form.CancelButton = btnCancel;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (dtpFrom.Value.Date > dtpTo.Value.Date)
                    {
                        MessageBox.Show("From Date cannot be later than To Date.", "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    DateFrom = dtpFrom.Value.Date;
                    DateTo = dtpTo.Value.Date;
                    return true;
                }
                return false;
            }
        }

        private void LoadData()
        {
            using (MySqlConnection conn = DBConfig.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT i.invoice_date, i.invoice_id, i.customer_name,
                           ii.prescription_id, it.generic_name, it.brand_name, it.strength,
                           it.dosage, it.category, ii.quantity AS quantity_dispensed, ii.unit_price, ii.total_price
                    FROM invoice_items ii
                    JOIN invoices i ON ii.invoice_id = i.invoice_id
                    JOIN items it ON ii.item_id = it.item_id
                    WHERE i.invoice_date BETWEEN @from AND @to
                    ORDER BY i.invoice_date, i.invoice_id, it.generic_name";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@from", DateFrom);
                    cmd.Parameters.AddWithValue("@to", DateTo);
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        _data = new DataTable();
                        da.Fill(_data);
                    }
                }
            }
        }

        private void InitializePrinting()
        {
            _printDocument = new PrintDocument();
            _printDocument.DefaultPageSettings.Landscape = true;
            _printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        public void ShowPreview()
        {
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
                        if (item is ToolStripButton btn && btn.ToolTipText.ToLower().Contains("print"))
                            btn.Visible = false;

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

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int left = e.MarginBounds.Left;
            int top = e.MarginBounds.Top;
            int width = e.MarginBounds.Width;
            int y = top;

            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font columnFont = new Font("Arial", 10, FontStyle.Bold);
            Font rowFont = new Font("Arial", 9);
            Brush brush = Brushes.Black;

            // Header
            e.Graphics.DrawString("Dispensing Report", headerFont, brush, left + width / 3, y);
            y += 30;
            e.Graphics.DrawString($"Period: {DateFrom:d} - {DateTo:d}", rowFont, brush, left, y);
            y += 30;

            // Column headers
            int x = left;
            for (int i = 0; i < colNames.Length; i++)
            {
                RectangleF rect = new RectangleF(x, y, colWidths[i], rowHeight);
                StringFormat fmt = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(colNames[i], columnFont, brush, rect, fmt);
                x += colWidths[i];
            }
            y += rowHeight;

            bool alt = false;
            decimal totalQty = 0;
            decimal totalAmount = 0;
            int pageBottom = e.MarginBounds.Bottom;

            // Draw rows
            while (_currentRow < _data.Rows.Count)
            {
                DataRow row = _data.Rows[_currentRow];
                x = left;
                alt = !alt;

                int rowMaxHeight = rowHeight; // track tallest cell (for wrapped text)

                // Check if new page needed
                if (y + rowMaxHeight > pageBottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                // Background
                if (alt)
                    e.Graphics.FillRectangle(Brushes.LightGray, left, y, width, rowMaxHeight);

                // Draw each column with wrapping
                for (int i = 0; i < colNames.Length; i++)
                {
                    string text = row[i].ToString();
                    RectangleF rect = new RectangleF(x, y, colWidths[i], 1000); // large height to measure
                    StringFormat fmt = new StringFormat
                    {
                        Alignment = StringAlignment.Near,
                        LineAlignment = StringAlignment.Near,
                        Trimming = StringTrimming.Word,
                        FormatFlags = StringFormatFlags.LineLimit
                    };
                    SizeF measured = e.Graphics.MeasureString(text, rowFont, (int)colWidths[i]);
                    rowMaxHeight = Math.Max(rowMaxHeight, (int)measured.Height);

                    e.Graphics.DrawString(text, rowFont, brush, rect, fmt);
                    x += colWidths[i];
                }

                totalQty += Convert.ToDecimal(row["quantity_dispensed"]);
                totalAmount += Convert.ToDecimal(row["total_price"]);

                y += rowMaxHeight + 2; // advance by tallest cell
                _currentRow++;
            }

            // Draw totals
            y += 10;
            e.Graphics.DrawString("TOTALS:", columnFont, brush, left + 600, y);
            e.Graphics.DrawString(totalQty.ToString(), columnFont, brush, left + 660, y);
            e.Graphics.DrawString(totalAmount.ToString("N2"), columnFont, brush, left + 720, y);

            e.HasMorePages = false;
            _currentRow = 0;
        }
    }
}
