using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Payments
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
        }

        // ================================
        // 🔹 Setup selected items table
        // ================================

        // 🔹 Load patients with prescriptions
        // ================================
        private void LoadPatientsFromPrescriptions()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

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

        // ================================
        // 🔹 Load inventory items
        // ================================
        private void LoadAvailableItems()
        {
            availableItemsDataGridView.DataSource = helper.GetAllItems();
        }

        // ================================
        // 🔹 Hook events
        // ================================
        private void HookEvents()
        {
            availableItemsDataGridView.CellDoubleClick += DgvAvailableItems_CellDoubleClick;
            selectedItemsDataGridView.CellEndEdit += DgvSelectedItems_CellEndEdit;
            selectedItemsDataGridView.KeyDown += DgvSelectedItems_KeyDown;

            saveButton.Click += BtnSave_Click;
            itemsAmountRecievedNumericUpDown.TextChanged += (s, e) => UpdateChangeDue();
            patientsDataGridView.CellClick += DgvPatients_CellClick;
            prescriptionDataGridView.CellDoubleClick += DgvPrescriptions_CellDoubleClick;

            // 🔹 Update totals when discount changes (real-time)
            discountPercentComboBox.TextChanged += (s, e) => CalculateTotals();
        }

        // ================================
        // 🔹 Add prescription item
        // ================================
        private void DgvPrescriptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = prescriptionDataGridView.Rows[e.RowIndex];

            int prescriptionId = row.Cells["prescription_id"] != null ? Convert.ToInt32(row.Cells["prescription_id"].Value) : 0;
            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);

            // Build display name from brand/generic/strength/dosage
            string brand = row.Cells["brand_name"]?.Value?.ToString() ?? "";
            string generic = row.Cells["generic_name"]?.Value?.ToString() ?? "";
            string strength = row.Cells["strength"]?.Value?.ToString() ?? "";
            string dosage = row.Cells["dosage"]?.Value?.ToString() ?? "";
            string itemDisplayName = string.Join(" ", new[] { brand, generic, strength, dosage }.Where(s => !string.IsNullOrWhiteSpace(s)));

            string category = row.Cells["category"].Value?.ToString() ?? "";
            string description = row.Cells["description"].Value?.ToString() ?? "";
            decimal price = row.Cells["selling_price"].Value is DBNull ? 0m : Convert.ToDecimal(row.Cells["selling_price"].Value);
            int quantity = row.Cells["quantity"] != null ? Convert.ToInt32(row.Cells["quantity"].Value) : 1;

            DataRow existingRow = null;
            foreach (DataRow r in selectedItems.Rows)
            {
                if ((int)r["item_id"] == itemId && (int)r["prescription_id"] == prescriptionId)
                {
                    existingRow = r;
                    break;
                }
            }

            if (existingRow != null)
                existingRow["quantity"] = (int)existingRow["quantity"] + quantity;
            else
                selectedItems.Rows.Add(itemId, itemDisplayName, category, description, price, quantity, prescriptionId);

            CalculateTotals();
        }

        // ================================
        // 🔹 Select patient → load prescriptions
        // ================================
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
                        SELECT p.prescription_id, p.item_id,
                               i.brand_name, i.generic_name, i.strength, i.dosage, p.sig,
                               i.description, i.category, i.selling_price, SUM(p.quantity) AS quantity
                        FROM prescription p
                        INNER JOIN items i ON p.item_id = i.item_id
                        WHERE p.patient_id = @patientId
                        GROUP BY p.prescription_id, p.item_id, i.brand_name, i.generic_name, i.strength, i.dosage, i.description, i.category, i.selling_price";

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

        // ================================
        // 🔹 Double-click inventory item
        // ================================
        private void DgvAvailableItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = availableItemsDataGridView.Rows[e.RowIndex];

            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);

            // Build display name from available items columns (brand + generic + strength + dosage)
            string brand = row.Cells["brand_name"]?.Value?.ToString() ?? "";
            string generic = row.Cells["generic_name"]?.Value?.ToString() ?? "";
            string strength = row.Cells["strength"]?.Value?.ToString() ?? "";
            string dosage = row.Cells["dosage"]?.Value?.ToString() ?? "";
            string itemDisplayName = string.Join(" ", new[] { brand, generic, strength, dosage }.Where(s => !string.IsNullOrWhiteSpace(s)));

            string category = row.Cells["category"]?.Value?.ToString() ?? "";
            string description = row.Cells["description"]?.Value?.ToString() ?? "";
            decimal price = row.Cells["selling_price"].Value != DBNull.Value ? Convert.ToDecimal(row.Cells["selling_price"].Value) : 0m;

            DataRow existingRow = null;
            foreach (DataRow r in selectedItems.Rows)
            {
                if ((int)r["item_id"] == itemId && (int)r["prescription_id"] == 0) // 🔹 for non-prescription items
                {
                    existingRow = r;
                    break;
                }
            }

            if (existingRow != null)
                existingRow["quantity"] = (int)existingRow["quantity"] + 1;
            else
                selectedItems.Rows.Add(itemId, itemDisplayName, category, description, price, 1, 0); // 🔹 prescription_id = 0

            CalculateTotals();
        }

        // ================================
        // 🔹 Validate quantity edits
        // ================================
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

        // ================================
        // 🔹 Calculate totals
        // ================================
        private void CalculateTotals()
        {
            decimal subtotal = 0;
            foreach (DataRow row in selectedItems.Rows)
            {
                int qty = Convert.ToInt32(row["quantity"]);
                decimal price = Convert.ToDecimal(row["unit_price"]);
                subtotal += price * qty;
            }

            decimal discountPercent = 0;
            if (!string.IsNullOrWhiteSpace(discountPercentComboBox.Text) &&
                decimal.TryParse(discountPercentComboBox.Text, out decimal val))
            {
                discountPercent = val;
            }

            decimal discountAmount = subtotal * (discountPercent / 100);
            decimal netTotal = subtotal - discountAmount;

            subTotalTextBox.Text = subtotal.ToString("N2");
            discountTextBox.Text = discountAmount.ToString("N2");
            totalAmountTextBox.Text = netTotal.ToString("N2");

            UpdateChangeDue();
        }

        // ================================
        // 🔹 Change due calculation
        // ================================
        private void UpdateChangeDue()
        {
            if (decimal.TryParse(itemsAmountRecievedNumericUpDown.Text, out decimal received) &&
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

        // ================================
        // 🔹 Save invoice
        // ================================
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 1. Check if there are items
            if (selectedItems.Rows.Count == 0)
            {
                MessageBox.Show("Please add items before saving.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Default customer name
            if (string.IsNullOrEmpty(customerName))
                customerName = "Walk-in";

            // 3. Parse amount received
            if (!decimal.TryParse(itemsAmountRecievedNumericUpDown.Text, out decimal amountReceived) || amountReceived <= 0)
            {
                MessageBox.Show("Enter valid amount received.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Calculate subtotal from selected items (qty × unit price)
            decimal subtotal = 0;
            foreach (DataRow row in selectedItems.Rows)
            {
                if (row["quantity"] != DBNull.Value && row["unit_price"] != DBNull.Value)
                {
                    subtotal += Convert.ToDecimal(row["quantity"]) * Convert.ToDecimal(row["unit_price"]);
                }
            }

            // 5. Parse discount percent from ComboBox
            decimal discountPercent = 0;
            if (decimal.TryParse(discountPercentComboBox.Text?.Replace("%", ""), out decimal parsedPercent))
                discountPercent = parsedPercent;

            // 6. Calculate discount amount
            decimal discountAmount = Math.Round(subtotal * (discountPercent / 100), 2);

            // 7. Calculate net total
            decimal netTotal = subtotal - discountAmount;

            // 8. Validate total against textbox value (optional cross-check)
            if (!decimal.TryParse(totalAmountTextBox.Text, out decimal netTotalFromText) || netTotalFromText != netTotal)
            {
                // If mismatch, update the textbox so it shows the correct calculation
                totalAmountTextBox.Text = netTotal.ToString("0.00");
            }

            // 9. Validate if received money is enough
            if (amountReceived < netTotal)
            {
                MessageBox.Show("Amount received is less than total.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 10. Calculate change due
            decimal changeDue = amountReceived - netTotal;

            // 11. Prepare other values
            string note = noteComboBox.Text;
            string invoiceType = "ITEMS";

            // 12. Save invoice
            int currentInvoiceId = helper.AddInvoice(
                customerName,
                selectedItems,
                subtotal,
                discountAmount,
                netTotal,
                amountReceived,
                changeDue,
                discountPercentComboBox.Text?.ToString(),
                note,
                invoiceType
            );

            // 13. Handle result
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

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            // Populate combobox items from the same column
            ComboBoxCollectionHelper.PopulateComboBox(
                discountPercentComboBox,
                "invoices",
                "discount_percent"
            );
            // Populate combobox items from the same column
            ComboBoxCollectionHelper.PopulateComboBox(
                noteComboBox,
                "invoices",
                "note"
            );
        }

        private void searchItemtButton_Click(object sender, EventArgs e)
        {
            SearchHelper.Search(
                dgv: availableItemsDataGridView,
                tableName: "items",
                columnNames: new string[] { "brand_name", "generic_name", "description" }, // search brand/generic instead of item_name
                filterControl: searchItemsTextBox
            );
        }

        private void refreshPatientsButton_Click(object sender, EventArgs e)
        {
            LoadAvailableItems();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

        }

        private void saveButton_Click_1(object sender, EventArgs e)
        {

        }
    }
}