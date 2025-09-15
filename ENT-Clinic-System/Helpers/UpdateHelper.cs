using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public class UpdateHelper
    {
        /// <summary>
        /// Checks for updates and launches updater if a newer version exists.
        /// Fail silently if update fails or no update is needed.
        /// </summary>
        public async Task CheckForUpdatesAsync()
        {
            try
            {
                string appFolder = AppDomain.CurrentDomain.BaseDirectory;
                string updaterPath = Path.Combine(appFolder, "Updater.exe");

                string versionUrl = "https://github.com/DuzyBuzz/ENT-Clinic-System/releases/latest/download/version.txt";

                // Check if updater exists
                if (!File.Exists(updaterPath))
                {
                    MessageBox.Show("Updater.exe not found! Skipping update check.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Check internet
                if (!IsInternetAvailable())
                {
                    MessageBox.Show("No internet connection detected. Skipping update check.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Version currentVersion = new Version(Application.ProductVersion);
                string latestVersionStr;

                using (WebClient client = new WebClient())
                {
                    try
                    {
                        latestVersionStr = (await client.DownloadStringTaskAsync(versionUrl)).Trim();
                    }
                    catch (WebException)
                    {
                        MessageBox.Show("Failed to reach update server. Skipping update check.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (!Version.TryParse(latestVersionStr, out Version latestVersion))
                {
                    MessageBox.Show("Invalid version format on server. Skipping update check.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Compare versions
                if (currentVersion >= latestVersion)
                {
                    MessageBox.Show($"Current version ({currentVersion}) is up to date. No update needed.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Launch updater (no arguments needed)
                MessageBox.Show($"Launching updater... Current version: {currentVersion}, Latest version: {latestVersion}", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    Process.Start(updaterPath);
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to launch updater: " + ex.Message, "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update check failed: " + ex.Message, "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Checks if internet is available by pinging a reliable host (Google DNS).
        /// </summary>
        private bool IsInternetAvailable()
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send("8.8.8.8", 2000);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}