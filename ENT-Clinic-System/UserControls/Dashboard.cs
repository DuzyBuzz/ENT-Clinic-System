using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ENT_Clinic_System.UserControls
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();

            // Initialize top filters (defensive: check controls exist)
            try
            {
                cboZoneFilter.Items.Clear();
                cboZoneFilter.Items.Add("All");
                cboZoneFilter.SelectedIndex = 0;

                cboServiceFilter.Items.Clear();
                cboServiceFilter.Items.Add("All");
                cboServiceFilter.SelectedIndex = 0;
            }
            catch { /* ignore if designer not yet wired */ }

            // initial load
            LoadCurrentTab();
        }

        #region Tab switching / refresh

        private void BtnRefresh_Click(object sender, EventArgs e) => LoadCurrentTab();
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e) => LoadCurrentTab();

        private void LoadCurrentTab()
        {
            if (tabControl?.SelectedTab == null) return;

            switch (tabControl.SelectedTab.Name)
            {
                case "tabMonthly":
                    LoadMonthlyEntSummary();
                    break;
                case "tabDaily":
                    LoadDailySummary();
                    break;
                case "tabQueueDaily":
                    LoadQueueDaily();
                    break;
                case "tabQueueMonthly":
                    LoadQueueMonthly();
                    break;
                case "tabPatientStats":
                    LoadPatientStats();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ENT Monthly / Daily

        private void LoadMonthlyEntSummary()
        {
            const string sql = @"
SELECT 
    m.MonthNumber,
    m.MonthName,
    SUM(CASE WHEN c.ear_exam IS NOT NULL AND TRIM(c.ear_exam) <> '' THEN 1 ELSE 0 END) AS EarCount,
    SUM(CASE WHEN c.nose_exam IS NOT NULL AND TRIM(c.nose_exam) <> '' THEN 1 ELSE 0 END) AS NoseCount,
    SUM(CASE WHEN c.throat_exam IS NOT NULL AND TRIM(c.throat_exam) <> '' THEN 1 ELSE 0 END) AS ThroatCount,
    SUM(CASE WHEN c.others_exam IS NOT NULL AND TRIM(c.others_exam) <> '' THEN 1 ELSE 0 END) AS OthersCount,
    COUNT(c.consultation_id) AS TotalConsults
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
LEFT JOIN consultation c
    ON MONTH(c.consultation_date) = m.MonthNumber
   AND YEAR(c.consultation_date) = YEAR(CURDATE())
GROUP BY m.MonthNumber, m.MonthName
ORDER BY m.MonthNumber;
";
            var dt = QueryToTable(sql);

            var dtClean = new DataTable();
            dtClean.Columns.Add("MonthNumber", typeof(int));
            dtClean.Columns.Add("MonthName", typeof(string));
            dtClean.Columns.Add("EarCount", typeof(int));
            dtClean.Columns.Add("NoseCount", typeof(int));
            dtClean.Columns.Add("ThroatCount", typeof(int));
            dtClean.Columns.Add("OthersCount", typeof(int));
            dtClean.Columns.Add("TotalConsults", typeof(int));

            if (dt != null)
            {
                foreach (DataRow r in dt.Rows)
                {
                    dtClean.Rows.Add(
                        r["MonthNumber"] == DBNull.Value ? 0 : Convert.ToInt32(r["MonthNumber"]),
                        r["MonthName"]?.ToString() ?? string.Empty,
                        r["EarCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["EarCount"]),
                        r["NoseCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["NoseCount"]),
                        r["ThroatCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["ThroatCount"]),
                        r["OthersCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["OthersCount"]),
                        r["TotalConsults"] == DBNull.Value ? 0 : Convert.ToInt32(r["TotalConsults"])
                    );
                }
            }

            dgvEnt.DataSource = dtClean;
            FormatGrid(dgvEnt);
            RenderMonthlyLineChart(chartEnt, dtClean);
        }

        private void RenderMonthlyLineChart(Chart chart, DataTable dt)
        {
            try
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.Legends.Clear();
                chart.Titles.Clear();

                var ca = new ChartArea("MainArea");
                ca.BackColor = Color.White;
                ca.AxisX.Title = "Month";
                ca.AxisX.LabelStyle.Angle = -45;
                ca.AxisY.Title = "Count";
                ca.AxisY.Minimum = 0;
                ca.AxisX.Interval = 1;
                chart.ChartAreas.Add(ca);

                chart.Titles.Add(new Title("Monthly ENT Summary (This Year)", Docking.Top,
                    new Font("Segoe UI", 12, FontStyle.Bold), Color.FromArgb(40, 40, 40)));

                chart.Legends.Add(new Legend("Legend") { Docking = Docking.Top, LegendStyle = LegendStyle.Row });

                var totalSeries = new Series("TotalConsults")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "MonthName",
                    YValueMembers = "TotalConsults",
                    IsValueShownAsLabel = true,
                    ChartArea = "MainArea"
                };
                chart.Series.Add(totalSeries);

                chart.Series.Add(CreateLineSeries("Ear", "MonthName", "EarCount", "MainArea"));
                chart.Series.Add(CreateLineSeries("Nose", "MonthName", "NoseCount", "MainArea"));
                chart.Series.Add(CreateLineSeries("Throat", "MonthName", "ThroatCount", "MainArea"));
                chart.Series.Add(CreateLineSeries("Others", "MonthName", "OthersCount", "MainArea"));

                chart.DataSource = dt;
                chart.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to render monthly chart:\n" + ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDailySummary()
        {
            const string sql = @"
SELECT 
    x.DayDate,
    DATE_FORMAT(x.DayDate, '%b %d') AS DayLabel,
    COUNT(c.consultation_id) AS TotalConsults,
    SUM(CASE WHEN c.ear_exam IS NOT NULL AND TRIM(c.ear_exam) <> '' THEN 1 ELSE 0 END) AS EarCount,
    SUM(CASE WHEN c.nose_exam IS NOT NULL AND TRIM(c.nose_exam) <> '' THEN 1 ELSE 0 END) AS NoseCount,
    SUM(CASE WHEN c.throat_exam IS NOT NULL AND TRIM(c.throat_exam) <> '' THEN 1 ELSE 0 END) AS ThroatCount,
    SUM(CASE WHEN c.others_exam IS NOT NULL AND TRIM(c.others_exam) <> '' THEN 1 ELSE 0 END) AS OthersCount
FROM (
    SELECT DATE(consultation_date) AS DayDate, consultation_id
    FROM consultation
    WHERE consultation_date >= DATE_SUB(CURDATE(), INTERVAL 29 DAY)
) x
LEFT JOIN consultation c 
    ON c.consultation_id = x.consultation_id
GROUP BY x.DayDate
ORDER BY x.DayDate;
";
            var raw = QueryToTable(sql);

            var dt = new DataTable();
            dt.Columns.Add("DayDate", typeof(DateTime));
            dt.Columns.Add("DayLabel", typeof(string));
            dt.Columns.Add("TotalConsults", typeof(int));
            dt.Columns.Add("EarCount", typeof(int));
            dt.Columns.Add("NoseCount", typeof(int));
            dt.Columns.Add("ThroatCount", typeof(int));
            dt.Columns.Add("OthersCount", typeof(int));

            if (raw != null)
            {
                foreach (DataRow r in raw.Rows)
                {
                    dt.Rows.Add(
                        r["DayDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(r["DayDate"]),
                        r["DayLabel"]?.ToString() ?? "",
                        r["TotalConsults"] == DBNull.Value ? 0 : Convert.ToInt32(r["TotalConsults"]),
                        r["EarCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["EarCount"]),
                        r["NoseCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["NoseCount"]),
                        r["ThroatCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["ThroatCount"]),
                        r["OthersCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["OthersCount"])
                    );
                }
            }

            dgvDaily.DataSource = dt;
            FormatGrid(dgvDaily);
            RenderDailyLineChart(chartDaily, dt);
        }

        private void RenderDailyLineChart(Chart chart, DataTable dt)
        {
            try
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.Titles.Clear();
                chart.Legends.Clear();

                var area = new ChartArea("DailyArea");
                area.AxisX.Interval = 0;
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.Enabled = true;
                chart.ChartAreas.Add(area);

                chart.Titles.Add("Daily Consultation Summary (Last 30 Days)");
                chart.Legends.Add(new Legend() { Docking = Docking.Top });

                var total = new Series("TotalConsults")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "DayLabel",
                    YValueMembers = "TotalConsults",
                    BorderWidth = 2,
                    IsValueShownAsLabel = true,
                    ChartArea = "DailyArea"
                };
                chart.Series.Add(total);

                chart.Series.Add(CreateLineSeries("Ear", "DayLabel", "EarCount", "DailyArea"));
                chart.Series.Add(CreateLineSeries("Nose", "DayLabel", "NoseCount", "DailyArea"));
                chart.Series.Add(CreateLineSeries("Throat", "DayLabel", "ThroatCount", "DailyArea"));
                chart.Series.Add(CreateLineSeries("Others", "DayLabel", "OthersCount", "DailyArea"));

                chart.DataSource = dt;
                chart.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Daily chart error: " + ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Create a line series with explicit X column and ChartArea.
        /// Caller must pass the correct X column name (e.g. "DayLabel" or "MonthName")
        /// and the exact ChartArea name created on the chart.
        /// </summary>
        private Series CreateLineSeries(string name, string xMember, string yMember, string chartArea)
        {
            if (string.IsNullOrWhiteSpace(chartArea))
                throw new ArgumentException("chartArea must be provided and must match an existing ChartArea.Name", nameof(chartArea));

            var s = new Series(name)
            {
                ChartType = SeriesChartType.Line,
                XValueMember = xMember,
                YValueMembers = yMember,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 5,
                ChartArea = chartArea,
                IsValueShownAsLabel = false
            };
            return s;
        }

        #endregion

        #region Queue summaries (daily & monthly)

        private void LoadQueueDaily()
        {
            const string sql = @"
SELECT 
    x.DayDate,
    DATE_FORMAT(x.DayDate, '%b %d') AS DayLabel,
    COUNT(q.queue_id) AS TotalQueued,
    SUM(CASE WHEN q.status = 'Examining' THEN 1 ELSE 0 END) AS CalledCount,
    SUM(CASE WHEN q.status = 'Done' THEN 1 ELSE 0 END) AS FinishedCount,
    SUM(CASE WHEN q.status = 'Waiting' THEN 1 ELSE 0 END) AS PendingCount,
    SUM(CASE WHEN q.status = 'Skipped' THEN 1 ELSE 0 END) AS SkippedCount,
    SUM(CASE WHEN q.status = 'Cancelled' THEN 1 ELSE 0 END) AS CancelledCount
FROM (
    SELECT DATE(created_at) AS DayDate
    FROM queue
    WHERE created_at >= DATE_SUB(CURDATE(), INTERVAL 29 DAY)
    GROUP BY DATE(created_at)
) x
LEFT JOIN queue q 
    ON DATE(q.created_at) = x.DayDate
GROUP BY x.DayDate
ORDER BY x.DayDate;
";
            var raw = QueryToTable(sql);

            var dt = new DataTable();
            dt.Columns.Add("DayDate", typeof(DateTime));
            dt.Columns.Add("DayLabel", typeof(string));
            dt.Columns.Add("TotalQueued", typeof(int));
            dt.Columns.Add("CalledCount", typeof(int));
            dt.Columns.Add("FinishedCount", typeof(int));
            dt.Columns.Add("PendingCount", typeof(int));
            dt.Columns.Add("SkippedCount", typeof(int));
            dt.Columns.Add("CancelledCount", typeof(int));

            if (raw != null)
            {
                foreach (DataRow r in raw.Rows)
                {
                    dt.Rows.Add(
                        r["DayDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(r["DayDate"]),
                        r["DayLabel"]?.ToString() ?? "",
                        r["TotalQueued"] == DBNull.Value ? 0 : Convert.ToInt32(r["TotalQueued"]),
                        r["CalledCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["CalledCount"]),
                        r["FinishedCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["FinishedCount"]),
                        r["PendingCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["PendingCount"]),
                        r["SkippedCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["SkippedCount"]),
                        r["CancelledCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["CancelledCount"])
                    );
                }
            }

            dgvQueueDaily.DataSource = dt;
            FormatGrid(dgvQueueDaily);
            RenderDailyQueueChart(chartQueueDaily, dt);
        }

        private void RenderDailyQueueChart(Chart chart, DataTable dt)
        {
            try
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.Titles.Clear();
                chart.Legends.Clear();

                var area = new ChartArea("QDaily");
                area.AxisX.Interval = 0;
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.Enabled = true;
                chart.ChartAreas.Add(area);

                chart.Titles.Add("Daily Queue Summary (Last 30 Days)");
                chart.Legends.Add(new Legend() { Docking = Docking.Top });

                var total = new Series("TotalQueued")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "DayLabel",
                    YValueMembers = "TotalQueued",
                    BorderWidth = 2,
                    IsValueShownAsLabel = true,
                    ChartArea = "QDaily"
                };
                chart.Series.Add(total);

                chart.Series.Add(CreateLineSeries("Called", "DayLabel", "CalledCount", "QDaily"));
                chart.Series.Add(CreateLineSeries("Finished", "DayLabel", "FinishedCount", "QDaily"));
                chart.Series.Add(CreateLineSeries("Pending", "DayLabel", "PendingCount", "QDaily"));
                chart.Series.Add(CreateLineSeries("Skipped", "DayLabel", "SkippedCount", "QDaily"));
                chart.Series.Add(CreateLineSeries("Cancelled", "DayLabel", "CancelledCount", "QDaily"));

                chart.DataSource = dt;
                chart.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Daily queue chart error: " + ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadQueueMonthly()
        {
            const string sql = @"
SELECT 
    m.MonthNumber,
    m.MonthName,
    COUNT(q.queue_id) AS TotalQueued,
    SUM(CASE WHEN q.status = 'Examining' THEN 1 ELSE 0 END) AS CalledCount,
    SUM(CASE WHEN q.status = 'Done' THEN 1 ELSE 0 END) AS FinishedCount,
    SUM(CASE WHEN q.status = 'Waiting' THEN 1 ELSE 0 END) AS PendingCount,
    SUM(CASE WHEN q.status = 'Skipped' THEN 1 ELSE 0 END) AS SkippedCount,
    SUM(CASE WHEN q.status = 'Cancelled' THEN 1 ELSE 0 END) AS CancelledCount
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
LEFT JOIN queue q
    ON MONTH(q.created_at) = m.MonthNumber
   AND YEAR(q.created_at) = YEAR(CURDATE())
GROUP BY m.MonthNumber, m.MonthName
ORDER BY m.MonthNumber;
";
            var raw = QueryToTable(sql);

            var dt = new DataTable();
            dt.Columns.Add("MonthNumber", typeof(int));
            dt.Columns.Add("MonthName", typeof(string));
            dt.Columns.Add("TotalQueued", typeof(int));
            dt.Columns.Add("CalledCount", typeof(int));
            dt.Columns.Add("FinishedCount", typeof(int));
            dt.Columns.Add("PendingCount", typeof(int));
            dt.Columns.Add("SkippedCount", typeof(int));
            dt.Columns.Add("CancelledCount", typeof(int));

            if (raw != null)
            {
                foreach (DataRow r in raw.Rows)
                {
                    dt.Rows.Add(
                        r["MonthNumber"] == DBNull.Value ? 0 : Convert.ToInt32(r["MonthNumber"]),
                        r["MonthName"]?.ToString() ?? string.Empty,
                        r["TotalQueued"] == DBNull.Value ? 0 : Convert.ToInt32(r["TotalQueued"]),
                        r["CalledCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["CalledCount"]),
                        r["FinishedCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["FinishedCount"]),
                        r["PendingCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["PendingCount"]),
                        r["SkippedCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["SkippedCount"]),
                        r["CancelledCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["CancelledCount"])
                    );
                }
            }

            dgvQueueMonthly.DataSource = dt;
            FormatGrid(dgvQueueMonthly);
            RenderMonthlyQueueChart(chartQueueMonthly, dt);
        }

        private void RenderMonthlyQueueChart(Chart chart, DataTable dt)
        {
            try
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.Titles.Clear();
                chart.Legends.Clear();

                var ca = new ChartArea("QMonthly");
                ca.AxisX.LabelStyle.Angle = -45;
                ca.AxisX.Interval = 1;
                ca.AxisY.MajorGrid.LineColor = Color.LightGray;
                ca.AxisX.MajorGrid.LineColor = Color.LightGray;
                chart.ChartAreas.Add(ca);

                chart.Titles.Add("Monthly Queue Summary (This Year)");
                chart.Legends.Add(new Legend() { Docking = Docking.Top });

                var s = new Series("TotalQueued")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "MonthName",
                    YValueMembers = "TotalQueued",
                    BorderWidth = 2,
                    IsValueShownAsLabel = true,
                    ChartArea = "QMonthly"
                };
                chart.Series.Add(s);

                chart.Series.Add(CreateLineSeries("Called", "MonthName", "CalledCount", "QMonthly"));
                chart.Series.Add(CreateLineSeries("Finished", "MonthName", "FinishedCount", "QMonthly"));
                chart.Series.Add(CreateLineSeries("Pending", "MonthName", "PendingCount", "QMonthly"));
                chart.Series.Add(CreateLineSeries("Skipped", "MonthName", "SkippedCount", "QMonthly"));
                chart.Series.Add(CreateLineSeries("Cancelled", "MonthName", "CancelledCount", "QMonthly"));

                chart.DataSource = dt;
                chart.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Monthly queue chart error: " + ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Patient stats

        private void LoadPatientStats()
        {
            const string sqlAge = @"
SELECT 
    CASE
        WHEN age IS NULL OR age < 0 OR age > 120 THEN 'Unknown'
        WHEN age BETWEEN 0 AND 12 THEN '0-12'
        WHEN age BETWEEN 13 AND 19 THEN '13-19'
        WHEN age BETWEEN 20 AND 39 THEN '20-39'
        WHEN age BETWEEN 40 AND 59 THEN '40-59'
        WHEN age BETWEEN 60 AND 79 THEN '60-79'
        WHEN age BETWEEN 80 AND 99 THEN '80-99'
        WHEN age BETWEEN 100 AND 120 THEN '100-120'
        ELSE 'Unknown'
    END AS AgeGroup,
    COUNT(*) AS CountPatients
FROM consultation
WHERE YEAR(consultation_date) = YEAR(CURDATE())
GROUP BY AgeGroup
ORDER BY FIELD(AgeGroup, '0-12','13-19','20-39','40-59','60-79','80-99','100-120','Unknown');
";
            var dtAge = QueryToTable(sqlAge);
            dgvPatientStats.DataSource = dtAge;
            FormatGrid(dgvPatientStats);
            RenderPie(chartPatientStats, dtAge, "AgeGroup", "CountPatients", "Age Group Distribution");
        }

        #endregion

        #region Helpers: Query / Format / Charts / Schema

        private DataTable QueryToTable(string sql, Dictionary<string, object> parameters = null)
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
                            cmd.Parameters.AddWithValue(p.Key, p.Value);
                    }
                    using (var da = new MySqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error:\n" + ex.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        private void FormatGrid(DataGridView dgv)
        {
            if (dgv == null || dgv.DataSource == null) return;

            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dgv.RowHeadersVisible = false;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                string name = col.Name.ToLower();
                if (name.Contains("count") || name.Contains("total") || name.Contains("consult") || name.Contains("amount") || name.Contains("age") || name.Contains("queued"))
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

        private void RenderPie(Chart chart, DataTable dt, string labelColumn, string valueColumn, string seriesName)
        {
            try
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.Legends.Clear();

                var ca = new ChartArea("CA");
                ca.Area3DStyle.Enable3D = true;
                chart.ChartAreas.Add(ca);

                var legend = new Legend("L") { Docking = Docking.Bottom, LegendStyle = LegendStyle.Table };
                chart.Legends.Add(legend);

                var s = new Series(seriesName) { ChartType = SeriesChartType.Pie, ChartArea = "CA", IsValueShownAsLabel = true };
                s["PieLabelStyle"] = "Outside";
                s.Label = "#PERCENT{P1}\n#VAL{N0}";
                chart.Series.Add(s);

                if (dt != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        var lbl = r[labelColumn]?.ToString() ?? "Unknown";
                        long val = 0;
                        try { val = Convert.ToInt64(r[valueColumn]); } catch { val = 0; }
                        if (val <= 0) continue;
                        s.Points.AddY(val);
                        var pt = s.Points[s.Points.Count - 1];
                        pt.AxisLabel = lbl;
                        pt.LegendText = $"{lbl} ({val:N0})";
                    }
                }

                if (s.Points.Count == 0)
                {
                    s.Points.AddY(1);
                    s.Points[0].AxisLabel = "No data";
                    s.Points[0].LegendText = "No data";
                }

                chart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pie chart error: " + ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ColumnExists(string tableName, string columnName)
        {
            try
            {
                const string sql = @"
SELECT COUNT(*) AS Cnt
FROM information_schema.columns
WHERE table_schema = DATABASE()
  AND table_name = @tbl
  AND column_name = @col;
";
                var dt = QueryToTable(sql, new Dictionary<string, object> { { "@tbl", tableName }, { "@col", columnName } });
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["Cnt"]) > 0;
                }
            }
            catch { /* ignore */ }
            return false;
        }

        #endregion

        #region Export CSV & active grid

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            var active = GetActiveGrid();
            if (active == null || active.DataSource == null)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog { Filter = "CSV file|*.csv", FileName = "export.csv" })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var dt = (DataTable)active.DataSource;
                    var sb = new StringBuilder();
                    var cols = dt.Columns.Cast<DataColumn>().Select(c => "\"" + c.ColumnName.Replace("\"", "\"\"") + "\"");
                    sb.AppendLine(string.Join(",", cols));
                    foreach (DataRow r in dt.Rows)
                    {
                        var fields = dt.Columns.Cast<DataColumn>().Select(c => "\"" + Convert.ToString(r[c]).Replace("\"", "\"\"") + "\"");
                        sb.AppendLine(string.Join(",", fields));
                    }
                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Exported.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export failed: " + ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private DataGridView GetActiveGrid()
        {
            var t = tabControl.SelectedTab;
            if (t == null) return null;
            switch (t.Name)
            {
                case "tabMonthly": return dgvEnt;
                case "tabDaily": return dgvDaily;
                case "tabQueueDaily": return dgvQueueDaily;
                case "tabQueueMonthly": return dgvQueueMonthly;
                case "tabPatientStats": return dgvPatientStats;
                default: return null;
            }
        }

        #endregion

        #region Misc UI stubs

        private void ChartEnt_MouseClick(object sender, MouseEventArgs e) { /* optional drilldown */ }
        private void dgvService_CellContentClick(object sender, DataGridViewCellEventArgs e) { /* designer stub */ }
        private void splitPatientStats_Panel1_Paint(object sender, PaintEventArgs e) { /* designer stub */ }

        #endregion
    }
}
