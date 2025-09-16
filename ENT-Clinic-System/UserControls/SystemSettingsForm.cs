using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Runtime;
using System.Windows.Forms;

namespace ENT_Clinic_System.UserControls
{
    public partial class SystemAdminForm : Form
    {
        private DataTable settingsTable;
        private DataTable usersTable;

        public SystemAdminForm()
        {
            InitializeComponent();
            LoadSettings();
            LoadUsers();
        }

        #region System Settings
        private void LoadSettings()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT setting_key, setting_value FROM system_settings ORDER BY setting_key";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        settingsTable = new DataTable();
                        adapter.Fill(settingsTable);
                        dgvSettings.DataSource = settingsTable;

                        dgvSettings.Columns["setting_key"].ReadOnly = true;
                        dgvSettings.Columns["setting_value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvSettings.Columns["setting_key"].HeaderText = "Setting Key";
                        dgvSettings.Columns["setting_value"].HeaderText = "Setting Value";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading settings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        foreach (DataRow row in settingsTable.Rows)
                        {
                            string key = row["setting_key"].ToString();
                            string value = row["setting_value"].ToString();

                            string updateQuery = "UPDATE system_settings SET setting_value=@value WHERE setting_key=@key";
                            using (var cmd = new MySqlCommand(updateQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@key", key);
                                cmd.Parameters.AddWithValue("@value", value);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        MessageBox.Show("Settings updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving settings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshSettings_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }
        #endregion

        #region Users Management
        private void LoadUsers()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT username, full_name, role FROM users ORDER BY username";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        usersTable = new DataTable();
                        adapter.Fill(usersTable);
                        dgvUsers.DataSource = usersTable;

                        dgvUsers.Columns["username"].HeaderText = "Username";
                        dgvUsers.Columns["full_name"].HeaderText = "Full Name";
                        dgvUsers.Columns["role"].HeaderText = "Role";

                        dgvUsers.Columns["username"].ReadOnly = true;
                        dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) || cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO users (username, password, full_name, role) VALUES (@username, @password, @full_name, @role)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@full_name", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers();
                        ClearUserForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to update", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = dgvUsers.SelectedRows[0].Cells["username"].Value.ToString();
            string fullName = dgvUsers.SelectedRows[0].Cells["full_name"].Value.ToString();
            string role = dgvUsers.SelectedRows[0].Cells["role"].Value.ToString();

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE users SET full_name=@full_name, role=@role WHERE username=@username";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@full_name", fullName);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = dgvUsers.SelectedRows[0].Cells["username"].Value.ToString();

            if (MessageBox.Show($"Are you sure you want to delete user '{username}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM users WHERE username=@username";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsers();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshUsers_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void ClearUserForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtFullName.Clear();
            cmbRole.SelectedIndex = -1;
        }
        #endregion
    }
}
