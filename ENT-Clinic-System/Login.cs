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

        // 🔹 Print test by clicking the picture box
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                // Create the PrintDocument instance
                PrintDocument printDoc = new PrintDocument();

                // 🖨️ Use the exact printer name shown in Control Panel
                printDoc.PrinterSettings.PrinterName = "POS-58";

                // 📄 Configure 58 mm custom paper (≈ 220 pixels wide)
                PaperSize paperSize = new PaperSize("Custom", 220, 600);
                printDoc.DefaultPageSettings.PaperSize = paperSize;
                printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                // Set high print quality
                printDoc.PrinterSettings.DefaultPageSettings.PrinterResolution =
                    new PrinterResolution { Kind = PrinterResolutionKind.High };

                // Handle the PrintPage event
                printDoc.PrintPage += PrintDoc_PrintPage_XPIID;

                // Start printing
                printDoc.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printing Error: " + ex.Message);
            }
        }

        // 🔹 Event: draw the layout for XP-IID printer
        private void PrintDoc_PrintPage_XPIID(object sender, PrintPageEventArgs e)
        {
            // Safe font for GDI printing — avoid Unicode fonts
            using (Font font = new Font("Lucida Console", 8, FontStyle.Regular))
            using (Font bold = new Font("Lucida Console", 9, FontStyle.Bold))
            {
                float left = 5;            // left margin
                float top = 5;             // top margin
                float lineHeight = font.GetHeight(e.Graphics) + 2;

                // Header
                e.Graphics.DrawString("DUZY BUZZ CAFE", bold, Brushes.Black, left, top);
                top += lineHeight;
                e.Graphics.DrawString("Buntatala, Jaro, Iloilo City", font, Brushes.Black, left, top);
                top += lineHeight * 2;

                // Receipt info
                e.Graphics.DrawString("Receipt No: 001", font, Brushes.Black, left, top);
                top += lineHeight;
                e.Graphics.DrawString(DateTime.Now.ToString("MMM dd, yyyy  hh:mm tt"), font, Brushes.Black, left, top);
                top += lineHeight;
                e.Graphics.DrawString("--------------------------------", font, Brushes.Black, left, top);
                top += lineHeight;

                // Items
                e.Graphics.DrawString("Inasal         ₱120.00", font, Brushes.Black, left, top);
                top += lineHeight;
                e.Graphics.DrawString("Softdrinks     ₱40.00", font, Brushes.Black, left, top);
                top += lineHeight;

                e.Graphics.DrawString("--------------------------------", font, Brushes.Black, left, top);
                top += lineHeight;

                // Total
                e.Graphics.DrawString("TOTAL: ₱160.00", bold, Brushes.Black, left, top);
                top += lineHeight * 2;

                // Footer
                e.Graphics.DrawString("Thank you for dining!", font, Brushes.Black, left, top);
                top += lineHeight;
                e.Graphics.DrawString("Please come again.", font, Brushes.Black, left, top);
                top += lineHeight * 2;

                // Add paper feed spacing (simulate paper cut)
                e.Graphics.DrawString("\n\n\n-------------------------------\n", font, Brushes.Black, left, top);
            }


        }
        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
