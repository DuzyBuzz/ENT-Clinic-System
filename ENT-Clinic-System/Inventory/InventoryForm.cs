using ENT_Clinic_System.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.Inventory
{
    public partial class InventoryForm : UserControl
    {
        private readonly InventoryHelper _inventoryHelper;
        private bool _isProcessingStockIn = false; // Prevent double stock in
        private ContextMenuStrip movementContextMenu;
        private ContextMenuStrip itemContextMenu;


        public InventoryForm()
        {
            InitializeComponent();
            _inventoryHelper = new InventoryHelper();
            LoadInventory();
            SetupMovementContextMenu();
            SetupItemContextMenu();
        }

        // ==========================
        // Load Inventory and ComboBoxes
        // ==========================
        private void LoadInventory()
        {
            DGVColumnHeaderFilterHelper.ResetFilters(movementDataGridView);
            try
            {
                // Populate combo boxes from database
                ComboBoxCollectionHelper.PopulateComboBox(genericNameComboBox, "items", "generic_name");
                ComboBoxCollectionHelper.PopulateComboBox(brandNameComboBox, "items", "brand_name");

                ComboBoxCollectionHelper.PopulateComboBox(stregnthComboBox, "items", "strength");
                ComboBoxCollectionHelper.PopulateComboBox(dosageComboBox, "items", "dosage");
                ComboBoxCollectionHelper.PopulateComboBox(categoryComboBox, "items", "category");
                ComboBoxCollectionHelper.PopulateComboBox(sortCategoryCombobox, "items", "category");

                // Load inventory DataGridView
                DataTable dt = _inventoryHelper.GetAllItems();
                dgvItems.DataSource = dt;

                // Hide internal IDs
                if (dgvItems.Columns.Contains("item_id")) dgvItems.Columns["item_id"].Visible = false;
                if (dgvItems.Columns.Contains("created_at")) dgvItems.Columns["created_at"].Visible = false;
                if (dgvItems.Columns.Contains("updated_at")) dgvItems.Columns["updated_at"].Visible = false;

                // Professional column headers
                if (dgvItems.Columns.Contains("generic_name")) dgvItems.Columns["generic_name"].HeaderText = "Generic Name";

                if (dgvItems.Columns.Contains("brand_name")) dgvItems.Columns["brand_name"].HeaderText = "Brand Name";
                if (dgvItems.Columns.Contains("strength")) dgvItems.Columns["strength"].HeaderText = "Strength";
                if (dgvItems.Columns.Contains("dosage")) dgvItems.Columns["dosage"].HeaderText = "Dosage";
                if (dgvItems.Columns.Contains("category")) dgvItems.Columns["category"].HeaderText = "Category";
                if (dgvItems.Columns.Contains("cost_price")) dgvItems.Columns["cost_price"].HeaderText = "Cost Price";
                if (dgvItems.Columns.Contains("selling_price")) dgvItems.Columns["selling_price"].HeaderText = "Selling Price";
                if (dgvItems.Columns.Contains("quantity")) dgvItems.Columns["quantity"].HeaderText = "Stock Qty";
                ClearInputs();
                DGVColumnHeaderFilterHelper.ResetFilters(dgvItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SetupItemContextMenu()
        {
            itemContextMenu = new ContextMenuStrip();

            var deleteItem = new ToolStripMenuItem("Delete Item")
            {
                ForeColor = System.Drawing.Color.Red
            };
            deleteItem.Click += DeleteItemMenu_Click;

            itemContextMenu.Items.Add(deleteItem);

            // Show menu on right-click
            dgvItems.MouseDown += DgvItems_MouseDown;
        }
        private void DgvItems_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dgvItems.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    dgvItems.ClearSelection();
                    dgvItems.Rows[hit.RowIndex].Selected = true;
                    itemContextMenu.Show(dgvItems, e.Location);
                }
            }
        }
        private void DeleteItemMenu_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0) return;

            int itemId = Convert.ToInt32(dgvItems.SelectedRows[0].Cells["item_id"].Value);
            string brand = dgvItems.SelectedRows[0].Cells["brand_name"].Value.ToString();
            string generic = dgvItems.SelectedRows[0].Cells["generic_name"].Value.ToString();

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete the item:\n{brand} ({generic})?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                bool success = _inventoryHelper.DeleteItem(itemId);
                if (success)
                {
                    MessageBox.Show("Item deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventory();
                    LoadMovements(); // optional: refresh related movements
                }
                else
                {
                    MessageBox.Show("Failed to delete item.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // ==========================
        // Setup Context Menu for Movements
        // ==========================
        private void SetupMovementContextMenu()
        {
            movementContextMenu = new ContextMenuStrip();

            // Delete movement menu item
            var deleteItem = new ToolStripMenuItem("Delete Movement")
            {
                ForeColor = System.Drawing.Color.Red
            };
            deleteItem.Click += DeleteMovementItem_Click; // Assign proper handler

            movementContextMenu.Items.Add(deleteItem);

            // Show context menu on right-click
            movementDataGridView.MouseDown += MovementDataGridView_MouseDown;
        }

        private void MovementDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = movementDataGridView.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    // Select the row under the mouse
                    movementDataGridView.ClearSelection();
                    movementDataGridView.Rows[hit.RowIndex].Selected = true;

                    // Show context menu at cursor position
                    movementContextMenu.Show(movementDataGridView, e.Location);
                }
            }
        }

        // ==========================
        // Delete Movement Handler
        // ==========================
        private void DeleteMovementItem_Click(object sender, EventArgs e)
        {
            if (movementDataGridView.SelectedRows.Count == 0) return;

            int movementId = Convert.ToInt32(movementDataGridView.SelectedRows[0].Cells["movement_id"].Value);

            var confirm = MessageBox.Show("Are you sure you want to delete this movement?",
                                          "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                bool success = _inventoryHelper.DeleteStockMovement(movementId, UserCredentials.UserId); // Implement in InventoryHelper
                if (success)
                {
                    MessageBox.Show("Movement deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMovements();
                    LoadInventory();
                }
                else
                {
                    MessageBox.Show("Failed to delete movement.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==========================
        // Add, Update, Delete Item
        // ==========================
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                string brandName = FirstLetterUpperHelper.ToFirstUpper(brandNameComboBox.Text);
                string genericName = FirstLetterUpperHelper.ToFirstUpper(genericNameComboBox.Text);
                string strength = FirstLetterUpperHelper.ToFirstUpper(stregnthComboBox.Text);
                string dosage = FirstLetterUpperHelper.ToFirstUpper(dosageComboBox.Text);
                string category = FirstLetterUpperHelper.ToFirstUpper(categoryComboBox.Text);

                if (string.IsNullOrWhiteSpace(brandName)) { ShowValidationError("Brand Name cannot be empty.", brandNameComboBox); return; }
                if (string.IsNullOrWhiteSpace(genericName)) { ShowValidationError("Generic Name cannot be empty.", genericNameComboBox); return; }
                if (string.IsNullOrWhiteSpace(strength)) { ShowValidationError("Strength cannot be empty.", stregnthComboBox); return; }
                if (string.IsNullOrWhiteSpace(dosage)) { ShowValidationError("Dosage cannot be empty.", dosageComboBox); return; }
                if (string.IsNullOrWhiteSpace(category)) { ShowValidationError("Category cannot be empty.", categoryComboBox); return; }

                decimal costPrice = costPriceNumericUpDown.Value;
                decimal sellingPrice = sellingNumericUpDown.Value;

                if (costPrice < 0 || sellingPrice < 0)
                {
                    MessageBox.Show("Prices cannot be negative.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string[] columns = { "brand_name", "generic_name", "strength", "dosage" };
                object[] values = { brandName, genericName, strength, dosage };
                if (UniqueHelper.Exists("items", columns, values))
                {
                    MessageBox.Show("This item already exists!", "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool success = _inventoryHelper.AddItem(brandName, genericName, strength, dosage, category, costPrice, sellingPrice);
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

                string brandName = FirstLetterUpperHelper.ToFirstUpper(brandNameComboBox.Text);
                string genericName = FirstLetterUpperHelper.ToFirstUpper(genericNameComboBox.Text);
                string strength = FirstLetterUpperHelper.ToFirstUpper(stregnthComboBox.Text);
                string dosage = FirstLetterUpperHelper.ToFirstUpper(dosageComboBox.Text);
                string category = FirstLetterUpperHelper.ToFirstUpper(categoryComboBox.Text);
                decimal costPrice = costPriceNumericUpDown.Value;
                decimal sellingPrice = sellingNumericUpDown.Value;

                // ✅ Ask for confirmation before updating
                var confirm = MessageBox.Show(
                    "Are you sure you want to update the selected item?",
                    "Confirm Update",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirm != DialogResult.Yes)
                    return;

                bool success = _inventoryHelper.UpdateItem(itemId, brandName, genericName, strength, dosage, category, costPrice, sellingPrice);
                if (success)
                {
                    MessageBox.Show("Item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventory();
                    LoadMovements();
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
        private async void btnStockIn_Click(object sender, EventArgs e)
        {
            if (_isProcessingStockIn) return; // prevent double click
            _isProcessingStockIn = true;

            try
            {
                if (!int.TryParse(itemIdTextBox.Text.Trim(), out int itemId))
                {
                    MessageBox.Show("Please select a valid item first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int quantity = (int)quantityNumericUpDown.Value;
                if (quantity <= 0)
                {
                    MessageBox.Show("Quantity must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime expirationDate = expirationDateTimePicker.Value;
                bool hasExpiration = expirationDateCheckBox.Checked;

                if (hasExpiration && expirationDate < DateTime.Today)
                {
                    MessageBox.Show("Expiration date cannot be in the past.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Stock in asynchronously
                bool success = await Task.Run(() => _inventoryHelper.AddStockMovement(itemId, "IN", quantity, expirationDate, hasExpiration));

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
            finally
            {
                _isProcessingStockIn = false;
            }
        }

        // ==========================
        // DataGridView Item Selection
        // ==========================
        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgvItems.SelectedRows[0];

            brandNameComboBox.Text = row.Cells["brand_name"].Value.ToString();
            genericNameComboBox.Text = row.Cells["generic_name"].Value.ToString();
            stregnthComboBox.Text = row.Cells["strength"].Value.ToString();
            dosageComboBox.Text = row.Cells["dosage"].Value.ToString();
            categoryComboBox.Text = row.Cells["category"].Value.ToString();
            costPriceNumericUpDown.Value = Convert.ToDecimal(row.Cells["cost_price"].Value);
            sellingNumericUpDown.Value = Convert.ToDecimal(row.Cells["selling_price"].Value);

            itemIdTextBox.Text = row.Cells["item_id"].Value.ToString();
            discountCheckBox.Checked = false;
        }

        // ==========================
        // Clear Inputs
        // ==========================
        private void clearButton_Click(object sender, EventArgs e) => ClearInputs();
        private void ClearInputs()
        {
            brandNameComboBox.Text = "";
            genericNameComboBox.Text = "";
            stregnthComboBox.Text = "";
            dosageComboBox.Text = "";
            categoryComboBox.Text = "";
            costPriceNumericUpDown.Value = 0;
            sellingNumericUpDown.Value = 0;
            itemIdTextBox.Text = "";
            quantityNumericUpDown.Value = 0;
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
                expirationDateTimePicker.Checked = false;
        }

        // ==========================
        // Search and Filtering
        // ==========================
        private void searchPatientButton_Click(object sender, EventArgs e)
        {
            SearchHelper.Search(
                dgv: dgvItems,
                tableName: "items",
                columnNames: new string[] { "generic_name", "brand_name",  "strength", "dosage", "category"},
                filterControl: searchItemsTextBox
            );
        }

        private void refreshPatientsButton_Click(object sender, EventArgs e) => LoadInventory();

        private void categoryCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchHelper.Search(
                dgv: dgvItems,
                tableName: "items",
                columnNames: new string[] { "category" },
                filterControl: sortCategoryCombobox
            );
            if (dgvItems.Columns.Contains("status")) dgvItems.Columns["status"].Visible = false;
            if (dgvItems.Columns.Contains("item_id")) dgvItems.Columns["item_id"].Visible = false;
        }

        // ==========================
        // Form Load & Movements
        // ==========================
        private void InventoryForm_Load(object sender, EventArgs e)
        {
            LoadMovements();

            // Auto-complete
            AutoCompleteHelper.SetupAutoComplete(brandNameComboBox, "items", new List<string> { "brand_name" });
            AutoCompleteHelper.SetupAutoComplete(genericNameComboBox, "items", new List<string> { "generic_name" });
            AutoCompleteHelper.SetupAutoComplete(stregnthComboBox, "items", new List<string> { "strength" });
            AutoCompleteHelper.SetupAutoComplete(dosageComboBox, "items", new List<string> { "dosage" });
            AutoCompleteHelper.SetupAutoComplete(categoryComboBox, "items", new List<string> { "category" });
            AutoCompleteHelper.SetupAutoComplete(searchItemsTextBox, "items", new List<string> { "generic_name", "brand_name" });
            movementDateFromDateTimePicker.Value = DateTime.Now.AddMonths(-1);
            SortMovementDate();

            // Attach to any DataGridView
            DGVColumnHeaderFilterHelper.Attach(movementDataGridView);
            DGVColumnHeaderFilterHelper.Attach(dgvItems);


        }


        private void LoadMovements()
        {
            try
            {
                string query = @"
                    SELECT sm.movement_id, sm.item_id, i.brand_name, i.generic_name, i.strength, i.dosage, i.category,
                           sm.movement_type, sm.quantity, sm.movement_date, sm.expiration_date
                    FROM stock_movements sm
                    INNER JOIN items i ON sm.item_id = i.item_id
                    ORDER BY sm.movement_date DESC";

                DataTable dt = new DataTable();
                using (var conn = DBConfig.GetConnection())
                using (var adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(query, conn))
                {
                    adapter.Fill(dt);
                }

                movementDataGridView.DataSource = dt;

                // Hide internal IDs
                if (movementDataGridView.Columns.Contains("movement_id")) movementDataGridView.Columns["movement_id"].Visible = false;
                if (movementDataGridView.Columns.Contains("item_id")) movementDataGridView.Columns["item_id"].Visible = false;

                // Professional headers
                if (movementDataGridView.Columns.Contains("brand_name")) movementDataGridView.Columns["brand_name"].HeaderText = "Brand Name";
                if (movementDataGridView.Columns.Contains("generic_name")) movementDataGridView.Columns["generic_name"].HeaderText = "Generic Name";
                if (movementDataGridView.Columns.Contains("strength")) movementDataGridView.Columns["strength"].HeaderText = "Strength";
                if (movementDataGridView.Columns.Contains("dosage")) movementDataGridView.Columns["dosage"].HeaderText = "Dosage";
                if (movementDataGridView.Columns.Contains("category")) movementDataGridView.Columns["category"].HeaderText = "Category";
                if (movementDataGridView.Columns.Contains("movement_type")) movementDataGridView.Columns["movement_type"].HeaderText = "Movement Type";
                if (movementDataGridView.Columns.Contains("quantity")) movementDataGridView.Columns["quantity"].HeaderText = "Quantity";
                if (movementDataGridView.Columns.Contains("movement_date"))
                {
                    movementDataGridView.Columns["movement_date"].HeaderText = "Date";
                    movementDataGridView.Columns["movement_date"].DefaultCellStyle.Format = "dd/MM/yyyy"; // format with slashes
                }
                if (movementDataGridView.Columns.Contains("expiration_date"))
                {
                    movementDataGridView.Columns["expiration_date"].HeaderText = "Expiration";
                    movementDataGridView.Columns["expiration_date"].DefaultCellStyle.Format = "dd/MM/yyyy"; // format with slashes
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load movements: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void movementDateFromDateTimePicker_ValueChanged(object sender, EventArgs e) => SortMovementDate();
        private void movementDateToDateTimePicker_ValueChanged(object sender, EventArgs e) => SortMovementDate();

        private void SortMovementDate()
        {
            if (movementDateFromDateTimePicker.Value <= movementDateToDateTimePicker.Value)
            {
                string query = @"
                    SELECT sm.movement_id, sm.item_id, i.brand_name, i.generic_name, i.strength, i.dosage, i.category,
                           sm.movement_type, sm.quantity, sm.movement_date, sm.expiration_date
                    FROM stock_movements sm
                    INNER JOIN items i ON sm.item_id = i.item_id
                    WHERE sm.movement_date BETWEEN @fromDate AND @toDate
                    ORDER BY sm.movement_date DESC";

                DataTable dt = new DataTable();
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn))
                using (var adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@fromDate", movementDateFromDateTimePicker.Value.Date);
                    cmd.Parameters.AddWithValue("@toDate", movementDateToDateTimePicker.Value.Date.AddDays(1).AddSeconds(-1));
                    adapter.Fill(dt);
                }

                movementDataGridView.DataSource = dt;

                if (movementDataGridView.Columns.Contains("movement_id")) movementDataGridView.Columns["movement_id"].Visible = false;
                if (movementDataGridView.Columns.Contains("item_id")) movementDataGridView.Columns["item_id"].Visible = false;
            }
            else
            {
                MessageBox.Show("Start date must be earlier than end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void quantityNumericUpDown_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == '-')
            {
                e.Handled = true;
                MessageBox.Show("Decimal values are not allowed. Please enter a whole number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void writeOffButton_Click(object sender, EventArgs e)
        {
            // Ensure the textbox has a value
            if (string.IsNullOrWhiteSpace(itemIdTextBox.Text))
            {
                MessageBox.Show("Please select an item to write-off.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                itemIdTextBox.Focus();
                return;
            }

            // Safely parse item ID
            if (!int.TryParse(itemIdTextBox.Text.Trim(), out int itemId) || itemId <= 0)
            {
                MessageBox.Show("Invalid item selected. Please select a valid item.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                itemIdTextBox.Focus();
                return;
            }

            // Open Write-Off form
            using (WriteOffForm writeOffForm = new WriteOffForm(itemId))
            {
                writeOffForm.ShowDialog();

                // Refresh inventory after write-off
                LoadInventory();
                LoadMovements();
            }

        }


        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void descriptionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void costPriceNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvItems_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchItemsTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchHelper.Search(
dgv: dgvItems,
tableName: "items",
columnNames: new string[] { "generic_name", "brand_name" },
filterControl: searchItemsTextBox
);
        }

        private void clearFilterButton_Click(object sender, EventArgs e)
        {
            // Reset filters anywhere
            DGVColumnHeaderFilterHelper.ResetFilters(movementDataGridView);
            movementDateFromDateTimePicker.Value = DateTime.Now.AddMonths(-1);
            movementDateToDateTimePicker.Value = DateTime.Now;
        }
    }
}
