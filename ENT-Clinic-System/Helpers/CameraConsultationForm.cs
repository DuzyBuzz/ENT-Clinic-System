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

        private PictureBox previewPictureBox;
        private Panel flashOverlay;
        private Timer watchdogTimer;
        private DateTime lastFrameTime = DateTime.MinValue;

        public List<string> CapturedImages { get; private set; } = new List<string>();
        public List<string> CapturedVideos { get; private set; } = new List<string>();

        private readonly string logFilePath;
        private bool notifiedCameraLost = false; // prevent multiple messageboxes

        public CameraConsultationForm()
        {
            InitializeComponent();
            OpenOnSecondMonitor();
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CameraError.log");

            try
            {
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

                fireflyHelper = new FireflyHelper();
                fireflyHelper.FireflySingleClick += (s, ev) => InvokeSafe(() => captureImageButton.PerformClick());
                fireflyHelper.FireflyLongPress += (s, ev) => InvokeSafe(() => captureVideoButton.PerformClick());
            }
            catch (Exception ex)
            {
                LogError("Hardware controller init failed: " + ex);
                MessageBox.Show("Hardware controller initialization failed: " + ex.Message,
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fireflyHelper = null;
            }

            SafeLoadAvailableCameras();
            SetupWatchdog();

            this.FormClosing += CameraConsultationForm_FormClosing;
        }
        private void OpenOnSecondMonitor()
        {
            // ✅ Move to 2nd monitor if available
            Screen[] screens = Screen.AllScreens;
            Screen targetScreen = screens.Length > 2 ? screens[2] : screens[0];

            this.StartPosition = FormStartPosition.Manual;
            this.Location = targetScreen.WorkingArea.Location;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

        }
        // -------------------------------------------
        // Safe helpers
        // -------------------------------------------
        private void InvokeSafe(Action action)
        {
            try
            {
                if (!IsHandleCreated || IsDisposed) return;
                if (InvokeRequired) BeginInvoke(action);
                else action();
            }
            catch { }
        }

        private void SafeDispose(IDisposable obj)
        {
            try { obj?.Dispose(); } catch { }
        }

        private void LogError(string text)
        {
            try
            {
                string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {text}{Environment.NewLine}";
                lock (logFilePath) File.AppendAllText(logFilePath, line);
            }
            catch { }
        }

        // -------------------------------------------
        // Flash overlay
        // -------------------------------------------
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
            if (flashOverlay == null) return;
            InvokeSafe(() =>
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

        // -------------------------------------------
        // Camera handling
        // -------------------------------------------
        private void SafeLoadAvailableCameras()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                cameraComboBox.Items.Clear();

                foreach (FilterInfo dev in videoDevices)
                    cameraComboBox.Items.Add(dev.Name);

                if (videoDevices.Count == 0)
                {
                    captureImageButton.Enabled = false;
                    captureVideoButton.Enabled = false;
                    MessageBox.Show("No camera devices detected.", "Camera",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }

                cameraComboBox.SelectedIndex = 0;
                StartSelectedCamera();
            }
            catch (Exception ex)
            {
                LogError("Camera enumeration failed: " + ex);
                captureImageButton.Enabled = false;
                captureVideoButton.Enabled = false;
                MessageBox.Show("Error enumerating cameras: " + ex.Message,
                    "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (videoDevices == null || cameraComboBox.SelectedIndex < 0)
                    return;

                string moniker = videoDevices[cameraComboBox.SelectedIndex].MonikerString;
                currentCamera = new VideoCaptureDevice(moniker);

                if (currentCamera.VideoCapabilities.Length > 0)
                {
                    var best = currentCamera.VideoCapabilities
                        .OrderByDescending(r => r.FrameSize.Width * r.FrameSize.Height)
                        .First();
                    currentCamera.VideoResolution = best;
                }

                currentCamera.NewFrame += CurrentCamera_NewFrame;
                currentCamera.Start();

                captureImageButton.Enabled = true;
                captureVideoButton.Enabled = true;
            }
            catch (Exception ex)
            {
                LogError("StartSelectedCamera: " + ex);
                MessageBox.Show("Failed to start camera: " + ex.Message, "Camera Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        try
                        {
                            currentCamera.SignalToStop();
                            DateTime until = DateTime.Now.AddSeconds(3);
                            while (currentCamera.IsRunning && DateTime.Now < until)
                            {
                                Application.DoEvents();
                                Thread.Sleep(10);
                            }
                        }
                        catch (Exception ex) { LogError("StopCameraInternal: " + ex); }

                        try { if (currentCamera.IsRunning) currentCamera.Stop(); } catch { }
                    }

                    // 🔹 Do NOT call Dispose() — VideoCaptureDevice is not IDisposable
                    currentCamera = null;
                }

                lock (frameLock)
                {
                    SafeDispose(currentFrame);
                    currentFrame = null;
                }
            }
            catch (Exception ex)
            {
                LogError("StopCameraInternal outer: " + ex);
            }
        }

        // -------------------------------------------
        // Watchdog for camera disconnection
        // -------------------------------------------
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
                        InvokeSafe(() =>
                        {
                            captureImageButton.Enabled = false;
                            captureVideoButton.Enabled = false;
                        });
                        SafeStopOnCameraLost();
                    }
                }
                catch (Exception ex) { LogError("Watchdog error: " + ex); }
            };
            watchdogTimer.Start();
        }

        private void SafeStopOnCameraLost()
        {
            try
            {
                StopRecording();
                StopCameraInternal();
            }
            catch (Exception ex)
            {
                LogError("SafeStopOnCameraLost: " + ex);
            }

            if (notifiedCameraLost) return;
            notifiedCameraLost = true;

            InvokeSafe(() =>
            {
                try { watchdogTimer?.Stop(); } catch { }
                MessageBox.Show(
                    "Camera feed lost. This window will close. Please reconnect the camera and try again.",
                    "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close(); // ✅ Close only this form
            });
        }

        // -------------------------------------------
        // Frame processing
        // -------------------------------------------
        private void CurrentCamera_NewFrame(object sender, NewFrameEventArgs e)
        {
            if (currentCamera == null || !currentCamera.IsRunning) return;

            try
            {
                lastFrameTime = DateTime.Now;

                lock (frameLock)
                {
                    SafeDispose(currentFrame);
                    currentFrame = (Bitmap)e.Frame.Clone();
                }

                InvokeSafe(UpdatePreviewFromBuffer);

                bool recordNow;
                lock (recordingLock) { recordNow = isRecording && videoWriter != null; }
                if (!recordNow) return;

                Bitmap toWrite = null;
                try
                {
                    lock (frameLock) toWrite = (Bitmap)currentFrame.Clone();

                    if (toWrite.Width != recordingWidth || toWrite.Height != recordingHeight)
                    {
                        using (var resized = new Bitmap(toWrite, new Size(recordingWidth, recordingHeight)))
                            videoWriter.WriteVideoFrame(resized);
                    }
                    else
                    {
                        videoWriter.WriteVideoFrame(toWrite);
                    }
                }
                finally
                {
                    SafeDispose(toWrite);
                }
            }
            catch (AccessViolationException ex)
            {
                LogError("AccessViolationException: " + ex);
                SafeStopRecordingOnError("Memory access violation while recording. Recording stopped.");
            }
            catch (Exception ex)
            {
                LogError("Frame error: " + ex);
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

                    Bitmap bmp = (Bitmap)currentFrame.Clone();
                    SafeDispose(previewPictureBox.Image);
                    previewPictureBox.Image = bmp;
                }
            }
            catch (Exception ex)
            {
                LogError("UpdatePreviewFromBuffer: " + ex);
            }
        }

        // -------------------------------------------
        // Image capture
        // -------------------------------------------
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

                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image");
                Directory.CreateDirectory(folder);

                string path = Path.Combine(folder, $"image_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                frameCopy.Save(path);
                frameCopy.Dispose();

                CapturedImages.Add(path);
                imageHelper?.AddImage(path);
                ShowFlash(120);
            }
            catch (Exception ex)
            {
                LogError("captureImageButton_Click: " + ex);
                MessageBox.Show("Capture failed: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------------------------------------------
        // Video capture
        // -------------------------------------------
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
            try
            {
                lock (frameLock)
                {
                    if (currentFrame == null)
                    {
                        MessageBox.Show("Start the camera before recording.", "Recording",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Video");
                Directory.CreateDirectory(folder);

                currentVideoPath = Path.Combine(folder, $"video_{DateTime.Now:yyyyMMdd_HHmmss}.avi");
                SafeDispose(videoWriter);
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
                LogError("StartRecordingSafe: " + ex);
                SafeStopRecordingOnError("Failed to start recording: " + ex.Message);
            }
        }

        private void StopRecordingSafe()
        {
            try
            {
                if (videoWriter == null) return;

                isRecording = false;
                videoWriter.Close();
                videoWriter.Dispose();
                videoWriter = null;

                captureVideoButton.Text = "Start Recording";
                captureVideoButton.BackColor = SystemColors.Control;
                captureVideoButton.ForeColor = SystemColors.ControlText;

                bool ok = File.Exists(currentVideoPath) && new FileInfo(currentVideoPath).Length > 1000;

                if (ok)
                {
                    CapturedVideos.Add(currentVideoPath);
                    videoHelper?.AddVideo(currentVideoPath);
                }
                else
                {
                    if (File.Exists(currentVideoPath)) File.Delete(currentVideoPath);
                    MessageBox.Show("Recorded video was invalid and discarded.", "Recording",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                LogError("StopRecordingSafe: " + ex);
                MessageBox.Show("Error while stopping recording: " + ex.Message,
                    "Recording Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                currentVideoPath = null;
            }
        }

        private void SafeStopRecordingOnError(string message)
        {
            try { StopRecordingSafe(); }
            finally
            {
                InvokeSafe(() =>
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

        // -------------------------------------------
        // Cleanup
        // -------------------------------------------
        private void CameraConsultationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                watchdogTimer?.Stop();
                StopRecording();
                StopCameraInternal();

                lock (frameLock)
                {
                    SafeDispose(currentFrame);
                    currentFrame = null;
                }
                StopRecordingSafe();
                SafeDispose(fireflyHelper);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                LogError("FormClosing: " + ex);
            }
        }
    }
}
