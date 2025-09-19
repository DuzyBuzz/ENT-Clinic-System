using ENT_Clinic_System.Helpers;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ENT_Clinic_System.UserControls
{
    public partial class CameraConsultationForm : Form
    {
        // Helpers
        private ImageFlowHelper imageHelper;
        private VideoFlowHelper videoHelper;

        // Events to notify parent form/control
        public event EventHandler<Bitmap> ImageCaptured;
        public event EventHandler<string> VideoCaptured;

        public CameraConsultationForm()
        {
            InitializeComponent();

            // Initialize helpers
            imageHelper = new ImageFlowHelper(imageVideoFlowPanel);
            videoHelper = new VideoFlowHelper(imageVideoFlowPanel);

            // Simulation button events
            captureImageButton.Click += CaptureImageButton_Click;
            captureVideoButton.Click += CaptureVideoButton_Click;

            // Populate camera combo box (simulate available cameras)
            LoadAvailableCameras();
        }

        private void LoadAvailableCameras()
        {
            // For simulation, just add placeholder cameras
            cameraComboBox.Items.Clear();
            cameraComboBox.Items.Add("Camera 1");
            cameraComboBox.Items.Add("Camera 2");
            cameraComboBox.SelectedIndex = 0;
        }

        private void CaptureImageButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Simulate a captured frame (placeholder image)
                Bitmap bmp = new Bitmap(cameraPreviewPanel.Width, cameraPreviewPanel.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Black);
                    g.DrawString("Captured Image", new Font("Segoe UI", 20), Brushes.White, new PointF(10, 10));
                }

                // Trigger event for parent form
                ImageCaptured?.Invoke(this, bmp);

                // Show preview in FlowLayoutPanel
                string tempPath = Path.Combine(Path.GetTempPath(), $"image_{Guid.NewGuid()}.png");
                bmp.Save(tempPath);
                var container = imageHelper.AddImage(tempPath);
                if (container != null)
                    foreach (Control c in container.Controls)
                        c.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Right) imageHelper.DeleteImage(container); };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to capture image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CaptureVideoButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Simulate video capture: just create a temp file path
                string tempVideoPath = Path.Combine(Path.GetTempPath(), $"video_{Guid.NewGuid()}.mp4");
                File.WriteAllText(tempVideoPath, "Simulated video content"); // placeholder file

                // Trigger event for parent form
                VideoCaptured?.Invoke(this, tempVideoPath);

                // Show preview in FlowLayoutPanel
                var container = videoHelper.AddVideo(tempVideoPath);
                if (container != null)
                    foreach (Control c in container.Controls)
                        c.MouseDown += (s, ev) => { if (ev.Button == MouseButtons.Right) videoHelper.DeleteVideo(container); };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to capture video: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
