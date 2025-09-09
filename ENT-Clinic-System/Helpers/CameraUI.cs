using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace ENT_Clinic_System.CustomUI
{
    public partial class CameraUI : Form
    {
        private VideoCaptureDevice videoDevice;
        private FilterInfoCollection videoDevices;
        private Bitmap currentFrame;
        private bool isClosing = false;

        public event EventHandler<Bitmap> ImageCaptured;

        public CameraUI()
        {
            InitializeComponent();
            InitializeCamera();
        }

        private void InitializeCamera()
        {
            // Load cameras
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo device in videoDevices)
                    cameraComboBox.Items.Add(device.Name);

                if (cameraComboBox.Items.Count > 0)
                    cameraComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error detecting cameras: " + ex.Message);
            }

            captureButton.Click += CaptureButton_Click;
            StartCamera();
        }

        private void StartCamera()
        {
            if (videoDevice != null && videoDevice.IsRunning)
                videoDevice.Stop();

            if (cameraComboBox.SelectedIndex < 0)
                return;

            videoDevice = new VideoCaptureDevice(videoDevices[cameraComboBox.SelectedIndex].MonikerString);
            videoDevice.NewFrame += VideoDevice_NewFrame;
            videoDevice.Start();
        }

        private void VideoDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (isClosing) return; // skip if form is closing

            try
            {
                Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

                if (previewBox.InvokeRequired)
                {
                    previewBox.Invoke(new MethodInvoker(() =>
                    {
                        previewBox.Image?.Dispose();
                        previewBox.Image = (Bitmap)frame.Clone();
                    }));
                }
                else
                {
                    previewBox.Image?.Dispose();
                    previewBox.Image = (Bitmap)frame.Clone();
                }

                currentFrame?.Dispose();
                currentFrame = frame;
            }
            catch
            {
                // Ignore errors if form is closing
            }
        }


        private void CaptureButton_Click(object sender, EventArgs e)
        {
            if (currentFrame == null)
                return;

            Bitmap captured = (Bitmap)currentFrame.Clone();
            ImageCaptured?.Invoke(this, captured);

            // Add to captured images panel
            PictureBox pb = new PictureBox
            {
                Image = captured,
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 150,
                Height = 150,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

        }
        private void CameraUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true; // stop processing frames

            if (videoDevice != null)
            {
                if (videoDevice.IsRunning)
                {
                    videoDevice.SignalToStop();

                    // Wait for stop with timeout to avoid UI hang
                    DateTime waitUntil = DateTime.Now.AddSeconds(3);
                    while (videoDevice.IsRunning && DateTime.Now < waitUntil)
                    {
                        Application.DoEvents(); // keep UI responsive
                        System.Threading.Thread.Sleep(10);
                    }

                    if (videoDevice.IsRunning)
                    {
                        // Force stop
                        videoDevice.Stop();
                    }
                }

                videoDevice.NewFrame -= VideoDevice_NewFrame;
                videoDevice = null;
            }

            currentFrame?.Dispose();
            currentFrame = null;
        }

    }
}
