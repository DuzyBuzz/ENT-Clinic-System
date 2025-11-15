using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers.ReportHelpers
{
    public partial class DaySelectionForm : Form
    {
        public DaySelectionForm()
        {
            InitializeComponent();
            dtpDate.Value = DateTime.Now.Date; // sensible default: today
        }

        /// <summary>
        /// Selected date (Date component only).
        /// </summary>
        public DateTime SelectedDate => dtpDate.Value.Date;

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
