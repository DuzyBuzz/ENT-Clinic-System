using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class VideoFolderHelper
    {
        public static readonly string VideoFolderPath = Path.Combine(Application.StartupPath, "Videos");

        /// <summary>
        /// Deletes all videos in the folder and shows a progress bar automatically.
        /// </summary>
        public static void DeleteAllVideos()
        {
            if (!Directory.Exists(VideoFolderPath))
            {
                return; 
            }

            string[] files = Directory.GetFiles(VideoFolderPath);
            if (files.Length == 0)
            {
                return;
            }

            // Create progress form
            Form progressForm = new Form
            {
                Width = 400,
                Height = 100,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Deleting Videos",
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
        }
    }
}
