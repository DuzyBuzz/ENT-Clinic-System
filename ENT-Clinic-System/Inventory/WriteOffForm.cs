using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.Inventory
{
    public partial class WriteOffForm : Form
    {
        public WriteOffForm()
        {
            InitializeComponent();
        }

        private void WriteOffForm_Load(object sender, EventArgs e)
        {
            LoadAvailableItems();
            LoadWriteOffHistory();
        }

        // ==================== Load Available Items ====================
        private void LoadAvailableItems()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT item_id, item_name, stock FROM inventory ORDER BY item_name";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    availableItemsDataGridView.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading available items: " + ex.Message);
            }
        }

        // ==================== Load Write-Off History ====================
        private void LoadWriteOffHistory()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT w.write_off_id, i.item_name, w.quantity, w.reason, w.date
                                     FROM write_off w
                                     INNER JOIN inventory i ON w.item_id = i.item_id
                                     ORDER BY w.date DESC";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvWriteOffHistory.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading write-off history: " + ex.Message);
            }
        }

        // ==================== Add Item to Selected Items ====================
        private void availableItemsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header clicks
            DataGridViewRow row = availableItemsDataGridView.Rows[e.RowIndex];

            // Prevent duplicates
            foreach (DataGridViewRow r in selectedItemsDataGridView.Rows)
            {
                if (r.Cells["item_id"].Value.ToString() == row.Cells["item_id"].Value.ToString())
                    return;
            }

            // Add to selected items grid
            DataTable dt;
            if (selectedItemsDataGridView.DataSource == null)
            {
                dt = new DataTable();
                dt.Columns.Add("item_id");
                dt.Columns.Add("item_name");
                dt.Columns.Add("stock");
            }
            else
            {
                dt = (DataTable)selectedItemsDataGridView.DataSource;
            }

            dt.Rows.Add(row.Cells["item_id"].Value, row.Cells["item_name"].Value, row.Cells["stock"].Value);
            selectedItemsDataGridView.DataSource = dt;
        }

        // ==================== Submit Write-Off ====================
        private void btnAddWriteOff_Click(object sender, EventArgs e)
        {
            if (selectedItemsDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item from the list.");
                return;
            }

            var selectedRow = selectedItemsDataGridView.SelectedRows[0];
            int itemId = Convert.ToInt32(selectedRow.Cells["item_id"].Value);
            int quantity = (int)numericQuantity.Value;
            string reason = txtReason.Text.Trim();
            DateTime date = dtpWriteOffDate.Value;

            if (quantity <= 0)
            {
                MessageBox.Show("Quantity must be greater than 0.");
                return;
            }

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"
                            INSERT INTO write_off (item_id, quantity, reason, date) 
                            VALUES (@itemId, @qty, @reason, @date);
                            UPDATE inventory SET stock = stock - @qty WHERE item_id = @itemId;";
                        cmd.Parameters.AddWithValue("@itemId", itemId);
                        cmd.Parameters.AddWithValue("@qty", quantity);
                        cmd.Parameters.AddWithValue("@reason", reason);
                        cmd.Parameters.AddWithValue("@date", date);

                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows > 0)
                        {
                            MessageBox.Show("✅ Write-Off recorded successfully!");
                            LoadAvailableItems();
                            LoadWriteOffHistory();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error submitting write-off: " + ex.Message);
            }
        }
    }
}
