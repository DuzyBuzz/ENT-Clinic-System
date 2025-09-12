using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ENT_Clinic_System.Helpers
{
    internal class InsertHelper
    {
        /// <summary>
        /// Generic Insert method
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <param name="columns">List of column names</param>
        /// <param name="values">List of values matching the columns</param>
        /// <returns>Number of affected rows</returns>
        public static int Insert(string tableName, List<string> columns, List<object> values)
        {
            if (columns.Count != values.Count)
                throw new ArgumentException("Columns count must match values count.");

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // Build query dynamically
                string columnNames = string.Join(", ", columns);
                string paramNames = string.Join(", ", columns.ConvertAll(c => "@" + c));

                string query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({paramNames})";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Add parameters dynamically
                    for (int i = 0; i < columns.Count; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + columns[i], values[i]);
                    }

                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
