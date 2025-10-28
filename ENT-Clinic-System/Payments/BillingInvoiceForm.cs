using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ENT_Clinic_System.Payments
{
    public partial class BillingInvoiceForm : Form
    {
        private int billingId;
        private decimal totalBill = 0;
        private decimal discountPercent = 0;
        private decimal discountAmount = 0;
        private decimal currentBalance = 0; // track remaining balance
        private TableChangeWatcher _billingWatcher;

        public BillingInvoiceForm()
        {
            InitializeComponent();
            // ✅ This makes the Enter key trigger saveButton automatically
            this.AcceptButton = saveButton;
            // Hook events
            billingDataGridView.CellClick += BillingDataGridView_CellClick;
            amountRecievedNumericUpDown.ValueChanged += AmountRecievedNumericUpDown_ValueChanged;
        }

        private void BillingInvoiceForm_Load(object sender, EventArgs e)
        {

            LoadAllBilling();


            // Setup auto-complete
            AutoCompleteHelper.SetupAutoComplete(searchPatientTextBox, "patients", new List<string> { "full_name" });

            billingtDateToDateTimePicker.Value = DateTime.Now.AddDays(+1);
            SortBillingDate();
            // Initialize watcher: table name + what to do when it changes
            _billingWatcher = new TableChangeWatcher(new[] { "billing" }, () =>
            {
                LoadAllBilling();
                Console.WriteLine("Watcher triggered → Refreshed billing data");
            });


            // Start watching
            _billingWatcher.Start();
            DGVColumnHeaderFilterHelper.Attach(billingDataGridView);
        }


        /// <summary>
        /// Load all billing records into DataGridView
        /// </summary>
        private void LoadAllBilling()
        {
            
            var columns = new List<string>
            {
                "billing_id", "patient_name", "consultation_id", "fee", "discount_percent", "discount_amount",
                "total_amount", "note", "payment_status", "amount_paid", "balance", "updated_at"
            };

            var helper = new DGVViewHelper(billingDataGridView, "billing_with_patient", columns, "billing_id");
            helper.LoadAllData();
            billingtDateToDateTimePicker.Value = DateTime.Now.AddDays(+1);
            billingDateFromDateTimePicker.Value = DateTime.Now;
        }

        /// <summary>
        /// When row clicked, load summary + reset payment entry
        /// </summary>
        private void BillingDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = billingDataGridView.Rows[e.RowIndex];
            billingId = Convert.ToInt32(row.Cells["billing_id"].Value);

            doctorsFeeTextBox.Text = row.Cells["fee"].Value?.ToString() ?? "0.00";

            discountPercent = row.Cells["discount_percent"].Value != DBNull.Value
                ? Convert.ToDecimal(row.Cells["discount_percent"].Value)
                : 0;
            discountPercentLabel.Text = $"Discount ({discountPercent}%):";

            discountAmount = row.Cells["discount_amount"].Value != DBNull.Value
                ? Convert.ToDecimal(row.Cells["discount_amount"].Value)
                : 0;
            discountAmountTextBox.Text = discountAmount.ToString("0.00");

            totalBill = row.Cells["total_amount"].Value != DBNull.Value
                ? Convert.ToDecimal(row.Cells["total_amount"].Value)
                : 0;
            totalBillTextBox.Text = totalBill.ToString("0.00");

            noteTextBox.Text = row.Cells["note"].Value?.ToString() ?? "";
           string paymentStatus = row.Cells["payment_status"].Value?.ToString() ?? "";

            string patientName = row.Cells["patient_name"].Value?.ToString() ?? "";

            // Payment status
            paymentStatusLabel.Text = row.Cells["payment_status"].Value?.ToString() ?? "N/A";

            // ✅ Use balance from DB, not from totalBill
            currentBalance = row.Cells["balance"].Value != DBNull.Value
                ? Convert.ToDecimal(row.Cells["balance"].Value)
                : totalBill;

            balanceTextBox.Text = currentBalance.ToString("0.00");
            groupBox1.Text = $"Billing History of {patientName}";
            // Reset payment entry
            amountRecievedNumericUpDown.Value = 0;
            changeTextBox.Text = "0.00";

            if (paymentStatus == "PARTIALLY PAID")
            {
                remainingBalanceTextBox.Text = currentBalance.ToString("0.00");
                labelRemainingBalance.Visible = true;
                remainingBalanceTextBox.Visible = true;

                // Make sure cashier can still enter payments
                amountRecievedNumericUpDown.Enabled = true;
                saveButton.Enabled = true;
            }
            else if (paymentStatus == "FULLY PAID")
            {
                // Show remaining balance = 0
                remainingBalanceTextBox.Text = "0.00";
                labelRemainingBalance.Visible = true;
                remainingBalanceTextBox.Visible = true;

                // Disable inputs because no more payment is needed
                amountRecievedNumericUpDown.Enabled = false;
                saveButton.Enabled = false;

                // 🔔 Show warning
                MessageBox.Show("This bill has been fully paid. No further payments are required.",
                    "Fully Paid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else // UNPAID
            {
                // Just show grand total
                labelRemainingBalance.Visible = false;
                remainingBalanceTextBox.Visible = false;

                // Allow entering first payment
                amountRecievedNumericUpDown.Enabled = true;
                saveButton.Enabled = true;
            }



            // Load payment history
            LoadPaymentHistory(billingId);
        }

        /// <summary>
        /// Load payment history for the selected billing into paymentHistoryDataGridView
        /// </summary>
        private void LoadPaymentHistory(int billingId)
        {
            try
            {
                string sql = @"SELECT 
                            payment_date AS 'Date',
                            amount AS 'Amount Paid',
                            balance AS 'Balance',
                            change_due AS 'Change',
                            note AS 'Note'
                       FROM billing_payments
                       WHERE billing_id = @billingId
                       ORDER BY payment_date ASC";

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@billingId", billingId);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    paymentHistoryDataGridView.AutoGenerateColumns = true;
                    paymentHistoryDataGridView.DataSource = dt;

                    // Optional: make it clean
                    paymentHistoryDataGridView.ReadOnly = true;
                    paymentHistoryDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    paymentHistoryDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load payment history: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearFields()
        {
            billingId = 0;
            doctorsFeeTextBox.Text = "0.00";
            discountPercentLabel.Text = "Discount (%):";
            discountAmountTextBox.Text = "0.00";
            totalBillTextBox.Text = "0.00";
            noteTextBox.Text = "";
            paymentStatusLabel.Text = "N/A";
            currentBalance = 0;
            balanceTextBox.Text = "0.00";
            amountRecievedNumericUpDown.Value = 0;
            changeTextBox.Text = "0.00";
            remainingBalanceTextBox.Text = "0.00";
            labelRemainingBalance.Visible = false;
            remainingBalanceTextBox.Visible = false;
            paymentHistoryDataGridView.DataSource = null;
        }

        /// <summary>
        /// When amount received changes → recalc change
        /// </summary>
        private void AmountRecievedNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        /// <summary>
        /// Calculate change and updated balance
        /// </summary>
        private void CalculateChange()
        {
            decimal received = amountRecievedNumericUpDown.Value;
            decimal change = 0;
            decimal balance = 0;

            // Get payment status
            string status = paymentStatusLabel.Text.Trim().ToUpper();

            if (status == "UNPAID")
            {
                // 🔹 Work with the total bill if it's still unpaid
                change = received - totalBill;
                balance = totalBill - received;
            }
            else
            {
                // 🔹 Work with remaining balance if partially paid
                change = received - currentBalance;
                balance = currentBalance - received;
            }

            // Ensure values never go negative in UI
            changeTextBox.Text = change >= 0 ? change.ToString("0.00") : "0.00";
            balanceTextBox.Text = balance > 0 ? balance.ToString("0.00") : "0.00";
        }



        /// <summary>
        /// Save payment to DB
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            decimal received = amountRecievedNumericUpDown.Value;

            // ✅ 1. Validate the input
            if (received <= 0)
            {
                MessageBox.Show("Enter a valid amount received.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                amountRecievedNumericUpDown.Focus();
                return;
            }

            // ✅ 2. Confirm save
            var confirm = MessageBox.Show("Do you want to save this payment?",
                "Confirm Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            // ✅ 3. Ask for payment note
            string note = PromptNote("Payment Note", "Enter a note for this payment:");
            if (note == null) // user pressed Cancel
                return;

            try
            {
                // ✅ 4. Save payment into database
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("CALL add_billing_payment(@billingId, @amount, @note)", conn))
                    {
                        cmd.Parameters.AddWithValue("@billingId", billingId);
                        cmd.Parameters.AddWithValue("@amount", received);
                        cmd.Parameters.AddWithValue("@note", note);
                        cmd.ExecuteNonQuery();
                    }
                }



                // ✅ 6. Ask if user wants to print the receipt
                var printConfirm = MessageBox.Show("Do you want to print the receipt now?",
                    "Print Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (printConfirm == DialogResult.Yes)
                {
                    var printer = new BillingPrinter(billingId);
                    printer.PrintReceipt();
                }

                // ✅ 7. Refresh billing data after transaction
                RefreshBilling();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to record payment: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Prompt user for a note before saving.
        /// </summary>
        /// <summary>
        /// Prompt user with a ComboBox to select a payment note.
        /// </summary>
        private string PromptNote(string title, string message)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 220,
                Text = title,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label textLabel = new Label()
            {
                Left = 20,
                Top = 20,
                Text = message,
                AutoSize = true
            };

            // ComboBox with predefined notes
            ComboBox comboBox = new ComboBox()
            {
                Left = 20,
                Top = 50,
                Width = 340,
                DropDownStyle = ComboBoxStyle.DropDown // user can only select
            };


            // Populate combobox items from the same column
            ComboBoxCollectionHelper.PopulateComboBox(
                comboBox,
                "billing_payments",
                "note"
            );
            AutoCompleteHelper.SetupAutoComplete(
                comboBox,
                "billing_payments",
                new List<string> { "note" } // pass as a list
            );

            Button confirmation = new Button()
            {
                Text = "OK",
                Left = 200,
                Width = 75,
                Top = 110,
                DialogResult = DialogResult.OK
            };

            Button cancel = new Button()
            {
                Text = "Cancel",
                Left = 285,
                Width = 75,
                Top = 110,
                DialogResult = DialogResult.Cancel
            };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(comboBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);

            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog() == DialogResult.OK ? comboBox.Text.ToString() : null;
        }
        /// <summary>
        /// Applies color formatting to rows in billingDataGridView based on payment status.
        /// </summary>



        private void searchPatientButton_Click(object sender, EventArgs e)
        {
            SearchHelper.Search(
                dgv: billingDataGridView,
                tableName: "billing_with_patient",
                columnNames: new string[] { "patient_name" },
                filterControl: searchPatientTextBox
            );


        }
        private void SortBillingDate()
        {
            if (billingDateFromDateTimePicker.Value <= billingtDateToDateTimePicker.Value)
            {
                SearchHelper.Search(
                    dgv: billingDataGridView,
                    tableName: "billing_with_patient",
                    columnNames: new string[] { "updated_at" },
                    fromDate: billingDateFromDateTimePicker.Value,
                    toDate: billingtDateToDateTimePicker.Value,
                    columns: new string[]
                    {
                "billing_id", "patient_name", "consultation_id", "fee", "discount_percent", "discount_amount",
                "total_amount", "note", "payment_status", "amount_paid", "balance", "updated_at"
                    }
                );
            }
            else
            {
                MessageBox.Show("Start date must be earlier than end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void labelRemainingBalance_Click(object sender, EventArgs e)
        {

        }

        private void refreshPatientsButton_Click(object sender, EventArgs e)
        {
            RefreshBilling();
            DGVColumnHeaderFilterHelper.ResetFilters(billingDataGridView);

        }
        private void RefreshBilling()
        {
            SortBillingDate();
            billingtDateToDateTimePicker.Value = DateTime.Now.AddDays(+1);
            billingDateFromDateTimePicker.Value = DateTime.Now;
            ClearFields();
        }
        private void billingDateFromDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SortBillingDate();
        }

        private void billingtDateToDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SortBillingDate();
        }

        private void balanceTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBoxPayment_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void billingDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchPatientTextBox_TextChanged(object sender, EventArgs e)
        {
            SearchHelper.Search(
    dgv: billingDataGridView,
    tableName: "billing_with_patient",
    columnNames: new string[] { "patient_name" },
    filterControl: searchPatientTextBox
);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void amountRecievedNumericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // ✅ Prevents "ding" sound or moving to next control
                saveButton.PerformClick(); // ✅ Triggers the button click event
            }
        }
    }
}
