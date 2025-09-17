using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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

            // 1️⃣ Prompt for notes
            PrescriptionNoteForm noteForm = new PrescriptionNoteForm(dgvSelectedItems);
            if (noteForm.ShowDialog() != DialogResult.OK) return;
            var itemNotes = noteForm.ItemNotes;

            // 2️⃣ Save prescription to database
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
                                                   (patient_id, item_id, quantity, note) 
                                                   VALUES (@patient_id, @item_id, @quantity, @note)";

                                using (var cmd = new MySqlCommand(insertQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@patient_id", _patientId);
                                    cmd.Parameters.AddWithValue("@item_id", itemId);
                                    cmd.Parameters.AddWithValue("@quantity", qty);
                                    cmd.Parameters.AddWithValue("@note", itemNotes.ContainsKey(itemId) ? itemNotes[itemId] : "");
                                    cmd.ExecuteNonQuery();
                                }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting prescription: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3️⃣ Print prescription
            PrintPrescription(itemNotes);

            dgvSelectedItems.Rows.Clear();
            this.Close();
        }

    // ================================
    // 🔹 Print Prescription
    // ================================
    private void PrintPrescription(Dictionary<int, string> itemNotes)
    {
        PrintDocument pd = new PrintDocument();
        pd.DefaultPageSettings.Landscape = false;
        pd.PrintPage += (sender, e) =>
        {
            int y = 50;
            int lineHeight = 20;
            Font headerFont = new Font("Segoe UI", 12, FontStyle.Bold);
            Font normalFont = new Font("Segoe UI", 10);

            // Clinic info from SettingsHelper
            e.Graphics.DrawString(SettingsHelper.GetSetting("clinic_name"), headerFont, Brushes.Black, 100, y);
            y += lineHeight;
            e.Graphics.DrawString(SettingsHelper.GetSetting("clinic_address"), normalFont, Brushes.Black, 100, y);
            y += lineHeight;
            e.Graphics.DrawString($"Tel: {SettingsHelper.GetSetting("clinic_tel")} | Mobile: {SettingsHelper.GetSetting("clinic_mobile")}", normalFont, Brushes.Black, 100, y);
            y += lineHeight * 2;

            // Patient info
            string patientName = PatientDataHelper.GetPatientValue(_patientId, "full_name");
            e.Graphics.DrawString($"Patient Name: {patientName}        Date: {DateTime.Now:yyyy-MM-dd}", normalFont, Brushes.Black, 50, y);
            y += lineHeight * 2;

            // Header for items
            e.Graphics.DrawString("Item Name               Qty      Description", normalFont, Brushes.Black, 50, y);
            y += lineHeight;
            e.Graphics.DrawLine(Pens.Black, 50, y, e.PageBounds.Width - 50, y);
            y += lineHeight;

            // Items with notes
            foreach (DataGridViewRow row in dgvSelectedItems.Rows)
            {
                if (row.IsNewRow) continue;

                string itemName = row.Cells["item_name"].Value.ToString();
                int qty = Convert.ToInt32(row.Cells["quantity"].Value);
                string description = row.Cells["description"].Value.ToString();
                int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                string note = itemNotes.ContainsKey(itemId) ? itemNotes[itemId] : "";

                e.Graphics.DrawString($"{itemName,-22} {qty,-7} {description}", normalFont, Brushes.Black, 50, y);
                y += lineHeight;

                if (!string.IsNullOrEmpty(note))
                {
                    e.Graphics.DrawString($"- {note}", normalFont, Brushes.Black, 60, y);
                    y += lineHeight + 5;
                }
            }

            y += lineHeight;
            e.Graphics.DrawLine(Pens.Black, 50, y, e.PageBounds.Width - 50, y);
            y += lineHeight;
            e.Graphics.DrawString("MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS: _____________________    LICENSE # 99566", normalFont, Brushes.Black, 50, y);
        };

        PrintPreviewDialog preview = new PrintPreviewDialog { Document = pd, Width = 800, Height = 600 };
        preview.ShowDialog();
    }


}
}
