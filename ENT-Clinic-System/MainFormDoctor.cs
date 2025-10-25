using ENT_Clinic_System.Consultation;
using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.InsertForms;
using ENT_Clinic_System.Inventory;
using ENT_Clinic_System.Payments;
using ENT_Clinic_System.PrintingForms;
using ENT_Clinic_System.Reports;
using ENT_Clinic_System.Reports.ParamsForm;
using ENT_Clinic_System.UI;
using ENT_Clinic_System.UserControls;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ENT_Clinic_System
{
    public partial class MainFormDoctor : Form
    {
        private Rectangle originalBounds;
        public MainFormDoctor()
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
            ShowSingleInstanceForm<PatientInfoForm>();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        /// <summary>
        /// Opens a single-instance form. If already open, brings it to front.
        /// </summary>
        private void ShowSingleInstanceForm<T>() where T : Form, new()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is T)
                {
                    // If the form is minimized, restore it
                    if (form.WindowState == FormWindowState.Minimized)
                    {
                        form.WindowState = FormWindowState.Maximized;
                    }

                    // Bring the form to front and focus it
                    form.BringToFront();
                    form.Focus();
                    return;
                }
            }

            // Otherwise, create and show a new instance
            var instance = new T();
            instance.Show();
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
            systemSettingsForm.ShowDialog();
        }
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoCompleteManager autoCompleteManager = new AutoCompleteManager();
            autoCompleteManager.Show();
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
            ShowSingleInstanceForm<DoctorPatientsQueu>();

        }

        private void doctorPatientsQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();
        }


        private void stocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Columns to display
            List<string> displayColumns = new List<string>
    {
        "Item_ID",
        "Generic_Name",
        "Brand_Name",
        "Strength",
        "Dosage",
        "Category",
        "Current_Stock",
    };

            // Columns to hide (internal IDs)
            List<string> hiddenColumns = new List<string>
    {
        "Item_ID"
    };

            // Generate the report (no date filtering needed)
            ReportHelper.GenerateReport(
                tableName: "v_low_stock_report",
                dateColumn: null,
                dateFrom: null,
                dateTo: null,
                displayColumns: displayColumns,
                hiddenColumns: hiddenColumns,
                                 reportTitle: "LOW STOCK REORDER REPORT"
            );

            // Show print preview
            ReportHelper.ShowPreview();
        }


        private void itemsDispensingPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSingleInstanceForm<InvoiceForm>();
        }

        private void billingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSingleInstanceForm<BillingInvoiceForm>();
        }

        private void paymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSingleInstanceForm<PaymentsControl>();
        }

        private void billingToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (var dateForm = new DateRangeForm())
            {
                if (dateForm.ShowDialog() == DialogResult.OK)
                {
                    DateTime fromDate = dateForm.FromDate;
                    DateTime toDate = dateForm.ToDate;

                    // Columns to display in the report
                    List<string> displayColumns = new List<string>
            {
                "Billing_ID",
                "Consultation_ID",
                "Patient_ID",
                "Patient_Name",
                "Fee",
                "Discount_Percent",
                "Discount_Amount",
                "Total_Amount",
                "Amount_Paid",
                "Balance",
                "Payment_Status",
                "Note",
                "Date_Billed"
            };

                    // Columns to hide (internal IDs, if you want)
                    List<string> hiddenColumns = new List<string>
            {
                "Billing_ID",
                "Consultation_ID",
                "Patient_ID"
            };

                    // Generate the report (dateColumn must match your view column)
                    ReportHelper.GenerateReport(
                        tableName: "v_billing_with_patient_report",
                        dateColumn: "Date_Billed",
                        dateFrom: fromDate,
                        dateTo: toDate,
                        displayColumns: displayColumns,
                        hiddenColumns: hiddenColumns,
                                         reportTitle: "BILLING REPORT"
                    );

                    // Show print preview
                    ReportHelper.ShowPreview();
                }
            }
        }



        private void revenueToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void dispensingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var printer = new DispensingReportPrinter();
            //printer.ShowPreview();

            using (var dateForm = new DateRangeForm())
            {
                if (dateForm.ShowDialog() == DialogResult.OK)
                {
                    DateTime fromDate = dateForm.FromDate;
                    DateTime toDate = dateForm.ToDate;

                    // Display headers must match the view aliases with underscores
                    List<string> displayColumns = new List<string>
        {
            "Invoice_ID",
            "Invoice_Date",
            "Customer_Name",
            "Prescription_ID",
            "Item_ID",
            "Generic_Name",
            "Brand_Name",
            "Strength",
            "Dosage",
            "Category",
            "Quantity",
            "Cost_Price",
            "Unit_Price",
            "Total"
        };

                    // Columns to hide (use the same underscore names)
                    List<string> hiddenColumns = new List<string> { "Invoice_ID", "Prescription_ID", "Item_ID" };

                    // Generate the report
                    // dateColumn must match the real column name (with underscore)
                    ReportHelper.GenerateReport(
                        tableName: "v_detailed_dispensing_report",
                        dateColumn: "Invoice_Date", // matches view alias
                        dateFrom: fromDate,
                        dateTo: toDate,
                        displayColumns: displayColumns,
                        hiddenColumns: hiddenColumns,
                                         reportTitle: "DISPENSING REPORT"
                    );

                    // Show print preview
                    ReportHelper.ShowPreview();
                }
            }





        }






        private void expiryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Columns to display in the report
            List<string> displayColumns = new List<string>
    {
        "Movement_ID",
        "Item_ID",
        "Generic_Name",
        "Brand_Name",
        "Strength",
        "Dosage",
        "Movement_Type",
        "Quantity",
        "Movement_Date",
        "Expiration_Date",
        "User_ID"
    };

            // Columns to hide in the printed report (internal IDs)
            List<string> hiddenColumns = new List<string>
    {
        "Movement_ID",
        "Item_ID",
        "User_ID"
    };

            // Generate the report using the near-expiry view
            ReportHelper.GenerateReport(
                tableName: "v_stock_near_expiry_report",
                dateColumn: null,   // view already filters by near expiry, no need for a date filter
                dateFrom: null,
                dateTo: null,
                displayColumns: displayColumns,
                hiddenColumns: hiddenColumns,
                                 reportTitle: "NEAR EXPIRATION REPORT"
            );

            // Show print preview
            ReportHelper.ShowPreview();
        }


        private void wastageDamagedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salesReportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void stockOnHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Columns to display in the report
            List<string> displayColumns = new List<string>
    {
        "Item_ID",
        "Generic_Name",
        "Brand_Name",
        "Strength",
        "Dosage",
        "Category",
        "Current_Stock",
        "Updated_At"
    };

            // Columns to hide in the printed report (internal IDs)
            List<string> hiddenColumns = new List<string>
    {
        "Item_ID"
    };

            // Generate the report
            ReportHelper.GenerateReport(
                tableName: "v_stock_on_hand_report",
                dateColumn: null,  // no date filtering needed
                dateFrom: null,
                dateTo: null,
                displayColumns: displayColumns,
                hiddenColumns: hiddenColumns,
                 reportTitle: "STOCK ON HAND REPORT"
            );

            // Show print preview
            ReportHelper.ShowPreview();
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSingleInstanceForm<InventoryForm>();
        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserProfile userProfile = new UserProfile();
            userProfile.ShowDialog();
        }

        private void patientsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void consultationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void consultationsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConsultationListControl consultationList = new ConsultationListControl();
            LoadUserControl(consultationList);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void paymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSingleInstanceForm<PaymentsControl>();
        }

        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Columns to display in the report
            List<string> displayColumns = new List<string>
    {
        "Write_Off_ID",
        "Item_ID",
        "Generic_Name",
        "Brand_Name",
        "Strength",
        "Dosage",
        "Quantity",
        "Reason",
        "Expiration_Date",
        "Created_At",
    };

            // Columns to hide in the printed report (internal IDs)
            List<string> hiddenColumns = new List<string>
    {
        "Write_Off_ID",
        "Item_ID"
    };

            // Generate the report (no date filtering needed)
            ReportHelper.GenerateReport(
                tableName: "v_write_off_report",
                dateColumn: null,
                dateFrom: null,
                dateTo: null,
                displayColumns: displayColumns,
                hiddenColumns: hiddenColumns,
                 reportTitle: "WRITE-OFF REPORT"
            );

            // Show print preview
            ReportHelper.ShowPreview();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

}
