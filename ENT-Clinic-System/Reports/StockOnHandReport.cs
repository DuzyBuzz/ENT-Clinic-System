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
    internal class StockOnHandReport
    {
        private string categoryFilter;
        private DateTime asOfDate;
        private PrintDocument printDocument;

        // Data storage
        private List<(string ItemName, string Description, string Category, int Quantity)> stockItems
            = new List<(string, string, string, int)>();

        // Constructor receives user-friendly parameters
        public StockOnHandReport(string category, DateTime asOf)
        {
            categoryFilter = category;
            asOfDate = asOf;

            LoadData(); // Load the data from the database

            printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Landscape = false; // Use landscape
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        // ===========================
        // LOAD DATA FROM DATABASE
        // ===========================
        private void LoadData()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // Query the stock_overview view
                    string query = @"
                        SELECT item_name, description, category, stock_quantity AS quantity
                        FROM stock_overview
                        WHERE (@category = '' OR category = @category)
                          AND updated_at <= @asOfDate
                        ORDER BY category, item_name";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@category", categoryFilter);
                        cmd.Parameters.AddWithValue("@asOfDate", asOfDate);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                stockItems.Add((
                                    reader["item_name"].ToString(),
                                    reader["description"].ToString(),
                                    reader["category"].ToString(),
                                    Convert.ToInt32(reader["quantity"])
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stock data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===========================
        // PRINT PAGE
        // ===========================
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int leftMargin = 40;
            float y = 40;
            int pageWidth = e.PageBounds.Width;

            using (Font headerFont = new Font("Segoe UI", 14, FontStyle.Bold))
            using (Font tableHeaderFont = new Font("Segoe UI", 10, FontStyle.Bold))
            using (Font valueFont = new Font("Segoe UI", 10))
            using (Pen lightPen = new Pen(Color.LightGray))
            {
                // ---------- HEADER ----------
                g.DrawString("STOCK ON HAND REPORT", headerFont, Brushes.Black,
                    new RectangleF(leftMargin, y, pageWidth - 2 * leftMargin, 30),
                    new StringFormat() { Alignment = StringAlignment.Center });
                y += 40;

                g.DrawString($"As of: {asOfDate:MMMM dd, yyyy}", valueFont, Brushes.Black, leftMargin, y);
                if (!string.IsNullOrEmpty(categoryFilter))
                    g.DrawString($"Category: {categoryFilter}", valueFont, Brushes.Black, leftMargin + 300, y);
                y += 30;

                // ---------- TABLE HEADER ----------
                int xItem = leftMargin;
                int xDesc = xItem + 200;
                int xCategory = xDesc + 250;
                int xQuantity = xCategory + 150;
                int xUnit = xQuantity + 80;

                g.DrawString("Item Name", tableHeaderFont, Brushes.Black, xItem, y);
                g.DrawString("Description", tableHeaderFont, Brushes.Black, xDesc, y);
                g.DrawString("Category", tableHeaderFont, Brushes.Black, xCategory, y);
                g.DrawString("Quantity", tableHeaderFont, Brushes.Black, xQuantity, y);
                y += 25;

                g.DrawLine(Pens.Black, leftMargin, y, pageWidth - leftMargin, y);
                y += 5;

                // ---------- TABLE ROWS ----------
                foreach (var item in stockItems)
                {
                    g.DrawString(item.ItemName, valueFont, Brushes.Black, xItem, y);
                    g.DrawString(item.Description, valueFont, Brushes.Black, xDesc, y);
                    g.DrawString(item.Category, valueFont, Brushes.Black, xCategory, y);
                    g.DrawString(item.Quantity.ToString(), valueFont, Brushes.Black, xQuantity, y);
                    y += 25;

                    // Light separator
                    g.DrawLine(lightPen, leftMargin, y, pageWidth - leftMargin, y);
                    y += 5;

                    // ---------- PAGE BREAK ----------
                    if (y > e.PageBounds.Height - 100)
                    {
                        e.HasMorePages = true;
                        y = 40; // reset for new page
                        return;
                    }
                }

                // ---------- FOOTER ----------
                string printedBy = "Printed by: " + UserCredentials.Fullname;
                string dateTimeNow = DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt");

                g.DrawString(printedBy, valueFont, Brushes.Black, leftMargin, e.PageBounds.Bottom - 80);

                SizeF dateTimeSize = g.MeasureString(dateTimeNow, valueFont);
                g.DrawString(dateTimeNow, valueFont, Brushes.Black,
                    pageWidth - leftMargin - dateTimeSize.Width, e.PageBounds.Bottom - 80);
            }
        }

        // ===========================
        // SHOW PREVIEW
        // ===========================
        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1000,
                Height = 700,
                Text = "Stock On Hand Report Preview"
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
