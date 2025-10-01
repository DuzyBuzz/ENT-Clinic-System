using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.InsertForms;
using ENT_Clinic_System.Inventory;
using ENT_Clinic_System.Payments;
using ENT_Clinic_System.PrintingForms;
using ENT_Clinic_System.Reports;
using ENT_Clinic_System.Reports.ParamsForm;
using ENT_Clinic_System.UI;
using ENT_Clinic_System.UserControls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
            // Show a MessageBox with Yes and No buttons
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit?", // Message text
                "Confirm Exit",                  // Title of the message box
                MessageBoxButtons.YesNo,         // Buttons to display
                MessageBoxIcon.Question          // Icon type
            );

            // Check the user's choice
            if (result == DialogResult.Yes)
            {
                // Exit the application
                this.Close();
            }
            // If No, do nothing, the form stays open
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

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stockOutButton_Click(object sender, EventArgs e)
        {

        }

        private void systemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemAdminForm systemSettingsForm = new SystemAdminForm();
            systemSettingsForm.Show();
        }
        private async void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateHelper helper = new UpdateHelper();
            await helper.CheckForUpdatesAsync();
        }

        private void MainFormReceptionist_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();

        }


        private void scheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AppointmentsUserControl appointmentsUserControl = new AppointmentsUserControl();
            LoadUserControl(appointmentsUserControl);
        }

        private void patientQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PatientsQueue patientsQueue = new PatientsQueue();
            patientsQueue.Show();
        }

        private void doctorPatientsQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoctorPatientQueue doctorPatientQueue = new DoctorPatientQueue();
            doctorPatientQueue.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void returnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteOffForm writeOffForm = new WriteOffForm();
            writeOffForm.Show();
        }

        private void stocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Create the report instance
                var report = new Reports.LowStockReport();

                // Show the print preview
                report.ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to generate Low Stock Report: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void itemsDispensingPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceForm invoiceForm = new InvoiceForm();
            invoiceForm.Show();
        }

        private void billingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BillingInvoiceForm billingInvoiceForm = new BillingInvoiceForm();
            billingInvoiceForm.Show();
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void billingToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (var paramForm = new Reports.ParamsForm.BillingParamsForm())
            {
                if (paramForm.ShowDialog() == DialogResult.OK)
                {
                    string patient = paramForm.SelectedPatient;
                    DateTime fromDate = paramForm.FromDate;
                    DateTime toDate = paramForm.ToDate;

                    var report = new Reports.BillingReport(patient, fromDate, toDate);
                    report.ShowPreview();
                }
            }
        }


        private void revenueToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void dispensingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open the parameters form
            using (var paramForm = new Reports.ParamsForm.DispenseParamsForm())
            {
                if (paramForm.ShowDialog() == DialogResult.OK)
                {
                    // Get all selected parameters
                    string patient = paramForm.SelectedPatient;
                    string category = paramForm.SelectedCategory;
                    string itemName = paramForm.SelectedItemName;
                    string description = paramForm.SelectedDescription;
                    DateTime fromDate = paramForm.FromDate;
                    DateTime toDate = paramForm.ToDate;

                    // Pass all parameters to the report
                    var report = new Reports.DispensingReport(patient, category, itemName, description, fromDate, toDate);
                    report.ShowPreview();
                }
            }
        }






        private void expiryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the report
                var report = new Reports.ExpiryReport();

                // Show print preview
                report.ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating expiry report: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void wastageDamagedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salesReportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void stockOnHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Open the parameter form
                using (var paramForm = new StockOnHandReportForm())
                {
                    if (paramForm.ShowDialog() == DialogResult.OK)
                    {
                        // 2️⃣ Get the selected parameters
                        string category = paramForm.SelectedCategory;
                        DateTime asOfDate = paramForm.AsOfDate;

                        // 3️⃣ Create the report
                        StockOnHandReport report = new StockOnHandReport(category, asOfDate);

                        // 4️⃣ Show the preview
                        report.ShowPreview();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening Stock On Hand report: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var paramForm = new Reports.ParamsForm.SalesParamsForm())
            {
                if (paramForm.ShowDialog() == DialogResult.OK)
                {
                    var report = new Reports.SalesReport(paramForm.FromDate, paramForm.ToDate);
                    report.ShowPreview();
                }
            }
        }
    }

}
