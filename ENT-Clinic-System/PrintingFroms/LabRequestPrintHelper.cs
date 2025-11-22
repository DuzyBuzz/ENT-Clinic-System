using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    public class LabRequestPrintHelper
    {
        private int _consultationId;
        private PrintDocument _printDocument;

        // Patient info (from first lab request row)
        private string _patientName = "";
        private string _patientAddress = "";
        private string _patientAge = "";
        private string _patientGender = "";
        private DateTime _latestRequestDate;

        // All selected lab tests grouped by category
        private Dictionary<string, List<string>> _selectedTestsByCategory = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

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
        // LOAD DATA (merge all lab requests)
        // ===========================
        private void LoadData()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT lr.request_date, lr.test_ids, 
                               p.full_name, p.address, p.age, p.sex
                        FROM lab_requests lr
                        JOIN patients p ON lr.patient_id = p.patient_id
                        WHERE lr.consultation_id = @consultationId
                        ORDER BY lr.request_date ASC;";

                    List<int> allTestIds = new List<int>();

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultationId", _consultationId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("No lab requests found for this consultation.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            while (reader.Read())
                            {
                                // Load patient info once
                                if (string.IsNullOrEmpty(_patientName))
                                {
                                    _patientName = reader["full_name"]?.ToString() ?? "";
                                    _patientAddress = reader["address"]?.ToString() ?? "";
                                    _patientAge = reader["age"]?.ToString() ?? "";
                                    _patientGender = reader["sex"]?.ToString() ?? "";
                                }

                                // Keep latest request date
                                if (!reader.IsDBNull(reader.GetOrdinal("request_date")))
                                {
                                    DateTime reqDate = Convert.ToDateTime(reader["request_date"]);
                                    if (reqDate > _latestRequestDate) _latestRequestDate = reqDate;
                                }

                                // Parse test_ids safely
                                string rawTestIds = reader["test_ids"]?.ToString() ?? "";
                                var parsed = ParseTestIds(rawTestIds);
                                if (parsed?.Count > 0) allTestIds.AddRange(parsed);
                            }
                        }
                    }

                    // Remove duplicates and order
                    var mergedTestIds = allTestIds.Distinct().OrderBy(id => id).ToList();
                    if (mergedTestIds.Count == 0) return;

                    // Fetch test info from lab_tests table
                    string idsCsv = string.Join(",", mergedTestIds);
                    string testQuery = $@"
                        SELECT category, test_name
                        FROM lab_tests
                        WHERE id IN ({idsCsv})
                        ORDER BY category, test_name;";

                    using (var testCmd = new MySqlCommand(testQuery, conn))
                    using (var testReader = testCmd.ExecuteReader())
                    {
                        while (testReader.Read())
                        {
                            string category = testReader["category"]?.ToString() ?? "Uncategorized";
                            string testName = testReader["test_name"]?.ToString() ?? "";

                            if (!_selectedTestsByCategory.TryGetValue(category, out var list))
                            {
                                list = new List<string>();
                                _selectedTestsByCategory[category] = list;
                            }

                            if (!list.Contains(testName))
                                list.Add(testName);
                        }
                    }

                    // Sort tests within each category
                    foreach (var key in _selectedTestsByCategory.Keys.ToList())
                        _selectedTestsByCategory[key] = _selectedTestsByCategory[key].OrderBy(n => n).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading lab requests: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Robust parser for test_ids JSON array or [1,2,3] format
        private List<int> ParseTestIds(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return new List<int>();

            raw = raw.Trim();

            try
            {
                // Try JSON array first
                var ints = JsonSerializer.Deserialize<List<int>>(raw);
                if (ints != null) return ints;
            }
            catch { }

            // Fallback regex for [1,2,3] format
            var matches = Regex.Matches(raw, @"\d+");
            var list = new List<int>();
            foreach (Match m in matches)
                if (int.TryParse(m.Value, out int v)) list.Add(v);

            return list;
        }

        // ===========================
        // PRINT PAGE
        // ===========================
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int left = 10;
            int y = 10;

            // Header
            y = WaterMarkHelper.PrintHeader(g, left, y, e.PageBounds.Width);

            // Patient info
            using (Font labelFont = new Font("Arial", 8, FontStyle.Bold))
            using (Font valueFont = new Font("Arial", 8, FontStyle.Underline))
            {
                g.DrawString("Name:", labelFont, Brushes.Black, left, y);
                g.DrawString(_patientName, valueFont, Brushes.Black, left + 100, y);

                g.DrawString("Age:", labelFont, Brushes.Black, left + 400, y);
                g.DrawString(_patientAge, valueFont, Brushes.Black, left + 430, y);

                g.DrawString("Sex:", labelFont, Brushes.Black, left + 470, y);
                g.DrawString(_patientGender, valueFont, Brushes.Black, left + 500, y);

                y += 20;

                g.DrawString("Address:", labelFont, Brushes.Black, left, y);
                g.DrawString(_patientAddress, valueFont, Brushes.Black, left + 100, y);

                g.DrawString("Date:", labelFont, Brushes.Black, left + 400, y);
                g.DrawString(_latestRequestDate.ToString("MMMM dd, yyyy"), valueFont, Brushes.Black, left + 435, y);

                y += 15;
            }

            // Title
            using (Font titleFont = new Font("Times Roman", 12, FontStyle.Bold | FontStyle.Underline))
            {
                string title = "LABORATORY REQUEST";
                SizeF size = g.MeasureString(title, titleFont);
                float centerX = (e.PageBounds.Width - size.Width) / 2;
                g.DrawString(title, titleFont, Brushes.Black, centerX, y);
                y += (int)size.Height + 10;
            }

            // Tests by category
            using (Font catFont = new Font("Arial", 9, FontStyle.Bold))
            using (Font testFont = new Font("Arial", 8))
            {
                if (_selectedTestsByCategory.Count == 0)
                {
                    g.DrawString("No lab tests selected.", testFont, Brushes.Black, left + 20, y);
                }
                else
                {
                    foreach (var cat in _selectedTestsByCategory)
                    {
                        g.DrawString(cat.Key, catFont, Brushes.Black, left + 20, y);
                        y += 18;

                        foreach (var test in cat.Value)
                        {
                            g.DrawString($"• {test}", testFont, Brushes.Black, left + 40, y);
                            y += 16;
                        }

                        using (Pen p = new Pen(Color.FromArgb(120, 0, 0, 0), 1))
                            g.DrawLine(p, left + 50, y, e.PageBounds.Width - left - 20, y);

                        y += 10;
                    }
                }
            }

            // Footer
            WaterMarkHelper.PrintFooter(g, left, e.MarginBounds.Bottom - 30, e.MarginBounds.Width + 10);
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
