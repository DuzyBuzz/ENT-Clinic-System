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
                case "tabConsultation":   // <--- new tab
                    cmbExam.SelectedIndex = 0;
                    LoadEntExamTrend(cmbExam.SelectedItem.ToString());
                    break;
                case "tabMostBoughtItems":
                    LoadMonthlyDispensingByItemThisMonth();
                    break;
                case "tabBilling":
                    LoadMonthlyBillingSummary();
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

        // ----------------------
        // RenderMonthlyLineChart (dynamic Y axis)
        // ----------------------
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
                ca.AxisX.Interval = 1;
                ca.AxisY.Title = "Count";
                ca.AxisY.MajorGrid.LineColor = Color.LightGray;
                chart.ChartAreas.Add(ca);

                chart.Titles.Add(new Title("Monthly ENT Summary (This Year)", Docking.Top,
                    new Font("Segoe UI", 12, FontStyle.Bold), Color.FromArgb(40, 40, 40)));

                chart.Legends.Add(new Legend("Legend") { Docking = Docking.Top, LegendStyle = LegendStyle.Row });

                var totalSeries = new Series("Total Consultations")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "MonthName",
                    YValueMembers = "TotalConsults",
                    IsValueShownAsLabel = true,
                    ChartArea = "MainArea",
                    Color = Color.LightGray
                };
                chart.Series.Add(totalSeries);

                chart.Series.Add(CreateLineSeries("Ear", "MonthName", "EarCount", "MainArea"));
                chart.Series.Add(CreateLineSeries("Nose", "MonthName", "NoseCount", "MainArea"));
                chart.Series.Add(CreateLineSeries("Throat", "MonthName", "ThroatCount", "MainArea"));
                chart.Series.Add(CreateLineSeries("Others", "MonthName", "OthersCount", "MainArea"));

                // determine max value across relevant columns to set axis nicely
                try
                {
                    var values = new List<int>();
                    foreach (DataRow r in dt.Rows)
                    {
                        int v;
                        if (int.TryParse(Convert.ToString(r["TotalConsults"]), out v)) values.Add(v);
                        if (int.TryParse(Convert.ToString(r["EarCount"]), out v)) values.Add(v);
                        if (int.TryParse(Convert.ToString(r["NoseCount"]), out v)) values.Add(v);
                        if (int.TryParse(Convert.ToString(r["ThroatCount"]), out v)) values.Add(v);
                        if (int.TryParse(Convert.ToString(r["OthersCount"]), out v)) values.Add(v);
                    }
                    ApplyDynamicYAxis(ca, values);
                }
                catch { /* ignore if something unexpected */ }

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
WITH RECURSIVE days AS (
    SELECT CURDATE() - INTERVAL 29 DAY AS DayDate
    UNION ALL
    SELECT DayDate + INTERVAL 1 DAY
    FROM days
    WHERE DayDate < CURDATE()
)
SELECT 
    d.DayDate,
    DATE_FORMAT(d.DayDate, '%b %d') AS DayLabel,
    COUNT(c.consultation_id) AS TotalConsults,
    SUM(CASE WHEN c.ear_exam IS NOT NULL AND TRIM(c.ear_exam) <> '' THEN 1 ELSE 0 END) AS EarCount,
    SUM(CASE WHEN c.nose_exam IS NOT NULL AND TRIM(c.nose_exam) <> '' THEN 1 ELSE 0 END) AS NoseCount,
    SUM(CASE WHEN c.throat_exam IS NOT NULL AND TRIM(c.throat_exam) <> '' THEN 1 ELSE 0 END) AS ThroatCount,
    SUM(CASE WHEN c.others_exam IS NOT NULL AND TRIM(c.others_exam) <> '' THEN 1 ELSE 0 END) AS OthersCount
FROM days d
LEFT JOIN consultation c
    ON DATE(c.consultation_date) = d.DayDate
