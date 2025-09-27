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

        private int pageSize = 100;
        private int currentPage = 1;
        private int totalRecords = 0;
        private int totalPages = 0;

        private Label pageInfoLabel;

        public DGVCrudHelper(DataGridView dgv, string tableName, List<string> columns, string primaryKeyColumn)
        {
            this.dgv = dgv;
            this.tableName = tableName;
            this.columns = columns;
            this.primaryKeyColumn = primaryKeyColumn;

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

                using (var conn = DBConfig.GetConnection())
                using (var cmdCount = new MySqlCommand($"SELECT COUNT(*) FROM {tableName}", conn))
                {
                    conn.Open();
                    totalRecords = Convert.ToInt32(cmdCount.ExecuteScalar());
                }

                totalPages = Math.Max(1, (int)Math.Ceiling((double)totalRecords / pageSize));

                int offset = (currentPage - 1) * pageSize;
                string columnsString = string.Join(",", columns);
                string sql = $"SELECT {columnsString}, {primaryKeyColumn} FROM {tableName} LIMIT @limit OFFSET @offset";

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@limit", pageSize);
                    cmd.Parameters.AddWithValue("@offset", offset);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }

                UpdatePageInfoLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load data: " + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region CRUD Events
        private void Dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                var cell = dgv[e.ColumnIndex, e.RowIndex];
                oldCellValues[e.RowIndex] = cell.Value;
            }
            catch { /* silently ignore */ }
        }

        private void Dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
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
                            UpdateCellValueSafe(e.RowIndex, e.ColumnIndex);
                            LoadData(currentPage);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Update failed: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cell.Value = oldValue;
                        }
                    }
                    else
                    {
                        cell.Value = oldValue;
                    }
                }
            }
            catch { /* ignore unexpected errors */ }
        }

        private void UpdateCellValueSafe(int rowIndex, int colIndex)
        {
            string columnName = columns[colIndex];
            object value = dgv[colIndex, rowIndex].Value;
            object id = dgv[primaryKeyColumn, rowIndex].Value;

            // Handle empty values
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                value = DBNull.Value;
            else
            {
                // Convert to proper type
                Type targetType = dgv.Columns[colIndex].ValueType;
                try
                {
                    if (targetType == typeof(int))
                        value = Convert.ToInt32(value);
                    else if (targetType == typeof(decimal))
                        value = Convert.ToDecimal(value);
                    else if (targetType == typeof(double))
                        value = Convert.ToDouble(value);
                    else if (targetType == typeof(bool))
                        value = Convert.ToBoolean(value);
                    else if (targetType == typeof(DateTime))
                    {
                        if (!DateTime.TryParse(value.ToString(), out DateTime dt))
                            throw new Exception($"Invalid date value for {columnName}.");
                        value = dt;
                    }
                    else
                    {
                        value = value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Invalid value for column '{columnName}': {ex.Message}");
                }
            }

            string sql = $"UPDATE {tableName} SET {columnName}=@value WHERE {primaryKeyColumn}=@id";

            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@value", value);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void Dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            e.Cancel = true;
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
                    MessageBox.Show("Delete failed: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
