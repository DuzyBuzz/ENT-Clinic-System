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
            // Fill textboxes with logged-in user info
            txtUserId.Text = UserCredentials.UserId.ToString();
            txtUsername.Text = UserCredentials.Username;
            txtFullName.Text = UserCredentials.Fullname;
            txtRole.Text = UserCredentials.Role;  // read-only (role cannot be changed)
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Password validation
            if (!string.IsNullOrWhiteSpace(txtPassword.Text) || !string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                // Load correct connection string based on ROLE
                DBConfig.SetConnectionString(UserCredentials.Role);

                using (var conn = new MySqlConnection(DBConfig.ConnectionString))
                {
                    conn.Open();

                    string sql;

                    // Only update password if the user entered one
                    if (string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        sql = @"UPDATE user 
                               SET username=@username, 
                                   full_name=@fullname
                               WHERE user_id=@userId";
                    }
                    else
                    {
                        sql = @"UPDATE user 
                               SET username=@username, 
                                   password=@password, 
                                   full_name=@fullname
                               WHERE user_id=@userId";
                    }

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@fullname", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@userId", int.Parse(txtUserId.Text));

                        if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                }

                // Update in-memory session values
                UserCredentials.Username = txtUsername.Text.Trim();
                UserCredentials.Fullname = txtFullName.Text.Trim();
                // Role stays the same

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
                MessageBox.Show("Error updating profile:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