GROUP BY d.DayDate
ORDER BY d.DayDate;
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

        // ----------------------
        // RenderDailyLineChart (dynamic Y axis)
        // ----------------------
        private void RenderDailyLineChart(Chart chart, DataTable dt)
        {
            try
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.Titles.Clear();
                chart.Legends.Clear();

                var area = new ChartArea("DailyArea");
                area.AxisX.Interval = 1;                 // show every day label
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisX.LabelStyle.Angle = -45;
                area.AxisX.IsMarginVisible = true;
                area.AxisY.MajorGrid.Enabled = true;
                area.AxisY.MajorGrid.LineColor = Color.LightGray;
                chart.ChartAreas.Add(area);

                chart.Titles.Add("Daily Consultation Summary (Last 30 Days)");
                chart.Legends.Add(new Legend() { Docking = Docking.Top });

                var total = new Series("Total Consultations")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "DayLabel",
                    YValueMembers = "TotalConsults",
                    BorderWidth = 2,
                    IsValueShownAsLabel = true,
                    ChartArea = "DailyArea",
                    Color = Color.LightGray
                };
                chart.Series.Add(total);

                chart.Series.Add(CreateLineSeries("Ear", "DayLabel", "EarCount", "DailyArea"));
                chart.Series.Add(CreateLineSeries("Nose", "DayLabel", "NoseCount", "DailyArea"));
                chart.Series.Add(CreateLineSeries("Throat", "DayLabel", "ThroatCount", "DailyArea"));
                chart.Series.Add(CreateLineSeries("Others", "DayLabel", "OthersCount", "DailyArea"));

                // Build values list and apply dynamic Y axis
                try
                {
                    var values = new List<int>();
                    foreach (DataRow r in dt.Rows)
                    {
                        int v;
                        if (int.TryParse(Convert.ToString(r["TotalConsults"]), out v)) values.Add(v);
                        if (int.TryParse(Convert.ToString(r["EarCount"]), out v)) values.Add(v);
                        if (int.TryParse(Convert.ToString(r["NoseCount"]), out v)) values.Add(v);
                        if (int.TryParse(Convert.ToString(r["ThroatCount"]), out v)) values.Add(v);
                        if (int.TryParse(Convert.ToString(r["OthersCount"]), out v)) values.Add(v);
                    }
                    ApplyDynamicYAxis(area, values);
                }
                catch { /* ignore */ }

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
WITH RECURSIVE days AS (
    SELECT CURDATE() - INTERVAL 29 DAY AS DayDate
    UNION ALL
    SELECT DayDate + INTERVAL 1 DAY
    FROM days
    WHERE DayDate < CURDATE()
)
SELECT 
    d.DayDate,
    DATE_FORMAT(d.DayDate, '%b %d') AS DayLabel,

    COUNT(q.queue_id) AS TotalQueued,
    SUM(CASE WHEN q.status = 'Examining' THEN 1 ELSE 0 END) AS CalledCount,
    SUM(CASE WHEN q.status = 'Done' THEN 1 ELSE 0 END) AS FinishedCount,
    SUM(CASE WHEN q.status = 'Waiting' THEN 1 ELSE 0 END) AS WaitingCount,
    SUM(CASE WHEN q.status = 'Skipped' THEN 1 ELSE 0 END) AS SkippedCount,
    SUM(CASE WHEN q.status = 'Cancelled' THEN 1 ELSE 0 END) AS CancelledCount

FROM days d
LEFT JOIN queue q
    ON DATE(q.created_at) = d.DayDate

