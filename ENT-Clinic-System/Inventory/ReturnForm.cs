using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Inventory
{
    public partial class ReturnForm : Form
    {
        public ReturnForm()
        {
            InitializeComponent();
        }

        // Event handler for Return Item button
        private void btnReturn_Click(object sender, EventArgs e)
        {
            // Simple validation
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                MessageBox.Show("Please enter the item name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Add returned item to DataGridView
            dgvReturns.Rows.Add(txtItemName.Text, quantity, DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

            // Clear input fields
            txtItemName.Clear();
            txtQuantity.Clear();
        }

        // Event handler for Cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
