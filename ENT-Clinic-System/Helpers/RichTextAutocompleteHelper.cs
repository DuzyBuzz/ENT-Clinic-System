using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class RichTextBulletAutocompleteHelper
    {
        // Store autocomplete data from DB
        private static readonly Dictionary<string, List<string>> columnData = new Dictionary<string, List<string>>();
        private static readonly Dictionary<RichTextBox, string> lastSuggestion = new Dictionary<RichTextBox, string>();

        /// <summary>
        /// Loads autocomplete entries from the database for specified columns.
        /// Call this once on Form Load.
        /// </summary>
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

        /// <summary>
        /// Enables both bulleting and autocomplete for a RichTextBox.
        /// </summary>
        public static void Enable(RichTextBox rtb, string tableName, string columnName)
        {
            if (rtb == null || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName))
                return;

            string key = $"{tableName}.{columnName}";
            if (!columnData.ContainsKey(key))
                columnData[key] = new List<string>();

            // Ensure first bullet
            if (string.IsNullOrWhiteSpace(rtb.Text))
            {
                rtb.Text = "• ";
                rtb.SelectionStart = rtb.Text.Length;
            }

            // --- ENTER HANDLING (reverse logic) ---
            rtb.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    // If there is an active suggestion -> accept instead of newline
                    if (lastSuggestion.ContainsKey(rtb) && !string.IsNullOrEmpty(lastSuggestion[rtb]))
                    {
                        e.SuppressKeyPress = true;

                        // Accept suggestion
                        rtb.SelectionStart += rtb.SelectionLength;
                        rtb.SelectionLength = 0;
                        lastSuggestion[rtb] = null; // ✅ clear suggestion so Backspace works
                    }
                    else
                    {
                        // Let Enter insert newline, bullet is handled in KeyUp
                        e.SuppressKeyPress = false;
                    }
                }

                // ✅ Reset suggestion if user presses Backspace or Delete
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    lastSuggestion[rtb] = null;
                }
            };

            rtb.KeyUp += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        int currentLine = rtb.GetLineFromCharIndex(rtb.SelectionStart);
                        int prevLine = currentLine - 1;

                        // Fix previous line
                        if (prevLine >= 0)
                        {
                            string prevText = GetLineText(rtb, prevLine).Trim();
                            if (prevText == "•" || prevText == "• ")
                            {
                                // Remove the newline because prev line had only a bullet
                                int prevLineStart = rtb.GetFirstCharIndexFromLine(prevLine);
                                int currLineStart = rtb.GetFirstCharIndexFromLine(currentLine);
                                if (currLineStart > prevLineStart)
                                {
                                    rtb.Select(currLineStart - Environment.NewLine.Length, Environment.NewLine.Length);
                                    rtb.SelectedText = string.Empty;
                                    rtb.SelectionStart = prevLineStart + (rtb.Lines[prevLine].Length);
                                }
                                return;
                            }
                            else if (!prevText.StartsWith("•"))
                            {
                                int prevLineStart = rtb.GetFirstCharIndexFromLine(prevLine);
                                rtb.SelectionStart = prevLineStart;
                                rtb.SelectionLength = 0;
                                rtb.SelectedText = "• ";
                            }
                        }

                        // Ensure current line starts with bullet
                        string currText = GetLineText(rtb, currentLine).TrimStart();
                        if (!currText.StartsWith("•"))
                        {
                            int currLineStart = rtb.GetFirstCharIndexFromLine(currentLine);
                            rtb.SelectionStart = currLineStart;
                            rtb.SelectionLength = 0;
                            rtb.SelectedText = "• ";
                        }
                    }
                    catch
                    {
                        if (string.IsNullOrWhiteSpace(rtb.Text))
                        {
                            rtb.Text = "• ";
                            rtb.SelectionStart = rtb.Text.Length;
                        }
                    }
                }

                // --- SAFEGUARD EMPTY TEXT (Ctrl+A + Delete) ---
                if (string.IsNullOrWhiteSpace(rtb.Text))
                {
                    rtb.Text = "• ";
                    rtb.SelectionStart = rtb.Text.Length;
                }
            };

            // --- AUTOCOMPLETE HANDLING ---
            rtb.TextChanged += (s, e) =>
            {
                try
                {
                    int pos = rtb.SelectionStart;
                    if (pos <= 0 || pos > rtb.Text.Length) return;

                    int lastBullet = rtb.Text.LastIndexOf('•', pos - 1);
                    int lastNewLine = rtb.Text.LastIndexOf('\n', pos - 1);
                    int start = Math.Max(lastBullet, lastNewLine) + 1;
                    if (start < 0 || start >= rtb.Text.Length) start = 0;

                    string currentWord = rtb.Text.Substring(start, pos - start).TrimStart();
                    if (string.IsNullOrEmpty(currentWord))
                    {
                        lastSuggestion[rtb] = null;
                        return;
                    }

                    string match = columnData[key]
                        .FirstOrDefault(sug => sug.StartsWith(currentWord, StringComparison.OrdinalIgnoreCase));

                    if (!string.IsNullOrEmpty(match) && match.Length > currentWord.Length)
                    {
                        rtb.SelectionStart = pos;
                        rtb.SelectionLength = 0;
                        rtb.SelectedText = match.Substring(currentWord.Length);
                        rtb.SelectionStart = pos;
                        rtb.SelectionLength = match.Length - currentWord.Length;
                        lastSuggestion[rtb] = match;
                    }
                    else
                    {
                        lastSuggestion[rtb] = null;
                    }
                }
                catch
                {
                    // Fail-safe: restore at least one bullet
                    if (string.IsNullOrWhiteSpace(rtb.Text))
                    {
                        rtb.Text = "• ";
                        rtb.SelectionStart = rtb.Text.Length;
                    }
                }
            };
        }

        private static string GetLineText(RichTextBox rtb, int lineIndex)
        {
            if (lineIndex < 0 || lineIndex >= rtb.Lines.Length)
                return string.Empty;
            return rtb.Lines[lineIndex];
        }
    }
}
