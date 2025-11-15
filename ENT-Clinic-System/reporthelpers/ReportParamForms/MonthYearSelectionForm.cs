using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers.ReportHelpers
{
    public partial class MonthYearSelectionForm : Form
    {
        public MonthYearSelectionForm()
        {
            InitializeComponent();
            numYear.Value = DateTime.Now.Year;
            cboMonth.SelectedIndex = DateTime.Now.Month - 1;
        }

        public int SelectedMonth => cboMonth.SelectedIndex + 1;
        public int SelectedYear => (int)numYear.Value;

        private void btnOK_Click(object sender, EventArgs e)
        {
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
