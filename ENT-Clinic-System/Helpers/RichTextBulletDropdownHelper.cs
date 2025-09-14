using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    internal static class RichTextBulletDropdownHelper
    {
        private static readonly Dictionary<string, List<string>> columnData = new Dictionary<string, List<string>>();
        private static readonly Dictionary<RichTextBox, ListBox> dropdowns = new Dictionary<RichTextBox, ListBox>();

        public static void LoadColumnsData(string tableName, List<string> columns)
        {
            if (string.IsNullOrEmpty(tableName) || columns == null || columns.Count == 0)
                return;

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    foreach (var column in columns)
                    {
                        string key = $"{tableName}.{column}";
                        if (!columnData.ContainsKey(key))
                            columnData[key] = new List<string>();
                        else
                            columnData[key].Clear();

                        string sql = "SELECT DISTINCT value FROM autocomplete_entries WHERE column_name=@col";
                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@col", column);
                            using (var reader = cmd.ExecuteReader())
                            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Error loading autocomplete data: " + ex.Message);
            }
        }

        public static void Enable(RichTextBox rtb, string tableName, string columnName)
        {
            if (rtb == null || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName))
                return;

            string key = $"{tableName}.{columnName}";
            if (!columnData.ContainsKey(key))
                columnData[key] = new List<string>();

            Form topForm = rtb.FindForm();
            ListBox dropdown = new ListBox
            {
                Visible = false,
                Font = rtb.Font,
                TabStop = false,
                IntegralHeight = false
            };
            dropdown.Click += (s, e) => AcceptSuggestion(rtb, key);
            topForm.Controls.Add(dropdown);
            dropdowns[rtb] = dropdown;

            EnsureBullet(rtb);

            // KeyDown only handles navigation and accepting suggestions
            rtb.KeyDown += (s, e) =>
            {
                try
                {
                    if (dropdown.Visible)
                    {
                        if (e.KeyCode == Keys.Down)
                        {
                            if (dropdown.SelectedIndex < dropdown.Items.Count - 1)
                                dropdown.SelectedIndex++;
                            e.SuppressKeyPress = true;
                        }
                        else if (e.KeyCode == Keys.Up)
                        {
                            if (dropdown.SelectedIndex > 0)
                                dropdown.SelectedIndex--;
                            e.SuppressKeyPress = true;
                        }
                        else if (e.KeyCode == Keys.Enter && dropdown.SelectedItem != null)
                        {
                            AcceptSuggestion(rtb, key);
                            e.SuppressKeyPress = true;
                        }
                        else if (e.KeyCode == Keys.Escape)
                        {
                            dropdown.Visible = false;
                            e.SuppressKeyPress = true;
                        }
                    }
                }
                catch { /* fail silently */ }
            };

            // KeyUp handles showing suggestions
            rtb.KeyUp += (s, e) =>
            {
                try
                {
                    EnsureBullet(rtb);
                    ShowDropdown(rtb, key);
                }
                catch { /* fail silently */ }
            };

            rtb.TextChanged += (s, e) =>
            {
                try
                {
                    ShowDropdown(rtb, key);
                }
                catch { /* fail silently */ }
            };
        }

        private static void EnsureBullet(RichTextBox rtb)
        {
            if (string.IsNullOrEmpty(rtb.Text))
            {
                rtb.Text = "• ";
                rtb.SelectionStart = rtb.Text.Length;
                return;
            }

            int currentLine = rtb.GetLineFromCharIndex(rtb.SelectionStart);
            if (currentLine < 0 || currentLine >= rtb.Lines.Length)
                return;

            string line = rtb.Lines[currentLine].TrimStart();
            if (!line.StartsWith("•"))
            {
                int lineStart = rtb.GetFirstCharIndexFromLine(currentLine);
                rtb.SelectionStart = lineStart;
                rtb.SelectionLength = 0;
                rtb.SelectedText = "• ";
                rtb.SelectionStart = lineStart + 2;
            }
        }
        private static void ShowDropdown(RichTextBox rtb, string key)
        {
            if (!dropdowns.ContainsKey(rtb)) return;
            ListBox dropdown = dropdowns[rtb];

            int pos = Math.Max(0, Math.Min(rtb.SelectionStart, rtb.Text.Length));

            // Find the start of the current word (after last space, bullet, or newline)
            int wordStart = rtb.Text.LastIndexOfAny(new char[] { ' ', '•', '\n' }, pos - 1);
            wordStart = (wordStart >= 0) ? wordStart + 1 : 0;

            int length = pos - wordStart;
            if (length < 0) length = 0;

            string currentWord = rtb.Text.Substring(wordStart, length);
            if (string.IsNullOrWhiteSpace(currentWord))
            {
                dropdown.Visible = false;
                return;
            }

            var matches = columnData[key]
                .Where(s => s.StartsWith(currentWord, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matches.Count == 0)
            {
                dropdown.Visible = false;
                return;
            }

            dropdown.Items.Clear();
            foreach (var m in matches) dropdown.Items.Add(m);
            dropdown.SelectedIndex = 0;

            Point caretPos = rtb.GetPositionFromCharIndex(rtb.SelectionStart);
            caretPos.Y += (int)rtb.Font.GetHeight();
            caretPos = rtb.PointToScreen(caretPos);
            caretPos = rtb.FindForm().PointToClient(caretPos);

            dropdown.Location = caretPos;
            dropdown.Width = Math.Max(250, rtb.Width);
            dropdown.Height = Math.Min(matches.Count, 10) * dropdown.ItemHeight;
            dropdown.Visible = true;
            dropdown.BringToFront();
        }

        private static void AcceptSuggestion(RichTextBox rtb, string key)
        {
            if (!dropdowns.ContainsKey(rtb)) return;
            ListBox dropdown = dropdowns[rtb];
            if (dropdown.SelectedItem == null) return;

            int pos = Math.Max(0, Math.Min(rtb.SelectionStart, rtb.Text.Length));

            // Replace only the current word (after last space, bullet, or newline)
            int wordStart = rtb.Text.LastIndexOfAny(new char[] { ' ', '•', '\n' }, pos - 1);
            wordStart = (wordStart >= 0) ? wordStart + 1 : 0;

            int length = pos - wordStart;
            if (length < 0) length = 0;

            string selectedText = dropdown.SelectedItem.ToString();

            rtb.Text = rtb.Text.Remove(wordStart, length);
            rtb.Text = rtb.Text.Insert(wordStart, selectedText);
            rtb.SelectionStart = wordStart + selectedText.Length;

            dropdown.Visible = false;
            rtb.Focus();
        }

    }
}
