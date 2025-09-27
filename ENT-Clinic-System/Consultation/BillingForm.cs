using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Consultation
{
    public partial class BillingForm : Form
    {
        private readonly int _consultationId;
        private readonly int _patientId;

        public BillingForm(int consultationId, int patientId)
        {
            InitializeComponent();
            _consultationId = consultationId;

            // Event bindings for auto calculation
            feeComboBox.TextChanged += RecalculateFinalAmount;
            discountComboBox.TextChanged += RecalculateFinalAmount; // ✅ use TextChanged instead
            fullDiscountCheckBox.CheckedChanged += FullDiscountCheckBox_CheckedChanged;
            _patientId = patientId;
        }

        // ------------------------------
        // Handle full discount checkbox
        // ------------------------------
        private void FullDiscountCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fullDiscountCheckBox.Checked)
            {
                discountComboBox.Enabled = false;
                discountAmountLabel.Text = "₱0.00";
                finalAmountLabel.Text = "₱0.00";
            }
            else
            {
                discountComboBox.Enabled = true;
                RecalculateFinalAmount(null, null);
            }
        }

        // ------------------------------
        // Auto recalc whenever fee/discount changes
        // ------------------------------
        private void RecalculateFinalAmount(object sender, EventArgs e)
        {
            if (!decimal.TryParse(feeComboBox.Text, out decimal fee))
            {
                fee = 0;
            }

            if (fullDiscountCheckBox.Checked)
            {
                discountAmountLabel.Text = "-" +fee.ToString("N2");
                finalAmountLabel.Text = "₱0.00";
                return;
            }

            int discountPercent = 0;
            if (!string.IsNullOrWhiteSpace(discountComboBox.Text))
            {
                int.TryParse(discountComboBox.Text.Replace("%", "").Trim(), out discountPercent);
            }

            decimal discountAmount = (fee * discountPercent) / 100;
            decimal final = Math.Max(fee - discountAmount, 0);

            // Update breakdown labels
            discountAmountLabel.Text = discountAmount.ToString("N2");
            finalAmountLabel.Text = final.ToString("N2");
        }

        // ------------------------------
        // Save billing to DB
        // ------------------------------
        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(feeComboBox.Text, out decimal fee))
                {
                    MessageBox.Show("Please enter a valid doctor's fee.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int discountPercent = 0;
                if (fullDiscountCheckBox.Checked)
                {
                    discountPercent = 100;
                }
                else if (!string.IsNullOrWhiteSpace(discountComboBox.Text))
                {
                    int.TryParse(discountComboBox.Text.Replace("%", "").Trim(), out discountPercent);
                }

                decimal discountAmount = (fee * discountPercent) / 100;
                decimal finalAmount = Math.Max(fee - discountAmount, 0);
                string note = noteComboBox.Text.Trim();

                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO billing 
                            (consultation_id, patient_id, fee, discount_percent, discount_amount, total_amount, note, created_at) 
                        VALUES 
                            (@consultation_id, @patient_id, @fee, @discount_percent, @discount_amount, @total_amount, @note, NOW())";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultation_id", _consultationId);
                        cmd.Parameters.AddWithValue("@patient_id", _patientId);
                        cmd.Parameters.AddWithValue("@fee", fee);
                        cmd.Parameters.AddWithValue("@discount_percent", discountPercent);
                        cmd.Parameters.AddWithValue("@discount_amount", discountAmount);
                        cmd.Parameters.AddWithValue("@total_amount", finalAmount);
                        cmd.Parameters.AddWithValue("@note", string.IsNullOrWhiteSpace(note) ? "" : note);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Billing record saved successfully!",
                    "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving billing record: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            // Show confirmation prompt
            DialogResult result = MessageBox.Show(
                "This consultation record has not been saved.\n\n" +
                "Do you want to cancel and discard this consultation?",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {
                // ✅ User confirmed cancellation → delete latest consultation
                bool deleted = LatestIdHelper.DeleteLatest("consultation", "consultation_id");

                if (deleted)
                {
                    MessageBox.Show("The consultation record has been discarded.",
                        "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                // ❌ User chose NO → do nothing, return to form
                return;
            }
        }


        private void BillingForm_Load(object sender, EventArgs e)
        {
            ComboBoxCollectionHelper.PopulateComboBox(feeComboBox, "billing", "fee");
            ComboBoxCollectionHelper.PopulateComboBox(discountComboBox, "billing", "discount_percent");
            ComboBoxCollectionHelper.PopulateComboBox(noteComboBox, "billing", "note");
        }
    }
}