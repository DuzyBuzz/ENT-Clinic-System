using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ENT_Clinic_System.Reports.ParamsForm
{
    public partial class DispenseParamsForm : Form
    {
        public string SelectedPatient { get; private set; } = "";
        public string SelectedCategory { get; private set; } = "";
        public string SelectedItemName { get; private set; } = "";
        public string SelectedDescription { get; private set; } = "";
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }

        public DispenseParamsForm()
        {
            InitializeComponent();
        }

        private void DispenseParamsForm_Load(object sender, EventArgs e)
        {
            // Populate patient combobox
            ComboBoxCollectionHelper.PopulateComboBox(
                cmbPatient,
                "dispense_history",
                "patient_name"
            );
            AutoCompleteHelper.SetupAutoComplete(
                cmbPatient,
                "dispense_history",
                new List<string> { "patient_name" }
            );

            // Populate category combobox
            ComboBoxCollectionHelper.PopulateComboBox(
                cmbCategory,
                "dispense_history",
                "category"
            );
            AutoCompleteHelper.SetupAutoComplete(
                cmbCategory,
                "dispense_history",
                new List<string> { "category" }
            );

            // Populate item name combobox
            ComboBoxCollectionHelper.PopulateComboBox(
                cmbItemName,
                "dispense_history",
                "item_name"
            );
            AutoCompleteHelper.SetupAutoComplete(
                cmbItemName,
                "dispense_history",
                new List<string> { "item_name" }
            );

            // Populate description combobox
            ComboBoxCollectionHelper.PopulateComboBox(
                cmbDescription,
                "dispense_history",
                "description"
            );
            AutoCompleteHelper.SetupAutoComplete(
                cmbDescription,
                "dispense_history",
                new List<string> { "description" }
            );
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            SelectedPatient = cmbPatient.SelectedItem?.ToString() ?? cmbPatient.Text;
            SelectedCategory = cmbCategory.SelectedItem?.ToString() ?? cmbCategory.Text;
            SelectedItemName = cmbItemName.SelectedItem?.ToString() ?? cmbItemName.Text;
            SelectedDescription = cmbDescription.SelectedItem?.ToString() ?? cmbDescription.Text;

            FromDate = dtpFrom.Value.Date;
            ToDate = dtpTo.Value.Date;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
