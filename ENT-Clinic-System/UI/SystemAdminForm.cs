using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.UI
{
    public partial class SystemAdminForm : Form
    {
        private DGVCrudHelper usersHelper;
        private DGVCrudHelper settingsHelper;

        public SystemAdminForm()
        {
            InitializeComponent();
            
            // --- USERS HELPER ---
            usersHelper = new DGVCrudHelper(
                dgvUsers,                       // Users DataGridView
                "user",                          // Table name
                new List<string> { "username", "password", "full_name", "role" }, // Columns
                "user_id"                       // Primary key
            );

            usersHelper.LoadData();

            // Users search filter
            txtSearchUsers.TextChanged += (s, e) =>
            {
                ApplyUserFilter(txtSearchUsers.Text);
            };

            // Users buttons
            btnAddUser.Click += BtnAddUser_Click;
            btnRefreshUsers.Click += (s, e) => usersHelper.LoadData();

            // --- SETTINGS HELPER ---
            settingsHelper = new DGVCrudHelper(
                dgvSettings,                     // Settings DataGridView
                "system_settings",               // Table name
                new List<string> { "setting_value", "description" }, // Columns editable
                "setting_key"                    // Primary key (read-only)
            );

            settingsHelper.LoadData();

            // Settings search filter
            txtSearchSettings.TextChanged += (s, e) =>
            {
                ApplySettingsFilter(txtSearchSettings.Text);
            };

            btnRefreshSettings.Click += (s, e) => settingsHelper.LoadData();
        }

        #region Users Methods
        private void ApplyUserFilter(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                (dgvUsers.DataSource as DataTable).DefaultView.RowFilter = "";
            }
            else
            {
                string escaped = filter.Replace("'", "''");
                (dgvUsers.DataSource as DataTable).DefaultView.RowFilter =
                    $"username LIKE '%{escaped}%' OR full_name LIKE '%{escaped}%' OR role LIKE '%{escaped}%'";
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

            var rowValues = new Dictionary<string, object>
            {
                { "username", txtUsername.Text.Trim() },
                { "password", txtPassword.Text.Trim() },
                { "full_name", txtFullName.Text.Trim() },
                { "role", cmbRole.SelectedItem.ToString() }
            };

            try
            {
                usersHelper.InsertRow(rowValues);
                MessageBox.Show("User added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                usersHelper.LoadData();
                ClearUserForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearUserForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtFullName.Clear();
            cmbRole.SelectedIndex = -1;
        }
        #endregion

        #region Settings Methods
        private void ApplySettingsFilter(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                (dgvSettings.DataSource as DataTable).DefaultView.RowFilter = "";
            }
            else
            {
                string escaped = filter.Replace("'", "''");
                (dgvSettings.DataSource as DataTable).DefaultView.RowFilter =
                    $"setting_key LIKE '%{escaped}%' OR setting_value LIKE '%{escaped}%' OR description LIKE '%{escaped}%'";
            }
        }
        #endregion

        private void SystemAdminForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

        }
    }
}
