using ENT_Clinic_System.Consultation;
using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.Inventory;
using ENT_Clinic_System.PrintingForms;
using MySql.Data.MySqlClient;
using Syncfusion.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ENT_Clinic_System.UserControls
{
    public partial class ConsultationControl : Form
    {
        private int _patientId;

        // Tools
        private ContextMenuStrip videoContextMenu;

        // Flow helpers
        private VideoFlowHelper videoHelper;
        private ImageFlowHelper imageHelper;
        private DGVViewHelper viewHelper;
        public ConsultationControl(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;

            LoadPatientLabels(_patientId);
            InitializeVideoContextMenu();
            LoadConsultationDate(patientId);
        }

        private void ConsultationControl_Load(object sender, EventArgs e)
        {
            videoHelper = new VideoFlowHelper(videoFlowLayoutPanel);
            imageHelper = new ImageFlowHelper(imageFlowLayoutPanel);
            // Call this after initializing your form and your FlowLayoutPanel



            LoadComboBoxes();



            LoadAutoComplete();
            HealthRecordHelper.LoadHealthRecord(
                _patientId,
                pastMedicalHistoryDGV,
                allergiesDGV,
                familyHistoryDGV,
                personalSocialHistoryDGV,
                bpTextBox,
                temperatureTextBox,
                prTextBox,
                rrTextBox,
                htTextBox,
                wtTextBox,
                generalApperanceComboBox,
                skinComboBox,
                headAndFaceComboBox,
                eyesComboBox,
                neckComboBox,
                chestLungsComboBox,
                heartComboBox,
                abdomenComboBox,
                extremetiesComboBox,
                neurologicComboBox
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
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(pastMedicalHistoryDGV, "past_medical_history", "past_medical_history");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(allergiesDGV, "allergies", "allergies");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(familyHistoryDGV, "family_history", "family_history");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(personalSocialHistoryDGV, "personal_social_history", "personal_social_history");

            // Other Consultation DGVs
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(earsDGV, "ears", "ear_exam");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(noseDGV, "nose", "nose_exam");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(throatDGV, "throat", "throat_exam");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(diagnosisDGV, "diagnosis", "diagnosis");
            AutoCompleteDgvHelper.InitializeAutocompleteColumn(recommendationsDGV, "recommendations", "recommendations");
        }

        private void SaveAutoComplete()
        {
            AutoCompleteDgvHelper.SaveAllAutocompleteEntries(pastMedicalHistoryDGV, "past_medical_history", "past_medical_history");
            AutoCompleteDgvHelper.SaveAllAutocompleteEntries(allergiesDGV, "allergies", "allergies");
            AutoCompleteDgvHelper.SaveAllAutocompleteEntries(familyHistoryDGV, "family_history", "family_history");
            AutoCompleteDgvHelper.SaveAllAutocompleteEntries(personalSocialHistoryDGV, "personal_social_history", "personal_social_history");

            AutoCompleteDgvHelper.SaveAllAutocompleteEntries(earsDGV, "ears", "ear_exam");
            AutoCompleteDgvHelper.SaveAllAutocompleteEntries(noseDGV, "nose", "nose_exam");
            AutoCompleteDgvHelper.SaveAllAutocompleteEntries(throatDGV, "throat", "throat_exam");
            AutoCompleteDgvHelper.SaveAllAutocompleteEntries(diagnosisDGV, "diagnosis", "diagnosis");
            AutoCompleteDgvHelper.SaveAllAutocompleteEntries(recommendationsDGV, "recommendations", "recommendations");
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

            // Hide ID and patient ID columns
            if (consultationDateDataGridView.Columns.Contains("consultation_id"))
                consultationDateDataGridView.Columns["consultation_id"].Visible = false;

            if (consultationDateDataGridView.Columns.Contains("patient_id"))
                consultationDateDataGridView.Columns["patient_id"].Visible = false;
        }


        private void InitializeVideoContextMenu()
        {
            videoContextMenu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete") { ForeColor = System.Drawing.Color.Red };
            deleteItem.Click += (s, e) =>
            {
                if (videoContextMenu.Tag is Panel container)
                    videoHelper.DeleteVideo(container);
            };
            videoContextMenu.Items.Add(deleteItem);
        }

        private void VideoControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            Panel container = GetParentPanel(sender, videoFlowLayoutPanel);
            if (container == null) return;

            videoContextMenu.Tag = container;
            videoContextMenu.Show(Cursor.Position);
        }

        private void ImageControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            Panel container = GetParentPanel(sender, imageFlowLayoutPanel);
            if (container == null) return;

            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete") { ForeColor = System.Drawing.Color.Red };
            deleteItem.Click += (s, ev) => imageHelper.DeleteImage(container);
            menu.Items.Add(deleteItem);
            menu.Show(Cursor.Position);
        }

        private Panel GetParentPanel(object sender, FlowLayoutPanel parentPanel)
        {
            if (sender is Panel pnl && parentPanel.Controls.Contains(pnl))
                return pnl;
            else if (sender is Control ctrl && ctrl.Parent is Panel parent)
                return parent;
            return null;
        }

        private void LoadPatientLabels(int patientId)
        {
            fullNameLabel.Text = PatientDataHelper.GetPatientValue(patientId, "full_name");
            addressLabel.Text = PatientDataHelper.GetPatientValue(patientId, "address");
            ageLabel.Text = PatientDataHelper.GetPatientValue(patientId, "age");
            sexLabel.Text = PatientDataHelper.GetPatientValue(patientId, "sex");
            civilStatusLabel.Text = PatientDataHelper.GetPatientValue(patientId, "civil_status");
            patientContactNumberLabel.Text = PatientDataHelper.GetPatientValue(patientId, "patient_contact_number");


            // Load photo
            Image photo = PatientDataHelper.GetPatientPhoto(patientId);
            if (photo != null)
                patientProfilePictureBox.Image = photo;
        }



        private void openRecorderButton_Click(object sender, EventArgs e)
        {

        }
        // =============================
        // SAVE FOLLOW-UP APPOINTMENT
        // =============================
        private void SaveAppointments(int patientId, DateTime followUpDate, string note)
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"INSERT INTO appointments (patient_id, follow_up_date, note)
                             VALUES (@patient_id, @follow_up_date, @note)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@patient_id", patientId);
                        cmd.Parameters.AddWithValue("@follow_up_date", followUpDate);
                        if (string.IsNullOrWhiteSpace(note))
                            cmd.Parameters.Add("@note", MySqlDbType.VarChar).Value = DBNull.Value;
                        else
                            cmd.Parameters.Add("@note", MySqlDbType.VarChar).Value = note.Trim();


                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving appointment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveConsultationButton_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Helper: Convert a DataGridView to a comma-separated uppercase string
                string GetDgvValuesAsCsv(DataGridView dgv)
                {
                    if (dgv == null || dgv.Rows.Count == 0)
                        return null;

                    List<string> values = new List<string>();

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell?.Value == null) continue;

                            string val = cell.Value.ToString().Trim().ToUpper(); // ← uppercase
                            if (!string.IsNullOrEmpty(val))
                                values.Add(val);
                        }
                    }

                    return values.Count > 0 ? string.Join(", ", values) : null;
                }

                // 2️⃣ Helper: Extract trimmed rich text safely (uppercase)
                string GetRichTextValue(RichTextBox rtb)
                {
                    if (rtb == null) return string.Empty;
                    string text = rtb.Text.Trim().ToUpper(); // ← uppercase
                    return string.IsNullOrWhiteSpace(text) ? string.Empty : text;
                }

                // 3️⃣ Prepare ConsultationInputs
                ConsultationInputs inputs = new ConsultationInputs
                {
                    ageLabel = ageLabel,

                    // 🔹 RichTextBoxes (now uppercase)
                    ComplaintsText = GetRichTextValue(complaintsRichTextBox),
                    RecentIllnessText = GetRichTextValue(recentIllnessRichTextBox),

                    // 🔹 DataGridView values (converted to CSV uppercase)
                    PastMedicalHistoryCsv = GetDgvValuesAsCsv(pastMedicalHistoryDGV),
                    EarsCsv = GetDgvValuesAsCsv(earsDGV),
                    NoseCsv = GetDgvValuesAsCsv(noseDGV),
                    ThroatCsv = GetDgvValuesAsCsv(throatDGV),
                    DiagnosisCsv = GetDgvValuesAsCsv(diagnosisDGV),
                    RecommendationsCsv = GetDgvValuesAsCsv(recommendationsDGV),

                    // 🔹 Notes and Attachments
                    NoteRichText = noteRichTextBox,
                    ImageFlowLayout = imageFlowLayoutPanel,
                    VideoFlowLayout = videoFlowLayoutPanel
                };

                // 4️⃣ Validation – ensure at least one meaningful input
                bool hasMeaningfulInput =
                    !string.IsNullOrEmpty(inputs.ComplaintsText) ||
                    !string.IsNullOrEmpty(inputs.RecentIllnessText) ||
                    !string.IsNullOrEmpty(inputs.PastMedicalHistoryCsv) ||
                    !string.IsNullOrEmpty(inputs.EarsCsv) ||
                    !string.IsNullOrEmpty(inputs.NoseCsv) ||
                    !string.IsNullOrEmpty(inputs.ThroatCsv) ||
                    !string.IsNullOrEmpty(inputs.DiagnosisCsv) ||
                    !string.IsNullOrEmpty(inputs.ProceduresCsv) ||
                    !string.IsNullOrEmpty(inputs.RecommendationsCsv) ||
                    !string.IsNullOrEmpty(GetRichTextValue(noteRichTextBox)) ||
                    inputs.ImageFlowLayout?.Controls.Count > 0 ||
                    inputs.VideoFlowLayout?.Controls.Count > 0;

                if (!hasMeaningfulInput)
                {
                    MessageBox.Show(
                        "Please enter at least one input before saving.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // 5️⃣ Follow-up date
                DateTime? followUpDate = followUpCheckBox.Checked
                                         ? (DateTime?)followUpDateTimePicker.Value
                                         : null;

                // 6️⃣ Save Consultation to DB + Files
                var savedFiles = ConsultationSaver.SaveConsultation(
                    _patientId,
                    UserCredentials.UserId.ToString(), // ← uppercase doctor name
                    DateTime.Now,
                    followUpDate,
                    inputs,
                    imageHelper,
                    videoHelper
                );

                // 7️⃣ Save appointment if applicable
                if (followUpCheckBox.Checked && followUpDate.HasValue)
                {
                    string note = noteRichTextBox.Text.ToUpper(); // ← uppercase note
                    SaveAppointments(_patientId, followUpDate.Value, note);
                }

                // 8️⃣ Save or update Health Record (auto-uppercase inside helper)
                HealthRecordHelper.SaveUpdateHealthRecord(
                    _patientId,
                    pastMedicalHistoryDGV,
                    allergiesDGV,
                    familyHistoryDGV,
                    personalSocialHistoryDGV,
                    bpTextBox,
                    temperatureTextBox,
                    prTextBox,
                    rrTextBox,
                    htTextBox,
                    wtTextBox,
                    generalApperanceComboBox,
                    skinComboBox,
                    headAndFaceComboBox,
                    eyesComboBox,
                    neckComboBox,
                    chestLungsComboBox,
                    heartComboBox,
                    abdomenComboBox,
                    extremetiesComboBox,
                    neurologicComboBox
                );

                // 9️⃣ Success
                MessageBox.Show("Consultation saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int latestConsultationId = LatestIdHelper.GetLatestId("consultation", "consultation_id");

                // 🔟 Ask to open Prescription and Billing
                var result = MessageBox.Show(
                    "Do you want to open the Prescription and Billing forms?",
                    "Open Forms",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    using (PrescriptionForm prescriptionForm = new PrescriptionForm(_patientId, latestConsultationId))
                        prescriptionForm.ShowDialog();
                }

                using (BillingForm billingForm = new BillingForm(latestConsultationId, _patientId))
                    billingForm.ShowDialog();

                // 1️⃣1️⃣ Reset UI
                imageFlowLayoutPanel.Controls.Clear();
                videoFlowLayoutPanel.Controls.Clear();
                imageHelper = new ImageFlowHelper(imageFlowLayoutPanel);
                videoHelper = new VideoFlowHelper(videoFlowLayoutPanel);

                // Save autocomplete values (user-defined function)
                SaveAutoComplete();

                // ✅ Close form after save
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save consultation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        /// <summary>
        /// Checks if a DataGridView has any non-empty rows in the first column
        /// </summary>
        private bool DgvHasData(DataGridView dgv)
        {
            if (dgv == null) return false;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                var value = row.Cells[0].Value;
                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Returns null if RichTextBox contains only the default bullet or is empty
        /// Otherwise returns the RichTextBox itself for saving
        /// </summary>
        private RichTextBox FilterEmptyRichText(RichTextBox rtb)
        {
            if (rtb == null) return null;

            string text = rtb.Text.Trim();
            if (string.IsNullOrEmpty(text) || text == "•") // only bullet or empty
                return null;

            // Check if the text contains only bullets and spaces
            string cleanText = text.Replace("•", "").Trim();
            if (string.IsNullOrEmpty(cleanText))
                return null;

            return rtb;
        }


        private void uploadImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    var container = imageHelper.AddImage(file);
                    if (container != null)
                        foreach (Control c in container.Controls)
                            c.MouseDown += ImageControl_MouseDown;
                }
            }
        }

        private void openVideoButton_Click(object sender, EventArgs e)
        {
        }

        private void complaintsRichTextBox_KeyUp(object sender, KeyEventArgs e)
        {



        }

        private void consultationDateDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printConsultationHistoryButton_Click(object sender, EventArgs e)
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
                    string consultationDate = Convert.ToString(row.Cells["consultation_date"].Value);

                    try
                    {
                        // Create the print helper (this loads the patient + consultation data)
                        PrintTextHistory printer = new PrintTextHistory(patientId, consultationId);

                        // Call your custom ShowPreview() (the version with custom toolbar buttons)
                        printer.ShowPreview();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error printing consultation history: " + ex.Message,
                            "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening consultation record: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void printAttachmentButton_Click(object sender, EventArgs e)
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
                    string consultationDate = Convert.ToString(row.Cells["consultation_date"].Value);

                    // Open the PrintConsultationHistory form and pass the IDs
                    PrintAttachments printForm = new PrintAttachments(consultationId);
                    printForm.Show();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening consultation record: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printMedicalCertificateToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void prescribeMedicineButton_Click(object sender, EventArgs e)
        {


        }

        private void fullNameLabel_Click(object sender, EventArgs e)
        {

        }
        // Open Camera button
        private void openCameraButton_Click(object sender, EventArgs e)
        {
            using (var cameraForm = new CameraConsultationForm())
            {
                var result = cameraForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Transfer captured media into ConsultationControl
                    foreach (var img in cameraForm.CapturedImages)
                        imageHelper.AddImage(img);

                    foreach (var vid in cameraForm.CapturedVideos)
                        videoHelper.AddVideo(vid);
                }
            }
        }

        private void labRequestButton_Click(object sender, EventArgs e)
        {


        }

        private void followUpCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void familyHistoryTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void personalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void wtTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void followUpTablePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void earsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void temperatureTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void bpTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void rrTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void htTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void prTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void generalApperanceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void headAndFaceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void eyesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void neckComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chestLungsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void heartComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void abdomenComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void extremetiesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void neurologicComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void showPrescriptionsToolStripMenuItem_Click(object sender, EventArgs e)
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
                    string consultationDate = Convert.ToString(row.Cells["consultation_date"].Value);

                    var printer = new PrescriptionPrintHelper(consultationId);
                    printer.ShowPreview();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening consultation record: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void laboratoryRequestToolStripMenuItem1_Click(object sender, EventArgs e)
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
                    if (row.Cells["consultation_id"].Value == null)
                        continue;

                    int consultationId = Convert.ToInt32(row.Cells["consultation_id"].Value);

                    // Directly show the lab request preview
                    LabRequestPrintHelper helper = new LabRequestPrintHelper(consultationId);
                    helper.ShowPreview();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening consultation record: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void laboratoryResultToolStripMenuItem1_Click(object sender, EventArgs e)
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
                    if (row.Cells["consultation_id"].Value == null || row.Cells["patient_id"].Value == null)
                        continue;

                    int consultationId = Convert.ToInt32(row.Cells["consultation_id"].Value);
                    int patientId = Convert.ToInt32(row.Cells["patient_id"].Value);

                    // Directly show the lab results form
                    LabResultsForm labResultsForm = new LabResultsForm(consultationId, patientId);
                    labResultsForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening consultation record: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox15_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel25_Paint(object sender, PaintEventArgs e)
        {

        }

        private void allergiesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox14_Enter(object sender, EventArgs e)
        {

        }

        private void patientProfilePictureBox_Click(object sender, EventArgs e)
        {

        }

        private void patientProfilePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // --- LEFT CLICK: Open image in system default viewer ---
                if (e.Button == MouseButtons.Left)
                {
                    if (patientProfilePictureBox.Image == null)
                    {
                        MessageBox.Show("No profile photo available to open.",
                                        "No Image", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    string tempPath = Path.Combine(Path.GetTempPath(), $"patient_photo_{Guid.NewGuid()}.jpg");

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (Bitmap bmp = new Bitmap(patientProfilePictureBox.Image))
                        {
                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            File.WriteAllBytes(tempPath, ms.ToArray());
                        }
                    }

                    Process.Start(new ProcessStartInfo(tempPath)
                    {
                        UseShellExecute = true
                    });
                }

                // --- RIGHT CLICK: Change photo ---
                else if (e.Button == MouseButtons.Right)
                {
                    ContextMenuStrip menu = new ContextMenuStrip();
                    ToolStripMenuItem changeItem = new ToolStripMenuItem("Change Photo");
                    menu.Items.Add(changeItem);

                    changeItem.Click += (s, ev) =>
                    {
                        using (SelectPhotoOptionForm optionForm = new SelectPhotoOptionForm())
                        {
                            if (optionForm.ShowDialog() == DialogResult.OK)
                            {
                                Image selectedImage = null;

                                if (optionForm.SelectedOption == SelectPhotoOptionForm.PhotoOption.Camera)
                                {
                                    using (PatientCamera cameraForm = new PatientCamera())
                                    {
                                        if (cameraForm.ShowDialog() == DialogResult.OK && cameraForm.CapturedImage != null)
                                            selectedImage = (Image)cameraForm.CapturedImage.Clone();
                                    }
                                }
                                else if (optionForm.SelectedOption == SelectPhotoOptionForm.PhotoOption.File)
                                {
                                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                                    {
                                        openFileDialog.Title = "Select a photo";
                                        openFileDialog.Filter =
                                            "All Supported Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;*.tiff;*.heic;*.heif;*.webp;*.avif|" +
                                            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                            "PNG (*.png)|*.png|" +
                                            "Bitmap (*.bmp)|*.bmp|" +
                                            "GIF (*.gif)|*.gif|" +
                                            "TIFF (*.tif;*.tiff)|*.tif;*.tiff|" +
                                            "WEBP (*.webp)|*.webp|" +
                                            "HEIC/HEIF (*.heic;*.heif)|*.heic;*.heif|" +
                                            "AVIF (*.avif)|*.avif|" +
                                            "All Files (*.*)|*.*";

                                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                                        {
                                            string filePath = openFileDialog.FileName;

                                            // ✅ Validate image before loading
                                            if (!IsValidImage(filePath))
                                            {
                                                MessageBox.Show("The selected file is not a valid image.",
                                                                "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                return;
                                            }

                                            // ✅ Safely load and clone image (breaks any file lock)
                                            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                                            using (Image tempImg = Image.FromStream(fs))
                                            {
                                                selectedImage = new Bitmap(tempImg);
                                            }

                                        }
                                    }
                                }

                                if (selectedImage != null)
                                {
                                    patientProfilePictureBox.Image = (Image)selectedImage.Clone();
                                    SavePatientPhotoToDatabase(_patientId, selectedImage);
                                    selectedImage.Dispose(); // free memory
                                }
                            }
                        }
                    };

                    menu.Show(Cursor.Position);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while handling the photo.\n\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Helper to verify that file is an actual image
        private bool IsValidImage(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (Image img = Image.FromStream(fs, false, true))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Saves or updates the patient photo in the database.
        /// </summary>

        private void SavePatientPhotoToDatabase(int patientId, Image image)
        {
            try
            {
                if (image == null) return;

                using (Bitmap safeCopy = new Bitmap(image))
                using (MemoryStream ms = new MemoryStream())
                {
                    safeCopy.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] imageBytes = ms.ToArray();

                    using (var conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        string query = "UPDATE patients SET photo = @photo WHERE patient_id = @patientId";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@photo", imageBytes);
                            cmd.Parameters.AddWithValue("@patientId", patientId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Profile photo updated successfully!",
                                    "Photo Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save photo to database.\n\n" + ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ConsultationControl_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

    }
}
