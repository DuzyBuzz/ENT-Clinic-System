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
                pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169); // A4 in 1/100 inch
                pd.DefaultPageSettings.Landscape = true;

                int currentRow = 0; // Track which row we're printing

                pd.PrintPage += (s, ev) =>
                {
                    Graphics g = ev.Graphics;
                    int startX = ev.MarginBounds.Left;
                    int startY = ev.MarginBounds.Top;
                    int offsetY = 0;

                    // ===============================
                    // Header
                    // ===============================
                    string clinicName = GetSystemSetting("clinic_name");
                    string clinicAddress = GetSystemSetting("clinic_address");
                    string clinicTel = GetSystemSetting("clinic_tel");
                    string clinicMobile = GetSystemSetting("clinic_mobile");
                    string reportHeader = GetSystemSetting("report_header");
                    string reportFooter = GetSystemSetting("report_footer");

                    // Full width rectangle for centering
                    float headerWidth = ev.MarginBounds.Width;

                    // Clinic Name
                    g.DrawString(clinicName, new Font("Segoe UI", 14, FontStyle.Bold), Brushes.Black,
                        new RectangleF(startX, startY, headerWidth, 25),
                        new StringFormat() { Alignment = StringAlignment.Center });
                    offsetY += 25;

                    // Clinic Address
                    g.DrawString(clinicAddress, new Font("Segoe UI", 10), Brushes.Black,
                        new RectangleF(startX, startY + offsetY, headerWidth, 20),
                        new StringFormat() { Alignment = StringAlignment.Center });
                    offsetY += 20;

                    // Contact Info
                    g.DrawString($"Tel: {clinicTel} | Mobile: {clinicMobile}", new Font("Segoe UI", 10), Brushes.Black,
                        new RectangleF(startX, startY + offsetY, headerWidth, 20),
                        new StringFormat() { Alignment = StringAlignment.Center });
                    offsetY += 25;

                    // Report Header
                    g.DrawString(reportHeader, new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Black,
                        new RectangleF(startX, startY + offsetY, headerWidth, 25),
                        new StringFormat() { Alignment = StringAlignment.Center });
                    offsetY += 25;

                    // Report Type (Daily, Monthly, etc.)
                    g.DrawString(reportType, new Font("Segoe UI", 10, FontStyle.Italic), Brushes.Black,
                        new RectangleF(startX, startY + offsetY, headerWidth, 20),
                        new StringFormat() { Alignment = StringAlignment.Center });
                    offsetY += 30;


                    // ===============================
                    // Column headers
                    // ===============================
                    int colCount = dgvReport.Columns.Count;
                    float colWidth = ev.MarginBounds.Width / colCount;
                    float[] colPositions = new float[colCount];
                    for (int i = 0; i < colCount; i++)
                    {
                        colPositions[i] = startX + i * colWidth;
                        g.DrawString(dgvReport.Columns[i].HeaderText, new Font("Segoe UI", 9, FontStyle.Bold),
                            Brushes.Black, colPositions[i], startY + offsetY);
                    }
                    offsetY += 25;

                    // ===============================
                    // Rows
                    // ===============================
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
                            g.DrawString(value, new Font("Segoe UI", 9), Brushes.Black, colPositions[j], startY + offsetY);
                        }

                        offsetY += 20;

                        // Check if we need a new page
                        if (startY + offsetY > ev.MarginBounds.Bottom - 100)
                        {
                            ev.HasMorePages = true;
                            currentRow++; // Continue on next page
                            return;
                        }

                        currentRow++;
                    }

                    // ===============================
                    // Summary Section
                    // ===============================
                    decimal grossTotal = 0, netTotal = 0;
                    foreach (DataGridViewRow row in dgvReport.Rows)
                    {
                        if (row.IsNewRow) continue;
                        grossTotal += Convert.ToDecimal(row.Cells["Gross Total"].Value ?? 0);
                        netTotal += Convert.ToDecimal(row.Cells["Net Total"].Value ?? 0);
                    }

                    offsetY += 20;
                    g.DrawLine(Pens.Black, startX, startY + offsetY, startX + ev.MarginBounds.Width, startY + offsetY);
                    offsetY += 5;
                    g.DrawString($"Gross Total: {grossTotal:C}", new Font("Segoe UI", 10, FontStyle.Bold), Brushes.Black, startX, startY + offsetY);
                    offsetY += 20;
                    g.DrawString($"Net Total: {netTotal:C}", new Font("Segoe UI", 10, FontStyle.Bold), Brushes.Black, startX, startY + offsetY);

                    // ===============================
                    // Footer
                    // ===============================
                    offsetY += 30;
                    g.DrawString(reportFooter, new Font("Segoe UI", 10, FontStyle.Italic), Brushes.Black,
                        new RectangleF(startX, startY + offsetY, ev.MarginBounds.Width, 25),
                        new StringFormat() { Alignment = StringAlignment.Center });

                    ev.HasMorePages = false; // Finished printing
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
