using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;

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
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = "";
            pd.PrintPage += PrintPage;
            pd.Print();
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

            // Invoice Info
            string invoiceNo = "", invoiceDate = "", customer = "";
            decimal amountReceived = 0, changeDue = 0;

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string q = "SELECT invoice_id, invoice_date, customer_name, amount_received, change_due FROM invoices WHERE invoice_id=@id";
                using (var cmd = new MySqlCommand(q, conn))
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
                        }
                    }
                }
            }

            e.Graphics.DrawString($"Invoice No: {invoiceNo}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Date: {invoiceDate}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Customer: {customer}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // Item Header
            e.Graphics.DrawString("Item", fontBold, Brushes.Black, leftMargin, y);
            e.Graphics.DrawString("Qty  Price  Total", fontBold, Brushes.Black, leftMargin, y + lineHeight);
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y + (2 * lineHeight));
            y += (3 * lineHeight);

            // Items
            decimal subtotal = 0, discountTotal = 0, taxTotal = 0, netTotal = 0;

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string q = @"SELECT ii.quantity, ii.unit_price, ii.discount_amount, ii.tax_amount, ii.total_price, 
                            i.item_name, i.description, i.category
                     FROM invoice_items ii
                     JOIN items i ON ii.item_id = i.item_id
                     WHERE ii.invoice_id=@id";
                using (var cmd = new MySqlCommand(q, conn))
                {
                    cmd.Parameters.AddWithValue("@id", invoiceId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string item = reader["item_name"].ToString();
                            string desc = reader["description"].ToString();
                            string category = reader["category"].ToString();
                            int qty = Convert.ToInt32(reader["quantity"]);
                            decimal price = Convert.ToDecimal(reader["unit_price"]);
                            decimal total = Convert.ToDecimal(reader["total_price"]);
                            decimal discount = Convert.ToDecimal(reader["discount_amount"]);
                            decimal tax = Convert.ToDecimal(reader["tax_amount"]);

                            subtotal += (price * qty);
                            discountTotal += discount;
                            taxTotal += tax;
                            netTotal += total;

                            // 🔹 First line: Item + Category + Description
                            string fullItemLine = $"{item} {desc}";
                            e.Graphics.DrawString(fullItemLine, fontBold, Brushes.Black, leftMargin, y);
                            y += lineHeight;

                            // 🔹 Second line: Qty / Price / Total
                            string qtyLine = $" {qty,2}  {currencySymbol}{price,5:F2}  {currencySymbol}{total,5:F2}";
                            e.Graphics.DrawString(qtyLine, fontRegular, Brushes.Black, leftMargin + 1, y);
                            y += lineHeight;
                        }
                    }
                }
            }

            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // Totals
            e.Graphics.DrawString($"Subtotal:      {currencySymbol}{subtotal:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Discount:      {currencySymbol}{discountTotal:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Tax:           {currencySymbol}{taxTotal:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Net Total:     {currencySymbol}{netTotal:F2}", fontBold, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString(new string('-', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            // Amount Received & Change
            e.Graphics.DrawString($"Amount Paid:   {currencySymbol}{amountReceived:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
            e.Graphics.DrawString($"Change:        {currencySymbol}{changeDue:F2}", fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            e.Graphics.DrawString(new string('=', 32), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            // Footer
            if (!string.IsNullOrEmpty(reportFooter))
            {
                e.Graphics.DrawString(reportFooter, fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;
                e.Graphics.DrawString(new string('-', 60), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

            }
            e.Graphics.DrawString(new string('-', 30), fontRegular, Brushes.Black, leftMargin, y); y += lineHeight;

        }

    }
}
