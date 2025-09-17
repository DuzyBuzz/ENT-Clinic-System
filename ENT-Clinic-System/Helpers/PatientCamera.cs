using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;

namespace ENT_Clinic_System.Helpers
{
    public partial class PatientCamera : Form
    {
        private FilterInfoCollection videoDevices;   // all webcams
        private VideoCaptureDevice videoSource;      // selected webcam

        public Image CapturedImage { get; private set; } // expose captured image to other forms

        public PatientCamera()
        {
            InitializeComponent();
        }

        private void PatientCamera_Load(object sender, EventArgs e)
        {
            try
            {
                // Get all available video devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                {
                    MessageBox.Show("No webcam detected.", "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Fill the ComboBox
                comboBoxCameras.Items.Clear();
                foreach (FilterInfo device in videoDevices)
                {
                    comboBoxCameras.Items.Add(device.Name);
                }

                // Select the first camera
                comboBoxCameras.SelectedIndex = 0;

                // Automatically start the first camera
                StartCamera(comboBoxCameras.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading cameras: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartCamera(int index)
        {
            try
            {
                StopCamera(); // stop any running camera first

                videoSource = new VideoCaptureDevice(videoDevices[index].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(Video_NewFrame);
                videoSource.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to start camera: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                // Show live preview
                Bitmap frame = (Bitmap)eventArgs.Frame.Clone();
                pictureBoxLive.Image = frame;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Frame error: " + ex.Message);
            }
        }

        private void comboBoxCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCameras.SelectedIndex >= 0)
            {
                StartCamera(comboBoxCameras.SelectedIndex);
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBoxLive.Image != null)
                {
                    // Clone the live image safely
                    using (Bitmap frame = new Bitmap(pictureBoxLive.Image))
                    {
                        // Resize to 2x2 inch (192x192 pixels at 96 DPI)
                        Bitmap captured = new Bitmap(frame, new Size(192, 192));

                        // Show in the captured preview box
                        pictureBoxCaptured.Image = captured;

                        // Save to property (for passing back to parent form)
                        CapturedImage = (Image)captured.Clone();
                    }
                }
                else
                {
                    MessageBox.Show("No live image available to capture.", "Capture Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Capture failed: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (CapturedImage != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please capture an image before confirming.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void StopCamera()
        {
            try
            {
                if (videoSource != null && videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource.WaitForStop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Camera stop error: " + ex.Message);
            }
        }

        private void PatientCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }
    }
}
