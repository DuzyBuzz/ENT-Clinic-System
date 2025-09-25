using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.Json;
using System.Windows.Forms;


namespace ENT_Clinic_System.UI
{
    public partial class LabRequestForm : Form
    {
        private Dictionary<string, List<CheckBox>> categoryCheckBoxes = new Dictionary<string, List<CheckBox>>();
        private DGVCrudHelper crudHelper;

        public LabRequestForm()
        {
            InitializeComponent();

            // Initialize CRUD helper
            crudHelper = new DGVCrudHelper(labTestsDGV, "lab_tests", new List<string> { "id", "category", "test_name" }, "id");
            crudHelper.SetPageInfoLabel(pageInfoLabel);
            crudHelper.LoadData();
            labTestsDGV.Columns["id"].Visible = false;

            LoadLabTests();

            // Event handlers
            selectAllButton.Click += (s, e) => SetAllCheckBoxes(true);
            deselectAllButton.Click += (s, e) => SetAllCheckBoxes(false);
            saveRequestButton.Click += SaveRequestButton_Click;
            nextPageButton.Click += (s, e) => crudHelper.NextPage();
            prevPageButton.Click += (s, e) => crudHelper.PreviousPage();
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
                                AutoSize = true,
                                Location = new Point(panelX + colIndex * colWidth, testYOffset)
                            };
                            labTestsPanel.Controls.Add(cb);
                            categoryCheckBoxes[cat].Add(cb);
                            testYOffset += 25;
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

        private void SaveRequestButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(patientNameTextBox.Text))
            {
                MessageBox.Show("Please enter patient name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Collect selected test IDs
            List<int> selectedTestIds = new List<int>();

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // Get all lab tests to map name -> ID
                var testNameToId = new Dictionary<string, int>();
                using (var cmd = new MySqlCommand("SELECT id, test_name FROM lab_tests", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        testNameToId[reader.GetString("test_name")] = reader.GetInt32("id");
                }

                // Collect selected test IDs from checkboxes
                foreach (var cat in categoryCheckBoxes.Keys)
                {
                    foreach (var cb in categoryCheckBoxes[cat])
                    {
                        if (cb.Checked && testNameToId.ContainsKey(cb.Text))
                            selectedTestIds.Add(testNameToId[cb.Text]);
                    }
                }

                if (selectedTestIds.Count == 0)
                {
                    MessageBox.Show("Please select at least one lab test.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Insert a single lab request with JSON array of test IDs
                string jsonTestIds = JsonSerializer.Serialize(selectedTestIds);


                using (var cmd = new MySqlCommand(
                    "INSERT INTO lab_requests (patient_id, test_ids, request_date) VALUES (@patient, @tests, @date)", conn))
                {
                    // TODO: replace with actual patient ID lookup
                    int patientId = 2;

                    cmd.Parameters.AddWithValue("@patient", patientId);
                    cmd.Parameters.AddWithValue("@tests", jsonTestIds);
                    cmd.Parameters.AddWithValue("@date", datePicker.Value.Date);
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Lab request saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Optionally, refresh your CRUD DataGridView if you want
            crudHelper.LoadData();
        }

        private void addTestsButton_Click(object sender, EventArgs e)
        {
            // 1. Validate inputs
            string category = categoryComboBox.Text.Trim();
            string testName = testNameComboBox.Text.Trim();

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

                    // 2. Check if this test already exists in the same category
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

                    // 3. Insert new test
                    using (var insertCmd = new MySqlCommand("INSERT INTO lab_tests (category, test_name) VALUES (@category, @test)", conn))
                    {
                        insertCmd.Parameters.AddWithValue("@category", category);
                        insertCmd.Parameters.AddWithValue("@test", testName);

                        insertCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Lab test added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 4. Optional: refresh the lab test DataGridView / panel
                crudHelper.LoadData();   // if using your DGVCrudHelper
                LoadLabTests();          // reload dynamic checkboxes panel
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding lab test: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    private void printButton_Click(object sender, EventArgs e)
    {
        PrintDocument printDoc = new PrintDocument();
        printDoc.PrintPage += PrintDoc_PrintPage;

        PrintPreviewDialog preview = new PrintPreviewDialog();
        preview.Document = printDoc;
        preview.ShowDialog();
    }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int leftMargin = 20;
            int y = 20; // starting Y position

            // 1. Header
            y = WaterMarkHelper.PrintHeader(g, leftMargin, y, e.PageBounds.Width);

            // 2. Patient Info in one line (bold labels)
            using (Font labelFont = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Font valueFont = new Font("Segoe UI", 9))
            {
                int x = leftMargin;
                g.DrawString("Patient: ", labelFont, Brushes.Black, x, y);
                x += 60;
                g.DrawString(patientNameTextBox.Text, valueFont, Brushes.Black, x, y);
                x += 150;
                g.DrawString("Age: ", labelFont, Brushes.Black, x, y);
                x += 35;
                g.DrawString(ageTextBox.Text, valueFont, Brushes.Black, x, y);
                x += 50;
                g.DrawString("Gender: ", labelFont, Brushes.Black, x, y);
                x += 55;
                g.DrawString(genderComboBox.Text, valueFont, Brushes.Black, x, y);
                x += 80;
                g.DrawString("Date: ", labelFont, Brushes.Black, x, y);
                x += 40;
                g.DrawString(datePicker.Value.ToShortDateString(), valueFont, Brushes.Black, x, y);
                y += 30;
            }

            // 3. Lab Tests Grid (3 columns)
            using (Font categoryFont = new Font("Segoe UI", 10, FontStyle.Bold))
            using (Font testFont = new Font("Segoe UI", 9))
            {
                int columnWidth = (int)((e.PageBounds.Width - 2 * leftMargin) / 3);
                int startX = leftMargin;
                int startY = y;
                int colIndex = 0;
                int rowSpacing = 20;
                int rowMaxY = startY;

                foreach (var category in categoryCheckBoxes.Keys)
                {
                    int colX = startX + colIndex * columnWidth;
                    int colY = startY;

                    // Draw category title
                    g.DrawString(category, categoryFont, Brushes.Black, colX, colY);
                    colY += 20;

                    // Draw tests under the category
                    foreach (var cb in categoryCheckBoxes[category])
                    {
                        string text = cb.Checked ? "✔ " + cb.Text : cb.Text;
                        g.DrawString(text, testFont, Brushes.Black, colX, colY);
                        colY += 20;
                    }

                    // Update the maximum Y position for the current row
                    rowMaxY = Math.Max(rowMaxY, colY);

                    colIndex++;

                    // If 3 columns filled, move to next row
                    if (colIndex >= 3)
                    {
                        colIndex = 0;
                        startY = rowMaxY + rowSpacing;
                        rowMaxY = startY;
                    }
                }

                // If last row has less than 3 columns, ensure proper spacing
                if (colIndex != 0)
                    startY = rowMaxY + 40;

                y = startY; // final y before footer
            }

            // 4. Footer
            y = WaterMarkHelper.PrintFooter(g, leftMargin, y);
        }







    }
}
