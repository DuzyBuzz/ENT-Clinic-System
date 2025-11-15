using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers.ReportHelpers
{
    public partial class DateRangeSelectionForm : Form
    {
        public DateRangeSelectionForm()
        {
            InitializeComponent();
            dtpFrom.Value = DateTime.Now.Date.AddMonths(-1); // Default: 7 days ago
            dtpTo.Value = DateTime.Now.Date;               // Default: today
        }

        // Properties to access selected dates
        public DateTime DateFrom => dtpFrom.Value.Date;
        public DateTime DateTo => dtpTo.Value.Date;

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (DateFrom > DateTo)
            {
                MessageBox.Show("The 'From' date cannot be later than the 'To' date.",
                    "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
