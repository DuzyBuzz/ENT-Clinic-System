using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class AutoCompleteHelper
    {
        /// <summary>
        /// Populates autocomplete suggestions for a TextBox or ComboBox from multiple database columns.
        /// </summary>
        /// <param name="control">TextBox or ComboBox to attach autocomplete to.</param>
        /// <param name="tableName">Database table name.</param>
        /// <param name="columnNames">List of database columns to include in autocomplete.</param>
        public static void SetupAutoComplete(Control control, string tableName, List<string> columnNames)
        {
            try
            {
                if (columnNames == null || columnNames.Count == 0)
                    throw new ArgumentException("At least one column must be specified.");

                HashSet<string> suggestions = new HashSet<string>(); // use HashSet to avoid duplicates

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    foreach (var column in columnNames)
                    {
                        string sql = $"SELECT DISTINCT {column} FROM {tableName} ORDER BY {column}";
                        using (var cmd = new MySqlCommand(sql, conn))
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                    suggestions.Add(reader.GetString(0));
                            }
                        }
                    }
                }

                string[] suggestionArray = new string[suggestions.Count];
                suggestions.CopyTo(suggestionArray);

                if (control is TextBox textBox)
                {
                    var autoComplete = new AutoCompleteStringCollection();
                    autoComplete.AddRange(suggestionArray);
                    textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    textBox.AutoCompleteCustomSource = autoComplete;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.Items.Clear();
                    comboBox.Items.AddRange(suggestionArray);
                    comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
                else
                {
                    throw new ArgumentException("Control must be a TextBox or ComboBox.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load autocomplete data: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
