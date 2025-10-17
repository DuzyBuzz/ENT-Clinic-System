using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    public class LabRequestPrintHelper
    {
        private int _consultationId;
        private PrintDocument _printDocument;

        // Patient info
        private string _patientName = "";
        private string _patientAddress = "";
        private string _patientAge = "";
        private string _patientGender = "";
        private DateTime _requestDate;

        // Selected lab tests grouped by category
        private Dictionary<string, List<string>> _selectedTestsByCategory = new Dictionary<string, List<string>>();

        public LabRequestPrintHelper(int consultationId)
        {
            _consultationId = consultationId;
            LoadData();

            _printDocument = new PrintDocument();
            _printDocument.DefaultPageSettings.PaperSize = new PaperSize("A5", 583, 827);
            _printDocument.DefaultPageSettings.Margins = new Margins(40, 40, 60, 60);
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        // ===========================
        // LOAD DATA
        // ===========================
        private void LoadData()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT p.full_name, p.address, p.age, p.sex, lr.request_date, lr.test_ids
                        FROM lab_requests lr
                        JOIN patients p ON lr.patient_id = p.patient_id
                        WHERE lr.consultation_id = @consultationId
                        ORDER BY lr.request_date DESC
                        LIMIT 1";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultationId", _consultationId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new Exception("No lab request found for this consultation.");

                            _patientName = reader["full_name"].ToString();
                            _patientAddress = reader["address"].ToString();
                            _patientAge = reader["age"].ToString();
                            _patientGender = reader["sex"].ToString();
                            _requestDate = Convert.ToDateTime(reader["request_date"]);

                            string jsonTests = reader["test_ids"].ToString();
                            List<int> checkedTestIds = JsonSerializer.Deserialize<List<int>>(jsonTests) ?? new List<int>();
                            reader.Close();

                            if (checkedTestIds.Count > 0)
                            {
                                string ids = string.Join(",", checkedTestIds);
                                string testQuery = $@"
                                    SELECT category, test_name
                                    FROM lab_tests
                                    WHERE id IN ({ids})
                                    ORDER BY category, test_name";

                                using (var testCmd = new MySqlCommand(testQuery, conn))
                                using (var testReader = testCmd.ExecuteReader())
                                {
                                    while (testReader.Read())
                                    {
                                        string cat = testReader["category"].ToString();
                                        string testName = testReader["test_name"].ToString();

                                        if (!_selectedTestsByCategory.ContainsKey(cat))
                                            _selectedTestsByCategory[cat] = new List<string>();

                                        _selectedTestsByCategory[cat].Add(testName);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading lab request: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===========================
        // PRINT PAGE
        // ===========================
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int leftMargin = 10;
            int y = 10;

            // 1️⃣ Header (Watermark + Clinic Info)
            y = WaterMarkHelper.PrintHeader(g, leftMargin, y, e.PageBounds.Width);

            // 2️⃣ Patient Info (with underline style like prescription)
            using (Font labelFont = new Font("Arial", 8, FontStyle.Bold))
            using (Font valueFont = new Font("Arial", 8))
            {
                int underlineOffset = 2;

                // Patient Name
                g.DrawString("Name:", labelFont, Brushes.Black, leftMargin, y);
                g.DrawString(_patientName, valueFont, Brushes.Black, leftMargin + 100, y);
                SizeF nameSize = g.MeasureString(_patientName, valueFont);
                g.DrawLine(Pens.Black, leftMargin + 100, y + nameSize.Height + underlineOffset,
                           leftMargin + 100 + nameSize.Width, y + nameSize.Height + underlineOffset);

                // Age
                g.DrawString("Age:", labelFont, Brushes.Black, leftMargin + 350, y);
                g.DrawString(_patientAge, valueFont, Brushes.Black, leftMargin + 380, y);
                SizeF ageSize = g.MeasureString(_patientAge, valueFont);
                g.DrawLine(Pens.Black, leftMargin + 380, y + ageSize.Height + underlineOffset,
                           leftMargin + 380 + ageSize.Width, y + ageSize.Height + underlineOffset);

                // Sex
                g.DrawString("Sex:", labelFont, Brushes.Black, leftMargin + 420, y);
                g.DrawString(_patientGender, valueFont, Brushes.Black, leftMargin + 450, y);
                SizeF sexSize = g.MeasureString(_patientGender, valueFont);
                g.DrawLine(Pens.Black, leftMargin + 450, y + sexSize.Height + underlineOffset,
                           leftMargin + 450 + sexSize.Width, y + sexSize.Height + underlineOffset);

                y += 20;

                // Address
                g.DrawString("Address:", labelFont, Brushes.Black, leftMargin, y);
                g.DrawString(_patientAddress, valueFont, Brushes.Black, leftMargin + 100, y);
                SizeF addressSize = g.MeasureString(_patientAddress, valueFont);
                g.DrawLine(Pens.Black, leftMargin + 100, y + addressSize.Height + underlineOffset,
                           leftMargin + 100 + addressSize.Width, y + addressSize.Height + underlineOffset);

                // Date
                g.DrawString("Date:", labelFont, Brushes.Black, leftMargin + 345, y);
                string formattedDate = _requestDate.ToString("MMMM dd, yyyy");
                g.DrawString(formattedDate, valueFont, Brushes.Black, leftMargin + 380, y);
                SizeF dateSize = g.MeasureString(formattedDate, valueFont);
                g.DrawLine(Pens.Black, leftMargin + 380, y + dateSize.Height + underlineOffset,
                           leftMargin + 380 + dateSize.Width, y + dateSize.Height + underlineOffset);

                y += 25;
            }

            // 2️⃣.5️⃣ Title Section (LABORATORY REQUEST)
            using (Font titleFont = new Font("Times Roman", 12, FontStyle.Bold | FontStyle.Underline))
            {
                string titleText = "LABORATORY REQUEST";
                SizeF titleSize = g.MeasureString(titleText, titleFont);

                // Center horizontally
                float centerX = (e.PageBounds.Width - titleSize.Width) / 2;
                g.DrawString(titleText, titleFont, Brushes.Black, centerX, y);
                y += (int)titleSize.Height + 10; // move down a bit for spacing
            }

            // 3️⃣ Selected Lab Tests Section
            using (Font categoryFont = new Font("Arial", 9, FontStyle.Bold))
            using (Font testFont = new Font("Arial", 8))
            {
                if (_selectedTestsByCategory.Count == 0)
                {
                    g.DrawString("No lab tests selected.", testFont, Brushes.Black, leftMargin + 20, y);
                }
                else
                {
                    foreach (var category in _selectedTestsByCategory)
                    {
                        // Draw category title
                        g.DrawString(category.Key, categoryFont, Brushes.Black, leftMargin + 20, y);
                        y += 18;

                        // Draw tests
                        foreach (var testName in category.Value)
                        {
                            g.DrawString($"• {testName}", testFont, Brushes.Black, leftMargin + 40, y);
                            y += 16;
                        }

                        // Light divider line after each category
                        using (Pen dividerPen = new Pen(Color.FromArgb(120, 0, 0, 0), 1))
                        {
                            g.DrawLine(dividerPen, leftMargin + 50, y, e.PageBounds.Width - leftMargin - 20, y);
                        }
                        y += 10;
                    }
                }
            }

            // 4️⃣ Footer
            WaterMarkHelper.PrintFooter(g, leftMargin, e.MarginBounds.Bottom - 30, e.MarginBounds.Width + 10);
        }


        // ===========================
        // SHOW PRINT PREVIEW
        // ===========================
        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = _printDocument,
                Width = 900,
                Height = 700
            };

            preview.Shown += delegate
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
                        using (PrintDialog dlg = new PrintDialog { Document = _printDocument })
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                                _printDocument.Print();
                        }
                    };
                    tool.Items.Insert(0, customPrint);
                }
            };

            preview.ShowDialog();
        }
    }
}
