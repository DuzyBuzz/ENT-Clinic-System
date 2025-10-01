using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Reports
{
    internal class ExpiryReport
    {
        private PrintDocument printDocument;
        private List<(string ItemName, string Category, string Description, int Quantity, DateTime Expiration, string Note)> data
            = new List<(string, string, string, int, DateTime, string)>();

        public ExpiryReport()
        {
            LoadData();

            printDocument = new PrintDocument
            {
                DefaultPageSettings = { Landscape = true } // Set landscape
            };
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void LoadData()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT item_name, category, description, quantity, expiration_date, note
                        FROM expiry_report
                        ORDER BY expiration_date";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data.Add((
                                    reader["item_name"].ToString(),
                                    reader["category"].ToString(),
                                    reader["description"].ToString(),
                                    Convert.ToInt32(reader["quantity"]),
                                    Convert.ToDateTime(reader["expiration_date"]),
                                    reader["note"].ToString()
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading expiry data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int left = 40;
            int top = 40;
            int width = e.PageBounds.Width;

            // Header
            using (Font headerFont = new Font("Segoe UI", 14, FontStyle.Bold))
                g.DrawString("EXPIRY REPORT", headerFont, Brushes.Black,
                    new RectangleF(left, top, width - 2 * left, 30),
                    new StringFormat() { Alignment = StringAlignment.Center });
            top += 40;

            using (Font font = new Font("Segoe UI", 10))
            {
                // Column positions
                int xItem = left;
                int xCategory = xItem + 200;
                int xDescription = xCategory + 150;
                int xQty = xDescription + 200;
                int xExp = xQty + 80;
                int xNote = xExp + 100;

                // Column headers
                g.DrawString("Item Name", font, Brushes.Black, xItem, top);
                g.DrawString("Category", font, Brushes.Black, xCategory, top);
                g.DrawString("Description", font, Brushes.Black, xDescription, top);
                g.DrawString("Qty", font, Brushes.Black, xQty, top);
                g.DrawString("Expiration", font, Brushes.Black, xExp, top);
                g.DrawString("Note", font, Brushes.Black, xNote, top);
                top += 25;

                g.DrawLine(Pens.Black, left, top, width - left, top);
                top += 5;

                // Print rows
                foreach (var row in data)
                {
                    g.DrawString(row.ItemName, font, Brushes.Black, xItem, top);
                    g.DrawString(row.Category, font, Brushes.Black, xCategory, top);
                    g.DrawString(row.Description, font, Brushes.Black, xDescription, top);
                    g.DrawString(row.Quantity.ToString(), font, Brushes.Black, xQty, top);
                    g.DrawString(row.Expiration.ToString("MM/dd/yyyy"), font, Brushes.Black, xExp, top);
                    g.DrawString(row.Note, font, Brushes.Black, xNote, top);
                    top += 25;

                    if (top > e.PageBounds.Height - 80)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }

                // Footer
                top = e.PageBounds.Height - 60;
                g.DrawString("Printed By: " + UserCredentials.Fullname, font, Brushes.Black, left, top);
                g.DrawString("Date/Time: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"), font, Brushes.Black, width - 300, top);
            }
        }

        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1000,
                Height = 700,
                Text = "Expiry Report Preview"
            };

            preview.Shown += (s, e) =>
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
                        using (PrintDialog pd = new PrintDialog { Document = printDocument })
                        {
                            if (pd.ShowDialog() == DialogResult.OK)
                                printDocument.Print();
                        }
                    };
                    tool.Items.Insert(0, customPrint);
                }
            };

            preview.ShowDialog();
        }
    }
}
