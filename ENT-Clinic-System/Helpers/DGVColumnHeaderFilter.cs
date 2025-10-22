using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class DGVColumnHeaderFilterHelper
    {
        private static readonly Dictionary<DataGridView, Dictionary<string, string>> dgvFilters
            = new Dictionary<DataGridView, Dictionary<string, string>>();

        private static readonly Dictionary<DataGridView, Dictionary<string, string>> originalHeaders
            = new Dictionary<DataGridView, Dictionary<string, string>>();

        public static void Attach(DataGridView dgv)
        {
            if (dgv == null) throw new ArgumentNullException(nameof(dgv));
            if (dgvFilters.ContainsKey(dgv)) return;

            dgvFilters[dgv] = new Dictionary<string, string>();
            originalHeaders[dgv] = new Dictionary<string, string>();

            // Store original headers
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                originalHeaders[dgv][col.Name] = col.HeaderText;
            }

            dgv.ColumnHeaderMouseClick += Dgv_ColumnHeaderMouseClick;
        }

        private static void Dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (!(sender is DataGridView dgv) || e.ColumnIndex < 0) return;

            string columnName = dgv.Columns[e.ColumnIndex].Name;
            if (string.IsNullOrEmpty(columnName)) return;
            if (!(dgv.DataSource is DataTable dt)) return;

            try
            {
                HashSet<string> distinctValues = new HashSet<string>();
                foreach (DataRow row in dt.Rows)
                {
                    object val = row[columnName];
                    distinctValues.Add(val == null || val == DBNull.Value ? "(Blank)" : val.ToString());
                }

                if (!distinctValues.Any()) return;

                int maxWidth = 300;   // Max width of panel/buttons
                int maxHeight = 700;  // Max height of panel
                var panel = new NoVScrollFlowLayoutPanel
                {
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    AutoSize = true,
                    MaximumSize = new System.Drawing.Size(maxWidth, maxHeight)
                };


                // Create buttons
                var allButtons = new List<Button>();

                // "Show All" button
                ToolStripDropDown dropDown = null; // declare first

                // Show All button
                var showAllBtn = new Button
                {
                    Text = "Show All",
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    BackColor = !dgvFilters[dgv].ContainsKey(columnName) ? Color.LightBlue : Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                showAllBtn.FlatAppearance.BorderSize = 0;
                showAllBtn.Click += (s, ev) =>
                {
                    if (dgvFilters[dgv].ContainsKey(columnName))
                        dgvFilters[dgv].Remove(columnName);

                    ApplyFilters(dgv);
                    dropDown?.Close(); // now OK
                };

                panel.Controls.Add(showAllBtn);
                allButtons.Add(showAllBtn);

                // Distinct value buttons
                foreach (var val in distinctValues.OrderBy(v => v))
                {
                    var btn = new Button
                    {
                        Text = val,
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleLeft,
                        BackColor = dgvFilters[dgv].ContainsKey(columnName) &&
                                    dgvFilters[dgv][columnName] == (val == "(Blank)" ? string.Empty : val)
                                    ? Color.LightBlue
                                    : Color.White,
                        FlatStyle = FlatStyle.Flat
                    };
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += (s, ev) =>
                    {
                        dgvFilters[dgv][columnName] = val == "(Blank)" ? string.Empty : val;
                        ApplyFilters(dgv);
                        dropDown?.Close();
                    };
                    panel.Controls.Add(btn);
                    allButtons.Add(btn);
                }

                // Calculate max width among buttons
                int neededWidth = allButtons.Max(b => b.PreferredSize.Width);
                int finalWidth = Math.Min(neededWidth + 10, maxWidth); // add a little padding

                // Apply width to all buttons
                foreach (var btn in allButtons)
                {
                    btn.Width = finalWidth;
                }

                // Adjust panel size
                panel.Width = finalWidth + SystemInformation.VerticalScrollBarWidth; // add scrollbar space
                panel.Height = Math.Min(panel.PreferredSize.Height, maxHeight);

                var host = new ToolStripControlHost(panel)
                {
                    AutoSize = false,
                    Width = panel.Width,
                    Height = panel.Height
                };

                dropDown = new ToolStripDropDown();
                dropDown.Items.Add(host);
                dropDown.Show(Cursor.Position);

            }
            catch
            {
                // ignore errors
            }
        }


        private static void ApplyFilters(DataGridView dgv)
        {
            if (!(dgv.DataSource is DataTable dt)) return;
            if (!dgvFilters.ContainsKey(dgv)) return;

            try
            {
                string filterExpr = string.Join(" AND ", dgvFilters[dgv].Select(kv =>
                {
                    if (!dt.Columns.Contains(kv.Key)) return "1=1";

                    var col = dt.Columns[kv.Key];
                    string val = kv.Value;

                    if (col.DataType == typeof(string))
                        return $"[{kv.Key}] = '{val.Replace("'", "''")}'";

                    if (col.DataType == typeof(DateTime))
                    {
                        if (DateTime.TryParse(val, out DateTime dtVal))
                            return $"[{kv.Key}] = #{dtVal:MM/dd/yyyy}#";
                        return "1=1";
                    }

                    if (col.DataType == typeof(int) || col.DataType == typeof(decimal) || col.DataType == typeof(double))
                    {
                        if (decimal.TryParse(val, out decimal numVal))
                            return $"[{kv.Key}] = {numVal}";
                        return "1=1";
                    }

                    return $"[{kv.Key}] = '{val.Replace("'", "''")}'";
                }));

                dt.DefaultView.RowFilter = filterExpr;

                // Update column headers to show the selected filter
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (originalHeaders[dgv].ContainsKey(col.Name))
                    {
                        if (dgvFilters[dgv].ContainsKey(col.Name))
                            col.HeaderText = $"{originalHeaders[dgv][col.Name]} ({dgvFilters[dgv][col.Name]})";
                        else
                            col.HeaderText = originalHeaders[dgv][col.Name];
                    }
                }
            }
            catch
            {
                // ignore runtime errors
            }
        }

        public static void ResetFilters(DataGridView dgv)
        {
            if (dgv == null || !(dgv.DataSource is DataTable dt)) return;
            if (!dgvFilters.ContainsKey(dgv)) return;

            dgvFilters[dgv].Clear();
            dt.DefaultView.RowFilter = string.Empty;

            // Reset headers
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (originalHeaders[dgv].ContainsKey(col.Name))
                    col.HeaderText = originalHeaders[dgv][col.Name];
            }
        }
    }
}
public class NoVScrollFlowLayoutPanel : FlowLayoutPanel
{
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            // Remove vertical scrollbar
            cp.Style &= ~0x00200000; // WS_VSCROLL = 0x00200000
            return cp;
        }
    }

    public NoVScrollFlowLayoutPanel()
    {
        this.AutoScroll = true; // still allow scrolling with mouse wheel
    }
}
