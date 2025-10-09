using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Consultation
{
    public partial class PrescriptionForm : Form
    {
        private readonly int _patientId;
        private readonly int _consultationId;
        private readonly InventoryHelper _inventoryHelper;
        private DGVCrudHelper otherItemsCrudHelper;

        private DataTable _availableItemsTable;
        private DataTable _availableOtherItemsTable;

        public PrescriptionForm(int patientId, int consultationId)
        {
            InitializeComponent();
            _patientId = patientId;
            _consultationId = consultationId;
            _inventoryHelper = new InventoryHelper();

            // =========================
            // INITIALIZE GRIDS
            // =========================
            SetupSelectedDgvColumns();         // for medicines
            SetupSelectedOtherDgvColumns();    // for other items

            LoadAvailableItems();              // Load medicines
            LoadAvailableOtherItems();         // Load other items

            // =========================
            // EVENT HANDLERS
            // =========================
            dgvAvailableItems.CellDoubleClick += DgvAvailableItems_CellDoubleClick;
            dgvOtherItems.CellDoubleClick += DgvOtherItems_CellDoubleClick;
            btnSubmit.Click += BtnSubmit_Click;

            // Enable double-click deletion for medicines
            dgvSelectedItems.CellDoubleClick += DgvSelectedItems_CellDoubleClick;


            // 🟩 NEW: Enable realtime deletion for Other Items
            selectedOtherDGV.CellDoubleClick += SelectedOtherDGV_CellDoubleClick;
            selectedOtherDGV.KeyDown += SelectedOtherDGV_KeyDown;

            dgvOtherItems.UserAddedRow += DgvOtherItems_UserAddedRow;
            dgvOtherItems.CellEndEdit += DgvOtherItems_CellEndEdit;
            dgvOtherItems.UserDeletingRow += DgvOtherItems_UserDeletingRow;

        }

        // =========================
        // SETUP MEDICINE GRID
        // =========================
        private void SetupSelectedDgvColumns()
        {
            if (dgvSelectedItems.Columns.Count == 0)
            {
                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "item_id",
                    HeaderText = "Item ID",
                    Visible = false
                });

                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "item_name",
                    HeaderText = "Item Name",
                    ReadOnly = true
                });

                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "description",
                    HeaderText = "Description",
                    ReadOnly = true
                });

                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "quantity",
                    HeaderText = "Quantity",
                    ValueType = typeof(int),
                    Width = 70
                });
            }
        }

        // =========================
        // LOAD AVAILABLE MEDICINES
        // =========================
        private void LoadAvailableItems()
        {
            try
            {
                _availableItemsTable = _inventoryHelper.GetAllItems();
                dgvAvailableItems.DataSource = _availableItemsTable;

                // Hide columns not needed
                foreach (var col in new[] { "cost_price", "selling_price", "created_at", "updated_at" })
                    if (dgvAvailableItems.Columns.Contains(col))
                        dgvAvailableItems.Columns[col].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // DOUBLE CLICK TO ADD MEDICINE
        // =========================
        private void DgvAvailableItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvAvailableItems.Rows[e.RowIndex];
            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
            string itemName = row.Cells["item_name"].Value.ToString();
            string description = row.Cells["description"].Value.ToString();

            // Check if item already exists in selected grid
            var existingRow = dgvSelectedItems.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(r => Convert.ToInt32(r.Cells["item_id"].Value) == itemId);

            if (existingRow != null)
            {
                existingRow.Cells["quantity"].Value = Convert.ToInt32(existingRow.Cells["quantity"].Value) + 1;
            }
            else
            {
                int newIndex = dgvSelectedItems.Rows.Add();
                dgvSelectedItems.Rows[newIndex].Cells["item_id"].Value = itemId;
                dgvSelectedItems.Rows[newIndex].Cells["item_name"].Value = itemName;
                dgvSelectedItems.Rows[newIndex].Cells["description"].Value = description;
                dgvSelectedItems.Rows[newIndex].Cells["quantity"].Value = 1;
            }
        }

        // =========================
        // SETUP OTHER ITEMS GRID
        // =========================
        private void SetupSelectedOtherDgvColumns()
        {
            if (selectedOtherDGV.Columns.Count == 0)
            {
                selectedOtherDGV.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "item_id",
                    HeaderText = "Item ID",
                    Visible = false
                });

                selectedOtherDGV.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "item_name",
                    HeaderText = "Item Name",
                    ReadOnly = true
                });

                selectedOtherDGV.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "description",
                    HeaderText = "Description",
                    ReadOnly = true
                });

                selectedOtherDGV.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "category",
                    HeaderText = "Category",
                    ReadOnly = true
                });

                selectedOtherDGV.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "quantity",
                    HeaderText = "Quantity",
                    ValueType = typeof(int),
                    Width = 70
                });
            }
        }

        // =========================
        // LOAD OTHER ITEMS
        // =========================
        private void LoadAvailableOtherItems()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT item_id, item_name, description, category FROM other_items";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        _availableOtherItemsTable = new DataTable();
                        adapter.Fill(_availableOtherItemsTable);
                        dgvOtherItems.DataSource = _availableOtherItemsTable;
                        dgvOtherItems.ReadOnly = false;
                        dgvOtherItems.AllowUserToDeleteRows = true;
                        dgvOtherItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Prevent editing the ID (primary key)
                        if (dgvOtherItems.Columns.Contains("item_id"))
                            dgvOtherItems.Columns["item_id"].ReadOnly = true;

                    }
                }

                dgvOtherItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading other items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // DOUBLE CLICK TO ADD OTHER ITEM
        // =========================
        private void DgvOtherItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvOtherItems.Rows[e.RowIndex];
            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
            string itemName = row.Cells["item_name"].Value.ToString();
            string description = row.Cells["description"].Value?.ToString();
            string category = row.Cells["category"].Value?.ToString();

            // Check if item already exists in selected grid
            var existingRow = selectedOtherDGV.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(r => Convert.ToInt32(r.Cells["item_id"].Value) == itemId);

            if (existingRow != null)
            {
                existingRow.Cells["quantity"].Value = Convert.ToInt32(existingRow.Cells["quantity"].Value) + 1;
            }
            else
            {
                int newIndex = selectedOtherDGV.Rows.Add();
                selectedOtherDGV.Rows[newIndex].Cells["item_id"].Value = itemId;
                selectedOtherDGV.Rows[newIndex].Cells["item_name"].Value = itemName;
                selectedOtherDGV.Rows[newIndex].Cells["description"].Value = description;
                selectedOtherDGV.Rows[newIndex].Cells["category"].Value = category;
                selectedOtherDGV.Rows[newIndex].Cells["quantity"].Value = 1;
            }
        }

        // =========================
        // SUBMIT PRESCRIPTION
        // =========================
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvSelectedItems.Rows.Count == 0 && selectedOtherDGV.Rows.Count == 0)
            {
                MessageBox.Show("No items selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 📝 Ask for notes for both sections
            PrescriptionNoteForm noteForm = new PrescriptionNoteForm(dgvSelectedItems, selectedOtherDGV);
            if (noteForm.ShowDialog() != DialogResult.OK) return;

            var itemNotes = noteForm.ItemNotes;
            var otherNotes = noteForm.OtherItemNotes;

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    var transaction = conn.BeginTransaction();

                    try
                    {
                        // ✅ Save medicines
                        foreach (DataGridViewRow row in dgvSelectedItems.Rows)
                        {
                            if (row.IsNewRow) continue;

                            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                            int qty = Convert.ToInt32(row.Cells["quantity"].Value);
                            if (qty <= 0) throw new Exception($"Invalid quantity for item ID {itemId}");

                            string insertQuery = @"
                        INSERT INTO prescription (patient_id, item_id, consultation_id, quantity, note)
                        VALUES (@patient_id, @item_id, @consultation_id, @quantity, @note)";

                            var cmd = new MySqlCommand(insertQuery, conn, transaction);
                            cmd.Parameters.AddWithValue("@patient_id", _patientId);
                            cmd.Parameters.AddWithValue("@item_id", itemId);
                            cmd.Parameters.AddWithValue("@consultation_id", _consultationId);
                            cmd.Parameters.AddWithValue("@quantity", qty);
                            cmd.Parameters.AddWithValue("@note", itemNotes.ContainsKey(itemId) ? itemNotes[itemId] : "");
                            cmd.ExecuteNonQuery();
                        }

                        // ✅ Save other items
                        foreach (DataGridViewRow row in selectedOtherDGV.Rows)
                        {
                            if (row.IsNewRow) continue;

                            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                            int qty = Convert.ToInt32(row.Cells["quantity"].Value);
                            if (qty <= 0) throw new Exception($"Invalid quantity for other item ID {itemId}");

                            string insertOther = @"
                        INSERT INTO prescription_other (patient_id, consultation_id, item_id, quantity, note)
                        VALUES (@patient_id, @consultation_id, @item_id, @quantity, @note)";

                            var cmdOther = new MySqlCommand(insertOther, conn, transaction);
                            cmdOther.Parameters.AddWithValue("@patient_id", _patientId);
                            cmdOther.Parameters.AddWithValue("@consultation_id", _consultationId);
                            cmdOther.Parameters.AddWithValue("@item_id", itemId);
                            cmdOther.Parameters.AddWithValue("@quantity", qty);
                            cmdOther.Parameters.AddWithValue("@note", otherNotes.ContainsKey(itemId) ? otherNotes[itemId] : "");
                            cmdOther.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Prescription submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting prescription: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ✅ Print and reset
            var printer = new PrintingForms.PrescriptionPrintHelper(_consultationId);
            printer.ShowPreview();

            dgvSelectedItems.Rows.Clear();
            selectedOtherDGV.Rows.Clear();
            this.Close();
        }


        // =========================
        // SEARCH FUNCTIONS
        // =========================
        private void searchItemtButton_Click(object sender, EventArgs e)
        {
            SearchHelper.Search(
                dgv: dgvAvailableItems,
                tableName: "items",
                columnNames: new string[] { "item_name", "description" },
                filterControl: searchItemsTextBox
            );
        }

        //private void searchOtherButton_Click(object sender, EventArgs e)
        //{
        //    SearchHelper.Search(
        //        dgv: dgvOtherItems,
        //        tableName: "other_items",
        //        columnNames: new string[] { "item_name", "description", "category" },
        //        filterControl: searchOtherTextBox
        //    );
        //}

        // =========================
        // MISC EVENTS
        // =========================
        private void PrescriptionForm_Load(object sender, EventArgs e)
        {
            AutoCompleteHelper.SetupAutoComplete(categoryCombobox, "items", new List<string> { "category" });
            LoadOtherItems();
        }
        private void LoadOtherItems()
        {
            // Define the columns you want to display/edit
            List<string> otherItemsColumns = new List<string>
    {
        "item_name",
        "description",
        "category"
    };

            // Initialize the CRUD helper for the Other Items table
            otherItemsCrudHelper = new DGVCrudHelper(
                dgvOtherItems,
                "other_items",        // ✅ Table name in MySQL
                otherItemsColumns,    // ✅ Columns to show
                "item_id"             // ✅ Primary key column
            );

            // Optional: add pagination label if you want to show page info

            // Load first page of data
            otherItemsCrudHelper.LoadData();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            dgvSelectedItems.Rows.Clear();
            selectedOtherDGV.Rows.Clear();
        }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {

        }
        // =========================
        // DOUBLE CLICK TO REMOVE / DECREASE OTHER ITEM
        // =========================
        private void SelectedOtherDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = selectedOtherDGV.Rows[e.RowIndex];
            int currentQty = Convert.ToInt32(row.Cells["quantity"].Value);

            if (currentQty > 1)
            {
                // Decrease quantity by 1
                row.Cells["quantity"].Value = currentQty - 1;
            }
            else
            {
                // Remove item completely if quantity = 1
                selectedOtherDGV.Rows.RemoveAt(e.RowIndex);
            }
        }

        // =========================
        // PRESS DELETE KEY TO REMOVE SELECTED OTHER ITEM
        // =========================
        private void SelectedOtherDGV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedOtherDGV.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Remove selected item?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in selectedOtherDGV.SelectedRows)
                    {
                        if (!row.IsNewRow)
                            selectedOtherDGV.Rows.Remove(row);
                    }
                }
            }
        }

        private void refreshPatientsButton_Click(object sender, EventArgs e)
        {
            LoadAvailableOtherItems();
        }
        // =========================
        // 1️⃣ ADD NEW OTHER ITEM
        // =========================
        private void DgvOtherItems_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                var row = e.Row;
                if (row.IsNewRow) return;

                string itemName = row.Cells["item_name"].Value?.ToString();
                string description = row.Cells["description"].Value?.ToString() ?? "";
                string category = row.Cells["category"].Value?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(itemName))
                {
                    MessageBox.Show("Item Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO other_items (item_name, description, category) VALUES (@item_name, @description, @category)";
                    using (var cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_name", itemName);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadAvailableOtherItems(); // Refresh table after adding
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // =========================
        // 2️⃣ EDIT OTHER ITEM (UPDATE)
        // =========================
        private void DgvOtherItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = dgvOtherItems.Rows[e.RowIndex];
                if (row.IsNewRow) return;

                int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                string itemName = row.Cells["item_name"].Value?.ToString();
                string description = row.Cells["description"].Value?.ToString() ?? "";
                string category = row.Cells["category"].Value?.ToString() ?? "";

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string updateQuery = @"
                UPDATE other_items 
                SET item_name = @item_name, description = @description, category = @category
                WHERE item_id = @item_id";

                    using (var cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_name", itemName);
                        cmd.Parameters.AddWithValue("@description", description);
                        cmd.Parameters.AddWithValue("@category", category);
                        cmd.Parameters.AddWithValue("@item_id", itemId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // =========================
        // 3️⃣ DELETE OTHER ITEM
        // =========================
        private void DgvOtherItems_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                var row = e.Row;
                if (row.Cells["item_id"].Value == null) return;

                int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                var confirm = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM other_items WHERE item_id = @item_id";
                    using (var cmd = new MySqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@item_id", itemId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Item deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOtherItems_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxItem_Enter(object sender, EventArgs e)
        {

        }

        private void addItemButton_Click(object sender, EventArgs e)
        {
            string category = addCategoryComboBox.Text.Trim();
            string itemName = addItemNameComboBox.Text.Trim();
            string description = addDescriptionComboBox.Text.Trim();

            if (string.IsNullOrEmpty(category) || string.IsNullOrEmpty(itemName))
            {
                MessageBox.Show("Category and Item Name are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO other_items (category, item_name, description) VALUES (@category, @item_name, @description)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@item_name", itemName);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOtherItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateItemButton_Click(object sender, EventArgs e)
        {
            if (dgvOtherItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvOtherItems.SelectedRows[0];
            int itemId = Convert.ToInt32(selectedRow.Cells["item_id"].Value);

            string category = addCategoryComboBox.Text.Trim();
            string itemName = addItemNameComboBox.Text.Trim();
            string description = addDescriptionComboBox.Text.Trim();

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE other_items SET category = @category, item_name = @item_name, description = @description WHERE item_id = @item_id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@item_name", itemName);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@item_id", itemId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Item updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOtherItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOtherItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return; // Ignore header clicks

                DataGridViewRow selectedRow = dgvOtherItems.Rows[e.RowIndex];

                // Fill ComboBoxes with selected item values
                addCategoryComboBox.Text = selectedRow.Cells["category"]?.Value?.ToString() ?? "";
                addItemNameComboBox.Text = selectedRow.Cells["item_name"]?.Value?.ToString() ?? "";
                addDescriptionComboBox.Text = selectedRow.Cells["description"]?.Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading item details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // =========================
        // DOUBLE CLICK TO REMOVE / DECREASE MEDICINE
        // =========================
        private void DgvSelectedItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvSelectedItems.Rows[e.RowIndex];
            int currentQty = Convert.ToInt32(row.Cells["quantity"].Value);

            if (currentQty > 1)
            {
                // Decrease quantity by 1
                row.Cells["quantity"].Value = currentQty - 1;
            }
            else
            {
                // Remove item completely if quantity = 1
                dgvSelectedItems.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            categoryCombobox.Text = "";
            addItemNameComboBox.Text = "";
            addDescriptionComboBox.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