GROUP BY d.DayDate
ORDER BY d.DayDate;
";

            var raw = QueryToTable(sql);

            var dt = new DataTable();
            dt.Columns.Add("DayDate", typeof(DateTime));
            dt.Columns.Add("DayLabel", typeof(string));
            dt.Columns.Add("TotalQueued", typeof(int));
            dt.Columns.Add("CalledCount", typeof(int));
            dt.Columns.Add("FinishedCount", typeof(int));
            dt.Columns.Add("WaitingCount", typeof(int));
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
                        r["WaitingCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["WaitingCount"]),
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
                area.AxisX.Interval = 1;                 // ← show each day
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisX.LabelStyle.Angle = -45;       // ← readable
                area.AxisX.IsMarginVisible = true;       // ← prevent missing days
                area.AxisY.MajorGrid.Enabled = true;
                chart.ChartAreas.Add(area);

                chart.Titles.Add("Daily Queue Summary (Last 30 Days)");
                chart.Legends.Add(new Legend() { Docking = Docking.Top });

                var total = new Series("Total Queued")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "DayLabel",
                    YValueMembers = "TotalQueued",
                    BorderWidth = 2,
                    IsValueShownAsLabel = true,
                    ChartArea = "QDaily",
                    Color = Color.LightGray
                };
                chart.Series.Add(total);

                chart.Series.Add(CreateLineSeries("Called", "DayLabel", "CalledCount", "QDaily"));
                chart.Series["Called"].Color = Color.SteelBlue; // Examining

                chart.Series.Add(CreateLineSeries("Finished", "DayLabel", "FinishedCount", "QDaily"));
                chart.Series["Finished"].Color = Color.MediumSeaGreen; // Done

                chart.Series.Add(CreateLineSeries("Waiting", "DayLabel", "WaitingCount", "QDaily"));
                chart.Series["Waiting"].Color = Color.Goldenrod; // Waiting

                chart.Series.Add(CreateLineSeries("Skipped", "DayLabel", "SkippedCount", "QDaily"));
                chart.Series["Skipped"].Color = Color.Gray; // Skipped

                chart.Series.Add(CreateLineSeries("Cancelled", "DayLabel", "CancelledCount", "QDaily"));
                chart.Series["Cancelled"].Color = Color.IndianRed; // Cancelled


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
    SUM(CASE WHEN q.status = 'Waiting' THEN 1 ELSE 0 END) AS WaitingCount,
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
            dt.Columns.Add("WaitingCount", typeof(int));
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
                        r["WaitingCount"] == DBNull.Value ? 0 : Convert.ToInt32(r["WaitingCount"]),
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

                var s = new Series("Total Queued")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "MonthName",
                    YValueMembers = "TotalQueued",
                    BorderWidth = 2,
                    IsValueShownAsLabel = true,
                    ChartArea = "QMonthly",
                    Color = Color.LightGray

                };
                chart.Series.Add(s);


                chart.Series.Add(CreateLineSeries("Called", "MonthName", "CalledCount", "QMonthly"));
                chart.Series["Called"].Color = Color.SteelBlue;

                chart.Series.Add(CreateLineSeries("Finished", "MonthName", "FinishedCount", "QMonthly"));
                chart.Series["Finished"].Color = Color.MediumSeaGreen;

                chart.Series.Add(CreateLineSeries("Waiting", "MonthName", "WaitingCount", "QMonthly"));
                chart.Series["Waiting"].Color = Color.Goldenrod;

                chart.Series.Add(CreateLineSeries("Skipped", "MonthName", "SkippedCount", "QMonthly"));
                chart.Series["Skipped"].Color = Color.Gray;

                chart.Series.Add(CreateLineSeries("Cancelled", "MonthName", "CancelledCount", "QMonthly"));
                chart.Series["Cancelled"].Color = Color.IndianRed;


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



        #region ENT Overview + Exam Distribution

        // Load general ENT overview in DataGridView
        // Load general ENT overview in DataGridView
        private void LoadEntConsultationOverview()
        {
            const string sql = @"
SELECT 
    column_name AS ColumnName,
    value AS Value,
    `count` AS EntryCount
FROM autocomplete_entries
ORDER BY column_name, `count` DESC;
";

            var dt = QueryToTable(sql);
            if (dt == null) return;

            dgvEntOverview.DataSource = dt;
            FormatGrid(dgvEntOverview);

            // Automatically render chart for the first column if data exists
            // No default mapping to ear_exam or chief_complaint
            if (dgvEntOverview.Rows.Count > 0)
            {
                string firstColumn = dgvEntOverview.Rows[0].Cells["ColumnName"].Value?.ToString();
                if (!string.IsNullOrEmpty(firstColumn))
                {
                    RenderAutocompleteChart(firstColumn);
                }
            }
        }

        // Render chart for autocomplete_entries column
        private void RenderAutocompleteChart(string columnName)
        {
            if (string.IsNullOrEmpty(columnName)) return;

            string sql = @"
SELECT 
    value AS Value,
    `count` AS EntryCount
FROM v_autocomplete_entries
WHERE column_name = @col
ORDER BY EntryCount DESC;
";

            var dt = QueryToTable(sql, new Dictionary<string, object> { { "@col", columnName } });
            if (dt == null || dt.Rows.Count == 0) return;

            // Top 20 values, combine others
            int topN = 20;
            var dtChart = dt.Clone();
            int othersCount = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i < topN)
                    dtChart.ImportRow(dt.Rows[i]);
                else
                    othersCount += Convert.ToInt32(dt.Rows[i]["EntryCount"]);
            }

            if (othersCount > 0)
            {
                var row = dtChart.NewRow();
                row["Value"] = "Others";
                row["EntryCount"] = othersCount;
                dtChart.Rows.Add(row);
            }

            // Clear previous chart
            chartEntOverview.Series.Clear();
            chartEntOverview.ChartAreas.Clear();
            chartEntOverview.Titles.Clear();
            chartEntOverview.Legends.Clear();

            var ca = new ChartArea("CA");
            ca.AxisX.Interval = 1;
            ca.AxisX.LabelStyle.Angle = -45;
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.MajorGrid.LineColor = Color.LightGray;
            ca.AxisY.Title = "Count";
            ca.AxisX.Title = "Values";
            chartEntOverview.ChartAreas.Add(ca);

            chartEntOverview.Titles.Add($"Top {topN} Values for '{columnName}'");
            chartEntOverview.Legends.Add(new Legend() { Docking = Docking.Bottom, LegendStyle = LegendStyle.Table });

            var series = new Series("Entries")
            {
                ChartType = SeriesChartType.Column,
                XValueMember = "Value",
                YValueMembers = "EntryCount",
                IsValueShownAsLabel = true,
                ChartArea = "CA",
                Color = Color.SteelBlue
            };
            chartEntOverview.Series.Add(series);

            chartEntOverview.DataSource = dtChart;
            chartEntOverview.DataBind();
        }


        // Load ENT exam value distribution based on ComboBox selection
        private void LoadEntExamTrend(string examName)
        {
            string examColumn;

            switch (examName)
            {
                case "Ear Exam":
                    examColumn = "ear_exam";
                    break;
                case "Nose Exam":
                    examColumn = "nose_exam";
                    break;
                case "Throat Exam":
                    examColumn = "throat_exam";
                    break;
                case "Others Exam":
                    examColumn = "others_exam";
                    break;
                default:
                    examColumn = "ear_exam";
                    break;
            }

            RenderExamValueDistribution(examColumn, examName);
        }

        // Render chart for selected ENT exam
        // Render chart for selected ENT exam
        // ----------------------
        // RenderExamValueDistribution (dynamic Y axis) - top 20 (no "Others")
        // ----------------------
        private void RenderExamValueDistribution(string examColumn, string displayName)
        {
            if (string.IsNullOrEmpty(examColumn)) return;

            string sql = $@"
SELECT 
    {examColumn} AS Value,
    COUNT(*) AS EntryCount
FROM consultation
WHERE {examColumn} IS NOT NULL AND TRIM({examColumn}) <> ''
GROUP BY {examColumn}
ORDER BY EntryCount DESC
LIMIT 20;  -- Only take top 20
";

            var dt = QueryToTable(sql);
            if (dt == null || dt.Rows.Count == 0) return;

            // Clear previous chart
            chartEntOverview.Series.Clear();
            chartEntOverview.ChartAreas.Clear();
            chartEntOverview.Titles.Clear();
            chartEntOverview.Legends.Clear();

            var ca = new ChartArea("CA");
            ca.AxisX.Interval = 1;
            ca.AxisX.LabelStyle.Angle = -45;
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.MajorGrid.LineColor = Color.LightGray;
            ca.AxisX.Title = "Values";
            ca.AxisY.Title = "Count";
            chartEntOverview.ChartAreas.Add(ca);

            chartEntOverview.Titles.Add($"Top {dt.Rows.Count} Values for {displayName}");
            chartEntOverview.Legends.Add(new Legend() { Docking = Docking.Bottom, LegendStyle = LegendStyle.Table });

            var series = new Series("Entries")
            {
                ChartType = SeriesChartType.Column,
                XValueMember = "Value",
                YValueMembers = "EntryCount",
                IsValueShownAsLabel = true,
                ChartArea = "CA",
                Color = Color.SteelBlue
            };
            chartEntOverview.Series.Add(series);

            // compute dynamic axis range from dt values
            try
            {
                var vals = dt.AsEnumerable().Select(r => Convert.ToInt32(r["EntryCount"])).ToList();
                ApplyDynamicYAxis(ca, vals);
            }
            catch { /* ignore */ }

            chartEntOverview.DataSource = dt;
            chartEntOverview.DataBind();
        }


        #endregion



        #region Mosty bought item

        private void LoadMonthlyDispensingByItemThisMonth()
        {
            const string sql = @"
SELECT 
    i.item_id,
    i.generic_name,
    i.brand_name,
    i.strength,
    i.dosage,
    i.category,
    SUM(ii.quantity) AS total_quantity_sold,
    SUM(ii.total_price) AS total_revenue
FROM ent_clinic_db.invoice_items ii
INNER JOIN ent_clinic_db.items i
    ON ii.item_id = i.item_id
INNER JOIN ent_clinic_db.invoices inv
    ON ii.invoice_id = inv.invoice_id
WHERE YEAR(inv.invoice_date) = YEAR(CURDATE())
  AND MONTH(inv.invoice_date) = MONTH(CURDATE())
GROUP BY i.item_id, i.generic_name, i.brand_name, i.strength, i.dosage, i.category
ORDER BY total_quantity_sold DESC;
";

            var dt = QueryToTable(sql);
            if (dt == null || dt.Rows.Count == 0) return;

            // Bind to DataGridView
            dgvMostBoughtItems.DataSource = dt;
            FormatGrid(dgvMostBoughtItems);

            // Render chart
            RenderMonthlyByItemChartThisMonth(dt);
        }
        private void RenderMonthlyByItemChartThisMonth(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            chartMostBought.Series.Clear();
            chartMostBought.ChartAreas.Clear();
            chartMostBought.Titles.Clear();
            chartMostBought.Legends.Clear();

            var ca = new ChartArea("MainArea");
            ca.AxisX.Title = "Item";
            ca.AxisY.Title = "Quantity Sold";
            ca.AxisX.Interval = 1;
            ca.AxisX.LabelStyle.Angle = -45;
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartMostBought.ChartAreas.Add(ca);

            chartMostBought.Titles.Add($"Dispensing Summary for {DateTime.Now:MMMM yyyy}");
            chartMostBought.Legends.Add(new Legend() { Docking = Docking.Bottom, LegendStyle = LegendStyle.Table });

            // Create ONE series for all items
            var series = new Series("Quantity Sold")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                ChartArea = "MainArea",
                Color = Color.SteelBlue
            };

            // Add points for each item
            foreach (DataRow row in dt.Rows)
            {
                string itemName = row["brand_name"].ToString();
                double quantity = Convert.ToDouble(row["total_quantity_sold"]);
                series.Points.AddXY(itemName, quantity);
            }

            chartMostBought.Series.Add(series);
        }







        #endregion
        private void LoadMonthlyBillingSummary()
        {
            const string sql = @"
SELECT 
    m.MonthNumber,
    m.MonthName,
    SUM(b.total_amount) AS TotalBilled,
    SUM(b.discount_amount) AS TotalDiscount,
    SUM(b.amount_paid) AS TotalPaid,
    SUM(b.balance) AS TotalBalance,
    COUNT(b.billing_id) AS TotalBills
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
LEFT JOIN ent_clinic_db.billing b
    ON MONTH(b.created_at) = m.MonthNumber
   AND YEAR(b.created_at) = YEAR(CURDATE())
GROUP BY m.MonthNumber, m.MonthName
ORDER BY m.MonthNumber;
";

            var dt = QueryToTable(sql);
            if (dt == null || dt.Rows.Count == 0) return;

            // Format money columns as N2
            foreach (DataRow row in dt.Rows)
            {
                row["TotalBilled"] = row["TotalBilled"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TotalBilled"]);
                row["TotalDiscount"] = row["TotalDiscount"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TotalDiscount"]);
                row["TotalPaid"] = row["TotalPaid"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TotalPaid"]);
                row["TotalBalance"] = row["TotalBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TotalBalance"]);
            }

            // Bind to DataGridView
            dgvBilling.DataSource = dt;
            FormatGrid(dgvBilling);

            // Apply N2 formatting to money columns
            dgvBilling.Columns["TotalBilled"].DefaultCellStyle.Format = "N2";
            dgvBilling.Columns["TotalDiscount"].DefaultCellStyle.Format = "N2";
            dgvBilling.Columns["TotalPaid"].DefaultCellStyle.Format = "N2";
            dgvBilling.Columns["TotalBalance"].DefaultCellStyle.Format = "N2";

            // Rename headers to presentable title case
            dgvBilling.Columns["MonthNumber"].HeaderText = "Month #";
            dgvBilling.Columns["MonthName"].HeaderText = "Month";
            dgvBilling.Columns["TotalBilled"].HeaderText = "Total Billed";
            dgvBilling.Columns["TotalDiscount"].HeaderText = "Total Discount";
            dgvBilling.Columns["TotalPaid"].HeaderText = "Total Paid";
            dgvBilling.Columns["TotalBalance"].HeaderText = "Total Balance";
            dgvBilling.Columns["TotalBills"].HeaderText = "Total Bills";


            // Render chart
            RenderMonthlyBillingChart(dt);
        }

        private void RenderMonthlyBillingChart(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            chartBilling.Series.Clear();
            chartBilling.ChartAreas.Clear();
            chartBilling.Titles.Clear();
            chartBilling.Legends.Clear();

            var ca = new ChartArea("MainArea");
            ca.AxisX.Title = "Month";
            ca.AxisY.Title = "Amount";
            ca.AxisX.Interval = 1;
            ca.AxisX.LabelStyle.Angle = -45;
            ca.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartBilling.ChartAreas.Add(ca);

            chartBilling.Titles.Add($"Monthly Billing Summary - {DateTime.Now:yyyy}");
            chartBilling.Legends.Add(new Legend() { Docking = Docking.Bottom, LegendStyle = LegendStyle.Table });

            // Ensure month names exist
            dt.Columns.Add("MonthNameTemp", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                row["MonthNameTemp"] = row["MonthName"].ToString();
            }

            dgvBilling.Columns["MonthNameTemp"].Visible = false;
            dgvBilling.Columns["MonthNumber"].Visible = false;
            // All series as columns
            string[] seriesNames = { "Total Billed", "Total Discount", "Total Paid", "Total Balance" };
            Color[] seriesColors = { Color.SteelBlue, Color.OrangeRed, Color.Green, Color.Purple };
            string[] yValueMembers = { "TotalBilled", "TotalDiscount", "TotalPaid", "TotalBalance" };

            for (int i = 0; i < seriesNames.Length; i++)
            {
                var series = new Series(seriesNames[i])
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "MonthNameTemp",
                    YValueMembers = yValueMembers[i],
                    IsValueShownAsLabel = true,
                    ChartArea = "MainArea",
                    Color = seriesColors[i],
                    LabelFormat = "N2"
                };
                chartBilling.Series.Add(series);
            }

            chartBilling.DataSource = dt;
            chartBilling.DataBind();
        }







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

            dgv.RowHeadersVisible = false;
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                string name = col.Name.ToLower();
                if (name.Contains("count") || name.Contains("total") || name.Contains("amount"))
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
                case "tabConsultaton": return dgvEnt;
                case "tabDaily": return dgvDaily;
                case "tabQueueDaily": return dgvQueueDaily;
                case "tabQueueMonthly": return dgvQueueMonthly;
                case "tabPatientStats": return dgvPatientStats;
                case "tabConsultation": return dgvEntOverview;
                    
                default: return null;
            }
        }

        #endregion

        #region Misc UI stubs

        private void ChartEnt_MouseClick(object sender, MouseEventArgs e) { /* optional drilldown */ }
        private void dgvService_CellContentClick(object sender, DataGridViewCellEventArgs e) { /* designer stub */ }
        private void splitPatientStats_Panel1_Paint(object sender, PaintEventArgs e) { /* designer stub */ }

        #endregion

        private void cmbExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEntExamTrend(cmbExam.SelectedItem.ToString());
        }

        private void chartQueueMonthly_Click(object sender, EventArgs e)
        {

        }
        // ----------------------
        // Helper: compute friendly axis maximum and interval and apply
        // ----------------------
        private void ApplyDynamicYAxis(ChartArea ca, IEnumerable<int> values, int desiredSteps = 5)
        {
            try
            {
                int maxVal = 0;
                if (values != null && values.Any())
                    maxVal = values.Max();

                // If nothing or zero, provide sensible defaults
                if (maxVal <= 0)
                {
                    ca.AxisY.Minimum = 0;
                    ca.AxisY.Maximum = 10;
                    ca.AxisY.Interval = 2;
                    return;
                }

                // Compute an interval so there are ~desiredSteps ticks
                double rawInterval = Math.Ceiling(maxVal / (double)desiredSteps);

                // Round rawInterval up to a "nice" number (1,2,5,10,20,50,100,...)
                double magnitude = Math.Pow(10, Math.Floor(Math.Log10(rawInterval)));
                double normalized = rawInterval / magnitude;
                double niceNormalized;

                if (normalized <= 1) niceNormalized = 1;
                else if (normalized <= 2) niceNormalized = 2;
                else if (normalized <= 5) niceNormalized = 5;
                else niceNormalized = 10;

                double niceInterval = niceNormalized * magnitude;

                // Axis maximum = round up maxVal to nearest multiple of niceInterval
                double axisMax = Math.Ceiling(maxVal / niceInterval) * niceInterval;

                ca.AxisY.Minimum = 0;
                ca.AxisY.Maximum = axisMax;
                ca.AxisY.Interval = niceInterval;
            }
            catch
            {
                // fallback safe values
                ca.AxisY.Minimum = 0;
                ca.AxisY.Maximum = 10;
                ca.AxisY.Interval = 2;
            }
        }
    }
}
