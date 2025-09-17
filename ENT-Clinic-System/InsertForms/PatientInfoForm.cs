using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public partial class PatientInfoForm : Form
    {
        public Image CapturedImage { get; private set; }
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
            this.contactFistNameTextBox.TabIndex = 9;
            this.contactMiddleNameTextBox.TabIndex = 10;
            this.contactLastNameTextBox.TabIndex = 11;
            this.contactsuffixComboBox.TabIndex = 12;





            this.contactNumberTextBox.TabIndex = 13;
            this.relationshipComboBox.TabIndex = 14;

            // Submit Button
            this.submitButton.TabIndex = 15;

        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Ask for confirmation before saving
                DialogResult confirmResult = MessageBox.Show(
                    "Are you sure you want to save this patient’s information?\n\n" +
                    "Please double-check the entered details before proceeding.",
                    "Confirm Save",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (confirmResult == DialogResult.Yes)
                {
                    InsertPatient();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save patient: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void InsertPatient()
        {
            // --- Validate required fields ---
            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text))
            {
                MessageBox.Show("Please enter the patient's first name.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                firstNameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(lastnameTexBox.Text))
            {
                MessageBox.Show("Please enter the patient's last name.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lastnameTexBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(addressTextBox.Text))
            {
                MessageBox.Show("Please enter the patient's address.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                addressTextBox.Focus();
                return;
            }

            if (sexComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the patient's sex.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sexComboBox.Focus();
                return;
            }

            if (statusComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select the patient's civil status.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                statusComboBox.Focus();
                return;
            }

            // --- Validate patient phone number ---
            string patientPhone = patientContactNumberTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(patientPhone))
            {
                MessageBox.Show("Please enter the patient's contact number.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                patientContactNumberTextBox.Focus();
                return;
            }
            if (!PhoneNumberValidatorHelper.IsValidPhilippineMobile(patientPhone))
            {
                MessageBox.Show("Invalid patient contact number. It must be 11 digits.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                patientContactNumberTextBox.Focus();
                return;
            }

            // --- Validate emergency contact (optional, but if filled must be valid) ---
            string emergencyPhone = contactNumberTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(emergencyPhone) &&
                !PhoneNumberValidatorHelper.IsValidPhilippineMobile(emergencyPhone))
            {
                MessageBox.Show("Invalid emergency contact number. It must be 11 digits.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                contactNumberTextBox.Focus();
                return;
            }

            // --- Validate emergency contact name if phone number is given ---
            if (!string.IsNullOrEmpty(emergencyPhone) &&
                string.IsNullOrWhiteSpace(contactFistNameTextBox.Text))
            {
                MessageBox.Show("Please provide the emergency contact's name when entering their number.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                contactFistNameTextBox.Focus();
                return;
            }

            // --- Compute age from birth date ---
            DateTime birthDate = dateOfBirthDateTimePicker.Value;
            int age = AgeCalculatorHelper.CalculateAge(birthDate);

            if (age < 0 || age > 120) // sanity check
            {
                MessageBox.Show("Invalid birth date. Please enter a valid date of birth.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateOfBirthDateTimePicker.Focus();
                return;
            }

            // --- Concatenate full name ---
            string fullName = $"{CamelCaseHelper.ToCamelCase(firstNameTextBox.Text)} " +
                              $"{CamelCaseHelper.ToCamelCase(MITextBox.Text)} " +
                              $"{CamelCaseHelper.ToCamelCase(lastnameTexBox.Text)}. " +
                              $"{CamelCaseHelper.ToCamelCase(suffixComboBox.Text)}".Trim();

            // --- Concatenate emergency contact name ---
            string contactName = $"{CamelCaseHelper.ToCamelCase(contactFistNameTextBox.Text)} " +
                                 $"{CamelCaseHelper.ToCamelCase(contactMiddleNameTextBox.Text)} " +
                                 $"{CamelCaseHelper.ToCamelCase(contactLastNameTextBox.Text)}. " +
                                 $"{CamelCaseHelper.ToCamelCase(contactsuffixComboBox.Text)}".Trim();

            // --- Convert patient photo to byte[] ---
            byte[] photoBytes = null;
            if (CaptureImagePictureBox.Image != null)
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    CaptureImagePictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    photoBytes = ms.ToArray();
                }
            }

            // --- Show confirmation dialog before saving ---
            DialogResult confirm = MessageBox.Show(
                $"Please confirm the following details:\n\n" +
                $"Name: {fullName}\n" +
                $"Address: {addressTextBox.Text}\n" +
                $"Date of Birth: {birthDate:MMMM dd, yyyy} (Age {age})\n" +
                $"Sex: {sexComboBox.Text}\n" +
                $"Civil Status: {statusComboBox.Text}\n" +
                $"Contact Number: {patientPhone}\n" +
                $"Emergency Contact: {contactName}\n" +
                $"Emergency Number: {emergencyPhone}\n" +
                $"Relationship: {relationshipComboBox.Text}\n\n" +
                $"Do you want to save this patient record?",
                "Confirm Save",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
            {
                return; // User cancelled
            }

            // --- SQL Insert (added photo column) ---
            string sql = @"INSERT INTO patients
        (full_name, address, birth_date, age, sex, civil_status,
         patient_contact_number, emergency_name, emergency_contact_number, emergency_relationship, photo)
        VALUES
        (@full_name, @address, @birth_date, @age, @sex, @civil_status,
         @patient_contact_number, @emergency_name, @emergency_contact_number, @emergency_relationship, @photo)";

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
                cmd.Parameters.AddWithValue("@emergency_name", contactName);
                cmd.Parameters.AddWithValue("@emergency_contact_number", emergencyPhone);
                cmd.Parameters.AddWithValue("@emergency_relationship", CamelCaseHelper.ToCamelCase(relationshipComboBox.Text));

                // Handle null photo (avoid errors if no picture was taken)
                if (photoBytes != null)
                    cmd.Parameters.Add("@photo", MySqlDbType.LongBlob).Value = photoBytes;
                else
                    cmd.Parameters.Add("@photo", MySqlDbType.LongBlob).Value = DBNull.Value;

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
            contactNumberTextBox.Clear();
            contactFistNameTextBox.Clear();
            contactLastNameTextBox.Clear();
            contactMiddleNameTextBox.Clear();
            contactsuffixComboBox.SelectedIndex = -1;
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
        private void CaptureImagePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                using (SelectPhotoOptionForm optionForm = new SelectPhotoOptionForm())
                {
                    if (optionForm.ShowDialog() == DialogResult.OK)
                    {
                        if (optionForm.SelectedOption == SelectPhotoOptionForm.PhotoOption.Camera)
                        {
                            // --- Option 1: Capture from camera ---
                            using (PatientCamera cameraForm = new PatientCamera())
                            {
                                if (cameraForm.ShowDialog() == DialogResult.OK)
                                {
                                    if (cameraForm.CapturedImage != null)
                                    {
                                        CaptureImagePictureBox.Image = cameraForm.CapturedImage;
                                    }
                                    else
                                    {
                                        MessageBox.Show("No image was captured from the camera.",
                                                        "Capture Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                        }
                        else if (optionForm.SelectedOption == SelectPhotoOptionForm.PhotoOption.File)
                        {
                            // --- Option 2: Upload from file ---
                            using (OpenFileDialog openFileDialog = new OpenFileDialog())
                            {
                                openFileDialog.Title = "Select a photo";
                                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                                if (openFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    try
                                    {
                                        Image uploadedImage = Image.FromFile(openFileDialog.FileName);
                                        CaptureImagePictureBox.Image = uploadedImage;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Failed to load the selected image.\n\nError: " + ex.Message,
                                                        "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred while selecting the photo.\n\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



    }
}
