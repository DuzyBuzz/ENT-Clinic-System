using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// Reusable Dashboard helper library.
    /// Provide table + columns + options, and this helper returns DataTable or binds to Chart/DataGridView.
    /// It is intentionally conservative: no UI assumptions, works with Chart & DataGridView already used in your project.
    /// </summary>
    public static class DashboardHelper
    {
        #region Public DTOs / Options

        /// <summary>
        /// Options used when binding a DataTable to a Chart.
        /// - If SeriesTypes is null/empty, default mapping will be used:
        ///   first Y column -> Column, remaining -> Line.
        /// - XColumn must exist in the DataTable.
        /// </summary>
        /// 
        /// <summary>
        /// HOW TO USE THIS -----------------------------------------


        //private void LoadChart()
        //{
        //    // Monthly: count non-empty exam columns + totals
        //    var dt = DashboardHelper.GetMonthlyCounts(
        //        "consultation",            // table
        //        "consultation_date",       // date column
        //        new[] { "ear_exam", "nose_exam", "throat_exam", "others_exam" }, // columns to count non-empty
        //        "consultation_id"          // primary key column (for totals)
        //    );

        //    // Bind grid
        //    DashboardHelper.BindGrid(dgvEnt, dt);

        //    // Chart: first y is TotalConsults column -> column, others -> lines
        //    var opt = new DashboardHelper.ChartOptions
        //    {
        //        Title = "Monthly ENT",
        //        XColumn = "MonthName",
        //        YColumns = new List<string> { "TotalConsults", "EarCount", "NoseCount", "ThroatCount", "OthersCount" }
        //    };
        //    DashboardHelper.BindChart(chartEnt, dt, opt);

        //}
        /// </summary>
        public class ChartOptions
        {
            public string Title { get; set; } = "";
            public string XColumn { get; set; } = null;
            public List<string> YColumns { get; set; } = new List<string>();
            public Dictionary<string, SeriesChartType> SeriesTypes { get; set; } = new Dictionary<string, SeriesChartType>();
            public bool ShowLegend { get; set; } = true;
            public bool ShowValuesOnPoints { get; set; } = false;
            public bool RotateXLabels45 { get; set; } = true;
        }

        #endregion

        #region Query Helpers (Monthly / Daily / Top)

        /// <summary>
        /// Returns a DataTable with months 1..12 and counts:
        /// - For each column in countColumns: SUM(CASE WHEN col IS NOT NULL AND TRIM(col) <> '' THEN 1 ELSE 0 END) AS {ColumnName}Count
        /// - TotalConsults uses COUNT(primaryKeyColumn) so months with no rows return 0.
        /// Columns in result: MonthNumber, MonthName, {Col}Count..., TotalConsults
        /// </summary>
        public static DataTable GetMonthlyCounts(string tableName, string dateColumn, string[] countColumns, string primaryKeyColumn = "id")
        {
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("tableName required");
            if (string.IsNullOrWhiteSpace(dateColumn)) throw new ArgumentException("dateColumn required");
            if (countColumns == null) countColumns = new string[0];

            // Build SUM(CASE...) parts
            var sums = new StringBuilder();
            foreach (var col in countColumns)
            {
                var safeCol = col.Trim();
                if (sums.Length > 0) sums.AppendLine(",");
                sums.AppendFormat("    SUM(CASE WHEN {0} IS NOT NULL AND TRIM({0}) <> '' THEN 1 ELSE 0 END) AS `{1}Count`", QuoteIdent(safeCol), safeCol);
            }

            // If no count columns, still produce TotalConsults
            var sql = $@"
SELECT 
    m.MonthNumber,
    m.MonthName,
    {(sums.Length > 0 ? sums.ToString() + "," : "")}
    COUNT(c.{QuoteIdent(primaryKeyColumn)}) AS TotalConsults
FROM (
    SELECT 1 AS MonthNumber, 'January' AS MonthName UNION ALL
    SELECT 2, 'February' UNION ALL
    SELECT 3, 'March' UNION ALL
    SELECT 4, 'April' UNION ALL
    SELECT 5, 'May' UNION ALL
    SELECT 6, 'June' UNION ALL
    SELECT 7, 'July' UNION ALL
    SELECT 8, 'August' UNION ALL
    SELECT 9, 'September' UNION ALL
    SELECT 10, 'October' UNION ALL
    SELECT 11, 'November' UNION ALL
    SELECT 12, 'December'
) m
LEFT JOIN {QuoteIdent(tableName)} c
    ON MONTH(c.{QuoteIdent(dateColumn)}) = m.MonthNumber
   AND YEAR(c.{QuoteIdent(dateColumn)}) = YEAR(CURDATE())
GROUP BY m.MonthNumber, m.MonthName
ORDER BY m.MonthNumber;
";
            return ExecuteQuery(sql);
        }

        /// <summary>
        /// Returns a DataTable aggregated per day for last (days+1) days (0..days), column list same as monthly counts.
        /// Columns: DayDate (DATE), DayLabel (e.g. 'Nov 15'), {Col}Count..., TotalConsults
        /// </summary>
        public static DataTable GetDailyCounts(string tableName, string dateColumn, string[] countColumns, string primaryKeyColumn = "id", int days = 29)
        {
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("tableName required");
            if (string.IsNullOrWhiteSpace(dateColumn)) throw new ArgumentException("dateColumn required");
            if (countColumns == null) countColumns = new string[0];

            var sums = new StringBuilder();
            foreach (var col in countColumns)
            {
                var safeCol = col.Trim();
                if (sums.Length > 0) sums.AppendLine(",");
                sums.AppendFormat("    SUM(CASE WHEN c.{0} IS NOT NULL AND TRIM(c.{0}) <> '' THEN 1 ELSE 0 END) AS `{1}Count`", QuoteIdent(safeCol), safeCol);
            }

            var sql = $@"
SELECT 
    x.DayDate,
    DATE_FORMAT(x.DayDate, '%b %d') AS DayLabel,
    COUNT(c.{QuoteIdent(primaryKeyColumn)}) AS TotalConsults,
    {(sums.Length > 0 ? sums.ToString() : "")}
FROM (
    SELECT DATE({QuoteIdent(dateColumn)}) AS DayDate, {QuoteIdent(primaryKeyColumn)}
    FROM {QuoteIdent(tableName)}
    WHERE {QuoteIdent(dateColumn)} >= DATE_SUB(CURDATE(), INTERVAL {days} DAY)
) x
LEFT JOIN {QuoteIdent(tableName)} c
    ON c.{QuoteIdent(primaryKeyColumn)} = x.{QuoteIdent(primaryKeyColumn)}
GROUP BY x.DayDate
ORDER BY x.DayDate;
";
            return ExecuteQuery(sql);
        }

        /// <summary>
        /// Simple top-values query (e.g. top diagnoses). Returns columns: Value, Count
        /// If dateColumn provided and yearOnly true -> filter by current year.
        /// </summary>
        public static DataTable GetTopValues(string tableName, string columnName, int top = 20, string dateColumn = null, bool yearOnly = true)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentException("tableName and columnName required");

            var where = "";
            if (!string.IsNullOrEmpty(dateColumn) && yearOnly)
                where = $"WHERE YEAR({QuoteIdent(dateColumn)}) = YEAR(CURDATE())";

            var sql = $@"
SELECT TRIM({QuoteIdent(columnName)}) AS `Value`, COUNT(*) AS `Count`
FROM {QuoteIdent(tableName)}
{where}
AND {QuoteIdent(columnName)} IS NOT NULL AND TRIM({QuoteIdent(columnName)}) <> ''
GROUP BY TRIM({QuoteIdent(columnName)})
ORDER BY `Count` DESC
LIMIT {top};
";
            // If we appended WHERE then the AND above is wrong, fix:
            if (!string.IsNullOrEmpty(dateColumn) && yearOnly)
            {
                sql = $@"
SELECT TRIM({QuoteIdent(columnName)}) AS `Value`, COUNT(*) AS `Count`
FROM {QuoteIdent(tableName)}
WHERE YEAR({QuoteIdent(dateColumn)}) = YEAR(CURDATE())
  AND {QuoteIdent(columnName)} IS NOT NULL AND TRIM({QuoteIdent(columnName)}) <> ''
GROUP BY TRIM({QuoteIdent(columnName)})
ORDER BY `Count` DESC
LIMIT {top};
";
            }
            return ExecuteQuery(sql);
        }

        #endregion

        #region Binding Helpers (Grid / Chart / Pie)

        /// <summary>
        /// Binds a DataTable to a DataGridView and applies simple formatting.
        /// </summary>
        public static void BindGrid(DataGridView dgv, DataTable dt)
        {
            if (dgv == null) throw new ArgumentNullException(nameof(dgv));
            dgv.DataSource = dt;
            FormatGrid(dgv);
        }

        /// <summary>
        /// Binds a DataTable to a Chart using ChartOptions.
        /// - If options.SeriesTypes not provided, the helper will use defaults:
        ///   first Y column => Column, remaining => Line.
        /// - For Pie charts, call RenderPieFromTable instead.
        /// </summary>
        public static void BindChart(Chart chart, DataTable dt, ChartOptions options)
        {
            if (chart == null) throw new ArgumentNullException(nameof(chart));
            if (dt == null) throw new ArgumentNullException(nameof(dt));
            if (options == null) options = new ChartOptions();

            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Titles.Clear();
            chart.Legends.Clear();
            chart.BackColor = Color.White;

            // Title
            if (!string.IsNullOrEmpty(options.Title))
                chart.Titles.Add(new Title(options.Title, Docking.Top, new Font("Segoe UI", 11, FontStyle.Bold), Color.FromArgb(40, 40, 40)));

            var ca = new ChartArea("CA") { BackColor = Color.White };
            if (options.RotateXLabels45) ca.AxisX.LabelStyle.Angle = -45;
            ca.AxisX.Interval = 1;
            ca.AxisY.MajorGrid.LineColor = Color.LightGray;
            ca.AxisX.MajorGrid.LineColor = Color.LightGray;
            chart.ChartAreas.Add(ca);

            if (options.ShowLegend)
                chart.Legends.Add(new Legend() { Docking = Docking.Top, LegendStyle = LegendStyle.Row });

            // Validate columns
            if (string.IsNullOrEmpty(options.XColumn))
            {
                // try auto-detect X: prefer MonthName, DayLabel, or first string column
                if (dt.Columns.Contains("MonthName")) options.XColumn = "MonthName";
                else if (dt.Columns.Contains("DayLabel")) options.XColumn = "DayLabel";
                else options.XColumn = dt.Columns.Cast<DataColumn>().FirstOrDefault(c => c.DataType == typeof(string))?.ColumnName;
            }

            // If not found still, pick first column
            if (string.IsNullOrEmpty(options.XColumn) && dt.Columns.Count > 0)
                options.XColumn = dt.Columns[0].ColumnName;

            // If YColumns not provided, pick all numeric columns except X
            if (options.YColumns == null || options.YColumns.Count == 0)
            {
                options.YColumns = new List<string>();
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName == options.XColumn) continue;
                    if (IsNumericType(c.DataType)) options.YColumns.Add(c.ColumnName);
                }
            }

            // Default series types
            var seriesTypes = new Dictionary<string, SeriesChartType>();
            if (options.SeriesTypes != null && options.SeriesTypes.Count > 0)
                seriesTypes = new Dictionary<string, SeriesChartType>(options.SeriesTypes, StringComparer.OrdinalIgnoreCase);

            // If still empty, set first to Column, rest to Line
            for (int i = 0; i < options.YColumns.Count; i++)
            {
                var y = options.YColumns[i];
                if (!seriesTypes.ContainsKey(y))
                {
                    seriesTypes[y] = (i == 0) ? SeriesChartType.Column : SeriesChartType.Line;
                }
            }

            // Create series
            foreach (var y in options.YColumns)
            {
                if (!dt.Columns.Contains(y)) continue; // skip missing
                var sType = seriesTypes.ContainsKey(y) ? seriesTypes[y] : SeriesChartType.Line;

                var s = new Series(y)
                {
                    ChartArea = "CA",
                    XValueMember = options.XColumn,
                    YValueMembers = y,
                    ChartType = sType,
                    BorderWidth = 2,
                    IsValueShownAsLabel = options.ShowValuesOnPoints
                };

                // Use markers for lines
                if (sType == SeriesChartType.Line)
                {
                    s.MarkerStyle = MarkerStyle.Circle;
                    s.MarkerSize = 5;
                }

                chart.Series.Add(s);
            }

            // Bind and redraw
            chart.DataSource = dt;
            chart.DataBind();
            chart.Invalidate();
        }

        /// <summary>
        /// Render a pie chart using the provided DataTable where labelColumn and valueColumn exist in dt.
        /// </summary>
        public static void RenderPieFromTable(Chart chart, DataTable dt, string labelColumn, string valueColumn, string title = "")
        {
            if (chart == null) throw new ArgumentNullException(nameof(chart));
            if (dt == null) throw new ArgumentNullException(nameof(dt));
            if (string.IsNullOrEmpty(labelColumn) || string.IsNullOrEmpty(valueColumn)) throw new ArgumentException("labelColumn and valueColumn required");

            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();
            chart.BackColor = Color.White;

            if (!string.IsNullOrEmpty(title))
                chart.Titles.Add(new Title(title, Docking.Top, new Font("Segoe UI", 11, FontStyle.Bold), Color.FromArgb(40, 40, 40)));

            var ca = new ChartArea("PieArea");
            ca.Area3DStyle.Enable3D = true;
            ca.Area3DStyle.Inclination = 35;
            ca.Area3DStyle.Rotation = 20;
            ca.Area3DStyle.PointDepth = 30;
            chart.ChartAreas.Add(ca);

            var legend = new Legend("L") { Docking = Docking.Bottom, LegendStyle = LegendStyle.Table };
            chart.Legends.Add(legend);

            var s = new Series("S")
            {
                ChartType = SeriesChartType.Pie,
                ChartArea = "PieArea",
                IsValueShownAsLabel = true
            };
            s["PieLabelStyle"] = "Outside";
            s.Label = "#PERCENT{P1}\n#VAL{N0}";
            s.ToolTip = "#AXISLABEL: #VAL{N0}";
            chart.Series.Add(s);

            foreach (DataRow r in dt.Rows)
            {
                try
                {
                    var lbl = r[labelColumn]?.ToString() ?? "Unknown";
                    long val = 0;
                    try { val = Convert.ToInt64(r[valueColumn]); } catch { val = 0; }
                    if (val <= 0) continue;

                    var pointIndex = s.Points.AddY(val);
                    var pt = s.Points[pointIndex];
                    pt.AxisLabel = lbl;
                    pt.LegendText = $"{lbl} ({val:N0})";
                }
                catch
                {
                    // ignore problematic row
                }
            }

            if (s.Points.Count == 0)
            {
                s.Points.AddY(1);
                var pt = s.Points[0];
                pt.AxisLabel = "No data";
                pt.LegendText = "No data";
            }

            chart.Invalidate();
        }

        #endregion

        #region Utility Helpers (Query execution, formatting)

        /// <summary>
        /// Executes the SQL using DBConfig.GetConnection() and returns a DataTable.
        /// </summary>
        public static DataTable ExecuteQuery(string sql, Dictionary<string, object> parameters = null)
        {
            var dt = new DataTable();
            try
            {
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var p in parameters)
                            cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                    }

                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DashboardHelper DB error:\n" + ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Formats a DataGridView: aligns numeric columns to the right and sets N0 for counts.
        /// </summary>
        public static void FormatGrid(DataGridView dgv)
        {
            if (dgv == null || dgv.DataSource == null) return;
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                var name = col.Name.ToLower();
                if (name.Contains("count") || name.Contains("total") || name.Contains("consult") || name.Contains("amount") || name.Contains("age"))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.DefaultCellStyle.Format = "N0";
                }
                else
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                if (col.Width > 400) col.Width = 400;
            }
        }

        #endregion

        #region Small helpers

        private static bool IsNumericType(Type t)
        {
            if (t == null) return false;
            return t == typeof(int) || t == typeof(long) || t == typeof(short) ||
                   t == typeof(decimal) || t == typeof(double) || t == typeof(float);
        }

        /// <summary>
        /// Very small protecting wrapper to quote identifiers. 
        /// This is NOT a full SQL identifier sanitizer — if you accept arbitrary user input for table/column
        /// names you should validate them against a whitelist. This helper simply wraps with backticks.
        /// </summary>
        private static string QuoteIdent(string ident)
        {
            if (string.IsNullOrWhiteSpace(ident)) return ident;
            // remove accidental backticks inside
            var cleaned = ident.Replace("`", "");
            return $"`{cleaned}`";
        }

        #endregion
    }
}
