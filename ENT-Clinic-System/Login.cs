using ENT_Clinic_System.Helpers;
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
            dbCheckTimer = new Timer();
            dbCheckTimer.Interval = 10000; // 10 seconds
            dbCheckTimer.Tick += (s, e) => DBConnectionMonitor.TestConnection();
            dbCheckTimer.Start();

            // Optional: check immediately on form load
            DBConnectionMonitor.TestConnection();
        }

        private void DBConnectionMonitor_ConnectionStatusChanged(object sender, ConnectionStatusEventArgs e)
        {
            // Ensure UI updates happen on the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateUI(e)));
            }
            else
            {
                UpdateUI(e);
            }
        }

        private void UpdateUI(ConnectionStatusEventArgs e)
        {


            if (e.IsConnected)
            {
                connectionStatusLabel.Visible = false;

                Debug.WriteLine("DB Connection OK: " + e.Message);

            }
            else
            {
                connectionStatusLabel.Visible = true;
                connectionStatusLabel.ForeColor = Color.Red;
                connectionStatusLabel.Text = "❌ DB Connection Failed";
                Debug.WriteLine("Database connection failed: " + e.Message);

                // Show MessageBox only on connection failure (event triggers only on status change)
                MessageBox.Show(
                    "Database connection failed. Please check your settings.\n\n" + e.Message,
                    "Connection Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            PlaceholderHelper.AddPlaceholder(userNameTextBox, "Username");
            PlaceholderHelper.AddPlaceholder(passwordTextBox, "Password");
            passwordTextBox.UseSystemPasswordChar = true;
            // Get the version of the assembly
            Version appVersion = Assembly.GetExecutingAssembly().GetName().Version;

            // Display in the label
            versionLabel.Text = $"Version: {appVersion.Major}.{appVersion.Minor}.{appVersion.Build}.{appVersion.Revision}";
        }

        private async void CheckUpdates()
        {
            UpdateHelper helper = new UpdateHelper();
            await helper.CheckForUpdatesAsync();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = userNameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(
                    "SELECT user_id, username, password, full_name, role " +
                    "FROM user WHERE BINARY username=@username", conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedPassword = reader["password"].ToString();
                            string role = reader["role"].ToString();

                            // ✅ force case-sensitive password check
                            if (string.Equals(storedPassword, password, StringComparison.Ordinal))
                            {
                                // ✅ Save user credentials globally
                                UserCredentials.UserId = Convert.ToInt32(reader["user_id"]);
                                UserCredentials.Username = reader["username"].ToString();
                                UserCredentials.Fullname = reader["full_name"].ToString();
                                UserCredentials.Role = role;

                                // ✅ Open form based on role
                                Form mainForm = null;
                                if (role.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
                                {
                                    mainForm = new MainFormDoctor();
                                }
                                else if (role.Equals("Receptionist", StringComparison.OrdinalIgnoreCase))
                                {
                                    mainForm = new MainFormReceptionist();
                                }
                                else if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                                {
                                    mainForm = new SystemAdminForm();
                                }
                                else
                                {
                                    MessageBox.Show("Role not recognized. Please contact admin.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                mainForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to database: " + ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            VideoFolderHelper.DeleteAllVideos();
        }

        private async void versionLabel_Click(object sender, EventArgs e)
        {
            UpdateHelper helper = new UpdateHelper();
            await helper.CheckForUpdatesAsync();
        }
    }
}
