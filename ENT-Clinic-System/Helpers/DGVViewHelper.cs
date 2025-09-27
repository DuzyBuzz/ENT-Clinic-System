using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public class DGVViewHelper
    {
        private readonly DataGridView dgv;
        private readonly string tableName;
        private readonly List<string> columns;
        private readonly string filterColumn;
        private object filterValue;

        public DGVViewHelper(DataGridView dgv, string tableName, List<string> columns, string filterColumn)
        {
            this.dgv = dgv ?? throw new ArgumentNullException(nameof(dgv));
            this.tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            this.columns = columns ?? throw new ArgumentNullException(nameof(columns));
            this.filterColumn = filterColumn ?? throw new ArgumentNullException(nameof(filterColumn));

            // Make the DataGridView read-only
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Load data filtered by a specific ID
        /// </summary>
        public void LoadData(object id)
        {
            filterValue = id ?? throw new ArgumentNullException(nameof(id));

            try
            {
                string columnsString = string.Join(",", columns);
                string sql = $"SELECT {columnsString} FROM {tableName} WHERE {filterColumn}=@filterValue";

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@filterValue", MySqlDbType.Int32).Value = Convert.ToInt32(filterValue);
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgv.AutoGenerateColumns = true;
                        dgv.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load all data from the table without filter
        /// </summary>
        public void LoadAllData()
        {
            try
            {
                string columnsString = string.Join(",", columns);
                string sql = $"SELECT {columnsString} FROM {tableName}";

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.AutoGenerateColumns = true;
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load all data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
