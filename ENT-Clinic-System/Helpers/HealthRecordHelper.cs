using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class HealthRecordHelper
    {
        /// <summary>
        /// Save or update a health record for a patient, then reload values into UI controls.
        /// Accepts both TextBox and ComboBox inputs.
        /// </summary>
        public static void SaveUpdateHealthRecord(
            int patientId,
            Control pastMedicalHistory,   // TextBox or DataGridView
            Control familyHistory,        // TextBox or ComboBox
            Control personalHistory,      // TextBox or ComboBox
            TextBox bpTextBox,
            TextBox temperatureTextBox,
            TextBox prTextBox,
            TextBox rrTextBox,
            TextBox htTextBox,
            TextBox wtTextBox,
            Control generalAppearance,    // ComboBox or TextBox
            Control skin,                 // ComboBox or TextBox
            Control headAndFace,          // ComboBox or TextBox
            Control eyes,                 // ComboBox or TextBox
            Control neck,                 // ComboBox or TextBox
            Control chestLungs,           // ComboBox or TextBox
            Control heart,                // ComboBox or TextBox
            Control abdomen,              // ComboBox or TextBox
            Control extremities,          // ComboBox or TextBox
            Control neurologic            // ComboBox or TextBox
        )
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // Check if health record exists
                    string checkQuery = "SELECT COUNT(*) FROM health_record WHERE patient_id=@patientId";
                    using (var cmdCheck = new MySqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@patientId", patientId);
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        string sql;
                        if (count > 0)
                        {
                            // UPDATE
                            sql = @"UPDATE health_record SET
                                        bp=@bp,
                                        temperature=@temperature,
                                        pr=@pr,
                                        rr=@rr,
                                        ht=@ht,
                                        wt=@wt,
                                        past_medical_history=@pastMedicalHistory,
                                        family_history=@familyHistory,
                                        personal_history=@personalHistory,
                                        general_appearance=@generalAppearance,
                                        skin=@skin,
                                        head_and_face=@headAndFace,
                                        eyes=@eyes,
                                        neck=@neck,
                                        chest_lungs=@chestLungs,
                                        heart=@heart,
                                        abdomen=@abdomen,
                                        extremities=@extremities,
                                        neurologic=@neurologic,
                                        updated_at=NOW()
                                    WHERE patient_id=@patientId";
                        }
                        else
                        {
                            // INSERT
                            sql = @"INSERT INTO health_record (
                                        patient_id, bp, temperature, pr, rr, ht, wt,
                                        past_medical_history, family_history, personal_history,
                                        general_appearance, skin, head_and_face, eyes, neck,
                                        chest_lungs, heart, abdomen, extremities, neurologic,
                                        created_at, updated_at
                                    ) VALUES (
                                        @patientId, @bp, @temperature, @pr, @rr, @ht, @wt,
                                        @pastMedicalHistory, @familyHistory, @personalHistory,
                                        @generalAppearance, @skin, @headAndFace, @eyes, @neck,
                                        @chestLungs, @heart, @abdomen, @extremities, @neurologic,
                                        NOW(), NOW()
                                    )";
                        }

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@bp", bpTextBox.Text.Trim());
                            cmd.Parameters.AddWithValue("@temperature", decimal.TryParse(temperatureTextBox.Text.Trim(), out decimal tVal) ? tVal : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@pr", int.TryParse(prTextBox.Text.Trim(), out int prVal) ? prVal : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@rr", int.TryParse(rrTextBox.Text.Trim(), out int rrVal) ? rrVal : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@ht", decimal.TryParse(htTextBox.Text.Trim(), out decimal htVal) ? htVal : (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@wt", decimal.TryParse(wtTextBox.Text.Trim(), out decimal wtVal) ? wtVal : (object)DBNull.Value);

                            cmd.Parameters.AddWithValue("@pastMedicalHistory", GetControlText(pastMedicalHistory));
                            cmd.Parameters.AddWithValue("@familyHistory", GetControlText(familyHistory));
                            cmd.Parameters.AddWithValue("@personalHistory", GetControlText(personalHistory));
                            cmd.Parameters.AddWithValue("@generalAppearance", GetControlText(generalAppearance));
                            cmd.Parameters.AddWithValue("@skin", GetControlText(skin));
                            cmd.Parameters.AddWithValue("@headAndFace", GetControlText(headAndFace));
                            cmd.Parameters.AddWithValue("@eyes", GetControlText(eyes));
                            cmd.Parameters.AddWithValue("@neck", GetControlText(neck));
                            cmd.Parameters.AddWithValue("@chestLungs", GetControlText(chestLungs));
                            cmd.Parameters.AddWithValue("@heart", GetControlText(heart));
                            cmd.Parameters.AddWithValue("@abdomen", GetControlText(abdomen));
                            cmd.Parameters.AddWithValue("@extremities", GetControlText(extremities));
                            cmd.Parameters.AddWithValue("@neurologic", GetControlText(neurologic));

                            cmd.Parameters.AddWithValue("@patientId", patientId);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 🔹 Load saved values back into UI
                    string selectQuery = "SELECT * FROM health_record WHERE patient_id=@patientId";
                    using (var cmdSelect = new MySqlCommand(selectQuery, conn))
                    {
                        cmdSelect.Parameters.AddWithValue("@patientId", patientId);
                        using (var reader = cmdSelect.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bpTextBox.Text = reader["bp"]?.ToString();
                                temperatureTextBox.Text = reader["temperature"]?.ToString();
                                prTextBox.Text = reader["pr"]?.ToString();
                                rrTextBox.Text = reader["rr"]?.ToString();
                                htTextBox.Text = reader["ht"]?.ToString();
                                wtTextBox.Text = reader["wt"]?.ToString();

                                SetControlText(pastMedicalHistory, reader["past_medical_history"]?.ToString());
                                SetControlText(familyHistory, reader["family_history"]?.ToString());
                                SetControlText(personalHistory, reader["personal_history"]?.ToString());
                                SetControlText(generalAppearance, reader["general_appearance"]?.ToString());
                                SetControlText(skin, reader["skin"]?.ToString());
                                SetControlText(headAndFace, reader["head_and_face"]?.ToString());
                                SetControlText(eyes, reader["eyes"]?.ToString());
                                SetControlText(neck, reader["neck"]?.ToString());
                                SetControlText(chestLungs, reader["chest_lungs"]?.ToString());
                                SetControlText(heart, reader["heart"]?.ToString());
                                SetControlText(abdomen, reader["abdomen"]?.ToString());
                                SetControlText(extremities, reader["extremities"]?.ToString());
                                SetControlText(neurologic, reader["neurologic"]?.ToString());
                            }
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save/load health record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper to get value from TextBox, ComboBox, or DataGridView
        private static string GetControlText(Control ctrl)
        {
            switch (ctrl)
            {
                case TextBox tb: return tb.Text.Trim();
                case ComboBox cb: return cb.Text.Trim();
                case DataGridView dgv:
                    {
                        // Return CSV of first column
                        string result = "";
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            if (row.IsNewRow) continue;
                            if (row.Cells.Count > 0 && row.Cells[0].Value != null)
                                result += row.Cells[0].Value.ToString().Trim() + ", ";
                        }
                        return result.TrimEnd(',', ' ');
                    }
                default: return null;
            }
        }

        // Helper to set value to TextBox or ComboBox
        private static void SetControlText(Control ctrl, string value)
        {
            switch (ctrl)
            {
                case TextBox tb: tb.Text = value; break;
                case ComboBox cb: cb.Text = value; break;
                case DataGridView dgv:
                    {
                        dgv.Rows.Clear();
                        if (!string.IsNullOrEmpty(value))
                        {
                            string[] items = value.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var item in items)
                                dgv.Rows.Add(item);
                        }
                        break;
                    }
            }
        }        // Load health record from database into your controls
        public static void LoadHealthRecord(
            int patientId,
            DataGridView pastMedicalHistoryDGV,
            ComboBox familyComboBox,
            ComboBox personalComboBox,
            TextBox bpTextBox,
            TextBox temperatureTextBox,
            TextBox prTextBox,
            TextBox rrTextBox,
            TextBox htTextBox,
            TextBox wtTextBox,
            ComboBox generalAppearanceComboBox,
            ComboBox skinComboBox,
            ComboBox headAndFaceComboBox,
            ComboBox eyesComboBox,
            ComboBox neckComboBox,
            ComboBox chestLungsComboBox,
            ComboBox heartComboBox,
            ComboBox abdomenComboBox,
            ComboBox extremitiesComboBox,
            ComboBox neurologicComboBox
        )
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT * FROM health_record WHERE patient_id = @patientId ORDER BY health_record_id DESC LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@patientId", patientId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Medical history
                            pastMedicalHistoryDGV.Rows.Clear();
                            string pastMedicalHistory = reader["past_medical_history"].ToString();
                            if (!string.IsNullOrEmpty(pastMedicalHistory))
                            {
                                string[] rows = pastMedicalHistory.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string row in rows)
                                    pastMedicalHistoryDGV.Rows.Add(row);
                            }

                            familyComboBox.Text = reader["family_history"].ToString();
                            personalComboBox.Text = reader["personal_history"].ToString();

                            // Vital signs
                            bpTextBox.Text = reader["bp"].ToString();
                            temperatureTextBox.Text = reader["temperature"].ToString();
                            prTextBox.Text = reader["pr"].ToString();
                            rrTextBox.Text = reader["rr"].ToString();
                            htTextBox.Text = reader["ht"].ToString();
                            wtTextBox.Text = reader["wt"].ToString();

                            // Physical exam
                            generalAppearanceComboBox.Text = reader["general_appearance"].ToString();
                            skinComboBox.Text = reader["skin"].ToString();
                            headAndFaceComboBox.Text = reader["head_and_face"].ToString();
                            eyesComboBox.Text = reader["eyes"].ToString();
                            neckComboBox.Text = reader["neck"].ToString();
                            chestLungsComboBox.Text = reader["chest_lungs"].ToString();
                            heartComboBox.Text = reader["heart"].ToString();
                            abdomenComboBox.Text = reader["abdomen"].ToString();
                            extremitiesComboBox.Text = reader["extremities"].ToString();
                            neurologicComboBox.Text = reader["neurologic"].ToString();
                        }
                        else
                        {
                            // No previous record found, clear UI
                            pastMedicalHistoryDGV.Rows.Clear();
                            familyComboBox.Text = "";
                            personalComboBox.Text = "";
                            bpTextBox.Text = "";
                            temperatureTextBox.Text = "";
                            prTextBox.Text = "";
                            rrTextBox.Text = "";
                            htTextBox.Text = "";
                            wtTextBox.Text = "";
                            generalAppearanceComboBox.Text = "";
                            skinComboBox.Text = "";
                            headAndFaceComboBox.Text = "";
                            eyesComboBox.Text = "";
                            neckComboBox.Text = "";
                            chestLungsComboBox.Text = "";
                            heartComboBox.Text = "";
                            abdomenComboBox.Text = "";
                            extremitiesComboBox.Text = "";
                            neurologicComboBox.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load health record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
