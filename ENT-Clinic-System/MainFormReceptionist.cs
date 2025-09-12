using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.Inventory;
using ENT_Clinic_System.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System
{
    public partial class MainFormReceptionist : Form
    {
        private Rectangle originalBounds;
        public MainFormReceptionist()
        {
            InitializeComponent();
            // Form settings
            this.FormBorderStyle = FormBorderStyle.None; // Remove default border
            this.MaximizeBox = true;                     // Allow maximize
            this.MinimizeBox = true;                     // Allow minimize
            this.ShowInTaskbar = true;                   // Show in taskbar
            this.StartPosition = FormStartPosition.CenterScreen; // Optional
            this.Resize += MainForm_Resize;
            this.FormClosing -= MainForm_FormClosing; // remove any previous subscription
            this.FormClosing += MainForm_FormClosing; // attach once

        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            UpdateMaximizeButtonIcon();
        }

        private void addNewPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if CreateConcessionaireForm is already open
            foreach (Form form in Application.OpenForms)
            {
                if (form is PatientInfoForm)
                {
                    form.BringToFront();   // bring it to front
                    form.Focus();          // set focus
                    return;                // stop, don’t open another
                }
            }

            // If not open, create and show new instance
            var addConcessionaireForm = new PatientInfoForm();
            addConcessionaireForm.Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Optional: ask the user for confirmation
            var result = MessageBox.Show(
                "Are you sure you want to exit the application?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // This will close all forms and exit the application
                Application.Exit();
            }
            else
            {
                // Cancel the closing
                e.Cancel = true;
            }

        }


        private void LoadUserControl(UserControl uc)
        {
            MainPanel.Controls.Clear();     
            uc.Dock = DockStyle.Fill;       
            MainPanel.Controls.Add(uc);     
            uc.BringToFront();            
        }


        private void patientListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatientListControl patientListControl = new PatientListControl();
            LoadUserControl(patientListControl);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            originalBounds = this.Bounds;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void maximizeMaximizeButton_Click(object sender, EventArgs e)
        {
            if (this.Bounds != Screen.FromControl(this).WorkingArea)
            {
                // Save current bounds before maximizing
                originalBounds = this.Bounds;

                // Maximize but respect taskbar
                this.Bounds = Screen.FromControl(this).WorkingArea;
            }
            else
            {
                // Restore previous bounds
                this.Bounds = originalBounds;
            }
        }


        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void UpdateMaximizeButtonIcon()
        {
            if (this.Bounds.Width == Screen.FromControl(this).WorkingArea.Width &&
                this.Bounds.Height == Screen.FromControl(this).WorkingArea.Height)
                maximizeMaximizeButton.Text = "❐"; // restore icon
            else
                maximizeMaximizeButton.Text = "🗖"; // maximize icon
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 1;
            const int HTCAPTION = 2;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;

            const int RESIZE_BORDER = 6; 

            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);
                var pos = this.PointToClient(new Point(m.LParam.ToInt32()));
                if (pos.X < RESIZE_BORDER && pos.Y < RESIZE_BORDER) m.Result = (IntPtr)HTTOPLEFT;
                else if (pos.X > this.ClientSize.Width - RESIZE_BORDER && pos.Y < RESIZE_BORDER) m.Result = (IntPtr)HTTOPRIGHT;
                else if (pos.X < RESIZE_BORDER && pos.Y > this.ClientSize.Height - RESIZE_BORDER) m.Result = (IntPtr)HTBOTTOMLEFT;
                else if (pos.X > this.ClientSize.Width - RESIZE_BORDER && pos.Y > this.ClientSize.Height - RESIZE_BORDER) m.Result = (IntPtr)HTBOTTOMRIGHT;
                else if (pos.X < RESIZE_BORDER) m.Result = (IntPtr)HTLEFT;
                else if (pos.X > this.ClientSize.Width - RESIZE_BORDER) m.Result = (IntPtr)HTRIGHT;
                else if (pos.Y < RESIZE_BORDER) m.Result = (IntPtr)HTTOP;
                else if (pos.Y > this.ClientSize.Height - RESIZE_BORDER) m.Result = (IntPtr)HTBOTTOM;
                else m.Result = (IntPtr)HTCAPTION; 
                return;
            }

            base.WndProc(ref m);
        }

        private void stockInButton_Click(object sender, EventArgs e)
        {
            InventoryForm inventoryForm = new InventoryForm();
            inventoryForm.Show();

        }
    }

}
