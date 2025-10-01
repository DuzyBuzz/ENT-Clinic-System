using ENT_Clinic_System.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ENT_Clinic_System.Reports.ParamsForm
{
    public partial class BillingParamsForm : Form
    {
        public string SelectedPatient { get; private set; } = "";
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }

        public BillingParamsForm()
        {
            InitializeComponent();
        }

        private void BillingParamsForm_Load(object sender, EventArgs e)
        {
            // Populate patient combobox
            ComboBoxCollectionHelper.PopulateComboBox(
                cmbPatient,
                "billing_report",
                "patient_name"
            );

            AutoCompleteHelper.SetupAutoComplete(
                cmbPatient,
                "billing_report",
                new List<string> { "patient_name" }
            );
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            SelectedPatient = cmbPatient.SelectedItem?.ToString() ?? cmbPatient.Text;
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
