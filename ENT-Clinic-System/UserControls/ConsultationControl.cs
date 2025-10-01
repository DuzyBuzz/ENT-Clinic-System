using ENT_Clinic_System.Consultation;
using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.Inventory;
using ENT_Clinic_System.PrintingForms;
using Syncfusion.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ENT_Clinic_System.UserControls
{
    public partial class ConsultationControl : UserControl
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
            followUpDateTimePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            videoHelper = new VideoFlowHelper(videoFlowLayoutPanel);
            imageHelper = new ImageFlowHelper(imageFlowLayoutPanel);

            RichTextBulletDropdownHelper.LoadColumnsData(
                "consultation",
                new List<string> { "chief_complaint", "history", "ear_exam", "nose_exam", "throat_exam", "diagnosis", "recommendations" }
            );

            RichTextBulletDropdownHelper.Enable(complaintsRichTextBox, "consultation", "chief_complaint");
            RichTextBulletDropdownHelper.Enable(illnessHistoryRichTextBox, "consultation", "history");
            RichTextBulletDropdownHelper.Enable(earsRichTextBox, "consultation", "ear_exam");
            RichTextBulletDropdownHelper.Enable(noseRichTextBox, "consultation", "nose_exam");
            RichTextBulletDropdownHelper.Enable(throatRichTextBox, "consultation", "throat_exam");
            RichTextBulletDropdownHelper.Enable(diagnosisRichTextBox, "consultation", "diagnosis");
            RichTextBulletDropdownHelper.Enable(recommendationRichTextBox, "consultation", "recommendations");

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
            emergencyNameLabel.Text = PatientDataHelper.GetPatientValue(patientId, "emergency_name");
            emergencyContactNumberLabel.Text = PatientDataHelper.GetPatientValue(patientId, "emergency_contact_number");
            emergencyRelationshipLabel.Text = PatientDataHelper.GetPatientValue(patientId, "emergency_relationship");

            // Load photo
            Image photo = PatientDataHelper.GetPatientPhoto(patientId);
            if (photo != null)
                patientProfilePictureBox.Image = photo;
        }



        private void openRecorderButton_Click(object sender, EventArgs e)
        {

        }


        private void saveConsultationButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Prepare the inputs, skip empty or default bullet RichTextBoxes
                ConsultationInputs inputs = new ConsultationInputs
                {
                    ComplaintsRichText = FilterEmptyRichText(complaintsRichTextBox),
                    IllnessHistoryRichText = FilterEmptyRichText(illnessHistoryRichTextBox),
                    EarsRichText = FilterEmptyRichText(earsRichTextBox),
                    NoseRichText = FilterEmptyRichText(noseRichTextBox),
                    ThroatRichText = FilterEmptyRichText(throatRichTextBox),
                    DiagnosisRichText = FilterEmptyRichText(diagnosisRichTextBox),
                    RecommendationRichText = FilterEmptyRichText(recommendationRichTextBox),
                    NoteRichText = FilterEmptyRichText(noteRichTextBox),
                    ImageFlowLayout = imageFlowLayoutPanel,
                    VideoFlowLayout = videoFlowLayoutPanel,
                };

                // Validation: prevent saving if nothing meaningful is entered
                if (inputs.ComplaintsRichText == null && inputs.IllnessHistoryRichText == null &&
                    inputs.EarsRichText == null && inputs.NoseRichText == null &&
                    inputs.ThroatRichText == null && inputs.DiagnosisRichText == null &&
                    inputs.RecommendationRichText == null && inputs.NoteRichText == null &&
                    inputs.ImageFlowLayout.Controls.Count == 0 && inputs.VideoFlowLayout.Controls.Count == 0)
                {
                    MessageBox.Show("Please enter at least one input before saving.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // stop here
                }

                DateTime? followUpDate = followUpDateTimePicker.Checked
                                         ? (DateTime?)followUpDateTimePicker.Value
                                         : null;

                // Call existing save logic (unchanged)
                var savedFiles = ConsultationSaver.SaveConsultation(
                    _patientId,
                    $"Dr. {UserCredentials.Fullname}",
                    DateTime.Now,
                    followUpDate,
                    inputs,
                    imageHelper,
                    videoHelper
                );

                MessageBox.Show("Consultation saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int latestConsultationId = LatestIdHelper.GetLatestId("consultation", "consultation_id");

                PrescriptionForm prescriptionForm = new PrescriptionForm(_patientId, latestConsultationId);
                prescriptionForm.ShowDialog();
                BillingForm billingForm = new BillingForm(latestConsultationId, _patientId);
                billingForm.ShowDialog();

                // Reset UI (same as before)
                imageFlowLayoutPanel.Controls.Clear();
                videoFlowLayoutPanel.Controls.Clear();
                imageHelper = new ImageFlowHelper(imageFlowLayoutPanel);
                videoHelper = new VideoFlowHelper(videoFlowLayoutPanel);

                // Refresh parent form safely
                if (this.ParentForm != null)
                {
                    Form parentForm = this.ParentForm;
                    parentForm.Hide();
                    Form newForm = (Form)Activator.CreateInstance(parentForm.GetType());
                    newForm.Show();
                    parentForm.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save consultation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                    // Create the helper
                    PrintTextHistory printer = new PrintTextHistory(patientId, consultationId);
                    string fullName = PatientDataHelper.GetPatientValue(patientId, "full_name");
                    // Use custom MultiPrintPreviewDialog (non-modal, taskbar visible)
                    MultiPrintPreviewDialog previewDialog = new MultiPrintPreviewDialog
                    {
                        Document = printer.Document,
                        StartPosition = FormStartPosition.CenterScreen,
                        ShowInTaskbar = true,
                        Text = $"{fullName} - {consultationDate}",
                        ShowIcon = false,
                       


                    };

                    previewDialog.Show(); // Non-modal: multiple dialogs can be opened
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
    }
}
