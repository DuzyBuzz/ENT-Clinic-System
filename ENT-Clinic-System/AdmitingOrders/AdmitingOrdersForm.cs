using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ENT_Clinic_System.AdmitingOrders
{
    public partial class AdmitingOrdersForm : Form
    {
        private int _patientId;
        private int _editingOrderId = 0; // 0 = new, >0 = editing existing

        public AdmitingOrdersForm(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
        }

        private void AdmitingOrdersForm_Load(object sender, EventArgs e)
        {
            LoadPatientName();
            LoadTemplate();
            LoadAdmitOrders();
            RichTextBoxFormatterHelper.Attach(richTextBoxTemplate, this);

            // Add right-click menu for DataGridView
            ContextMenuStrip dgvMenu = new ContextMenuStrip();
            ToolStripMenuItem printItem = new ToolStripMenuItem("Print");
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");

            printItem.Click += DgvPrintItem_Click;
            deleteItem.Click += DgvDeleteItem_Click;

            dgvMenu.Items.AddRange(new ToolStripItem[] { printItem, deleteItem });
            admitOrdersDGV.ContextMenuStrip = dgvMenu;
        }

        #region Right-Click Menu Handlers
        private void DgvPrintItem_Click(object sender, EventArgs e)
        {
            if (admitOrdersDGV.SelectedRows.Count == 0) return;

            int orderId = Convert.ToInt32(admitOrdersDGV.SelectedRows[0].Cells["admit_order_id"].Value);
            string rtf = admitOrdersDGV.SelectedRows[0].Cells["special_orders"].Value.ToString();

            RichTextBox tempRtb = new RichTextBox { Rtf = rtf };
            var printHelper = new AdmitOrdersPrintHelper(orderId, tempRtb);
            printHelper.ShowPreview(); // or .Print() for direct print
        }

        private void DgvDeleteItem_Click(object sender, EventArgs e)
        {
            if (admitOrdersDGV.SelectedRows.Count == 0) return;

            int orderId = Convert.ToInt32(admitOrdersDGV.SelectedRows[0].Cells["admit_order_id"].Value);

            if (MessageBox.Show("Delete selected admit order?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("DELETE FROM admit_orders WHERE admit_order_id = @orderId", conn))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadAdmitOrders();
                richTextBoxTemplate.Clear();
                _editingOrderId = 0;
            }
        }
        #endregion

        #region Load Methods
        private void LoadPatientName()
        {
            try
            {
                string fullName = HealthRecordHelper.GetPatientName(_patientId);
                nameLabel.Text = fullName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load patient name:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTemplate()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT template_text FROM admit_template LIMIT 1", conn))
                    {
                        var val = cmd.ExecuteScalar();
                        if (val != null && val != DBNull.Value)
                        {
                            richTextBoxTemplate.Rtf = val.ToString();
                        }
                        else
                        {
                            richTextBoxTemplate.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to load template:" + ex.Message);
            }
        }

        private void LoadAdmitOrders()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT * 
                                   FROM vw_admit_orders_with_patient
                                   WHERE patient_id = @patientId
                                   ORDER BY created_at DESC";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@patientId", _patientId);
                        using (var adapter = new MySqlDataAdapter(cmd))
                        {
                            var dt = new System.Data.DataTable();
                            adapter.Fill(dt);
                            admitOrdersDGV.DataSource = dt;

                            if (admitOrdersDGV.Columns.Contains("special_orders"))
                                admitOrdersDGV.Columns["special_orders"].Visible = false;

                            if (admitOrdersDGV.Columns.Contains("admit_order_id"))
                                admitOrdersDGV.Columns["admit_order_id"].HeaderText = "Order ID";
                            if (admitOrdersDGV.Columns.Contains("created_at"))
                                admitOrdersDGV.Columns["created_at"].HeaderText = "Created At";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load admit orders:\n" + ex.Message);
            }
        }
        #endregion

        #region Submit / Save
        private void submitButton_Click(object sender, EventArgs e)
        {
            try
            {
                int savedOrderId = 0;
                string rtfToSave = richTextBoxTemplate.Rtf ?? string.Empty; // preserve user RTF

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    if (_editingOrderId == 0)
                    {
                        string sql = @"INSERT INTO admit_orders (patient_id, special_orders) 
                                       VALUES (@patientId, @specialOrders);
                                       SELECT LAST_INSERT_ID();";

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@patientId", _patientId);
                            cmd.Parameters.AddWithValue("@specialOrders", rtfToSave);
                            savedOrderId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                    }
                    else
                    {
                        string sql = @"UPDATE admit_orders 
                                       SET special_orders = @specialOrders 
                                       WHERE admit_order_id = @orderId";

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@specialOrders", rtfToSave);
                            cmd.Parameters.AddWithValue("@orderId", _editingOrderId);
                            cmd.ExecuteNonQuery();
                            savedOrderId = _editingOrderId;
                        }
                    }
                }

                MessageBox.Show("Admitting orders saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _editingOrderId = 0;
                richTextBoxTemplate.Clear();
                LoadTemplate();
                LoadAdmitOrders();

                // Print immediately using the preserved RTF
                RichTextBox tempRtb = new RichTextBox { Rtf = rtfToSave };
                var printHelper = new AdmitOrdersPrintHelper(savedOrderId, tempRtb);
                printHelper.ShowPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save admitting orders:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Print Button
        private void printButton_Click(object sender, EventArgs e)
        {
            if (admitOrdersDGV.SelectedRows.Count == 0) return;

            int orderId = Convert.ToInt32(admitOrdersDGV.SelectedRows[0].Cells["admit_order_id"].Value);
            string rtf = admitOrdersDGV.SelectedRows[0].Cells["special_orders"].Value.ToString();

            RichTextBox tempRtb = new RichTextBox { Rtf = rtf };
            var printHelper = new AdmitOrdersPrintHelper(orderId, tempRtb);
            printHelper.ShowPreview();
        }
        #endregion

        #region Edit / Delete
        private void admitOrdersDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _editingOrderId = Convert.ToInt32(admitOrdersDGV.Rows[e.RowIndex].Cells["admit_order_id"].Value);
                string rtf = admitOrdersDGV.Rows[e.RowIndex].Cells["special_orders"].Value.ToString();
                richTextBoxTemplate.Rtf = rtf;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (admitOrdersDGV.SelectedRows.Count == 0) return;

            int orderId = Convert.ToInt32(admitOrdersDGV.SelectedRows[0].Cells["admit_order_id"].Value);

            if (MessageBox.Show("Delete selected admit order?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("DELETE FROM admit_orders WHERE admit_order_id = @orderId", conn))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadAdmitOrders();
                richTextBoxTemplate.Clear();
                _editingOrderId = 0;
            }
        }
        #endregion
    }
}
