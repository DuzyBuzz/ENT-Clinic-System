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
            catch { }
        }
        private void WireEventHandlers()
        {
            btnSave.Click += BtnSave_Click;
            btnClear.Click += BtnClear_Click;
            btnPrint.Click += BtnPrint_Click;
            dgvOrders.CellClick += DgvOrders_CellClick;
        }

        private void LoadPatientBasicInfo()
        {
            txtPatientName.Text = PatientDataHelper.GetPatientValue(patientId, "full_name") ?? "";
            txtAge.Text = PatientDataHelper.GetPatientValue(patientId, "age") ?? "";
            txtSex.Text = PatientDataHelper.GetPatientValue(patientId, "sex") ?? "";
        }

        private void LoadReferralsList()
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
                new MySqlDataAdapter(cmd).Fill(dt);
                dgvOrders.DataSource = dt;
            }
        }

        private void DgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (int.TryParse(dgvOrders.Rows[e.RowIndex].Cells["referral_id"].Value?.ToString(), out int id))
            {
                currentReferralId = id;
                LoadReferralDetails(currentReferralId);
            }
        }

        private void LoadReferralDetails(int id)
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
                        cmbReferingDoctor.Text = dr["referring_doctor"].ToString();
                        txtWorkingImp.Text = dr["present_working_impression"].ToString();
                        planTextBox.Text = dr["plan"].ToString();
                        txtAdditionalInfo.Text = dr["additional_info"].ToString();

                        // Set checkboxes based on referral_type (comma-separated)
                        string types = dr["referral_type"].ToString();
                        chkEvalMgmt.Checked = types.Contains("Evaluation & Management");
                        chkPreOp.Checked = types.Contains("Pre-Op Risk Assessment");
                        chkCoManagement.Checked = types.Contains("Co-Management");
                        chkEmergency.Checked = types.Contains("Emergency");
                    }
                }
            }
        }

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
                        InsertNewReferral(conn);
                    else
                        UpdateReferral(conn);
                }

                MessageBox.Show("Referral saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                currentReferralId = 0;
                LoadReferralsList();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving referral: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            cmd.Parameters.AddWithValue("@doctor", cmbReferingDoctor.Text.Trim());
            cmd.Parameters.AddWithValue("@type", GetReferralType());
            cmd.Parameters.AddWithValue("@working", txtWorkingImp.Text.Trim());
            cmd.Parameters.AddWithValue("@plan", planTextBox.Text.Trim());
            cmd.Parameters.AddWithValue("@addInfo", txtAdditionalInfo.Text.Trim());
        }

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

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentReferralId == 0)
                {
                    MessageBox.Show("Please select an admitting order to print.", "No Order Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var printHelper = new ReferralPrintHelper(currentReferralId);
                printHelper.ShowPreview();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Print failed", ex);
            }
        }

        private void ReferralForm_Load(object sender, EventArgs e)
        {
            // Autocomplete for the relationship combobox (single column)
            AutoCompleteHelper.SetupAutoComplete(
                cmbReferingDoctor,
                "referrals",
                new List<string> { "referring_doctor" } // pass as a list
            );

            // Populate combobox items from the same column
            ComboBoxCollectionHelper.PopulateComboBox(
                cmbReferingDoctor,
                "referrals",
                "referring_doctor"
            );
        }
    }
}
