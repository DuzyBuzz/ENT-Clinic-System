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
        /// Set the filter value (e.g., patient ID) and load the data
        /// </summary>
        public void LoadData(object id)
        {
            filterValue = id ?? throw new ArgumentNullException(nameof(id));

            try
            {
                string columnsString = string.Join(",", columns);
                string sql = $"SELECT {columnsString} FROM {tableName} WHERE {filterColumn}=@filterValue";

                // DEBUG: show the SQL and filter value
                Console.WriteLine("SQL Query: " + sql);
                Console.WriteLine("Filter Value: " + filterValue + " (Type: " + filterValue.GetType() + ")");

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    // Ensure correct data type
                    cmd.Parameters.Add("@filterValue", MySqlDbType.Int32).Value = Convert.ToInt32(filterValue);
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // DEBUG: show how many rows were returned
                        Console.WriteLine("From DGVViewHelper ---- Rows returned: " + dt.Rows.Count);

                        // ✅ Ensure columns are auto-generated
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

    }
}
