using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class RichTextBulletAutocompleteHelper
    {
        // Autocomplete data from DB
        private static readonly Dictionary<string, List<string>> columnData = new Dictionary<string, List<string>>();

        // Last suggestion per RichTextBox
        private static readonly Dictionary<RichTextBox, string> lastSuggestion = new Dictionary<RichTextBox, string>();

        // Autocomplete state machine per RichTextBox
        private enum AutoState { Normal, Suggesting, Accepted }
        private static readonly Dictionary<RichTextBox, AutoState> state = new Dictionary<RichTextBox, AutoState>();

        /// <summary>
        /// Load autocomplete entries from DB once on Form Load
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
                    string key = tableName + "." + column;
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
        /// Enable bullets + autocomplete on a RichTextBox
        /// </summary>
        public static void Enable(RichTextBox rtb, string tableName, string columnName)
        {
            if (rtb == null || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName))
                return;

            string key = tableName + "." + columnName;
            if (!columnData.ContainsKey(key))
                columnData[key] = new List<string>();

            // Ensure first bullet
            if (string.IsNullOrWhiteSpace(rtb.Text))
            {
                rtb.Text = "• ";
                rtb.SelectionStart = rtb.Text.Length;
            }

            state[rtb] = AutoState.Normal;

            // --- ENTER / BACKSPACE HANDLING ---
            rtb.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (lastSuggestion.ContainsKey(rtb) && !string.IsNullOrEmpty(lastSuggestion[rtb]))
                    {
                        e.SuppressKeyPress = true;

                        // Accept suggestion
                        rtb.SelectionStart += rtb.SelectionLength;
                        rtb.SelectionLength = 0;

                        // Mark as accepted → stop suggesting until user types again
                        state[rtb] = AutoState.Accepted;
                        lastSuggestion[rtb] = null;
                        return;
                    }
                }

                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    lastSuggestion[rtb] = null;

                    // Allow clean deletion in Accepted state
                    if (state[rtb] == AutoState.Accepted)
                        return;
                }
            };

            // --- BULLET HANDLING ---
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
                                // Remove newline if prev line only has bullet
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

                // Safeguard when everything deleted
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
                    // Skip autocomplete if in Accepted state
                    if (state[rtb] == AutoState.Accepted)
                        return;

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
                        state[rtb] = AutoState.Normal;
                        return;
                    }

                    string match = null;
                    foreach (string sug in columnData[key])
                    {
                        if (sug.StartsWith(currentWord, StringComparison.OrdinalIgnoreCase))
                        {
                            match = sug;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(match) && match.Length > currentWord.Length)
                    {
                        rtb.SelectionStart = pos;
                        rtb.SelectionLength = 0;
                        rtb.SelectedText = match.Substring(currentWord.Length);
                        rtb.SelectionStart = pos;
                        rtb.SelectionLength = match.Length - currentWord.Length;
                        lastSuggestion[rtb] = match;

                        // Now suggesting
                        state[rtb] = AutoState.Suggesting;
                    }
                    else
                    {
                        lastSuggestion[rtb] = null;
                        state[rtb] = AutoState.Normal;
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
            };

            // --- Unlock autocomplete when user types ---
            rtb.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar))
                {
                    if (state[rtb] == AutoState.Accepted)
                        state[rtb] = AutoState.Normal;
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
