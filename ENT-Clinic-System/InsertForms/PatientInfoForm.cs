using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public partial class PatientInfoForm : Form
    {
        public PatientInfoForm()
        {
            InitializeComponent();
            TabSequence();
        }

        private void TabSequence()
        {
            // Patient Info Controls
            this.firstNameTextBox.TabIndex = 0;
            this.MITextBox.TabIndex = 1;
            this.lastnameTexBox.TabIndex = 2;
            this.suffixComboBox.TabIndex = 3;
            this.addressTextBox.TabIndex = 4;
            this.dateOfBirthDateTimePicker.TabIndex = 5;
            this.sexComboBox.TabIndex = 6;
            this.statusComboBox.TabIndex = 7;
            this.patientContactNumberTextBox.TabIndex = 8;

            // Emergency Contact Controls
            this.emergencyNameTextBox.TabIndex = 9;
            this.contactNumberTextBox.TabIndex = 10;
            this.relationshipComboBox.TabIndex = 11;

            // Submit Button
            this.submitButton.TabIndex = 12;

        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                InsertPatient();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save patient: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InsertPatient()
        {
            // --- Validate patient phone number ---
            string patientPhone = patientContactNumberTextBox.Text.Trim();
            if (!PhoneNumberValidatorHelper.IsValidPhilippineMobile(patientPhone))
            {
                MessageBox.Show("Invalid patient contact number. Must be 11 digits",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Validate emergency phone number ---
            string emergencyPhone = contactNumberTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(emergencyPhone) &&
                !PhoneNumberValidatorHelper.IsValidPhilippineMobile(emergencyPhone))
            {
                MessageBox.Show("Invalid emergency contact number. Must be 11 digits",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Compute age from birth date ---
            DateTime birthDate = dateOfBirthDateTimePicker.Value;
            int age = AgeCalculatorHelper.CalculateAge(birthDate);

            // --- Concatenate full name ---
            string fullName = $"{CamelCaseHelper.ToCamelCase(lastnameTexBox.Text)} " +
                              $"{CamelCaseHelper.ToCamelCase(lastnameTexBox.Text)} " +
                              $"{CamelCaseHelper.ToCamelCase(MITextBox.Text)}. " +
                              $"{CamelCaseHelper.ToCamelCase(suffixComboBox.Text)}".Trim();

            // --- SQL Insert ---
            string sql = @"INSERT INTO patients
        (full_name, address, birth_date, age, sex, civil_status,
         patient_contact_number, emergency_name, emergency_contact_number, emergency_relationship)
        VALUES
        (@full_name, @address, @birth_date, @age, @sex, @civil_status,
         @patient_contact_number, @emergency_name, @emergency_contact_number, @emergency_relationship)";

            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@full_name", fullName);
                cmd.Parameters.AddWithValue("@address", CamelCaseHelper.ToCamelCase(addressTextBox.Text));
                cmd.Parameters.AddWithValue("@birth_date", birthDate);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@sex", CamelCaseHelper.ToCamelCase(sexComboBox.Text));
                cmd.Parameters.AddWithValue("@civil_status", CamelCaseHelper.ToCamelCase(statusComboBox.Text));
                cmd.Parameters.AddWithValue("@patient_contact_number", patientPhone);
                cmd.Parameters.AddWithValue("@emergency_name", CamelCaseHelper.ToCamelCase(emergencyNameTextBox.Text));
                cmd.Parameters.AddWithValue("@emergency_contact_number", emergencyPhone);
                cmd.Parameters.AddWithValue("@emergency_relationship", CamelCaseHelper.ToCamelCase(relationshipComboBox.Text));

                conn.Open();
                cmd.ExecuteNonQuery();

                // ✅ Ask user if they want to add another patient
                DialogResult result = MessageBox.Show(
                    "Patient record saved successfully!\n\nDo you want to add another patient?",
                    "Add Another?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    this.Close();
                }
                else
                {
                    ResetForm();
                }

                // 🔍 Debug values before executing
                Debug.WriteLine("=== Insert Debug Values ===");
                foreach (MySqlParameter param in cmd.Parameters)
                {
                    Debug.WriteLine($"{param.ParameterName} = {param.Value}");
                }
                Debug.WriteLine("===========================");
            }
        }


        private void ResetForm()
        {
            firstNameTextBox.Clear();
            MITextBox.Clear();
            lastnameTexBox.Clear();
            suffixComboBox.SelectedIndex = -1;
            addressTextBox.Clear();
            dateOfBirthDateTimePicker.Value = DateTime.Today;
            ageTextBox.Clear();
            sexComboBox.SelectedIndex = -1;
            statusComboBox.SelectedIndex = -1;
            patientContactNumberTextBox.Clear();
            emergencyNameTextBox.Clear();
            contactNumberTextBox.Clear();
            relationshipComboBox.SelectedIndex = -1;
        }

        private void PatientInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ask user before closing
            DialogResult result = MessageBox.Show(
                "All the inputs will not be saved.\n\nDo you really want to exit?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No)
            {
                // Cancel the closing
                e.Cancel = true;
            }
        }

        private void dateOfBirthDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            // --- Compute age from birth date ---
            DateTime birthDate = dateOfBirthDateTimePicker.Value;
            int age = AgeCalculatorHelper.CalculateAge(birthDate);
            ageTextBox.Text = age.ToString();
        }
        private void PatientInfoForm_Load(object sender, EventArgs e)
        {
            // Autocomplete for the address textbox (single column)
            AutoCompleteHelper.SetupAutoComplete(
                addressTextBox,
                "patients",
                new List<string> { "address" } // pass as a list
            );

            // Autocomplete for the relationship combobox (single column)
            AutoCompleteHelper.SetupAutoComplete(
                relationshipComboBox,
                "patients",
                new List<string> { "emergency_relationship" } // pass as a list
            );

            // Populate combobox items from the same column
            ComboBoxCollectionHelper.PopulateComboBox(
                relationshipComboBox,
                "patients",
                "emergency_relationship"
            );
        }

    }
}
