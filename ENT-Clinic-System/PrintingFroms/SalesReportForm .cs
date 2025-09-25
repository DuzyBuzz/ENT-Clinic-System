using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    public partial class SalesReportForm : Form
    {
        public SalesReportForm()
        {
            InitializeComponent();
        }

        private void SalesReportForm_Load(object sender, EventArgs e)
        {
            cmbReportType.SelectedIndex = 0; // Default to Full Report
            LoadReport();
        }

        // ===============================
        // 🔹 Get system setting
        // ===============================
        private string GetSystemSetting(string key)
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT setting_value FROM system_settings WHERE setting_key=@key LIMIT 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@key", key);
                        object result = cmd.ExecuteScalar();
                        return result?.ToString() ?? "";
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        // ===============================
        // 🔹 Load report
        // ===============================
        private void LoadReport()
        {
            if (cmbReportType.SelectedItem == null) return;

            string reportType = cmbReportType.SelectedItem.ToString();
            string query = "";

            try
            {
                if (reportType == "Full Report")
                {
                    query = @"SELECT movement_id AS 'Trans ID',
                                     DATE(movement_date) AS 'Date',
                                     item_name AS 'Item Name',
                                     description AS 'Description',
                                     category AS 'Category',
                                     quantity AS 'Qty',
                                     cost_price AS 'Cost Price',
                                     selling_price AS 'Selling Price',
                                     discount_amount AS 'Discount',
                                     tax_amount AS 'Tax',
                                     gross_total AS 'Gross Total',
                                     net_total AS 'Net Total'
                              FROM sales_report
                              ORDER BY movement_date ASC";
                }
                else if (reportType == "Daily Summary")
                {
                    string selectedDate = dtpReportDate.Value.ToString("yyyy-MM-dd");
                    query = $@"SELECT movement_id AS 'Trans ID',
                                      DATE(movement_date) AS 'Date',
                                      item_name AS 'Item Name',
                                      description AS 'Description',
                                      category AS 'Category',
                                      quantity AS 'Qty',
                                      cost_price AS 'Cost Price',
                                      selling_price AS 'Selling Price',
                                      discount_amount AS 'Discount',
                                      tax_amount AS 'Tax',
                                      gross_total AS 'Gross Total',
                                      net_total AS 'Net Total'
                               FROM sales_report
                               WHERE DATE(movement_date)='{selectedDate}'
                               ORDER BY movement_date ASC";
                }
                else if (reportType == "Monthly Summary")
                {
                    string selectedMonth = dtpReportDate.Value.ToString("yyyy-MM");
                    query = $@"SELECT movement_id AS 'Trans ID',
                                      DATE(movement_date) AS 'Date',
                                      item_name AS 'Item Name',
                                      description AS 'Description',
                                      category AS 'Category',
                                      quantity AS 'Qty',
                                      cost_price AS 'Cost Price',
                                      selling_price AS 'Selling Price',
                                      discount_amount AS 'Discount',
                                      tax_amount AS 'Tax',
                                      gross_total AS 'Gross Total',
                                      net_total AS 'Net Total'
                               FROM sales_report
                               WHERE DATE_FORMAT(movement_date, '%Y-%m')='{selectedMonth}'
                               ORDER BY movement_date ASC";
                }

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(query, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvReport.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading report: " + ex.Message);
            }
        }

        // ===============================
        // 🔹 ComboBox change
        // ===============================
        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedItem.ToString() == "Daily Summary")
            {
                dtpReportDate.Format = DateTimePickerFormat.Custom;
                dtpReportDate.CustomFormat = "yyyy-MM-dd";
                dtpReportDate.ShowUpDown = false;
                dtpReportDate.Visible = true;
            }
            else if (cmbReportType.SelectedItem.ToString() == "Monthly Summary")
            {
                dtpReportDate.Format = DateTimePickerFormat.Custom;
                dtpReportDate.CustomFormat = "MMMM yyyy";
                dtpReportDate.ShowUpDown = true;
                dtpReportDate.Visible = true;
            }
            else
            {
                dtpReportDate.Visible = false;
            }

            LoadReport();
        }

        // ===============================
        // 🔹 Print Report
        // ===============================
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedItem == null) return;

            PrintReport(cmbReportType.SelectedItem.ToString());
        }

        private void PrintReport(string reportType)
        {
            if (dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("No data to print.");
                return;
            }

            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            using (PrintDocument pd = new PrintDocument())
            {
                pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
                pd.DefaultPageSettings.Landscape = true;

                int currentRow = 0;
                int pageNumber = 1;

                pd.PrintPage += (s, ev) =>
                {
                    Graphics g = ev.Graphics;
                    int startX = ev.MarginBounds.Left;
                    int startY = ev.MarginBounds.Top;
                    int offsetY = 0;

                    Font clinicFont = new Font("Segoe UI", 12, FontStyle.Bold);
                    Font subClinicFont = new Font("Segoe UI", 10, FontStyle.Regular);
                    Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
                    Font bodyFont = new Font("Segoe UI", 9);
                    Font footerFont = new Font("Segoe UI", 9, FontStyle.Italic);

                    string clinicName = GetSystemSetting("clinic_name");
                    string clinicAddress = GetSystemSetting("clinic_address");
                    string clinicTel = GetSystemSetting("clinic_tel");
                    string clinicMobile = GetSystemSetting("clinic_mobile");
                    string reportHeader = GetSystemSetting("report_header");
                    string reportFooter = GetSystemSetting("report_footer");
                    string currency = GetSystemSetting("currency_symbol") ?? "₱";

                    float headerWidth = ev.MarginBounds.Width;

                    // -------------------------
                    // Header
                    // -------------------------
                    g.DrawString(clinicName, clinicFont, Brushes.Black, new RectangleF(startX, startY + offsetY, headerWidth, 25), new StringFormat() { Alignment = StringAlignment.Center });
                    offsetY += 25;

                    g.DrawString(clinicAddress, subClinicFont, Brushes.Black, new RectangleF(startX, startY + offsetY, headerWidth, 20), new StringFormat() { Alignment = StringAlignment.Center });
                    offsetY += 20;

                    g.DrawString($"Tel: {clinicTel} | Mobile: {clinicMobile}", subClinicFont, Brushes.Black, new RectangleF(startX, startY + offsetY, headerWidth, 20), new StringFormat() { Alignment = StringAlignment.Center });
                    offsetY += 25;

                    g.DrawString(reportHeader, clinicFont, Brushes.Black, new RectangleF(startX, startY + offsetY, headerWidth, 25), new StringFormat() { Alignment = StringAlignment.Center });
                    offsetY += 25;

                    g.DrawString(reportType, new Font("Segoe UI", 10, FontStyle.Italic), Brushes.Black, new RectangleF(startX, startY + offsetY, headerWidth, 20), new StringFormat() { Alignment = StringAlignment.Center });
                    offsetY += 25;

                    g.DrawLine(Pens.Black, startX, startY + offsetY, startX + ev.MarginBounds.Width, startY + offsetY);
                    offsetY += 5;

                    // -------------------------
                    // Column Headers
                    // -------------------------
                    string[] columns = { "Trans ID", "Date", "Item", "Description", "Category", "Qty", "Cost", "Selling", "Discount", "Tax", "Gross", "Net" };
                    int colCount = columns.Length;
                    float colWidth = ev.MarginBounds.Width / colCount;
                    float[] colPositions = new float[colCount];

                    for (int i = 0; i < colCount; i++)
                    {
                        colPositions[i] = startX + i * colWidth;
                        g.DrawString(columns[i], headerFont, Brushes.Black, colPositions[i], startY + offsetY);
                    }
                    offsetY += 20;

                    g.DrawLine(Pens.Black, startX, startY + offsetY, startX + ev.MarginBounds.Width, startY + offsetY);
                    offsetY += 5;

                    // -------------------------
                    // Rows
                    // -------------------------
                    decimal totalCost = 0, totalSelling = 0, totalDiscount = 0, totalTax = 0, totalGross = 0, totalNet = 0;
                    int totalItems = 0;

                    while (currentRow < dgvReport.Rows.Count)
                    {
                        DataGridViewRow row = dgvReport.Rows[currentRow];
                        if (row.IsNewRow)
                        {
                            currentRow++;
                            continue;
                        }

                        for (int j = 0; j < colCount; j++)
                        {
                            object cellValue = row.Cells[j].Value;
                            string value = (cellValue is DateTime dt) ? dt.ToString("yyyy-MM-dd") : cellValue?.ToString() ?? "";

                            // Format numbers with currency
                            if (j >= 6) value = $"{currency} {Convert.ToDecimal(cellValue):N2}";

                            g.DrawString(value, bodyFont, Brushes.Black, colPositions[j], startY + offsetY);
                        }

                        // Accumulate totals
                        totalCost += Convert.ToDecimal(row.Cells["Cost Price"].Value ?? 0);
                        totalSelling += Convert.ToDecimal(row.Cells["Selling Price"].Value ?? 0);
                        totalDiscount += Convert.ToDecimal(row.Cells["Discount"].Value ?? 0);
                        totalTax += Convert.ToDecimal(row.Cells["Tax"].Value ?? 0);
                        totalGross += Convert.ToDecimal(row.Cells["Gross Total"].Value ?? 0);
                        totalNet += Convert.ToDecimal(row.Cells["Net Total"].Value ?? 0);
                        totalItems += Convert.ToInt32(row.Cells["Qty"].Value ?? 0);

                        offsetY += 20;

                        // Check if page full
                        if (startY + offsetY > ev.MarginBounds.Bottom - 60)
                        {
                            ev.HasMorePages = true;
                            pageNumber++;
                            currentRow++;
                            return;
                        }

                        currentRow++;
                    }

                    // -------------------------
                    // Summary Section
                    // -------------------------
                    offsetY += 10;
                    g.DrawLine(Pens.Black, startX, startY + offsetY, startX + ev.MarginBounds.Width, startY + offsetY);
                    offsetY += 5;

                    g.DrawString($"Total Items Sold: {totalItems}", headerFont, Brushes.Black, startX, startY + offsetY);
                    offsetY += 20;
                    g.DrawString($"Total Cost: {currency} {totalCost:N2}", headerFont, Brushes.Black, startX, startY + offsetY);
                    offsetY += 20;
                    g.DrawString($"Total Selling: {currency} {totalSelling:N2}", headerFont, Brushes.Black, startX, startY + offsetY);
                    offsetY += 20;
                    g.DrawString($"Total Discount: {currency} {totalDiscount:N2}", headerFont, Brushes.Black, startX, startY + offsetY);
                    offsetY += 20;
                    g.DrawString($"Total Tax: {currency} {totalTax:N2}", headerFont, Brushes.Black, startX, startY + offsetY);
                    offsetY += 20;
                    g.DrawString($"Total Gross: {currency} {totalGross:N2}", headerFont, Brushes.Black, startX, startY + offsetY);
                    offsetY += 20;
                    g.DrawString($"Total Net Revenue: {currency} {totalNet:N2}", headerFont, Brushes.Black, startX, startY + offsetY);

                    // -------------------------
                    // Footer
                    // -------------------------
                    offsetY += 30;
                    g.DrawString($"{reportFooter} | Page {pageNumber}", footerFont, Brushes.Black,
                        new RectangleF(startX, ev.MarginBounds.Bottom - 30, ev.MarginBounds.Width, 20),
                        new StringFormat() { Alignment = StringAlignment.Center });

                    ev.HasMorePages = false;
                };

                preview.Document = pd;
                preview.ShowDialog();
            }
        }




        private void dtpReportDate_ValueChanged(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedItem.ToString() == "Daily Summary")
            {
                dtpReportDate.Format = DateTimePickerFormat.Custom;
                dtpReportDate.CustomFormat = "yyyy-MM-dd";
                dtpReportDate.ShowUpDown = false;
                dtpReportDate.Visible = true;
            }
            else if (cmbReportType.SelectedItem.ToString() == "Monthly Summary")
            {
                dtpReportDate.Format = DateTimePickerFormat.Custom;
                dtpReportDate.CustomFormat = "MMMM yyyy";
                dtpReportDate.ShowUpDown = true;
                dtpReportDate.Visible = true;
            }
            else
            {
                dtpReportDate.Visible = false;
            }

            LoadReport();
        }
    }
}
