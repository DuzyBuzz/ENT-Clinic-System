using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    public class InvoicePrinter
    {
        private readonly int invoiceId;

        // Fonts
        private readonly Font fontRegular = new Font("Consolas", 9, FontStyle.Regular);
        private readonly Font fontBold = new Font("Consolas", 9, FontStyle.Bold);
        private readonly Font fontHeader = new Font("Consolas", 11, FontStyle.Bold);
        private readonly Font fontSubtitle = new Font("Consolas", 9, FontStyle.Italic);

        // Clinic Info
        private readonly string clinicName = "MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS";
        private readonly string clinicSubtitle = "Fellow, Phil. Society of Otolaryngology, Head & Neck Surgery";
        private readonly string clinicAddress = "388 E. Lopez St., Jaro, Iloilo City (Front of Robinsons Jaro)";
        private readonly string clinicTel = "329-1796";
        private readonly string clinicMobile = "0925-5000149";
        private readonly string reportFooter = "Thank you for visiting!";
        private readonly string currencySymbol = "₱";

        public InvoicePrinter(int invoiceId)
        {
            this.invoiceId = invoiceId;
        }

        public void PrintReceipt()
        {
            try
            {
                using (PrintDialog printDialog = new PrintDialog())
                {
                    printDialog.AllowSomePages = false;
                    printDialog.UseEXDialog = true;

                    if (printDialog.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("No printer selected. Printing canceled.",
                            "Print Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    string printerName = printDialog.PrinterSettings.PrinterName;

                    PrintDocument pd = new PrintDocument();
                    pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 200, 600); // 58mm roll
                    pd.PrinterSettings.PrinterName = printerName;
                    pd.PrintPage += PrintPage;

                    pd.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while printing:\n" + ex.Message,
                    "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 0;
            float leftMargin = 5;
            float lineHeight = fontRegular.GetHeight(e.Graphics);
            int printerWidth = 180; // 58mm receipt printer

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.Word
            };

            // ================= HEADER =================
            y = DrawHeader(e.Graphics, y, printerWidth, leftMargin, format);

            // ================= BODY =================
            string invoiceNo = "INV-N/A", invoiceDate = "N/A", customer = "N/A";
            decimal amountReceived = 0, changeDue = 0, discountAmount = 0, discountPercent = 0, netTotal = 0;

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string qInvoice = @"SELECT invoice_id, invoice_date, customer_name, 
                                        amount_received, change_due, discount_amount, 
                                        discount_percent, net_total
                                        FROM invoices WHERE invoice_id=@id";

                    using (var cmd = new MySqlCommand(qInvoice, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", invoiceId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                invoiceNo = "INV-" + Convert.ToInt32(reader["invoice_id"]).ToString("D6");
                                invoiceDate = Convert.ToDateTime(reader["invoice_date"]).ToString("yyyy-MM-dd HH:mm");
                                customer = Convert.ToString(reader["customer_name"]) ?? "N/A";
                                amountReceived = reader["amount_received"] != DBNull.Value ? Convert.ToDecimal(reader["amount_received"]) : 0;
                                changeDue = reader["change_due"] != DBNull.Value ? Convert.ToDecimal(reader["change_due"]) : 0;
                                discountAmount = reader["discount_amount"] != DBNull.Value ? Convert.ToDecimal(reader["discount_amount"]) : 0;
                                discountPercent = reader["discount_percent"] != DBNull.Value ? Convert.ToDecimal(reader["discount_percent"]) : 0;
                                netTotal = reader["net_total"] != DBNull.Value ? Convert.ToDecimal(reader["net_total"]) : 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice data:\n" + ex.Message,
                    "Invoice Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            e.Graphics.DrawString("Invoice No: " + invoiceNo, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString("Date: " + invoiceDate, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString("Customer: " + customer, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // ================= ITEM HEADER =================
            e.Graphics.DrawString("Item Details", fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString("Qty  Price   Total", fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            decimal subtotal = 0;
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string qItems = @"
                        SELECT ii.quantity, ii.unit_price, ii.total_price,
                               i.generic_name, i.brand_name, i.strength,
                               i.dosage, i.category
                        FROM invoice_items ii
                        JOIN items i ON ii.item_id = i.item_id
                        WHERE ii.invoice_id = @id";

                    using (var cmd = new MySqlCommand(qItems, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", invoiceId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string generic = Convert.ToString(reader["generic_name"]) ?? "";
                                string brand = Convert.ToString(reader["brand_name"]) ?? "";
                                string strength = Convert.ToString(reader["strength"]) ?? "";
                                string dosage = Convert.ToString(reader["dosage"]) ?? "";
                                string category = Convert.ToString(reader["category"]) ?? "";

                                int qty = reader["quantity"] != DBNull.Value ? Convert.ToInt32(reader["quantity"]) : 0;
                                decimal price = reader["unit_price"] != DBNull.Value ? Convert.ToDecimal(reader["unit_price"]) : 0;
                                decimal total = reader["total_price"] != DBNull.Value ? Convert.ToDecimal(reader["total_price"]) : 0;

                                subtotal += total;

                                string itemLine = $"{generic} ({brand})";
                                e.Graphics.DrawString(itemLine, fontBold, Brushes.Black, leftMargin, y); y += lineHeight;

                                string detailsLine = $"{strength} {dosage}";
                                e.Graphics.DrawString(detailsLine, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

                                string qtyLine = $" {qty,2}  {currencySymbol}{price,6:F2}  {currencySymbol}{total,6:F2}";
                                e.Graphics.DrawString(qtyLine, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice items:\n" + ex.Message,
                    "Item Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // ================= TOTALS =================
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString("Subtotal:      " + currencySymbol + subtotal.ToString("F2"), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString("Discount (" + discountPercent + "%): " + currencySymbol + discountAmount.ToString("F2"), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString("Net Total:     " + currencySymbol + netTotal.ToString("F2"), fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            e.Graphics.DrawString("Amount Paid:   " + currencySymbol + amountReceived.ToString("F2"), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString("Change:        " + currencySymbol + changeDue.ToString("F2"), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            e.Graphics.DrawString(new string('=', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(reportFooter, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
        }

        // ================= HEADER HELPER =================
        private float DrawHeader(Graphics g, float y, int printerWidth, float leftMargin, StringFormat format)
        {
            // Clinic Name
            RectangleF rectClinicName = new RectangleF(leftMargin, y, printerWidth, 1000);
            g.DrawString(clinicName, fontHeader, Brushes.Black, rectClinicName, format);
            y += g.MeasureString(clinicName, fontHeader, printerWidth).Height;

            // Subtitle
            RectangleF rectSubtitle = new RectangleF(leftMargin, y, printerWidth, 1000);
            g.DrawString(clinicSubtitle, fontSubtitle, Brushes.Black, rectSubtitle, format);
            y += g.MeasureString(clinicSubtitle, fontSubtitle, printerWidth).Height;

            // Address
            RectangleF rectAddress = new RectangleF(leftMargin, y, printerWidth, 1000);
            g.DrawString(clinicAddress, fontRegular, Brushes.Black, rectAddress, format);
            y += g.MeasureString(clinicAddress, fontRegular, printerWidth).Height;

            // Tel & Mobile
            RectangleF rectTel = new RectangleF(leftMargin, y, printerWidth, 1000);
            g.DrawString($"Tel: {clinicTel} | Mobile: {clinicMobile}", fontRegular, Brushes.Black, rectTel, format);
            y += g.MeasureString($"Tel: {clinicTel} | Mobile: {clinicMobile}", fontRegular, printerWidth).Height;

            // Separator
            g.DrawString(new string('=', 40), fontRegular, Brushes.Black, leftMargin, y);
            y += fontRegular.GetHeight(g);

            return y;
        }
    }
}
