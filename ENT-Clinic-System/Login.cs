using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.UI;
using ENT_Clinic_System.UserControls;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace ENT_Clinic_System
{
    public partial class Login : Form
    {
        private Timer dbCheckTimer;

        public Login()
        {
            InitializeComponent();

            // Subscribe to the DB connection status event
            DBConnectionMonitor.ConnectionStatusChanged += DBConnectionMonitor_ConnectionStatusChanged;

            // Initialize and start the timer to test DB connection every 10 seconds
            dbCheckTimer = new Timer
            {
                Interval = 10000 // 10 seconds
            };
            dbCheckTimer.Tick += (s, e) => DBConnectionMonitor.TestConnection();
            dbCheckTimer.Start();

            // Optional: check immediately on form load
            DBConnectionMonitor.TestConnection();
        }

        private void DBConnectionMonitor_ConnectionStatusChanged(object sender, ConnectionStatusEventArgs e)
        {
            //// Ensure UI updates happen on the main thread
            ////if (InvokeRequired)
            ////{
            ////    Invoke(new Action(() => UpdateUI(e)));
            ////}
            ////else
            ////{
            ////    UpdateUI(e);
            //}
        }

        /// <summary>
        /// Updates the UI based on database connection status.
        /// </summary>
        //private void UpdateUI(ConnectionStatusEventArgs e)
        //{
        //    if (e.IsConnected)
        //    {
        //        connectionStatusLabel.Visible = false;
        //        Debug.WriteLine("✅ DB Connection OK: " + e.Message);
        //    }
        //    else
        //    {
        //        connectionStatusLabel.Visible = true;
        //        connectionStatusLabel.ForeColor = Color.Red;
        //        connectionStatusLabel.Text = "❌ DB Connection Failed";

        //        Debug.WriteLine("❌ Database connection failed: " + e.Message);

        //        // Show MessageBox only on connection failure (event triggers only on status change)
        //        MessageBox.Show(
        //            "Database connection failed. Please check your settings.\n\n" + e.Message,
        //            "Connection Error",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error
        //        );
        //    }
        //}

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
        }

        private async void versionLabel_Click(object sender, EventArgs e)
        {
            UpdateHelper helper = new UpdateHelper();
            await helper.CheckForUpdatesAsync();
        }

        private void doctorButton_Click(object sender, EventArgs e)
        {
            UserCredentials.Role = "Doctor"; // set role

            using (var loginForm = new UserLoginForm("Doctor"))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    if (UserCredentials.ValidateLogin(loginForm.EnteredUsername, loginForm.EnteredPassword, out string msg))
                    {

                        MainFormReceptionist mainFormReceptionist = new MainFormReceptionist();
                        mainFormReceptionist.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(msg, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void receptionistButton_Click(object sender, EventArgs e)
        {
            UserCredentials.Role = "Receptionist"; // set role

            using (var loginForm = new UserLoginForm("Receptionist"))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    if (UserCredentials.ValidateLogin(loginForm.EnteredUsername, loginForm.EnteredPassword, out string msg))
                    {

                        MainFormReceptionist mainFormReceptionist = new MainFormReceptionist();
                        mainFormReceptionist.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show(msg, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void OpenAdminLogin()
        {
            UserCredentials.Role = "Admin"; // set role to admin

            using (var adminLoginForm = new UserLoginForm("Admin"))
            {
                if (adminLoginForm.ShowDialog() == DialogResult.OK)
                {
                    if (UserCredentials.ValidateLogin(adminLoginForm.EnteredUsername, adminLoginForm.EnteredPassword, out string msg))
                    {
                        // Open the System Admin Form
                        SystemAdminForm adminForm = new SystemAdminForm();
                        adminForm.Show();
                    }
                    else
                    {
                        MessageBox.Show(msg, "Admin Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
