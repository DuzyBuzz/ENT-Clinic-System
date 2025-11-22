using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.Consultation
{
    public partial class ConsultationHistoryControl : Form
    {

        private int _patientId;
        private DGVViewHelper viewHelper;
        private int _consultationId;
        private DGVViewCrudHelper _prescriptionHelper;
        private DGVViewCrudHelper _otherPrescriptionHelper;
        private DGVViewCrudHelper _labRequestHelper;
        public ConsultationHistoryControl(int patientId)
        {
            InitializeComponent();
            consultationDateDataGridView.CellClick += ConsultationDateDGV_CellClick;

            _patientId = patientId; // you forgot this

            _prescriptionHelper = new DGVViewCrudHelper(
                prescriptionDGV,
                "v_prescription_with_items",
                "prescription_id",
                "prescription"
            );

            _otherPrescriptionHelper = new DGVViewCrudHelper(
                othersPrescriptionDGV,
                "v_prescription_other_with_items",
                "id",
                "prescription_other"
            );
            _labRequestHelper = new DGVViewCrudHelper(
                labRequestDGV,
                "v_lab_requests_with_tests",
                "request_id",
                "lab_requests"
            );

        }
        private void LoadConsultationAndHealthRecord()
        {
            if (_consultationId <= 0) return;
            ClearAllConsultationDGVs();

            using (var conn = DBConfig.GetConnection())
            {
                try
                {
                    conn.Open();

                    // 1️⃣ Load health record (patient-based)
                    string healthRecordQuery = @"
                SELECT past_medical_history, family_history, personal_social_history,
                       bp, temperature, pr, rr, ht, wt, general_appearance, skin,
                       head_and_face, eyes, neck, chest_lungs, heart, abdomen,
                       extremities, neurologic, allergies
                FROM health_record
                WHERE patient_id = @patientId
                ORDER BY health_record_id DESC
                LIMIT 1;";

                    using (var cmd = new MySqlCommand(healthRecordQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@patientId", _patientId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FillDgvFromString(pastMedicalHistoryDGV, reader["past_medical_history"].ToString());
                                FillDgvFromString(familyHistoryDGV, reader["family_history"].ToString());
                                FillDgvFromString(personalSocialHistoryDGV, reader["personal_social_history"].ToString());
                                FillDgvFromString(allergiesDGV, reader["allergies"].ToString());

                                bpTextBox.Text = reader["bp"].ToString();
                                temperatureTextBox.Text = reader["temperature"].ToString();
                                prTextBox.Text = reader["pr"].ToString();
                                rrTextBox.Text = reader["rr"].ToString();
                                htTextBox.Text = reader["ht"].ToString();
                                wtTextBox.Text = reader["wt"].ToString();

                                generalApperanceComboBox.Text = reader["general_appearance"].ToString();
                                skinComboBox.Text = reader["skin"].ToString();
                                headAndFaceComboBox.Text = reader["head_and_face"].ToString();
                                eyesComboBox.Text = reader["eyes"].ToString();
                                neckComboBox.Text = reader["neck"].ToString();
                                chestLungsComboBox.Text = reader["chest_lungs"].ToString();
                                heartComboBox.Text = reader["heart"].ToString();
                                abdomenComboBox.Text = reader["abdomen"].ToString();
                                extremetiesComboBox.Text = reader["extremities"].ToString();
                                neurologicComboBox.Text = reader["neurologic"].ToString();
                            }
                        }
                    }

                    // 2️⃣ Load consultation (still consultation-based)
                    string consultQuery = @"
                SELECT chief_complaint, history, ear_exam, nose_exam, throat_exam, others_exam,
                       maxillofacial_exam, head_and_neck_exam, diagnosis, recommendations,
                       notes, follow_up_date
                FROM consultation
                WHERE consultation_id = @consultationId
                LIMIT 1;";

                    using (var cmd = new MySqlCommand(consultQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultationId", _consultationId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                FillDgvFromString(earsDGV, reader["ear_exam"].ToString());
                                FillDgvFromString(noseDGV, reader["nose_exam"].ToString());
                                FillDgvFromString(throatDGV, reader["throat_exam"].ToString());
                                FillDgvFromString(othersDGV, reader["others_exam"].ToString());
                                FillDgvFromString(maxillofacialDGV, reader["maxillofacial_exam"].ToString());
                                FillDgvFromString(headNeckDGV, reader["head_and_neck_exam"].ToString());
                                FillDgvFromString(diagnosisDGV, reader["diagnosis"].ToString());
                                FillDgvFromString(recommendationsDGV, reader["recommendations"].ToString());

                                complaintsRichTextBox.Text = reader["chief_complaint"].ToString();
                                recentIllnessRichTextBox.Text = reader["history"].ToString();
                                noteRichTextBox.Text = reader["notes"].ToString();

                                if (!reader.IsDBNull(reader.GetOrdinal("follow_up_date")))
                                {
                                    followUpCheckBox.Checked = true;
                                    followUpDateTimePicker.Value = Convert.ToDateTime(reader["follow_up_date"]);
                                }
                                else
                                {
                                    followUpCheckBox.Checked = false;
                                }                    // --- Select first non-empty exam DGV ---
                                FocusEntTabWithMostRows();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading consultation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Selects the first DataGridView among the exams that has at least one row
        /// </summary>
        /// <summary>
        /// Selects the first non-empty exam DataGridView and activates its tab inside entTabControl
        /// </summary>
        private void FocusEntTabWithMostRows()
        {
            // Map each DataGridView to its corresponding TabPage
            var dgvTabMap = new (DataGridView dgv, TabPage tab)[]
            {
        (earsDGV, earsTabPage),
        (noseDGV, noseTabPage),
        (throatDGV, throatTabPage),
        (othersDGV, othersTabPage),
        (maxillofacialDGV, maxillofacialTabPage),
        (headNeckDGV, headNeckTabPage)
            };

            int maxRows = 0;
            TabPage tabToFocus = null;

            foreach (var (dgv, tab) in dgvTabMap)
            {
                int rowCount = dgv.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);
                if (rowCount > maxRows)
                {
                    maxRows = rowCount;
                    tabToFocus = tab;
                }
            }

            if (tabToFocus != null)
            {
                entTabControl.SelectedTab = tabToFocus;
                tabToFocus.Focus(); // optional
            }
            else
            {
                entTabControl.SelectedIndex = 0; // default tab if all empty
            }
        }





        // Helper to split CSV string into DGV rows
        private void FillDgvFromString(DataGridView dgv, string data)
        {
            dgv.Rows.Clear();
            if (!string.IsNullOrWhiteSpace(data))
            {
                var items = data.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in items)
                    AddDgvRow(dgv, item.Trim());
            }
        }

        private void ClearAllConsultationDGVs()
        {
            earsDGV.Rows.Clear();
            noseDGV.Rows.Clear();
            throatDGV.Rows.Clear();
            othersDGV.Rows.Clear();
            maxillofacialDGV.Rows.Clear();
            headNeckDGV.Rows.Clear();
            diagnosisDGV.Rows.Clear();
            recommendationsDGV.Rows.Clear();
            pastMedicalHistoryDGV.Rows.Clear();
            familyHistoryDGV.Rows.Clear();
            personalSocialHistoryDGV.Rows.Clear();
            allergiesDGV.Rows.Clear();
        }
        /// <summary>
        /// Adds a single string value as a new row to the specified DataGridView
        /// </summary>
        private void AddDgvRow(DataGridView dgv, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                dgv.Rows.Add(value);
        }


        private void ConsultationDateDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // ignore header

            DataGridViewRow row = consultationDateDataGridView.Rows[e.RowIndex];

            if (row.Cells["consultation_id"].Value != null)
            {
                _consultationId = Convert.ToInt32(row.Cells["consultation_id"].Value);
            }

            WireLoads();
        }
        private void LoadPrescriptions()
        {
            if (_consultationId <= 0)
                return;

            Debug.WriteLine("Loading prescriptions for consultation ID: " + _consultationId);

            // MAIN prescriptions
            _prescriptionHelper.LoadRowsByColumn(
                "v_prescription_with_items",
                "consultation_id",
                _consultationId
            );

            // OTHER ITEMS prescriptions
            _otherPrescriptionHelper.LoadRowsByColumn(
                "v_prescription_other_with_items",
                "consultation_id",
                _consultationId
            );
        }
        private void LoadLaboratory()
        {
            if (_consultationId <= 0)
                return;


            // OTHER ITEMS prescriptions
            _labRequestHelper.LoadRowsByColumn(
                "v_lab_requests_with_tests",
                "consultation_id",
                _consultationId
            );

            LabResultsForm labResultsForm = new LabResultsForm(_patientId, _consultationId);
            LoadUserControl(labResultsForm, labResultsPanel);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ConsultationHistoryControl_Load(object sender, EventArgs e)
        {
            LoadPatientLabels(_patientId);





            LoadComboBoxes();
            LoadAutoComplete();
            LoadConsultationDate(_patientId);


        }
        private void LoadPatientLabels(int patientId)
        {
            fullNameLabel.Text = PatientDataHelper.GetPatientValue(patientId, "full_name");
            addressLabel.Text = PatientDataHelper.GetPatientValue(patientId, "address");
            ageLabel.Text = PatientDataHelper.GetPatientValue(patientId, "age");
            sexLabel.Text = PatientDataHelper.GetPatientValue(patientId, "sex");
            civilStatusLabel.Text = PatientDataHelper.GetPatientValue(patientId, "civil_status");
            patientContactNumberLabel.Text = PatientDataHelper.GetPatientValue(patientId, "patient_contact_number");
            referedByLabel.Text = PatientDataHelper.GetPatientValue(patientId, "referred_by");


            // Load photo
            Image photo = PatientDataHelper.GetPatientPhoto(patientId);
            if (photo != null)
                patientProfilePictureBox.Image = photo;
        }
        private void LoadConsultationDate(int patientID)
        {
            List<string> consultationColumns = new List<string>
    {
        "consultation_id",
        "patient_id",
        "consultation_date",
    };

            viewHelper = new DGVViewHelper(
                consultationDateDataGridView,
                "consultation",
                consultationColumns,
                "patient_id"
            );

            viewHelper.LoadData(patientID);

            // Sort by consultation_date DESC (latest on top)
            if (consultationDateDataGridView.DataSource is DataTable dt)
            {
                dt.DefaultView.Sort = "consultation_date DESC";
                consultationDateDataGridView.DataSource = dt.DefaultView.ToTable();
            }

            // Hide ID and patient ID columns
            if (consultationDateDataGridView.Columns.Contains("consultation_id"))
                consultationDateDataGridView.Columns["consultation_id"].Visible = false;

            if (consultationDateDataGridView.Columns.Contains("patient_id"))
                consultationDateDataGridView.Columns["patient_id"].Visible = false;
            // 👉 Auto-select first row after everything is loaded
            SelectFirstConsultationRow();
        }
        private void SelectFirstConsultationRow()
        {
            if (consultationDateDataGridView.Rows.Count == 0)
                return;

            // Select first row
            consultationDateDataGridView.ClearSelection();
            consultationDateDataGridView.Rows[0].Selected = true;

            // Manually trigger CellClick event
            var firstRow = consultationDateDataGridView.Rows[0];
            ConsultationDateDGV_CellClick(
                consultationDateDataGridView,
                new DataGridViewCellEventArgs(0, 0)
            );
        }

        private void LoadComboBoxes()
        {
            // Example for all ComboBoxes in your form
            ComboBoxCollectionHelper.PopulateComboBox(generalApperanceComboBox, "health_record", "general_appearance");
            ComboBoxCollectionHelper.PopulateComboBox(skinComboBox, "health_record", "skin");
            ComboBoxCollectionHelper.PopulateComboBox(headAndFaceComboBox, "health_record", "head_and_face");
            ComboBoxCollectionHelper.PopulateComboBox(eyesComboBox, "health_record", "eyes");
            ComboBoxCollectionHelper.PopulateComboBox(neckComboBox, "health_record", "neck");
            ComboBoxCollectionHelper.PopulateComboBox(chestLungsComboBox, "health_record", "chest_lungs");
            ComboBoxCollectionHelper.PopulateComboBox(heartComboBox, "health_record", "heart");
            ComboBoxCollectionHelper.PopulateComboBox(abdomenComboBox, "health_record", "abdomen");
            ComboBoxCollectionHelper.PopulateComboBox(extremetiesComboBox, "health_record", "extremities");
            ComboBoxCollectionHelper.PopulateComboBox(neurologicComboBox, "health_record", "neurologic");
        }

        private void LoadAutoComplete()
        {
            // Health Record Autocomplete (DGVs)
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(pastMedicalHistoryDGV, "history", "history");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(allergiesDGV, "allergies", "allergies");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(familyHistoryDGV, "family_history", "family_history");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(personalSocialHistoryDGV, "personal_social_history", "personal_social_history");

            // Other Consultation DGVs
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(earsDGV, "ears", "ear_exam");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(noseDGV, "nose", "nose_exam");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(throatDGV, "throat", "throat_exam");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(maxillofacialDGV, "maxillofacial", "maxillofacial_exam");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(headNeckDGV, "head_and_neck", "head_and_neck_exam");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(othersDGV, "others", "others_exam");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(diagnosisDGV, "diagnosis", "diagnosis");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(recommendationsDGV, "recommendations", "recommendations");
        }

        private void LoadUserControl(UserControl uc, Panel panel)
        {
            panel.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel.Controls.Add(uc);
            uc.BringToFront();
        }


        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            WireLoads();

        }
        private void WireLoads()
        {
            var selectedTab = tabControl1.SelectedTab;

            if (selectedTab == tabPrescriptions)
            {
                LoadPrescriptions();
            }

            if (selectedTab == tabLaboratories)
            {
                LoadLaboratory();
            }
            if (selectedTab == tabAttachments)
            {
                LoadAttachments();
            }
            if (selectedTab == tabConsultation)
            {
                LoadConsultationAndHealthRecord();
            }
        }
        private void LoadAttachments()
        {
            PrintAttachments printAttachments = new PrintAttachments(_consultationId);
            LoadUserControl(printAttachments, AttachmentsPanel);
        }






        private void createNewLabRequest_Click(object sender, EventArgs e)
        {
            LabRequestForm labRequestForm = new LabRequestForm(_patientId, _consultationId);
            labRequestForm.ShowDialog();
        }

        private void printLabRequest_Click(object sender, EventArgs e)
        {
            var helper = new LabRequestPrintHelper(_consultationId);
            helper.ShowPreview();
        }

        private void newPrescriptionButton_Click_1(object sender, EventArgs e)
        {
            // Assuming you have a form for creating prescriptions
            var prescriptionForm = new PrescriptionForm(_patientId, _consultationId);
            prescriptionForm.ShowDialog();
        }
        private void printPrescriptionButton_Click(object sender, EventArgs e)
        {
            var printer = new PrescriptionPrintHelper(_consultationId);
            printer.ShowPreview();
        }

        private void updateConsultationButton_Click(object sender, EventArgs e)
        {
            if (_consultationId <= 0) return;

            using (MySqlConnection conn = DBConfig.GetConnection())
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1️⃣ Update health_record_history
                        string updateHistory = @"
                    UPDATE health_record_history
                    SET bp=@bp, temperature=@temp, pr=@pr, rr=@rr, ht=@ht, wt=@wt,
                        general_appearance=@ga, skin=@skin, head_and_face=@hf, eyes=@eyes,
                        neck=@neck, chest_lungs=@cl, heart=@heart, abdomen=@abd, extremities=@ext,
                        neurologic=@neuro, past_medical_history=@pmh, allergies=@all, family_history=@fam,
                        personal_social_history=@psh
                    WHERE consultation_id=@consultationId;";

                        using (MySqlCommand cmd = new MySqlCommand(updateHistory, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@bp", bpTextBox.Text);
                            cmd.Parameters.AddWithValue("@temp", temperatureTextBox.Text);
                            cmd.Parameters.AddWithValue("@pr", prTextBox.Text);
                            cmd.Parameters.AddWithValue("@rr", rrTextBox.Text);
                            cmd.Parameters.AddWithValue("@ht", htTextBox.Text);
                            cmd.Parameters.AddWithValue("@wt", wtTextBox.Text);
                            cmd.Parameters.AddWithValue("@ga", generalApperanceComboBox.Text);
                            cmd.Parameters.AddWithValue("@skin", skinComboBox.Text);
                            cmd.Parameters.AddWithValue("@hf", headAndFaceComboBox.Text);
                            cmd.Parameters.AddWithValue("@eyes", eyesComboBox.Text);
                            cmd.Parameters.AddWithValue("@neck", neckComboBox.Text);
                            cmd.Parameters.AddWithValue("@cl", chestLungsComboBox.Text);
                            cmd.Parameters.AddWithValue("@heart", heartComboBox.Text);
                            cmd.Parameters.AddWithValue("@abd", abdomenComboBox.Text);
                            cmd.Parameters.AddWithValue("@ext", extremetiesComboBox.Text);
                            cmd.Parameters.AddWithValue("@neuro", neurologicComboBox.Text);
                            cmd.Parameters.AddWithValue("@pmh", GetDgvValue(pastMedicalHistoryDGV));
                            cmd.Parameters.AddWithValue("@all", GetDgvValue(allergiesDGV));
                            cmd.Parameters.AddWithValue("@fam", GetDgvValue(familyHistoryDGV));
                            cmd.Parameters.AddWithValue("@psh", GetDgvValue(personalSocialHistoryDGV));
                            cmd.Parameters.AddWithValue("@consultationId", _consultationId);
                            cmd.ExecuteNonQuery();
                        }

                        // 2️⃣ Update consultation
                        string updateConsult = @"
                    UPDATE consultation
                    SET chief_complaint=@cc, ear_exam=@ear, nose_exam=@nose, throat_exam=@throat,
                        maxillofacial_exam=@max, head_and_neck_exam=@hn, others_exam=@others,
                        diagnosis=@diag, recommendations=@rec, notes=@notes,
                        follow_up_date=@fup
                    WHERE consultation_id=@consultationId;";

                        using (MySqlCommand cmd2 = new MySqlCommand(updateConsult, conn, transaction))
                        {
                            cmd2.Parameters.AddWithValue("@cc", complaintsRichTextBox.Text);
                            cmd2.Parameters.AddWithValue("@ear", GetDgvValue(earsDGV));
                            cmd2.Parameters.AddWithValue("@nose", GetDgvValue(noseDGV));
                            cmd2.Parameters.AddWithValue("@throat", GetDgvValue(throatDGV));
                            cmd2.Parameters.AddWithValue("@max", GetDgvValue(maxillofacialDGV));
                            cmd2.Parameters.AddWithValue("@hn", GetDgvValue(headNeckDGV));
                            cmd2.Parameters.AddWithValue("@others", GetDgvValue(othersDGV));
                            cmd2.Parameters.AddWithValue("@diag", GetDgvValue(diagnosisDGV));
                            cmd2.Parameters.AddWithValue("@rec", GetDgvValue(recommendationsDGV));
                            cmd2.Parameters.AddWithValue("@notes", noteRichTextBox.Text);
                            cmd2.Parameters.AddWithValue("@fup", followUpCheckBox.Checked ? (object)followUpDateTimePicker.Value : DBNull.Value);
                            cmd2.Parameters.AddWithValue("@consultationId", _consultationId);
                            cmd2.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Consultation updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Failed to update consultation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Combines all rows from a DataGridView into a comma-separated string
        /// </summary>
        private string GetDgvValue(DataGridView dgv)
        {
            List<string> values = new List<string>();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow && row.Cells[0].Value != null)
                {
                    string val = row.Cells[0].Value.ToString().Trim();
                    if (!string.IsNullOrEmpty(val))
                        values.Add(val);
                }
            }
            return string.Join(", ", values);
        }


        private void printConsultationHistoryButton_Click(object sender, EventArgs e)
        {
            try{
                // Create the print helper (this loads the patient + consultation data)
                PrintTextHistory printer = new PrintTextHistory(_patientId, _consultationId);

                // Call your custom ShowPreview() (the version with custom toolbar buttons)
                printer.ShowPreview();
            }
                    catch (Exception ex)
                    {
                MessageBox.Show("Error printing consultation history: " + ex.Message,
                    "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void medicalCertificateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (consultationDateDataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a consultation to print.",
                        "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (DataGridViewRow row in consultationDateDataGridView.SelectedRows)
                {
                    if (row.Cells["consultation_id"].Value == null ||
                        row.Cells["patient_id"].Value == null ||
                        row.Cells["consultation_date"].Value == null)
                        continue;

                    int consultationId = Convert.ToInt32(row.Cells["consultation_id"].Value);
                    int patientId = Convert.ToInt32(row.Cells["patient_id"].Value);

                    // 🔹 Show input dialog before printing
                    using (var inputForm = new PurposeInputForm())
                    {
                        if (inputForm.ShowDialog() == DialogResult.OK)
                        {
                            string requestName = inputForm.PurposeText;

                            // Pass user input to MedicalCertificatePrinter
                            var printer = new MedicalCertificatePrinter(patientId, consultationId, requestName);
                            printer.ShowPreview();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening consultation record: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
