using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Referral
{
    public partial class ReferralForm : Form
    {
        private int patientId;
        private int currentReferralId = 0;
        private string connectionString = DBConfig.ConnectionString;

        public ReferralForm(int patientId)
        {
            InitializeComponent();
            this.patientId = patientId;

            LoadPatientBasicInfo();
            LoadReferralsList();
            WireEventHandlers();
            FormatDataGridViewHeaders();
        }

        /// <summary>
        /// Hook up UI event handlers.
        /// </summary>
        private void WireEventHandlers()
        {
            btnSave.Click += BtnSave_Click;
            btnClear.Click += BtnClear_Click;
            btnPrint.Click += BtnPrint_Click;
            btnUpdate.Click += BtnUpdate_Click;

            dgvOrders.CellClick += DgvOrders_CellClick;
            dgvOrders.CellMouseDown += DgvOrders_CellMouseDown; // right-click selects row
            deleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
        }

        /// <summary>
        /// Hide/rename grid columns after data bind.
        /// </summary>
        private void FormatDataGridViewHeaders()
        {
            try
            {
                if (dgvOrders.Columns.Contains("referral_id"))
                    dgvOrders.Columns["referral_id"].Visible = false;

                if (dgvOrders.Columns.Contains("created_at"))
                    dgvOrders.Columns["created_at"].HeaderText = "Created Date";

                if (dgvOrders.Columns.Contains("referring_doctor"))
                    dgvOrders.Columns["referring_doctor"].HeaderText = "Referring Doctor";
            }
            catch { /* ignore layout failures */ }
        }

        /// <summary>
        /// Load basic patient values into the form (read-only fields).
        /// </summary>
        private void LoadPatientBasicInfo()
        {
            try
            {
                txtPatientName.Text = PatientDataHelper.GetPatientValue(patientId, "full_name") ?? "";
                txtAge.Text = PatientDataHelper.GetPatientValue(patientId, "age") ?? "";
                txtSex.Text = PatientDataHelper.GetPatientValue(patientId, "sex") ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load patient data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load referral list for the left grid.
        /// </summary>
        private void LoadReferralsList()
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand(@"
                    SELECT referral_id, created_at, referring_doctor
                    FROM referrals
                    WHERE patient_id = @pid
                    ORDER BY referral_id DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", patientId);
                    var dt = new DataTable();
                    using (var da = new MySqlDataAdapter(cmd))
                        da.Fill(dt);

                    dgvOrders.DataSource = dt;
                    FormatDataGridViewHeaders();
                    dgvOrders.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading referrals: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Grid left-click - load selected referral into the form.
        /// </summary>
        private void DgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                if (int.TryParse(dgvOrders.Rows[e.RowIndex].Cells["referral_id"].Value?.ToString(), out int id))
                {
                    currentReferralId = id;
                    LoadReferralDetails(currentReferralId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting referral: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ensure a right-click selects the row under cursor so context menu actions target that row.
        /// </summary>
        private void DgvOrders_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    dgvOrders.ClearSelection();
                    dgvOrders.Rows[e.RowIndex].Selected = true;

                    if (int.TryParse(dgvOrders.Rows[e.RowIndex].Cells["referral_id"].Value?.ToString(), out int id))
                    {
                        currentReferralId = id;
                    }

                    // Show context menu at mouse position (attached in designer)
                    // The grid will show contextMenuStrip automatically on right-click.
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Right-click selection failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Load a referral record into the fields for viewing/editing.
        /// </summary>
        private void LoadReferralDetails(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand(@"
                    SELECT referring_doctor, referral_type, present_working_impression, plan, additional_info
                    FROM referrals
                    WHERE referral_id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cmbReferingDoctor.Text = dr["referring_doctor"]?.ToString() ?? "";
                            txtWorkingImp.Text = dr["present_working_impression"]?.ToString() ?? "";
                            planTextBox.Text = dr["plan"]?.ToString() ?? "";
                            txtAdditionalInfo.Text = dr["additional_info"]?.ToString() ?? "";

                            string types = dr["referral_type"]?.ToString() ?? "";
                            chkEvalMgmt.Checked = types.Contains("Evaluation & Management");
                            chkPreOp.Checked = types.Contains("Pre-Op Risk Assessment");
                            chkCoManagement.Checked = types.Contains("Co-Management");
                            chkEmergency.Checked = types.Contains("Emergency");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading referral details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Save (insert) or update depending on currentReferralId.
        /// After successful save/update we ask the user if they want to print.
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWorkingImp.Text))
            {
                MessageBox.Show("Please enter the present working impression.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    if (currentReferralId == 0)
                        InsertNewReferral(conn);  // will set currentReferralId to inserted id when possible
                    else
                        UpdateReferral(conn);
                }

                MessageBox.Show("Referral saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ask user to print the newly saved/updated referral
                var resp = MessageBox.Show("Do you want to print this referral now?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes && currentReferralId > 0)
                {
                    try
                    {
                        var helper = new ReferralPrintHelper(currentReferralId);
                        helper.ShowPreview();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Print preview failed: " + ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Reset UI
                currentReferralId = 0;
                ClearFields();
                LoadReferralsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving referral: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Update button event - simply routes to the same save flow but ensures a selection exists.
        /// </summary>
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (currentReferralId == 0)
            {
                MessageBox.Show("Please select a referral to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Reuse the save handler so validation and printing prompt are consistent.
            BtnSave_Click(sender, e);
        }

        private void InsertNewReferral(MySqlConnection conn)
        {
            using (var cmd = new MySqlCommand(@"
                INSERT INTO referrals
                (patient_id, referring_doctor, referral_type, present_working_impression, plan, additional_info, created_at)
                VALUES (@pid, @doctor, @type, @working, @plan, @addInfo, CURRENT_TIMESTAMP)", conn))
            {
                AddParameters(cmd);
                cmd.ExecuteNonQuery();

                // Capture last inserted id so the print function can use it immediately
                try
                {
                    long lastId = cmd.LastInsertedId;
                    if (lastId > 0)
                        currentReferralId = (int)lastId;
                }
                catch
                {
                    // If LastInsertedId isn't available for some reason, printing will simply be skipped
                }
            }
        }

        private void UpdateReferral(MySqlConnection conn)
        {
            using (var cmd = new MySqlCommand(@"
                UPDATE referrals SET
                    referring_doctor=@doctor,
                    referral_type=@type,
                    present_working_impression=@working,
                    plan=@plan,
                    additional_info=@addInfo
                WHERE referral_id=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", currentReferralId);
                AddParameters(cmd);
                cmd.ExecuteNonQuery();
            }
        }

        private void AddParameters(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@pid", patientId);
            cmd.Parameters.AddWithValue("@doctor", (cmbReferingDoctor.Text ?? "").Trim());
            cmd.Parameters.AddWithValue("@type", GetReferralType());
            cmd.Parameters.AddWithValue("@working", (txtWorkingImp.Text ?? "").Trim());
            cmd.Parameters.AddWithValue("@plan", (planTextBox.Text ?? "").Trim());
            cmd.Parameters.AddWithValue("@addInfo", (txtAdditionalInfo.Text ?? "").Trim());
        }

        /// <summary>
        /// Build a comma separated referral_type string from checkboxes.
        /// </summary>
        private string GetReferralType()
        {
            var types = new[] {
                chkEvalMgmt.Checked ? "Evaluation & Management" : null,
                chkPreOp.Checked ? "Pre-Op Risk Assessment" : null,
                chkCoManagement.Checked ? "Co-Management" : null,
                chkEmergency.Checked ? "Emergency" : null
            }.Where(x => x != null);

            return string.Join(",", types);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        /// <summary>
        /// Reset form fields to default.
        /// </summary>
        private void ClearFields()
        {
            cmbReferingDoctor.SelectedIndex = -1;
            chkEvalMgmt.Checked = false;
            chkPreOp.Checked = false;
            chkCoManagement.Checked = false;
            chkEmergency.Checked = false;
            txtWorkingImp.Clear();
            planTextBox.Clear();
            txtAdditionalInfo.Clear();
            currentReferralId = 0;
            dgvOrders.ClearSelection();
        }

        /// <summary>
        /// Print currently selected referral (preview).
        /// </summary>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentReferralId == 0)
                {
                    MessageBox.Show("Please select a referral to print.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var helper = new ReferralPrintHelper(currentReferralId);
                helper.ShowPreview();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Print failed: " + ex.Message);
                MessageBox.Show("Print failed: " + ex.Message, "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Context menu delete handler.
        /// </summary>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedReferral();
        }

        /// <summary>
        /// Deletes the currently selected referral row after confirmation.
        /// </summary>
        private void DeleteSelectedReferral()
        {
            try
            {
                if (currentReferralId == 0)
                {
                    MessageBox.Show("Please select a referral to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var resp = MessageBox.Show("Are you sure you want to delete this referral? This action cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resp != DialogResult.Yes) return;

                using (var conn = new MySqlConnection(connectionString))
                using (var cmd = new MySqlCommand("DELETE FROM referrals WHERE referral_id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", currentReferralId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    if (affected > 0)
                    {
                        MessageBox.Show("Referral deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        currentReferralId = 0;
                        ClearFields();
                        LoadReferralsList();
                    }
                    else
                    {
                        MessageBox.Show("Delete failed or record not found.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting referral: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReferralForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Autocomplete for the referring doctor combobox (pull items from referrals.referring_doctor)
                AutoCompleteHelper.SetupAutoComplete(cmbReferingDoctor, "referrals", new List<string> { "referring_doctor" });
                ComboBoxCollectionHelper.PopulateComboBox(cmbReferingDoctor, "referrals", "referring_doctor");

                // refresh list just in case
                LoadReferralsList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ReferralForm_Load error: " + ex.Message);
            }
        }
    }
}
