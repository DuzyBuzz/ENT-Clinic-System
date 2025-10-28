using ENT_Clinic_System.Consultation;
using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.UserControls
{
    public partial class PatientListControl : UserControl
    {
        private DGVCrudHelper patientCrud;
        private int currentRow = 0;
        public PatientListControl()
        {
            InitializeComponent();

        }

        private void PatientListControl_Load(object sender, EventArgs e)
        {

            LoadPatients();

        }
        private void LoadPatients()
        {
            AutoCompleteHelper.SetupAutoComplete(
                searchPatientNameTextBox,
                "patients",
                new List<string> { "full_name" }
            );



            // Columns to allow editing
            List<string> columns = new List<string>
            {
                "patient_id",
                "full_name",
                "address",
                "birth_date",
                "age",
                "sex",
                "civil_status",
                "patient_contact_number",
                "emergency_name",
                "emergency_contact_number",
                "emergency_relationship",
                            "referred_by",
                "created_at",
                "photo"
            };

            try
            {
                if (patientCrud == null)
                    patientCrud = new DGVCrudHelper(patientsDataGridView, "patients", columns, "patient_id");

                patientCrud.SetPageInfoLabel(pageLabel);
                patientCrud.LoadData();

                // ✅ Sort by full_name ascending (if bound to DataTable)
                if (patientsDataGridView.DataSource is DataTable dt)
                {
                    dt.DefaultView.Sort = "full_name ASC";
                    patientsDataGridView.DataSource = dt;
                }

                UpdatePaginationButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load patient list: " + ex.Message);
            }


        }

        private void searchPatientNameTextBox_TextChanged(object sender, EventArgs e)
        {

            SearchPatients();
        }
        private void SearchPatients()
        {
            // Define which columns you want to show when searching
            string[] displayColumns = {
                "patient_id",
                "full_name",
                "address",
                "birth_date",
                "age",
                "sex",
                "civil_status",
                "patient_contact_number",
                "emergency_name",
                "emergency_contact_number",
                "emergency_relationship",
                            "referred_by",
                "created_at",
                "photo"
    };

            // Perform search with limited columns
            SearchHelper.Search(
                dgv: patientsDataGridView,
                tableName: "patients",
                columnNames: new string[] { "full_name" },
                filterControl: searchPatientNameTextBox,
                columns: displayColumns
            );

            // Disable pagination when showing search results
            prevButton.Enabled = false;
            nextButton.Enabled = false;
            pageLabel.Text = "Search results";
        }
        private void searchPatientButton_Click(object sender, EventArgs e)
        {
            SearchPatients();
        }


        private void refreshPatientsButton_Click(object sender, EventArgs e)
        {
            LoadPatients();
        }
        private void UpdatePaginationButtons()
        {
            prevButton.Enabled = patientCrud.CurrentPage > 1;
            nextButton.Enabled = patientCrud.CurrentPage < patientCrud.TotalPages;
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            patientCrud.PreviousPage();
            UpdatePaginationButtons();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            patientCrud.NextPage();
            UpdatePaginationButtons(); 
        }

        private void viewConsultationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int patientId = GetSelectedPatientId();
            if (patientId > 0)
            {
                ConsultationControl consultationControl = new ConsultationControl(patientId);
                consultationControl.Show();
            }
        }

        private void addLatestConsultationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        // Get patient_id from selected row
        private int GetSelectedPatientId()
        {
            if (patientsDataGridView.SelectedRows.Count > 0)
            {
                return Convert.ToInt32(patientsDataGridView.SelectedRows[0].Cells["patient_id"].Value);
            }
            return -1;
        }

        // Load a new UserControl (replace with your panel if needed)
        private void LoadUserControl(UserControl control)
        {
            this.Controls.Clear(); // Or replace "this" with your mainPanel
            control.Dock = DockStyle.Fill;
            this.Controls.Add(control);
        }

        private void patientsDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = patientsDataGridView.HitTest(e.X, e.Y);

                if (hitTest.RowIndex >= 0)
                {
                    patientsDataGridView.ClearSelection();
                    patientsDataGridView.Rows[hitTest.RowIndex].Selected = true;
                }
            }
        }

        private void searchPatientNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            SearchPatients();
        }


        private void patientsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void PatientListControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void patientsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void consultationHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }







        private void printConsultationHistoryButton_Click(object sender, EventArgs e)
        {

        }

        private void patientsContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {

        }

        private void createUntrackedLabRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printConsultationHistoryButton_Click_1(object sender, EventArgs e)
        {

        }

        private void consultationHistoryToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            int patientId = GetSelectedPatientId();
            if (patientId <= 0) return;

            // 1️⃣ Show "Loading..." immediately
            consultationHistoryToolStripMenuItem.DropDownItems.Clear();
            var loadingItem = new ToolStripMenuItem("Loading...");
            consultationHistoryToolStripMenuItem.DropDownItems.Add(loadingItem);
            Application.DoEvents(); // Forces UI to update so "Loading..." is visible

            // 2️⃣ Define actions - mixed simple and nested submenus
            var actions = new Dictionary<string, object>()
    {
        { "Print Consultation History", new Action<int>(consultationId =>
            {
                    try
                    {
                        // Create the print helper (this loads the patient + consultation data)
                        PrintTextHistory printer = new PrintTextHistory(patientId, consultationId);

                        // Call your custom ShowPreview() (the version with custom toolbar buttons)
                        printer.ShowPreview();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error printing consultation history: " + ex.Message,
                            "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

            })
        },
        { "Print Attachments", new Action<int>(consultationId =>
            {
                try
                {
                    PrintAttachments printForm = new PrintAttachments(consultationId);
                    printForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error printing attachments: " + ex.Message);
                }
            })
        },
        { "Print Medical Certificate", new Action<int>(consultationId =>
            {
                try
                {
                    using (var inputForm = new PurposeInputForm())
                    {
                        if (inputForm.ShowDialog() == DialogResult.OK)
                        {
                            string requestName = inputForm.PurposeText;
                            var printer = new MedicalCertificatePrinter(patientId, consultationId, requestName);
                            printer.ShowPreview();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error printing medical certificate: " + ex.Message);
                }
            })
        },
{
            // ✅ Prescription submenu
            "Prescription", new Func<int, ToolStripMenuItem>(consultationId =>
            {
                var prescriptionMenu = new ToolStripMenuItem("Prescription");

                // 🩺 Submenu: Create Prescription
                var createPrescription = new ToolStripMenuItem("Create Prescription");
                createPrescription.Click += (s, args) =>
                {
                    // Assuming you have a form for creating prescriptions
                    var prescriptionForm = new PrescriptionForm(patientId, consultationId);
                    prescriptionForm.ShowDialog();
                };
                prescriptionMenu.DropDownItems.Add(createPrescription);

                // 📄 Submenu: Show Prescriptions
                var showPrescriptions = new ToolStripMenuItem("Show Prescriptions");
                showPrescriptions.Click += (s, args) =>
                {
                    var printer = new PrescriptionPrintHelper(consultationId);
                    printer.ShowPreview();
                };
                prescriptionMenu.DropDownItems.Add(showPrescriptions);

                return prescriptionMenu;
            })
        },

        // ✅ Laboratory Request nested submenu
        { "Laboratory Request", new Func<int, ToolStripMenuItem>(consultationId =>
            {
                var labMenu = new ToolStripMenuItem("Laboratory");

                // Submenu: Create Lab Request
                var createLab = new ToolStripMenuItem("Create Laboratory Request");
                createLab.Click += (s, args) =>
                {
                    LabRequestForm labForm = new LabRequestForm(patientId, consultationId);
                    labForm.ShowDialog();
                };
                labMenu.DropDownItems.Add(createLab);

                // Submenu: Show Laboratory Requests
                var showLab = new ToolStripMenuItem("Laboratory Requests");
                showLab.Click += (s, args) =>
                {
                    var helper = new LabRequestPrintHelper(consultationId);
                    helper.ShowPreview();
                };
                labMenu.DropDownItems.Add(showLab);

                // Submenu: Show Laboratory Results
                var showResults = new ToolStripMenuItem("Laboratory Results");
                showResults.Click += (s, args) =>
                {
                    LabResultsForm labResultsForm = new LabResultsForm(consultationId, patientId);
                    labResultsForm.Show();
                };
                labMenu.DropDownItems.Add(showResults);

                return labMenu;
            })
        }

    };

            // 3️⃣ Populate all consultation rows dynamically using the updated helper
            DynamicToolStripMenuItemHelper.PopulateSubMenu(
                parentMenu: consultationHistoryToolStripMenuItem.DropDown, // DropDown of the parent item
                tableName: "consultation",
                idColumn: "consultation_id",
                displayColumns: new string[] { "consultation_date" },
                whereClause: $"patient_id = {patientId}",
                subMenuActions: actions // now accepts both Action<int> and Func<int, ToolStripMenuItem>
            );
        }



        private void showLaboratoryRequesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void scannedHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (patientsDataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a patient record first.",
                        "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Assume you only need to open one patient's history (not multiple)
                DataGridViewRow row = patientsDataGridView.SelectedRows[0];

                // Make sure the cell exists and has a value
                if (row.Cells["patient_id"].Value == null)
                {
                    MessageBox.Show("Selected row does not contain a valid patient ID.",
                        "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the IDs safely
                int patientId = Convert.ToInt32(row.Cells["patient_id"].Value);

                //// Optional: retrieve patient name for a friendlier window title
                //string patientName = row.Cells.Contains("patient_name")
                //    ? Convert.ToString(row.Cells["patient_name"].Value)
                //    : string.Empty;

                // Open the scanned history form for this patient
                try
                {
                    var consultationHistory = new ScannedConsultationHistoryForm(patientId.ToString());
                    //if (!string.IsNullOrEmpty(patientName))
                    //    consultationHistory.Text = $"Scanned Documents - {patientName}";

                    consultationHistory.Show(); // Use ShowDialog so it blocks until closed
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening consultation history: " + ex.Message,
                        "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error handling selection: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
