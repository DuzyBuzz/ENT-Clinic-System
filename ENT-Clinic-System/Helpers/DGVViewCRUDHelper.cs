using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public class SimpleDGVCRUDHelper
    {
        private DataGridView dgv;
        private string tableName;
        private string primaryKeyColumn;
        private HashSet<string> readonlyColumns;
        private Dictionary<string, object> oldCellValues = new Dictionary<string, object>();

        // Pagination
        private int pageSize = 50;
        private int currentPage = 1;
        private int totalRecords = 0;
        private int totalPages = 0;
        private Label pageInfoLabel;

        private string customSelectQuery; // optional custom query

        public SimpleDGVCRUDHelper(
            DataGridView dgv,
            string tableName,
            string primaryKeyColumn,
            List<string> readonlyColumns = null)
        {
            this.dgv = dgv;
            this.tableName = tableName;
            this.primaryKeyColumn = primaryKeyColumn;
            this.readonlyColumns = readonlyColumns != null ? new HashSet<string>(readonlyColumns) : new HashSet<string>();

            // Event subscriptions
            dgv.CellBeginEdit -= Dgv_CellBeginEdit;
            dgv.CellBeginEdit += Dgv_CellBeginEdit;

            dgv.CellEndEdit -= Dgv_CellEndEdit;
            dgv.CellEndEdit += Dgv_CellEndEdit;

            dgv.ColumnHeaderMouseClick -= Dgv_HeaderMouseClickIgnore;
            dgv.ColumnHeaderMouseClick += Dgv_HeaderMouseClickIgnore;
        }

        #region Custom Query
        public void SetCustomSelectQuery(string query)
        {
            this.customSelectQuery = query;
        }
        #endregion

        #region Ignore Header Right-Click
        private void Dgv_HeaderMouseClickIgnore(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;
        }
        #endregion

        #region Load / Refresh
        public void LoadData(int page = 1)
        {
            try
            {
                currentPage = page;

                // Count total records
                string countSql = "SELECT COUNT(*) FROM " + tableName;
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // If custom query exists, wrap it in a subquery for counting
                    if (!string.IsNullOrEmpty(customSelectQuery))
                        countSql = "SELECT COUNT(*) FROM (" + customSelectQuery + ") AS temp";

                    using (MySqlCommand cmdCount = new MySqlCommand(countSql, conn))
                    {
                        totalRecords = Convert.ToInt32(cmdCount.ExecuteScalar());
                    }
                }

                totalPages = Math.Max(1, (int)Math.Ceiling((double)totalRecords / pageSize));
                int offset = (currentPage - 1) * pageSize;

                string sql = string.IsNullOrEmpty(customSelectQuery) ?
                    "SELECT * FROM " + tableName + " LIMIT @limit OFFSET @offset" :
                    customSelectQuery + " LIMIT @limit OFFSET @offset";

                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@limit", pageSize);
                        cmd.Parameters.AddWithValue("@offset", offset);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgv.DataSource = dt;
                        }
                    }
                }

                UpdatePageInfoLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load data: " + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Refresh()
        {
            LoadData(currentPage);
        }

        public void SetPageInfoLabel(Label label)
        {
            pageInfoLabel = label;
        }

        private void UpdatePageInfoLabel()
        {
            if (pageInfoLabel != null)
                pageInfoLabel.Text = string.Format("Page {0} of {1}", currentPage, totalPages);
        }

        public void NextPage()
        {
            if (currentPage < totalPages)
                LoadData(currentPage + 1);
        }

        public void PreviousPage()
        {
            if (currentPage > 1)
                LoadData(currentPage - 1);
        }
        #endregion

        #region Editing
        private void Dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                var cell = dgv[e.ColumnIndex, e.RowIndex];
                string key = string.Format("{0}:{1}", e.RowIndex, e.ColumnIndex);
                oldCellValues[key] = cell.Value;
            }
            catch { }
        }

        private void Dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string columnName = dgv.Columns[e.ColumnIndex].DataPropertyName;
                if (readonlyColumns.Contains(columnName))
                {
                    MessageBox.Show(columnName + " is read-only.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgv[e.ColumnIndex, e.RowIndex].Value = oldCellValues[string.Format("{0}:{1}", e.RowIndex, e.ColumnIndex)];
                    return;
                }

                string key = string.Format("{0}:{1}", e.RowIndex, e.ColumnIndex);
                object oldValue = oldCellValues.ContainsKey(key) ? oldCellValues[key] : null;
                object newValue = dgv[e.ColumnIndex, e.RowIndex].Value;

                if (!object.Equals(oldValue, newValue))
                {
                    DialogResult result = MessageBox.Show(
                        "Save changes to " + columnName + "?",
                        "Confirm Update",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        UpdateCellValue(e.RowIndex, columnName);
                        LoadData(currentPage);
                    }
                    else
                    {
                        dgv[e.ColumnIndex, e.RowIndex].Value = oldValue;
                    }
                }

                oldCellValues.Remove(key);
            }
            catch { }
        }

        private void UpdateCellValue(int rowIndex, string columnName)
        {
            object value = dgv[columnName, rowIndex].Value ?? DBNull.Value;
            object id = dgv[primaryKeyColumn, rowIndex].Value;
            if (id == null) throw new Exception("Cannot determine primary key for update.");

            string sql = "UPDATE " + tableName + " SET `" + columnName + "`=@value WHERE " + primaryKeyColumn + "=@id";

            using (MySqlConnection conn = DBConfig.GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRow(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            string sql = $"DELETE FROM {tableName} WHERE {primaryKeyColumn}=@id";

            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
