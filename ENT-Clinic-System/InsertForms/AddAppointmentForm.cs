using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using ENT_Clinic_System.Helpers;

namespace ENT_Clinic_System.InsertForms
{
    public partial class AddAppointmentForm : Form
    {
        private readonly DateTime selectedDate;

        public AddAppointmentForm(DateTime date)
        {
            InitializeComponent();
            selectedDate = date;
            this.Text = $"Add Appointment - {selectedDate:MMMM dd, yyyy}";

            // Optional: show the date in a label or textbox on the form
            lblDate.Text = selectedDate.ToString("MMMM dd, yyyy");
        }


        /// <summary>
        /// Save the appointment to the database (no patient — only note and date).
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNote.Text))
            {
                MessageBox.Show("Please enter a note or description for the appointment.", "Missing Info");
                return;
            }

            string note = txtNote.Text.Trim();

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // patient_id removed — only saving note and date
                    string sql = @"INSERT INTO appointments (follow_up_date, note)
                                   VALUES (@date, @note)";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@date", selectedDate.Date);
                        cmd.Parameters.AddWithValue("@note", note);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Appointment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving appointment: " + ex.Message, "Database Error");
            }
        }
    }
}
