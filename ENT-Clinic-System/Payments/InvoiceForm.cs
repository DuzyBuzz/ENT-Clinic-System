using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.Inventory
{
    public partial class InvoiceForm : Form
    {
        private InventoryHelper helper;
        private DataTable selectedItems;
        private int currentInvoiceId;
        private string customerName = string.Empty;

        public InvoiceForm()
        {
            InitializeComponent();
            helper = new InventoryHelper();
            LoadAvailableItems();
            HookEvents();
            InitializeSelectedItemsTable();
        }

        // 🔹 Setup selected items table
        private void InitializeSelectedItemsTable()
        {
            selectedItems = new DataTable();
            selectedItems.Columns.Add("item_id", typeof(int));
            selectedItems.Columns.Add("item_name", typeof(string));
            selectedItems.Columns.Add("category", typeof(string));
            selectedItems.Columns.Add("description", typeof(string));
            selectedItems.Columns.Add("unit_price", typeof(decimal));
            selectedItems.Columns.Add("quantity", typeof(int));

            selectedItemsDataGridView.DataSource = selectedItems;

            selectedItemsDataGridView.Columns["item_id"].Visible = false;

            selectedItemsDataGridView.Columns["item_name"].HeaderText = "Item Name";
            selectedItemsDataGridView.Columns["category"].HeaderText = "Category";
            selectedItemsDataGridView.Columns["description"].HeaderText = "Description";
            selectedItemsDataGridView.Columns["unit_price"].HeaderText = "Price";
            selectedItemsDataGridView.Columns["quantity"].HeaderText = "Quantity";

            selectedItemsDataGridView.Columns["item_name"].ReadOnly = true;
            selectedItemsDataGridView.Columns["category"].ReadOnly = true;
            selectedItemsDataGridView.Columns["description"].ReadOnly = true;
            selectedItemsDataGridView.Columns["unit_price"].ReadOnly = true;

            LoadPatientsFromPrescriptions();

            try
            {
                availableItemsDataGridView.Columns["created_at"].Visible = false;
                availableItemsDataGridView.Columns["updated_at"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message);
            }
        }

        private void LoadPatientsFromPrescriptions()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // Get distinct patients for today
                    string sqlPatientIds = "SELECT DISTINCT patient_id FROM prescription WHERE DATE(created_at) = CURDATE()";
                    List<int> patientIds = new List<int>();

                    using (var cmd = new MySqlCommand(sqlPatientIds, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patientIds.Add(reader.GetInt32("patient_id"));
                        }
                    }

                    if (patientIds.Count > 0)
                    {
                        string idsString = string.Join(",", patientIds);
                        string sqlPatients = $"SELECT patient_id, full_name FROM patients WHERE patient_id IN ({idsString}) ORDER BY full_name";

                        using (var cmd = new MySqlCommand(sqlPatients, conn))
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dtPatients = new DataTable();
                            adapter.Fill(dtPatients);
                            patientsDataGridView.DataSource = dtPatients;
                        }
                    }
                    else
                    {
                        patientsDataGridView.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load patients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 Load inventory items
        private void LoadAvailableItems()
        {
            availableItemsDataGridView.DataSource = helper.GetAllItems();
        }

        // 🔹 Hook events
        private void HookEvents()
        {
            availableItemsDataGridView.CellDoubleClick += DgvAvailableItems_CellDoubleClick;
            selectedItemsDataGridView.CellEndEdit += DgvSelectedItems_CellEndEdit;
            selectedItemsDataGridView.KeyDown += DgvSelectedItems_KeyDown;

            saveButton.Click += BtnSave_Click;
            itemsAmountRecievedTextBox.TextChanged += TxtAmountReceived_TextChanged;
            patientsDataGridView.CellClick += DgvPatients_CellClick;
            prescriptionDataGridView.CellDoubleClick += DgvPrescriptions_CellDoubleClick;
            discountPercentComboBox.SelectedIndexChanged += (s, e) => CalculateTotals();
        }

        // 🔹 Double-click prescription item → add to invoice
        private void DgvPrescriptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = prescriptionDataGridView.Rows[e.RowIndex];

            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
            string itemName = row.Cells["item_name"].Value?.ToString() ?? "";
            string category = row.Cells["category"].Value?.ToString() ?? "";
            string description = row.Cells["description"].Value?.ToString() ?? "";
            decimal price = row.Cells["selling_price"].Value is DBNull ? 0m : Convert.ToDecimal(row.Cells["selling_price"].Value);
            int quantity = row.Cells["quantity"] != null ? Convert.ToInt32(row.Cells["quantity"].Value) : 1;

            // Check if already exists
            DataRow existingRow = null;
            foreach (DataRow r in selectedItems.Rows)
            {
                if ((int)r["item_id"] == itemId)
                {
                    existingRow = r;
                    break;
                }
            }

            if (existingRow != null)
                existingRow["quantity"] = (int)existingRow["quantity"] + quantity;
            else
                selectedItems.Rows.Add(itemId, itemName, category, description, price, quantity);

            CalculateTotals();
        }

        // 🔹 Select patient → load prescriptions
        private void DgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                int patientId = Convert.ToInt32(patientsDataGridView.Rows[e.RowIndex].Cells["patient_id"].Value);
                customerName = patientsDataGridView.Rows[e.RowIndex].Cells["full_name"].Value?.ToString();

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT p.item_id, i.item_name, i.description, i.category, i.selling_price, SUM(p.quantity) AS quantity
                        FROM prescription p
                        INNER JOIN items i ON p.item_id = i.item_id
                        WHERE p.patient_id = @patientId
                        GROUP BY p.item_id, i.item_name, i.description, i.category, i.selling_price";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@patientId", patientId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dtPrescriptions = new DataTable();
                            adapter.Fill(dtPrescriptions);
                            prescriptionDataGridView.DataSource = dtPrescriptions;

                            if (prescriptionDataGridView.Columns.Contains("item_id"))
                                prescriptionDataGridView.Columns["item_id"].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load prescriptions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 Double-click inventory item → add to invoice
        private void DgvAvailableItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = availableItemsDataGridView.Rows[e.RowIndex];

            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
            string itemName = row.Cells["item_name"].Value?.ToString() ?? "";
            string category = row.Cells["category"].Value?.ToString() ?? "";
            string description = row.Cells["description"].Value?.ToString() ?? "";
            decimal price = row.Cells["selling_price"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["selling_price"].Value) : 0m;

            DataRow existingRow = null;
            foreach (DataRow r in selectedItems.Rows)
            {
                if ((int)r["item_id"] == itemId)
                {
                    existingRow = r;
                    break;
                }
            }

            if (existingRow != null)
                existingRow["quantity"] = (int)existingRow["quantity"] + 1;
            else
                selectedItems.Rows.Add(itemId, itemName, category, description, price, 1);

            CalculateTotals();
        }

        // 🔹 Quantity validation
        private void DgvSelectedItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == selectedItemsDataGridView.Columns["quantity"].Index && e.RowIndex >= 0)
            {
                var cell = selectedItemsDataGridView.Rows[e.RowIndex].Cells["quantity"];
                if (!int.TryParse(cell.Value?.ToString(), out int qty) || qty <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cell.Value = 1;
                }
            }
            CalculateTotals();
        }

        private void DgvSelectedItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && selectedItemsDataGridView.CurrentRow != null && !selectedItemsDataGridView.CurrentRow.IsNewRow)
            {
                selectedItemsDataGridView.Rows.RemoveAt(selectedItemsDataGridView.CurrentRow.Index);
                CalculateTotals();
            }
        }

        // 🔹 Calculate totals (Subtotal → Discount → Net)
        private void CalculateTotals()
        {
            decimal subtotal = 0;
            foreach (DataRow row in selectedItems.Rows)
            {
                int qty = Convert.ToInt32(row["quantity"]);
                decimal price = Convert.ToDecimal(row["unit_price"]);
                subtotal += price * qty;
            }

            // Get discount from ComboBox
            decimal discountPercent = 0;
            if (discountPercentComboBox.SelectedItem != null && decimal.TryParse(discountPercentComboBox.Text.ToString(), out decimal val))
                discountPercent = val;

            decimal discountAmount = subtotal * (discountPercent / 100);
            decimal netTotal = subtotal - discountAmount;

            // Update UI
            subTotalTextBox.Text = subtotal.ToString("N2");
            discountTextBox.Text = discountAmount.ToString("N2");
            totalAmountTextBox.Text = netTotal.ToString("N2");

            UpdateChangeDue();
        }

        private void TxtAmountReceived_TextChanged(object sender, EventArgs e) => UpdateChangeDue();

        private void UpdateChangeDue()
        {
            if (decimal.TryParse(itemsAmountRecievedTextBox.Text, out decimal received) &&
                decimal.TryParse(totalAmountTextBox.Text, out decimal total))
            {
                decimal change = received - total;
                changeTextBox.Text = change >= 0 ? change.ToString("N2") : "0.00";
            }
            else
            {
                changeTextBox.Text = "0.00";
            }
        }

        // 🔹 Save invoice
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (selectedItems.Rows.Count == 0)
            {
                MessageBox.Show("Please add items before saving.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(customerName))
                customerName = "Walk-in";

            if (!decimal.TryParse(itemsAmountRecievedTextBox.Text, out decimal amountReceived) || amountReceived <= 0)
            {
                MessageBox.Show("Enter valid amount received.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(totalAmountTextBox.Text, out decimal netTotal))
            {
                MessageBox.Show("Error calculating totals.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (amountReceived < netTotal)
            {
                MessageBox.Show("Amount received is less than total.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ Save invoice (with discount, no tax, with note)
            string note = noteComboBox.Text;
            string invoiceType = "ITEMS";
            currentInvoiceId = helper.AddInvoice(customerName, selectedItems, amountReceived, discountPercentComboBox.Text?.ToString(), note, invoiceType);

            if (currentInvoiceId > 0)
            {
                MessageBox.Show("Invoice saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (MessageBox.Show("Print receipt?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    InvoicePrinter printer = new InvoicePrinter(currentInvoiceId);
                    printer.PrintReceipt();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Error saving invoice.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
