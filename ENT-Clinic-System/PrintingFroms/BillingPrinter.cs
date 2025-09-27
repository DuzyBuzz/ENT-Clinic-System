using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    public class BillingPrinter
    {
        private int billingId;

        // Fonts
        private Font fontRegular = new Font("Consolas", 9, FontStyle.Regular);
        private Font fontBold = new Font("Consolas", 9, FontStyle.Bold);
        private Font fontHeader = new Font("Consolas", 11, FontStyle.Bold);

        // Cached settings
        private string clinicName, clinicAddress, clinicTel, clinicMobile, reportHeader, reportFooter, currencySymbol;

        public BillingPrinter(int billingId)
        {
            this.billingId = billingId;
            LoadSystemSettings();
        }

        private void LoadSystemSettings()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string query = "SELECT setting_key, setting_value FROM system_settings";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string key = reader.GetString("setting_key");
                        string value = reader.GetString("setting_value");

                        switch (key)
                        {
                            case "clinic_name": clinicName = value; break;
                            case "clinic_address": clinicAddress = value; break;
                            case "clinic_tel": clinicTel = value; break;
                            case "clinic_mobile": clinicMobile = value; break;
                            case "report_header": reportHeader = value; break;
                            case "report_footer": reportFooter = value; break;
                            case "currency_symbol": currencySymbol = value; break;
                        }
                    }
                }
            }
        }

        public void PrintReceipt()
        {
            try
            {
                string printerName = SettingsHelper.GetSetting("printer_name");

                if (string.IsNullOrEmpty(printerName) || !PrinterExists(printerName))
                {
                    if (!AskAndSetPrinter(out printerName))
                        return;
                }

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 200, 600);
                pd.PrinterSettings.PrinterName = printerName;
                pd.PrintPage += PrintPage;

                try
                {
                    pd.Print();
                }
                catch (Exception ex)
                {
                    DialogResult retryChoice = MessageBox.Show(
                        $"Printing failed with printer '{printerName}'.\n\nError: {ex.Message}\n\nChoose another printer?",
                        "Print Error",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Error
                    );

                    if (retryChoice == DialogResult.Yes)
                    {
                        if (AskAndSetPrinter(out printerName))
                        {
                            PrintDocument retryPd = new PrintDocument();
                            retryPd.PrinterSettings.PrinterName = printerName;
                            retryPd.PrintPage += PrintPage;
                            retryPd.Print();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while printing: " + ex.Message,
                                "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool PrinterExists(string printerName)
        {
            foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
            {
                if (installedPrinter.Equals(printerName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private bool AskAndSetPrinter(out string printerName)
        {
            printerName = null;

            using (PrintDialog printDialog = new PrintDialog())
            {
                printDialog.AllowSomePages = false;
                printDialog.ShowHelp = false;
                printDialog.UseEXDialog = true;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printerName = printDialog.PrinterSettings.PrinterName;
                    SettingsHelper.UpdateSetting("printer_name", printerName);
                    MessageBox.Show($"Printer saved: {printerName}", "Printer Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("No printer selected. Printing canceled.", "Print Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 0;
            float leftMargin = 5;
            float lineHeight = fontRegular.GetHeight(e.Graphics);

            // Header
            e.Graphics.DrawString(clinicName, fontHeader, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(clinicAddress, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Tel: {clinicTel}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('=', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // Billing Info
            string patient = "", billingDate = "";
            decimal totalFee = 0, discountAmount = 0, netTotal = 0, amountReceived = 0, changeDue = 0;

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                string qBilling = @"SELECT b.billing_id, b.consultation_id, p.full_name, b.fee, 
                                    b.discount_percent, b.discount_amount, b.total_amount, 
                                    b.amount_paid, b.change_due, b.note, b.created_at
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
                            changeDue = reader["change_due"] != DBNull.Value ? Convert.ToDecimal(reader["change_due"]) : 0;
                        }
                    }
                }
            }

            e.Graphics.DrawString($"Billing ID: {billingId}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Patient: {patient}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Date: {billingDate}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // Fees
            e.Graphics.DrawString($"Fee: {currencySymbol}{totalFee:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Discount: {currencySymbol}{discountAmount:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Net Total: {currencySymbol}{netTotal:F2}", fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // Payment
            e.Graphics.DrawString($"Amount Paid: {currencySymbol}{amountReceived:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Change:      {currencySymbol}{changeDue:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('=', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // Footer
            if (!string.IsNullOrEmpty(reportFooter))
            {
                e.Graphics.DrawString(reportFooter, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            }
        }
    }
}
