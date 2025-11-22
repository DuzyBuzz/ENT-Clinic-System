using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.Admission
{
    public partial class AdmittingOrderForm : Form
    {
        private int _patientId;
        private int _currentOrderId = 0;

        public AdmittingOrderForm(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;

            LoadPatientBasicInfo();
            LoadOrdersList();
            WireEventHandlers();
        }

        private void WireEventHandlers()
        {
            dgvOrders.CellClick += DgvOrders_CellClick;
            dgvOrders.CellMouseDown += DgvOrders_CellMouseDown; // for right-click selection
            btnSave.Click += BtnSave_Click;
            btnClear.Click += BtnClear_Click;
            btnPrint.Click += BtnPrint_Click;
            btnUpdate.Click += BtnUpdate_Click;

            // Context menu delete
            deleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
        }

        private void LoadPatientBasicInfo()
        {
            try
            {
                txtPatientName.Text = PatientDataHelper.GetPatientValue(_patientId, "full_name") ?? "";
                txtAge.Text = PatientDataHelper.GetPatientValue(_patientId, "age") ?? "";
                txtSex.Text = PatientDataHelper.GetPatientValue(_patientId, "sex") ?? "";
            }
            catch (Exception ex)
            {
                ShowError("Failed to load patient info", ex);
            }
        }

        private void LoadOrdersList()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(@"
                    SELECT `admitting_order_id`, `created_at`, `diagnosis`
                    FROM `admitting_orders`
                    WHERE `patient_id` = @pid
                    ORDER BY `admitting_order_id` DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", _patientId);
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
                ShowError("Error loading admitting orders list", ex);
            }
        }

        private void FormatDataGridViewHeaders()
        {
            try
            {
                if (dgvOrders.Columns.Contains("admitting_order_id"))
                {
                    dgvOrders.Columns["admitting_order_id"].HeaderText = "Order ID";
                    dgvOrders.Columns["admitting_order_id"].Visible = false;
                }

                if (dgvOrders.Columns.Contains("created_at"))
                    dgvOrders.Columns["created_at"].HeaderText = "Created Date";

                if (dgvOrders.Columns.Contains("diagnosis"))
                    dgvOrders.Columns["diagnosis"].HeaderText = "Impression";
            }
            catch { }
        }

        private void DgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                var cell = dgvOrders.Rows[e.RowIndex].Cells["admitting_order_id"];
                if (cell?.Value != null &&
                    int.TryParse(cell.Value.ToString(), out int orderId))
                {
                    _currentOrderId = orderId;
                    LoadOrderDetails(orderId);
                }
            }
            catch (Exception ex)
            {
                ShowError("Error reading selected order", ex);
            }
        }

        // ensure right-click selects row under cursor so context menu acts on that row
        private void DgvOrders_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    dgvOrders.ClearSelection();
                    dgvOrders.Rows[e.RowIndex].Selected = true;

                    var cell = dgvOrders.Rows[e.RowIndex].Cells["admitting_order_id"];
                    if (cell?.Value != null && int.TryParse(cell.Value.ToString(), out int orderId))
                    {
                        _currentOrderId = orderId;
                    }

                    // Show context menu at mouse position (designer already associates ContextMenuStrip)
                    // The DataGridView will automatically show its ContextMenuStrip on right-click.
                }
            }
            catch (Exception ex)
            {
                ShowError("Error selecting row", ex);
            }
        }

        private void LoadOrderDetails(int id)
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(@"
                    SELECT `diagnosis`, `chief_complaints`, `vital_signs`, `diet`, `activity`,
                           `medications`, `iv_fluids`, `laboratory`, `imaging`,
                           `nursing_instructions`, `special_instructions`, `created_at`
                    FROM `admitting_orders`
                    WHERE `admitting_order_id` = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtDiagnosis.Text = GetSafeString(dr, "diagnosis");
                            txtCC.Text = GetSafeString(dr, "chief_complaints");
                            cbmVitalSigns.Text = GetSafeString(dr, "vital_signs");
                            cboDiet.Text = GetSafeString(dr, "diet");
                            cboActivity.Text = GetSafeString(dr, "activity");
                            txtMedications.Text = GetSafeString(dr, "medications");
                            txtIVFluids.Text = GetSafeString(dr, "iv_fluids");
                            txtLabs.Text = GetSafeString(dr, "laboratory");
                            txtImaging.Text = GetSafeString(dr, "imaging");
                            txtNursing.Text = GetSafeString(dr, "nursing_instructions");
                            txtSurgery.Text = GetSafeString(dr, "special_instructions");

                            dtAdmitDate.Value =
                                dr["created_at"] == DBNull.Value
                                ? DateTime.Now
                                : Convert.ToDateTime(dr["created_at"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error loading order details", ex);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    if (_currentOrderId == 0)
                        InsertNewOrder(conn);     // sets _currentOrderId to the inserted id
                    else
                        UpdateExistingOrder(conn); // updates row with _currentOrderId
                }

                MessageBox.Show("Order saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ask user if they want to print the saved/updated order
                var dr = MessageBox.Show("Do you want to print this order now?", "Print",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes && _currentOrderId > 0)
                {
                    try
                    {
                        var printHelper = new AdmitOrdersPrintHelper(_currentOrderId);
                        printHelper.ShowPreview();
                    }
                    catch (Exception ex)
                    {
                        ShowError("Failed to preview/print", ex);
                    }
                }

                // Reset UI & reload
                _currentOrderId = 0;
                LoadOrdersList();
                ClearFields();
            }
            catch (Exception ex)
            {
                ShowError("Error saving order", ex);
            }
        }

        // New Update button: triggers update flow. This uses the same save logic but ensures a selection exists.
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentOrderId == 0)
                {
                    MessageBox.Show("Please select an admitting order to update.", "No Order Selected",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Reuse save path; since _currentOrderId != 0 it will take the update branch
                BtnSave_Click(sender, e);
            }
            catch (Exception ex)
            {
                ShowError("Update failed", ex);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                MessageBox.Show("Please enter diagnosis.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiagnosis.Focus();
                return false;
            }
            return true;
        }

        private void InsertNewOrder(MySqlConnection conn)
        {
            using (var cmd = new MySqlCommand(@"
                INSERT INTO `admitting_orders`
                (`patient_id`, `diagnosis`, `chief_complaints`, `vital_signs`, `diet`, `activity`,
                 `medications`, `iv_fluids`, `laboratory`, `imaging`,
                 `nursing_instructions`, `special_instructions`, `created_at`)
                VALUES
                (@pid, @diagnosis, @chief, @vital, @diet, @activity,
                 @meds, @iv, @lab, @img, @nurse, @special, CURRENT_TIMESTAMP)", conn))
            {
                AddParametersToCommand(cmd);
                cmd.ExecuteNonQuery();

                // capture inserted id so user can print newly created order
                try
                {
                    long lastId = cmd.LastInsertedId;
                    if (lastId > 0)
                        _currentOrderId = (int)lastId;
                }
                catch
                {
                    // ignore failure to read last inserted id — printing will only be available if id is set
                }
            }
        }

        private void UpdateExistingOrder(MySqlConnection conn)
        {
            using (var cmd = new MySqlCommand(@"
                UPDATE `admitting_orders` SET
                    `diagnosis`=@diagnosis,
                    `chief_complaints`=@chief,
                    `vital_signs`=@vital,
                    `diet`=@diet,
                    `activity`=@activity,
                    `medications`=@meds,
                    `iv_fluids`=@iv,
                    `laboratory`=@lab,
                    `imaging`=@img,
                    `nursing_instructions`=@nurse,
                    `special_instructions`=@special,
                    `updated_at`=CURRENT_TIMESTAMP
                WHERE `admitting_order_id`=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", _currentOrderId);
                AddParametersToCommand(cmd);
                cmd.ExecuteNonQuery();
            }
        }

        private void AddParametersToCommand(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@pid", _patientId);
            cmd.Parameters.AddWithValue("@diagnosis", SqlSafeValue(txtDiagnosis.Text));
            cmd.Parameters.AddWithValue("@chief", SqlSafeValue(txtCC.Text));
            cmd.Parameters.AddWithValue("@vital", SqlSafeValue(cbmVitalSigns.Text));
            cmd.Parameters.AddWithValue("@diet", SqlSafeValue(cboDiet.Text));
            cmd.Parameters.AddWithValue("@activity", SqlSafeValue(cboActivity.Text));
            cmd.Parameters.AddWithValue("@meds", SqlSafeValue(txtMedications.Text));
            cmd.Parameters.AddWithValue("@iv", SqlSafeValue(txtIVFluids.Text));
            cmd.Parameters.AddWithValue("@lab", SqlSafeValue(txtLabs.Text));
            cmd.Parameters.AddWithValue("@img", SqlSafeValue(txtImaging.Text));
            cmd.Parameters.AddWithValue("@nurse", SqlSafeValue(txtNursing.Text));
            cmd.Parameters.AddWithValue("@special", SqlSafeValue(txtSurgery.Text));
        }

        private string GetSafeString(MySqlDataReader dr, string columnName)
        {
            try
            {
                return dr[columnName] == DBNull.Value ? "" : dr[columnName].ToString();
            }
            catch { return ""; }
        }

        private object SqlSafeValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.Trim();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtCC.Text = "";
            txtDiagnosis.Text = "";
            txtIVFluids.Text = "";
            txtMedications.Text = "";
            txtLabs.Text = "";
            txtImaging.Text = "";
            txtNursing.Text = "";
            txtSurgery.Text = "";
            cbmVitalSigns.SelectedIndex = -1;
            cboDiet.SelectedIndex = -1;
            cboActivity.SelectedIndex = -1;
            dtAdmitDate.Value = DateTime.Now;

            _currentOrderId = 0;
            if (dgvOrders.DataSource != null)
                dgvOrders.ClearSelection();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentOrderId == 0)
                {
                    MessageBox.Show("Please select an admitting order to print.",
                        "No Order Selected",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var printHelper = new AdmitOrdersPrintHelper(_currentOrderId);
                printHelper.ShowPreview();
            }
            catch (Exception ex)
            {
                ShowError("Print failed", ex);
            }
        }

        // Context menu delete click
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedOrder();
        }

        // Delete logic with confirmation
        private void DeleteSelectedOrder()
        {
            try
            {
                if (_currentOrderId == 0)
                {
                    MessageBox.Show("Please select a record to delete.", "No Selection",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var resp = MessageBox.Show("Are you sure you want to delete this admitting order? This action cannot be undone.",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resp != DialogResult.Yes) return;

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand("DELETE FROM `admitting_orders` WHERE `admitting_order_id` = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", _currentOrderId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    if (affected > 0)
                    {
                        MessageBox.Show("Order deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _currentOrderId = 0;
                        ClearFields();
                        LoadOrdersList();
                    }
                    else
                    {
                        MessageBox.Show("Delete failed or already removed.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error deleting record", ex);
            }
        } 

        private void ShowError(string title, Exception ex) 
        {
            MessageBox.Show($"{title}: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AdmittingOrderForm_Load(object sender, EventArgs e)
        {
            // DIET
            AutoCompleteHelper.SetupAutoComplete(
                cboDiet, "admitting_orders", new List<string> { "diet" });
            ComboBoxCollectionHelper.PopulateComboBox(
                cboDiet, "admitting_orders", "diet");

            // ACTIVITY
            AutoCompleteHelper.SetupAutoComplete(
                cboActivity, "admitting_orders", new List<string> { "activity" });
            ComboBoxCollectionHelper.PopulateComboBox(
                cboActivity, "admitting_orders", "activity");

            // VITAL SIGNS
            ComboBoxCollectionHelper.PopulateComboBox(
                cbmVitalSigns, "admitting_orders", "vital_signs");
            AutoCompleteHelper.SetupAutoComplete(
                cbmVitalSigns, "admitting_orders", new List<string> { "vital_signs" });
        }
    }
}
