using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Runtime;
using System.Windows.Forms;

namespace ENT_Clinic_System.UserControls
{
    public partial class SystemSettingsForm : Form
    {
        private DataTable settingsTable;

        public SystemSettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        // 🔹 Load system settings from database
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

        // 🔹 Save updated settings to database
        private void btnSave_Click(object sender, EventArgs e)
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

        // 🔹 Refresh button
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }
    }
}
