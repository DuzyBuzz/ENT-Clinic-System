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

        private DataTable _availableItemsTable;
        private DataTable _availableOtherItemsTable;

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
                selectedOtherDGV.Columns.Add(new DataGridViewTextBoxColumn { Name = "item_name", HeaderText = "Item Name", ReadOnly = true });
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
                newRow.Cells["item_name"].Value = row.Cells["item_name"].Value;
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
        private void LoadAvailableOtherItems()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    item_id, 
                    item_name AS 'Item Name', 
                    description AS 'Description', 
                    category AS 'Category',
                    quantity AS 'Available Qty'
                FROM other_items;
            ";

                    using (var adapter = new MySqlDataAdapter(query, conn))
                    {
                        _availableOtherItemsTable = new DataTable();
                        adapter.Fill(_availableOtherItemsTable);
                        dgvOtherItems.DataSource = _availableOtherItemsTable;
                    }

                    // ✅ Hide internal IDs
                    if (dgvOtherItems.Columns.Contains("item_id"))
                        dgvOtherItems.Columns["item_id"].Visible = false;

                    // ✅ Adjust column order and headers
                    string[] displayOrder = { "Item Name", "Description", "Category", "Available Qty" };
                    int order = 0;
                    foreach (string col in displayOrder)
                    {
                        if (dgvOtherItems.Columns.Contains(col))
                            dgvOtherItems.Columns[col].DisplayIndex = order++;
                    }

                    // ✅ Styling and behavior
                    dgvOtherItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvOtherItems.ReadOnly = true;
                    dgvOtherItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvOtherItems.MultiSelect = false;
                    dgvOtherItems.RowHeadersVisible = false;
                    dgvOtherItems.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading other items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
