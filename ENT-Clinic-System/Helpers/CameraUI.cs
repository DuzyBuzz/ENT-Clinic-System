using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace ENT_Clinic_System.CustomUI
{
    public partial class CameraUI : Form
    {
        private VideoCaptureDevice videoDevice;
        private FilterInfoCollection videoDevices;

        // Shared current frame. Access must be synchronized via frameLock.
        private Bitmap currentFrame;
        private readonly object frameLock = new object();

        private bool isClosing = false;

        // Folder to save captured images
        private string tempVideoFolder = Path.Combine(Application.StartupPath, "Images");

        // Event to notify other parts of the app when an image is captured.
        public event EventHandler<Bitmap> ImageCaptured;

        public CameraUI()
        {
            InitializeComponent();

            // Prepare UI panels (if present on the form)
            if (capturedImagesPanel != null)
            {
                capturedImagesPanel.AutoScroll = true;
                capturedImagesPanel.WrapContents = true;
            }

            // Wire up events
            captureButton.Click += CaptureButton_Click;
            cameraComboBox.SelectedIndexChanged += CameraComboBox_SelectedIndexChanged;
            this.FormClosing += CameraUI_FormClosing;

            InitializeCamera();
        }

        private void InitializeCamera()
        {
            try
            {
                // Enumerate video devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                cameraComboBox.Items.Clear();

                foreach (FilterInfo device in videoDevices)
                    cameraComboBox.Items.Add(device.Name);

                if (cameraComboBox.Items.Count > 0)
                    cameraComboBox.SelectedIndex = 0; // auto-select first device

                // Start the first camera (if any)
                StartCamera();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error detecting cameras: " + ex.Message, "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CameraComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Restart camera when user picks a different device
            StartCamera();
        }

        private void StartCamera()
        {
            try
            {
                // Stop previous device safely
                if (videoDevice != null)
                {
                    try
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
                    }
                    catch
                    {
                        // ignore stop errors
                    }
                    videoDevice = null;
                }

                if (videoDevices == null || cameraComboBox.SelectedIndex < 0 || cameraComboBox.SelectedIndex >= videoDevices.Count)
                    return;

                videoDevice = new VideoCaptureDevice(videoDevices[cameraComboBox.SelectedIndex].MonikerString);
                videoDevice.NewFrame += VideoDevice_NewFrame;
                videoDevice.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening camera: " + ex.Message, "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Called on the camera thread whenever a new frame is available.
        /// We clone the event frame, create a preview clone for the PictureBox,
        /// then atomically replace currentFrame under lock to avoid races.
        /// </summary>
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
            catch
            {
                // Ignore exceptions here; they often occur when form is closing
            }
        }

        /// <summary>
        /// Capture current frame, save it, raise ImageCaptured, and add thumbnail to capturedImagesPanel.
        /// Uses locking to avoid races and clones bitmaps so each consumer has own copy.
        /// </summary>
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

            // Fire event for listeners (CameraToolStrip will save the image)
            ImageCaptured?.Invoke(this, (Bitmap)captured.Clone());

            // Add thumbnail preview in CameraUI
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

                // Click to open full image
                thumb.Click += (s, e2) =>
                {
                    if (thumb.Image != null)
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
                    }
                };

                capturedImagesPanel.Controls.Add(thumb);
            }

            captured.Dispose(); // safe to dispose original
        }





        private void CameraUI_FormClosing(object sender, FormClosingEventArgs e)
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
        }

        // Optional: keep empty handler if designer generated
        private void captureButton_Click_1(object sender, EventArgs e) { }
    }
}
