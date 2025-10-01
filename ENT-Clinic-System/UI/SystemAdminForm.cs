using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Runtime;
using System.Windows.Forms;

namespace ENT_Clinic_System.UI
{
    public partial class SystemAdminForm : Form
    {
        private DataTable settingsTable;
        private DataTable usersTable;

        public SystemAdminForm()
        {
            InitializeComponent();

            // Load Users first by default
            LoadUsers();
            LoadSettings();

            // Events
            txtSearchUsers.TextChanged += TxtSearchUsers_TextChanged;
            txtSearchSettings.TextChanged += TxtSearchSettings_TextChanged;

            btnAddUser.Click += BtnAddUser_Click;
            btnUpdateUser.Click += BtnUpdateUser_Click;
            btnDeleteUser.Click += BtnDeleteUser_Click;
            btnRefreshUsers.Click += BtnRefreshUsers_Click;

            btnSaveSettings.Click += BtnSaveSettings_Click;
            btnRefreshSettings.Click += BtnRefreshSettings_Click;
        }

        #region System Settings
        private void LoadSettings()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT setting_key, setting_value, description FROM system_settings ORDER BY setting_key";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        settingsTable = new DataTable();
                        adapter.Fill(settingsTable);
                        dgvSettings.DataSource = settingsTable;

                        // Make key read-only
                        dgvSettings.Columns["setting_key"].ReadOnly = true;

                        // Fill mode for value and description
                        dgvSettings.Columns["setting_value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dgvSettings.Columns["description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                        // Friendly headers
                        dgvSettings.Columns["setting_key"].HeaderText = "Setting Key";
                        dgvSettings.Columns["setting_value"].HeaderText = "Setting Value";
                        dgvSettings.Columns["description"].HeaderText = "Description";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading settings: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            if (settingsTable == null || settingsTable.Rows.Count == 0)
            {
                MessageBox.Show("No settings to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                            string description = row["description"].ToString();

                            string updateQuery = "UPDATE system_settings SET setting_value=@value, description=@description WHERE setting_key=@key";
                            using (var cmd = new MySqlCommand(updateQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@key", key);
                                cmd.Parameters.AddWithValue("@value", value);
                                cmd.Parameters.AddWithValue("@description", description);
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

        private void BtnRefreshSettings_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void TxtSearchSettings_TextChanged(object sender, EventArgs e)
        {
            if (settingsTable == null) return;

            string filter = txtSearchSettings.Text.Trim().Replace("'", "''");
            (dgvSettings.DataSource as DataTable).DefaultView.RowFilter =
                $"setting_key LIKE '%{filter}%' OR setting_value LIKE '%{filter}%' OR description LIKE '%{filter}%'";
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
                    string query = "SELECT username, full_name, role FROM user ORDER BY username";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        usersTable = new DataTable();
                        adapter.Fill(usersTable);
                        dgvUsers.DataSource = usersTable;

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

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) || cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO user (username, password, full_name, role) VALUES (@username, @password, @full_name, @role)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@full_name", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@role", cmbRole.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("User added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                    ClearUserForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdateUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to update.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    string query = "UPDATE user SET full_name=@full_name, role=@role WHERE username=@username";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@full_name", fullName);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    string query = "DELETE FROM user WHERE username=@username";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefreshUsers_Click(object sender, EventArgs e)
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

        private void TxtSearchUsers_TextChanged(object sender, EventArgs e)
        {
            if (usersTable == null) return;

            string filter = txtSearchUsers.Text.Trim().Replace("'", "''");
            (dgvUsers.DataSource as DataTable).DefaultView.RowFilter =
                $"username LIKE '%{filter}%' OR full_name LIKE '%{filter}%' OR role LIKE '%{filter}%'";
        }
        #endregion

        private void SystemAdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            Login login = new Login();
            login.Show();
        }
    }
}
