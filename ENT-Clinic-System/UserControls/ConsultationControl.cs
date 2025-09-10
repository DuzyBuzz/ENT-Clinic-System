using ENT_Clinic_Receptionist.CustomUI;
using ENT_Clinic_Receptionist.Helpers;
using ENT_Clinic_System.CustomUI;
using ENT_Clinic_System.Helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ENT_Clinic_System.UserControls
{
    public partial class ConsultationControl : UserControl
    {
        private int _patientId;
        private CameraToolStrip cameraToolStrip;
        private ContextMenuStrip videoContextMenu;
        private VideoFlowHelper videoHelper;
        private FlowLayoutHelper imageHelper;

        public ConsultationControl(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;

            LoadPatientLabels(_patientId);
            InitializeVideoContextMenu();
            imageHelper = new FlowLayoutHelper(imageFlowLayoutPanel, imageFlowLayoutPanel);
        }
        private void InitializeCamera()
        {
            cameraToolStrip = new CameraToolStrip(imageToolStrip, imageFlowLayoutPanel);
            cameraToolStrip.ImageAdded += (s, bmp) =>
            {
                Debug.WriteLine("New image added to panel.");
            };
        }

        private void InitializeVideoContextMenu()
        {
            videoContextMenu = new ContextMenuStrip();
            videoContextMenu.Items.Add("Delete").Click += (s, e) =>
            {
                if (videoContextMenu.Tag is Panel container)
                {
                    var result = MessageBox.Show(
                        "Are you sure you want to delete this video?",
                        "Delete Video",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // Remove from UI
                        videoFlowLayoutPanel.Controls.Remove(container);

                        // Delete file if path exists
                        string videoPath = container.Tag as string;
                        if (!string.IsNullOrEmpty(videoPath))
                        {
                            try
                            {
                                if (File.Exists(videoPath))
                                    File.Delete(videoPath);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Error deleting video file: " + ex.Message);
                            }
                        }
                    }
                }
            };

            // Attach MouseDown to existing controls
            foreach (Control c in videoFlowLayoutPanel.Controls)
                c.MouseDown += VideoControl_MouseDown;

            // Attach automatically to new controls
            videoFlowLayoutPanel.ControlAdded += (s, e) =>
            {
                e.Control.MouseDown += VideoControl_MouseDown;
            };
        }

        private void VideoControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Panel container = null;

                if (sender is Panel pnl && videoFlowLayoutPanel.Controls.Contains(pnl))
                {
                    container = pnl;
                }
                else if (sender is Control ctrl && ctrl.Parent is Panel parentPanel)
                {
                    container = parentPanel;
                }

                if (container != null)
                {
                    videoContextMenu.Tag = container;
                    videoContextMenu.Show(Cursor.Position);
                }
            }
        }




        private void ConsultationControl_Load(object sender, EventArgs e)
        {
            ToolStripInitializer();
            InitializeCamera();
            videoHelper = new VideoFlowHelper(videoFlowLayoutPanel);
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

        private void ToolStripInitializer()
        {
            RichTextBoxBulletHelper.EnableAutoBullets(complaintsRichTextBox);
            RichTextBoxBulletHelper.EnableAutoBullets(illnessHistoryRichTextBox);
            RichTextBoxBulletHelper.EnableAutoBullets(diagnosisRichTextBox);
            RichTextBoxBulletHelper.EnableAutoBullets(recommendationRichTextBox);
            RichTextBoxBulletHelper.EnableAutoBullets(noteRichTextBox);
            RichTextBoxBulletHelper.EnableAutoBullets(noseRichTextBox);
            RichTextBoxBulletHelper.EnableAutoBullets(earsRichTextBox);
            RichTextBoxBulletHelper.EnableAutoBullets(throatRichTextBox);
        }

        private void openRecorderButton_Click(object sender, EventArgs e)
        {
            VideoRecorder recorder = new VideoRecorder();

            recorder.VideoCaptured += (s, videoPath) =>
            {
                videoFlowLayoutPanel.Invoke((MethodInvoker)(() =>
                {
                    var container = videoHelper.AddVideo(videoPath);

                    if (container != null)
                    {
                        foreach (Control c in container.Controls)
                        {
                            c.MouseDown += VideoControl_MouseDown;
                        }
                    }
                }));
            };

            recorder.ShowDialog();
        }

        private void saveConsultationButton_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ConsultationInputs inputs = new ConsultationInputs
            //    {
            //        ComplaintsRichText = complaintsRichTextBox,
            //        IllnessHistoryRichText = illnessHistoryRichTextBox,
            //        EarsRichText = earsRichTextBox,
            //        NoseRichText = noseRichTextBox,
            //        ThroatRichText = throatRichTextBox,
            //        DiagnosisRichText = diagnosisRichTextBox,
            //        RecommendationRichText = recommendationRichTextBox,
            //        NoteRichText = noteRichTextBox,
            //        ImageFlowLayout = imageFlowLayoutPanel,
            //        VideoFlowLayout = videoFlowLayoutPanel
            //    };

            //    ConsultationSaver.SaveConsultation(
            //        _patientId,
            //        UserCredentials.Fullname,
            //        DateTime.Now,
            //        followUpDateTimePicker.Checked ? followUpDateTimePicker.Value : (DateTime?)null,
            //        inputs,
            //        imageHelper,
            //        videoHelper
            //    );

            //    MessageBox.Show("Consultation and attachments saved successfully!",
            //                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    // optional cleanup (only if videos are temporary!)
            //    VideoFolderHelper.DeleteAllVideos();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error saving consultation: " + ex.Message,
            //                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void openCameraButton_Click(object sender, EventArgs e)
        {

        }
    }
}
