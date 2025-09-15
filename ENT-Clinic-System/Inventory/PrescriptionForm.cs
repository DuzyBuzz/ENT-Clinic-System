using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Inventory
{
    public partial class PrescriptionForm : Form
    {
        private readonly int _patientId;
        private readonly InventoryHelper _inventoryHelper;
        private DataTable _availableItemsTable;

        public PrescriptionForm(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            _inventoryHelper = new InventoryHelper();

            SetupSelectedDgvColumns(); // Ensure columns exist
            LoadAvailableItems();

            dgvAvailableItems.CellDoubleClick += DgvAvailableItems_CellDoubleClick;
            btnSubmit.Click += BtnSubmit_Click;
        }

        // ================================
        // 🔹 Setup Selected Items Columns
        // ================================
        private void SetupSelectedDgvColumns()
        {
            if (dgvSelectedItems.Columns.Count == 0)
            {
                dgvSelectedItems.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "item_id",
                    HeaderText = "Item ID",
                    Visible = false // hide ID from user
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

        // ================================
        // 🔹 Load Inventory Items
        // ================================
        private void LoadAvailableItems()
        {
            try
            {
                _availableItemsTable = _inventoryHelper.GetAllItems();
                dgvAvailableItems.DataSource = _availableItemsTable;

                // Hide unnecessary columns for prescription
                dgvAvailableItems.Columns["cost_price"].Visible = false;
                dgvAvailableItems.Columns["selling_price"].Visible = false;
                dgvAvailableItems.Columns["created_at"].Visible = false;
                dgvAvailableItems.Columns["updated_at"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================
        // 🔹 Add Selected Item on Double Click
        // ================================
        private void DgvAvailableItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgvAvailableItems.Rows[e.RowIndex];
                int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                string itemName = row.Cells["item_name"].Value.ToString();
                string description = row.Cells["description"].Value.ToString();

                // Check if item already exists in selected dgv
                var existingRow = dgvSelectedItems.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => Convert.ToInt32(r.Cells["item_id"].Value) == itemId);

                if (existingRow != null)
                {
                    int currentQty = Convert.ToInt32(existingRow.Cells["quantity"].Value);
                    existingRow.Cells["quantity"].Value = currentQty + 1;
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
            catch (Exception ex)
            {
                MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================
        // 🔹 Submit Prescription
        // ================================
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvSelectedItems.Rows.Count == 0)
            {
                MessageBox.Show("No items selected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (DataGridViewRow row in dgvSelectedItems.Rows)
                            {
                                if (row.IsNewRow) continue;

                                int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                                int qty = Convert.ToInt32(row.Cells["quantity"].Value);

                                if (qty <= 0)
                                    throw new Exception($"Invalid quantity for item ID {itemId}");

                                string insertQuery = @"INSERT INTO prescription 
                                                       (patient_id, item_id, quantity) 
                                                       VALUES (@patient_id, @item_id, @quantity)";

                                using (var cmd = new MySqlCommand(insertQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@patient_id", _patientId);
                                    cmd.Parameters.AddWithValue("@item_id", itemId);
                                    cmd.Parameters.AddWithValue("@quantity", qty);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Prescription submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvSelectedItems.Rows.Clear();
                            this.Close();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting prescription: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
