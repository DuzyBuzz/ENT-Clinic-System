using ENT_Clinic_System.Helpers;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

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

        }

        private void LoadInventory()
        {
            ComboBoxCollectionHelper.PopulateComboBox(categoryCombobox, "items", "category");
            ComboBoxCollectionHelper.PopulateComboBox(addItemNameComboBox, "items", "item_name");
            ComboBoxCollectionHelper.PopulateComboBox(addDescriptionComboBox, "items", "description");
            ComboBoxCollectionHelper.PopulateComboBox(addCategoryComboBox, "items", "category");
            try
            {
                DataTable dt = _inventoryHelper.GetAllItems();
                dgvItems.DataSource = dt;
                dgvItems.Columns["created_at"].Visible = false;
                dgvItems.Columns["updated_at"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string name = addItemNameComboBox.Text.Trim();
            string description = addDescriptionComboBox.Text.Trim();
            string category = addCategoryComboBox.Text.Trim();
            decimal costPrice = decimal.Parse(costPriceTextBox.Text.Trim());
            decimal sellingPrice = decimal.Parse(sellingPriceTextBox.Text.Trim());


            string[] columns = { "item_name", "description", "category" };
            object[] values = { name, description, category };

            bool exists = UniqueHelper.Exists("items", columns, values);

            if (exists)
            {
                MessageBox.Show("This item already exists!");
            }
            else
            {
                try
                {


                    if (_inventoryHelper.AddItem(name, description, category, costPrice, sellingPrice))
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
                string name = addItemNameComboBox.Text.Trim();
                string description = addDescriptionComboBox.Text.Trim();
                string category = addCategoryComboBox.Text.Trim();
                decimal costPrice = decimal.Parse(costPriceTextBox.Text.Trim());
                decimal sellingPrice = decimal.Parse(sellingPriceTextBox.Text.Trim());

                if (_inventoryHelper.UpdateItem(itemId, name,description, category, costPrice, sellingPrice))
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
                int itemId = int.Parse(itemIdTextBox.Text.Trim());
                int quantity = int.Parse(quantityTextBox.Text.Trim());
                DateTime expirationDate = expirationDateTimePicker.Value;

                bool discount = false; // Usually stock IN does not apply discount
                bool hasExpiration = expirationDateCheckBox.Checked;

                if (_inventoryHelper.AddStockMovement(itemId, "IN", quantity, expirationDate, discount, hasExpiration))
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
                int itemId = int.Parse(itemIdTextBox.Text.Trim());
                int quantity = int.Parse(quantityTextBox.Text.Trim());

                bool hasExpiration = false; // Stock OUT usually does not require expiration
                DateTime expirationDate = DateTime.Now; // Placeholder, won’t be used
                bool applyDiscount = discountCheckBox.Checked;

                if (_inventoryHelper.AddStockMovement(itemId, "OUT", quantity, expirationDate, applyDiscount, hasExpiration))
                {
                    MessageBox.Show("✅ Stock out successful!");
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



        private void dgvItems_Selected(object sender, EventArgs e)
        {

        }

        private void btnStockOut_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAddItem_Click_1(object sender, EventArgs e)
        {

        }

        private void searchPatientButton_Click(object sender, EventArgs e)
        {
            SearchHelper.Search(
                dgv: dgvItems,
                tableName: "items",
                columnNames: new string[] { "item_name", "description" },
                filterControl: searchItemsTextBox
            );


        }

        private void refreshPatientsButton_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void categoryCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchHelper.Search(
            dgv: dgvItems,
            tableName: "items",
            columnNames: new string[] { "category"},
            filterControl: categoryCombobox
        );
        }

        private void addItemNameComboBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void addDescriptionComboBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void addCategoryComboBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            AutoCompleteHelper.SetupAutoComplete(addItemNameComboBox, "items", new List<string> { "item_name" });
            AutoCompleteHelper.SetupAutoComplete(addDescriptionComboBox, "items", new List<string> { "description" });
            AutoCompleteHelper.SetupAutoComplete(addCategoryComboBox, "items", new List<string> { "category" });
        }

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvItems.SelectedRows[0];
                addItemNameComboBox.Text = row.Cells["item_name"].Value.ToString();
                addDescriptionComboBox.Text = row.Cells["description"].Value.ToString();
                addCategoryComboBox.Text = row.Cells["category"].Value.ToString();
                costPriceTextBox.Text = row.Cells["cost_price"].Value.ToString();
                sellingPriceTextBox.Text = row.Cells["selling_price"].Value.ToString();

                /// stock in load
                itemIdTextBox.Text = row.Cells["item_id"].Value.ToString();


                // ❌ No need to auto-check discount checkbox (discount only applies during stock out)
                discountCheckBox.Checked = false;
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            addItemNameComboBox.Text = "";
            addDescriptionComboBox.Text = "";
            addCategoryComboBox.Text = "";
            costPriceTextBox.Text = "";
            sellingPriceTextBox.Text = "";
        }

        private void expirationDateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // If checkbox is checked, enable the DateTimePicker
            // If checkbox is unchecked, disable it
            expirationDateTimePicker.Enabled = expirationDateCheckBox.Checked;

            // Optional: Reset the value to null if unchecked
            if (!expirationDateCheckBox.Checked)
            {

                expirationDateTimePicker.Checked = false; // or keep a default value
            }
        }

    }
}
