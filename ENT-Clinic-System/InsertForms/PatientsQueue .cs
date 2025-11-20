using AForge.Imaging.Filters;
using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.UserControls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.InsertForms
{
    public partial class PatientsQueue : Form
    {
        private DataTable patientsTable;
        private DataTable queueTable;
        private bool suppressEvents = false;
        private object oldCellValue = null;
        // Realtime watcher
        private TableChangeWatcher queueWatcher;

        public PatientsQueue()
        {
            InitializeComponent();

            dgvQueue.AutoGenerateColumns = true;

            // ✅ Add Context Menu for right click
            var contextMenu = new ContextMenuStrip();
            dgvQueue.ContextMenuStrip = contextMenu;

            // Subscribe events
            dgvQueue.CurrentCellDirtyStateChanged += dgvQueue_CurrentCellDirtyStateChanged;
            dgvQueue.CellValueChanged += dgvQueue_CellValueChanged;
            dgvQueue.MouseDown += dgvQueue_MouseDown;

            // Ensure watcher cleaned up on close
            this.FormClosing += PatientsQueue_FormClosing;
        }

        private void PatientsQueue_Load(object sender, EventArgs e)
        {
            LoadPatients();
            LoadQueue();
            AutoCompleteHelper.SetupAutoComplete(
                txtSearchPatient,
                "patients",
                new List<string> { "full_name" }
            );

            // Start the realtime watcher after initial load
            queueWatcher = new TableChangeWatcher(new[] { "queue" }, LoadQueue);
            queueWatcher.Start();
        }

        private void LoadPatients()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string sql = "SELECT patient_id, full_name, referred_by FROM patients";
                var adapter = new MySqlDataAdapter(sql, conn);
                patientsTable = new DataTable();
                adapter.Fill(patientsTable);
                dgvPatients.DataSource = patientsTable;
            }

            if (dgvPatients.Columns.Contains("patient_id"))
            {
                dgvPatients.Columns["patient_id"].HeaderText = "Patient ID";
                dgvPatients.Columns["patient_id"].Visible = false;
            }

            if (dgvPatients.Columns.Contains("full_name"))
                dgvPatients.Columns["full_name"].HeaderText = "Full Name";

            if (dgvPatients.Columns.Contains("referred_by"))
                dgvPatients.Columns["referred_by"].HeaderText = "Referred By";
        }

        private void LoadQueue()
        {
            try
            {
                suppressEvents = true;
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                SELECT 
                    q.queue_id,
                    q.patient_id,
                    q.queue_number,
                    p.full_name AS patient_name,
                    p.patient_contact_number,
                    p.emergency_contact_number,
                    q.status,
                    q.created_at,
                    q.finished_at
                FROM queue q
                INNER JOIN patients p ON q.patient_id = p.patient_id
                WHERE DATE(q.created_at) = CURDATE()
                ORDER BY
                    CASE q.status
                        WHEN 'Examining' THEN 1
                        WHEN 'Waiting' THEN 2
                        WHEN 'Done' THEN 3
                        WHEN 'Skipped' THEN 4
                        WHEN 'Cancelled' THEN 5
                    END,
                    q.queue_number ASC";

                    var adapter = new MySqlDataAdapter(sql, conn);
                    queueTable = new DataTable();
                    adapter.Fill(queueTable);
                    dgvQueue.DataSource = queueTable;
                }

                SetupStatusColumn();

                // Hide unneeded technical columns
                if (dgvQueue.Columns.Contains("queue_id"))
                    dgvQueue.Columns["queue_id"].Visible = false;

                if (dgvQueue.Columns.Contains("patient_id"))
                    dgvQueue.Columns["patient_id"].Visible = false;

                // ✅ Setup readable column headers
                if (dgvQueue.Columns.Contains("queue_number"))
                    dgvQueue.Columns["queue_number"].HeaderText = "Queue #";

                if (dgvQueue.Columns.Contains("patient_name"))
                    dgvQueue.Columns["patient_name"].HeaderText = "Patient Name";

                if (dgvQueue.Columns.Contains("patient_contact_number"))
                    dgvQueue.Columns["patient_contact_number"].HeaderText = "Contact Number";

                if (dgvQueue.Columns.Contains("created_at"))
                {
                    dgvQueue.Columns["created_at"].HeaderText = "Queued At";
                    dgvQueue.Columns["created_at"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss"; // ✅ Format date/time
                }
                if (dgvQueue.Columns.Contains("created_at"))
                {
                    dgvQueue.Columns["finished_at"].HeaderText = "Finished Time";
                    dgvQueue.Columns["finished_at"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss"; // ✅ Format date/time
                }

                if (dgvQueue.Columns.Contains("status"))
                    dgvQueue.Columns["status"].HeaderText = "Current Status";

                if (dgvQueue.Columns.Contains("emergency_contact_number"))
                    dgvQueue.Columns["emergency_contact_number"].HeaderText = "Emergency Contact Number";

                foreach (DataGridViewColumn col in dgvQueue.Columns)
                {
                    // Allow editing for "status" and "queue_number" only
                    col.ReadOnly = !(col.Name == "status" || col.Name == "queue_number");
                }

            }
            finally
            {
                suppressEvents = false;
            }
        }


        private void SetupStatusColumn()
        {
            if (!dgvQueue.Columns.Contains("status")) return;

            // Prevent re-creating if it’s already a combo column
            if (dgvQueue.Columns["status"] is DataGridViewComboBoxColumn) return;

            int idx = dgvQueue.Columns["status"].Index;
            dgvQueue.Columns.Remove("status");

            // Create combo column
            var combo = new DataGridViewComboBoxColumn
            {
                Name = "status",
                HeaderText = "Current Status",
                DataPropertyName = "status",
                ValueType = typeof(string),
                DataSource = new string[] { "Examining", "Waiting", "Done", "Skipped", "Cancelled" },
                FlatStyle = FlatStyle.Standard
            };

            dgvQueue.Columns.Insert(idx, combo);

            // ✅ Handle coloring when data is shown or status changes
            dgvQueue.CellFormatting -= dgvQueue_CellFormatting;
            dgvQueue.CellFormatting += dgvQueue_CellFormatting;

            dgvQueue.CurrentCellDirtyStateChanged -= dgvQueue_CurrentCellDirtyStateChanged;
            dgvQueue.CurrentCellDirtyStateChanged += dgvQueue_CurrentCellDirtyStateChanged;

            dgvQueue.CellValueChanged -= dgvQueue_CellValueChanged;
            dgvQueue.CellValueChanged += dgvQueue_CellValueChanged;
        }

        private void dgvQueue_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (suppressEvents || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columnName = dgvQueue.Columns[e.ColumnIndex].Name;
            if (columnName != "status" && columnName != "queue_number") return;

            var row = dgvQueue.Rows[e.RowIndex];
            int queueId = Convert.ToInt32(row.Cells["queue_id"].Value);

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string sql = "";
                    MySqlCommand cmd = null;

                    // ✅ Handle Status column
                    if (columnName == "status")
                    {
                        string newStatus = row.Cells["status"].Value?.ToString() ?? "";

                        if (newStatus.Equals("Examining", StringComparison.OrdinalIgnoreCase))
                            sql = "UPDATE queue SET status=@status, called_at=NOW() WHERE queue_id=@id";
                        else if (newStatus.Equals("Done", StringComparison.OrdinalIgnoreCase))
                            sql = "UPDATE queue SET status=@status, finished_at=NOW() WHERE queue_id=@id";
                        else
                            sql = "UPDATE queue SET status=@status WHERE queue_id=@id";

                        cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@status", newStatus);
                        cmd.Parameters.AddWithValue("@id", queueId);
                    }

                    // ✅ Handle Queue Number column
                    else if (columnName == "queue_number")
                    {
                        string value = row.Cells["queue_number"].Value?.ToString() ?? "";

                        // Empty?
                        if (string.IsNullOrWhiteSpace(value))
                        {

                        }

                        // Not numeric?
                        if (!int.TryParse(value, out int newNumber))
                        {

                        }

                        // ✅ Update DB if valid
                        sql = "UPDATE queue SET queue_number=@num WHERE queue_id=@id";
                        cmd = new MySqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@num", newNumber);
                        cmd.Parameters.AddWithValue("@id", queueId);
                    }

                    if (cmd != null)
                        cmd.ExecuteNonQuery();
                }

                // ✅ Reload data to reflect update
                LoadQueue();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update queue: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dgvQueue_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvQueue.IsCurrentCellDirty && dgvQueue.CurrentCell is DataGridViewComboBoxCell)
            {
                dgvQueue.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnSearchPatient_Click(object sender, EventArgs e)
        {
            LoadPatients();
        }

        private void dgvPatients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) AddPatientToQueue();
        }

        private void btnAddToQueue_Click(object sender, EventArgs e)
        {
            AddPatientToQueue();
        }

        private void AddPatientToQueue()
        {
            if (dgvPatients.SelectedRows.Count == 0) return;

            int patientId = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells["patient_id"].Value);

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                string checkSql = "SELECT COUNT(*) FROM queue WHERE patient_id=@pid AND DATE(created_at)=CURDATE()";
                using (var checkCmd = new MySqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@pid", patientId);
                    int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (exists > 0)
                    {
                        MessageBox.Show("This patient is already in the queue for today.",
                                        "Duplicate Entry",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        return;
                    }
                }

                int nextQueueNum;
                using (var cmd = new MySqlCommand("SELECT IFNULL(MAX(queue_number),0)+1 FROM queue WHERE DATE(created_at)=CURDATE()", conn))
                {
                    nextQueueNum = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string sql = "INSERT INTO queue (patient_id, queue_number, status, created_at) VALUES (@pid, @qnum, 'waiting', NOW())";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", patientId);
                    cmd.Parameters.AddWithValue("@qnum", nextQueueNum);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadQueue();
        }

        private void btnRemoveFromQueue_Click(object sender, EventArgs e)
        {
            if (dgvQueue.SelectedRows.Count == 0) return;
            int queueId = Convert.ToInt32(dgvQueue.SelectedRows[0].Cells["queue_id"].Value);

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM queue WHERE queue_id=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", queueId);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadQueue();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadQueue();
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            if (patientsTable == null || patientsTable.Rows.Count == 0)
                return;

            try
            {
                // Escape special characters for LIKE (e.g., ', %, [, ])
                string searchText = txtSearchPatient.Text.Replace("'", "''")
                                                         .Replace("[", "[[]")
                                                         .Replace("%", "[%]")
                                                         .Replace("*", "[*]");

                // Apply filter in real-time
                patientsTable.DefaultView.RowFilter = $"full_name LIKE '%{searchText}%'";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering patients: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // ✅ Handles row selection when right-clicking
        private void dgvQueue_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTest = dgvQueue.HitTest(e.X, e.Y);
                if (hitTest.RowIndex >= 0)
                {
                    dgvQueue.ClearSelection();
                    dgvQueue.Rows[hitTest.RowIndex].Selected = true;
                }
                else
                {
                    dgvQueue.ClearSelection();
                }
            }
        }



        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dgvQueue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PatientsQueue_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (queueWatcher != null)
            {
                queueWatcher.Stop();
                queueWatcher.Dispose();
                queueWatcher = null;
            }
        }

        private void dgvQueue_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvQueue.Columns[e.ColumnIndex].Name == "status" || dgvQueue.Columns.Contains("status"))
            {
                var row = dgvQueue.Rows[e.RowIndex];
                var statusValue = row.Cells["status"].Value?.ToString();

                if (string.IsNullOrEmpty(statusValue)) return;

                // Apply color based on status
                switch (statusValue)
                {
                    case "Examining":
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "Waiting":
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "Done":
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "Skipped":
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        break;
                    case "Cancelled":
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.White;
                        break;
                    default:
                        row.DefaultCellStyle.BackColor = dgvQueue.DefaultCellStyle.BackColor;
                        row.DefaultCellStyle.ForeColor = dgvQueue.DefaultCellStyle.ForeColor;
                        break;
                }

            }
        }

        private void dgvQueue_CurrentCellDirtyStateChanged_1(object sender, EventArgs e)
        {
            if (dgvQueue.IsCurrentCellDirty)
            {
                dgvQueue.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }

        }

        private void dgvQueue_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvQueue_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvQueue.Columns[e.ColumnIndex].Name == "queue_number")
            {
                // ✅ Store the current value before editing
                oldCellValue = dgvQueue.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        private void dgvQueue_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvQueue.CurrentCell.ColumnIndex == dgvQueue.Columns["queue_number"].Index)
            {
                TextBox tb = e.Control as TextBox;

                if (tb != null)
                {
                    // Remove previous handlers to avoid duplicate events
                    tb.KeyPress -= QueueNumber_KeyPress;
                    tb.KeyPress += QueueNumber_KeyPress;
                }
            }
        }
        private void QueueNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ✅ Allow only digits and control keys (Backspace, Delete)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-numeric input
            }
        }

    }
}