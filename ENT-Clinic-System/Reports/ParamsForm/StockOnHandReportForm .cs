using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ENT_Clinic_System.Reports.ParamsForm
{
    public partial class StockOnHandReportForm : Form
    {
        public string SelectedCategory { get; private set; } = "";
        public DateTime AsOfDate { get; private set; } = DateTime.Now;

        public StockOnHandReportForm()
        {
            InitializeComponent();
            dtpAsOfDate.Value = DateTime.Now;
        }



        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // Assign selected values
            SelectedCategory = cmbCategory.SelectedItem?.ToString() ?? "";
            if (SelectedCategory == "All")
                SelectedCategory = ""; // match report logic

            AsOfDate = dtpAsOfDate.Value.Date;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void StockOnHandReportForm_Load(object sender, EventArgs e)
        {
            // Populate combobox items from the same column
            ComboBoxCollectionHelper.PopulateComboBox(
                cmbCategory,
                "stock_overview",
                "category"
            );
        }
    }
}
