using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    internal class ComboBoxCollectionHelper
    {
        /// <summary>
        /// Populates a ComboBox with distinct values from any database column.
        /// Works with string, int, decimal, date, etc.
        /// </summary>
        /// <param name="comboBox">The ComboBox to populate.</param>
        /// <param name="tableName">Database table name.</param>
        /// <param name="columnName">Database column name.</param>
        public static void PopulateComboBox(ComboBox comboBox, string tableName, string columnName)
        {
            try
            {
                List<object> items = new List<object>(); // use object to support all data types
                string sql = $"SELECT DISTINCT {columnName} FROM {tableName} ORDER BY {columnName}";

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                items.Add(reader.GetValue(0));
                            // ✅ GetValue works for ANY SQL data type (int, decimal, string, date, etc.)
                        }
                    }
                }

                comboBox.Items.Clear();
                comboBox.Items.AddRange(items.ToArray());
                // ✅ Items can now be int, decimal, string, etc.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to populate ComboBox: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}