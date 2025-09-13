using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace ENT_Clinic_System.Helpers
{
    public partial class ImageCameraUI : Form
    {
        private VideoCaptureDevice videoDevice;
        private FilterInfoCollection videoDevices;

        private Bitmap currentFrame;
        private readonly object frameLock = new object();
        private bool isClosing = false;

        private string tempImageFolder = Path.Combine(Application.StartupPath, "CapturedImages");

        public event EventHandler<Bitmap> ImageCaptured;

        // 🔹 Firefly device helper
        private FireflyHelper fireflyHelper;

        public ImageCameraUI()
        {
            InitializeComponent();

            // Create image folder
            Directory.CreateDirectory(tempImageFolder);

            // Setup FlowLayoutPanel for thumbnails
            if (capturedImagesPanel != null)
            {
                capturedImagesPanel.AutoScroll = true;
                capturedImagesPanel.WrapContents = true;
            }

            // Wire up events
            captureButton.Click += CaptureButton_Click;
            cameraComboBox.SelectedIndexChanged += CameraComboBox_SelectedIndexChanged;
            this.FormClosing += ImageCameraUI_FormClosing;

            // 🔹 Setup Firefly helper (device button)
            fireflyHelper = new FireflyHelper();
            fireflyHelper.FireflyButtonPressed += FireflyHelper_FireflyButtonPressed;

            LoadCameras();
        }

        private void FireflyHelper_FireflyButtonPressed(object sender, EventArgs e)
        {
            // 🔹 Trigger capture just like clicking the UI button
            CaptureButton_Click(captureButton, EventArgs.Empty);
        }

        private void LoadCameras()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                cameraComboBox.Items.Clear();

                foreach (FilterInfo device in videoDevices)
                    cameraComboBox.Items.Add(device.Name);

                if (cameraComboBox.Items.Count > 0)
                    cameraComboBox.SelectedIndex = 0;

                StartCamera();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error detecting cameras: " + ex.Message, "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CameraComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartCamera();
        }

        private void StartCamera()
        {
            try
            {
                // Stop previous camera
                if (videoDevice != null)
                {
                    if (videoDevice.IsRunning)
                    {
                        videoDevice.SignalToStop();
                        DateTime waitUntil = DateTime.Now.AddSeconds(2);
                        while (videoDevice.IsRunning && DateTime.Now < waitUntil)
                        {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(10);
                        }
                        if (videoDevice.IsRunning)
                            videoDevice.Stop();
                    }
                    videoDevice.NewFrame -= VideoDevice_NewFrame;
                    videoDevice = null;
                }

                if (videoDevices == null || cameraComboBox.SelectedIndex < 0) return;

                videoDevice = new VideoCaptureDevice(videoDevices[cameraComboBox.SelectedIndex].MonikerString);
                videoDevice.NewFrame += VideoDevice_NewFrame;
                videoDevice.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening camera: " + ex.Message, "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VideoDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (isClosing) return;

            try
            {
                Bitmap frame = (Bitmap)eventArgs.Frame.Clone();
                Bitmap previewImage = (Bitmap)frame.Clone();

                if (previewBox.InvokeRequired)
                {
                    previewBox.Invoke(new Action(() =>
                    {
                        previewBox.Image?.Dispose();
                        previewBox.Image = previewImage;
                    }));
                }
                else
                {
                    previewBox.Image?.Dispose();
                    previewBox.Image = previewImage;
                }

                lock (frameLock)
                {
                    currentFrame?.Dispose();
                    currentFrame = frame;
                }
            }
            catch { }
        }

        private void CaptureButton_Click(object sender, EventArgs e)
        {
            Bitmap captured = null;
            try
            {
                lock (frameLock)
                {
                    if (currentFrame == null)
                    {
                        MessageBox.Show("No frame available to capture.");
                        return;
                    }
                    captured = (Bitmap)currentFrame.Clone();
                }
            }
            catch { return; }

            if (captured == null) return;

            // Save to folder
            string fileName = Path.Combine(tempImageFolder, $"Image_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            captured.Save(fileName);

            // Fire event for parent
            ImageCaptured?.Invoke(this, (Bitmap)captured.Clone());

            // Add thumbnail
            if (capturedImagesPanel != null)
            {
                PictureBox thumb = new PictureBox
                {
                    Image = (Bitmap)captured.Clone(),
                    Width = 100,
                    Height = 80,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand
                };

                // Click to view full image
                thumb.Click += (s, e2) =>
                {
                    Form previewForm = new Form
                    {
                        Size = new Size(800, 600),
                        StartPosition = FormStartPosition.CenterParent
                    };
                    PictureBox fullPb = new PictureBox
                    {
                        Image = (Bitmap)thumb.Image.Clone(),
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.Zoom
                    };
                    previewForm.Controls.Add(fullPb);
                    previewForm.ShowDialog();
                };

                capturedImagesPanel.Controls.Add(thumb);
            }

            captured.Dispose();
        }

        private void ImageCameraUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClosing = true;

            try
            {
                if (videoDevice != null)
                {
                    if (videoDevice.IsRunning)
                    {
                        videoDevice.SignalToStop();
                        DateTime waitUntil = DateTime.Now.AddSeconds(3);
                        while (videoDevice.IsRunning && DateTime.Now < waitUntil)
                        {
                            Application.DoEvents();
                            System.Threading.Thread.Sleep(10);
                        }
                        if (videoDevice.IsRunning)
                            videoDevice.Stop();
                    }

                    videoDevice.NewFrame -= VideoDevice_NewFrame;
                    videoDevice = null;
                }
            }
            catch { }

            lock (frameLock)
            {
                currentFrame?.Dispose();
                currentFrame = null;
            }

            // 🔹 Dispose Firefly helper
            fireflyHelper?.Dispose();
        }
    }
}
