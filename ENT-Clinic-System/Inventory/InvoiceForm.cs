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
        private bool isManualDiscount = false;

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
            selectedItems.Columns.Add("apply_discount", typeof(bool));

            dgvSelectedItems.DataSource = selectedItems;

            dgvSelectedItems.Columns["item_id"].Visible = false;
            dgvSelectedItems.Columns["apply_discount"].Visible = false;

            dgvSelectedItems.Columns["item_name"].HeaderText = "Item Name";
            dgvSelectedItems.Columns["category"].HeaderText = "Category";
            dgvSelectedItems.Columns["description"].HeaderText = "Description";
            dgvSelectedItems.Columns["unit_price"].HeaderText = "Price";
            dgvSelectedItems.Columns["quantity"].HeaderText = "Quantity";

            dgvSelectedItems.Columns["item_name"].ReadOnly = true;
            dgvSelectedItems.Columns["category"].ReadOnly = true;
            dgvSelectedItems.Columns["description"].ReadOnly = true;
            dgvSelectedItems.Columns["unit_price"].ReadOnly = true;
            dgvSelectedItems.Columns["quantity"].ReadOnly = false;
            LoadPatientsFromPrescriptions();
            try
            {
                dgvAvailableItems.Columns["created_at"].Visible = false;
                dgvAvailableItems.Columns["updated_at"].Visible = false;
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

                    // 1️⃣ Get distinct patient_ids from prescription table **for today only**
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

                    // 2️⃣ If there are patient IDs, load their details into dgvPatients
                    if (patientIds.Count > 0)
                    {
                        string idsString = string.Join(",", patientIds);

                        // Query patients table for these IDs
                        string sqlPatients = $"SELECT patient_id, full_name FROM patients WHERE patient_id IN ({idsString}) ORDER BY full_name";
                        using (var cmd = new MySqlCommand(sqlPatients, conn))
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dtPatients = new DataTable();
                            adapter.Fill(dtPatients);
                            dgvPatients.DataSource = dtPatients;
                        }
                    }
                    else
                    {
                        dgvPatients.DataSource = null; // clear dgv if no patients today
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
            dgvAvailableItems.DataSource = helper.GetAllItems();
        }

        // 🔹 Hook events
        private void HookEvents()
        {
            dgvAvailableItems.CellDoubleClick += DgvAvailableItems_CellDoubleClick;
            dgvSelectedItems.CellEndEdit += DgvSelectedItems_CellEndEdit;
            dgvSelectedItems.KeyDown += DgvSelectedItems_KeyDown;
            chekApplyDiscount.CheckedChanged += (s, e) => CalculateTotals();

            btnSave.Click += BtnSave_Click;
            txtAmountReceived.TextChanged += TxtAmountReceived_TextChanged;
            dgvPatients.CellClick += DgvPatients_CellClick;
            dgvPrescriptions.CellDoubleClick += DgvPrescriptions_CellDoubleClick;

        }
        private void DgvPrescriptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header clicks

            DataGridViewRow row = dgvPrescriptions.Rows[e.RowIndex];

            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
            string itemName = row.Cells["item_name"].Value?.ToString() ?? "";
            string category = row.Cells["category"].Value?.ToString() ?? "";
            string description = row.Cells["description"].Value?.ToString() ?? "";
            decimal price = row.Cells["selling_price"].Value is DBNull ? 0m : Convert.ToDecimal(row.Cells["selling_price"].Value);

            int quantity = row.Cells["quantity"] != null ? Convert.ToInt32(row.Cells["quantity"].Value) : 1;

            // Check if the item already exists in selectedItems
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
            {
                // Add quantity if already exists
                existingRow["quantity"] = (int)existingRow["quantity"] + quantity;
            }
            else
            {
                // Forward all fields
                selectedItems.Rows.Add(itemId, itemName, category, description, price, quantity, chekApplyDiscount.Checked);
            }

            CalculateTotals();
        }



        private void DgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header clicks

            try
            {
                // Get selected patient ID
                int patientId = Convert.ToInt32(dgvPatients.Rows[e.RowIndex].Cells["patient_id"].Value);
                // Get patient full name from the same row
                customerName = dgvPatients.Rows[e.RowIndex].Cells["full_name"].Value?.ToString();


                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // Load prescriptions for this patient, summing quantities if multiple
                    string sql = @"
                        SELECT 
                            p.item_id, 
                            i.item_name, 
                            i.description,
                            i.category,
                            i.selling_price,
                            SUM(p.quantity) AS quantity
                        FROM prescription p
                        INNER JOIN items i ON p.item_id = i.item_id
                        WHERE p.patient_id = @patientId
                        GROUP BY p.item_id, i.item_name, i.description, i.category, i.selling_price
                        ";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@patientId", patientId);

                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dtPrescriptions = new DataTable();
                            adapter.Fill(dtPrescriptions);

                            // Bind to dgvPrescription
                            dgvPrescriptions.DataSource = dtPrescriptions;

                            // Hide item_id if needed
                            if (dgvPrescriptions.Columns.Contains("item_id"))
                                dgvPrescriptions.Columns["item_id"].Visible = false;

                            // Set headers
                            dgvPrescriptions.Columns["item_name"].HeaderText = "Item Name";
                            dgvPrescriptions.Columns["description"].HeaderText = "Description";
                            dgvPrescriptions.Columns["quantity"].HeaderText = "Quantity";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load prescriptions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔹 Add item from inventory
        private void DgvAvailableItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvAvailableItems.Rows[e.RowIndex];

                int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                string itemName = row.Cells["item_name"].Value?.ToString() ?? "";
                string category = row.Cells["category"].Value?.ToString() ?? "";        // new
                string description = row.Cells["description"].Value?.ToString() ?? "";  // new
                decimal price = row.Cells["selling_price"].Value != DBNull.Value
                                ? Convert.ToDecimal(row.Cells["selling_price"].Value)
                                : 0m;

                // Check if item already exists
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
                {
                    existingRow["quantity"] = (int)existingRow["quantity"] + 1;
                }
                else
                {
                    selectedItems.Rows.Add(itemId, itemName, category, description, price, 1, chekApplyDiscount.Checked);
                }

                CalculateTotals();
            }
        }


        // 🔹 Handle quantity changes
        private void DgvSelectedItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvSelectedItems.Columns["quantity"].Index && e.RowIndex >= 0)
            {
                var cell = dgvSelectedItems.Rows[e.RowIndex].Cells["quantity"];

                if (!int.TryParse(cell.Value?.ToString(), out int qty) || qty <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity (must be greater than 0).",
                                    "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cell.Value = 1;
                }
            }
            CalculateTotals();
        }

        private void DgvSelectedItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvSelectedItems.CurrentRow != null && !dgvSelectedItems.CurrentRow.IsNewRow)
            {
                RemoveSelectedItem();
            }
        }

        private void RemoveSelectedItem()
        {
            if (dgvSelectedItems.CurrentRow != null && !dgvSelectedItems.CurrentRow.IsNewRow)
            {
                DialogResult confirm = MessageBox.Show("Are you sure you want to remove this item?",
                                                       "Remove Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    dgvSelectedItems.Rows.RemoveAt(dgvSelectedItems.CurrentRow.Index);
                    CalculateTotals();
                }
            }
        }

        // 🔹 Calculate totals
        // 🔹 Calculate totals with proper discount handling
        private void CalculateTotals()
        {
            decimal subtotal = 0;
            decimal totalDiscount = 0;
            decimal totalTax = 0;
            decimal netTotal = 0;

            // Get system settings for discount and tax
            decimal discountPercent = 0;
            decimal taxPercent = 0;
            decimal.TryParse(SettingsHelper.GetSetting("discount_percentage"), out discountPercent);
            decimal.TryParse(SettingsHelper.GetSetting("tax_percentage"), out taxPercent);

            foreach (DataRow row in selectedItems.Rows)
            {
                int qty = Convert.ToInt32(row["quantity"]);
                decimal price = Convert.ToDecimal(row["unit_price"]);
                bool applyDiscount = Convert.ToBoolean(row["apply_discount"]);

                // Step 1: Subtotal for this item
                decimal itemSubtotal = price * qty;

                // Step 2: Discount applied only if checkbox and item-level applyDiscount is true
                decimal itemDiscount = (chekApplyDiscount.Checked && applyDiscount)
                                       ? itemSubtotal * (discountPercent / 100)
                                       : 0;

                // Step 3: Price after discount
                decimal priceAfterDiscount = itemSubtotal - itemDiscount;

                // Step 4: Tax
                decimal taxAmount = priceAfterDiscount * (taxPercent / 100);

                // Step 5: Final price for this item
                decimal finalPrice = priceAfterDiscount + taxAmount;

                // Step 6: Add to totals
                subtotal += itemSubtotal;
                totalDiscount += itemDiscount;
                totalTax += taxAmount;
                netTotal += finalPrice;
            }

            // Step 7: Update UI
            txtSubtotal.Text = subtotal.ToString("N2");
            txtDiscount.Text = totalDiscount.ToString("N2");
            txtTax.Text = totalTax.ToString("N2");
            txtNetTotal.Text = netTotal.ToString("N2");

            // Step 8: Update change due
            UpdateChangeDue();
        }






        private void TxtAmountReceived_TextChanged(object sender, EventArgs e)
        {
            UpdateChangeDue();
        }

        private void UpdateChangeDue()
        {
            if (decimal.TryParse(txtAmountReceived.Text, out decimal received) &&
                decimal.TryParse(txtNetTotal.Text, System.Globalization.NumberStyles.Currency, null, out decimal total))
            {
                decimal change = received - total;
                txtChange.Text = change >= 0 ? change.ToString("N2") : "0.00";
            }
            else
            {
                txtChange.Text = "0.00";
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


            if(customerName == string.Empty)
            {
                customerName = "Walk-in";
            }

            decimal amountReceived = 0;

            // Check if the amount received is a valid decimal
            if (!decimal.TryParse(txtAmountReceived.Text, out amountReceived))
            {
                MessageBox.Show("Please enter a valid amount received.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the amount is greater than zero
            if (amountReceived <= 0)
            {
                MessageBox.Show("Amount received must be greater than zero.", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Parse net total
            if (!decimal.TryParse(txtNetTotal.Text, System.Globalization.NumberStyles.Currency, null, out decimal netTotal))
            {
                MessageBox.Show("Cannot read total amount. Please check items.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the amount received is sufficient
            if (amountReceived < netTotal)
            {
                MessageBox.Show($"The amount received (₱{amountReceived:N2}) is less than the total amount due (₱{netTotal:N2}). Please enter the correct amount.",
                                "Insufficient Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // 🔹 Save invoice with amount received
            currentInvoiceId = helper.AddInvoice(customerName, selectedItems, amountReceived);

            if (currentInvoiceId > 0)
            {
                MessageBox.Show("Invoice saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔹 Ask if user wants to print the receipt
                DialogResult printConfirm = MessageBox.Show("Do you want to print the receipt?", "Print Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (printConfirm == DialogResult.Yes)
                {
                    if (currentInvoiceId <= 0)
                    {
                        MessageBox.Show("Please save the invoice first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    InvoicePrinter printer = new InvoicePrinter(currentInvoiceId);
                    printer.PrintReceipt();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Error saving invoice.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // 🔹 Print receipt
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (currentInvoiceId <= 0)
            {
                MessageBox.Show("Please save the invoice first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InvoicePrinter printer = new InvoicePrinter(currentInvoiceId);
            printer.PrintReceipt();
        }

        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            string taxPercent = SettingsHelper.GetSetting("tax_percentage");
            string discountPercent = SettingsHelper.GetSetting("discount_percentage");
            chekApplyDiscount.Text = $"({discountPercent}%)Discount ";
            lblTax.Text = $"Tax ({taxPercent}%)";

        }
        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotals();
        }


        private void chekApplyDiscount_CheckedChanged(object sender, EventArgs e)
        {
            // Update all items' apply_discount column based on checkbox
            foreach (DataRow row in selectedItems.Rows)
            {
                row["apply_discount"] = chekApplyDiscount.Checked;
            }

            txtDiscount.ReadOnly = true;
            CalculateTotals();
        }


    }
}
