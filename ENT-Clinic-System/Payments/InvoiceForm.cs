using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Payments
{
    public partial class InvoiceForm : Form
    {
        private readonly InventoryHelper helper;
        private readonly DataTable selectedItems;
        private int currentInvoiceId;
        private string customerName = string.Empty;
        private TableChangeWatcher _prescriptionWatcher;

        // Fonts reused for all grids
        private readonly Font _boldHeaderFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        private readonly Font _regularFont = new Font("Segoe UI", 9F, FontStyle.Regular);

        // Context menu for selected items
        private readonly ContextMenuStrip selectedItemsContextMenu;

        public InvoiceForm()
        {
            InitializeComponent();
            helper = new InventoryHelper();

            // =============================
            // Setup DataTable for selected items
            // =============================
            selectedItems = new DataTable();
            selectedItems.Columns.Add("item_id", typeof(int));
            selectedItems.Columns.Add("brand_name", typeof(string));
            selectedItems.Columns.Add("generic_name", typeof(string));
            selectedItems.Columns.Add("strength", typeof(string));
            selectedItems.Columns.Add("dosage", typeof(string));
            selectedItems.Columns.Add("category", typeof(string));
            selectedItems.Columns.Add("description", typeof(string));
            selectedItems.Columns.Add("unit_price", typeof(decimal));
            selectedItems.Columns.Add("quantity", typeof(int));
            selectedItems.Columns.Add("prescription_id", typeof(int));


            selectedItemsDataGridView.DataSource = selectedItems;

            // Context menu setup (done ONCE)
            selectedItemsContextMenu = new ContextMenuStrip();
            var removeItem = new ToolStripMenuItem("Remove This Item")
            {
                ForeColor = Color.Red
            };
            removeItem.Click += RemoveItem_Click;
            selectedItemsContextMenu.Items.Add(removeItem);

            // Initial load
            LoadAvailableItems();
            LoadPatientsFromPrescriptions();

            // Hook UI events
            HookEvents();
        }

        // ================================
        // FORM LOAD EVENT
        // ================================
        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            // Setup autocomplete and combo boxes
            AutoCompleteHelper.SetupAutoComplete(searchItemsTextBox, "items", new List<string> { "generic_name", "brand_name" });
            ComboBoxCollectionHelper.PopulateComboBox(categoryCombobox, "items", "category");
            AutoCompleteHelper.SetupAutoComplete(categoryCombobox, "items", new List<string> { "category" });

            ComboBoxCollectionHelper.PopulateComboBox(discountPercentComboBox, "invoices", "discount_percent");

            // Start table watcher only once
            _prescriptionWatcher = new TableChangeWatcher(new[] { "prescription" }, () =>
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke((Action)(() =>
                    {
                        RefreshInvoice();
                        Console.WriteLine("Watcher triggered → Refreshed invoice data");
                    }));
                }
            });

            _prescriptionWatcher.Start();

            DGVColumnHeaderFilterHelper.Attach(availableItemsDataGridView);
            DGVColumnHeaderFilterHelper.ResetFilters(availableItemsDataGridView);
        }

        // ================================
        // Load patients with prescriptions
        // ================================
        private void LoadPatientsFromPrescriptions()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string sqlPatientIds = "SELECT DISTINCT patient_id FROM prescription WHERE DATE(created_at) = CURDATE()";
                    var patientIds = new List<int>();

                    using (var cmd = new MySqlCommand(sqlPatientIds, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            patientIds.Add(reader.GetInt32("patient_id"));
                    }

                    if (patientIds.Count > 0)
                    {
                        string idsString = string.Join(",", patientIds);
                        string sqlPatients = $"SELECT patient_id, full_name FROM patients WHERE patient_id IN ({idsString}) ORDER BY full_name";

                        using (var cmd = new MySqlCommand(sqlPatients, conn))
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            var dtPatients = new DataTable();
                            adapter.Fill(dtPatients);
                            patientsDataGridView.DataSource = dtPatients;
                            FormatPatientsGrid();
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

        private void FormatPatientsGrid()
        {
            var dgv = patientsDataGridView;
            if (dgv.Columns.Count == 0) return;

            if (dgv.Columns.Contains("patient_id"))
                dgv.Columns["patient_id"].Visible = false;

            if (dgv.Columns.Contains("full_name"))
                dgv.Columns["full_name"].HeaderText = "Patient Name";

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.Font = _boldHeaderFont;
            dgv.DefaultCellStyle.Font = _regularFont;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // ================================
        // Load available inventory items
        // ================================
        private void LoadAvailableItems()
        {
            availableItemsDataGridView.DataSource = helper.GetAllItems();
            FormatAvailableItemsGrid();
            FormatSelectedItemsGrid();
        }

        // ================================
        // Hook UI Events (called ONCE)
        // ================================
        private void HookEvents()
        {
            availableItemsDataGridView.CellDoubleClick += DgvAvailableItems_CellDoubleClick;
            selectedItemsDataGridView.CellEndEdit += DgvSelectedItems_CellEndEdit;
            selectedItemsDataGridView.KeyDown += DgvSelectedItems_KeyDown;
            selectedItemsDataGridView.CellMouseDown += SelectedItemsDataGridView_CellMouseDown;
            saveButton.Click += BtnSave_Click;
            patientsDataGridView.CellClick += DgvPatients_CellClick;
            prescriptionDataGridView.CellDoubleClick += DgvPrescriptions_CellDoubleClick;
            discountPercentComboBox.TextChanged += (s, e) => CalculateTotals();
            itemsAmountRecievedNumericUpDown.TextChanged += (s, e) => UpdateChangeDue();
            refreshPatientsButton.Click += (s, e) => RefreshInvoice();
            searchItemsTextBox.TextChanged += (s, e) => FilterAvailableItems();
            categoryCombobox.SelectedIndexChanged += (s, e) => FilterAvailableItems();
        }
        /// <summary>
        /// When the user double-clicks a prescription, it will automatically add
        /// that item into the Selected Items list for billing.
        /// </summary>
        private void DgvPrescriptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = prescriptionDataGridView.Rows[e.RowIndex];

            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
            int prescriptionId = Convert.ToInt32(row.Cells["prescription_id"].Value);

            var existingRow = selectedItems.AsEnumerable()
                .FirstOrDefault(r => (int)r["item_id"] == itemId && (int)r["prescription_id"] == prescriptionId);

            if (existingRow != null)
            {
                existingRow["quantity"] = (int)existingRow["quantity"] + Convert.ToInt32(row.Cells["quantity"].Value);
            }
            else
            {
                selectedItems.Rows.Add(
                    itemId,
                    row.Cells["brand_name"].Value,
                    row.Cells["generic_name"].Value,
                    row.Cells["strength"].Value,
                    row.Cells["dosage"].Value,
                    row.Cells["category"].Value,
                    row.Cells["description"].Value,
                    row.Cells["selling_price"].Value,
                    row.Cells["quantity"].Value,
                    prescriptionId
                );
            }

            CalculateTotals();
        }


        // ================================
        // Right-click context menu
        // ================================
        private void SelectedItemsDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                selectedItemsDataGridView.ClearSelection();
                selectedItemsDataGridView.Rows[e.RowIndex].Selected = true;
                selectedItemsContextMenu.Show(Cursor.Position);
            }
        }

        private void RemoveItem_Click(object sender, EventArgs e)
        {
            if (selectedItemsDataGridView.CurrentRow != null && !selectedItemsDataGridView.CurrentRow.IsNewRow)
            {
                selectedItemsDataGridView.Rows.RemoveAt(selectedItemsDataGridView.CurrentRow.Index);
                CalculateTotals();
            }
        }

        // ================================
        // Select patient → load prescriptions
        // ================================
        private void DgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                int patientId = Convert.ToInt32(patientsDataGridView.Rows[e.RowIndex].Cells["patient_id"].Value);
                customerName = patientsDataGridView.Rows[e.RowIndex].Cells["full_name"].Value?.ToString();
                groupBox3.Text = $"Payment of {customerName}";

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
                            var dtPrescriptions = new DataTable();
                            adapter.Fill(dtPrescriptions);
                            prescriptionDataGridView.DataSource = dtPrescriptions;
                            if (prescriptionDataGridView.Columns.Contains("item_id"))
                                prescriptionDataGridView.Columns["item_id"].Visible = false;
                        }
                    }
                }

                FormatPrescriptionGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load prescriptions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatPrescriptionGrid()
        {
            var dgv = prescriptionDataGridView;
            if (dgv.Columns.Count == 0) return;

            string[] hiddenCols = { "prescription_id", "item_id", "created_at", "updated_at" };
            foreach (var col in hiddenCols)
                if (dgv.Columns.Contains(col))
                    dgv.Columns[col].Visible = false;

            void SetHeader(string column, string header)
            {
                if (dgv.Columns.Contains(column))
                    dgv.Columns[column].HeaderText = header;
            }

            SetHeader("brand_name", "Brand Name");
            SetHeader("generic_name", "Generic Name");
            SetHeader("strength", "Strength");
            SetHeader("dosage", "Dosage");
            SetHeader("sig", "Prescription Sig");
            SetHeader("category", "Category");
            SetHeader("selling_price", "Unit Price");
            SetHeader("quantity", "Qty");

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.Font = _boldHeaderFont;
            dgv.DefaultCellStyle.Font = _regularFont;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dgv.Columns.Contains("selling_price"))
            {
                dgv.Columns["selling_price"].DefaultCellStyle.Format = "N2";
                dgv.Columns["selling_price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgv.Columns.Contains("quantity"))
                dgv.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            dgv.RowHeadersVisible = false;
        }

        // ================================
        // Double-click → add item to invoice
        // ================================
        private void DgvAvailableItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = availableItemsDataGridView.Rows[e.RowIndex];

            int itemId = Convert.ToInt32(row.Cells["item_id"].Value);

            // Check if already exists
            var existingRow = selectedItems.AsEnumerable()
                .FirstOrDefault(r => (int)r["item_id"] == itemId && (int)r["prescription_id"] == 0);

            if (existingRow != null)
            {
                existingRow["quantity"] = (int)existingRow["quantity"] + 1;
            }
            else
            {
                selectedItems.Rows.Add(
                    itemId,
                    row.Cells["brand_name"].Value,
                    row.Cells["generic_name"].Value,
                    row.Cells["strength"].Value,
                    row.Cells["dosage"].Value,
                    row.Cells["category"].Value,
                    row.Cells["description"].Value,
                    row.Cells["selling_price"].Value,
                    1,
                    0 // prescription_id = 0 for direct items
                );
            }

            CalculateTotals();
        }


        // ================================
        // Validate edited quantities
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
        // Totals, Change Due, and Save Invoice
        // ================================
        private void CalculateTotals()
        {
            decimal subtotal = selectedItems.AsEnumerable().Sum(r => Convert.ToDecimal(r["unit_price"]) * Convert.ToInt32(r["quantity"]));

            decimal discountPercent = 0;
            decimal.TryParse(discountPercentComboBox.Text?.Replace("%", "").Trim(), out discountPercent);

            decimal discountAmount = Math.Round(subtotal * (discountPercent / 100), 2);
            decimal netTotal = subtotal - discountAmount;

            subTotalTextBox.Text = subtotal.ToString("N2");
            discountTextBox.Text = discountAmount.ToString("N2");
            totalAmountTextBox.Text = netTotal.ToString("N2");

            UpdateChangeDue();
        }

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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (selectedItems.Rows.Count == 0)
            {
                MessageBox.Show("Please add items before saving.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(customerName))
                customerName = "Walk-in";

            if (!decimal.TryParse(itemsAmountRecievedNumericUpDown.Text, out decimal amountReceived) || amountReceived <= 0)
            {
                MessageBox.Show("Enter valid amount received.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal subtotal = selectedItems.AsEnumerable().Sum(r => Convert.ToDecimal(r["unit_price"]) * Convert.ToInt32(r["quantity"]));
            decimal discountPercent = 0;
            decimal.TryParse(discountPercentComboBox.Text?.Replace("%", "").Trim(), out discountPercent);
            decimal discountAmount = Math.Round(subtotal * (discountPercent / 100), 2);
            decimal netTotal = subtotal - discountAmount;
            decimal changeDue = amountReceived - netTotal;

            string note = noteComboBox.Text;
            string invoiceType = "ITEMS";

            currentInvoiceId = helper.AddInvoice(customerName, selectedItems, subtotal, discountAmount, netTotal, amountReceived, changeDue, discountPercent.ToString(), note, invoiceType);
            Console.WriteLine("customer name "+customerName);
            if (currentInvoiceId > 0)
            {
                MessageBox.Show("Invoice saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (MessageBox.Show("Print receipt?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var printer = new InvoicePrinter(currentInvoiceId);
                    printer.PrintReceipt();
                }

                selectedItems.Clear();
                RefreshInvoice();
            }
            else
            {
                MessageBox.Show("Error saving invoice.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================================
        // Refresh invoice data safely
        // ================================
        private void RefreshInvoice()
        {
            LoadAvailableItems();
            LoadPatientsFromPrescriptions();
            customerName = string.Empty;
            groupBox3.Text = "Customer Payment (Walk-in)";

            selectedItems.Rows.Clear();
            prescriptionDataGridView.DataSource = null;
            ClearInvoiceFields();

        }
        private void ClearInvoiceFields()
        {
            subTotalTextBox.Text = "0.00";
            discountTextBox.Text = "0.00";
            discountPercentComboBox.Text = "";
            totalAmountTextBox.Text = "0.00";
            itemsAmountRecievedNumericUpDown.Text = "0.00";
            changeTextBox.Text = "0.00";
            noteComboBox.Text = "";

        }



        // ================================
        // GRID FORMATTING HELPERS
        // ================================
        private void FormatAvailableItemsGrid()
        {
            var dgv = availableItemsDataGridView;
            if (dgv.Columns.Count == 0) return;

            string[] hideCols = { "item_id", "created_at", "updated_at", "description", "cost_price" };
            foreach (var c in hideCols)
                if (dgv.Columns.Contains(c))
                    dgv.Columns[c].Visible = false;

            void SetHeader(string column, string header)
            {
                if (dgv.Columns.Contains(column))
                    dgv.Columns[column].HeaderText = header;
            }

            SetHeader("brand_name", "Brand Name");
            SetHeader("generic_name", "Generic Name");
            SetHeader("strength", "Strength");
            SetHeader("dosage", "Dosage");
            SetHeader("category", "Category");
            SetHeader("selling_price", "Unit Price");
            SetHeader("stock_qty", "Stock Qty");

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.Font = _boldHeaderFont;
            dgv.DefaultCellStyle.Font = _regularFont;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dgv.Columns.Contains("selling_price"))
            {
                dgv.Columns["selling_price"].DefaultCellStyle.Format = "N2";
                dgv.Columns["selling_price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgv.Columns.Contains("stock_qty"))
                dgv.Columns["stock_qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void FormatSelectedItemsGrid()
        {
            var dgv = selectedItemsDataGridView;
            if (dgv.Columns.Count == 0) return;

            // Hide IDs
            dgv.Columns["item_id"].Visible = false;
            dgv.Columns["prescription_id"].Visible = false;
            dgv.Columns["description"].Visible = false;

            // Set headers
            dgv.Columns["brand_name"].HeaderText = "Brand Name";
            dgv.Columns["generic_name"].HeaderText = "Generic Name";
            dgv.Columns["strength"].HeaderText = "Strength";
            dgv.Columns["dosage"].HeaderText = "Dosage";
            dgv.Columns["category"].HeaderText = "Category";
            dgv.Columns["unit_price"].HeaderText = "Unit Price";
            dgv.Columns["quantity"].HeaderText = "Quantity";

            // Set ReadOnly: all columns except quantity
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.ReadOnly = col.Name != "quantity";
            }

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["unit_price"].DefaultCellStyle.Format = "N2";
            dgv.Columns["unit_price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            
        }


        // ================================
        // Search & Filter Functions
        // ================================
        private void FilterAvailableItems()
        {
            string searchText = searchItemsTextBox.Text.Trim().ToLower();
            string selectedCategory = categoryCombobox.Text.Trim().ToLower();

            var dt = helper.GetAllItems();

            var filteredRows = dt.AsEnumerable()
                .Where(row =>
                    (string.IsNullOrWhiteSpace(searchText) ||
                     row["brand_name"].ToString().ToLower().Contains(searchText) ||
                     row["generic_name"].ToString().ToLower().Contains(searchText)) &&
                    (string.IsNullOrWhiteSpace(selectedCategory) ||
                     row["category"].ToString().ToLower().Equals(selectedCategory))
                );

            if (filteredRows.Any())
                availableItemsDataGridView.DataSource = filteredRows.CopyToDataTable();
            else
                availableItemsDataGridView.DataSource = null;

            FormatAvailableItemsGrid();
        }

        private void selectedItemsDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // Only apply to the quantity column
            if (selectedItemsDataGridView.CurrentCell.ColumnIndex == selectedItemsDataGridView.Columns["quantity"].Index)
            {
                if (e.Control is TextBox tb)
                {
                    tb.KeyPress -= QuantityColumn_KeyPress; // remove previous handler to avoid duplicates
                    tb.KeyPress += QuantityColumn_KeyPress;
                }
            }
        }

        // Allow only digits
        private void QuantityColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // ignore input
                System.Media.SystemSounds.Beep.Play(); // optional feedback
            }
        }
    }
}
