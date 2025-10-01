using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Reports.ParamsForm
{
    public partial class SalesParamsForm : Form
    {
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }

        public SalesParamsForm()
        {
            InitializeComponent();
        }

        private void SalesParamsForm_Load(object sender, EventArgs e)
        {
            // Set default dates to today
            dtpFrom.Value = DateTime.Now.Date;
            dtpTo.Value = DateTime.Now.Date;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
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
