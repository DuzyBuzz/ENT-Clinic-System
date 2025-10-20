using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    public class BillingPrinter
    {
        private readonly int billingId;

        // Fonts
        private readonly Font fontRegular = new Font("Consolas", 9, FontStyle.Regular);
        private readonly Font fontBold = new Font("Consolas", 9, FontStyle.Bold);
        private readonly Font fontHeader = new Font("Consolas", 11, FontStyle.Bold);
        private readonly Font fontSubtitle = new Font("Consolas", 9, FontStyle.Italic);

        // Static Clinic Info (same as InvoicePrinter)
        private readonly string clinicName = "MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS";
        private readonly string clinicSubtitle = "Fellow, Phil. Society of Otolaryngology, Head & Neck Surgery";
        private readonly string clinicAddress = "388 E. Lopez St., Jaro, Iloilo City (Front of Robinsons Jaro)";
        private readonly string clinicTel = "329-1796";
        private readonly string clinicMobile = "0925-5000149";
        private readonly string reportFooter = "Thank you for visiting!";
        private readonly string currencySymbol = "₱";

        public BillingPrinter(int billingId)
        {
            this.billingId = billingId;
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
            int printerWidth = 180;

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.Word
            };

            // ================= HEADER =================
            y = DrawHeader(e.Graphics, y, printerWidth, leftMargin, format);

            // ================= BODY =================
            string patient = "", billingDate = "", paymentStatus = "";
            decimal totalFee = 0, discountAmount = 0, netTotal = 0, amountReceived = 0, balance = 0;

            List<(DateTime date, decimal amount, decimal balance, decimal changeDue, string note)> payments
                = new List<(DateTime, decimal, decimal, decimal, string)>();

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // --- Billing summary ---
                string qBilling = @"SELECT b.billing_id, p.full_name, b.fee, 
                                   b.discount_amount, b.total_amount, 
                                   b.amount_paid, b.balance, 
                                   b.payment_status, b.created_at
                            FROM billing b
                            JOIN patients p ON b.patient_id = p.patient_id
                            WHERE b.billing_id=@id";
                using (var cmd = new MySqlCommand(qBilling, conn))
                {
                    cmd.Parameters.AddWithValue("@id", billingId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patient = reader["full_name"].ToString();
                            billingDate = Convert.ToDateTime(reader["created_at"]).ToString("yyyy-MM-dd HH:mm");
                            totalFee = reader["fee"] != DBNull.Value ? Convert.ToDecimal(reader["fee"]) : 0;
                            discountAmount = reader["discount_amount"] != DBNull.Value ? Convert.ToDecimal(reader["discount_amount"]) : 0;
                            netTotal = reader["total_amount"] != DBNull.Value ? Convert.ToDecimal(reader["total_amount"]) : 0;
                            amountReceived = reader["amount_paid"] != DBNull.Value ? Convert.ToDecimal(reader["amount_paid"]) : 0;
                            balance = reader["balance"] != DBNull.Value ? Convert.ToDecimal(reader["balance"]) : 0;
                            paymentStatus = reader["payment_status"].ToString();
                        }
                    }
                }

                // --- Payments history ---
                string qPayments = @"SELECT payment_date, amount, balance, change_due, note
                             FROM billing_payments
                             WHERE billing_id=@id
                             ORDER BY payment_date ASC";
                using (var cmd = new MySqlCommand(qPayments, conn))
                {
                    cmd.Parameters.AddWithValue("@id", billingId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime date = reader["payment_date"] != DBNull.Value ? Convert.ToDateTime(reader["payment_date"]) : DateTime.Now;
                            decimal amt = reader["amount"] != DBNull.Value ? Convert.ToDecimal(reader["amount"]) : 0;
                            decimal bal = reader["balance"] != DBNull.Value ? Convert.ToDecimal(reader["balance"]) : 0;
                            decimal chg = reader["change_due"] != DBNull.Value ? Convert.ToDecimal(reader["change_due"]) : 0;
                            string note = reader["note"].ToString();
                            payments.Add((date, amt, bal, chg, note));
                        }
                    }
                }
            }

            // ================= BILLING INFO =================
            string invoiceNo = "BILL-" + billingId.ToString("D6");
            e.Graphics.DrawString($"Bill No: {invoiceNo}", fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Patient: {patient}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Date: {billingDate}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 40), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // ================= FEES =================
            e.Graphics.DrawString($"MD Fee: {currencySymbol}{totalFee:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Discount: {currencySymbol}{discountAmount:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Net Total: {currencySymbol}{netTotal:F2}", fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 40), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // ================= PAYMENTS =================
            e.Graphics.DrawString("Payments:", fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
            foreach (var p in payments)
            {
                e.Graphics.DrawString($"{p.date:MM/dd HH:mm}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString($"  Amount: {currencySymbol}{p.amount:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString($"  Balance: {currencySymbol}{p.balance:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString($"  Change: {currencySymbol}{p.changeDue:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                if (!string.IsNullOrEmpty(p.note)) { e.Graphics.DrawString($"  Note: {p.note}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight; }
                e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            }

            // ================= SUMMARY =================
            e.Graphics.DrawString($"Total Paid: {currencySymbol}{amountReceived:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Balance:    {currencySymbol}{balance:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Status:     {paymentStatus}", fontBold, Brushes.Black, leftMargin, y); y += lineHeight;

            // ================= FOOTER =================
            y = DrawFooter(e.Graphics, y, leftMargin, format);
        }

        // ===== HEADER HELPER (STATIC) =====
        private float DrawHeader(Graphics g, float y, int printerWidth, float leftMargin, StringFormat format)
        {
            RectangleF rectClinicName = new RectangleF(leftMargin, y, printerWidth, 1000);
            g.DrawString(clinicName, fontHeader, Brushes.Black, rectClinicName, format);
            y += g.MeasureString(clinicName, fontHeader, printerWidth).Height;

            RectangleF rectSubtitle = new RectangleF(leftMargin, y, printerWidth, 1000);
            g.DrawString(clinicSubtitle, fontSubtitle, Brushes.Black, rectSubtitle, format);
            y += g.MeasureString(clinicSubtitle, fontSubtitle, printerWidth).Height;

            RectangleF rectAddress = new RectangleF(leftMargin, y, printerWidth, 1000);
            g.DrawString(clinicAddress, fontRegular, Brushes.Black, rectAddress, format);
            y += g.MeasureString(clinicAddress, fontRegular, printerWidth).Height;

            RectangleF rectTel = new RectangleF(leftMargin, y, printerWidth, 1000);
            g.DrawString($"Tel: {clinicTel} | Mobile: {clinicMobile}", fontRegular, Brushes.Black, rectTel, format);
            y += g.MeasureString($"Tel: {clinicTel} | Mobile: {clinicMobile}", fontRegular, printerWidth).Height;

            g.DrawString(new string('=', 40), fontRegular, Brushes.Black, leftMargin, y);
            y += fontRegular.GetHeight(g);

            return y;
        }

        // ===== FOOTER HELPER (STATIC) =====
        private float DrawFooter(Graphics g, float y, float leftMargin, StringFormat format)
        {
            g.DrawString(new string('-', 40), fontRegular, Brushes.Black, leftMargin, y);
            y += fontRegular.GetHeight(g);
            RectangleF rectFooter = new RectangleF(leftMargin, y, 180, 1000);
            g.DrawString(reportFooter, fontRegular, Brushes.Black, rectFooter, format);
            y += g.MeasureString(reportFooter, fontRegular, 180).Height;


            return y;
        }
    }
}
