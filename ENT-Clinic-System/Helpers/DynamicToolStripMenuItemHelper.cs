using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// Helper to dynamically populate a ToolStripMenuItem or ContextMenuStrip with submenus from database.
    /// Each row becomes a parent menu item (like a date), and its submenus are defined by actions.
    /// Supports nested menus.
    /// </summary>
    public static class DynamicToolStripMenuItemHelper
    {
        /// <summary>
        /// Populates a ToolStripDropDown (either ContextMenuStrip or ToolStripMenuItem.DropDown) with items from database.
        /// </summary>
        /// <param name="parentMenu">The parent dropdown to populate. Can be ContextMenuStrip or ToolStripMenuItem.DropDown</param>
        /// <param name="tableName">Database table to query</param>
        /// <param name="idColumn">Primary key column name (int)</param>
        /// <param name="displayColumns">Columns to show in menu item text</param>
        /// <param name="whereClause">Optional WHERE clause (without "WHERE")</param>
        /// <param name="subMenuActions">Dictionary of sub-menu names and either Action<int> or Func<int, ToolStripMenuItem> for nested menus</param>
        public static void PopulateSubMenu(
            ToolStripDropDown parentMenu,
            string tableName,
            string idColumn,
            IEnumerable<string> displayColumns,
            string whereClause,
            Dictionary<string, object> subMenuActions // object can be Action<int> or Func<int, ToolStripMenuItem>
        )
        {
            if (parentMenu == null) throw new ArgumentNullException(nameof(parentMenu));
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentNullException(nameof(tableName));
            if (string.IsNullOrWhiteSpace(idColumn)) throw new ArgumentNullException(nameof(idColumn));
            if (displayColumns == null) throw new ArgumentException("Display columns cannot be null");
            if (subMenuActions == null || subMenuActions.Count == 0) throw new ArgumentException("SubMenu actions cannot be empty");

            parentMenu.Items.Clear();

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    var displayColsList = new List<string>(displayColumns);
                    if (displayColsList.Count == 0)
                        throw new ArgumentException("At least one display column is required.");

                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT ").Append(idColumn).Append(", ");
                    sb.Append(string.Join(", ", displayColsList));
                    sb.Append(" FROM ").Append(tableName);
                    if (!string.IsNullOrWhiteSpace(whereClause))
                        sb.Append(" WHERE ").Append(whereClause);
                    sb.Append(" ORDER BY ").Append(displayColsList[0]).Append(" ASC");

                    using (var cmd = new MySqlCommand(sb.ToString(), conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            parentMenu.Items.Add(new ToolStripMenuItem("No data found") { Enabled = false });
                            return;
                        }

                        while (reader.Read())
                        {
                            int rowId = reader.GetInt32(idColumn);

                            List<string> texts = new List<string>();
                            foreach (var col in displayColsList)
                                texts.Add(reader[col]?.ToString() ?? string.Empty);
                            string displayText = string.Join(" | ", texts);

                            ToolStripMenuItem parentItem = new ToolStripMenuItem(displayText)
                            {
                                Tag = rowId
                            };

                            // Add sub-menu items
                            foreach (var kvp in subMenuActions)
                            {
                                ToolStripMenuItem subItem;

                                if (kvp.Value is Action<int> simpleAction)
                                {
                                    subItem = new ToolStripMenuItem(kvp.Key) { Tag = rowId };
                                    subItem.Click += (s, e) =>
                                    {
                                        try
                                        {
                                            simpleAction.Invoke(rowId);
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Error executing action: " + ex.Message,
                                                "Action Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    };
                                }
                                else if (kvp.Value is Func<int, ToolStripMenuItem> nestedFunc)
                                {
                                    subItem = nestedFunc.Invoke(rowId);
                                    subItem.Tag = rowId;
                                }
                                else
                                {
                                    throw new ArgumentException("SubMenu action must be Action<int> or Func<int, ToolStripMenuItem>");
                                }

                                parentItem.DropDownItems.Add(subItem);
                            }

                            parentMenu.Items.Add(parentItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating menu: " + ex.Message, "Menu Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
