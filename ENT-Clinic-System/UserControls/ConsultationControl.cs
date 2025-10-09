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


            // Example for all ComboBoxes in your form
            ComboBoxCollectionHelper.PopulateComboBox(familyComboBox, "health_record", "family_history");
            ComboBoxCollectionHelper.PopulateComboBox(personalComboBox, "health_record", "personal_history");
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




            SaveAndLoadConsultations();
            // 🔹 Load health record for this patient
            HealthRecordHelper.LoadHealthRecord(
                _patientId,
                pastMedicalHistoryDGV,
                familyComboBox,
                personalComboBox,
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

        private void SaveAndLoadConsultations()
        {
            AutoCompleteDgvHelper.LoadColumnAutocomplete(recentIlnessDGV, "recentIllness", "history");
            AutoCompleteDgvHelper.LoadColumnAutocomplete(pastMedicalHistoryDGV, "pastMedicalHistory", "past_medical_history");
            AutoCompleteDgvHelper.LoadColumnAutocomplete(chiefComplaintsDGV, "chiefComplaintsDGVColumn", "chief_complaint");
            AutoCompleteDgvHelper.LoadColumnAutocomplete(earsDGV, "ears", "ear_exam");
            AutoCompleteDgvHelper.LoadColumnAutocomplete(noseDGV, "nose", "nose_exam");
            AutoCompleteDgvHelper.LoadColumnAutocomplete(throatDGV, "throat", "throat_exam");
            AutoCompleteDgvHelper.LoadColumnAutocomplete(diagnosisDGV, "diagnosis", "diagnosis");
            AutoCompleteDgvHelper.LoadColumnAutocomplete(proceduresDGV, "procedures", "procedures");
            AutoCompleteDgvHelper.LoadColumnAutocomplete(recommendationsDGV, "recommendations", "recommendations");
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
                // 1️⃣ Helper to convert a DGV column into a comma-separated string
                string GetDgvValuesAsCsv(DataGridView dgv)
                {
                    if (dgv == null || dgv.Rows.Count == 0) return null;

                    List<string> values = new List<string>();

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null)
                            {
                                string val = cell.Value.ToString().Trim();
                                if (!string.IsNullOrEmpty(val))
                                    values.Add(val);
                            }
                        }
                    }

                    return values.Count > 0 ? string.Join(", ", values) : null;
                }

                // 2️⃣ Prepare ConsultationInputs with DGVs converted to CSV strings
                ConsultationInputs inputs = new ConsultationInputs
                {
                    ageLabel = ageLabel,
                    ComplaintsCsv = GetDgvValuesAsCsv(chiefComplaintsDGV),
                    RecentIllnessCsv = GetDgvValuesAsCsv(recentIlnessDGV),
                    PastMedicalHistoryCsv = GetDgvValuesAsCsv(pastMedicalHistoryDGV),
                    EarsCsv = GetDgvValuesAsCsv(earsDGV),
                    NoseCsv = GetDgvValuesAsCsv(noseDGV),
                    ThroatCsv = GetDgvValuesAsCsv(throatDGV),
                    DiagnosisCsv = GetDgvValuesAsCsv(diagnosisDGV),
                    ProceduresCsv = GetDgvValuesAsCsv(proceduresDGV),
                    RecommendationsCsv = GetDgvValuesAsCsv(recommendationsDGV),
                    NoteRichText = FilterEmptyRichText(noteRichTextBox), // keep note as RichTextBox
                    ImageFlowLayout = imageFlowLayoutPanel,
                    VideoFlowLayout = videoFlowLayoutPanel
                };

                // 3️⃣ Validation: ensure at least one meaningful input
                bool hasMeaningfulInput =
                    !string.IsNullOrEmpty(inputs.ComplaintsCsv) ||
                    !string.IsNullOrEmpty(inputs.RecentIllnessCsv) ||
                    !string.IsNullOrEmpty(inputs.PastMedicalHistoryCsv) ||
                    !string.IsNullOrEmpty(inputs.EarsCsv) ||
                    !string.IsNullOrEmpty(inputs.NoseCsv) ||
                    !string.IsNullOrEmpty(inputs.ThroatCsv) ||
                    !string.IsNullOrEmpty(inputs.NeckCsv) ||
                    !string.IsNullOrEmpty(inputs.DiagnosisCsv) ||
                    !string.IsNullOrEmpty(inputs.ProceduresCsv) ||
                    !string.IsNullOrEmpty(inputs.RecommendationsCsv) ||
                    inputs.NoteRichText != null ||
                    inputs.ImageFlowLayout.Controls.Count > 0 ||
                    inputs.VideoFlowLayout.Controls.Count > 0;

                if (!hasMeaningfulInput)
                {
                    MessageBox.Show("Please enter at least one input before saving.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 4️⃣ Follow-up date
                DateTime? followUpDate = followUpCheckBox.Checked
                                         ? (DateTime?)followUpDateTimePicker.Value
                                         : null;


                // 5️⃣ Save consultation
                var savedFiles = ConsultationSaver.SaveConsultation(
                    _patientId,
                    $"Dr. {UserCredentials.Fullname}",
                    DateTime.Now,
                    followUpDate,
                    inputs,
                    imageHelper,
                    videoHelper
                );
                if (followUpCheckBox.Checked && followUpDate.HasValue)
                {
                    string note = noteRichTextBox.Text;
                    SaveAppointments(_patientId, followUpDate.Value, note);
                }
                // Save or update health record
                HealthRecordHelper.SaveUpdateHealthRecord(
                    _patientId,                  // Patient ID
                    pastMedicalHistoryDGV,       // Past Medical History DataGridView
                    familyComboBox,              // Family History ComboBox
                    personalComboBox,            // Personal History ComboBox
                    bpTextBox,                   // BP
                    temperatureTextBox,          // Temperature
                    prTextBox,                   // Pulse Rate
                    rrTextBox,                   // Respiratory Rate
                    htTextBox,                   // Height
                    wtTextBox,                   // Weight
                    generalApperanceComboBox,    // General Appearance
                    skinComboBox,                // Skin
                    headAndFaceComboBox,         // Head and Face
                    eyesComboBox,                // Eyes
                    neckComboBox,                // Neck
                    chestLungsComboBox,          // Chest/Lungs
                    heartComboBox,               // Heart
                    abdomenComboBox,             // Abdomen
                    extremetiesComboBox,         // Extremities
                    neurologicComboBox           // Neurologic
                );

                MessageBox.Show("Consultation saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Save dynamic Physical Exam values

                int latestConsultationId = LatestIdHelper.GetLatestId("consultation", "consultation_id");

                // 6️⃣ Ask user before opening Prescription and Billing forms
                var result = MessageBox.Show(
                    "Do you want to open the Prescription and Billing forms?",
                    "Open Forms",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    PrescriptionForm prescriptionForm = new PrescriptionForm(_patientId, latestConsultationId);
                    prescriptionForm.ShowDialog();
                }

                BillingForm billingForm = new BillingForm(latestConsultationId, _patientId);
                billingForm.ShowDialog();

                // 7️⃣ Reset UI
                imageFlowLayoutPanel.Controls.Clear();
                videoFlowLayoutPanel.Controls.Clear();
                imageHelper = new ImageFlowHelper(imageFlowLayoutPanel);
                videoHelper = new VideoFlowHelper(videoFlowLayoutPanel);

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
    }
}
