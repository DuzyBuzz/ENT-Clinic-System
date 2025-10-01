using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Reports.ParamsForm
{
    public partial class RevenueParamsForm : Form
    {
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }
        public string RevenueType { get; private set; } = "All";

        public RevenueParamsForm()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            FromDate = dtpFrom.Value.Date;
            ToDate = dtpTo.Value.Date;
            RevenueType = cmbRevenueType.SelectedItem?.ToString() ?? "All";

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void RevenueParamsForm_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now.Date;
            dtpTo.Value = DateTime.Now.Date;

            cmbRevenueType.Items.Add("All");
            cmbRevenueType.Items.Add("Billing");
            cmbRevenueType.Items.Add("Sales");
            cmbRevenueType.SelectedIndex = 0;
        }
    }
}
