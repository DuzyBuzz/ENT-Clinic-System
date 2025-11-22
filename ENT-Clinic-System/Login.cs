using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.UI;
using ENT_Clinic_System.UserControls;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Windows.Forms;

namespace ENT_Clinic_System
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();




            // Optional: check immediately on form load
            DBConnectionMonitor.TestConnection();
        }


        private void Login_Load(object sender, EventArgs e)
        {
            // Get the version of the assembly
            Version appVersion = Assembly.GetExecutingAssembly().GetName().Version;
            this.KeyPreview = true; // ensures form receives key events
            this.KeyDown += Login_KeyDown_Secret;
            // Display in the label
            versionLabel.Text = $"Version: {appVersion.Major}.{appVersion.Minor}.{appVersion.Build}.{appVersion.Revision}";
        }
        private void Login_KeyDown_Secret(object sender, KeyEventArgs e)
        {
            // Secret combo: Ctrl + Shift + Enter
            if (e.Control && e.Shift && e.KeyCode == Keys.Enter)
            {
                // Optional: log attempt, show a toast, etc.
                Debug.WriteLine("Secret key combo used to open admin login");
                OpenAdminLogin(); // show admin login (still validates credentials)
                e.Handled = true;
            }
        }
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            CleanupHelper.DeleteImageAndVideoFolders();
            BackupSql();
        }

        private void BackupSql()
        {
            try
            {
                // Provide the path to mysqldump.exe
                string mysqldumpPath = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqldump.exe";

                // Create the helper instance with WinForms-safe logging
                SQLBackupHelper backupHelper = new SQLBackupHelper(
                    mysqldumpPath,
                    infoLogger: msg => Debug.WriteLine("[BACKUP] " + msg),
                    warningLogger: msg => Debug.WriteLine("[BACKUP-WARN] " + msg),
                    errorLogger: msg => Debug.WriteLine("[BACKUP-ERROR] " + msg)
                );

                // Run the backup
                int result = backupHelper.RunBackup();

                // Check the result and provide appropriate feedback
                if (result == 0)
                {
                    Debug.WriteLine("Backup completed successfully.");
                }
                else
                {
                    Debug.WriteLine("Backup failed with code: " + result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Backup operation failed: " + ex.Message);
            }
        }

        private async void versionLabel_Click(object sender, EventArgs e)
        {
            UpdateHelper helper = new UpdateHelper();
            await helper.CheckForUpdatesAsync();
        }

        private void doctorButton_Click(object sender, EventArgs e)
        {
            UserCredentials.Role = "Doctor";
            DBConfig.SetConnectionString("Doctor");

            using (var loginForm = new UserLoginForm("Doctor"))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    string message;

                    if (UserCredentials.ValidateLogin(loginForm.EnteredUsername, loginForm.EnteredPassword, out message))
                    {
                        using (var welcome = new WelcomeForm(UserCredentials.Fullname, UserCredentials.Role))
                            welcome.ShowDialog();

                        new MainFormDoctor().Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void receptionistButton_Click(object sender, EventArgs e)
        {
            UserCredentials.Role = "Receptionist";
            DBConfig.SetConnectionString("Receptionist");

            using (var loginForm = new UserLoginForm("Receptionist"))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    string message;

                    if (UserCredentials.ValidateLogin(loginForm.EnteredUsername, loginForm.EnteredPassword, out message))
                    {
                        using (var welcome = new WelcomeForm(UserCredentials.Fullname, UserCredentials.Role))
                            welcome.ShowDialog();

                        new MainFormReceptionist().Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void Login_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void OpenAdminLogin()
        {
            UserCredentials.Role = "Admin";
            DBConfig.SetConnectionString("Admin");

            using (var adminLoginForm = new UserLoginForm("Admin"))
            {
                if (adminLoginForm.ShowDialog() == DialogResult.OK)
                {
                    string message;

                    if (UserCredentials.ValidateLogin(adminLoginForm.EnteredUsername, adminLoginForm.EnteredPassword, out message))
                    {
                        new SystemAdminForm().Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(message, "Admin Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



    }
}
