using Accord;
using ENT_Clinic_System.Admission;
using ENT_Clinic_System.Consultation;
using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.Helpers.ReportHelpers;
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



        public void LoadUserControl(UserControl uc)
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
            Dashboard dashboard = new Dashboard();
            LoadUserControl(dashboard);
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
            this.Hide();
            BackupSql();
            Application.Exit();

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


        private void scheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AppointmentsUserControl appointmentsUserControl = new AppointmentsUserControl();
            LoadUserControl(appointmentsUserControl);
        }

        private void patientQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoctorPatientsQueu doctorPatientsQueue = new DoctorPatientsQueu();
            LoadUserControl(doctorPatientsQueue);

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


        }




        private void revenueToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void wastageDamagedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salesReportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

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
            InventoryForm inventoryForm = new InventoryForm();
            LoadUserControl(inventoryForm);
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

            PaymentsControl paymentsControl = new PaymentsControl();
            LoadUserControl(paymentsControl);

        }



        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void admitOrderTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        // ===================== PATIENT VISIT REPORT =====================
        private void patientVisitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var monthForm = new MonthYearSelectionForm())
            {
                if (monthForm.ShowDialog() == DialogResult.OK)
                {
                    int selectedMonth = monthForm.SelectedMonth;
                    int selectedYear = monthForm.SelectedYear;

                    var filters = new Dictionary<string, object>
            {
                { "MONTH(Queued_At)", selectedMonth },
                { "YEAR(Queued_At)", selectedYear }
            };

                    var displayColumns = new List<string>
            {
                "Queue_Number",
                "Patient_Name",
                "Address",
                "Age",
                "Sex",
                "Civil_Status",
                "Patient_Contact_Number",
                "Emergency_Contact_Number",
                "Referred_By",
                "Status",
                "Queued_At",
                "Finished_Time"
            };

                    var columnWidths = new Dictionary<string, float>
            {
                { "Queue_Number", 70 },
                { "Patient_Name", 140 },
                { "Address", 200 },
                { "Age", 40 },
                { "Sex", 40 },
                { "Civil_Status", 80 },
                { "Patient_Contact_Number", 110 },
                { "Emergency_Contact_Number", 110 },
                { "Referred_By", 100 },
                { "Status", 80 },
                { "Queued_At", 110 },
                { "Finished_Time", 110 }
            };

                    ReportHelper_v2.GenerateReport(
                        tableName: "view_queue_with_patients",
                        displayColumns: displayColumns,
                        filters: filters,
                        reportTitle: "PATIENT VISIT REPORT",
                        reportSubtitle: $"For {monthForm.SelectedMonth}/{monthForm.SelectedYear}",
                        showRowNumbers: false,
                        landscape: false, // portrait
                        totalColumns: new List<string>(),
                        columnWidths: columnWidths
                    );

                    ReportHelper_v2.ShowPreview();
                }
            }
        }

        // ===================== WRITE-OFF REPORT =====================
        private void writeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var monthForm = new MonthYearSelectionForm())
            {
                if (monthForm.ShowDialog() == DialogResult.OK)
                {
                    int selectedMonth = monthForm.SelectedMonth;
                    int selectedYear = monthForm.SelectedYear;

                    var filters = new Dictionary<string, object>
            {
                { "MONTH(Created_At)", selectedMonth },
                { "YEAR(Created_At)", selectedYear }
            };

                    var displayColumns = new List<string>
            {
                "Generic_Name",
                "Brand_Name",
                "Strength",
                "Dosage",
                "Quantity",
                "Reason",
                "Expiration_Date",
                "Created_At"
            };

                    var columnWidths = new Dictionary<string, float>
            {
                { "Generic_Name", 180 },
                { "Brand_Name", 180 },
                { "Strength", 90 },
                { "Dosage", 90 },
                { "Quantity", 80 },
                { "Reason", 180 },
                { "Expiration_Date", 110 },
                { "Created_At", 110 }
            };

                    ReportHelper_v2.GenerateReport(
                        tableName: "v_write_off_report",
                        displayColumns: displayColumns,
                        filters: filters,
                        reportTitle: "WRITE-OFF REPORT",
                        reportSubtitle: $"For {monthForm.SelectedMonth}/{monthForm.SelectedYear}",
                        showRowNumbers: false,
                        landscape: false, // portrait
                        totalColumns: new List<string> { "Quantity" },
                        columnWidths: columnWidths
                    );

                    ReportHelper_v2.ShowPreview();
                }
            }
        }


        // ===================== DISPENSING REPORT =====================
        private void dispensingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var monthForm = new MonthYearSelectionForm())
            {
                if (monthForm.ShowDialog() == DialogResult.OK)
                {
                    int selectedMonth = monthForm.SelectedMonth;
                    int selectedYear = monthForm.SelectedYear;

                    var filters = new Dictionary<string, object>
            {
                { "MONTH(Invoice_Date)", selectedMonth },
                { "YEAR(Invoice_Date)", selectedYear }
            };

                    var displayColumns = new List<string>
            {
                "Invoice_ID", // keep
                "Invoice_Date",
                "Customer_Name",
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

                    var columnWidths = new Dictionary<string, float>
            {
                { "Invoice_ID", 80 },
                { "Invoice_Date", 110 },
                { "Customer_Name", 150 },
                { "Prescription_ID", 90 },
                { "Item_ID", 80 },
                { "Generic_Name", 130 },
                { "Brand_Name", 130 },
                { "Strength", 70 },
                { "Dosage", 70 },
                { "Category", 90 },
                { "Quantity", 70 },
                { "Cost_Price", 80 },
                { "Unit_Price", 80 },
                { "Total", 100 }
            };

                    ReportHelper_v2.GenerateReport(
                        tableName: "v_detailed_dispensing_report",
                        displayColumns: displayColumns,
                        filters: filters,
                        reportTitle: "DISPENSING REPORT",
                        reportSubtitle: $"For {monthForm.SelectedMonth}/{monthForm.SelectedYear}",
                        showRowNumbers: true,
                        landscape: false, // portrait
                        totalColumns: new List<string> { "Quantity", "Total", "Cost_Price", "Unit_Price" },
                        columnWidths: columnWidths
                    );

                    ReportHelper_v2.ShowPreview();
                }
            }
        }

        // ===================== LOW STOCK REPORT =====================
        private void stocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var displayColumns = new List<string>
    {
        "Generic_Name",
        "Brand_Name",
        "Strength",
        "Dosage",
        "Category",
        "Current_Stock"
    };

            var columnWidths = new Dictionary<string, float>
    {
    { "Generic_Name", 250 },
    { "Brand_Name", 250 },
    { "Strength", 100 },
    { "Dosage", 100 },
    { "Category", 120 },
    { "Current_Stock", 100 }
    };

            ReportHelper_v2.GenerateReport(
                tableName: "v_low_stock_report",
                displayColumns: displayColumns,
                filters: null,
                reportTitle: "LOW STOCK REORDER REPORT",
                reportSubtitle: "",
                showRowNumbers: false,
                landscape: false,
                totalColumns: new List<string>(),
                columnWidths: columnWidths
            );

            ReportHelper_v2.ShowPreview();
        }

        // ===================== BILLING REPORT =====================
        private void billingToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (var monthForm = new MonthYearSelectionForm())
            {
                if (monthForm.ShowDialog() == DialogResult.OK)
                {
                    int selectedMonth = monthForm.SelectedMonth;
                    int selectedYear = monthForm.SelectedYear;

                    var filters = new Dictionary<string, object>
            {
                { "MONTH(Date_Billed)", selectedMonth },
                { "YEAR(Date_Billed)", selectedYear }
            };

                    var displayColumns = new List<string>
            {
                "Billing_ID", // keep
                "Date_Billed",
                "Patient_Name",
                "Fee",
                "Discount_Percent",
                "Discount_Amount",
                "Total_Amount",
                "Amount_Paid",
                "Balance",
                "Payment_Status",
                "Note"
            };

                    var columnWidths = new Dictionary<string, float>
            {
                { "Billing_ID", 80 },
                { "Date_Billed", 120 },
                { "Patient_Name", 150 },
                { "Fee", 80 },
                { "Discount_Percent", 90 },
                { "Discount_Amount", 90 },
                { "Total_Amount", 100 },
                { "Amount_Paid", 100 },
                { "Balance", 100 },
                { "Payment_Status", 120 },
                { "Note", 150 }
            };

                    ReportHelper_v2.GenerateReport(
                        tableName: "v_billing_with_patient_report",
                        displayColumns: displayColumns,
                        filters: filters,
                        reportTitle: "BILLING REPORT",
                        reportSubtitle: $"For {monthForm.SelectedMonth}/{monthForm.SelectedYear}",
                        showRowNumbers: true,
                        landscape: false, // portrait
                        totalColumns: new List<string> { "Fee", "Discount_Amount", "Total_Amount", "Amount_Paid" },
                        columnWidths: columnWidths
                    );

                    ReportHelper_v2.ShowPreview();
                }
            }
        }

        // ===================== STOCK NEAR EXPIRY REPORT =====================
        private void expiryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var displayColumns = new List<string>
    {
        "Generic_Name",
        "Brand_Name",
        "Strength",
        "Dosage",
        "Movement_Type",
        "Quantity",
        "Movement_Date",
        "Expiration_Date"
    };

            var columnWidths = new Dictionary<string, float>
    {
        { "Generic_Name", 150 },
        { "Brand_Name", 150 },
        { "Strength", 80 },
        { "Dosage", 80 },
        { "Movement_Type", 100 },
        { "Quantity", 60 },
        { "Movement_Date", 110 },
        { "Expiration_Date", 110 },
        { "User_ID", 80 }
    };

            ReportHelper_v2.GenerateReport(
                tableName: "v_stock_near_expiry_report",
                displayColumns: displayColumns,
                filters: null,
                reportTitle: "STOCK NEAR EXPIRY REPORT",
                reportSubtitle: "",
                showRowNumbers: false,
                landscape: false,
                totalColumns: new List<string> { "Quantity" },
                columnWidths: columnWidths
            );

            ReportHelper_v2.ShowPreview();
        }

        // ===================== STOCK ON HAND REPORT =====================
        private void stockOnHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var displayColumns = new List<string>
    {
        "Generic_Name",
        "Brand_Name",
        "Strength",
        "Dosage",
        "Category",
        "Current_Stock",
        "Cost_Price",
        "Selling_Price",
        "Updated_At"
    };

            var columnWidths = new Dictionary<string, float>
    {
        { "Generic_Name", 250 },
        { "Brand_Name", 250 },
        { "Strength", 90 },
        { "Dosage", 90 },
        { "Category", 120 },
        { "Current_Stock", 100 },
        { "Cost_Price", 90 },
        { "Selling_Price", 90 },
        { "Updated_At", 120 }
    };

            ReportHelper_v2.GenerateReport(
                tableName: "v_stock_on_hand_report",
                displayColumns: displayColumns,
                filters: null,
                reportTitle: "STOCK ON HAND REPORT",
                reportSubtitle: "",
                showRowNumbers: false,
                landscape: false, // portrait
                totalColumns: new List<string> { "Current_Stock" , "Cost_Price"  , "Selling_Price"},
                columnWidths: columnWidths
            );

            ReportHelper_v2.ShowPreview();
        }


    }

}
