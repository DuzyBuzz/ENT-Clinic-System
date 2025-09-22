using Accord.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using ENT_Clinic_System.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ENT_Clinic_System.UserControls
{
    public partial class CameraConsultationForm : Form
    {
        // -------------------------
        // Helpers
        // -------------------------
        private ImageFlowHelper imageHelper;
        private VideoFlowHelper videoHelper;

        private FireflyHelper fireflyHelper;

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice currentCamera;

        private VideoFileWriter videoWriter;
        private bool isRecording = false;
        private readonly object recordingLock = new object();
        private string currentVideoPath;
        private int recordingWidth;
        private int recordingHeight;

        private Bitmap currentFrame;
        private readonly object frameLock = new object();

        public List<string> CapturedImages { get; private set; } = new List<string>();
        public List<string> CapturedVideos { get; private set; } = new List<string>();

        private PictureBox previewPictureBox;
        private Panel flashOverlay;

        private Timer watchdogTimer;
        private DateTime lastFrameTime = DateTime.MinValue;

        public CameraConsultationForm()
        {
            InitializeComponent();

            imageHelper = new ImageFlowHelper(imageVideoFlowPanel);
            videoHelper = new VideoFlowHelper(imageVideoFlowPanel);

            previewPictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black
            };

            cameraPreviewPanel.Controls.Clear();
            cameraPreviewPanel.Controls.Add(previewPictureBox);

            SetupFlashOverlay();

            try
            {
                fireflyHelper = new FireflyHelper();
                fireflyHelper.FireflySingleClick += (s, ev) => { InvokeIfRequired(() => captureImageButton.PerformClick()); };
                fireflyHelper.FireflyDoubleClick += (s, ev) => { InvokeIfRequired(() => captureVideoButton.PerformClick()); };
            }
            catch
            {
                fireflyHelper = null;
            }

            SafeLoadAvailableCameras();
            SetupWatchdog();

            this.FormClosing += CameraConsultationForm_FormClosing;
        }

        private void InvokeIfRequired(Action action)
        {
            if (this.IsHandleCreated && this.InvokeRequired)
                this.BeginInvoke(action);
            else
                action();
        }

        private void SafeDispose(IDisposable obj)
        {
            try { obj?.Dispose(); } catch { }
        }

        private void SetupFlashOverlay()
        {
            flashOverlay = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Visible = false,
                Enabled = false
            };
            cameraPreviewPanel.Controls.Add(flashOverlay);
            flashOverlay.BringToFront();
        }

        private void ShowFlash(int ms = 100)
        {
            try
            {
                if (flashOverlay == null) return;
                InvokeIfRequired(() =>
                {
                    flashOverlay.Visible = true;
                    var t = new Timer { Interval = ms };
                    t.Tick += (s, e) =>
                    {
                        t.Stop();
                        t.Dispose();
                        flashOverlay.Visible = false;
                    };
                    t.Start();
                });
            }
            catch { }
        }

        private void SafeLoadAvailableCameras()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                cameraComboBox.Items.Clear();

                foreach (FilterInfo dev in videoDevices)
                    cameraComboBox.Items.Add(dev.Name);

                if (cameraComboBox.Items.Count > 0)
                {
                    cameraComboBox.SelectedIndex = 0;
                    StartSelectedCamera();
                }
                else
                {
                    captureImageButton.Enabled = false;
                    captureVideoButton.Enabled = false;
                    MessageBox.Show("No camera devices detected.", "Camera", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                captureImageButton.Enabled = false;
                captureVideoButton.Enabled = false;
                MessageBox.Show($"Error enumerating camera devices: {ex.Message}", "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cameraComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartSelectedCamera();
        }

        private void StartSelectedCamera()
        {
            try
            {
                StopCameraInternal();
                if (videoDevices == null || cameraComboBox.SelectedIndex < 0) return;

                string moniker = videoDevices[cameraComboBox.SelectedIndex].MonikerString;
                currentCamera = new VideoCaptureDevice(moniker);

                if (currentCamera.VideoCapabilities.Length > 0)
                {
                    var bestResolution = currentCamera.VideoCapabilities
                        .OrderByDescending(r => r.FrameSize.Width * r.FrameSize.Height)
                        .First();
                    currentCamera.VideoResolution = bestResolution;
                }

                currentCamera.NewFrame += CurrentCamera_NewFrame;
                currentCamera.Start();

                captureImageButton.Enabled = true;
                captureVideoButton.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start camera: {ex.Message}", "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopCameraInternal()
        {
            try
            {
                if (currentCamera != null)
                {
                    currentCamera.NewFrame -= CurrentCamera_NewFrame;
                    if (currentCamera.IsRunning)
                    {
                        currentCamera.SignalToStop();
                        DateTime waitUntil = DateTime.Now.AddSeconds(2);
                        while (currentCamera.IsRunning && DateTime.Now < waitUntil)
                        {
                            Application.DoEvents();
                            Thread.Sleep(10);
                        }
                        if (currentCamera.IsRunning)
                        {
                            try { currentCamera.Stop(); } catch { }
                        }
                    }
                    currentCamera = null;
                }
            }
            catch { }
        }

        private void SetupWatchdog()
        {
            watchdogTimer = new Timer { Interval = 1500 };
            watchdogTimer.Tick += (s, e) =>
            {
                try
                {
                    if (lastFrameTime == DateTime.MinValue) return;
                    if ((DateTime.Now - lastFrameTime).TotalSeconds > 3)
                    {
                        InvokeIfRequired(() =>
                        {
                            captureImageButton.Enabled = false;
                            captureVideoButton.Enabled = false;
                        });
                        SafeStopOnCameraLost();
                        InvokeIfRequired(() =>
                            MessageBox.Show("Camera feed lost. Please reconnect the camera.", "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        );
                        watchdogTimer.Stop();
                    }
                }
                catch { }
            };
            watchdogTimer.Start();
        }

        private void SafeStopOnCameraLost()
        {
            try
            {
                lock (recordingLock)
                {
                    if (isRecording)
                    {
                        isRecording = false;
                        try { videoWriter?.Close(); } catch { }
                        try { videoWriter?.Dispose(); } catch { }
                        videoWriter = null;
                        try { if (!string.IsNullOrEmpty(currentVideoPath) && File.Exists(currentVideoPath)) File.Delete(currentVideoPath); } catch { }
                        currentVideoPath = null;
                    }
                }
            }
            catch { }
        }

        private void CurrentCamera_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                lastFrameTime = DateTime.Now;

                // Update currentFrame safely
                lock (frameLock)
                {
                    try { currentFrame?.Dispose(); } catch { }
                    currentFrame = (Bitmap)eventArgs.Frame.Clone();
                }

                // Update preview safely
                if (previewPictureBox.InvokeRequired)
                    previewPictureBox.BeginInvoke(new Action(UpdatePreviewFromBuffer));
                else
                    UpdatePreviewFromBuffer();

                // Only write if recording is active
                bool recordingNow;
                lock (recordingLock) { recordingNow = isRecording && videoWriter != null; }
                if (!recordingNow) return;

                Bitmap toWrite = null;
                try
                {
                    // Clone current frame for writing
                    lock (frameLock) { toWrite = (Bitmap)currentFrame.Clone(); }

                    try
                    {
                        // Resize if needed to match recording dimensions
                        if (toWrite.Width != recordingWidth || toWrite.Height != recordingHeight)
                        {
                            using (var resized = new Bitmap(toWrite, new Size(recordingWidth, recordingHeight)))
                            {
                                try
                                {
                                    videoWriter.WriteVideoFrame(resized);
                                }
                                catch (AccessViolationException)
                                {
                                    SafeStopRecordingOnError("Recording failed due to memory access violation. File discarded.");
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                videoWriter.WriteVideoFrame(toWrite);
                            }
                            catch (AccessViolationException)
                            {
                                SafeStopRecordingOnError("Recording failed due to memory access violation. File discarded.");
                            }
                        }
                    }
                    finally
                    {
                        toWrite.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    SafeStopRecordingOnError($"Unexpected error while writing video frame: {ex.Message}");
                    toWrite?.Dispose();
                }
            }
            catch
            {
                // Swallow any other exceptions to prevent crash
            }
        }



        private void UpdatePreviewFromBuffer()
        {
            try
            {
                lock (frameLock)
                {
                    if (currentFrame == null)
                    {
                        SafeDispose(previewPictureBox.Image);
                        previewPictureBox.Image = null;
                        return;
                    }

                    var bmp = (Bitmap)currentFrame.Clone();
                    SafeDispose(previewPictureBox.Image);
                    previewPictureBox.Image = bmp;
                }
            }
            catch { }
        }

        private void captureImageButton_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap frameCopy;
                lock (frameLock)
                {
                    if (currentFrame == null) return;
                    frameCopy = (Bitmap)currentFrame.Clone();
                }

                // Get the folder where your .exe is running
                string exeFolder = AppDomain.CurrentDomain.BaseDirectory;

                // Ensure the "Image" folder exists
                string imageFolder = Path.Combine(exeFolder, "Image");
                if (!Directory.Exists(imageFolder))
                {
                    Directory.CreateDirectory(imageFolder);
                }

                // Build the full path for the image
                string tempPath = Path.Combine(imageFolder, $"image_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                using (frameCopy) { frameCopy.Save(tempPath); }

                CapturedImages.Add(tempPath);
                try { imageHelper.AddImage(tempPath); } catch { }

                ShowFlash(120);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture failed: {ex.Message}", "Capture Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void captureVideoButton_Click(object sender, EventArgs e)
        {
            captureVideoButton.Enabled = false;
            try
            {
                lock (recordingLock)
                {
                    if (!isRecording) StartRecordingSafe();
                    else StopRecordingSafe();
                }
            }
            finally
            {
                var t = new Timer { Interval = 400 };
                t.Tick += (s, ev) => { t.Stop(); t.Dispose(); captureVideoButton.Enabled = true; };
                t.Start();
            }
        }

        private void StartRecordingSafe()
        {
            lock (frameLock)
            {
                if (currentFrame == null)
                {
                    MessageBox.Show("Start the camera before recording.", "Recording", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                // Get the folder where your .exe is running
                string exeFolder = AppDomain.CurrentDomain.BaseDirectory;

                // Ensure the "Video" folder exists
                string videoFolder = Path.Combine(exeFolder, "Video");
                if (!Directory.Exists(videoFolder))
                {
                    Directory.CreateDirectory(videoFolder);
                }

                // Build the full path for the video
                currentVideoPath = Path.Combine(videoFolder, $"video_{DateTime.Now:yyyyMMdd_HHmmss}.avi");

                videoWriter = new VideoFileWriter();

                lock (frameLock)
                {
                    recordingWidth = currentFrame.Width;
                    recordingHeight = currentFrame.Height;
                }

                if (recordingWidth % 2 != 0) recordingWidth--;
                if (recordingHeight % 2 != 0) recordingHeight--;

                videoWriter.Open(currentVideoPath, recordingWidth, recordingHeight, 25, VideoCodec.MPEG4);

                isRecording = true;
                captureVideoButton.Text = "Stop Recording";
                captureVideoButton.BackColor = Color.Red;
                captureVideoButton.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                try { videoWriter?.Close(); } catch { }
                try { videoWriter?.Dispose(); } catch { }
                videoWriter = null;
                isRecording = false;

                captureVideoButton.Text = "Start Recording";
                captureVideoButton.BackColor = SystemColors.Control;
                captureVideoButton.ForeColor = SystemColors.ControlText;

                MessageBox.Show($"Failed to start recording: {ex.Message}", "Recording Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopRecordingSafe()
        {
            try
            {
                if (videoWriter == null)
                {
                    isRecording = false;
                    captureVideoButton.Text = "Start Recording";
                    captureVideoButton.BackColor = SystemColors.Control;
                    captureVideoButton.ForeColor = SystemColors.ControlText;
                    return;
                }

                isRecording = false;
                try { videoWriter?.Close(); } catch { }
                try { videoWriter?.Dispose(); } catch { }
                videoWriter = null;

                captureVideoButton.Text = "Start Recording";
                captureVideoButton.BackColor = SystemColors.Control;
                captureVideoButton.ForeColor = SystemColors.ControlText;

                bool ok = false;
                try
                {
                    if (!string.IsNullOrEmpty(currentVideoPath) && File.Exists(currentVideoPath))
                        ok = new FileInfo(currentVideoPath).Length > 1000;
                }
                catch { }

                if (ok)
                {
                    CapturedVideos.Add(currentVideoPath);
                    try { videoHelper.AddVideo(currentVideoPath); } catch { }
                }
                else
                {
                    try { if (File.Exists(currentVideoPath)) File.Delete(currentVideoPath); } catch { }
                    MessageBox.Show("Recorded video was invalid and has been discarded.", "Recording", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while stopping recording: {ex.Message}", "Recording Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { currentVideoPath = null; }
        }

        private void SafeStopRecordingOnError(string message)
        {
            try
            {
                lock (recordingLock)
                {
                    if (isRecording)
                    {
                        isRecording = false;
                        try { videoWriter?.Close(); } catch { }
                        try { videoWriter?.Dispose(); } catch { }
                        videoWriter = null;
                        try { if (!string.IsNullOrEmpty(currentVideoPath) && File.Exists(currentVideoPath)) File.Delete(currentVideoPath); } catch { }
                        currentVideoPath = null;
                    }
                }
            }
            finally
            {
                InvokeIfRequired(() =>
                {
                    captureVideoButton.Text = "Start Recording";
                    captureVideoButton.BackColor = SystemColors.Control;
                    captureVideoButton.ForeColor = SystemColors.ControlText;
                    MessageBox.Show(message, "Recording Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }
        }

        private void StopRecording()
        {
            lock (recordingLock)
            {
                if (!isRecording) return;
                StopRecordingSafe();
            }
        }

        private void CameraConsultationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                try { watchdogTimer?.Stop(); } catch { }

                lock (recordingLock)
                {
                    if (isRecording)
                    {
                        isRecording = false;
                        try { videoWriter?.Close(); } catch { }
                        try { videoWriter?.Dispose(); } catch { }
                        videoWriter = null;
                        if (!string.IsNullOrEmpty(currentVideoPath))
                        {
                            try { if (File.Exists(currentVideoPath)) File.Delete(currentVideoPath); } catch { }
                            currentVideoPath = null;
                        }
                    }
                }

                StopCameraInternal();

                lock (frameLock)
                {
                    SafeDispose(currentFrame);
                    currentFrame = null;
                }

                SafeDispose(fireflyHelper);

                this.DialogResult = DialogResult.OK;
            }
            catch { }
        }
    }
}
