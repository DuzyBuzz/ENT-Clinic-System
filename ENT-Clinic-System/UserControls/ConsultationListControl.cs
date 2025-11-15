using ENT_Clinic_System.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.Consultation
{
    public partial class ConsultationListControl : UserControl
    {

        private DGVViewCrudHelper consultationCrud;
        public ConsultationListControl()
        {
            InitializeComponent();
            LoadConcessionaire();
        }

        private void LoadConcessionaire()
        {
            AutoCompleteHelper.SetupAutoComplete(
    searchTextBox,
    "v_consultation_details",
    new List<string> { "full_name" }
);
            try
            {
                // define searchable columns (these must exist in the VIEW)
                string[] searchableCols = { "full_name" };

                // initialize helper:
                // NOTE: first arg = DataGridView to bind
                //       second arg = VIEW name (used for SELECT / search / paging)
                //       third arg  = primary key column name (must be present in the VIEW)
                //       fourth arg = actual base table (used for UPDATE / DELETE)
                consultationCrud = new DGVViewCrudHelper(
                    consulationDGV,
                    "v_consultation_details",   // read from this view
                    "consultation_id",          // primary key (must exist in view)
                    "consultation"              // real table used for updates/deletes
                );

                // page size (server-side paging performed against the VIEW)
                consultationCrud.PageSize = 1500;

                // optional: set label that shows page info / search text
                consultationCrud.SetPageInfoLabel(pageLabel);
                dateFromPicker.Value = DateTime.Now.AddMonths(-1);

                // attach date range controls (the date column must exist in the VIEW)
                consultationCrud.AttachDateRangeControls(dateFromPicker, dateToPicker, searchDateButton, "consultation_date");

                // attach search box + buttons — searchable columns must exist in the VIEW
                consultationCrud.AttachSearchControls(
                    searchTextBox,
                    searchButton,
                    refreshButton,
                    searchableCols,    // pass the array we defined above
                    useFullText: false // set to true only if you created FULLTEXT index on these view/base-table columns
                );

                // initial load
                consultationCrud.LoadData();
                searchDateButton.PerformClick();
                // ✅ sort the DataView by latest billing date (DESC)
                if (consulationDGV.DataSource is DataTable dt)
                {
                    dt.DefaultView.Sort = "consultation_date DESC"; // latest first
                    consulationDGV.DataSource = dt;
                }

                // attach your header filter helper (works client-side on the loaded table)
                DGVColumnHeaderFilterHelper.Attach(consulationDGV);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load concessionaire list: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void consulationDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void searchDateButton_Click(object sender, EventArgs e)
        {

        }

        private void consulationDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateToPicker_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
