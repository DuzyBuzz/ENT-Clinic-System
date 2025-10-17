using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
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
                if (_itemId <= 0)
                {
                    MessageBox.Show("Invalid item selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

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
                            lblBrand.Text = reader["brand_name"] != DBNull.Value ? reader["brand_name"].ToString() : "N/A";
                            lblGeneric.Text = reader["generic_name"] != DBNull.Value ? reader["generic_name"].ToString() : "N/A";
                            lblStrength.Text = reader["strength"] != DBNull.Value ? reader["strength"].ToString() : "N/A";
                            lblDosage.Text = reader["dosage"] != DBNull.Value ? reader["dosage"].ToString() : "N/A";
                            lblCategory.Text = reader["category"] != DBNull.Value ? reader["category"].ToString() : "N/A";
                            lblDescription.Text = reader["description"] != DBNull.Value ? reader["description"].ToString() : "N/A";
                            lblStockQty.Text = reader["quantity"] != DBNull.Value ? reader["quantity"].ToString() : "0";
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
                MessageBox.Show($"Error loading item details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnAddWriteOff_Click(object sender, EventArgs e)
        {
            try
            {
                int quantity = (int)numericQuantity.Value;
                string reason = txtReason.Text.Trim();

                if (quantity <= 0)
                {
                    MessageBox.Show("Quantity must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numericQuantity.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(reason))
                {
                    MessageBox.Show("Please enter a reason for write-off.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtReason.Focus();
                    return;
                }

                if (!int.TryParse(lblStockQty.Text, out int currentStock))
                {
                    MessageBox.Show("Current stock quantity is invalid.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (quantity > currentStock)
                {
                    MessageBox.Show("Quantity cannot exceed current stock.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numericQuantity.Focus();
                    return;
                }

                var confirm = MessageBox.Show(
                    $"Are you sure you want to write-off {quantity} unit(s) of:\n{lblBrand.Text} ({lblGeneric.Text})?",
                    "Confirm Write-Off",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm != DialogResult.Yes) return;

                bool success = _inventoryHelper.AddWriteOff(_itemId, quantity, reason);

                if (success)
                {
                    MessageBox.Show("Write-Off successfully recorded!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to record write-off. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
