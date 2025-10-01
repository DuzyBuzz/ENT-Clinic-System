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
    internal class DispensingReport
    {
        private string patientFilter;
        private string categoryFilter;
        private string itemNameFilter;
        private string descriptionFilter;
        private DateTime fromDate;
        private DateTime toDate;
        private PrintDocument printDocument;

        // Tuple for dispensing data
        private List<(string Patient, string Item, int Quantity, DateTime DispensedAt, string Note)> data
            = new List<(string, string, int, DateTime, string)>();

        public DispensingReport(string patient, string category, string itemName, string description, DateTime from, DateTime to)
        {
            patientFilter = patient;
            categoryFilter = category;
            itemNameFilter = itemName;
            descriptionFilter = description;
            fromDate = from;
            toDate = to;

            LoadData();

            printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Landscape = true; // Landscape
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void LoadData()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT patient_name, CONCAT(category, ' - ', item_name, ' - ', description) AS item_display, 
                               quantity, dispensed_at, note
                        FROM dispense_history
                        WHERE (@patient = '' OR patient_name = @patient)
                          AND (@category = '' OR category = @category)
                          AND (@itemName = '' OR item_name = @itemName)
                          AND (@description = '' OR description LIKE CONCAT('%', @description, '%'))
                          AND dispensed_at BETWEEN @from AND @to
                        ORDER BY dispensed_at";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@patient", patientFilter);
                        cmd.Parameters.AddWithValue("@category", categoryFilter);
                        cmd.Parameters.AddWithValue("@itemName", itemNameFilter);
                        cmd.Parameters.AddWithValue("@description", descriptionFilter);
                        cmd.Parameters.AddWithValue("@from", fromDate);
                        cmd.Parameters.AddWithValue("@to", toDate);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data.Add((
                                    reader["patient_name"].ToString(),
                                    reader["item_display"].ToString(),
                                    Convert.ToInt32(reader["quantity"]),
                                    Convert.ToDateTime(reader["dispensed_at"]),
                                    reader["note"].ToString()
                                ));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dispensing data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int left = 40;
            int y = 40;
            int width = e.PageBounds.Width;

            // Header
            using (Font headerFont = new Font("Segoe UI", 16, FontStyle.Bold))
            {
                g.DrawString("DISPENSING REPORT", headerFont, Brushes.Black,
                    new RectangleF(left, y, width - 2 * left, 30),
                    new StringFormat() { Alignment = StringAlignment.Center });
            }
            y += 50;

            using (Font font = new Font("Segoe UI", 10))
            using (Font colFont = new Font("Segoe UI", 10, FontStyle.Bold))
            {
                g.DrawString($"From: {fromDate:MM/dd/yyyy}  To: {toDate:MM/dd/yyyy}", font, Brushes.Black, left, y);
                y += 30;

                // Columns
                int xPatient = left;
                int xItem = xPatient + 250;
                int xQuantity = xItem + 350;
                int xDispensedAt = xQuantity + 80;
                int xNote = xDispensedAt + 120;

                g.DrawString("Patient", colFont, Brushes.Black, xPatient, y);
                g.DrawString("Item", colFont, Brushes.Black, xItem, y);
                g.DrawString("Qty", colFont, Brushes.Black, xQuantity, y);
                g.DrawString("Dispensed At", colFont, Brushes.Black, xDispensedAt, y);
                g.DrawString("Note", colFont, Brushes.Black, xNote, y);
                y += 25;

                g.DrawLine(Pens.Black, left, y, width - left, y);
                y += 5;

                // Print each row
                foreach (var row in data)
                {
                    g.DrawString(row.Patient, font, Brushes.Black, xPatient, y);
                    g.DrawString(row.Item, font, Brushes.Black, xItem, y);
                    g.DrawString(row.Quantity.ToString(), font, Brushes.Black, xQuantity, y);
                    g.DrawString(row.DispensedAt.ToString("MM/dd/yyyy"), font, Brushes.Black, xDispensedAt, y);
                    g.DrawString(row.Note, font, Brushes.Black, xNote, y);
                    y += 25;

                    if (y > e.PageBounds.Height - 100)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }

                // Summary by Item
                y += 20;
                g.DrawLine(Pens.Black, left, y, width - left, y);
                y += 10;

                g.DrawString("SUMMARY (Total Quantity per Item)", colFont, Brushes.Black, left, y);
                y += 25;

                var summaryItem = data.GroupBy(d => d.Item)
                                      .Select(grp => new { Item = grp.Key, TotalQty = grp.Sum(x => x.Quantity) })
                                      .OrderBy(x => x.Item);

                foreach (var s in summaryItem)
                {
                    g.DrawString($"{s.Item}: {s.TotalQty}", font, Brushes.Black, left + 20, y);
                    y += 20;
                }

                // Footer
                y = e.PageBounds.Bottom - 80;
                g.DrawLine(Pens.Black, left, y - 5, width - left, y - 5);
                g.DrawString("Printed By: " + UserCredentials.Fullname, font, Brushes.Black, left, y);
                g.DrawString("Date/Time: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"), font, Brushes.Black,
                    width - 300, y);
            }
        }

        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1200,
                Height = 700,
                Text = "Dispensing Report Preview"
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
