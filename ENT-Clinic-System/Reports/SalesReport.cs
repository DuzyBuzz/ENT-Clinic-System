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
    internal class SalesReport
    {
        private DateTime fromDate;
        private DateTime toDate;
        private PrintDocument printDocument;

        private class Invoice
        {
            public int InvoiceID;
            public DateTime Date;
            public string Customer;
            public decimal Subtotal;
            public decimal DiscountPercent;
            public decimal DiscountAmount;
            public decimal NetTotal;
            public decimal AmountReceived;
            public decimal ChangeDue;
            public string Note;

            public string InvoiceNo => "INV-" + InvoiceID.ToString("D6");
        }

        private List<Invoice> invoices = new List<Invoice>();

        public SalesReport(DateTime from, DateTime to)
        {
            fromDate = from.Date;
            toDate = to.Date;

            LoadInvoices();

            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void LoadInvoices()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT invoice_id, customer_name, invoice_date, subtotal, discount_percent,
                               discount_amount, net_total, amount_received, change_due, note
                        FROM invoices
                        WHERE invoice_date BETWEEN @from AND @to
                        ORDER BY invoice_date, invoice_id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@from", fromDate);
                        cmd.Parameters.AddWithValue("@to", toDate);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                invoices.Add(new Invoice
                                {
                                    InvoiceID = Convert.ToInt32(reader["invoice_id"]),
                                    Date = Convert.ToDateTime(reader["invoice_date"]),
                                    Customer = reader["customer_name"].ToString(),
                                    Subtotal = Convert.ToDecimal(reader["subtotal"]),
                                    DiscountPercent = Convert.ToDecimal(reader["discount_percent"]),
                                    DiscountAmount = Convert.ToDecimal(reader["discount_amount"]),
                                    NetTotal = Convert.ToDecimal(reader["net_total"]),
                                    AmountReceived = Convert.ToDecimal(reader["amount_received"]),
                                    ChangeDue = Convert.ToDecimal(reader["change_due"]),
                                    Note = reader["note"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoices: " + ex.Message);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int left = 40;
            int right = e.PageBounds.Width - 40;
            int y = 40;
            int pageWidth = e.PageBounds.Width;

            using (Font headerFont = new Font("Segoe UI", 16, FontStyle.Bold))
            using (Font columnFont = new Font("Segoe UI", 10, FontStyle.Bold))
            using (Font font = new Font("Segoe UI", 10))
            {
                // Report Header
                g.DrawString("SALES REPORT", headerFont, Brushes.Black,
                    new RectangleF(left, y, pageWidth - 2 * left, 30),
                    new StringFormat() { Alignment = StringAlignment.Center });
                y += 40;

                g.DrawString($"From: {fromDate:MM/dd/yyyy}    To: {toDate:MM/dd/yyyy}", font, Brushes.Black, left, y);
                y += 20;

                // Column headers
                g.DrawLine(Pens.Black, left, y, right, y);
                y += 5;

                int colInvoice = left;
                int colDate = colInvoice + 120;
                int colCustomer = colDate + 100;
                int colSubtotal = colCustomer + 200;
                int colDiscount = colSubtotal + 80;
                int colNetTotal = colDiscount + 80;
                int colAmountReceived = colNetTotal + 80;
                int colChangeDue = colAmountReceived + 80;

                g.DrawString("Invoice No", columnFont, Brushes.Black, colInvoice, y);
                g.DrawString("Date", columnFont, Brushes.Black, colDate, y);
                g.DrawString("Customer", columnFont, Brushes.Black, colCustomer, y);
                g.DrawString("Subtotal", columnFont, Brushes.Black, colSubtotal, y);
                g.DrawString("Discount", columnFont, Brushes.Black, colDiscount, y);
                g.DrawString("Net Total", columnFont, Brushes.Black, colNetTotal, y);
                g.DrawString("Received", columnFont, Brushes.Black, colAmountReceived, y);
                g.DrawString("Change", columnFont, Brushes.Black, colChangeDue, y);
                y += 20;
                g.DrawLine(Pens.Black, left, y, right, y);
                y += 5;

                decimal grandSubtotal = 0;
                decimal grandDiscount = 0;
                decimal grandNetTotal = 0;
                decimal grandReceived = 0;
                decimal grandChange = 0;

                foreach (var inv in invoices)
                {
                    // Alternating row color
                    if ((invoices.IndexOf(inv) % 2) == 1)
                        g.FillRectangle(new SolidBrush(Color.FromArgb(240, 240, 240)), left, y - 2, pageWidth - 2 * left, 20);

                    g.DrawString(inv.InvoiceNo, font, Brushes.Black, colInvoice, y);
                    g.DrawString(inv.Date.ToString("MM/dd/yyyy"), font, Brushes.Black, colDate, y);
                    g.DrawString(inv.Customer, font, Brushes.Black, colCustomer, y);
                    g.DrawString(inv.Subtotal.ToString("N2"), font, Brushes.Black, colSubtotal, y);
                    g.DrawString(inv.DiscountAmount.ToString("N2"), font, Brushes.Black, colDiscount, y);
                    g.DrawString(inv.NetTotal.ToString("N2"), font, Brushes.Black, colNetTotal, y);
                    g.DrawString(inv.AmountReceived.ToString("N2"), font, Brushes.Black, colAmountReceived, y);
                    g.DrawString(inv.ChangeDue.ToString("N2"), font, Brushes.Black, colChangeDue, y);
                    y += 20;

                    grandSubtotal += inv.Subtotal;
                    grandDiscount += inv.DiscountAmount;
                    grandNetTotal += inv.NetTotal;
                    grandReceived += inv.AmountReceived;
                    grandChange += inv.ChangeDue;

                    if (y > e.PageBounds.Height - 100)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }

                // Grand Total
                g.DrawLine(Pens.Black, left, y, right, y);
                y += 5;
                g.DrawString("GRAND TOTAL", columnFont, Brushes.Black, colCustomer, y);
                g.DrawString(grandSubtotal.ToString("N2"), font, Brushes.Black, colSubtotal, y);
                g.DrawString(grandDiscount.ToString("N2"), font, Brushes.Black, colDiscount, y);
                g.DrawString(grandNetTotal.ToString("N2"), font, Brushes.Black, colNetTotal, y);
                g.DrawString(grandReceived.ToString("N2"), font, Brushes.Black, colAmountReceived, y);
                g.DrawString(grandChange.ToString("N2"), font, Brushes.Black, colChangeDue, y);
                y += 40;

                // Footer
                g.DrawString("Printed By: " + UserCredentials.Fullname, font, Brushes.Black, left, e.PageBounds.Bottom - 80);
                g.DrawString("Date: " + DateTime.Now.ToString("MM/dd/yyyy"), font, Brushes.Black, right - 100, e.PageBounds.Bottom - 80);
            }
        }

        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1000,
                Height = 700,
                Text = "Sales Report Preview"
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
