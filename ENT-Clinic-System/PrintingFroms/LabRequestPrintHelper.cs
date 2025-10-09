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

        // Lab tests organized by category
        private Dictionary<string, List<(string TestName, bool IsChecked)>> labTestsByCategory
            = new Dictionary<string, List<(string, bool)>>();

        public LabRequestPrintHelper(int consultationId)
        {
            this.consultationId = consultationId;
            LoadData();

            printDocument = new PrintDocument();

            // Set A5 Portrait
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("A5", 583, 827);
            printDocument.DefaultPageSettings.Margins = new Margins(30, 30, 30, 40);

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

                            string jsonTests = reader["test_ids"].ToString();
                            List<int> checkedTestIds = JsonSerializer.Deserialize<List<int>>(jsonTests) ?? new List<int>();

                            reader.Close();

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
            int leftMargin = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;

            // Header
            y = WaterMarkHelper.PrintHeader(g, leftMargin, y, e.MarginBounds.Width);

            // Patient Info
            DrawPatientInfo(g, leftMargin, ref y, e.MarginBounds.Width);

            // Lab Tests (2 columns for A5)
            DrawLabTests(g, y + 5, e.MarginBounds.Width, 2, e.MarginBounds.Width / 2 - 10);

            // Footer
            WaterMarkHelper.PrintFooter(g, leftMargin, e.MarginBounds.Bottom - 60, e.MarginBounds.Width);
        }

        // ===========================
        // DRAW PATIENT INFO
        // ===========================
        private void DrawPatientInfo(Graphics g, int leftMargin, ref int y, int pageWidth)
        {
            using (Font labelFont = new Font("Arial", 9, FontStyle.Bold))
            using (Font valueFont = new Font("Arial", 9))
            {
                int sectionWidth = pageWidth - leftMargin * 2;
                int x = leftMargin;

                g.DrawString($"Patient: {patientName}", valueFont, Brushes.Black, x, y);
                g.DrawString($"Date: {requestDate:MM/dd/yyyy}", valueFont, Brushes.Black, x + sectionWidth - 100, y);
                y += 18;

                g.DrawString($"Address: {patientAddress}", valueFont, Brushes.Black, x, y);
                y += 18;

                g.DrawString($"Age: {patientAge}", valueFont, Brushes.Black, x, y);
                g.DrawString($"Gender: {patientGender}", valueFont, Brushes.Black, x + 80, y);
                y += 25;
            }
        }

        // ===========================
        // DRAW LAB TESTS
        // ===========================
        // ===========================
        // DRAW LAB TESTS (Centered horizontally, left-aligned text)
        // ===========================
        private void DrawLabTests(Graphics g, int yStart, int pageWidth, int colCount, int colWidth)
        {
            using (Font categoryFont = new Font("Arial", 9, FontStyle.Bold))
            using (Font testFont = new Font("Arial", 8))
            {
                // Calculate total width of all columns
                int totalColsWidth = colCount * colWidth;
                int startX = (pageWidth - totalColsWidth) / 2 +20; // center columns horizontally

                int colIndex = 0;
                int currentY = yStart;
                int maxColHeight = 0;

                foreach (var category in labTestsByCategory)
                {
                    int colX = startX + (colIndex * colWidth);
                    int catY = currentY;

                    // Draw category title
                    g.DrawString(category.Key, categoryFont, Brushes.Black, colX, catY);
                    catY += 16;

                    // Draw tests under category
                    foreach (var test in category.Value)
                    {
                        // Checkbox rectangle
                        Rectangle boxRect = new Rectangle(colX, catY, 10, 10);
                        g.DrawRectangle(Pens.Black, boxRect);

                        // Draw check mark if selected
                        if (test.IsChecked)
                        {
                            g.DrawLine(Pens.Black, boxRect.Left + 2, boxRect.Top + 5,
                                boxRect.Left + 5, boxRect.Bottom - 2);
                            g.DrawLine(Pens.Black, boxRect.Left + 5, boxRect.Bottom - 2,
                                boxRect.Right - 2, boxRect.Top + 2);
                        }

                        // Draw test name left-aligned next to checkbox
                        RectangleF textRect = new RectangleF(boxRect.Right + 4, catY - 1, colWidth - 20, 40);
                        g.DrawString(test.TestName, testFont, Brushes.Black, textRect, new StringFormat
                        {
                            Alignment = StringAlignment.Near, // left-aligned
                            LineAlignment = StringAlignment.Near
                        });

                        // Measure text height to move Y
                        SizeF size = g.MeasureString(test.TestName, testFont, (int)textRect.Width);
                        catY += (int)size.Height + 6;
                    }

                    maxColHeight = Math.Max(maxColHeight, catY);
                    colIndex++;

                    // Move to next row if all columns filled
                    if (colIndex >= colCount)
                    {
                        colIndex = 0;
                        currentY = maxColHeight + 12;
                        maxColHeight = 0;
                    }
                }
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
                        using (PrintDialog dlg = new PrintDialog { Document = printDocument })
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
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
