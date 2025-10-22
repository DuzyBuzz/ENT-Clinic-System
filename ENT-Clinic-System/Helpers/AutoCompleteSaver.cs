using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ENT_Clinic_System.Helpers
{
    public static class AutoCompleteSaver
    {
        /// <summary>
        /// Saves all values from a specified DataGridView column into the autocomplete_entries table.
        /// </summary>
        /// <param name="dgv">The DataGridView containing the values</param>
        /// <param name="dgvColumnName">The column name in the DataGridView to save</param>
        /// <param name="entryColumnName">The name to store in autocomplete_entries.column_name</param>
        public static void SaveColumnValues(DataGridView dgv, string dgvColumnName, string entryColumnName)
        {
            if (dgv == null || dgv.Rows.Count == 0)
                return;

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;

                        var valueObj = row.Cells[dgvColumnName].Value;
                        if (valueObj == null) continue;

                        string value = valueObj.ToString().Trim();
                        if (string.IsNullOrEmpty(value)) continue;

                        // Check if the value already exists
                        string checkQuery = "SELECT COUNT(*) FROM autocomplete_entries WHERE column_name=@colName AND value=@val";
                        using (var checkCmd = new MySqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@colName", entryColumnName);
                            checkCmd.Parameters.AddWithValue("@val", value);
                            long count = (long)checkCmd.ExecuteScalar();

                            if (count == 0)
                            {
                                // Insert new value
                                string insertQuery = "INSERT INTO autocomplete_entries (column_name, value) VALUES (@colName, @val)";
                                using (var insertCmd = new MySqlCommand(insertQuery, conn))
                                {
                                    insertCmd.Parameters.AddWithValue("@colName", entryColumnName);
                                    insertCmd.Parameters.AddWithValue("@val", value);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
