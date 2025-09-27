using ENT_Clinic_System.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.Inventory
{
    public partial class InventoryForm : Form
    {
        private readonly InventoryHelper _inventoryHelper;
        private DGVCrudHelper movementCrud;

        public InventoryForm()
        {
            InitializeComponent();
            _inventoryHelper = new InventoryHelper();
            LoadInventory();
        }

        // ==========================
        // Load Inventory and ComboBoxes
        // ==========================
        private void LoadInventory()
        {
            try
            {
                // Populate combo boxes
                ComboBoxCollectionHelper.PopulateComboBox(categoryCombobox, "items", "category");
                ComboBoxCollectionHelper.PopulateComboBox(addItemNameComboBox, "items", "item_name");
                ComboBoxCollectionHelper.PopulateComboBox(addDescriptionComboBox, "items", "description");
                ComboBoxCollectionHelper.PopulateComboBox(addCategoryComboBox, "items", "category");

                // Load inventory DataGridView
                DataTable dt = _inventoryHelper.GetAllItems();
                dgvItems.DataSource = dt;

                // Hide system columns
                if (dgvItems.Columns.Contains("created_at")) dgvItems.Columns["created_at"].Visible = false;
                if (dgvItems.Columns.Contains("updated_at")) dgvItems.Columns["updated_at"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================
        // Add New Item
        // ==========================
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Trim and convert text to camel case
                string name = CamelCaseHelper.ToCamelCase(addItemNameComboBox.Text.Trim());
                string description = CamelCaseHelper.ToCamelCase(addDescriptionComboBox.Text.Trim());
                string category = CamelCaseHelper.ToCamelCase(addCategoryComboBox.Text.Trim());

                // Validate inputs
                if (string.IsNullOrWhiteSpace(name)) { ShowValidationError("Item name cannot be empty.", addItemNameComboBox); return; }
                if (string.IsNullOrWhiteSpace(description)) { ShowValidationError("Description cannot be empty.", addDescriptionComboBox); return; }
                if (string.IsNullOrWhiteSpace(category)) { ShowValidationError("Category cannot be empty.", addCategoryComboBox); return; }

                // Validate numeric values (already using NumericUpDown)
                decimal costPrice = costPriceNumericUpDown.Value;
                decimal sellingPrice = sellingNumericUpDown.Value;

                if (costPrice < 0 || sellingPrice < 0)
                {
                    MessageBox.Show("Prices cannot be negative.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check for duplicate
                string[] columns = { "item_name", "description", "category" };
                object[] values = { name, description, category };
                if (UniqueHelper.Exists("items", columns, values))
                {
                    MessageBox.Show("This item already exists!", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Add item to database
                bool success = _inventoryHelper.AddItem(name, description, category, costPrice, sellingPrice);
                if (success)
                {
                    MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventory();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Failed to add item. Check your database connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================
        // Update Item
        // ==========================
        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItems.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an item to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int itemId = Convert.ToInt32(dgvItems.SelectedRows[0].Cells["item_id"].Value);

                string name = CamelCaseHelper.ToCamelCase(addItemNameComboBox.Text.Trim());
                string description = CamelCaseHelper.ToCamelCase(addDescriptionComboBox.Text.Trim());
                string category = CamelCaseHelper.ToCamelCase(addCategoryComboBox.Text.Trim());

                decimal costPrice = costPriceNumericUpDown.Value;
                decimal sellingPrice = sellingNumericUpDown.Value;

                if (_inventoryHelper.UpdateItem(itemId, name, description, category, costPrice, sellingPrice))
                {
                    MessageBox.Show("Item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventory();
                }
                else
                {
                    MessageBox.Show("Failed to update item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating item: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================
        // Delete Item
        // ==========================
        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItems.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an item to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int itemId = Convert.ToInt32(dgvItems.SelectedRows[0].Cells["item_id"].Value);

                if (_inventoryHelper.DeleteItem(itemId))
                {
                    MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventory();
                }
                else
                {
                    MessageBox.Show("Failed to delete item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting item: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================
        // Stock In
        // ==========================
        private void btnStockIn_Click(object sender, EventArgs e)
        {
            try
            {
                // ==========================
                // 1️⃣ Validate Item ID
                // ==========================
                if (string.IsNullOrWhiteSpace(itemIdTextBox.Text))
                {
                    MessageBox.Show("Please select an item first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    itemIdTextBox.Focus();
                    return;
                }

                if (!int.TryParse(itemIdTextBox.Text.Trim(), out int itemId))
                {
                    MessageBox.Show("Invalid Item ID. Please select a valid item.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    itemIdTextBox.Focus();
                    return;
                }

                // ==========================
                // 2️⃣ Validate Quantity
                // ==========================
                int quantity = (int)quantityNumericUpDown.Value;
                if (quantity <= 0)
                {
                    MessageBox.Show("Quantity must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    quantityNumericUpDown.Focus();
                    return;
                }

                // ==========================
                // 3️⃣ Validate Expiration Date (if required)
                // ==========================
                DateTime expirationDate = expirationDateTimePicker.Value;
                bool hasExpiration = expirationDateCheckBox.Checked;

                if (hasExpiration)
                {
                    if (expirationDate < DateTime.Today)
                    {
                        MessageBox.Show("Expiration date cannot be in the past.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        expirationDateTimePicker.Focus();
                        return;
                    }
                }

                bool discount = false; // Stock IN usually does not apply discount

                // ==========================
                // 4️⃣ Attempt to Add Stock Movement
                // ==========================
                bool success = _inventoryHelper.AddStockMovement(itemId, "IN", quantity, expirationDate, discount, hasExpiration);

                if (success)
                {
                    MessageBox.Show("Stock in successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventory();
                    SortMovementDate();
                }
                else
                {
                    MessageBox.Show("Failed to stock in. Please check your database connection or input values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error during stock in: " + ex.Message, "Stock In Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ==========================
        // Stock Out
        // ==========================
        private void btnStockOut_Click(object sender, EventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(itemIdTextBox.Text.Trim());
                int quantity = (int)quantityNumericUpDown.Value;
                bool hasExpiration = false; // Stock out usually doesn't require expiration
                DateTime expirationDate = DateTime.Now; // Placeholder
                bool applyDiscount = discountCheckBox.Checked;

                bool success = _inventoryHelper.AddStockMovement(itemId, "OUT", quantity, expirationDate, applyDiscount, hasExpiration);
                if (success)
                {
                    MessageBox.Show(" Stock out successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventory();
                }
                else
                {
                    MessageBox.Show("Failed to stock out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error stocking out: " + ex.Message, "Stock Out Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================
        // DataGridView Item Selection
        // ==========================
        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvItems.SelectedRows[0];
            addItemNameComboBox.Text = row.Cells["item_name"].Value.ToString();
            addDescriptionComboBox.Text = row.Cells["description"].Value.ToString();
            addCategoryComboBox.Text = row.Cells["category"].Value.ToString();
            costPriceNumericUpDown.Value = Convert.ToDecimal(row.Cells["cost_price"].Value);
            sellingNumericUpDown.Value = Convert.ToDecimal(row.Cells["selling_price"].Value);

            // Stock in fields
            itemIdTextBox.Text = row.Cells["item_id"].Value.ToString();
            discountCheckBox.Checked = false; // Reset discount
        }

        // ==========================
        // Clear Inputs
        // ==========================
        private void clearButton_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            addItemNameComboBox.Text = "";
            addDescriptionComboBox.Text = "";
            addCategoryComboBox.Text = "";
            costPriceNumericUpDown.Value = 0;
            sellingNumericUpDown.Value = 0;
        }

        private void ShowValidationError(string message, Control controlToFocus)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            controlToFocus.Focus();
        }

        // ==========================
        // Expiration Checkbox
        // ==========================
        private void expirationDateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            expirationDateTimePicker.Enabled = expirationDateCheckBox.Checked;
            if (!expirationDateCheckBox.Checked)
            {
                expirationDateTimePicker.Checked = false;
            }
        }

        // ==========================
        // Search and Filtering
        // ==========================
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
                columnNames: new string[] { "category" },
                filterControl: categoryCombobox
            );
        }

        // ==========================
        // Form Load & Movements
        // ==========================
        private void InventoryForm_Load(object sender, EventArgs e)
        {
            LoadMovements();

            // Setup auto-complete
            AutoCompleteHelper.SetupAutoComplete(addItemNameComboBox, "items", new List<string> { "item_name" });
            AutoCompleteHelper.SetupAutoComplete(addDescriptionComboBox, "items", new List<string> { "description" });
            AutoCompleteHelper.SetupAutoComplete(addCategoryComboBox, "items", new List<string> { "category" });

            movementDateFromDateTimePicker.Value = DateTime.Now.AddMonths(-1);
            SortMovementDate();
        }

        private void LoadMovements()
        {
            try
            {
                List<string> columns = new List<string>
                {
                    "movement_id",
                    "item_id",
                    "movement_type",
                    "quantity",
                    "movement_date",
                    "expiration_date",
                };

                if (movementCrud == null)
                    movementCrud = new DGVCrudHelper(movementDataGridView, "stock_movements", columns, "movement_id");

                if (movementDataGridView.Columns.Contains("movement_id"))
                    movementDataGridView.Columns["movement_id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load movements: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void movementDateFromDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SortMovementDate();
        }

        private void movementDateToDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SortMovementDate();
        }

        private void SortMovementDate()
        {
            if (movementDateFromDateTimePicker.Value <= movementDateToDateTimePicker.Value)
            {
                SearchHelper.Search(
                    dgv: movementDataGridView,
                    tableName: "stock_movements",
                    columnNames: new string[] { "movement_date" },
                    fromDate: movementDateFromDateTimePicker.Value,
                    toDate: movementDateToDateTimePicker.Value,
                    columns: new string[]
                    {
                        "movement_id",
                        "item_id",
                        "movement_type",
                        "quantity",
                        "movement_date",
                        "expiration_date"
                    }
                );
            }
            else
            {
                MessageBox.Show("Start date must be earlier than end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void quantityNumericUpDown_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Prevent typing the decimal point
            if (e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == '-')
            {
                e.Handled = true; // ignore input
                MessageBox.Show("Decimal values are not allowed. Please enter a whole number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
