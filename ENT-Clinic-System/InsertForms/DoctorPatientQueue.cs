using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.InsertForms
{
    public partial class DoctorPatientQueue : Form
    {
        private DataTable queueTable;
        private bool suppressEvents = false;

        public DoctorPatientQueue()
        {
            InitializeComponent();
            dgvQueue.AutoGenerateColumns = true;

            // Subscribe to styling & events
            dgvQueue.CurrentCellDirtyStateChanged += dgvQueue_CurrentCellDirtyStateChanged;
            dgvQueue.CellValueChanged += dgvQueue_CellValueChanged;
        }

        private void DoctorPatientQueue_Load(object sender, EventArgs e)
        {
            LoadQueue();

            // UI tweaks
            dgvQueue.DefaultCellStyle.SelectionBackColor = dgvQueue.DefaultCellStyle.BackColor;
            dgvQueue.DefaultCellStyle.SelectionForeColor = dgvQueue.DefaultCellStyle.ForeColor;
            dgvQueue.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvQueue.RowHeadersVisible = false;
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
                            q.queue_number,
                            p.full_name AS patient_name,
                            q.status
                        FROM queue q
                        INNER JOIN patients p ON q.patient_id = p.patient_id
                        WHERE DATE(q.created_at) = CURDATE()
                        ORDER BY
                            CASE q.status
                                WHEN 'examining' THEN 1
                                WHEN 'waiting' THEN 2
                                WHEN 'done' THEN 3
                                WHEN 'skipped' THEN 4
                                ELSE 5
                            END,
                            q.queue_number ASC
                    ";

                    using (var adapter = new MySqlDataAdapter(sql, conn))
                    {
                        queueTable = new DataTable();
                        adapter.Fill(queueTable);
                        dgvQueue.DataSource = queueTable;
                    }
                }

                SetupStatusColumn();

                // Hide technical ID column
                if (dgvQueue.Columns.Contains("queue_id"))
                    dgvQueue.Columns["queue_id"].Visible = false;

                // Professional headers
                if (dgvQueue.Columns.Contains("queue_number"))
                    dgvQueue.Columns["queue_number"].HeaderText = "Queue #";
                if (dgvQueue.Columns.Contains("patient_name"))
                    dgvQueue.Columns["patient_name"].HeaderText = "Patient Name";
                if (dgvQueue.Columns.Contains("status"))
                    dgvQueue.Columns["status"].HeaderText = "Current Status";

                // Read-only except status
                foreach (DataGridViewColumn col in dgvQueue.Columns)
                {
                    col.ReadOnly = col.Name != "status";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load queue: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                suppressEvents = false;
            }
        }

        private void SetupStatusColumn()
        {
            if (!dgvQueue.Columns.Contains("status"))
                return;

            if (dgvQueue.Columns["status"] is DataGridViewComboBoxColumn) return;

            int statusColIndex = dgvQueue.Columns["status"].Index;
            dgvQueue.Columns.Remove("status");

            var combo = new DataGridViewComboBoxColumn
            {
                Name = "status",
                HeaderText = "Current Status",
                DataPropertyName = "status",
                ValueType = typeof(string),
                DataSource = new string[] { "examining", "waiting", "done", "skipped" },
                FlatStyle = FlatStyle.Standard
            };

            dgvQueue.Columns.Insert(statusColIndex, combo);
        }

        private void dgvQueue_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (suppressEvents) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var col = dgvQueue.Columns[e.ColumnIndex];
            if (col.Name != "status") return;

            try
            {
                var row = dgvQueue.Rows[e.RowIndex];
                int queueId = Convert.ToInt32(row.Cells["queue_id"].Value);
                string newStatus = row.Cells["status"].Value.ToString();

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string sql;
                    if (newStatus == "examining")
                        sql = "UPDATE queue SET status=@status, called_at = NOW() WHERE queue_id=@id";
                    else if (newStatus == "done")
                        sql = "UPDATE queue SET status=@status, finished_at = NOW() WHERE queue_id=@id";
                    else
                        sql = "UPDATE queue SET status=@status WHERE queue_id=@id";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", newStatus);
                        cmd.Parameters.AddWithValue("@id", queueId);
                        cmd.ExecuteNonQuery();
                    }
                }

                // ✅ Refresh order after status change
                ReorderQueue();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reorder DataTable based on status rules instead of manually clearing rows.
        /// </summary>
        private void ReorderQueue()
        {
            if (queueTable == null) return;

            var orderedRows = queueTable.AsEnumerable()
                .OrderBy(r => GetStatusRank(r.Field<string>("status")))
                .ThenBy(r => r.Field<int>("queue_number"));

            DataTable newTable = queueTable.Clone(); // same schema
            foreach (var row in orderedRows)
                newTable.ImportRow(row);

            suppressEvents = true;
            dgvQueue.DataSource = newTable;
            queueTable = newTable;
            suppressEvents = false;
            LoadQueue();

        }

        private int GetStatusRank(string status)
        {
            switch (status)
            {
                case "examining": return 1;
                case "waiting": return 2;
                case "done": return 3;
                case "skipped": return 4;
                default: return 5;
            }
        }

        private void dgvQueue_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvQueue.IsCurrentCellDirty &&
                dgvQueue.CurrentCell is DataGridViewComboBoxCell)
            {
                dgvQueue.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// Apply row colors depending on the status value.
        /// </summary>
        private void dgvQueue_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvQueue.Columns[e.ColumnIndex].Name == "status" && e.RowIndex >= 0)
            {
                var cell = dgvQueue.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string status = cell.Value?.ToString();

                switch (status)
                {
                    case "examining":
                        cell.Style.ForeColor = Color.DarkBlue;
                        cell.Style.BackColor = Color.LightBlue;
                        break;
                    case "waiting":
                        cell.Style.ForeColor = Color.DarkGoldenrod;
                        cell.Style.BackColor = Color.LightYellow;
                        break;
                    case "done":
                        cell.Style.ForeColor = Color.White;
                        cell.Style.BackColor = Color.Green;
                        break;
                    case "skipped":
                        cell.Style.ForeColor = Color.White;
                        cell.Style.BackColor = Color.Red;
                        break;
                    default:
                        cell.Style.ForeColor = dgvQueue.DefaultCellStyle.ForeColor;
                        cell.Style.BackColor = dgvQueue.DefaultCellStyle.BackColor;
                        break;
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadQueue();
        }

        private void refreshButton_Click_1(object sender, EventArgs e)
        {
            LoadQueue();
        }
    }
}
