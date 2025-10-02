using ENT_Clinic_System.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ENT_Clinic_System.UI
{
    public partial class AutoCompleteManager : Form
    {
        private DGVCrudHelper crudHelper;

        public AutoCompleteManager()
        {
            InitializeComponent();
        }

        private void AutoCompleteManager_Load(object sender, EventArgs e)
        {
            try
            {
                // Define the table and columns you want to manage
                string tableName = "autocomplete_entries";
                string primaryKey = "id";
                List<string> columns = new List<string>
                {
                    "column_name",
                    "value"
                };

                // Initialize DGVCrudHelper
                crudHelper = new DGVCrudHelper(autoCompleteDataGridView, tableName, columns, primaryKey);

                // Optional: add a label to show page info

                // Load first page of data
                crudHelper.LoadData(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load autocomplete manager: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
