using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public class UpdateHelper
    {
        /// <summary>
        /// Checks for updates and launches updater if a newer version exists.
        /// </summary>
        public async Task CheckForUpdatesAsync()
        {
            try
            {
                // Main app folder
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;

                // Updater.exe must be in the same folder
                string updaterPath = Path.Combine(appFolder, "Updater.exe");

                // GitHub URLs
                string updateZipUrl = "https://github.com/DuzyBuzz/ENT-Clinic-System/releases/latest/download/ENT-Clinic-System.zip";

                string versionUrl = "https://raw.githubusercontent.com/DuzyBuzz/ENT-Clinic-System/main/version.txt";

                // Check if updater exists
                if (!File.Exists(updaterPath))
                {
                    MessageBox.Show("Updater.exe not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Current app version
                Version currentVersion = new Version(Application.ProductVersion);

                // Download latest version string
                string latestVersionStr;
                using (WebClient client = new WebClient())
                {
                    latestVersionStr = (await client.DownloadStringTaskAsync(versionUrl)).Trim();
                }

                // Parse latest version
                if (!Version.TryParse(latestVersionStr, out Version latestVersion))
                {
                    MessageBox.Show("Invalid version format on server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Compare versions
                if (currentVersion >= latestVersion)
                {
                    MessageBox.Show("You already have the latest version.", "No Update Needed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Launch updater
                Process.Start(updaterPath, $"\"{appFolder}\" \"{updateZipUrl}\"");

                // Close current app
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update check failed:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
