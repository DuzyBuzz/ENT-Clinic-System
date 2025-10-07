using AForge.Imaging.Filters;
using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.UserControls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.InsertForms
{
    public partial class PatientsQueue : Form
    {
        private DataTable patientsTable;
        private DataTable queueTable;
        private bool suppressEvents = false;

        public PatientsQueue()
        {
            InitializeComponent();

            dgvQueue.AutoGenerateColumns = true;

            // ✅ Add Context Menu for right click
            var contextMenu = new ContextMenuStrip();
            var viewConsultationItem = new ToolStripMenuItem("View Consultation");
            viewConsultationItem.Click += viewConsultationItem_Click;
            contextMenu.Items.Add(viewConsultationItem);
            dgvQueue.ContextMenuStrip = contextMenu;

            // Subscribe events
            dgvQueue.CurrentCellDirtyStateChanged += dgvQueue_CurrentCellDirtyStateChanged;
            dgvQueue.CellValueChanged += dgvQueue_CellValueChanged;
            dgvQueue.MouseDown += dgvQueue_MouseDown;
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
        }

        private void LoadPatients()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();
                string sql = "SELECT patient_id, full_name FROM patients";
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
                            q.patient_id,           -- ✅ Added to retrieve patient_id
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
                            q.queue_number ASC";

                    var adapter = new MySqlDataAdapter(sql, conn);
                    queueTable = new DataTable();
                    adapter.Fill(queueTable);
                    dgvQueue.DataSource = queueTable;
                }

                SetupStatusColumn();

                if (dgvQueue.Columns.Contains("queue_id"))
                    dgvQueue.Columns["queue_id"].Visible = false;

                // ✅ Hide patient_id column from UI
                if (dgvQueue.Columns.Contains("patient_id"))
                    dgvQueue.Columns["patient_id"].Visible = false;

                if (dgvQueue.Columns.Contains("queue_number"))
                    dgvQueue.Columns["queue_number"].HeaderText = "Queue #";

                if (dgvQueue.Columns.Contains("patient_name"))
                    dgvQueue.Columns["patient_name"].HeaderText = "Patient Name";

                if (dgvQueue.Columns.Contains("status"))
                    dgvQueue.Columns["status"].HeaderText = "Current Status";


                foreach (DataGridViewColumn col in dgvQueue.Columns)
                {
                    col.ReadOnly = col.Name != "status"; // only status editable
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

            var combo = new DataGridViewComboBoxColumn
            {
                Name = "status",
                HeaderText = "Current Status",
                DataPropertyName = "status",
                ValueType = typeof(string),
                DataSource = new string[] { "examining", "waiting", "done", "skipped" },
                FlatStyle = FlatStyle.Standard
            };

            dgvQueue.Columns.Insert(idx, combo);
        }

        private void dgvQueue_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (suppressEvents || e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvQueue.Columns[e.ColumnIndex].Name != "status") return;

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
                        sql = "UPDATE queue SET status=@status, called_at=NOW() WHERE queue_id=@id";
                    else if (newStatus == "done")
                        sql = "UPDATE queue SET status=@status, finished_at=NOW() WHERE queue_id=@id";
                    else
                        sql = "UPDATE queue SET status=@status WHERE queue_id=@id";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", newStatus);
                        cmd.Parameters.AddWithValue("@id", queueId);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadQueue();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update status: " + ex.Message, "Error",
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
            patientsTable.DefaultView.RowFilter = $"full_name LIKE '%{txtSearchPatient.Text}%'";
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

        // ✅ Opens ConsultationControl safely
        private void viewConsultationItem_Click(object sender, EventArgs e)
        {
            if (dgvQueue.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient from the queue first.", "No selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedRow = dgvQueue.SelectedRows[0];

                // ✅ Check if patient_id column exists
                if (!dgvQueue.Columns.Contains("patient_id"))
                {
                    MessageBox.Show("The queue does not contain a patient_id column.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var patientIdObj = selectedRow.Cells["patient_id"].Value;
                if (patientIdObj == null || patientIdObj == DBNull.Value)
                {
                    MessageBox.Show("This queue entry is not linked to a patient.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int patientId = Convert.ToInt32(patientIdObj);

                // ✅ Update status to 'examining'
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string updateSql = @"
                UPDATE queue 
                SET status = 'examining', called_at = NOW() 
                WHERE patient_id = @pid AND DATE(created_at) = CURDATE();
            ";

                    using (var cmd = new MySqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@pid", patientId);
                        cmd.ExecuteNonQuery();
                    }
                }

                // ✅ Open consultation window
                ConsultationControl consultation = new ConsultationControl(patientId);
                consultation.Show();

                // ✅ Refresh queue display to reflect updated status
                LoadQueue();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening consultation: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
