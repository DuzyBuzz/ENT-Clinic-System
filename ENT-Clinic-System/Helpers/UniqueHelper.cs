using MySql.Data.MySqlClient;
using System;

namespace ENT_Clinic_System.Helpers
{
    internal class UniqueHelper
    {
        /// <summary>
        /// Checks if a combination of values exists in a table.
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="columns">Array of column names</param>
        /// <param name="values">Array of values corresponding to columns</param>
        /// <returns>True if the values already exist, false if unique</returns>
        public static bool Exists(string tableName, string[] columns, object[] values)
        {
            if (columns.Length != values.Length)
                throw new ArgumentException("Columns and values must have the same length.");

            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // Build WHERE clause like: "col1=@val0 AND col2=@val1 ..."
                    string whereClause = "";
                    for (int i = 0; i < columns.Length; i++)
                    {
                        whereClause += $"{columns[i]}=@val{i}";
                        if (i < columns.Length - 1)
                            whereClause += " AND ";
                    }

                    string query = $"SELECT COUNT(*) FROM {tableName} WHERE {whereClause}";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Add parameters to avoid SQL injection
                        for (int i = 0; i < values.Length; i++)
                        {
                            cmd.Parameters.AddWithValue($"@val{i}", values[i]);
                        }

                        // Execute query
                        long count = (long)cmd.ExecuteScalar();
                        return count > 0; // True if already exists
                    }
                }
            }
            catch (Exception ex)
            {
                // Optional: log the exception
                throw new Exception("Error checking unique values: " + ex.Message);
            }
        }
    }
}
