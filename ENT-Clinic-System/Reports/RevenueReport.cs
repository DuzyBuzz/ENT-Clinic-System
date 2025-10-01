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
    internal class RevenueReport
    {
        private DateTime fromDate;
        private DateTime toDate;
        private string revenueType; // "All", "Billing", or "Sales"
        private PrintDocument printDocument;

        // Use simple class instead of tuple for .NET 4.8 compatibility
        private class RevenueRow
        {
            public DateTime Date;
            public string Type;
            public decimal Amount;
        }

        private class SalesDetailRow
        {
            public DateTime Date;
            public string Customer;
            public string InvoiceType;
            public string Item;
            public int Qty;
            public decimal UnitPrice;
            public decimal TotalPrice;
        }

        private List<RevenueRow> revenueData = new List<RevenueRow>();
        private List<SalesDetailRow> salesDetails = new List<SalesDetailRow>();

        public RevenueReport(DateTime from, DateTime to, string type = "All")
        {
            fromDate = from.Date;
            toDate = to.Date;
            revenueType = type;

            LoadRevenueData();
            LoadSalesDetails();

            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void LoadRevenueData()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT revenue_date, revenue_type, revenue_amount
                        FROM revenue_report
                        WHERE revenue_date BETWEEN @from AND @to";

                    if (revenueType != "All")
                        query += " AND revenue_type = @type";

                    query += " ORDER BY revenue_date";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@from", fromDate);
                        cmd.Parameters.AddWithValue("@to", toDate);
                        if (revenueType != "All")
                            cmd.Parameters.AddWithValue("@type", revenueType);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                revenueData.Add(new RevenueRow
                                {
                                    Date = Convert.ToDateTime(reader["revenue_date"]),
                                    Type = reader["revenue_type"].ToString(),
                                    Amount = Convert.ToDecimal(reader["revenue_amount"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading revenue data: " + ex.Message);
            }
        }

        private void LoadSalesDetails()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT invoice_date, customer_name, invoice_type, item_name, item_quantity, item_unit_price, item_total_price
                        FROM sales_summary
                        WHERE invoice_date BETWEEN @from AND @to
                        ORDER BY invoice_date, customer_name";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@from", fromDate);
                        cmd.Parameters.AddWithValue("@to", toDate);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                salesDetails.Add(new SalesDetailRow
                                {
                                    Date = Convert.ToDateTime(reader["invoice_date"]),
                                    Customer = reader["customer_name"].ToString(),
                                    InvoiceType = reader["invoice_type"].ToString(),
                                    Item = reader["item_name"].ToString(),
                                    Qty = Convert.ToInt32(reader["item_quantity"]),
                                    UnitPrice = Convert.ToDecimal(reader["item_unit_price"]),
                                    TotalPrice = Convert.ToDecimal(reader["item_total_price"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales details: " + ex.Message);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int left = 40;
            int y = 40;
            int width = e.PageBounds.Width;

            using (Font headerFont = new Font("Segoe UI", 14, FontStyle.Bold))
            {
                g.DrawString("REVENUE REPORT", headerFont, Brushes.Black,
                    new RectangleF(left, y, width - 2 * left, 30),
                    new StringFormat() { Alignment = StringAlignment.Center });
            }
            y += 40;

            using (Font font = new Font("Segoe UI", 10))
            {
                g.DrawString($"From: {fromDate:MM/dd/yyyy}  To: {toDate:MM/dd/yyyy}", font, Brushes.Black, left, y);
                g.DrawString($"Revenue Type: {revenueType}", font, Brushes.Black, left + 300, y);
                y += 30;

                int xDate = left;
                int xType = xDate + 150;
                int xAmount = xType + 150;

                g.DrawString("Date", font, Brushes.Black, xDate, y);
                g.DrawString("Type", font, Brushes.Black, xType, y);
                g.DrawString("Amount", font, Brushes.Black, xAmount, y);
                y += 25;
                g.DrawLine(Pens.Black, left, y, width - left, y);
                y += 5;

                decimal totalRevenue = 0;

                foreach (var row in revenueData)
                {
                    g.DrawString(row.Date.ToString("MM/dd/yyyy"), font, Brushes.Black, xDate, y);
                    g.DrawString(row.Type, font, Brushes.Black, xType, y);
                    g.DrawString(row.Amount.ToString("N2"), font, Brushes.Black, xAmount, y);
                    totalRevenue += row.Amount;
                    y += 25;

                    if (row.Type == "Sales")
                    {
                        foreach (var d in salesDetails)
                        {
                            if (d.Date.Date != row.Date.Date)
                                continue;

                            int xCustomer = xDate + 20;
                            int xInvoiceType = xCustomer + 150;
                            int xItem = xInvoiceType + 100;
                            int xQty = xItem + 150;
                            int xUnitPrice = xQty + 50;
                            int xTotalPrice = xUnitPrice + 70;

                            g.DrawString("Customer", font, Brushes.Black, xCustomer, y);
                            g.DrawString("Invoice Type", font, Brushes.Black, xInvoiceType, y);
                            g.DrawString("Item", font, Brushes.Black, xItem, y);
                            g.DrawString("Qty", font, Brushes.Black, xQty, y);
                            g.DrawString("Unit Price", font, Brushes.Black, xUnitPrice, y);
                            g.DrawString("Total Price", font, Brushes.Black, xTotalPrice, y);
                            y += 20;
                            g.DrawLine(Pens.Gray, xCustomer, y, width - left, y);
                            y += 5;

                            g.DrawString(d.Customer, font, Brushes.Black, xCustomer, y);
                            g.DrawString(d.InvoiceType, font, Brushes.Black, xInvoiceType, y);
                            g.DrawString(d.Item, font, Brushes.Black, xItem, y);
                            g.DrawString(d.Qty.ToString(), font, Brushes.Black, xQty, y);
                            g.DrawString(d.UnitPrice.ToString("N2"), font, Brushes.Black, xUnitPrice, y);
                            g.DrawString(d.TotalPrice.ToString("N2"), font, Brushes.Black, xTotalPrice, y);
                            y += 20;

                            if (y > e.PageBounds.Height - 100)
                            {
                                e.HasMorePages = true;
                                return;
                            }
                        }
                        y += 10;
                    }

                    if (y > e.PageBounds.Height - 100)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }

                y += 10;
                g.DrawLine(Pens.Black, left, y, width - left, y);
                y += 5;
                g.DrawString("Total Revenue: " + totalRevenue.ToString("N2"), font, Brushes.Black, xAmount - 150, y);

                // Footer
                g.DrawString("Printed By: " + UserCredentials.Fullname, font, Brushes.Black, left, e.PageBounds.Bottom - 80);
                g.DrawString("Date: " + DateTime.Now.ToString("MM/dd/yyyy"), font, Brushes.Black, width - 200, e.PageBounds.Bottom - 80);
            }
        }

        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1000,
                Height = 700,
                Text = "Revenue Report Preview"
            };
            preview.ShowDialog();
        }
    }
}
