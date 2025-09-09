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
            // Use ShowDialog to block until closed
            using (CameraUI camWindow = new CameraUI())
            {
                camWindow.StartPosition = FormStartPosition.CenterParent;

                // Subscribe to captured image event
                camWindow.ImageCaptured += (sender, bitmap) =>
                {
                    try
                    {
                        // ✅ Add via helper so delete + notes are enabled
                        flowHelper.AddImage(bitmap);

                        // Notify listeners
                        ImageAdded?.Invoke(this, bitmap);
                    }
                    finally
                    {
                        bitmap.Dispose(); // we cloned inside AddImage, safe to dispose original
                    }
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
