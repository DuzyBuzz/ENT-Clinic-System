using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.Inventory;
using ENT_Clinic_System.PrintingForms;
using ENT_Clinic_System.PrintingFroms;
using System;
using System.Collections.Generic;
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
            // Initialize image helper (no tool strip)
            imageHelper = new ImageFlowHelper(imageFlowLayoutPanel);
        }

        private void ConsultationControl_Load(object sender, EventArgs e)
        {
            followUpDateTimePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            videoHelper = new VideoFlowHelper(videoFlowLayoutPanel);
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
                ConsultationInputs inputs = new ConsultationInputs
                {
                    ComplaintsRichText = complaintsRichTextBox,
                    IllnessHistoryRichText = illnessHistoryRichTextBox,
                    EarsRichText = earsRichTextBox,
                    NoseRichText = noseRichTextBox,
                    ThroatRichText = throatRichTextBox,
                    DiagnosisRichText = diagnosisRichTextBox,
                    RecommendationRichText = recommendationRichTextBox,
                    NoteRichText = noteRichTextBox,
                    ImageFlowLayout = imageFlowLayoutPanel,
                    VideoFlowLayout = videoFlowLayoutPanel
                };

                DateTime? followUpDate = followUpDateTimePicker.Checked
                                         ? (DateTime?)followUpDateTimePicker.Value
                                         : null;
                var savedFiles = ConsultationSaver.SaveConsultation(
                    _patientId,
                    $"Dr. {UserCredentials.Fullname}",
                    DateTime.Now,
                    followUpDate,
                    inputs,
                    imageHelper,   
                    videoHelper
                );

                //string message = "Consultation saved successfully!\n\nSaved files:\n";
                //foreach (var (type, path) in savedFiles)
                //    message += $"{type}: {path}\n";
                string message = "Consultation saved successfully!";
                MessageBox.Show(message, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset UI
                imageFlowLayoutPanel.Controls.Clear();
                videoFlowLayoutPanel.Controls.Clear();
                imageHelper = new ImageFlowHelper(imageFlowLayoutPanel);
                videoHelper = new VideoFlowHelper(videoFlowLayoutPanel);
                if(this.Parent != null)
                {
                    this.Parent.Controls.Remove(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save consultation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    PrintAttachments printForm = new PrintAttachments(consultationId, patientId, fullNameLabel.Text, consultationDate);
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
            PrescriptionForm prescriptionForm = new PrescriptionForm(_patientId);
            prescriptionForm.ShowDialog();
        }

        private void fullNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void openCameraButton_Click(object sender, EventArgs e)
        {

        }
    }
}
