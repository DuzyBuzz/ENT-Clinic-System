using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    internal class ComboBoxCollectionHelper
    {
        /// <summary>
        /// Populates a ComboBox with distinct non-null values from a database column.
        /// Works with string, int, decimal, date, etc.
        /// </summary>
        /// <param name="comboBox">The ComboBox to populate.</param>
        /// <param name="tableName">Database table name.</param>
        /// <param name="columnName">Database column name.</param>
        /// <param name="append">If true, appends items instead of clearing existing ones.</param>
        public static void PopulateComboBox(ComboBox comboBox, string tableName, string columnName, bool append = false)
        {
            try
            {
                List<object> items = new List<object>();
                string sql = $"SELECT DISTINCT {columnName} FROM {tableName} WHERE {columnName} IS NOT NULL ORDER BY {columnName}";

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Already filtered by SQL IS NOT NULL, but double-check
                            if (!reader.IsDBNull(0))
                                items.Add(reader.GetValue(0));
                        }
                    }
                }

                if (!append)
                    comboBox.Items.Clear();

                comboBox.Items.AddRange(items.ToArray());
            }
            catch (Exception ex)
            {

            }
        }
        public static void PopulateComboBox(object comboControl, string tableName, string columnName, bool append = false)
        {
            try
            {
                List<object> items = new List<object>();
                string sql = $"SELECT DISTINCT {columnName} FROM {tableName} WHERE {columnName} IS NOT NULL ORDER BY {columnName}";

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
                        }
                    }
                }

                if (comboControl is ComboBox cb)
                {
                    if (!append) cb.Items.Clear();
                    cb.Items.AddRange(items.ToArray());
                }
                else if (comboControl is DataGridViewComboBoxColumn dgvCb)
                {
                    if (!append) dgvCb.Items.Clear();
                    dgvCb.Items.AddRange(items.ToArray());
                }
                else
                {
                    throw new ArgumentException("comboControl must be ComboBox or DataGridViewComboBoxColumn");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
