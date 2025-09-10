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

        public ConsultationControl(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            LoadPatientLabels(_patientId);

            InitializeVideoContextMenu();
        }

        private void InitializeVideoContextMenu()
        {
            videoContextMenu = new ContextMenuStrip();
            videoContextMenu.Items.Add("Delete").Click += (s, e) =>
            {
                if (videoContextMenu.Tag is VideoClipControl clip)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this video?", "Delete Video", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        videoFlowLayoutPanel.Controls.Remove(clip);

                        // Optional: delete the file
                        try { if (File.Exists(clip.VideoPath)) File.Delete(clip.VideoPath); } catch { }
                    }
                }
            };

            // Attach MouseDown to any existing video clips already in the panel
            foreach (Control c in videoFlowLayoutPanel.Controls)
            {
                c.MouseDown += VideoControl_MouseDown;
            }

            // Attach MouseDown to any new controls added dynamically
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

                // Determine which container was clicked
                if (sender is Panel pnl && videoFlowLayoutPanel.Controls.Contains(pnl))
                    container = pnl;
                else if (sender is Control ctrl)
                    container = ctrl.Parent as Panel;

                if (container != null)
                {
                    videoContextMenu.Tag = container; // store container
                    videoContextMenu.Show(Cursor.Position);
                }
            }
        }


        private void InitializeCamera()
        {
            cameraToolStrip = new CameraToolStrip(imageToolStrip, imageFlowLayoutPanel);
            cameraToolStrip.ImageAdded += (s, bmp) =>
            {
                Debug.WriteLine("New image added to panel.");
            };
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
                // Use VideoFlowHelper to add video with note & category
                videoFlowLayoutPanel.Invoke((MethodInvoker)(() =>
                {
                    // This automatically adds thumbnail, note label, category, and context menu
                    var container = videoHelper.AddVideo(videoPath);

                    // Attach MouseDown for right-click (if needed)
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
            VideoFolderHelper.DeleteAllVideos();
        }
    }
}
