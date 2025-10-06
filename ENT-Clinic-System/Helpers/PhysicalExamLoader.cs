using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class PhysicalExamLoader
    {
        /// <summary>
        /// Loads all PE names from the general_pe table and creates dynamic rows with Label, CheckBox, and ComboBox.
        /// </summary>
        /// <param name="flowPanel">The FlowLayoutPanel to fill</param>
        public static void LoadPhysicalExaminations(FlowLayoutPanel flowPanel)
        {
            if (flowPanel == null) return;

            flowPanel.Controls.Clear(); // Clear existing controls

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT pe_name FROM general_pe ORDER BY id ASC";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string peName = reader.GetString("pe_name");

                            // Create a Panel for one row
                            Panel rowPanel = new Panel();
                            rowPanel.Width = flowPanel.Width - 25; // Slight padding
                            rowPanel.Height = 30;
                            rowPanel.Margin = new Padding(0, 0, 0, 5); // spacing between rows

                            // Column 1: PE Name Label
                            Label lblName = new Label();
                            lblName.Text = peName;
                            lblName.Width = (int)(rowPanel.Width * 0.4); // 40% width
                            lblName.Location = new System.Drawing.Point(0, 5);
                            lblName.AutoSize = false;

                            // Column 2: Normal CheckBox
                            CheckBox chkNormal = new CheckBox();
                            chkNormal.Width = (int)(rowPanel.Width * 0.2); // 20% width
                            chkNormal.Location = new System.Drawing.Point(lblName.Right + 10, 5);

                            // Column 3: Findings ComboBox
                            ComboBox cmbFindings = new ComboBox();
                            cmbFindings.Width = (int)(rowPanel.Width * 0.35); // 35% width
                            cmbFindings.Location = new System.Drawing.Point(chkNormal.Right + 10, 0);
                            cmbFindings.DropDownStyle = ComboBoxStyle.DropDown; // user can type
                            cmbFindings.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            cmbFindings.AutoCompleteSource = AutoCompleteSource.ListItems;

                            // Add controls to row panel
                            rowPanel.Controls.Add(lblName);
                            rowPanel.Controls.Add(chkNormal);
                            rowPanel.Controls.Add(cmbFindings);

                            // Add row panel to flow layout
                            flowPanel.Controls.Add(rowPanel);
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Physical Examination:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void SaveUpdatePhysicalExam(int patientId, FlowLayoutPanel flowPanel)
        {
            var peValues = GetPhysicalExamValues(flowPanel);

            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                foreach (var kvp in peValues)
                {
                    string peName = kvp.Key;
                    bool isNormal = kvp.Value.IsNormal;
                    string findings = kvp.Value.Findings;

                    // Insert or update based on patient_id + pe_name
                    string sql = @"
                INSERT INTO health_record_pe
                    (patient_id, pe_name, is_normal, findings)
                VALUES
                    (@patient_id, @pe_name, @is_normal, @findings)
                ON DUPLICATE KEY UPDATE
                    is_normal = @is_normal, findings = @findings;
            ";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@patient_id", patientId);
                        cmd.Parameters.AddWithValue("@pe_name", peName);
                        cmd.Parameters.AddWithValue("@is_normal", isNormal);
                        cmd.Parameters.AddWithValue("@findings", findings);
                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
            }
        }
        public static Dictionary<string, (bool IsNormal, string Findings)> GetPhysicalExamValues(FlowLayoutPanel flowPanel)
        {
            Dictionary<string, (bool, string)> result = new Dictionary<string, (bool, string)>();

            foreach (Control rowPanel in flowPanel.Controls)
            {
                if (rowPanel is Panel pnl && pnl.Controls.Count >= 3)
                {
                    Label lbl = pnl.Controls[0] as Label;
                    CheckBox chk = pnl.Controls[1] as CheckBox;
                    ComboBox cmb = pnl.Controls[2] as ComboBox;

                    if (lbl != null && chk != null && cmb != null)
                    {
                        result[lbl.Text] = (chk.Checked, cmb.Text.Trim());
                    }
                }
            }

            return result;
        }

    }
}
