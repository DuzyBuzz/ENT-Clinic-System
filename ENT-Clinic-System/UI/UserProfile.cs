using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.UI
{
    public partial class UserProfile : Form
    {
        public UserProfile()
        {
            InitializeComponent();
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            // Load current logged-in user details into form
            txtUserId.Text = UserCredentials.UserId.ToString();
            txtUsername.Text = UserCredentials.Username;
            txtFullName.Text = UserCredentials.Fullname;
            txtRole.Text = UserCredentials.Role; // Role displayed as read-only
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var conn = new MySqlConnection(UserCredentials.ConnectionString))
                {
                    conn.Open();
                    string sql = @"UPDATE user 
                                   SET username=@username, 
                                       password=@password, 
                                       full_name=@fullname
                                   WHERE user_id=@userId";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@fullname", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@userId", int.Parse(txtUserId.Text));

                        cmd.ExecuteNonQuery();
                    }
                }

                // Update session values
                UserCredentials.Username = txtUsername.Text;
                UserCredentials.Fullname = txtFullName.Text;
                // Role is unchanged since it's read-only

                MessageBox.Show(
                    "Profile updated successfully!\n\nThe system will now restart to apply changes.",
                    "Profile Updated",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
                Application.Restart();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
