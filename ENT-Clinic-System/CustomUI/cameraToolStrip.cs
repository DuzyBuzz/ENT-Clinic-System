using ENT_Clinic_System.Helpers;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ENT_Clinic_System.CustomUI
{
    public class CameraToolStrip
    {
        private readonly ToolStrip toolStrip;
        private readonly FlowLayoutHelper flowHelper; // ✅ Use helper, not direct panel

        /// <summary>
        /// Raised whenever a new image is successfully added (from camera or upload).
        /// </summary>
        public event EventHandler<Bitmap> ImageAdded;

        public CameraToolStrip(ToolStrip toolStrip, FlowLayoutPanel capturedPanel)
        {
            this.toolStrip = toolStrip ?? throw new ArgumentNullException(nameof(toolStrip));
            if (capturedPanel == null) throw new ArgumentNullException(nameof(capturedPanel));

            flowHelper = new FlowLayoutHelper(capturedPanel);
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
            using (CameraUI camWindow = new CameraUI())
            {
                camWindow.StartPosition = FormStartPosition.CenterParent;

                camWindow.ImageCaptured += (sender, bitmap) =>
                {
                    try
                    {
                        // Save the image to disk first
                        string savedFilePath = Path.Combine(Path.Combine(Application.StartupPath, "Images"), $"Capture_{DateTime.Now:yyyyMMdd_HHmmssfff}.png");
                        if (!Directory.Exists(Path.GetDirectoryName(savedFilePath)))
                            Directory.CreateDirectory(Path.GetDirectoryName(savedFilePath));

                        bitmap.Save(savedFilePath, System.Drawing.Imaging.ImageFormat.Png);

                        // Add via helper, pass file path for deletion later
                        flowHelper.AddImage(bitmap, "", "", savedFilePath);

                        // Notify listeners
                        ImageAdded?.Invoke(this, bitmap);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to add captured image: " + ex.Message);
                    }
                    finally
                    {
                        bitmap.Dispose(); // safe because AddImage clones internally
                    }
                };

                // Show camera window as modal
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
                    using (Bitmap img = new Bitmap(ofd.FileName))
                    {
                        // ✅ Add via helper
                        flowHelper.AddImage(img);

                        // Notify listeners
                        ImageAdded?.Invoke(this, (Bitmap)img.Clone());
                    }
                }
            }
        }
    }
}
