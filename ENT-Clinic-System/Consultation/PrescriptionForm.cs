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
        private DataTable _availableItemsTable;

        public PrescriptionForm(int patientId, int consultationId)
        {
            InitializeComponent();
            _patientId = patientId;
            _consultationId = consultationId;
            _inventoryHelper = new InventoryHelper();

            SetupSelectedDgvColumns(); // Ensure columns exist
            LoadAvailableItems();

            dgvAvailableItems.CellDoubleClick += DgvAvailableItems_CellDoubleClick;
            btnSubmit.Click += BtnSubmit_Click;
        }

        // =========================
        // Setup Selected Items Columns
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
        // Load Available Inventory Items
        // =========================
        private void LoadAvailableItems()
        {
            try
            {
                _availableItemsTable = _inventoryHelper.GetAllItems();
                dgvAvailableItems.DataSource = _availableItemsTable;

                // Hide unnecessary columns
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
        // Add Item on Double Click
        // =========================
        private void DgvAvailableItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvAvailableItems.Rows[e.RowIndex];
            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
            string itemName = row.Cells["item_name"].Value.ToString();
            string description = row.Cells["description"].Value.ToString();

            // Check if already added
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
        // Submit Prescription
        // =========================
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvSelectedItems.Rows.Count == 0)
            {
                MessageBox.Show("No items selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prompt notes for items
            PrescriptionNoteForm noteForm = new PrescriptionNoteForm(dgvSelectedItems);
            if (noteForm.ShowDialog() != DialogResult.OK) return;
            var itemNotes = noteForm.ItemNotes;

            // Save to database
            try
            {
                var conn = DBConfig.GetConnection();
                conn.Open();
                var transaction = conn.BeginTransaction();

                try
                {
                    foreach (DataGridViewRow row in dgvSelectedItems.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                        int qty = Convert.ToInt32(row.Cells["quantity"].Value);

                        if (qty <= 0)
                            throw new Exception($"Invalid quantity for item ID {itemId}");

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

                    transaction.Commit();
                    MessageBox.Show("Prescription submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting prescription: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Print prescription using the helper (clean layout, notes included)
            var printer = new PrintingForms.PrescriptionPrintHelper(_consultationId);
            printer.ShowPreview();

            dgvSelectedItems.Rows.Clear();
            this.Close();
        }

        // =========================
        // Search Items
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
        private void PrescriptionForm_Load(object sender, EventArgs e)
        {
        }
    }
}
