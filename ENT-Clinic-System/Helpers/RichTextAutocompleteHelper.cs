using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    internal static class RichTextBulletAutocompleteHelper
    {
        private static readonly Dictionary<string, List<string>> columnData = new Dictionary<string, List<string>>();
        private static readonly Dictionary<RichTextBox, string> lastSuggestion = new Dictionary<RichTextBox, string>();
        private static readonly Dictionary<RichTextBox, bool> suspendAutocomplete = new Dictionary<RichTextBox, bool>();

        public static void LoadColumnsData(string tableName, List<string> columns)
        {
            if (string.IsNullOrEmpty(tableName) || columns == null || columns.Count == 0)
                return;

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                foreach (var column in columns)
                {
                    string key = $"{tableName}.{column}";
                    if (!columnData.ContainsKey(key))
                        columnData[key] = new List<string>();

                    string sql = "SELECT DISTINCT value FROM autocomplete_entries WHERE column_name=@col";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@col", column);
                        using (var reader = cmd.ExecuteReader())
                        {
                            columnData[key].Clear();
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                    columnData[key].Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
        }

        public static void Enable(RichTextBox rtb, string tableName, string columnName)
        {
            if (rtb == null || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName))
                return;

            string key = $"{tableName}.{columnName}";
            if (!columnData.ContainsKey(key))
                columnData[key] = new List<string>();

            if (!suspendAutocomplete.ContainsKey(rtb))
                suspendAutocomplete[rtb] = false;

            if (!lastSuggestion.ContainsKey(rtb))
                lastSuggestion[rtb] = null;

            // --- Removed bullet insertion at start ---
            // (If you want to start empty, leave this blank)

            rtb.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(lastSuggestion[rtb]))
                {
                    // Accept suggestion on Enter
                    e.SuppressKeyPress = true;
                    suspendAutocomplete[rtb] = true;
                    lastSuggestion[rtb] = null;
                    suspendAutocomplete[rtb] = false;
                }

                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    // Allow deletion normally
                    lastSuggestion[rtb] = null;
                }
            };

            // --- Removed Enter KeyUp bullet behavior completely ---
            // No bullet insertion logic here anymore

            rtb.TextChanged += (s, e) =>
            {
                if (suspendAutocomplete[rtb])
                    return;

                try
                {
                    int pos = rtb.SelectionStart;
                    if (pos <= 0 || pos > rtb.Text.Length)
                        return;

                    // Find start of current word
                    int lastSpace = rtb.Text.LastIndexOf(' ', pos - 1);
                    int lastNewLine = rtb.Text.LastIndexOf('\n', pos - 1);
                    int start = Math.Max(lastSpace, lastNewLine) + 1;

                    if (start < 0 || start >= rtb.Text.Length)
                        start = 0;

                    string currentWord = rtb.Text.Substring(start, pos - start).Trim();
                    if (string.IsNullOrEmpty(currentWord))
                    {
                        lastSuggestion[rtb] = null;
                        return;
                    }

                    // Try to match suggestion
                    string match = columnData[key]
                        .FirstOrDefault(sug => sug.StartsWith(currentWord, StringComparison.OrdinalIgnoreCase));

                    if (!string.IsNullOrEmpty(match) && match.Length > currentWord.Length)
                    {
                        suspendAutocomplete[rtb] = true;

                        // Insert completion text
                        rtb.SelectionStart = pos;
                        rtb.SelectionLength = 0;
                        rtb.SelectedText = match.Substring(currentWord.Length);

                        // Highlight suggestion text
                        rtb.SelectionStart = pos;
                        rtb.SelectionLength = match.Length - currentWord.Length;

                        lastSuggestion[rtb] = match;
                        suspendAutocomplete[rtb] = false;
                    }
                    else
                    {
                        lastSuggestion[rtb] = null;
                    }
                }
                catch
                {
                    // Prevent crash on invalid text states
                    lastSuggestion[rtb] = null;
                }
            };
        }
    }
}
