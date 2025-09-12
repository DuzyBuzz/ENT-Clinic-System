using System;
using System.Data;
using System.Windows.Forms;
using ENT_Clinic_System.Helpers;

namespace ENT_Clinic_System.Inventory
{
    public partial class InventoryForm : Form
    {
        private readonly InventoryHelper _inventoryHelper;

        public InventoryForm()
        {
            InitializeComponent();

            _inventoryHelper = new InventoryHelper();

            LoadInventory();

            btnAddItem.Click += btnAddItem_Click;
            btnUpdateItem.Click += btnUpdateItem_Click;
            btnDeleteItem.Click += btnDeleteItem_Click;
            btnStockIn.Click += btnStockIn_Click;
            btnStockOut.Click += btnStockOut_Click;

            dgvItems.SelectionChanged += dgvItems_SelectionChanged;
        }

        private void LoadInventory()
        {
            try
            {
                DataTable dt = _inventoryHelper.GetAllItems();
                dgvItems.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtItemName.Text.Trim();
                string category = txtCategory.Text.Trim();
                decimal costPrice = decimal.Parse(txtCostPrice.Text.Trim());

                if (_inventoryHelper.AddItem(name, category, costPrice))
                {
                    MessageBox.Show("✅ Item added successfully!");
                    LoadInventory();
                }
                else
                {
                    MessageBox.Show("❌ Failed to add item.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message);
            }
        }

        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItems.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an item to update.");
                    return;
                }

                int itemId = Convert.ToInt32(dgvItems.SelectedRows[0].Cells["item_id"].Value);
                string name = txtItemName.Text.Trim();
                string category = txtCategory.Text.Trim();
                decimal costPrice = decimal.Parse(txtCostPrice.Text.Trim());

                if (_inventoryHelper.UpdateItem(itemId, name, category, costPrice))
                {
                    MessageBox.Show("✅ Item updated successfully!");
                    LoadInventory();
                }
                else
                {
                    MessageBox.Show("❌ Failed to update item.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating item: " + ex.Message);
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItems.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an item to delete.");
                    return;
                }

                int itemId = Convert.ToInt32(dgvItems.SelectedRows[0].Cells["item_id"].Value);

                if (_inventoryHelper.DeleteItem(itemId))
                {
                    MessageBox.Show("✅ Item deleted successfully!");
                    LoadInventory();
                }
                else
                {
                    MessageBox.Show("❌ Failed to delete item.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting item: " + ex.Message);
            }
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            try
            {
                int itemId = int.Parse(txtItemId.Text.Trim());
                int quantity = int.Parse(txtQuantity.Text.Trim());

                if (_inventoryHelper.AddStockMovement(itemId, "IN", quantity))
                {
                    MessageBox.Show("✅ Stock in successful!");
                    LoadInventory();
                }
                else
                {
                    MessageBox.Show("❌ Failed to stock in.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error stocking in: " + ex.Message);
            }
        }

        private void btnStockOut_Click(object sender, EventArgs e)
        {
            try
            {
                int itemId = int.Parse(txtItemId.Text.Trim());
                int quantity = int.Parse(txtQuantity.Text.Trim());
                decimal costPrice = decimal.Parse(txtCostPrice.Text.Trim());


                var priceDetails = _inventoryHelper.CalculateFinalPrice(costPrice, discountCheckBox.Checked, quantity);

                if (_inventoryHelper.AddStockMovement(itemId, "OUT", quantity))
                {
                    MessageBox.Show(
                        $"✅ Stock out successful!\n\n" +
                        $"Base: {priceDetails.BasePrice:C}\n" +
                        $"Discount: -{priceDetails.DiscountAmount:C}\n" +
                        $"Quantiry: -{quantity}\n" +
                        $"Tax: +{priceDetails.TaxAmount:C}\n" +
                        $"Final: {priceDetails.FinalPrice:C}");

                    LoadInventory();
                }
                else
                {
                    MessageBox.Show("❌ Failed to stock out.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error stocking out: " + ex.Message);
            }
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvItems.SelectedRows[0];
                txtItemId.Text = row.Cells["item_id"].Value.ToString();
                txtItemName.Text = row.Cells["item_name"].Value.ToString();
                txtCategory.Text = row.Cells["category"].Value.ToString();
                txtCostPrice.Text = row.Cells["cost_price"].Value.ToString();
                txtSellingPrice.Text = row.Cells["selling_price"].Value.ToString();

                // ❌ No need to auto-check discount checkbox (discount only applies during stock out)
                discountCheckBox.Checked = false;
            }
        }
    }
}
