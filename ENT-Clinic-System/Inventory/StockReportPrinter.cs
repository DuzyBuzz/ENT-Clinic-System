using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ENT_Clinic_System.Inventory
{
    public static class StockReportPrinter
    {
        private static DataTable stockTable;
        private static int currentPage = 0;
        private static int totalPages = 1;
        private static int recordsPerPage = 20;

        /// <summary>
        /// Call this method to show the stock print preview.
        /// </summary>
        public static void ShowPrintPreview()
        {
            LoadStockData();

            // Read records per page from system settings
            int.TryParse(SettingsHelper.GetSetting("records_per_page"), out recordsPerPage);
            if (recordsPerPage <= 0) recordsPerPage = 20;

            totalPages = (int)Math.Ceiling(stockTable.Rows.Count / (double)recordsPerPage);
            currentPage = 0;

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;

            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDoc,
                WindowState = FormWindowState.Maximized
            };
            previewDialog.ShowDialog();
        }

        /// <summary>
        /// Loads inventory stock data from database.
        /// </summary>
        private static void LoadStockData()
        {
            stockTable = new DataTable();
            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand("SELECT item_name, description, category, stock_quantity FROM items ORDER BY category, item_name", conn))
            {
                try
                {
                    conn.Open();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(stockTable);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load stock data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the PrintPage event for the stock report.
        /// </summary>
        private static void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int y = 20;
            int lineHeight = 25;

            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            Font subHeaderFont = new Font("Arial", 10, FontStyle.Regular);
            Font bodyFont = new Font("Arial", 10);
            Font footerFont = new Font("Arial", 8, FontStyle.Italic);

            // Clinic info & Report Header
            string clinicName = SettingsHelper.GetSetting("clinic_name");
            string clinicAddress = SettingsHelper.GetSetting("clinic_address");
            string clinicTel = SettingsHelper.GetSetting("clinic_tel");
            string clinicMobile = SettingsHelper.GetSetting("clinic_mobile");
            string reportHeader = SettingsHelper.GetSetting("report_header");
            string reportFooter = SettingsHelper.GetSetting("report_footer");

            // Draw clinic & report header
            e.Graphics.DrawString(clinicName, headerFont, Brushes.Black, 50, y);
            y += lineHeight;
            e.Graphics.DrawString(clinicAddress, subHeaderFont, Brushes.Black, 50, y);
            y += lineHeight;
            e.Graphics.DrawString($"Tel: {clinicTel} | Mobile: {clinicMobile}", subHeaderFont, Brushes.Black, 50, y);
            y += lineHeight;
            e.Graphics.DrawString(reportHeader, headerFont, Brushes.Black, 200, y);
            y += lineHeight * 2;

            // Column headers
            e.Graphics.DrawString("Item Name", headerFont, Brushes.Black, 20, y);
            e.Graphics.DrawString("Description", headerFont, Brushes.Black, 200, y);
            e.Graphics.DrawString("Category", headerFont, Brushes.Black, 400, y);
            e.Graphics.DrawString("Quantity", headerFont, Brushes.Black, 550, y);
            y += lineHeight;

            // Draw a line
            e.Graphics.DrawLine(Pens.Black, 20, y, 600, y);
            y += 5;

            // Low stock threshold
            int.TryParse(SettingsHelper.GetSetting("low_stock_threshold"), out int threshold);

            // Print page records
            int startRecord = currentPage * recordsPerPage;
            int endRecord = Math.Min(startRecord + recordsPerPage, stockTable.Rows.Count);

            for (int i = startRecord; i < endRecord; i++)
            {
                DataRow row = stockTable.Rows[i];
                string itemName = row["item_name"].ToString();
                string description = row["description"].ToString();
                string category = row["category"].ToString();
                string quantityStr = row["stock_quantity"].ToString();
                Brush brush = Brushes.Black;

                if (int.TryParse(quantityStr, out int qty) && qty < threshold)
                {
                    brush = Brushes.Red; // Highlight low stock
                }

                e.Graphics.DrawString(itemName, bodyFont, brush, 20, y);
                e.Graphics.DrawString(description, bodyFont, brush, 200, y);
                e.Graphics.DrawString(category, bodyFont, brush, 400, y);
                e.Graphics.DrawString($"{quantityStr}", bodyFont, brush, 550, y);

                y += lineHeight;
            }

            // Footer with page info
            y = e.MarginBounds.Bottom - 40;
            e.Graphics.DrawString($"{reportFooter} | Page {currentPage + 1} of {totalPages}", footerFont, Brushes.Gray, 200, y);

            // Prepare next page
            currentPage++;
            e.HasMorePages = currentPage < totalPages;
        }
    }
}
