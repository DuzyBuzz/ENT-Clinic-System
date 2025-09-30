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
        private int consultationId;
        private PrintDocument printDocument;

        // Patient info
        private string patientName = "";
        private string patientAddress = "";
        private string patientAge = "";
        private string patientGender = "";
        private DateTime requestDate;

        // Lab tests organized by category, with checked state
        private Dictionary<string, List<(string TestName, bool IsChecked)>> labTestsByCategory
            = new Dictionary<string, List<(string, bool)>>();

        public LabRequestPrintHelper(int consultationId)
        {
            this.consultationId = consultationId;
            LoadData();

            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        // ===========================
        // LOAD DATA FROM DATABASE
        // ===========================
        private void LoadData()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // Get the latest lab request for this consultation
                    string query = @"
                        SELECT p.full_name, p.address, p.age, p.sex, lr.request_date, lr.test_ids
                        FROM lab_requests lr
                        JOIN patients p ON lr.patient_id = p.patient_id
                        WHERE lr.consultation_id = @consultationId
                        ORDER BY lr.request_date DESC
                        LIMIT 1";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultationId", consultationId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new Exception("No lab request found for this consultation.");

                            patientName = reader["full_name"].ToString();
                            patientAddress = reader["address"].ToString();
                            patientAge = reader["age"].ToString();
                            patientGender = reader["sex"].ToString();
                            requestDate = Convert.ToDateTime(reader["request_date"]);

                            // Deserialize the test IDs (checked)
                            string jsonTests = reader["test_ids"].ToString();
                            List<int> checkedTestIds = JsonSerializer.Deserialize<List<int>>(jsonTests) ?? new List<int>();

                            reader.Close(); // Close before executing another command

                            // Load all lab tests
                            string testQuery = "SELECT id, category, test_name FROM lab_tests ORDER BY category, test_name";
                            using (var testCmd = new MySqlCommand(testQuery, conn))
                            using (var testReader = testCmd.ExecuteReader())
                            {
                                while (testReader.Read())
                                {
                                    string cat = testReader["category"].ToString();
                                    string testName = testReader["test_name"].ToString();
                                    int testId = Convert.ToInt32(testReader["id"]);

                                    bool isChecked = checkedTestIds.Contains(testId);

                                    if (!labTestsByCategory.ContainsKey(cat))
                                        labTestsByCategory[cat] = new List<(string, bool)>();

                                    labTestsByCategory[cat].Add((testName, isChecked));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading lab request: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===========================
        // PRINT PAGE
        // ===========================
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int leftMargin = 20;
            int y = 20;

            y = WaterMarkHelper.PrintHeader(g, leftMargin, y, e.PageBounds.Width);

            DrawPatientInfo(g, leftMargin, ref y);
            DrawLabTests(g, y, e.PageBounds.Width, 3, 260);

            WaterMarkHelper.PrintFooter(g, leftMargin, e.PageBounds.Bottom - 80);
        }

        // ===========================
        // DRAW PATIENT INFO
        // ===========================
        private void DrawPatientInfo(Graphics g, int leftMargin, ref int y)
        {
            using (Font labelFont = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Font valueFont = new Font("Segoe UI", 9))
            {
                int x = leftMargin;
                g.DrawString("Patient: ", labelFont, Brushes.Black, x, y);
                x += 60;
                g.DrawString(patientName, valueFont, Brushes.Black, x, y);
                x += 150;
                g.DrawString("Address: ", labelFont, Brushes.Black, x, y);
                x += 60;

                g.DrawString(patientAddress, valueFont, Brushes.Black, x, y);
                x += 150;
                g.DrawString("Age: ", labelFont, Brushes.Black, x, y);
                x += 35;
                g.DrawString(patientAge, valueFont, Brushes.Black, x, y);
                x += 50;
                g.DrawString("Gender: ", labelFont, Brushes.Black, x, y);
                x += 55;
                g.DrawString(patientGender, valueFont, Brushes.Black, x, y);
                x += 80;
                g.DrawString("Date: ", labelFont, Brushes.Black, x, y);
                x += 40;
                g.DrawString(requestDate.ToShortDateString(), valueFont, Brushes.Black, x, y);
                y += 40;
            }
        }

        // ===========================
        // DRAW LAB TESTS WITH CHECKBOXES
        // ===========================
        private void DrawLabTests(Graphics g, int yStart, int pageWidth, int colCount, int colWidth)
        {
            using (Font categoryFont = new Font("Segoe UI", 10, FontStyle.Bold))
            using (Font testFont = new Font("Segoe UI", 9))
            {
                int panelY = yStart;
                int totalColsWidth = colCount * colWidth;
                int panelX = (pageWidth - totalColsWidth) / 2;
                int rowSpacing = 20;
                int colIndex = 0;
                int maxHeightInRow = 0;

                foreach (var cat in labTestsByCategory.Keys)
                {
                    int catX = panelX + colIndex * colWidth;
                    int catY = panelY;

                    g.DrawString(cat, categoryFont, Brushes.Black, catX, catY);
                    int testYOffset = catY + 20;

                    foreach (var test in labTestsByCategory[cat])
                    {
                        Rectangle boxRect = new Rectangle(catX, testYOffset, 14, 14);
                        g.DrawRectangle(Pens.Black, boxRect);

                        if (test.IsChecked)
                        {
                            g.DrawLine(Pens.Black, boxRect.Left + 2, boxRect.Top + 7,
                                boxRect.Left + 6, boxRect.Bottom - 2);
                            g.DrawLine(Pens.Black, boxRect.Left + 6, boxRect.Bottom - 2,
                                boxRect.Right - 2, boxRect.Top + 2);
                        }

                        RectangleF textRect = new RectangleF(boxRect.Right + 5, testYOffset - 2, colWidth - 25, 100);
                        using (StringFormat sf = new StringFormat())
                        {
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Near;
                            sf.Trimming = StringTrimming.Word;
                            sf.FormatFlags = StringFormatFlags.LineLimit;

                            g.DrawString(test.TestName, testFont, Brushes.Black, textRect, sf);
                        }

                        SizeF textSize = g.MeasureString(test.TestName, testFont, (int)textRect.Width);
                        int textHeight = (int)Math.Ceiling(textSize.Height);
                        testYOffset += Math.Max(25, textHeight + 5);
                    }

                    maxHeightInRow = Math.Max(maxHeightInRow, testYOffset);
                    colIndex++;
                    if (colIndex >= colCount)
                    {
                        colIndex = 0;
                        panelY = maxHeightInRow + rowSpacing;
                        maxHeightInRow = 0;
                    }
                }

                if (colIndex != 0)
                    panelY = maxHeightInRow + 40;
            }
        }

        // ===========================
        // SHOW PRINT PREVIEW
        // ===========================
        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1000,
                Height = 700
            };

            preview.Shown += delegate
            {
                ToolStrip tool = preview.Controls.OfType<ToolStrip>().FirstOrDefault();
                if (tool != null)
                {
                    // Hide default print button
                    foreach (ToolStripItem item in tool.Items)
                        if (item is ToolStripButton btn && btn.ToolTipText.ToLower().Contains("print"))
                            btn.Visible = false;

                    ToolStripButton customPrint = new ToolStripButton("Print");
                    customPrint.Click += delegate
                    {
                        PrintDialog printDialog = new PrintDialog
                        {
                            Document = printDocument,
                            AllowSomePages = true,
                            AllowSelection = true
                        };

                        if (printDialog.ShowDialog() == DialogResult.OK)
                            printDocument.Print();

                        printDialog.Dispose();
                    };
                    tool.Items.Insert(0, customPrint);
                }
            };

            preview.ShowDialog();
        }
    }
}
