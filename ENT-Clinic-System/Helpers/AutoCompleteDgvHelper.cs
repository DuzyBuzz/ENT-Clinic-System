using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ENT_Clinic_System.Helpers
{
    public static class AutoCompleteDgvHelper
    {
        public static void LoadColumnAutocomplete(DataGridView dgv, string dgvColumnName, string entryColumnName)
        {
            if (dgv == null || string.IsNullOrWhiteSpace(dgvColumnName) || string.IsNullOrWhiteSpace(entryColumnName))
                return;

            try
            {
                void RefreshItems()
                {
                    try
                    {
                        HashSet<string> autocompleteValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase); // avoid duplicates

                        using (var conn = DBConfig.GetConnection())
                        {
                            conn.Open();
                            string selectQuery = "SELECT value FROM autocomplete_entries WHERE column_name=@colName ORDER BY value ASC";
                            using (var cmd = new MySqlCommand(selectQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@colName", entryColumnName);
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        string val = reader["value"]?.ToString()?.Trim();
                                        if (!string.IsNullOrEmpty(val))
                                            autocompleteValues.Add(val); // HashSet automatically ignores duplicates
                                    }
                                }
                            }
                        }

                        if (dgv.Columns[dgvColumnName] is DataGridViewComboBoxColumn comboCol)
                        {
                            comboCol.Items.Clear();
                            comboCol.Items.AddRange(autocompleteValues.OrderBy(x => x).ToArray()); // sorted unique values
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error refreshing autocomplete items:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Initial load
                RefreshItems();

                // Subscribe to editing control showing
                dgv.EditingControlShowing -= Dgv_EditingControlShowing;
                dgv.EditingControlShowing += Dgv_EditingControlShowing;

                dgv.DataError -= Dgv_DataError;
                dgv.DataError += Dgv_DataError;

                void Dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
                {
                    if (dgv.CurrentCell.OwningColumn.Name != dgvColumnName) return;
                    if (!(e.Control is ComboBox combo)) return;

                    combo.DropDownStyle = ComboBoxStyle.DropDown;
                    combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    combo.AutoCompleteSource = AutoCompleteSource.ListItems;

                    combo.KeyDown -= Combo_KeyDown;
                    combo.KeyDown += Combo_KeyDown;

                    combo.Leave -= Combo_Leave;
                    combo.Leave += Combo_Leave;

                    void Combo_KeyDown(object s, KeyEventArgs ke)
                    {
                        if (ke.KeyCode == Keys.Enter)
                        {
                            SaveAndRefresh(combo);
                            MoveToNextRow(dgv);
                            ke.Handled = true;
                        }
                    }

                    void Combo_Leave(object s, EventArgs le)
                    {
                        SaveAndRefresh(combo);
                    }

                    void SaveAndRefresh(ComboBox c)
                    {
                        string input = c.Text.Trim();
                        if (string.IsNullOrEmpty(input)) return;

                        try
                        {
                            // Save to database if not exists
                            SaveEntryToDatabase(entryColumnName, input);

                            // Refresh collection and remove duplicates
                            RefreshItems();

                            c.Text = input; // restore text so it doesn't disappear
                        }
                        catch { /* ignore DB errors */ }
                    }
                }

                void Dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
                {
                    // suppress errors for free text
                    e.ThrowException = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing autocomplete column:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void MoveToNextRow(DataGridView dgv)
        {
            try
            {
                int colIndex = dgv.CurrentCell.ColumnIndex;
                int rowIndex = dgv.CurrentCell.RowIndex;

                if (rowIndex == dgv.Rows.Count - 1 && !dgv.Rows[rowIndex].IsNewRow)
                    dgv.Rows.Add();

                if (rowIndex < dgv.Rows.Count - 1)
                    dgv.CurrentCell = dgv[colIndex, rowIndex + 1];
            }
            catch { }
        }

        private static void SaveEntryToDatabase(string columnName, string value)
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM autocomplete_entries WHERE column_name=@colName AND value=@val";
                    using (var checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@colName", columnName);
                        checkCmd.Parameters.AddWithValue("@val", value);
                        long count = Convert.ToInt64(checkCmd.ExecuteScalar());

                        if (count == 0) // save only if not exists
                        {
                            string insertQuery = "INSERT INTO autocomplete_entries (column_name, value) VALUES (@colName, @val)";
                            using (var insertCmd = new MySqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@colName", columnName);
                                insertCmd.Parameters.AddWithValue("@val", value);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch { /* ignore DB errors */ }
        }
    }
}
