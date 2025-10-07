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

        private ContextMenuStrip calendarContextMenu;
        private DateTime? rightClickedDate = null;

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
            CreateWeekdayHeaders();

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
            tableLayoutCalendar.ColumnStyles.Clear();
            tableLayoutCalendar.RowStyles.Clear();

            // 7 columns (equal percent width)
            for (int i = 0; i < 7; i++)
            {
                tableLayoutCalendar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7f));
            }

            // 1 row for weekdays header (fixed small height)
            tableLayoutCalendar.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));

            // 6 rows for days
            for (int i = 0; i < 6; i++)
            {
                tableLayoutCalendar.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6f));
            }
        }
        private void CreateWeekdayHeaders()
        {
            string[] weekDays = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

            for (int i = 0; i < 7; i++)
            {
                var lbl = new Label
                {
                    Text = weekDays[i],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    BackColor = (i == 0) ? Color.LightCoral : Color.LightGray, // Sunday = redish
                    ForeColor = Color.Black,
                    Margin = new Padding(0)
                };

                tableLayoutCalendar.Controls.Add(lbl, i, 0); // add to header row (row 0)
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

                    panel.Click += DayCell_Click;

                    // 👇 ADD THIS LINE
                    panel.DoubleClick += DayCell_DoubleClick;

                    this.tableLayoutCalendar.Controls.Add(panel, c, r + 1);
                    dayPanels[r * 7 + c] = panel;
                }
            }
        }
        /// <summary>
        /// Double-click handler — opens AddAppointmentForm for that specific date.
        /// </summary>
        private void DayCell_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Panel panel = null;
                if (sender is Panel p) panel = p;
                else if (sender is Control c && c.Parent is Panel parentPanel) panel = parentPanel;

                if (panel == null || panel.Tag == null) return;

                if (panel.Tag is DateTime date)
                {
                    // Open AddAppointmentForm, passing the selected date
                    using (var form = new ENT_Clinic_System.InsertForms.AddAppointmentForm(date))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            // After saving, refresh the calendar
                            RenderCalendar();
                            LoadAppointmentsIntoGrid(date);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("DayCell_DoubleClick error: " + ex);
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
                            // Make Sundays red
                            if (col == 0) // Sunday column
                            {
                                lblDay.ForeColor = Color.Red;
                            }
                            lblDay.Click += (s, ev) => DayCell_Click(dayCell, EventArgs.Empty);
                            dayCell.Controls.Add(lblDay);

                            // Appointment count (only if > 0)
                            if (appointmentsByDate.TryGetValue(cellDate.Date, out var rows) && rows.Count > 0)
                            {
                                var lblCount = new Label
                                {
                                    Text = $"{rows.Count} Appointment/s",
                                    AutoSize = false,
                                    Dock = DockStyle.Bottom,
                                    Font = apptFont,
                                    ForeColor = Color.Yellow,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Cursor = Cursors.Hand,
                                    BackColor = Color.SteelBlue

                                };
                                lblCount.Click += (s, ev) => DayCell_Click(dayCell, EventArgs.Empty);
                                dayCell.Controls.Add(lblCount);
                            }

                            // Highlight today
                            if (cellDate.Date == DateTime.Today)
                            {
                                dayCell.BackColor = Color.AntiqueWhite;
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
        /// Loads all appointments for the given month from the 'appointments' table.
        /// Groups results by follow_up_date.
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
    SELECT 
        a.follow_up_id,
        a.patient_id,
        COALESCE(p.full_name, 'Clinic Appointment') AS patient_name,
        a.follow_up_date,
        a.note
    FROM appointments a
    LEFT JOIN patients p ON a.patient_id = p.patient_id
    WHERE DATE(a.follow_up_date) BETWEEN @start AND @end
    ORDER BY a.follow_up_date, patient_name;
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

                // Group results by date
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["follow_up_date"] == DBNull.Value) continue;
                    if (!DateTime.TryParse(dr["follow_up_date"].ToString(), out var fullDt)) continue;

                    DateTime key = fullDt.Date;
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
            apointmentDateLabel.Text = date.ToString("MMMM dd, yyyy");

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
            // Hide internal columns
            if (dgvAppointments.Columns.Contains("follow_up_id"))
                dgvAppointments.Columns["follow_up_id"].Visible = false;

            if (dgvAppointments.Columns.Contains("patient_id"))
                dgvAppointments.Columns["patient_id"].Visible = false;

            // User-friendly headers
            if (dgvAppointments.Columns.Contains("patient_name"))
                dgvAppointments.Columns["patient_name"].HeaderText = "Name";

            if (dgvAppointments.Columns.Contains("follow_up_date"))
                dgvAppointments.Columns["follow_up_date"].HeaderText = "Follow-up Date";

            if (dgvAppointments.Columns.Contains("note"))
                dgvAppointments.Columns["note"].HeaderText = "Notes";
            foreach (DataGridViewRow row in dgvAppointments.Rows)
            {
                var patientName = row.Cells["patient_name"].Value?.ToString();
                if (patientName == "(Custom Appointment)")
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }


        }

        /// <summary>
        /// Fallback: fetch appointments for a single date directly from the 'appointments' table.
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
        a.follow_up_id,
        a.patient_id,
        COALESCE(p.full_name, '(Custom Appointment)') AS patient_name,
        a.follow_up_date,
        a.note
    FROM appointments a
    LEFT JOIN patients p ON a.patient_id = p.patient_id
    WHERE DATE(a.follow_up_date) = DATE(@date)
    ORDER BY patient_name;
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
            try
            {
                if (dgvAppointments.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an appointment first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get patient_id value safely
                var patientIdObj = dgvAppointments.SelectedRows[0].Cells["patient_id"].Value;

                if (patientIdObj == DBNull.Value || patientIdObj == null || string.IsNullOrWhiteSpace(patientIdObj.ToString()))
                {
                    MessageBox.Show("This appointment does not have a linked patient record.",
                                    "No Patient Linked",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                // Convert safely
                if (int.TryParse(patientIdObj.ToString(), out int patientId))
                {
                    // Open consultation form for that patient
                    ConsultationControl consultation = new ConsultationControl(patientId);
                    consultation.Show();
                }
                else
                {
                    MessageBox.Show("Invalid patient ID found in the selected appointment.",
                                    "Invalid Data",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to view the consultation:\n" + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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

        private void todayButton_Click(object sender, EventArgs e)
        {
            // Reset to today’s date
            currentMonth = DateTime.Today;

            // Re-render the calendar
            RenderCalendar();

            // Also load today's appointments in the grid
            LoadAppointmentsIntoGrid(DateTime.Today);
        }
    }
}
