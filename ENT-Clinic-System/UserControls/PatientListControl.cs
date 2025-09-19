using ENT_Clinic_System.Helpers;
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
            this.Load += PatientListControl_Load;
        }

        private void PatientListControl_Load(object sender, EventArgs e)
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
                "created_at",
                "photo"
            };

            try
            {
                if (patientCrud == null)
                    patientCrud = new DGVCrudHelper(patientsDataGridView, "patients", columns, "patient_id");

                patientCrud.SetPageInfoLabel(pageLabel);
                patientCrud.LoadData();
                UpdatePaginationButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load patient list: " + ex.Message);
            }

            // Hide the primary key column
            if (patientsDataGridView.Columns.Contains("patient_id"))
                patientsDataGridView.Columns["patient_id"].Visible = false;




        }

        private void searchPatientNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void searchPatientButton_Click(object sender, EventArgs e)
        {
            SearchHelper.Search(
                dgv: patientsDataGridView,
                tableName: "patients",
                columnNames: new string[] { "full_name"},
                filterControl: searchPatientNameTextBox
            );

            // Disable pagination buttons when searching
            prevButton.Enabled = false;
            nextButton.Enabled = false;
            pageLabel.Text = "Search results";
        }


        private void refreshPatientsButton_Click(object sender, EventArgs e)
        {
            searchPatientNameTextBox.Text = "";
            patientCrud.LoadData();
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
                var viewControl = new ConsultationControl(patientId);
                LoadUserControl(viewControl);
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
            if (e.KeyCode == Keys.Enter)
            {
                // Prevent the 'ding' sound on Enter
                e.SuppressKeyPress = true;

                SearchHelper.Search(
                    dgv: patientsDataGridView,
                    tableName: "patients",
                    columnNames: new string[] { "full_name" },
                    filterControl: searchPatientNameTextBox
                );


                // Disable pagination buttons when searching
                prevButton.Enabled = false;
                nextButton.Enabled = false;
                pageLabel.Text = "Search results";
            }
        }

        private void patientsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void PatientListControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void patientsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                ReportHelper.PrintDataGridView(patientsDataGridView, "Patient List");
            }
        }
    }
}
