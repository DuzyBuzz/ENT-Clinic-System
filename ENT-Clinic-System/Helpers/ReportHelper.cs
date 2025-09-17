using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class ReportHelper
    {
        private static int currentRow;

        /// <summary>
        /// Generates a printable report from a DataGridView with system header and footer.
        /// </summary>
        /// <param name="dgv">The DataGridView to print.</param>
        public static void PrintDataGridView(DataGridView dgv, string reportTitle)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("No records to print.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentRow = 0;

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, e) => Pd_PrintPage(sender, e, dgv, reportTitle);

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = pd,
                WindowState = FormWindowState.Maximized
            };

            try
            {
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printing failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void Pd_PrintPage(object sender, PrintPageEventArgs e, DataGridView dgv, string reportTitle)
        {
            int startX = 50;
            int startY = 100; // leave space for header
            int offsetY = 0;
            int rowHeight = 30;
            Font font = new Font("Arial", 10);
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font footerFont = new Font("Arial", 10, FontStyle.Italic);
            Brush brush = Brushes.Black;

            int pageWidth = e.MarginBounds.Width;
            int pageHeight = e.MarginBounds.Height;

            // Get header and footer from settings
            string header = SettingsHelper.GetSetting("report_header") ?? reportTitle;
            string footer = SettingsHelper.GetSetting("report_footer") ?? "";

            try
            {
                // --- Print header ---
                SizeF headerSize = e.Graphics.MeasureString(header, headerFont);
                e.Graphics.DrawString(header, headerFont, brush,
                    startX + (pageWidth - headerSize.Width) / 2, startY - 80);

                // Print column headers
                int currentX = startX;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible)
                    {
                        e.Graphics.DrawString(col.HeaderText, font, brush, currentX, startY + offsetY);
                        currentX += col.Width;
                    }
                }
                offsetY += rowHeight;

                // --- Print rows ---
                while (currentRow < dgv.Rows.Count)
                {
                    DataGridViewRow row = dgv.Rows[currentRow];
                    currentX = startX;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (dgv.Columns[cell.ColumnIndex].Visible)
                        {
                            e.Graphics.DrawString(cell.Value?.ToString() ?? "", font, brush, currentX, startY + offsetY);
                            currentX += dgv.Columns[cell.ColumnIndex].Width;
                        }
                    }

                    offsetY += rowHeight;

                    // Check if page bottom reached
                    if (startY + offsetY + rowHeight + 50 > pageHeight) // 50px for footer
                    {
                        // --- Print footer ---
                        e.Graphics.DrawString(footer, footerFont, brush,
                            startX, pageHeight - 40);

                        currentRow++;
                        e.HasMorePages = true;
                        return;
                    }

                    currentRow++;
                }

                // --- Print footer on last page ---
                e.Graphics.DrawString(footer, footerFont, brush, startX, pageHeight - 40);

                e.HasMorePages = false;
                currentRow = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while printing: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.HasMorePages = false;
                currentRow = 0;
            }
        }
    }
}
