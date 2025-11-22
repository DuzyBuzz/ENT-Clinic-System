using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// DGVViewCrudHelper
    /// - Loads data from a VIEW (viewName) for display/search
    /// - Applies updates/deletes against a target base table (baseTableName)
    /// - Mirrors DGVCrudHelper API/behavior (paging, search, date-range, inline edit/delete)
    /// - Prevents updates to columns not present in the base table (computed/joined view columns)
    /// </summary>
    public class DGVViewCrudHelper
    {
        private readonly DataGridView dgv;
        private readonly string viewName;             // used for SELECT / COUNT / SEARCH
        private readonly string baseTableName;        // used for UPDATE / DELETE
        private readonly string primaryKeyColumn;
        private ContextMenuStrip dgvContextMenu;

        // paging state
        public int PageSize { get; set; } = 1500;
        public int CurrentPage { get; private set; } = 1;
        public int TotalPages { get; private set; } = 1;
        public bool EnablePagination { get; set; } = true;

        // paging mode state
        private enum ActiveMode { None, DateRange }
        private ActiveMode lastMode = ActiveMode.None;
        private string lastDateColumn = null;
        private DateTime lastDateFrom = DateTime.MinValue;
        private DateTime lastDateTo = DateTime.MinValue;

        /// <summary>
        /// If true, the helper will only paginate using a date-from / date-to filter.
        /// When enabled, LoadData/Refresh/NextPage/PreviousPage will call SearchByDateRange.
        /// You must provide a date column & range (via AttachDateRangeControls and the bound button,
        /// or via SetDateRangePagination) before using paging.
        /// </summary>
        public bool OnlyDateRangePagination { get; set; } = false;

        private Label pageInfoLabel;

        // date picker overlay for date columns
        private DateTimePicker dgvDatePicker;

        // inline edit tracking
        private readonly Dictionary<string, object> oldCellValues = new Dictionary<string, object>();

        // search bindings
        private TextBox boundSearchBox;
        private Button boundSearchButton;
        private Button boundRefreshButton;
        private string[] searchableColumns = new string[0];
        private bool useFullTextSearch = false;
        private bool ensureIndexesAutomatically = true; // try to create indexes when attaching search controls

        // optional date range controls
        private DateTimePicker boundDateFrom;
        private DateTimePicker boundDateTo;
        private Button boundDateSearchButton;
        private string boundDateColumn;

        // editable columns from the base table
        private HashSet<string> editableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Create helper bound to a DataGridView, view (for read) and base table (for write).
        /// primaryKeyColumn must be present in the view so the helper can determine row identity.
        /// </summary>
        public DGVViewCrudHelper(DataGridView dgv, string viewName, string primaryKeyColumn, string baseTableName)
        {
            this.dgv = dgv ?? throw new ArgumentNullException(nameof(dgv));
            this.viewName = viewName ?? throw new ArgumentNullException(nameof(viewName));
            this.baseTableName = baseTableName ?? throw new ArgumentNullException(nameof(baseTableName));
            this.primaryKeyColumn = primaryKeyColumn ?? throw new ArgumentNullException(nameof(primaryKeyColumn));

            InitDatePicker();
            WireEvents();

            // load editable columns for the base table (used to avoid updating computed/view-only cols)
            LoadEditableColumns();
        }

        /// <summary>
        /// Programmatically set the date-range to use for pagination and immediately load page 1.
        /// </summary>
        public void SetDateRangePagination(string dateColumn, DateTime from, DateTime to)
        {
            if (string.IsNullOrWhiteSpace(dateColumn)) throw new ArgumentException("dateColumn required", nameof(dateColumn));

            lastMode = ActiveMode.DateRange;
            lastDateColumn = dateColumn;
            lastDateFrom = from.Date;
            lastDateTo = to.Date;

            // ensure UI-bound pickers (if any) reflect this range
            try
            {
                if (boundDateFrom != null) boundDateFrom.Value = lastDateFrom;
                if (boundDateTo != null) boundDateTo.Value = lastDateTo;
            }
            catch { /* ignore UI update failures */ }

            // perform the initial load for the date-range
            SearchByDateRange(lastDateColumn, lastDateFrom, lastDateTo, 1);
        }

        #region Initialization
        private void InitDatePicker()
        {
            dgvDatePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Visible = false
            };
            dgv.Controls.Add(dgvDatePicker);
            dgvDatePicker.CloseUp += DgvDatePicker_CloseUp;
            dgvDatePicker.TextChanged += DgvDatePicker_TextChanged;
        }

        private void WireEvents()
        {
            dgv.CellBeginEdit -= Dgv_CellBeginEdit;
            dgv.CellBeginEdit += Dgv_CellBeginEdit;

            dgv.CellEndEdit -= Dgv_CellEndEdit;
            dgv.CellEndEdit += Dgv_CellEndEdit;

            dgv.UserDeletingRow -= Dgv_UserDeletingRow;
            dgv.UserDeletingRow += Dgv_UserDeletingRow;

            dgv.CellClick -= Dgv_CellClick;
            dgv.CellClick += Dgv_CellClick;
            InitContextMenu();

        }
        #endregion

        #region Attach / Bind Controls
        /// <summary>
        /// Wire search (textbox + button) and refresh button.
        /// searchableCols: columns to search (e.g. new[]{ "name","code" }).
        /// useFullText: if true, helper uses MATCH...AGAINST (requires FULLTEXT index).
        /// ensureIndexes: if true, helper will attempt to create simple indexes on provided cols (best-effort).
        /// NOTE: index creation targets the base table (since views can't be indexed directly).
        /// </summary>
        public void AttachSearchControls(TextBox searchBox, Button searchButton, Button refreshButton, string[] searchableCols, bool useFullText = false, bool ensureIndexes = true)
        {
            searchableColumns = searchableCols ?? new string[0];
            useFullTextSearch = useFullText;
            ensureIndexesAutomatically = ensureIndexes;

            // detach previous handlers safely
            if (boundSearchButton != null) boundSearchButton.Click -= BoundSearchButton_Click;
            if (boundRefreshButton != null) boundRefreshButton.Click -= BoundRefreshButton_Click;
            if (boundSearchBox != null) boundSearchBox.KeyDown -= BoundSearchBox_KeyDown;

            boundSearchBox = searchBox;
            boundSearchButton = searchButton;
            boundRefreshButton = refreshButton;

            if (boundSearchButton != null) boundSearchButton.Click += BoundSearchButton_Click;
            if (boundRefreshButton != null) boundRefreshButton.Click += BoundRefreshButton_Click;
            if (boundSearchBox != null) boundSearchBox.KeyDown += BoundSearchBox_KeyDown;

            // Try to ensure indexes for faster search — best-effort, will not throw on failure
            if (ensureIndexesAutomatically && searchableColumns.Length > 0 && !useFullTextSearch)
            {
                try
                {
                    EnsureIndexesForSearch(searchableColumns);
                }
                catch
                {
                    // ignore any failure — it just means we couldn't create indexes (no privileges, etc.)
                }
            }
        }

        /// <summary>
        /// Attach date range controls (two DateTimePickers + button). Supply the view's date column name to filter.
        /// </summary>
        public void AttachDateRangeControls(DateTimePicker dateFrom, DateTimePicker dateTo, Button dateSearchButton, string dateColumn)
        {
            boundDateFrom = dateFrom;
            boundDateTo = dateTo;
            boundDateSearchButton = dateSearchButton;
            boundDateColumn = dateColumn;

            if (boundDateSearchButton != null)
            {
                boundDateSearchButton.Click -= BoundDateSearchButton_Click;
                boundDateSearchButton.Click += BoundDateSearchButton_Click;
            }
        }

        private void BoundDateSearchButton_Click(object sender, EventArgs e)
        {
            if (boundDateFrom == null || boundDateTo == null || string.IsNullOrWhiteSpace(boundDateColumn))
                return;

            DateTime from = boundDateFrom.Value.Date;
            DateTime to = boundDateTo.Value.Date;

            // store last-mode so paging will continue this filter
            lastMode = ActiveMode.DateRange;
            lastDateColumn = boundDateColumn;
            lastDateFrom = from;
            lastDateTo = to;

            SearchByDateRange(boundDateColumn, from, to, 1);
        }

        private void BoundSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                OnSearchTriggered();
            }
        }

        private void BoundSearchButton_Click(object sender, EventArgs e) => OnSearchTriggered();
        private void BoundRefreshButton_Click(object sender, EventArgs e) => Refresh();

        private void OnSearchTriggered()
        {
            string term = (boundSearchBox?.Text ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(term))
                LoadData(1);
            else
                Search(term, 1);
        }
        #endregion

        #region Index helper (best-effort)
        private void EnsureIndexesForSearch(string[] cols)
        {
            if (cols == null || cols.Length == 0) return;

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                // gather existing index names on base table
                var existing = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                using (var cmd = new MySqlCommand($"SHOW INDEX FROM `{baseTableName}`", conn))
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                        existing.Add(Convert.ToString(rdr["Key_name"]));
                }

                foreach (var col in cols)
                {
                    string idxName = $"idx_{baseTableName}_{col}";
                    if (existing.Contains(idxName)) continue;

                    // create index; wrap in try to continue on failure
                    try
                    {
                        using (var alt = new MySqlCommand($"ALTER TABLE `{baseTableName}` ADD INDEX `{idxName}` (`{col}`)", conn))
                        {
                            alt.ExecuteNonQuery();
                        }
                    }
                    catch
                    {
                        // ignore failures (e.g., privileges)
                    }
                }
            }
        }
        #endregion

        #region Load / Search / Paging
        /// <summary>
        /// Load page (server-side) from the VIEW.
        /// </summary>
        public void LoadData(int page = 1)
        {
            try
            {
                // If we are configured to only paginate by date-range, or if last mode was date-range,
                // delegate to SearchByDateRange so paging uses the date filter.
                if (OnlyDateRangePagination || lastMode == ActiveMode.DateRange)
                {
                    // prefer stored lastDateColumn/from/to; if not present, fallback to bound controls
                    string dateCol = lastDateColumn ?? boundDateColumn;
                    if (string.IsNullOrWhiteSpace(dateCol))
                        throw new InvalidOperationException("Date column not set for date-range pagination.");

                    DateTime from = (lastDateFrom != DateTime.MinValue) ? lastDateFrom : (boundDateFrom?.Value.Date ?? DateTime.Today);
                    DateTime to = (lastDateTo != DateTime.MinValue) ? lastDateTo : (boundDateTo?.Value.Date ?? DateTime.Today);

                    // update stored values
                    lastMode = ActiveMode.DateRange;
                    lastDateColumn = dateCol;
                    lastDateFrom = from;
                    lastDateTo = to;

                    // delegate to the date-range search which already handles paging/count
                    SearchByDateRange(dateCol, from, to, page);
                    return;
                }

                // === original LoadData implementation follows ===
                CurrentPage = Math.Max(1, page);

                // total count (from view)
                int total = 0;
                using (var conn = DBConfig.GetConnection())
                using (var cmdCount = new MySqlCommand($"SELECT COUNT(*) FROM `{viewName}`", conn))
                {
                    conn.Open();
                    total = Convert.ToInt32(cmdCount.ExecuteScalar());
                }

                TotalPages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
                int offset = (CurrentPage - 1) * PageSize;

                string sql = $"SELECT * FROM `{viewName}` LIMIT @limit OFFSET @offset";
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@limit", PageSize);
                    cmd.Parameters.AddWithValue("@offset", offset);

                    var dt = new DataTable();
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

        /// <summary>
        /// Text search (server-side) against the VIEW. page parameter for result paging.
        /// </summary>
        public void Search(string searchTerm, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || searchableColumns.Length == 0)
            {
                LoadData(page);
                return;
            }

            try
            {
                CurrentPage = Math.Max(1, page);
                List<MySqlParameter> parameters = new List<MySqlParameter>();
                string whereClause;

                if (useFullTextSearch)
                {
                    string cols = string.Join(",", searchableColumns.Select(c => $"`{c}`"));
                    whereClause = $"MATCH ({cols}) AGAINST (@ft IN BOOLEAN MODE)";
                    parameters.Add(new MySqlParameter("@ft", searchTerm + "*"));
                }
                else
                {
                    var likes = new List<string>();
                    for (int i = 0; i < searchableColumns.Length; i++)
                    {
                        string p = $"@p{i}";
                        likes.Add($"`{searchableColumns[i]}` LIKE {p}");
                        parameters.Add(new MySqlParameter(p, $"%{searchTerm}%"));
                    }
                    whereClause = "(" + string.Join(" OR ", likes) + ")";
                }

                // count matches (from view)
                int matched = 0;
                using (var conn = DBConfig.GetConnection())
                using (var cmdCount = new MySqlCommand($"SELECT COUNT(*) FROM `{viewName}` WHERE {whereClause}", conn))
                {
                    cmdCount.Parameters.AddRange(parameters.ToArray());
                    conn.Open();
                    matched = Convert.ToInt32(cmdCount.ExecuteScalar());
                }

                TotalPages = Math.Max(1, (int)Math.Ceiling(matched / (double)PageSize));
                int offset = (CurrentPage - 1) * PageSize;

                string sql = $"SELECT * FROM `{viewName}` WHERE {whereClause} LIMIT @limit OFFSET @offset";
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    cmd.Parameters.AddWithValue("@limit", PageSize);
                    cmd.Parameters.AddWithValue("@offset", offset);

                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }

                UpdatePageInfoLabel(searchTerm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search failed: " + ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Search rows between two dates (inclusive) on a given date column (VIEW).
        /// </summary>
        public void SearchByDateRange(string dateColumn, DateTime from, DateTime to, int page = 1)
        {
            try
            {
                CurrentPage = Math.Max(1, page);

                string countSql = $"SELECT COUNT(*) FROM `{viewName}` WHERE DATE(`{dateColumn}`) BETWEEN @from AND @to";
                int matched = 0;
                using (var conn = DBConfig.GetConnection())
                using (var cmdCount = new MySqlCommand(countSql, conn))
                {
                    cmdCount.Parameters.AddWithValue("@from", from.Date);
                    cmdCount.Parameters.AddWithValue("@to", to.Date);
                    conn.Open();
                    matched = Convert.ToInt32(cmdCount.ExecuteScalar());
                }

                TotalPages = Math.Max(1, (int)Math.Ceiling(matched / (double)PageSize));
                int offset = (CurrentPage - 1) * PageSize;

                string sql = $"SELECT * FROM `{viewName}` WHERE DATE(`{dateColumn}`) BETWEEN @from AND @to LIMIT @limit OFFSET @offset";
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@from", from.Date);
                    cmd.Parameters.AddWithValue("@to", to.Date);
                    cmd.Parameters.AddWithValue("@limit", PageSize);
                    cmd.Parameters.AddWithValue("@offset", offset);

                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }

                UpdatePageInfoLabel($"{from:yyyy-MM-dd} → {to:yyyy-MM-dd}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Date range search failed: " + ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Refresh()
        {
            // Respect date-range mode when active
            if (OnlyDateRangePagination || lastMode == ActiveMode.DateRange)
            {
                if (string.IsNullOrWhiteSpace(lastDateColumn))
                    throw new InvalidOperationException("Date column not set for date-range pagination.");

                SearchByDateRange(lastDateColumn, lastDateFrom, lastDateTo, CurrentPage);
                return;
            }

            // default behavior
            string cur = boundSearchBox?.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(cur))
                Search(cur, CurrentPage);
            else
                LoadData(CurrentPage);
        }

        public void NextPage()
        {
            if (CurrentPage >= TotalPages) return;

            int next = CurrentPage + 1;
            if (OnlyDateRangePagination || lastMode == ActiveMode.DateRange)
            {
                if (string.IsNullOrWhiteSpace(lastDateColumn))
                    throw new InvalidOperationException("Date column not set for date-range pagination.");
                SearchByDateRange(lastDateColumn, lastDateFrom, lastDateTo, next);
            }
            else
            {
                LoadData(next);
            }
        }

        public void PreviousPage()
        {
            if (CurrentPage <= 1) return;

            int prev = CurrentPage - 1;
            if (OnlyDateRangePagination || lastMode == ActiveMode.DateRange)
            {
                if (string.IsNullOrWhiteSpace(lastDateColumn))
                    throw new InvalidOperationException("Date column not set for date-range pagination.");
                SearchByDateRange(lastDateColumn, lastDateFrom, lastDateTo, prev);
            }
            else
            {
                LoadData(prev);
            }
        }

        #endregion

        #region Inline Edit / Delete

        private void Dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                var cell = dgv[e.ColumnIndex, e.RowIndex];
                string key = $"{e.RowIndex}:{e.ColumnIndex}";
                oldCellValues[key] = cell.Value;
            }
            catch { /* ignore */ }
        }

        private void Dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string key = $"{e.RowIndex}:{e.ColumnIndex}";
                object oldValue = oldCellValues.ContainsKey(key) ? oldCellValues[key] : null;
                object newValue = dgv[e.ColumnIndex, e.RowIndex].Value;

                bool changed = ValuesChanged(oldValue, newValue);
                if (!changed)
                {
                    if (oldCellValues.ContainsKey(key)) oldCellValues.Remove(key);
                    return;
                }

                string columnName = GetColumnDataPropertyName(e.ColumnIndex);
                // Use the header text if available, otherwise fall back to the column name
                string displayName = dgv.Columns[e.ColumnIndex].HeaderText;
                if (string.IsNullOrWhiteSpace(displayName))
                    displayName = columnName;
                var result = MessageBox.Show($"Save changes to '{displayName}'?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    dgv[e.ColumnIndex, e.RowIndex].Value = oldValue;
                    if (oldCellValues.ContainsKey(key)) oldCellValues.Remove(key);
                    return;
                }

                UpdateCellValueSafe(e.RowIndex, e.ColumnIndex, columnName);

                if (oldCellValues.ContainsKey(key)) oldCellValues.Remove(key);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValuesChanged(object oldVal, object newVal)
        {
            if (oldVal == null && newVal == null) return false;
            if (oldVal == null || newVal == null) return true;
            return !object.Equals(oldVal, newVal);
        }

        private string GetColumnDataPropertyName(int colIndex)
        {
            var col = dgv.Columns[colIndex];
            return string.IsNullOrWhiteSpace(col.DataPropertyName) ? col.Name : col.DataPropertyName;
        }

        private void UpdateCellValueSafe(int rowIndex, int colIndex, string columnName)
        {
            // Prevent updates to columns not present in the base table
            if (!editableColumns.Contains(columnName))
                throw new Exception($"Column '{columnName}' is not editable in the target table '{baseTableName}'.");

            var col = dgv.Columns[colIndex];
            object value = dgv[colIndex, rowIndex].Value ?? DBNull.Value;

            // Special handling
            if (col is DataGridViewCheckBoxColumn)
            {
                value = Convert.ToBoolean(value);
            }
            else if (col is DataGridViewComboBoxColumn)
            {
                // assume ValueMember is used and cell.Value is actual value
                value = dgv[colIndex, rowIndex].Value ?? DBNull.Value;
            }
            else
            {
                var vt = col.ValueType;
                if (value != DBNull.Value && vt != null)
                {
                    try
                    {
                        if (vt == typeof(int)) value = Convert.ToInt32(value);
                        else if (vt == typeof(long)) value = Convert.ToInt64(value);
                        else if (vt == typeof(decimal)) value = Convert.ToDecimal(value);
                        else if (vt == typeof(double)) value = Convert.ToDouble(value);
                        else if (vt == typeof(bool)) value = Convert.ToBoolean(value);
                        else if (vt == typeof(DateTime)) value = Convert.ToDateTime(value);
                        else value = value.ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Invalid value for column '{columnName}': {ex.Message}");
                    }
                }
            }

            // determine primary key value
            object id = null;
            // try direct cell lookup by primaryKeyColumn name (designer column)
            if (dgv.Columns.Contains(primaryKeyColumn))
            {
                try { id = dgv[primaryKeyColumn, rowIndex].Value; } catch { id = null; }
            }

            // fallback to DataBoundItem
            if (id == null)
            {
                var drv = dgv.Rows[rowIndex].DataBoundItem as DataRowView;
                if (drv != null && drv.Row.Table.Columns.Contains(primaryKeyColumn))
                    id = drv.Row[primaryKeyColumn];
            }

            if (id == null || id == DBNull.Value) throw new Exception("Unable to determine primary key value for the selected row.");

            // UPDATE the base table (not the view)
            string sql = $"UPDATE `{baseTableName}` SET `{columnName}`=@value WHERE `{primaryKeyColumn}`=@id";
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
            e.Cancel = true;
            var idCell = e.Row.Cells[primaryKeyColumn];
            if (idCell == null)
            {
                MessageBox.Show("Primary key column not present. Cannot delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            object id = idCell.Value;
            if (MessageBox.Show("Delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    DeleteRow(id);
                    LoadData(CurrentPage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete failed: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteRow(object id)
        {
            // Delete from the base table (not the view)
            string sql = $"DELETE FROM `{baseTableName}` WHERE `{primaryKeyColumn}`=@id";
            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region DateTimePicker overlay handling
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                dgvDatePicker.Visible = false;
                return;
            }

            try
            {
                var col = dgv.Columns[e.ColumnIndex];
                bool isDateType = col.ValueType == typeof(DateTime) || col.Name.ToLower().Contains("date");

                if (isDateType)
                {
                    Rectangle rect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    dgvDatePicker.Size = new Size(rect.Width, rect.Height);
                    dgvDatePicker.Location = new Point(rect.X, rect.Y);
                    dgvDatePicker.Visible = true;

                    var cellVal = dgv[e.ColumnIndex, e.RowIndex].Value;
                    if (cellVal == DBNull.Value || cellVal == null)
                        dgvDatePicker.Value = DateTime.Today;
                    else
                        dgvDatePicker.Value = Convert.ToDateTime(cellVal);

                    dgvDatePicker.Tag = Tuple.Create(e.RowIndex, e.ColumnIndex);
                }
                else
                {
                    dgvDatePicker.Visible = false;
                }
            }
            catch
            {
                dgvDatePicker.Visible = false;
            }
        }

        private void DgvDatePicker_CloseUp(object sender, EventArgs e)
        {
            dgvDatePicker.Visible = false;
        }

        private void DgvDatePicker_TextChanged(object sender, EventArgs e)
        {
            if (dgvDatePicker.Tag is Tuple<int, int> t)
            {
                int row = t.Item1;
                int col = t.Item2;
                dgv[col, row].Value = dgvDatePicker.Value.Date;
            }
        }
        #endregion

        #region Page info
        public void SetPageInfoLabel(Label label)
        {
            pageInfoLabel = label;
        }

        private void UpdatePageInfoLabel(string searchTerm = null)
        {
            if (pageInfoLabel == null) return;

            if (!string.IsNullOrWhiteSpace(searchTerm))
                pageInfoLabel.Text = $"Searched:\n{searchTerm.ToUpper()}";
            else
                pageInfoLabel.Text = $"Rows: {dgv.Rows.Count:N0}\nPage {CurrentPage} of {TotalPages}";
        }
        #endregion

        /// <summary>
        /// Executes a custom SQL command that targets a specific row by ID.
        /// Automatically binds @id to the given primary key value.
        /// (Note: SQL executes as provided — use baseTableName in the SQL if you intend to affect base table.)
        /// </summary>
        public void ExecuteCustomQuery(string sql, object id)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL query cannot be empty.", nameof(sql));

            if (id == null || id == DBNull.Value)
                throw new ArgumentException("ID cannot be null.", nameof(id));

            try
            {
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Query executed successfully.\nRows affected: {affected}",
                        "Custom Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Optional: refresh the current page after the custom query
                Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Custom query failed: " + ex.Message,
                    "Query Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads only the rows from today based on a specific date column in the VIEW.
        /// </summary>
        public void LoadToday(string dateColumn)
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(
                    $"SELECT * FROM `{viewName}` WHERE DATE(`{dateColumn}`) = CURDATE()", conn))
                using (var adapter = new MySqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }

                // update label to show that we are displaying today's data
                UpdatePageInfoLabel($"Today's Rows ({DateTime.Now:MMMM dd, yyyy})");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load today's rows: " + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitContextMenu()
        {
            dgvContextMenu = new ContextMenuStrip();
            var deleteItem = new ToolStripMenuItem("Delete Row");
            deleteItem.ForeColor = Color.Red;
            deleteItem.Click += DgvContextMenu_DeleteClick;
            dgvContextMenu.Items.Add(deleteItem);

            dgv.CellMouseDown -= Dgv_CellMouseDown;
            dgv.CellMouseDown += Dgv_CellMouseDown;
        }
        private void Dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Check if the row is fully selected
                bool rowFullySelected = dgv.Rows[e.RowIndex].Selected &&
                                        dgv.SelectedRows.Count == 1;

                if (!rowFullySelected)
                {
                    // Prevent context menu when row is not fully selected
                    return;
                }

                // Allow Delete menu only if full row is selected
                dgvContextMenu.Show(Cursor.Position);
                dgvContextMenu.Tag = e.RowIndex;
            }
        }
        /// <summary>
        /// Loads rows from any table filtered by any column.
        /// Fills the target DataGridView with the results.
        /// </summary>
        /// <param name="dgv">DataGridView to populate</param>
        /// <param name="tableName">Name of the database table</param>
        /// <param name="columnName">Name of the column used for WHERE filtering</param>
        /// <param name="value">Value to filter (ex: consultation_id = 1)</param>
        /// <summary>
        /// Instance version: load rows filtered by a column and enable CRUD operations
        /// (updates will be applied to the helper's configured baseTableName).
        /// </summary>
        public void LoadRowsByColumn(string tableOrViewName, string columnName, object value)
        {
            if (string.IsNullOrWhiteSpace(tableOrViewName)) throw new ArgumentException("tableOrViewName required", nameof(tableOrViewName));
            if (string.IsNullOrWhiteSpace(columnName)) throw new ArgumentException("columnName required", nameof(columnName));
            if (value == null) throw new ArgumentNullException(nameof(value));

            // Basic safety for names to avoid injection via table/column identifiers
            var safeNamePattern = @"^[A-Za-z0-9_]+$";
            if (!Regex.IsMatch(tableOrViewName, safeNamePattern))
                throw new ArgumentException("Invalid table/view name format.", nameof(tableOrViewName));
            if (!Regex.IsMatch(columnName, safeNamePattern))
                throw new ArgumentException("Invalid column name format.", nameof(columnName));

            try
            {
                string sql = $"SELECT * FROM `{tableOrViewName}` WHERE `{columnName}` = @value";

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@value", value);

                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // bind
                        dgv.AutoGenerateColumns = true;
                        dgv.DataSource = dt;

                        // visual / behavior settings for CRUD
                        dgv.AllowUserToAddRows = false;            // adding through form instead of DGV
                        dgv.AllowUserToDeleteRows = true;          // delete handled by UserDeletingRow or context menu
                        dgv.ReadOnly = false;                      // allow editing, but we'll mark specific columns readonly below
                        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dgv.MultiSelect = false;
                        dgv.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

                        // Prevent SortMode conflict when SelectionMode = FullColumnSelect/FullRowSelect
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            col.SortMode = DataGridViewColumnSortMode.NotSortable;

                            // Determine data property name (the actual column name in DataTable)
                            string dataName = string.IsNullOrWhiteSpace(col.DataPropertyName) ? col.Name : col.DataPropertyName;

                            // Primary key should be read-only (and optionally hidden)
                            if (string.Equals(dataName, primaryKeyColumn, StringComparison.OrdinalIgnoreCase))
                            {
                                col.ReadOnly = true;
                                // If you prefer to hide the PK column, uncomment next line:
                                // col.Visible = false;
                                continue;
                            }

                            // Only columns that exist in the base table (editableColumns) are editable
                            if (editableColumns != null && editableColumns.Count > 0)
                            {
                                col.ReadOnly = !editableColumns.Contains(dataName);
                            }
                            else
                            {
                                // If we couldn't load editableColumns (permissions error), be conservative and make columns read-only
                                col.ReadOnly = true;
                            }
                        }

                        // If primary key is not present in the result, warn (updates/deletes will fail)
                        if (!dt.Columns.Contains(primaryKeyColumn))
                        {
                            // keep UI responsive but inform developer
                            // you might want to hide edit/delete features in UI when PK missing
                            Console.WriteLine($"Warning: result does not contain primary key column '{primaryKeyColumn}'. Updates/deletes will not work.");
                        }

                        UpdatePageInfoLabel();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load data: " + ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DgvContextMenu_DeleteClick(object sender, EventArgs e)
        {
            if (dgvContextMenu.Tag is int rowIndex)
            {
                object id = null;

                // Try primary key from the selected row
                if (dgv.Columns.Contains(primaryKeyColumn))
                {
                    id = dgv[primaryKeyColumn, rowIndex].Value;
                }
                else
                {
                    var drv = dgv.Rows[rowIndex].DataBoundItem as DataRowView;
                    if (drv != null && drv.Row.Table.Columns.Contains(primaryKeyColumn))
                        id = drv.Row[primaryKeyColumn];
                }

                if (id == null || id == DBNull.Value)
                {
                    MessageBox.Show("Unable to determine primary key for deletion.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("Delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        DeleteRow(id);
                        LoadData(CurrentPage);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Delete failed: " + ex.Message, "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        #region Helper: editable columns discovery
        /// <summary>
        /// Loads column names from the target base table (not the view)
        /// so updates do not attempt to modify computed/joined view columns.
        /// </summary>
        private void LoadEditableColumns()
        {
            try
            {
                editableColumns.Clear();
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand($"SHOW COLUMNS FROM `{baseTableName}`;", conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            editableColumns.Add(reader.GetString("Field"));
                        }
                    }
                }
            }
            catch
            {
                // If we fail (permissions, table missing), leave editableColumns empty so updates will fail with clear message.
                editableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }
        }
        #endregion
    }
}
