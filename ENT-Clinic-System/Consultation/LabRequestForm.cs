using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace ENT_Clinic_System.Consultation
{
    public partial class LabRequestForm : Form
    {
        private Dictionary<string, List<CheckBox>> categoryCheckBoxes = new Dictionary<string, List<CheckBox>>();
        private DGVCrudHelper crudHelper;
        private int patientId;
        private int consultationId;
        private PrintDocument printDocument;

        public LabRequestForm(int patientId, int consultationId)
        {
            InitializeComponent();

            crudHelper = new DGVCrudHelper(labTestsDGV, "lab_tests", new List<string> { "category", "test_name" }, "id");
            crudHelper.SetPageInfoLabel(pageInfoLabel);
            crudHelper.LoadData();
            labTestsDGV.Columns["id"].Visible = false;

            LoadLabTests();

            selectAllButton.Click += (s, e) => SetAllCheckBoxes(true);
            deselectAllButton.Click += (s, e) => SetAllCheckBoxes(false);
            nextPageButton.Click += (s, e) => crudHelper.NextPage();
            prevPageButton.Click += (s, e) => crudHelper.PreviousPage();
            addTestsButton.Click += (s, e) => AddTests();
            printButton.Click += (s, e) => LoadLabTests();

            this.patientId = patientId;
            this.consultationId = consultationId;

            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            LoadPatientLabels(patientId);
        }

        private void LoadPatientLabels(int patientId)
        {
            patientNameTextBox.Text = PatientDataHelper.GetPatientValue(patientId, "full_name");
            addressTextBox.Text = PatientDataHelper.GetPatientValue(patientId, "address");
            ageTextBox.Text = PatientDataHelper.GetPatientValue(patientId, "age");
            genderTextBox.Text = PatientDataHelper.GetPatientValue(patientId, "sex");
        }

        private void LoadLabTests()
        {
            labTestsPanel.Controls.Clear();
            categoryCheckBoxes.Clear();

            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand("SELECT category, test_name FROM lab_tests ORDER BY category, test_name", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var categoryTests = new Dictionary<string, List<string>>();
                    while (reader.Read())
                    {
                        string category = reader["category"].ToString();
                        string test = reader["test_name"].ToString();

                        if (!categoryTests.ContainsKey(category))
                            categoryTests[category] = new List<string>();
                        categoryTests[category].Add(test);
                    }

                    int panelX = 10, panelY = 10, colWidth = 260, rowSpacing = 20;
                    int colIndex = 0, maxHeightInRow = 0;

                    foreach (var cat in categoryTests.Keys)
                    {
                        Label lblCategory = new Label
                        {
                            Text = cat,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            AutoSize = true,
                            Location = new Point(panelX + colIndex * colWidth, panelY)
                        };
                        labTestsPanel.Controls.Add(lblCategory);

                        int testYOffset = lblCategory.Bottom + 5;
                        categoryCheckBoxes[cat] = new List<CheckBox>();

                        foreach (var test in categoryTests[cat])
                        {
                            CheckBox cb = new CheckBox
                            {
                                Text = test,
                                AutoSize = false, // disable autosize
                                Width = colWidth - 20, // fit inside the column width
                                Height = 40, // give more height for wrapping
                                Location = new Point(panelX + colIndex * colWidth, testYOffset)
                            };

                            // enable word wrap by setting MaximumSize
                            cb.MaximumSize = new Size(colWidth - 20, 0);
                            cb.AutoEllipsis = false;

                            // let checkbox adjust height based on wrapped text
                            cb.Height = TextRenderer.MeasureText(cb.Text, cb.Font, cb.MaximumSize, TextFormatFlags.WordBreak).Height + 10;

                            labTestsPanel.Controls.Add(cb);
                            categoryCheckBoxes[cat].Add(cb);
                            testYOffset += cb.Height + 5;

                        }

                        maxHeightInRow = Math.Max(maxHeightInRow, testYOffset);
                        colIndex++;
                        if (colIndex >= 3)
                        {
                            colIndex = 0;
                            panelY = maxHeightInRow + rowSpacing;
                            maxHeightInRow = 0;
                        }
                    }
                }
            }
        }

        private void SetAllCheckBoxes(bool value)
        {
            foreach (var list in categoryCheckBoxes.Values)
                foreach (var cb in list)
                    cb.Checked = value;
        }

        private void SaveRequest()
        {
            if (string.IsNullOrWhiteSpace(patientNameTextBox.Text))
            {
                MessageBox.Show("Please enter patient name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<int> selectedTestIds = GetSelectedTestIds();
            if (selectedTestIds.Count == 0)
            {
                MessageBox.Show("Please select at least one lab test.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string jsonTestIds = JsonSerializer.Serialize(selectedTestIds);

                using (var cmd = new MySqlCommand(
                    "INSERT INTO lab_requests (patient_id, consultation_id, test_ids, request_date) VALUES (@patient, @consultation, @tests, @date)", conn))
                {
                    cmd.Parameters.AddWithValue("@patient", patientId);
                    cmd.Parameters.AddWithValue("@consultation", consultationId);
                    cmd.Parameters.AddWithValue("@tests", jsonTestIds);
                    cmd.Parameters.AddWithValue("@date", datePicker.Value.Date);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Lab request saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            crudHelper.LoadData();
            ShowPreview();

        }

        private List<int> GetSelectedTestIds()
        {
            List<int> selectedTestIds = new List<int>();
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                var testNameToId = new Dictionary<string, int>();
                using (var cmd = new MySqlCommand("SELECT id, test_name FROM lab_tests", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        testNameToId[reader.GetString("test_name")] = reader.GetInt32("id");
                }

                foreach (var cat in categoryCheckBoxes.Keys)
                    foreach (var cb in categoryCheckBoxes[cat])
                        if (cb.Checked && testNameToId.ContainsKey(cb.Text))
                            selectedTestIds.Add(testNameToId[cb.Text]);
            }
            return selectedTestIds;
        }

        private void AddTests()
        {
            string category = categoryComboBox.Text.Trim();
            string testName = testNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Please select or enter a category.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(testName))
            {
                MessageBox.Show("Please enter a test name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    using (var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM lab_tests WHERE category=@category AND test_name=@test", conn))
                    {
                        checkCmd.Parameters.AddWithValue("@category", category);
                        checkCmd.Parameters.AddWithValue("@test", testName);
                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (exists > 0)
                        {
                            MessageBox.Show("This test already exists in the selected category.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    using (var insertCmd = new MySqlCommand("INSERT INTO lab_tests (category, test_name) VALUES (@category, @test)", conn))
                    {
                        insertCmd.Parameters.AddWithValue("@category", category);
                        insertCmd.Parameters.AddWithValue("@test", testName);
                        insertCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Lab test added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                crudHelper.LoadData();
                LoadLabTests();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding lab test: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int leftMargin = 20;
            int y = 20;

            y = WaterMarkHelper.PrintHeader(g, leftMargin, y, e.PageBounds.Width);

            DrawPatientInfo(g, leftMargin, ref y);
            DrawLabTests(g, y, e.PageBounds.Width, 3, 260);

            WaterMarkHelper.PrintFooter(g, (int)leftMargin, (int)(e.PageBounds.Bottom - 80));
        }

        private void DrawPatientInfo(Graphics g, int leftMargin, ref int y)
        {
            using (Font labelFont = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Font valueFont = new Font("Segoe UI", 9))
            {
                int x = leftMargin;
                g.DrawString("Patient: ", labelFont, Brushes.Black, x, y);
                x += 60;
                g.DrawString(patientNameTextBox.Text, valueFont, Brushes.Black, x, y);
                x += 150;
                g.DrawString("Address: ", labelFont, Brushes.Black, x, y);
                x += 60;
                g.DrawString(addressTextBox.Text, valueFont, Brushes.Black, x, y);
                x += 150;
                g.DrawString("Age: ", labelFont, Brushes.Black, x, y);
                x += 35;
                g.DrawString(ageTextBox.Text, valueFont, Brushes.Black, x, y);
                x += 50;
                g.DrawString("Gender: ", labelFont, Brushes.Black, x, y);
                x += 55;
                g.DrawString(genderTextBox.Text, valueFont, Brushes.Black, x, y);
                x += 80;
                g.DrawString("Date: ", labelFont, Brushes.Black, x, y);
                x += 40;
                g.DrawString(datePicker.Value.ToShortDateString(), valueFont, Brushes.Black, x, y);
                y += 40;
            }
        }

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

                foreach (var cat in categoryCheckBoxes.Keys)
                {
                    int catX = panelX + colIndex * colWidth;
                    int catY = panelY;

                    // Draw category name
                    g.DrawString(cat, categoryFont, Brushes.Black, catX, catY);

                    int testYOffset = catY + 20;

                    foreach (var cb in categoryCheckBoxes[cat])
                    {
                        // Checkbox box
                        Rectangle boxRect = new Rectangle(catX, testYOffset, 14, 14);
                        g.DrawRectangle(Pens.Black, boxRect);

                        if (cb.Checked)
                        {
                            g.DrawLine(Pens.Black, boxRect.Left + 2, boxRect.Top + 7,
                                boxRect.Left + 6, boxRect.Bottom - 2);
                            g.DrawLine(Pens.Black, boxRect.Left + 6, boxRect.Bottom - 2,
                                boxRect.Right - 2, boxRect.Top + 2);
                        }

                        // Text wrapping rectangle (beside checkbox, fits in column width)
                        RectangleF textRect = new RectangleF(
                            boxRect.Right + 5,
                            testYOffset - 2,
                            colWidth - 25,  // leave margin inside column
                            100             // max height allowed per test
                        );

                        // Word wrapping format
                        using (StringFormat sf = new StringFormat())
                        {
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Near;
                            sf.Trimming = StringTrimming.Word;
                            sf.FormatFlags = StringFormatFlags.LineLimit;

                            g.DrawString(cb.Text, testFont, Brushes.Black, textRect, sf);
                        }

                        // Measure text height to move Y properly
                        SizeF textSize = g.MeasureString(cb.Text, testFont, (int)textRect.Width);
                        int textHeight = (int)Math.Ceiling(textSize.Height);

                        // Move Y based on text height (minimum 25 to align boxes nicely)
                        testYOffset += Math.Max(25, textHeight + 5);
                    }

                    maxHeightInRow = Math.Max(maxHeightInRow, testYOffset);
                    colIndex++;

                    // Move to next row if max columns reached
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
                    // hide default print button
                    foreach (ToolStripItem item in tool.Items)
                        if (item is ToolStripButton btn && btn.ToolTipText.ToLower().Contains("print"))
                            btn.Visible = false;

                    ToolStripButton customPrint = new ToolStripButton("Print");
                    customPrint.Click += delegate
                    {
                        PrintDialog printDialog = new PrintDialog();
                        printDialog.Document = printDocument;
                        printDialog.AllowSomePages = true;
                        printDialog.AllowSelection = true;

                        if (printDialog.ShowDialog() == DialogResult.OK)
                            printDocument.Print();

                        printDialog.Dispose();
                    };
                    tool.Items.Insert(0, customPrint);
                }
            };

            preview.ShowDialog();
        }

        private void LabRequestForm_Load(object sender, EventArgs e)
        {
            ComboBoxCollectionHelper.PopulateComboBox(categoryComboBox, "lab_tests", "category");
        }

        private void groupBoxAvailable_Enter(object sender, EventArgs e)
        {

        }

        private void saveRequestButton_Click(object sender, EventArgs e)
        {
            SaveRequest();
        }
    }
}
