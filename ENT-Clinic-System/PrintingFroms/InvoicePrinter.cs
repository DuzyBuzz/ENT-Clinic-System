using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    public class InvoicePrinter
    {
        private int invoiceId;

        // Fonts
        private Font fontRegular = new Font("Consolas", 9, FontStyle.Regular);
        private Font fontBold = new Font("Consolas", 9, FontStyle.Bold);
        private Font fontHeader = new Font("Consolas", 11, FontStyle.Bold);

        // Cached settings
        private string clinicName, clinicAddress, clinicTel, clinicMobile, reportHeader, reportFooter, currencySymbol;

        public InvoicePrinter(int invoiceId)
        {
            this.invoiceId = invoiceId;
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

                // 🔹 Check if saved printer is valid
                if (string.IsNullOrEmpty(printerName) || !PrinterExists(printerName))
                {
                    if (!AskAndSetPrinter(out printerName))
                        return; // user canceled
                }

                // 🔹 Initialize PrintDocument
                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = printerName;
                pd.PrintPage += PrintPage;

                try
                {
                    pd.Print(); // attempt to print
                }
                catch (Exception ex)
                {
                    // 🔹 Handle printer failure
                    DialogResult retryChoice = MessageBox.Show(
                        $"Printing failed with printer '{printerName}'.\n\nError: {ex.Message}\n\nWould you like to choose another printer?",
                        "Print Error",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Error
                    );

                    if (retryChoice == DialogResult.Yes)
                    {
                        if (AskAndSetPrinter(out printerName))
                        {
                            // 🔹 Retry with new printer
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

        /// <summary>
        /// Check if printer is installed
        /// </summary>
        private bool PrinterExists(string printerName)
        {
            foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
            {
                if (installedPrinter.Equals(printerName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Ask user to select a printer, save to settings, return true if success
        /// </summary>
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

                    // ✅ Save selected printer
                    SettingsHelper.UpdateSetting("printer_name", printerName);

                    MessageBox.Show($"Printer saved: {printerName}",
                                    "Printer Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
                else
                {
                    MessageBox.Show("No printer selected. Printing canceled.",
                                    "Print Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
        }




        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 0;
            float leftMargin = 5;
            float lineHeight = fontRegular.GetHeight(e.Graphics);

            // 🔹 Header
            e.Graphics.DrawString(clinicName, fontHeader, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(clinicAddress, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Tel: {clinicTel}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('=', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // 🔹 Invoice Info
            string invoiceNo = "", invoiceDate = "", customer = "";
            decimal amountReceived = 0, changeDue = 0, discountAmount = 0, taxAmount = 0, netTotal = 0;

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string qInvoice = @"SELECT invoice_id, invoice_date, customer_name, 
                                   amount_received, change_due,
                                   discount_amount, tax_total, net_total
                            FROM invoices 
                            WHERE invoice_id=@id";
                using (var cmd = new MySqlCommand(qInvoice, conn))
                {
                    cmd.Parameters.AddWithValue("@id", invoiceId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            invoiceNo = "INV-" + reader.GetInt32("invoice_id").ToString("D6");
                            invoiceDate = Convert.ToDateTime(reader["invoice_date"]).ToString("yyyy-MM-dd HH:mm");
                            customer = reader["customer_name"].ToString();
                            amountReceived = reader["amount_received"] != DBNull.Value ? Convert.ToDecimal(reader["amount_received"]) : 0;
                            changeDue = reader["change_due"] != DBNull.Value ? Convert.ToDecimal(reader["change_due"]) : 0;
                            discountAmount = reader["discount_amount"] != DBNull.Value ? Convert.ToDecimal(reader["discount_amount"]) : 0;
                            taxAmount = reader["tax_total"] != DBNull.Value ? Convert.ToDecimal(reader["tax_total"]) : 0;
                            netTotal = reader["net_total"] != DBNull.Value ? Convert.ToDecimal(reader["net_total"]) : 0;
                        }
                    }
                }
            }

            e.Graphics.DrawString($"Invoice No: {invoiceNo}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Date: {invoiceDate}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Customer: {customer}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // 🔹 Item Header
            e.Graphics.DrawString("Item", fontBold, Brushes.Black, leftMargin, y);
            e.Graphics.DrawString("Qty", fontBold, Brushes.Black, leftMargin + 120, y);
            e.Graphics.DrawString("Price", fontBold, Brushes.Black, leftMargin + 170, y);
            e.Graphics.DrawString("Total", fontBold, Brushes.Black, leftMargin + 240, y);
            y += lineHeight;

            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // 🔹 Items
            decimal subtotal = 0;
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string qItems = @"SELECT ii.quantity, ii.unit_price, ii.total_price, 
                                 i.item_name, i.description
                          FROM invoice_items ii
                          JOIN items i ON ii.item_id = i.item_id
                          WHERE ii.invoice_id=@id";
                using (var cmd = new MySqlCommand(qItems, conn))
                {
                    cmd.Parameters.AddWithValue("@id", invoiceId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string item = reader["item_name"].ToString();
                            string desc = reader["description"].ToString();
                            int qty = Convert.ToInt32(reader["quantity"]);
                            decimal price = Convert.ToDecimal(reader["unit_price"]);
                            decimal total = Convert.ToDecimal(reader["total_price"]);

                            subtotal += total;

                            // Item line
                            string fullItemLine = $"{item} {desc}";
                            e.Graphics.DrawString(fullItemLine, fontRegular, Brushes.Black, leftMargin, y);
                            y += lineHeight;

                            // Qty / Price / Total line
                            e.Graphics.DrawString(qty.ToString(), fontRegular, Brushes.Black, leftMargin + 120, y);
                            e.Graphics.DrawString($"{currencySymbol}{price:F2}", fontRegular, Brushes.Black, leftMargin + 170, y);
                            e.Graphics.DrawString($"{currencySymbol}{total:F2}", fontRegular, Brushes.Black, leftMargin + 240, y);
                            y += lineHeight;
                        }
                    }
                }
            }

            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // 🔹 Totals
            e.Graphics.DrawString($"Subtotal:      {currencySymbol}{subtotal:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Discount:      {currencySymbol}{discountAmount:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Tax:           {currencySymbol}{taxAmount:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Net Total:     {currencySymbol}{netTotal:F2}", fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // 🔹 Payment
            e.Graphics.DrawString($"Amount Paid:   {currencySymbol}{amountReceived:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Change:        {currencySymbol}{changeDue:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('=', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // 🔹 Footer
            if (!string.IsNullOrEmpty(reportFooter))
            {
                e.Graphics.DrawString(reportFooter, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            }
        }

    }
}
