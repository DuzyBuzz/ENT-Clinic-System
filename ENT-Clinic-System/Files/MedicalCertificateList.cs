using ENT_Clinic_System.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.Files
{
    public partial class MedicalCertificateList : UserControl
    {
        private DGVViewCrudHelper patientCrud;
        public MedicalCertificateList()
        {
            InitializeComponent();
            LoadMedicalCert();
        }
        private void LoadMedicalCert()
        {
            AutoCompleteHelper.SetupAutoComplete(
                searchPatientNameTextBox,
                "v_medical_certificate_details",
                new List<string> { "patient_name" }
            );


            try
            {
                // Define searchable columns
                string[] searchableCols = { "patient_name" };
                patientCrud = new DGVViewCrudHelper(
                    patientsDataGridView,
                    "v_medical_certificate_details",       // View or table name (used for SELECT)
                    "medical_certificate_id",    // Primary key column
                    "issued_medical_certificate"        // Base table name for UPDATE/DELETE
                );


                patientCrud.PageSize = 2000;
                patientCrud.SetPageInfoLabel(pageLabel);
                patientCrud.AttachSearchControls(searchPatientNameTextBox, searchPatientButton, refreshPatientsButton, searchableCols);

                // Initial data load
                patientCrud.LoadData();

                // Sort by concessionaire_code ASC
                if (patientsDataGridView.DataSource is DataTable dt)
                {
                    dt.DefaultView.Sort = "created_at DESC";
                    patientsDataGridView.DataSource = dt;
                }

                UpdatePaginationButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load concessionaire list:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


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

    }
}
