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
    internal class LowStockReport
    {
        private PrintDocument printDocument;
        private List<(string ItemName, string Category, int Quantity, decimal CostPrice, decimal SellingPrice)> data
            = new List<(string, string, int, decimal, decimal)>();

        public LowStockReport()
        {
            LoadData();

            printDocument = new PrintDocument();
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
                        SELECT item_name, category, stock_quantity, cost_price, selling_price
                        FROM low_stock_report
                        ORDER BY stock_quantity ASC, item_name";

                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            data.Add((
                                reader["item_name"].ToString(),
                                reader["category"].ToString(),
                                Convert.ToInt32(reader["stock_quantity"]),
                                Convert.ToDecimal(reader["cost_price"]),
                                Convert.ToDecimal(reader["selling_price"])
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading low stock data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int left = 40;
            int y = 40;
            int width = e.PageBounds.Width;

            // Header
            using (Font headerFont = new Font("Segoe UI", 14, FontStyle.Bold))
            {
                // Report Title
                g.DrawString("LOW STOCK REPORT", headerFont, Brushes.Black,
                    new RectangleF(left, y, width - 2 * left, 30),
                    new StringFormat() { Alignment = StringAlignment.Center });
                y += 35;

                // Current date (no time)
                using (Font dateFont = new Font("Segoe UI", 10))
                {
                    g.DrawString($"Date: {DateTime.Now:MMMM dd, yyyy}", dateFont, Brushes.Black, left, y);
                    y += 25;
                }
            }

            y += 40;

            using (Font font = new Font("Segoe UI", 10))
            {
                int xItem = left;
                int xCategory = xItem + 250;
                int xQty = xCategory + 150;
                int xCost = xQty + 80;
                int xSelling = xCost + 80;

                // Table headers
                g.DrawString("Item Name", font, Brushes.Black, xItem, y);
                g.DrawString("Category", font, Brushes.Black, xCategory, y);
                g.DrawString("Qty", font, Brushes.Black, xQty, y);
                g.DrawString("Cost Price", font, Brushes.Black, xCost, y);
                g.DrawString("Selling Price", font, Brushes.Black, xSelling, y);
                y += 25;

                g.DrawLine(Pens.Black, left, y, width - left, y);
                y += 5;

                foreach (var row in data)
                {
                    g.DrawString(row.ItemName, font, Brushes.Black, xItem, y);
                    g.DrawString(row.Category, font, Brushes.Black, xCategory, y);
                    g.DrawString(row.Quantity.ToString(), font, Brushes.Black, xQty, y);
                    g.DrawString(row.CostPrice.ToString("N2"), font, Brushes.Black, xCost, y);
                    g.DrawString(row.SellingPrice.ToString("N2"), font, Brushes.Black, xSelling, y);
                    y += 25;

                    if (y > e.PageBounds.Height - 100)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }

                // Footer
                g.DrawString("Printed By: " + UserCredentials.Fullname, font, Brushes.Black, left, e.PageBounds.Bottom - 80);
                g.DrawString("Date/Time: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"), font, Brushes.Black,
                    width - 300, e.PageBounds.Bottom - 80);
            }
        }


        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1000,
                Height = 700,
                Text = "Low Stock Report Preview"
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
