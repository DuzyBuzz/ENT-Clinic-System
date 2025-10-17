using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        private DGVCrudHelper _otherItemsCrudHelper;
        private DataTable _availableItemsTable;
        private DataTable _availableOtherItemsTable;
        private ContextMenuStrip _otherItemsContextMenu;

        public PrescriptionForm(int patientId, int consultationId)
        {
            InitializeComponent();
            _patientId = patientId;
            _consultationId = consultationId;
            _inventoryHelper = new InventoryHelper();

            // Initialize Grids


            // Event Handlers
            dgvAvailableItems.CellDoubleClick += DgvAvailableItems_CellDoubleClick;
            dgvOtherItems.CellDoubleClick += DgvOtherItems_CellDoubleClick;
            dgvSelectedItems.MouseDown += DgvSelectedItems_MouseDown;
            selectedOtherDGV.MouseDown += SelectedOtherDGV_MouseDown;
            selectedOtherDGV.KeyDown += SelectedOtherDGV_KeyDown;
            btnSubmit.Click += BtnSubmit_Click;
            clearButton.Click += ClearButton_Click;
            button2.Click += Button2_Click; // Close button
            SetupSelectedDgvColumns();
            SetupSelectedOtherDgvColumns();
            LoadAvailableItems();
            LoadAvailableOtherItems();
            dgvOtherItems.SelectionChanged += (s, e) =>
            {
                if (dgvOtherItems.SelectedRows.Count > 0)
                    PopulateOtherItemInputsFromRow(dgvOtherItems.SelectedRows[0]);
            };
            dgvOtherItems.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    PopulateOtherItemInputsFromRow(dgvOtherItems.Rows[e.RowIndex]);
            };

        }

        #region === GRID SETUP ===

        private void SetupSelectedDgvColumns()
        {
            if (dgvSelectedItems.Columns.Count == 0)
            {
                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "item_id", Visible = false });
                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "generic_name", HeaderText = "Generic Name", ReadOnly = true });
                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "brand_name", HeaderText = "Brand Name", ReadOnly = true });
                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "strength", HeaderText = "Strength", ReadOnly = true });
                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "dosage", HeaderText = "Dosage", ReadOnly = true });
                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "category", HeaderText = "Category", ReadOnly = true });
                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "description", HeaderText = "Description", ReadOnly = true });
                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "quantity", HeaderText = "Quantity", ValueType = typeof(int), Width = 70 });
            }

        }

        private void SetupSelectedOtherDgvColumns()
        {
            if (selectedOtherDGV.Columns.Count == 0)
            {
                selectedOtherDGV.Columns.Add(new DataGridViewTextBoxColumn { Name = "item_id", Visible = false });
                selectedOtherDGV.Columns.Add(new DataGridViewTextBoxColumn { Name = "description", HeaderText = "Description", ReadOnly = true });
                selectedOtherDGV.Columns.Add(new DataGridViewTextBoxColumn { Name = "category", HeaderText = "Category", ReadOnly = true });
                selectedOtherDGV.Columns.Add(new DataGridViewTextBoxColumn { Name = "quantity", HeaderText = "Quantity", ValueType = typeof(int), Width = 70 });
            }
        }

        #endregion

        #region === LOAD DATA ===

        private void LoadAvailableItems()
        {
            try
            {
                _availableItemsTable = _inventoryHelper.GetAllItems();
                dgvAvailableItems.DataSource = _availableItemsTable;

                // ✅ Hide unnecessary columns
                string[] hiddenCols = { "item_id", "cost_price", "selling_price", "created_at", "updated_at" };
                foreach (string col in hiddenCols)
                {
                    if (dgvAvailableItems.Columns.Contains(col))
                        dgvAvailableItems.Columns[col].Visible = false;
                }

                // ✅ Rename and reorder visible columns (to match selected DGV)
                if (dgvAvailableItems.Columns.Contains("generic_name"))
                    dgvAvailableItems.Columns["generic_name"].HeaderText = "Generic Name";
                if (dgvAvailableItems.Columns.Contains("brand_name"))
                    dgvAvailableItems.Columns["brand_name"].HeaderText = "Brand Name";
                if (dgvAvailableItems.Columns.Contains("strength"))
                    dgvAvailableItems.Columns["strength"].HeaderText = "Strength";
                if (dgvAvailableItems.Columns.Contains("dosage"))
                    dgvAvailableItems.Columns["dosage"].HeaderText = "Dosage";
                if (dgvAvailableItems.Columns.Contains("category"))
                    dgvAvailableItems.Columns["category"].HeaderText = "Category";
                if (dgvAvailableItems.Columns.Contains("description"))
                    dgvAvailableItems.Columns["description"].HeaderText = "Description";

                // ✅ Show and format the Quantity column
                if (dgvAvailableItems.Columns.Contains("quantity"))
                {
                    dgvAvailableItems.Columns["quantity"].Visible = true;
                    dgvAvailableItems.Columns["quantity"].HeaderText = "Available Qty";
                    dgvAvailableItems.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAvailableItems.Columns["quantity"].ReadOnly = true;
                }

                // ✅ Adjust column display order (to mirror selected items DGV)
                string[] displayOrder = { "generic_name", "brand_name", "strength", "dosage", "category", "description", "quantity" };
                int order = 0;
                foreach (string col in displayOrder)
                {
                    if (dgvAvailableItems.Columns.Contains(col))
                        dgvAvailableItems.Columns[col].DisplayIndex = order++;
                }

                // ✅ General DataGridView UI settings
                dgvAvailableItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvAvailableItems.ReadOnly = true;
                dgvAvailableItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAvailableItems.MultiSelect = false;
                dgvAvailableItems.RowHeadersVisible = false; // cleaner look
                dgvAvailableItems.AllowUserToAddRows = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading medicines: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        #endregion

        #region === ADD / REMOVE ITEMS ===

        private void DgvAvailableItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvAvailableItems.Rows[e.RowIndex];
            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);

            var existing = dgvSelectedItems.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(r => Convert.ToInt32(r.Cells["item_id"].Value) == itemId);

            if (existing != null)
                existing.Cells["quantity"].Value = Convert.ToInt32(existing.Cells["quantity"].Value) + 1;
            else
            {
                int idx = dgvSelectedItems.Rows.Add();
                var newRow = dgvSelectedItems.Rows[idx];
                newRow.Cells["item_id"].Value = itemId;
                newRow.Cells["generic_name"].Value = row.Cells["generic_name"].Value;
                newRow.Cells["brand_name"].Value = row.Cells["brand_name"].Value;
                newRow.Cells["strength"].Value = row.Cells["strength"].Value;
                newRow.Cells["dosage"].Value = row.Cells["dosage"].Value;
                newRow.Cells["category"].Value = row.Cells["category"].Value;
                newRow.Cells["description"].Value = row.Cells["description"].Value;
                newRow.Cells["quantity"].Value = 1;
            }
        }

        private void DgvOtherItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvOtherItems.Rows[e.RowIndex];
            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);

            var existing = selectedOtherDGV.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(r => Convert.ToInt32(r.Cells["item_id"].Value) == itemId);

            if (existing != null)
                existing.Cells["quantity"].Value = Convert.ToInt32(existing.Cells["quantity"].Value) + 1;
            else
            {
                int idx = selectedOtherDGV.Rows.Add();
                var newRow = selectedOtherDGV.Rows[idx];
                newRow.Cells["item_id"].Value = itemId;
                newRow.Cells["description"].Value = row.Cells["description"].Value;
                newRow.Cells["category"].Value = row.Cells["category"].Value;
                newRow.Cells["quantity"].Value = 1;
            }
        }

        private void DgvSelectedItems_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var hit = dgvSelectedItems.HitTest(e.X, e.Y);
            if (hit.RowIndex < 0) return;

            dgvSelectedItems.ClearSelection();
            dgvSelectedItems.Rows[hit.RowIndex].Selected = true;

            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem remove = new ToolStripMenuItem("Remove This Item") { ForeColor = Color.Red };
            remove.Click += (s, ev) =>
            {
                if (MessageBox.Show("Remove this item?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    dgvSelectedItems.Rows.RemoveAt(hit.RowIndex);
            };
            menu.Items.Add(remove);
            menu.Show(dgvSelectedItems, e.Location);
        }
        /// <summary>
        /// Loads and enables editing for non-clinic (other) items using DGVCrudHelper.
        /// Includes inline edit + right-click delete.
        /// </summary>
        private void LoadAvailableOtherItems()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    other_items.item_id,
                    other_items.generic_name,
                    other_items.brand_name,
                    other_items.strength,
                    other_items.dosage,
                    other_items.description,
                    other_items.category,
                    other_items.created_at,
                    other_items.updated_at
                FROM ent_clinic_db.other_items;
            ";

                    using (var adapter = new MySqlDataAdapter(query, conn))
                    {
                        _availableOtherItemsTable = new DataTable();
                        adapter.Fill(_availableOtherItemsTable);
                        dgvOtherItems.DataSource = _availableOtherItemsTable;
                    }

                    // ✅ Hide unnecessary columns
                    string[] hiddenCols = { "created_at", "updated_at" };
                    foreach (string col in hiddenCols)
                    {
                        if (dgvOtherItems.Columns.Contains(col))
                            dgvOtherItems.Columns[col].Visible = false;
                    }

                    // ✅ Rename headers
                    var headers = new Dictionary<string, string>
            {
                { "generic_name", "Generic Name" },
                { "brand_name", "Brand Name" },
                { "strength", "Strength" },
                { "dosage", "Dosage" },
                { "description", "Description" },
                { "category", "Category" }
            };

                    foreach (var h in headers)
                    {
                        if (dgvOtherItems.Columns.Contains(h.Key))
                            dgvOtherItems.Columns[h.Key].HeaderText = h.Value;
                    }

                    // ✅ Reorder columns
                    string[] displayOrder = {
                "generic_name", "brand_name", "strength", "dosage",
                 "description", "category"
            };
                    int order = 0;
                    foreach (string col in displayOrder)
                    {
                        if (dgvOtherItems.Columns.Contains(col))
                            dgvOtherItems.Columns[col].DisplayIndex = order++;
                    }



                    // Make primary key hidden but accessible for CRUD
                    if (dgvOtherItems.Columns.Contains("item_id"))
                        dgvOtherItems.Columns["item_id"].Visible = false;
                }

                // ✅ Attach DGVCrudHelper for inline edit support
                _otherItemsCrudHelper = new DGVCrudHelper(
                    dgvOtherItems,
                    "other_items",
                    new List<string>
                    {
                "generic_name",
                "brand_name",
                "strength",
                "dosage",
                "description",
                "category"
                    },
                    "item_id"
                );

                // ✅ Add right-click delete option
                InitializeOtherItemsContextMenu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading other items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeOtherItemsContextMenu()
        {
            _otherItemsContextMenu = new ContextMenuStrip();
            var deleteItem = new ToolStripMenuItem("Delete This Item") { ForeColor = Color.Red };
            deleteItem.Click += (s, e) =>
            {
                if (dgvOtherItems.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select an item to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var row = dgvOtherItems.SelectedRows[0];
                var id = row.Cells["item_id"].Value;

                if (MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        string sql = "DELETE FROM other_items WHERE item_id = @id";
                        using (var conn = DBConfig.GetConnection())
                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Item deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAvailableOtherItems(); // Refresh grid
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Delete failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            _otherItemsContextMenu.Items.Add(deleteItem);
            dgvOtherItems.ContextMenuStrip = _otherItemsContextMenu;
        }



        #endregion
        private void SelectedOtherDGV_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var hit = selectedOtherDGV.HitTest(e.X, e.Y);
            if (hit.RowIndex < 0) return;

            selectedOtherDGV.ClearSelection();
            selectedOtherDGV.Rows[hit.RowIndex].Selected = true;

            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem remove = new ToolStripMenuItem("Remove This Item") { ForeColor = Color.Red };
            remove.Click += (s, ev) =>
            {
                if (MessageBox.Show("Remove this item?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    selectedOtherDGV.Rows.RemoveAt(hit.RowIndex);
            };
            menu.Items.Add(remove);
            menu.Show(selectedOtherDGV, e.Location);
        }

        private void SelectedOtherDGV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedOtherDGV.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Remove selected item?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in selectedOtherDGV.SelectedRows)
                        selectedOtherDGV.Rows.Remove(row);
                }
            }
        }


        #region === SUBMIT PRESCRIPTION ===

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvSelectedItems.Rows.Count == 0 && selectedOtherDGV.Rows.Count == 0)
            {
                MessageBox.Show("No items selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open SIG form
            PrescriptionNoteForm sigForm = new PrescriptionNoteForm(dgvSelectedItems, selectedOtherDGV);
            if (sigForm.ShowDialog() != DialogResult.OK) return;

            var itemSigs = sigForm.ItemSigs;        // Use ItemSigs
            var otherSigs = sigForm.OtherItemSigs;  // Use OtherItemSigs

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    var transaction = conn.BeginTransaction();

                    try
                    {
                        // Insert medicines
                        foreach (DataGridViewRow row in dgvSelectedItems.Rows)
                        {
                            if (row.IsNewRow) continue;
                            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                            int qty = Convert.ToInt32(row.Cells["quantity"].Value);

                            string query = @"INSERT INTO prescription 
                                     (patient_id, item_id, consultation_id, quantity, sig)
                                     VALUES (@patient_id, @item_id, @consultation_id, @quantity, @sig)";
                            var cmd = new MySqlCommand(query, conn, transaction);
                            cmd.Parameters.AddWithValue("@patient_id", _patientId);
                            cmd.Parameters.AddWithValue("@item_id", itemId);
                            cmd.Parameters.AddWithValue("@consultation_id", _consultationId);
                            cmd.Parameters.AddWithValue("@quantity", qty);
                            cmd.Parameters.AddWithValue("@sig", itemSigs.ContainsKey(itemId) ? itemSigs[itemId] : "");
                            cmd.ExecuteNonQuery();
                        }

                        // Insert other items
                        foreach (DataGridViewRow row in selectedOtherDGV.Rows)
                        {
                            if (row.IsNewRow) continue;
                            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                            int qty = Convert.ToInt32(row.Cells["quantity"].Value);

                            string query = @"INSERT INTO prescription_other 
                                     (patient_id, consultation_id, item_id, quantity, sig)
                                     VALUES (@patient_id, @consultation_id, @item_id, @quantity, @sig)";
                            var cmd = new MySqlCommand(query, conn, transaction);
                            cmd.Parameters.AddWithValue("@patient_id", _patientId);
                            cmd.Parameters.AddWithValue("@consultation_id", _consultationId);
                            cmd.Parameters.AddWithValue("@item_id", itemId);
                            cmd.Parameters.AddWithValue("@quantity", qty);
                            cmd.Parameters.AddWithValue("@sig", otherSigs.ContainsKey(itemId) ? otherSigs[itemId] : "");
                            cmd.ExecuteNonQuery();
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

            // Print
            var printer = new PrintingForms.PrescriptionPrintHelper(_consultationId);
            printer.ShowPreview();

            // Clear grids and close form
            dgvSelectedItems.Rows.Clear();
            selectedOtherDGV.Rows.Clear();
            this.Close();
        }


        #endregion

        private void ClearButton_Click(object sender, EventArgs e)
        {
            dgvSelectedItems.Rows.Clear();
            selectedOtherDGV.Rows.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {

        }

        private void addItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Get values and clean formatting
                string brand = FirstLetterUpperHelper.ToFirstUpper(brandNameComboBox.Text.Trim());
                string generic = FirstLetterUpperHelper.ToFirstUpper(genericNameComboBox.Text.Trim());
                string strength = FirstLetterUpperHelper.ToFirstUpper(stregnthComboBox.Text.Trim());
                string dosage = FirstLetterUpperHelper.ToFirstUpper(dosageComboBox.Text.Trim());
                string category = FirstLetterUpperHelper.ToFirstUpper(categoryComboBox.Text.Trim());
                string description = FirstLetterUpperHelper.ToFirstUpper(descriptionComboBox.Text.Trim());

                if (string.IsNullOrWhiteSpace(brand) || string.IsNullOrWhiteSpace(generic))
                {
                    MessageBox.Show("Brand and Generic names are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check duplicate
                string checkQuery = @"SELECT COUNT(*) FROM other_items 
                              WHERE brand_name = @brand AND generic_name = @generic 
                                    AND strength = @strength AND dosage = @dosage;";
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@generic", generic);
                    cmd.Parameters.AddWithValue("@strength", strength);
                    cmd.Parameters.AddWithValue("@dosage", dosage);
                    conn.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("This item already exists!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // Insert
                string insertQuery = @"INSERT INTO other_items (brand_name, generic_name, strength, dosage, category, description)
                               VALUES (@brand, @generic, @strength, @dosage, @category, @description);";
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@generic", generic);
                    cmd.Parameters.AddWithValue("@strength", strength);
                    cmd.Parameters.AddWithValue("@dosage", dosage);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@description", description);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAvailableOtherItems();
                ClearOtherItemInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void updateItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOtherItems.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an item to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRow = dgvOtherItems.SelectedRows[0];
                int itemId = Convert.ToInt32(selectedRow.Cells["item_id"].Value);

                string brand = FirstLetterUpperHelper.ToFirstUpper(brandNameComboBox.Text.Trim());
                string generic = FirstLetterUpperHelper.ToFirstUpper(genericNameComboBox.Text.Trim());
                string strength = FirstLetterUpperHelper.ToFirstUpper(stregnthComboBox.Text.Trim());
                string dosage = FirstLetterUpperHelper.ToFirstUpper(dosageComboBox.Text.Trim());
                string category = FirstLetterUpperHelper.ToFirstUpper(categoryComboBox.Text.Trim());
                string description = FirstLetterUpperHelper.ToFirstUpper(descriptionComboBox.Text.Trim());

                if (string.IsNullOrWhiteSpace(brand) || string.IsNullOrWhiteSpace(generic))
                {
                    MessageBox.Show("Brand and Generic names are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show("Are you sure you want to update this item?",
                    "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                string updateQuery = @"UPDATE other_items 
                               SET brand_name = @brand, generic_name = @generic,
                                   strength = @strength, dosage = @dosage,
                                   category = @category, description = @description
                               WHERE item_id = @id;";
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@brand", brand);
                    cmd.Parameters.AddWithValue("@generic", generic);
                    cmd.Parameters.AddWithValue("@strength", strength);
                    cmd.Parameters.AddWithValue("@dosage", dosage);
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@id", itemId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Item updated successfully!", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAvailableOtherItems();
                ClearOtherItemInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ClearOtherItemInputs();
        }
        /// <summary>
        /// Clears all input ComboBoxes for other_items
        /// </summary>
        private void ClearOtherItemInputs()
        {
            brandNameComboBox.Text = "";
            genericNameComboBox.Text = "";
            stregnthComboBox.Text = "";
            dosageComboBox.Text = "";
            categoryComboBox.Text = "";
            descriptionComboBox.Text = "";
        }

        /// <summary>
        /// Populates input ComboBoxes from a selected DataGridView row
        /// </summary>
        private void PopulateOtherItemInputsFromRow(DataGridViewRow row)
        {
            brandNameComboBox.Text = row.Cells["brand_name"].Value?.ToString() ?? "";
            genericNameComboBox.Text = row.Cells["generic_name"].Value?.ToString() ?? "";
            stregnthComboBox.Text = row.Cells["strength"].Value?.ToString() ?? "";
            dosageComboBox.Text = row.Cells["dosage"].Value?.ToString() ?? "";
            categoryComboBox.Text = row.Cells["category"].Value?.ToString() ?? "";
            descriptionComboBox.Text = row.Cells["description"].Value?.ToString() ?? "";
        }
        private void PrescriptionForm_Load(object sender, EventArgs e)
        {
            AutoCompleteHelper.SetupAutoComplete(sortCategoryCombobox, "items", new List<string> { "category" });




            AutoCompleteHelper.SetupAutoComplete(brandNameComboBox, "other_items", new List<string> { "brand_name" });
            AutoCompleteHelper.SetupAutoComplete(genericNameComboBox, "other_items", new List<string> { "generic_name" });
            AutoCompleteHelper.SetupAutoComplete(stregnthComboBox, "other_items", new List<string> { "strength" });
            AutoCompleteHelper.SetupAutoComplete(dosageComboBox, "other_items", new List<string> { "dosage" });
            AutoCompleteHelper.SetupAutoComplete(categoryComboBox, "other_items", new List<string> { "category" });
            AutoCompleteHelper.SetupAutoComplete(descriptionComboBox, "other_items", new List<string> { "description" });
        }
    }
}
