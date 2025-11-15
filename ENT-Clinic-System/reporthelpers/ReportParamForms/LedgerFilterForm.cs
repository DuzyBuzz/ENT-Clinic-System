using System;
using System.Windows.Forms;
using System.Collections.Generic;
using ENT_Clinic_System.Helpers;

namespace ENT_Clinic_System.Helpers.ReportHelpers
{
    public partial class LedgerFilterForm : Form
    {
        public string SelectedConcessionaireCode => txtConcessionaire.Text.Trim();
        public DateTime FromDate => dtpFrom.Value.Date;
        public DateTime ToDate => dtpTo.Value.Date;

        public LedgerFilterForm()
        {
            InitializeComponent();
        }

        private void LedgerFilterForm_Load(object sender, EventArgs e)
        {
            // Use AutoCompleteHelper to fill suggestions
            AutoCompleteHelper.SetupAutoComplete(
                txtConcessionaire,
                "concessionaire",
                new List<string> { "concessionaire_code", "concessionaire_name" }
            );
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConcessionaire.Text))
            {
                MessageBox.Show("Please enter a concessionaire name or code.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpTo.Value.Date < dtpFrom.Value.Date)
            {
                MessageBox.Show("The 'To' date must be later than or equal to the 'From' date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
