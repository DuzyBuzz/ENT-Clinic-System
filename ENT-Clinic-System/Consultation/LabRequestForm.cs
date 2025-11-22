using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms; // <-- include your helper
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public LabRequestForm(int patientId, int consultationId)
        {
            InitializeComponent();

            this.patientId = patientId;
            this.consultationId = consultationId;

            crudHelper = new DGVCrudHelper(labTestsDGV, "lab_tests", new List<string> { "category", "test_name" }, "id");
            crudHelper.SetPageInfoLabel(pageInfoLabel);
            crudHelper.LoadData();
            labTestsDGV.Columns["id"].Visible = false;

            LoadLabTests();
            LoadPatientLabels(patientId);

            selectAllButton.Click += (s, e) => SetAllCheckBoxes(true);
            deselectAllButton.Click += (s, e) => SetAllCheckBoxes(false);
            nextPageButton.Click += (s, e) => crudHelper.NextPage();
            prevPageButton.Click += (s, e) => crudHelper.PreviousPage();
            addTestsButton.Click += (s, e) => AddTests();

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
                                AutoSize = false,
                                Width = colWidth - 20,
                                Height = TextRenderer.MeasureText(test, this.Font, new Size(colWidth - 20, 0),
                                         TextFormatFlags.WordBreak).Height + 10,
                                Location = new Point(panelX + colIndex * colWidth, testYOffset)
                            };

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

        private void SaveRequest()
        {
            // 🧩 Step 1: Validation
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

                // 🧩 Step 2: Optional - start a transaction for data safety
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        //// 🧩 Step 3: Delete any existing lab request with the same consultation_id
                        //using (var deleteCmd = new MySqlCommand(
                        //    "DELETE FROM lab_requests WHERE consultation_id = @consultation", conn, transaction))
                        //{
                        //    deleteCmd.Parameters.AddWithValue("@consultation", consultationId);
                        //    deleteCmd.ExecuteNonQuery();
                        //}

                        // 🧩 Step 4: Prepare the new record to insert
                        string jsonTestIds = JsonSerializer.Serialize(selectedTestIds);

                        using (var insertCmd = new MySqlCommand(
                            @"INSERT INTO lab_requests 
                      (patient_id, consultation_id, test_ids, request_date) 
                      VALUES (@patient, @consultation, @tests, @date)", conn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@patient", patientId);
                            insertCmd.Parameters.AddWithValue("@consultation", consultationId);
                            insertCmd.Parameters.AddWithValue("@tests", jsonTestIds);
                            insertCmd.Parameters.AddWithValue("@date", datePicker.Value.Date);
                            insertCmd.ExecuteNonQuery();
                        }

                        // 🧩 Step 5: Commit transaction
                        transaction.Commit();

                        MessageBox.Show("Lab request saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        crudHelper.LoadData();

                        // 🧩 Step 6: Print preview and close
                        PrintLabRequest();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        // Rollback if something failed
                        transaction.Rollback();
                        MessageBox.Show($"Error saving lab request:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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

        private void PrintLabRequest()
        {
            try
            {
                LabRequestPrintHelper printHelper = new LabRequestPrintHelper(consultationId);
                printHelper.ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load lab request for printing: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LabRequestForm_Load(object sender, EventArgs e)
        {
            ComboBoxCollectionHelper.PopulateComboBox(categoryComboBox, "lab_tests", "category");
        }

        private void saveRequestButton_Click(object sender, EventArgs e)
        {
            SaveRequest();
           
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {

        }

        private void printButton_Click(object sender, EventArgs e)
        {
            LoadLabTests();
        }
    }
}
