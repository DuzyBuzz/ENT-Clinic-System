using Accord.Video.FFMPEG;
using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public partial class VideoRecorder : Form
    {
        public event EventHandler<string> VideoCaptured;

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private VideoFileWriter writer;
        private string tempVideoFolder = Path.Combine(Application.StartupPath, "Videos");
        private string currentVideoFile;
        private bool isRecording = false;
        private Stopwatch recordingTimer;
        private const int MaxRecordingSeconds = 300;
        private bool isClosing = false;

        // 🔹 Firefly device helper
        private FireflyHelper fireflyHelper;

        public VideoRecorder()
        {
            try
            {
                InitializeComponent();
                Directory.CreateDirectory(tempVideoFolder);
                LoadCameras();
                this.FormClosing += VideoRecorder_FormClosing;
                startRecordingButton.Text = "Start Recording";
                recordingTimer = new Stopwatch();

                // 🔹 Setup Firefly helper
                fireflyHelper = new FireflyHelper();
                fireflyHelper.FireflyButtonPressed += FireflyHelper_FireflyButtonPressed;

                // 🔹 Add camera selection change handler
                cameraComboBox.SelectedIndexChanged += CameraComboBox_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                ShowError("Initialization error", ex);
            }
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
            }
            catch (Exception ex)
            {
                ShowError("Error loading cameras", ex);
            }
        }

        private void startRecordingButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isRecording)
                    StartRecording();
                else
                    StopRecording();
            }
            catch (Exception ex)
            {
                ShowError("Error toggling recording", ex);
            }
        }

        private void FireflyHelper_FireflyButtonPressed(object sender, EventArgs e)
        {
            // 🔹 Simulate clicking the Start/Stop button
            startRecordingButton.PerformClick();
        }

        private void StartRecording()
        {
            try
            {
                if (videoSource == null || !videoSource.IsRunning)
                {
                    MessageBox.Show("Camera not running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                currentVideoFile = Path.Combine(tempVideoFolder, $"Video_{DateTime.Now:yyyyMMdd_HHmmss}.mp4");

                writer = new VideoFileWriter();
                writer.Open(currentVideoFile, 640, 480, 25, VideoCodec.MPEG4);

                isRecording = true;
                startRecordingButton.Text = "Stop Recording";
                recordingTimer.Restart();
            }
            catch (Exception ex)
            {
                ShowError("Error starting recording", ex);
                DisposeWriterSafely();
                isRecording = false;
            }
        }

        private void StopRecording()
        {
            try
            {
                if (!isRecording) return;

                isRecording = false;
                startRecordingButton.Text = "Start Recording";
                recordingTimer.Stop();

                DisposeWriterSafely();

                if (!string.IsNullOrEmpty(currentVideoFile) && File.Exists(currentVideoFile))
                {
                    try
                    {
                        VideoClipControl clipControl = new VideoClipControl(currentVideoFile);
                        recordCapturedFlowLayoutPanel.Controls.Add(clipControl);
                        VideoCaptured?.Invoke(this, currentVideoFile);
                    }
                    catch { /* Ignore UI errors */ }
                }

                currentVideoFile = null;
            }
            catch (Exception ex)
            {
                ShowError("Error stopping recording", ex);
            }
        }

        private void StopRecordingWithoutForwarding()
        {
            try
            {
                if (!isRecording) return;

                isRecording = false;
                startRecordingButton.Text = "Start Recording";
                recordingTimer.Stop();

                DisposeWriterSafely();

                if (!string.IsNullOrEmpty(currentVideoFile) && File.Exists(currentVideoFile))
                    File.Delete(currentVideoFile);

                currentVideoFile = null;
            }
            catch { /* Ignore */ }
        }

        private void DisposeWriterSafely()
        {
            try
            {
                if (writer != null)
                {
                    if (writer.IsOpen)
                        writer.Close();
                    writer.Dispose();
                }
            }
            catch { /* Ignore */ }
            finally
            {
                writer = null;
            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap frameCopy = (Bitmap)eventArgs.Frame.Clone();

                if (livePreviewPictureBox.InvokeRequired)
                {
                    livePreviewPictureBox.Invoke((MethodInvoker)(() =>
                    {
                        try
                        {
                            livePreviewPictureBox.Image?.Dispose();
                            livePreviewPictureBox.Image = frameCopy;
                        }
                        catch { frameCopy.Dispose(); }
                    }));
                }
                else
                {
                    livePreviewPictureBox.Image?.Dispose();
                    livePreviewPictureBox.Image = frameCopy;
                }

                if (isRecording && writer != null)
                {
                    writer.WriteVideoFrame(eventArgs.Frame);

                    if (recordingTimer.Elapsed.TotalSeconds >= MaxRecordingSeconds)
                    {
                        if (!isClosing)
                        {
                            this.Invoke((MethodInvoker)(() =>
                            {
                                MessageBox.Show("Maximum recording length of 5 minutes reached.", "Recording Limit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                StopRecording();
                            }));
                        }
                    }
                }
            }
            catch { /* Ignore frame errors */ }
        }

        private async void VideoRecorder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClosing) return;
            isClosing = true;

            try
            {
                if (isRecording)
                {
                    var result = MessageBox.Show(
                        "Recording in progress. Closing will discard the video. Continue?",
                        "Warning",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.No)
                    {
                        e.Cancel = true;
                        isClosing = false;
                        return;
                    }

                    StopRecordingWithoutForwarding();
                }

                if (videoSource != null && videoSource.IsRunning)
                {
                    e.Cancel = true;
                    Cursor.Current = Cursors.WaitCursor;

                    bool stopped = await StopCameraSafely();

                    Cursor.Current = Cursors.Default;

                    if (!stopped)
                    {
                        MessageBox.Show("Camera did not stop properly. Please wait and try again.", "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        isClosing = false;
                        return;
                    }

                    this.Close();
                    return;
                }

                if (livePreviewPictureBox.Image != null)
                {
                    livePreviewPictureBox.Image.Dispose();
                    livePreviewPictureBox.Image = null;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error while closing", ex);
            }
            finally
            {
                fireflyHelper?.Dispose();
            }
        }

        private async Task<bool> StopCameraSafely()
        {
            if (videoSource == null || !videoSource.IsRunning)
                return true;

            try
            {
                bool stopped = false;
                await Task.Run(() =>
                {
                    try
                    {
                        videoSource.SignalToStop();
                        videoSource.WaitForStop();
                        stopped = true;
                    }
                    catch { stopped = false; }
                });

                videoSource.NewFrame -= CameraPreview_NewFrame;
                videoSource = null;

                return stopped;
            }
            catch { return false; }
        }

        private void VideoRecorder_Load(object sender, EventArgs e)
        {
            try
            {
                StartCameraPreview();
            }
            catch (Exception ex)
            {
                ShowError("Error loading camera preview", ex);
            }
        }

        private void StartCameraPreview()
        {
            try
            {
                if (cameraComboBox.SelectedIndex < 0) return;

                videoSource = new VideoCaptureDevice(videoDevices[cameraComboBox.SelectedIndex].MonikerString);
                videoSource.NewFrame += CameraPreview_NewFrame;
                videoSource.Start();
            }
            catch (Exception ex)
            {
                ShowError("Error starting camera preview", ex);
            }
        }

        private void CameraPreview_NewFrame(object sender, NewFrameEventArgs e)
        {
            VideoSource_NewFrame(sender, e);
        }

        private void CameraComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Stop current preview if running
                if (videoSource != null && videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource.WaitForStop();
                    videoSource.NewFrame -= CameraPreview_NewFrame;
                    videoSource = null;
                }
                // Start preview for the newly selected camera
                StartCameraPreview();
            }
            catch (Exception ex)
            {
                ShowError("Error switching camera", ex);
            }
        }

        private void ShowError(string title, Exception ex)
        {
            try
            {
                MessageBox.Show($"{title}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
    }

    public class VideoClipControl : UserControl
    {
        public string VideoPath { get; private set; }

        public VideoClipControl(string videoPath)
        {
            VideoPath = videoPath;
            this.Width = 150;
            this.Height = 180;
            this.Margin = new Padding(5);
            this.BorderStyle = BorderStyle.FixedSingle;

            PictureBox pb = new PictureBox
            {
                Image = GetVideoThumbnail(videoPath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top,
                Height = 150,
                Width = 300,
                Cursor = Cursors.Hand
            };

            this.Controls.Add(pb);

            pb.DoubleClick += (s, e) =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = VideoPath,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    MessageBox.Show("Unable to open video.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
        }

        private Bitmap GetVideoThumbnail(string path)
        {
            try
            {
                using (var reader = new VideoFileReader())
                {
                    reader.Open(path);
                    Bitmap frame = reader.ReadVideoFrame();
                    reader.Close();
                    return new Bitmap(frame, 300, 300);
                }
            }
            catch
            {
                Bitmap bmp = new Bitmap(300, 300);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Black);
                    g.DrawString("Video", new Font("Segoe UI", 14), Brushes.White, new PointF(20, 50));
                }
                return bmp;
            }
        }
    }
}
