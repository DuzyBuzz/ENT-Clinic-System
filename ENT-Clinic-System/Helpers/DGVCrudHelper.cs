using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public class DGVCrudHelper
    {
        private DataGridView dgv;
        private string tableName;
        private List<string> columns;
        private string primaryKeyColumn;
        private Dictionary<int, object> oldCellValues = new Dictionary<int, object>();

        private int pageSize = 100;       // rows per page
        private int currentPage = 1;
        private int totalRecords = 0;
        private int totalPages = 0;

        private Label pageInfoLabel; // label to show page info

        public DGVCrudHelper(DataGridView dgv, string tableName, List<string> columns, string primaryKeyColumn)
        {
            this.dgv = dgv;
            this.tableName = tableName;
            this.columns = columns;
            this.primaryKeyColumn = primaryKeyColumn;

            // Subscribe to events
            dgv.CellBeginEdit -= Dgv_CellBeginEdit;
            dgv.CellBeginEdit += Dgv_CellBeginEdit;

            dgv.CellEndEdit -= Dgv_CellEndEdit;
            dgv.CellEndEdit += Dgv_CellEndEdit;

            dgv.UserDeletingRow -= Dgv_UserDeletingRow;
            dgv.UserDeletingRow += Dgv_UserDeletingRow;
        }

        #region Pagination

        public int CurrentPage => currentPage;
        public int TotalPages => totalPages;

        public void SetPageInfoLabel(Label label)
        {
            pageInfoLabel = label;
        }

        private void UpdatePageInfoLabel()
        {
            if (pageInfoLabel != null)
                pageInfoLabel.Text = $"Page {currentPage} of {totalPages}";
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

        #region Load Data

        public void LoadData(int page = 1)
        {
            try
            {

                currentPage = page;

                // Count total rows
                using (var conn = DBConfig.GetConnection())
                using (var cmdCount = new MySqlCommand($"SELECT COUNT(*) FROM {tableName}", conn))
                {
                    conn.Open();
                    totalRecords = Convert.ToInt32(cmdCount.ExecuteScalar());
                }

                totalPages = Math.Max(1, (int)Math.Ceiling((double)totalRecords / pageSize));

                // Load current page data
                int offset = (currentPage - 1) * pageSize;
                string columnsString = string.Join(",", columns);
                string sql = $"SELECT {columnsString} FROM {tableName} LIMIT @limit OFFSET @offset";

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@limit", pageSize);
                    cmd.Parameters.AddWithValue("@offset", offset);

                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgv.DataSource = dt;
                    }
                }

                // Update page label
                UpdatePageInfoLabel();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Failed to load data: " + ex.Message);
                
            }
        }

        #endregion

        #region CRUD Events

        private void Dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var cell = dgv[e.ColumnIndex, e.RowIndex];
            oldCellValues[e.RowIndex] = cell.Value;
        }

        private void Dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dgv[e.ColumnIndex, e.RowIndex];
            object oldValue = oldCellValues.ContainsKey(e.RowIndex) ? oldCellValues[e.RowIndex] : null;

            if ((cell.Value == null && oldValue != null) || (cell.Value != null && !cell.Value.Equals(oldValue)))
            {
                DialogResult result = MessageBox.Show(
                    $"Do you want to save changes for {columns[e.ColumnIndex]}?",
                    "Confirm Update",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        UpdateCellValue(e.RowIndex, e.ColumnIndex);
                        MessageBox.Show("Updated successfully!");
                        LoadData(currentPage); // reload current page
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Update failed: " + ex.Message);
                        cell.Value = oldValue;
                    }
                }
                else
                {
                    cell.Value = oldValue;
                }
            }
        }

        private void UpdateCellValue(int rowIndex, int colIndex)
        {
            string columnName = columns[colIndex];
            object value = dgv[colIndex, rowIndex].Value;
            object id = dgv[primaryKeyColumn, rowIndex].Value;

            string sql = $"UPDATE {tableName} SET {columnName}=@value WHERE {primaryKeyColumn}=@id";

            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@value", value ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void Dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            object id = e.Row.Cells[primaryKeyColumn].Value;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DeleteRow(id);
                    LoadData(currentPage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete failed: " + ex.Message);
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void DeleteRow(object id)
        {
            string sql = $"DELETE FROM {tableName} WHERE {primaryKeyColumn}=@id";
            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertRow(Dictionary<string, object> rowValues)
        {
            string columnsString = string.Join(",", rowValues.Keys);
            string parametersString = "@" + string.Join(",@", rowValues.Keys);

            string sql = $"INSERT INTO {tableName} ({columnsString}) VALUES ({parametersString})";

            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                foreach (var kv in rowValues)
                    cmd.Parameters.AddWithValue("@" + kv.Key, kv.Value ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
