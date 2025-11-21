using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ENT_Clinic_System.Helpers
{
    public static class AutoCompleteDgvHelper
    {
        private static readonly TextInfo TextInfo = CultureInfo.CurrentCulture.TextInfo;

        /// <summary>
        /// Enables title case autocomplete Suggest+Append for a DataGridViewTextBoxColumn.
        /// Loads suggestions from the 'autocomplete_entries' table.
        /// </summary>
        public static void InitializeAutocompleteColumn(DataGridView dgv, string dgvColumnName, string entryColumnName)
        {
            if (dgv == null || string.IsNullOrWhiteSpace(dgvColumnName) || string.IsNullOrWhiteSpace(entryColumnName))
                return;

            try
            {
                // ✅ Load autocomplete values and convert to Title Case
                var autocompleteValues = LoadExistingAutocompleteValues(entryColumnName)
                    .Select(v => ToTitleCase(v))
                    .Distinct()
                    .ToList();

                // Attach events once
                dgv.EditingControlShowing -= OnEditingControlShowing;
                dgv.EditingControlShowing += OnEditingControlShowing;

                void OnEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
                {
                    if (dgv.CurrentCell == null || dgv.CurrentCell.OwningColumn == null)
                        return;

                    if (dgv.CurrentCell.OwningColumn.Name != dgvColumnName)
                        return;

                    if (e.Control is TextBox tb)
                    {
                        // AutoComplete setup
                        tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        tb.AutoCompleteSource = AutoCompleteSource.CustomSource;

                        AutoCompleteStringCollection autoSource = new AutoCompleteStringCollection();
                        autoSource.AddRange(autocompleteValues?.ToArray() ?? Array.Empty<string>());
                        tb.AutoCompleteCustomSource = autoSource;

                        // Allow normal typing
                        tb.CharacterCasing = CharacterCasing.Normal;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing autocomplete column:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Saves unique non-empty title case user entries from a DataGridViewTextBoxColumn
        /// into the autocomplete_entries table (if not already present).
        /// </summary>
        public static void SaveAllAutocompleteEntries(DataGridView dgv, string dgvColumnName, string entryColumnName)
        {
            try
            {
                if (dgv == null || dgv.Rows.Count == 0)
                    return;

                HashSet<string> uniqueValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;

                    var cellValue = row.Cells[dgvColumnName].Value?.ToString()?.Trim();
                    if (!string.IsNullOrEmpty(cellValue))
                        uniqueValues.Add(ToTitleCase(cellValue));
                }

                if (uniqueValues.Count == 0) return;

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    foreach (var value in uniqueValues)
                    {
                        string checkQuery = "SELECT COUNT(*) FROM v_autocomplete_entries WHERE column_name=@col AND value=@val";
                        using (var checkCmd = new MySqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@col", entryColumnName);
                            checkCmd.Parameters.AddWithValue("@val", value);
                            long count = Convert.ToInt64(checkCmd.ExecuteScalar());

                            if (count == 0)
                            {
                                string insertQuery = "INSERT INTO v_autocomplete_entries (column_name, value) VALUES (@col, @val)";
                                using (var insertCmd = new MySqlCommand(insertQuery, conn))
                                {
                                    insertCmd.Parameters.AddWithValue("@col", entryColumnName);
                                    insertCmd.Parameters.AddWithValue("@val", value);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignore errors silently
            }
        }

        /// <summary>
        /// Loads autocomplete values from DB.
        /// </summary>
        private static List<string> LoadExistingAutocompleteValues(string columnName)
        {
            List<string> values = new List<string>();

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT DISTINCT value FROM v_autocomplete_entries WHERE column_name=@col ORDER BY value ASC";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@col", columnName);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string val = reader["value"]?.ToString()?.Trim();
                                if (!string.IsNullOrEmpty(val))
                                    values.Add(val);
                            }
                        }
                    }
                }
            }
            catch
            {
                // Ignore errors silently
            }

            return values;
        }

        /// <summary>
        /// Converts a string to Title Case.
        /// </summary>
        private static string ToTitleCase(string input)
        {
            return string.IsNullOrEmpty(input) ? input : TextInfo.ToTitleCase(input.ToLower());
        }
    }
}
