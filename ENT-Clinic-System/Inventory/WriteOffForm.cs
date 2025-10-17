using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.Inventory
{
    public partial class WriteOffForm : Form
    {
        private readonly InventoryHelper _inventoryHelper;
        private int _itemId;

        public WriteOffForm(int itemId)
        {
            InitializeComponent();
            _inventoryHelper = new InventoryHelper();
            _itemId = itemId;

            LoadItemDetails();
        }

        private void LoadItemDetails()
        {
            try
            {
                string query = "SELECT brand_name, generic_name, strength, dosage, category, description, quantity FROM items WHERE item_id=@itemId";
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@itemId", _itemId);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblBrand.Text = reader["brand_name"].ToString();
                            lblGeneric.Text = reader["generic_name"].ToString();
                            lblStrength.Text = reader["strength"].ToString();
                            lblDosage.Text = reader["dosage"].ToString();
                            lblCategory.Text = reader["category"].ToString();
                            lblDescription.Text = reader["description"].ToString();
                            lblStockQty.Text = reader["quantity"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Item not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load item details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnAddWriteOff_Click(object sender, EventArgs e)
        {
            int quantity = (int)numericQuantity.Value;
            string reason = txtReason.Text.Trim();

            if (quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(reason))
            {
                MessageBox.Show("Please enter a reason for write-off.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReason.Focus();
                return;
            }

            if (quantity > int.Parse(lblStockQty.Text))
            {
                MessageBox.Show("Quantity cannot be greater than current stock.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericQuantity.Focus();
                return;
            }

            bool success = _inventoryHelper.AddWriteOff(_itemId, quantity, reason);

            if (success)
            {
                MessageBox.Show("Write-Off successfully recorded!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to record Write-Off. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
