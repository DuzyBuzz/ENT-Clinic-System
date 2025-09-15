using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.UserControls
{
    public partial class AppointmentsUserControl : UserControl
    {
        // current displayed month (any date inside that month)
        private DateTime currentMonth;

        // Cache: appointments for the current month grouped by date (date -> list of DataRow)
        private Dictionary<DateTime, List<DataRow>> appointmentsByDate;

        // Panels (6 rows x 7 cols = 42)
        private Panel[] dayPanels;

        // Reused fonts (avoid recreating on each render)
        private readonly Font dayFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        private readonly Font apptFont = new Font("Segoe UI", 8F, FontStyle.Regular);

        public AppointmentsUserControl()
        {
            InitializeComponent();

            // Make control double-buffered to reduce flicker
            this.DoubleBuffered = true;
            // Improve TableLayoutPanel drawing (non-public property)
            typeof(TableLayoutPanel).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?
                .SetValue(tableLayoutCalendar, true, null);

            currentMonth = DateTime.Today;
            appointmentsByDate = new Dictionary<DateTime, List<DataRow>>();

            // Configure columns and rows for the calendar at runtime (designer remains clean)
            ConfigureTableLayout();

            // Create 42 day panels (at runtime) and add them to the table layout
            CreateDayPanels();

            // Render the initial calendar
            // Note: Load event will also call RenderCalendar, but it's safe to call here as well
            // to show the initial view if user constructs the control programmatically.
            RenderCalendar();
        }

        /// <summary>
        /// Sets up the TableLayoutPanel column/row styles (7 columns x 6 rows).
        /// This is done at runtime to avoid loops inside InitializeComponent (designer compatibility).
        /// </summary>
        private void ConfigureTableLayout()
        {
            // Clear any existing styles (designer might have created default entries)
            tableLayoutCalendar.ColumnStyles.Clear();
            tableLayoutCalendar.RowStyles.Clear();

            // 7 columns (equal percent)
            for (int i = 0; i < 7; i++)
            {
                tableLayoutCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7f));
            }

            // 6 rows (equal percent)
            for (int i = 0; i < 6; i++)
            {
                tableLayoutCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6f));
            }
        }

        /// <summary>
        /// Creates 42 panel controls and adds them to the TableLayoutPanel at runtime.
        /// We create them once and reuse their contents for each month render.
        /// </summary>
        private void CreateDayPanels()
        {
            dayPanels = new Panel[42];

            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    var panel = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.None,
                        BackColor = Color.White,
                        Margin = new Padding(1),
                        Name = $"dayCell_{r}_{c}"
                    };

                    // Click should open the day's appointments
                    panel.Click += DayCell_Click;

                    // Add to table layout at column c, row r
                    this.tableLayoutCalendar.Controls.Add(panel, c, r);

                    dayPanels[r * 7 + c] = panel;
                }
            }
        }

        private void AppointmentsUserControl_Load(object sender, EventArgs e)
        {
            // Ensure calendar is rendered on load
            RenderCalendar();
        }

        private void BtnPrevMonth_Click(object sender, EventArgs e)
        {
            currentMonth = currentMonth.AddMonths(-1);
            RenderCalendar();
        }

        private void BtnNextMonth_Click(object sender, EventArgs e)
        {
            currentMonth = currentMonth.AddMonths(1);
            RenderCalendar();
        }

        /// <summary>
        /// Render calendar for currentMonth using cached dayPanels.
        /// Optimizations:
        /// - Loads all appointments for the month in a single DB call.
        /// - Reuses the same 42 panels (no recreation) and only updates their child controls.
        /// - Suspends layout while updating to avoid thrash.
        /// </summary>
        private void RenderCalendar()
        {
            if (dayPanels == null) return;

            this.SuspendLayout();
            tableLayoutCalendar.SuspendLayout();

            try
            {
                lblMonthYear.Text = currentMonth.ToString("MMMM yyyy");

                // Load all appointments for the current month into dictionary (1 DB call)
                LoadAppointmentsForMonth(currentMonth);

                DateTime firstDayOfMonth = new DateTime(currentMonth.Year, currentMonth.Month, 1);
                int daysInMonth = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
                int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek; // Sunday = 0

                int day = 1;
                int idx = 0;

                for (int row = 0; row < 6; row++)
                {
                    for (int col = 0; col < 7; col++, idx++)
                    {
                        Panel dayCell = dayPanels[idx];

                        // Clear old controls
                        dayCell.Controls.Clear();
                        dayCell.BackColor = Color.White;
                        dayCell.Tag = null;

                        if (row == 0 && col < startDayOfWeek)
                        {
                            // Empty cell (before month starts)
                            continue;
                        }
                        else if (day <= daysInMonth)
                        {
                            DateTime cellDate = new DateTime(currentMonth.Year, currentMonth.Month, day);

                            // Day number (always shown at top)
                            var lblDay = new Label
                            {
                                Text = day.ToString(),
                                AutoSize = false,
                                Dock = DockStyle.Top,
                                Font = dayFont,
                                Height = 22,
                                Padding = new Padding(4, 2, 0, 0),
                                TextAlign = ContentAlignment.MiddleLeft,
                                Cursor = Cursors.Hand
                            };
                            lblDay.Click += (s, ev) => DayCell_Click(dayCell, EventArgs.Empty);
                            dayCell.Controls.Add(lblDay);

                            // Appointment count (only if > 0)
                            if (appointmentsByDate.TryGetValue(cellDate.Date, out var rows) && rows.Count > 0)
                            {
                                var lblCount = new Label
                                {
                                    Text = $"{rows.Count} Appointment/s",
                                    AutoSize = false,
                                    Dock = DockStyle.Fill,
                                    Font = apptFont,
                                    ForeColor = Color.Red,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Cursor = Cursors.Hand
                                };
                                lblCount.Click += (s, ev) => DayCell_Click(dayCell, EventArgs.Empty);
                                dayCell.Controls.Add(lblCount);
                            }

                            // Highlight today
                            if (cellDate.Date == DateTime.Today)
                            {
                                dayCell.BackColor = Color.FromArgb(245, 250, 255);
                            }

                            // Store the date in the panel tag for click handler
                            dayCell.Tag = cellDate.Date;

                            day++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("RenderCalendar error: " + ex);
            }
            finally
            {
                tableLayoutCalendar.ResumeLayout();
                this.ResumeLayout();
            }
        }


        /// <summary>
        /// Loads all appointments for the month into appointmentsByDate (single DB call).
        /// </summary>
        private void LoadAppointmentsForMonth(DateTime anyDateInMonth)
        {
            appointmentsByDate.Clear();

            DateTime startOfMonth = new DateTime(anyDateInMonth.Year, anyDateInMonth.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            DataTable dt = new DataTable();
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT c.consultation_id, c.patient_id, p.full_name AS patient_name, c.follow_up_date, c.notes
                        FROM consultation c
                        INNER JOIN patients p ON c.patient_id = p.patient_id
                        WHERE DATE(c.follow_up_date) BETWEEN @start AND @end
                        ORDER BY c.follow_up_date, p.full_name;
                    ";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@start", startOfMonth.Date);
                        cmd.Parameters.AddWithValue("@end", endOfMonth.Date);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }

                // Group by date
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["follow_up_date"] == DBNull.Value) continue;
                    if (!DateTime.TryParse(dr["follow_up_date"].ToString(), out var fullDt)) continue;

                    var key = fullDt.Date;
                    if (!appointmentsByDate.TryGetValue(key, out var list))
                    {
                        list = new List<DataRow>();
                        appointmentsByDate[key] = list;
                    }
                    list.Add(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadAppointmentsForMonth error: " + ex);
            }
        }

        /// <summary>
        /// Click handler for day cells. The panel's Tag contains the DateTime (or null).
        /// </summary>
        private void DayCell_Click(object sender, EventArgs e)
        {
            try
            {
                Panel panel = null;
                if (sender is Panel p) panel = p;
                else if (sender is Control c && c.Parent is Panel parentPanel) panel = parentPanel;

                if (panel == null) return;

                if (panel.Tag is DateTime date)
                {
                    LoadAppointmentsIntoGrid(date);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("DayCell_Click error: " + ex);
            }
        }

        /// <summary>
        /// Loads appointments for a specific date into the DataGridView.
        /// Prefer cached values (appointmentsByDate); fallback to DB query if not present.
        /// </summary>
        private void LoadAppointmentsIntoGrid(DateTime date)
        {
            DataTable dt = new DataTable();

            if (appointmentsByDate.TryGetValue(date.Date, out var rows) && rows.Count > 0)
            {
                // Create a table clone and import the rows
                dt = rows[0].Table.Clone();
                foreach (var r in rows) dt.ImportRow(r);
            }
            else
            {
                // fallback single-date query
                dt = GetAppointmentsForDate(date);
            }

            dgvAppointments.DataSource = dt;

            // Hide ID columns (they’re just for internal use)
            if (dgvAppointments.Columns.Contains("consultation_id"))
                dgvAppointments.Columns["consultation_id"].Visible = false;

            if (dgvAppointments.Columns.Contains("patient_id"))
                dgvAppointments.Columns["patient_id"].Visible = false;

            // User-friendly headers
            if (dgvAppointments.Columns.Contains("patient_name"))
                dgvAppointments.Columns["patient_name"].HeaderText = "Patient Name";

            if (dgvAppointments.Columns.Contains("follow_up_date"))
                dgvAppointments.Columns["follow_up_date"].HeaderText = "Follow-up Date";

            if (dgvAppointments.Columns.Contains("notes"))
                dgvAppointments.Columns["notes"].HeaderText = "Notes";
        }

        /// <summary>
        /// Fallback: fetch appointments for a single date from DB
        /// </summary>
        private DataTable GetAppointmentsForDate(DateTime date)
        {
            var dt = new DataTable();
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            c.consultation_id,
                            c.patient_id,
                            c.patient_id,
                            p.full_name AS patient_name,
                            c.follow_up_date,
                            c.notes
                        FROM consultation c
                        JOIN patient p ON c.patient_id = p.patient_id
                        WHERE DATE(c.follow_up_date) = DATE(@date)

                    ";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@date", date.Date);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GetAppointmentsForDate error: " + ex);
            }
            return dt;
        }
        private void viewConsultationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count > 0)
            {
                int patientId = Convert.ToInt32(
                    dgvAppointments.SelectedRows[0].Cells["patient_id"].Value
                );

                var viewControl = new ConsultationControl(patientId);
                LoadUserControl(viewControl);
            }
        }


        private void LoadUserControl(UserControl control)
        {
            this.Controls.Clear(); // Or replace "this" with your mainPanel
            control.Dock = DockStyle.Fill;
            this.Controls.Add(control);
        }
        private void dgvAppointments_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvAppointments.ClearSelection();
                dgvAppointments.Rows[e.RowIndex].Selected = true;
            }
        }

    }
}
