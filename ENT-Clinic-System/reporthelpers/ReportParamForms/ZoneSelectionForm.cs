using System;
using System.Windows.Forms;
using ENT_Clinic_System.Helpers;

namespace ENT_Clinic_System.ReportHelpers
{
    public partial class ZoneSelectionForm : Form
    {
        public string SelectedZone { get; private set; }

        public ZoneSelectionForm()
        {
            InitializeComponent();
        }

        private void ZoneSelectionForm_Load(object sender, EventArgs e)
        {
            // Populate ComboBox with zone values from your database
            ComboBoxCollectionHelper.PopulateComboBox(comboBoxZone, "zone", "zone_id");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (comboBoxZone.SelectedItem == null)
            {
                MessageBox.Show("Please select a zone before continuing.",
                                "Zone Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedZone = comboBoxZone.SelectedItem.ToString();
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
