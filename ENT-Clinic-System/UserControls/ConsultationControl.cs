using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingFroms;
using System;
using System.Collections.Generic;
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
            videoHelper = new VideoFlowHelper(videoFlowLayoutPanel);
            RichTextBulletAutocompleteHelper.LoadColumnsData(
                "consultation",
                new List<string> { "chief_complaint", "history", "ear_exam", "nose_exam", "throat_exam", "diagnosis", "recommendations" }
            );

            RichTextBulletAutocompleteHelper.Enable(complaintsRichTextBox, "consultation", "chief_complaint");
            RichTextBulletAutocompleteHelper.Enable(illnessHistoryRichTextBox, "consultation", "history");
            RichTextBulletAutocompleteHelper.Enable(earsRichTextBox, "consultation", "ear_exam");
            RichTextBulletAutocompleteHelper.Enable(noseRichTextBox, "consultation", "nose_exam");
            RichTextBulletAutocompleteHelper.Enable(throatRichTextBox, "consultation", "throat_exam");
            RichTextBulletAutocompleteHelper.Enable(diagnosisRichTextBox, "consultation", "diagnosis");
            RichTextBulletAutocompleteHelper.Enable(recommendationRichTextBox, "consultation", "recommendations");

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
        }


        private void openRecorderButton_Click(object sender, EventArgs e)
        {

        }
        private void openCameraButton_Click(object sender, EventArgs e)
        {
            ImageCameraUI camUI = new ImageCameraUI();
            camUI.ImageCaptured += (s, bmp) =>
            {
                // Save Bitmap to temp file
                string tempPath = Path.Combine(Path.GetTempPath(), $"image_{Guid.NewGuid()}.png");
                bmp.Save(tempPath);

                var container = imageHelper.AddImage(tempPath);
                if (container != null)
                    foreach (Control c in container.Controls)
                        c.MouseDown += ImageControl_MouseDown;
            };
            camUI.ShowDialog();
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
                    "Dr. Smith",
                    DateTime.Now,
                    followUpDate,
                    inputs,
                    imageHelper,   
                    videoHelper
                );

                string message = "Consultation saved successfully!\n\nSaved files:\n";
                foreach (var (type, path) in savedFiles)
                    message += $"{type}: {path}\n";

                MessageBox.Show(message, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset UI
                imageFlowLayoutPanel.Controls.Clear();
                videoFlowLayoutPanel.Controls.Clear();
                imageHelper = new ImageFlowHelper(imageFlowLayoutPanel);
                videoHelper = new VideoFlowHelper(videoFlowLayoutPanel);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save consultation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void imageCaptureButton_Click(object sender, EventArgs e)
        {
            openCameraButton_Click(sender, e);
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
            VideoRecorder recorder = new VideoRecorder();
            recorder.VideoCaptured += (s, videoPath) =>
            {
                videoFlowLayoutPanel.Invoke((MethodInvoker)(() =>
                {
                    var container = videoHelper.AddVideo(videoPath);
                    if (container != null)
                        foreach (Control c in container.Controls)
                            c.MouseDown += VideoControl_MouseDown;
                }));
            };
            recorder.ShowDialog();
        }

        private void complaintsRichTextBox_KeyUp(object sender, KeyEventArgs e)
        {



        }

        private void consultationDateDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Prevent errors if user clicks the header row
                if (e.RowIndex < 0) return;

                // Get the selected row
                DataGridViewRow row = consultationDateDataGridView.Rows[e.RowIndex];

                // Extract consultation_id and patient_id
                int consultationId = Convert.ToInt32(row.Cells["consultation_id"].Value);
                int patientId = Convert.ToInt32(row.Cells["patient_id"].Value);

                // Open the PrintConsultationHistory form and pass the IDs
                PrintConsultationHistory printForm = new PrintConsultationHistory(consultationId, patientId);
                printForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening consultation record: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
