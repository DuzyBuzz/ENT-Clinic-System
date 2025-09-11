using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class ImageFolderHelper
    {
        // Path to the Images folder
        public static readonly string ImageFolderPath = Path.Combine(Application.StartupPath, "Images");

        /// <summary>
        /// Deletes all images in the folder and shows a progress bar automatically.
        /// </summary>
        public static void DeleteAllImages()
        {
            if (!Directory.Exists(ImageFolderPath))
            {
                MessageBox.Show("No images to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string[] files = Directory.GetFiles(ImageFolderPath);
            if (files.Length == 0)
            {
                MessageBox.Show("No images to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Create a progress form
            Form progressForm = new Form
            {
                Width = 400,
                Height = 100,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Deleting Images",
                MaximizeBox = false,
                MinimizeBox = false
            };

            ProgressBar progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Continuous,
                Dock = DockStyle.Fill,
                Minimum = 0,
                Maximum = 100
            };

            progressForm.Controls.Add(progressBar);

            progressForm.Shown += async (s, e) =>
            {
                await Task.Run(() =>
                {
                    int totalFiles = files.Length;
                    int deletedCount = 0;

                    foreach (string file in files)
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch { }

                        deletedCount++;
                        int percent = (int)((deletedCount / (double)totalFiles) * 100);

                        progressBar.Invoke((MethodInvoker)(() =>
                        {
                            progressBar.Value = percent;
                        }));
                    }

                    // Close progress form when done
                    progressForm.Invoke((MethodInvoker)(() =>
                    {
                        progressForm.Close();
                    }));
                });
            };

            progressForm.ShowDialog();
            MessageBox.Show("All images have been deleted.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
