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

        // Shared current frame. Access must be synchronized via frameLock.
        private Bitmap currentFrame;
        private readonly object frameLock = new object();

        private bool isClosing = false;

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
                        // ignore stop errors, continue to start new device
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
                // Clone the frame provided by AForge
                Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

                // Create a separate clone for preview (so preview and currentFrame are different objects)
                Bitmap previewImage = (Bitmap)frame.Clone();

                // Update previewBox on UI thread
                if (previewBox.InvokeRequired)
                {
                    previewBox.Invoke(new Action(() =>
                    {
                        // Dispose previous preview image safely
                        previewBox.Image?.Dispose();
                        previewBox.Image = previewImage;
                    }));
                }
                else
                {
                    previewBox.Image?.Dispose();
                    previewBox.Image = previewImage;
                }

                // Atomically replace currentFrame so CaptureButton_Click can clone safely
                lock (frameLock)
                {
                    currentFrame?.Dispose();
                    currentFrame = frame; // take ownership of 'frame' (do not dispose it here)
                }
            }
            catch
            {
                // Ignore exceptions here; they often occur when form is closing
            }
        }

        /// <summary>
        /// Capture current frame, raise ImageCaptured, and add thumbnail to capturedFlowPanel.
        /// Uses locking to avoid races and clones bitmaps so each consumer has own copy.
        /// </summary>
        private void CaptureButton_Click(object sender, EventArgs e)
        {
            // Clone the current frame under lock
            Bitmap captured = null;
            try
            {
                lock (frameLock)
                {
                    if (currentFrame == null)
                    {
                        MessageBox.Show("No frame available to capture.", "Capture", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Clone so we detach from the shared currentFrame
                    captured = (Bitmap)currentFrame.Clone();
                }
            }
            catch (Exception ex)
            {
                // This is the place where "Parameter is not valid" often appears.
                MessageBox.Show("Error capturing image: " + ex.Message, "Capture Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                captured?.Dispose();
                return;
            }

            if (captured == null)
                return;

            // Fire event with a clone so subscribers get their own copy
            try
            {
                ImageCaptured?.Invoke(this, (Bitmap)captured.Clone());
            }
            catch
            {
                // swallow subscriber exceptions; keep captured for our UI
            }

            // Add the captured image into the capturedFlowPanel (thumbnail with delete)
            try
            {
                if (capturedImagesPanel != null)
                {
                    Panel container = new Panel
                    {
                        Width = 140,
                        Height = 80,
                        Margin = new Padding(5),
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    PictureBox pb = new PictureBox
                    {
                        // give the panel ownership of this captured bitmap
                        Image = captured,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Dock = DockStyle.Fill
                    };



                    container.Controls.Add(pb);
                    capturedImagesPanel.Controls.Add(container);
                }
                else
                {
                    // If the capturedFlowPanel control is missing for any reason,
                    // dispose the captured bitmap to avoid a leak.
                    captured.Dispose();
                }
            }
            catch (Exception ex)
            {
                // In case adding to panel fails for any reason, make sure to free resources
                captured.Dispose();
                MessageBox.Show("Failed to add captured image to panel: " + ex.Message, "UI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        {
                            // Force stop if necessary
                            videoDevice.Stop();
                        }
                    }

                    videoDevice.NewFrame -= VideoDevice_NewFrame;
                    videoDevice = null;
                }
            }
            catch
            {
                // ignore shutdown errors
            }

            // Dispose the shared frame safely
            lock (frameLock)
            {
                currentFrame?.Dispose();
                currentFrame = null;
            }
        }

        // (Optional) if you have an unused designer-generated handler, keep it empty to avoid confusion
        private void captureButton_Click_1(object sender, EventArgs e)
        {
            // Not used (capture handled by CaptureButton_Click event)
        }
    }
}
