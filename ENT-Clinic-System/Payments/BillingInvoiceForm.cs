using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.Payments
{
    public partial class BillingInvoiceForm : Form
    {
        private int billingId;
        private decimal totalBill = 0;
        private decimal discountPercent = 0;
        private decimal discountAmount = 0;

        public BillingInvoiceForm()
        {
            InitializeComponent();

            // Hook events for real-time calculation
            billingDataGridView.CellClick += BillingDataGridView_CellClick;
            amountRecievedNumericUpDown.ValueChanged += AmountRecievedNumericUpDown_ValueChanged;
        }

        /// <summary>
        /// Load all billing records into the DataGridView on form load
        /// </summary>
        private void BillingInvoiceForm_Load(object sender, EventArgs e)
        {
            LoadAllBilling();
        }

        /// <summary>
        /// Load all billing records into the DataGridView
        /// </summary>
        private void LoadAllBilling()
        {
            var columns = new List<string>
            {
                "billing_id", "consultation_id", "fee", "discount_percent", "discount_amount",
                "total_amount", "note", "payment_status", "amount_paid"
            };

            var helper = new DGVViewHelper(billingDataGridView, "billing", columns, "billing_id");
            helper.LoadAllData(); // Loads all billing records
        }

        /// <summary>
        /// When a DataGridView row is clicked, populate TextBoxes & calculate
        /// </summary>
        private void BillingDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // header row

            DataGridViewRow row = billingDataGridView.Rows[e.RowIndex];

            // Assign values to fields
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

            // Set the numeric updown to totalBill by default (or minimum if lower)
            amountRecievedNumericUpDown.Value = 0;


            // Real-time calculation
            CalculateChange();
        }

        /// <summary>
        /// When amount received changes, update the change immediately
        /// </summary>
        private void AmountRecievedNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        /// <summary>
        /// Calculate change based on amount received and total bill
        /// </summary>
        private void CalculateChange()
        {
            decimal received = amountRecievedNumericUpDown.Value;
            decimal change = received - totalBill;
            changeTextBox.Text = change >= 0 ? change.ToString("0.00") : "0.00";
        }

        /// <summary>
        /// Save the payment to the database
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            decimal received = amountRecievedNumericUpDown.Value;

            if (received <= 0)
            {
                MessageBox.Show("Enter a valid amount received.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                amountRecievedNumericUpDown.Focus();
                return;
            }

            string note = noteTextBox.Text.Trim();

            try
            {
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

                MessageBox.Show("Payment recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var printer = new BillingPrinter(billingId);
                printer.PrintReceipt();

                this.Close(); // close form after submit
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to record payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
