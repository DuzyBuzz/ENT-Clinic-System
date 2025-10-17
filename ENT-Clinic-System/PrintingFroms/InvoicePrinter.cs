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
        private readonly int invoiceId;

        // Fonts
        private readonly Font fontRegular = new Font("Consolas", 9, FontStyle.Regular);
        private readonly Font fontBold = new Font("Consolas", 9, FontStyle.Bold);
        private readonly Font fontHeader = new Font("Consolas", 11, FontStyle.Bold);

        // Cached settings
        private string clinicName = "Clinic Name N/A";
        private string clinicAddress = "Address N/A";
        private string clinicTel = "N/A";
        private string clinicMobile = "N/A";
        private string reportHeader = "";
        private string reportFooter = "Thank you for visiting!";
        private string currencySymbol = "₱";

        public InvoicePrinter(int invoiceId)
        {
            this.invoiceId = invoiceId;
            LoadSystemSettings();
        }

        private void LoadSystemSettings()
        {
            try
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
                            string key = Convert.ToString(reader["setting_key"]) ?? "";
                            string value = Convert.ToString(reader["setting_value"]) ?? "";

                            switch (key)
                            {
                                case "clinic_name": clinicName = value; break;
                                case "clinic_address": clinicAddress = value; break;
                                case "clinic_tel": clinicTel = value; break;
                                case "clinic_mobile": clinicMobile = value; break;
                                case "report_header": reportHeader = value; break;
                                case "report_footer": reportFooter = value; break;
                                case "currency_symbol": currencySymbol = string.IsNullOrEmpty(value) ? "₱" : value; break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load system settings.\n\n" + ex.Message,
                    "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        return; // user canceled
                }

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 200, 600); // 58mm roll
                pd.PrinterSettings.PrinterName = printerName;
                pd.PrintPage += PrintPage;

                try
                {
                    pd.Print();
                }
                catch (Exception ex)
                {
                    DialogResult retryChoice = MessageBox.Show(
                        "Printing failed with printer '" + printerName + "'.\n\nError: " + ex.Message +
                        "\n\nWould you like to choose another printer?",
                        "Print Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

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
                MessageBox.Show("Unexpected error while printing:\n" + ex.Message,
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
                printDialog.UseEXDialog = true;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printerName = printDialog.PrinterSettings.PrinterName;
                    SettingsHelper.UpdateSetting("printer_name", printerName);

                    MessageBox.Show("Printer saved: " + printerName,
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
            int printerWidth = 180;

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.Word
            };

            try
            {
                // ===============================
                // HEADER SECTION
                // ===============================
                e.Graphics.DrawString(clinicName, fontHeader, Brushes.Black, leftMargin, y, format);
                y += e.Graphics.MeasureString(clinicName, fontHeader, printerWidth).Height;

                e.Graphics.DrawString(clinicAddress, fontRegular, Brushes.Black, leftMargin, y, format);
                y += e.Graphics.MeasureString(clinicAddress, fontRegular, printerWidth).Height;

                e.Graphics.DrawString("Tel: " + clinicTel, fontRegular, Brushes.Black, leftMargin, y);
                y += lineHeight;

                e.Graphics.DrawString(new string('=', 40), fontRegular, Brushes.Black, leftMargin, y);
                y += lineHeight;

                // ===============================
                // INVOICE HEADER INFO
                // ===============================
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

                // ===============================
                // ITEM HEADER
                // ===============================
                e.Graphics.DrawString("Item Details", fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString("Qty  Price   Total", fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

                // ===============================
                // ITEM LIST
                // ===============================
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

                                    string itemLine = string.Format("{0} ({1})", generic, brand);
                                    e.Graphics.DrawString(itemLine, fontBold, Brushes.Black, leftMargin, y); y += lineHeight;

                                    string detailsLine = string.Format("{0} {1} - {2}", strength, dosage, category);
                                    e.Graphics.DrawString(detailsLine, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

                                    string qtyLine = string.Format(" {0,2}  {1}{2,6:F2}  {1}{3,6:F2}", qty, currencySymbol, price, total);
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

                // ===============================
                // TOTALS
                // ===============================
                e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString("Subtotal:      " + currencySymbol + subtotal.ToString("F2"), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString("Discount (" + discountPercent + "%): " + currencySymbol + discountAmount.ToString("F2"), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString("Net Total:     " + currencySymbol + netTotal.ToString("F2"), fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

                e.Graphics.DrawString("Amount Paid:   " + currencySymbol + amountReceived.ToString("F2"), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString("Change:        " + currencySymbol + changeDue.ToString("F2"), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

                e.Graphics.DrawString(new string('=', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString(reportFooter, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while rendering receipt:\n" + ex.Message,
                    "Render Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
