using ENT_Clinic_System.Helpers;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ENT_Clinic_System.CustomUI
{
    public class CameraToolStrip
    {
        private ToolStrip toolStrip;
        private FlowLayoutPanel capturedPanel;

        public CameraToolStrip(ToolStrip toolStrip, FlowLayoutPanel capturedPanel)
        {
            this.toolStrip = toolStrip ?? throw new ArgumentNullException(nameof(toolStrip));
            this.capturedPanel = capturedPanel ?? throw new ArgumentNullException(nameof(capturedPanel));
            InitializeControls();
        }

        private void InitializeControls()
        {
            toolStrip.Items.Clear();

            // ===== Open Camera Button =====
            var openCameraButton = new ToolStripButton("Open Camera");
            openCameraButton.Click += (s, e) =>
            {
                try
                {
                    OpenCamera();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening camera: " + ex.Message);
                }
            };

            // ===== Upload Image Button =====
            var uploadImageButton = new ToolStripButton("Upload Image");
            uploadImageButton.Click += (s, e) =>
            {
                try
                {
                    UploadImage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error uploading image: " + ex.Message);
                }
            };

            toolStrip.Items.AddRange(new ToolStripItem[]
            {
                openCameraButton,
                uploadImageButton
            });
        }

        private void OpenCamera()
        {
            // Open CameraUI form
            using (CameraUI camWindow = new CameraUI())
            {
                camWindow.StartPosition = FormStartPosition.CenterParent;

                // Subscribe to captured image event
                camWindow.ImageCaptured += (sender, bitmap) =>
                {
                    // Add captured image to FlowLayoutPanel
                    PictureBox pb = new PictureBox
                    {
                        Image = bitmap,
                        Width = 150,
                        Height = 150,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Margin = new Padding(5)
                    };
                    capturedPanel.Controls.Add(pb);
                };

                camWindow.ShowDialog();
            }
        }

        private void UploadImage()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path = ofd.FileName;
                    Bitmap img = new Bitmap(path);

                    PictureBox pb = new PictureBox
                    {
                        Image = img,
                        Width = 150,
                        Height = 150,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Margin = new Padding(5)
                    };
                    capturedPanel.Controls.Add(pb);
                }
            }
        }
    }
}
