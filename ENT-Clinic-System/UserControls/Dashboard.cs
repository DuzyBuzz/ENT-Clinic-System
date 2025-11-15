using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
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

            // initialize simple filters
            cboZoneFilter.Items.Clear();
            cboZoneFilter.Items.Add("All");
            cboZoneFilter.SelectedIndex = 0;

            cboServiceFilter.Items.Clear();
            cboServiceFilter.Items.Add("All");
            cboServiceFilter.SelectedIndex = 0;

            // initial load
            LoadCurrentTab();
        }

        #region Tab switching / refresh

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadCurrentTab();
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCurrentTab();
        }

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
                ////case "tabTopDiagnoses":
                ////    LoadTopDiagnoses();
                //    break;
                case "tabPatientStats":
                    LoadPatientStats();
                    break;
                //case "tabDoctorWorkload":
                //    LoadDoctorWorkload();
                    break;
                //case "tabAggregated":
                //    LoadAggregatedMonthlyCounts();
                //    break;
                default:
                    break;
            }
        }

        #endregion

        #region 1) Monthly ENT summary (chartEnt + dgvEnt)
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
                chart.BackColor = Color.White;

                chart.Titles.Add(new Title("Monthly ENT Summary (This Year)",
                                           Docking.Top,
                                           new Font("Segoe UI", 12, FontStyle.Bold),
                                           Color.FromArgb(40, 40, 40)));

                var ca = new ChartArea("MainArea");
                ca.BackColor = Color.White;
                ca.AxisX.Title = "Month";
                ca.AxisX.Interval = 0;
                ca.AxisX.LabelStyle.Angle = -45;
                ca.AxisX.MajorGrid.LineColor = Color.LightGray;
                ca.AxisY.Title = "Count";
                ca.AxisY.Minimum = 0;
                ca.AxisY.MajorGrid.LineColor = Color.LightGray;
                chart.ChartAreas.Add(ca);

                chart.Legends.Add(new Legend("Legend") { Docking = Docking.Top, LegendStyle = LegendStyle.Row });

                // Total Consults as column (tower)
                var totalSeries = new Series("TotalConsults")
                {
                    ChartType = SeriesChartType.Column,
                    XValueMember = "MonthName",
                    YValueMembers = "TotalConsults",
                    IsValueShownAsLabel = true,
                    BorderWidth = 1
                };
                chart.Series.Add(totalSeries);

                // ENT line series
                var seriesMap = new Dictionary<string, string>
        {
            { "Ear", "EarCount" },
            { "Nose", "NoseCount" },
            { "Throat", "ThroatCount" },
            { "Others", "OthersCount" }
        };

                foreach (var kv in seriesMap)
                {
                    var s = new Series(kv.Key)
                    {
                        ChartType = SeriesChartType.Line,
                        XValueMember = "MonthName",
                        YValueMembers = kv.Value,
                        BorderWidth = 2,
                        MarkerStyle = MarkerStyle.Circle,
                        MarkerSize = 6,
                        IsValueShownAsLabel = false,
                        ChartArea = "MainArea"
                    };
                    chart.Series.Add(s);
                }

                chart.DataSource = dt;
                chart.DataBind();
                chart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to render monthly chart:\n" + ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region 2) Daily summary (last 30 days) - chartDaily + dgvDaily

        // ==========================
        // DAILY SUMMARY LOAD FUNCTION
        // ==========================
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





        // =============================
        // FIXED DAILY CHART RENDER CODE
        // =============================
        private void RenderDailyLineChart(Chart chart, DataTable dt)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Titles.Clear();
            chart.Legends.Clear();

            // Chart Area
            var area = new ChartArea("DailyArea");
            area.AxisX.Interval = 0;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = true;
            chart.ChartAreas.Add(area);

            // Title
            chart.Titles.Add("Daily Consultation Summary (Last 30 Days)");

            // Legend
            var legend = new Legend();
            legend.Docking = Docking.Top;
            chart.Legends.Add(legend);

            // ========================
            // BAR SERIES (TOWER STYLE)
            // ========================
            var total = new Series("TotalConsults")
            {
                ChartType = SeriesChartType.Column,
                XValueMember = "DayLabel",
                YValueMembers = "TotalConsults",
                BorderWidth = 2,
                IsValueShownAsLabel = true
            };
            chart.Series.Add(total);

            // ========================
            // ENT LINE SERIES
            // ========================
            chart.Series.Add(CreateLineSeries("Ear", "EarCount"));
            chart.Series.Add(CreateLineSeries("Nose", "NoseCount"));
            chart.Series.Add(CreateLineSeries("Throat", "ThroatCount"));
            chart.Series.Add(CreateLineSeries("Others", "OthersCount"));

            // Bind Data
            chart.DataSource = dt;
            chart.DataBind();
        }



        // =======================================
        // REUSABLE FUNCTION FOR ENT LINE SERIES
        // =======================================
        private Series CreateLineSeries(string name, string valueMember)
        {
            return new Series(name)
            {
                ChartType = SeriesChartType.Line,
                XValueMember = "DayLabel",
                YValueMembers = valueMember,
                BorderWidth = 2,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 5,
                IsValueShownAsLabel = false
            };
        }

        #endregion

        #region 3) Top Diagnoses (chartDiagnoses + dgvDiagnoses)

        //private void LoadTopDiagnoses()
        //{
        //    const string sql = @"
        //        SELECT 
        //            TRIM(c.diagnosis) AS Diagnosis,
        //            COUNT(*) AS CountDiagnosis
        //        FROM consultation c
        //        WHERE c.diagnosis IS NOT NULL AND TRIM(c.diagnosis) <> ''
        //          AND YEAR(c.consultation_date) = YEAR(CURDATE())
        //        GROUP BY TRIM(c.diagnosis)
        //        ORDER BY CountDiagnosis DESC
        //        LIMIT 50;
        //    ";

        //    var dt = QueryToTable(sql);
        //    dgvDiagnoses.DataSource = dt;
        //    FormatGrid(dgvDiagnoses);
        //    RenderPie(chartDiagnoses, dt, "Diagnosis", "CountDiagnosis", "Diagnoses");
        //}

        #endregion

        #region 4) Patient Stats (chartPatientStats + dgvPatientStats + optional gender pie)

        private void LoadPatientStats()
        {
            // Age groups 0-120
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

            // Render pie chart instead of bar
            RenderPie(chartPatientStats, dtAge, "AgeGroup", "CountPatients", "Age Group Distribution");

            // Gender (if available in patients)
            if (ColumnExists("patients", "sex") || ColumnExists("patients", "gender"))
            {
                string genderCol = ColumnExists("patients", "sex") ? "sex" : "gender";
                string sqlGender = $@"
            SELECT IFNULL(TRIM(p.{genderCol}), 'Unknown') AS Gender, COUNT(*) AS CountGender
            FROM consultation c
            LEFT JOIN patients p ON p.patient_id = c.patient_id
            WHERE YEAR(c.consultation_date) = YEAR(CURDATE())
            GROUP BY IFNULL(TRIM(p.{genderCol}), 'Unknown')
            ORDER BY CountGender DESC;
        ";

                var dtGender = QueryToTable(sqlGender);

                //// Render pie chart for gender
                //RenderPie(chartDiagnoses, dtGender, "Gender", "CountGender", "Gender");
            }
            else
            {
                // No gender column found
                var note = new DataTable();
                note.Columns.Add("Info", typeof(string));
                note.Rows.Add("No gender column found in patients table. Gender stats skipped.");
            }
        }



        #endregion

        #region 5) Doctor Workload (chartDoctorWorkload + dgvDoctorWorkload)

        //private void LoadDoctorWorkload()
        //{
        //    const string sql = @"
        //        SELECT 
        //            COALESCE(TRIM(doctor_name), CONCAT('Dr ID ', IFNULL(doctor_id,'Unknown'))) AS Doctor,
        //            COUNT(*) AS Consultations
        //        FROM consultation
        //        WHERE YEAR(consultation_date) = YEAR(CURDATE())
        //        GROUP BY COALESCE(TRIM(doctor_name), CONCAT('Dr ID ', IFNULL(doctor_id,'Unknown')))
        //        ORDER BY Consultations DESC
        //        LIMIT 50;
        //    ";

        //    var dt = QueryToTable(sql);
        //    dgvDoctorWorkload.DataSource = dt;
        //    FormatGrid(dgvDoctorWorkload);
        //    RenderBar(chartDoctorWorkload, dt, "Doctor", "Consultations", "Doctor Workload");
        //}

        #endregion

        #region 6) Aggregated monthly counts (dgvAggregated)

        private void LoadAggregatedMonthlyCounts()
        {
            const string sql = @"
                SELECT 
                    m.MonthNumber,
                    m.MonthName,
                    SUM(CASE WHEN c.ear_exam IS NOT NULL AND TRIM(c.ear_exam) <> '' THEN 1 ELSE 0 END) AS EarCount,
                    SUM(CASE WHEN c.nose_exam IS NOT NULL AND TRIM(c.nose_exam) <> '' THEN 1 ELSE 0 END) AS NoseCount,
                    SUM(CASE WHEN c.throat_exam IS NOT NULL AND TRIM(c.throat_exam) <> '' THEN 1 ELSE 0 END) AS ThroatCount,
                    SUM(CASE WHEN c.others_exam IS NOT NULL AND TRIM(c.others_exam) <> '' THEN 1 ELSE 0 END) AS OthersCount,
                    COUNT(*) AS TotalConsults
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
            //dgvAggregated.DataSource = dt;
            //FormatGrid(dgvAggregated);
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
                    {
                        da.Fill(dt);
                    }
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
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                string name = col.Name.ToLower();
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

        private void RenderPie(Chart chart, DataTable dt, string labelColumn, string valueColumn, string seriesName)
        {
            try
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.Legends.Clear();

                var ca = new ChartArea("CA");
                ca.Area3DStyle.Enable3D = true;
                ca.Area3DStyle.Inclination = 35;
                ca.Area3DStyle.Rotation = 20;
                ca.Area3DStyle.PointDepth = 30;
                chart.ChartAreas.Add(ca);

                var legend = new Legend("L") { Docking = Docking.Bottom, LegendStyle = LegendStyle.Table };
                chart.Legends.Add(legend);

                var s = new Series(seriesName)
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    ChartArea = "CA"
                };
                s["PieLabelStyle"] = "Outside";
                s.Label = "#PERCENT{P1}\n#VAL{N0}";
                s.ToolTip = "#AXISLABEL: #VAL{N0}";
                chart.Series.Add(s);

                if (dt != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string lbl = r[labelColumn]?.ToString() ?? "Unknown";
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
                    var pt = s.Points[0];
                    pt.AxisLabel = "No data";
                    pt.LegendText = "No data";
                }

                chart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pie chart error: " + ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderBar(Chart chart, DataTable dt, string labelColumn, string valueColumn, string title)
        {
            try
            {
                chart.Series.Clear();
                chart.ChartAreas.Clear();
                chart.Legends.Clear();

                var ca = new ChartArea("CA");
                ca.AxisX.LabelStyle.Angle = -45;
                ca.AxisY.MajorGrid.LineColor = Color.LightGray;
                ca.AxisX.MajorGrid.LineColor = Color.LightGray;
                chart.ChartAreas.Add(ca);

                var legend = new Legend("L") { Docking = Docking.Top };
                chart.Legends.Add(legend);

                var s = new Series(title)
                {
                    ChartType = SeriesChartType.Column,
                    ChartArea = "CA",
                    IsValueShownAsLabel = true
                };
                s.Label = "#VAL{N0}";
                chart.Series.Add(s);

                Color[] colors = new Color[]
                {
                    Color.CornflowerBlue,
                    Color.Orange,
                    Color.MediumSeaGreen,
                    Color.Salmon,
                    Color.MediumPurple,
                    Color.Goldenrod,
                    Color.Tomato
                };

                int colorIndex = 0;

                if (dt != null)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string lbl = r[labelColumn]?.ToString() ?? "";
                        long val = 0;
                        try { val = Convert.ToInt64(r[valueColumn]); } catch { val = 0; }

                        var idx = s.Points.AddY(val);
                        s.Points[idx].AxisLabel = lbl;
                        s.Points[idx].LegendText = $"{lbl} ({val:N0})";
                        s.Points[idx].Color = colors[colorIndex % colors.Length];
                        colorIndex++;
                    }
                }

                chart.Titles.Clear();
                chart.Titles.Add(new Title(title, Docking.Top, new Font("Segoe UI", 12, FontStyle.Bold), Color.FromArgb(40, 40, 40)));

                chart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bar chart error: " + ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    int cnt = Convert.ToInt32(dt.Rows[0]["Cnt"]);
                    return cnt > 0;
                }
            }
            catch
            {
                // ignore
            }
            return false;
        }

        #endregion

        #region Export CSV & active grid

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            DataGridView activeGrid = GetActiveGrid();
            if (activeGrid == null || activeGrid.DataSource == null)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog { Filter = "CSV file|*.csv", FileName = "ent_export.csv" })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var dt = (DataTable)activeGrid.DataSource;
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
            var tab = tabControl.SelectedTab;
            if (tab == null) return null;

            switch (tab.Name)
            {
                case "tabMonthly": return dgvEnt;
                case "tabDaily": return dgvDaily;
                //case "tabTopDiagnoses": return dgvDiagnoses;
                case "tabPatientStats": return dgvPatientStats;
                //case "tabAggregated": return dgvAggregated;
                default: return null;
            }
        }

        #endregion

        #region Chart click handler stub

        private void ChartEnt_MouseClick(object sender, MouseEventArgs e)
        {
            // placeholder: optionally show detail list for clicked month/series
            // Can implement as needed
        }

        #endregion

        // keep designer event stub
        private void dgvService_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void splitPatientStats_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
