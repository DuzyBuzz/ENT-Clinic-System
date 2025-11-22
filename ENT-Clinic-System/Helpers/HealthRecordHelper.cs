using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class HealthRecordHelper
    {
        /// <summary>
        /// Saves or updates a patient's health record in the database.
        /// Handles DGVs for health histories and ComboBoxes/TextBoxes for vital signs & physical exams.
        /// </summary>
        public static void SaveUpdateHealthRecord(
            int patientId,
            DataGridView pastMedicalHistoryDGV,
            DataGridView allergiesDGV,
            DataGridView familyHistoryDGV,
            DataGridView personalSocialHistoryDGV,
            TextBox bpTextBox,
            TextBox temperatureTextBox,
            TextBox prTextBox,
            TextBox rrTextBox,
            TextBox htTextBox,
            TextBox wtTextBox,
            ComboBox generalAppearance,
            ComboBox skin,
            ComboBox headAndFace,
            ComboBox eyes,
            ComboBox neck,
            ComboBox chestLungs,
            ComboBox heart,
            ComboBox abdomen,
            ComboBox extremities,
            ComboBox neurologic
        )
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // Check if patient already has a record
                    string checkQuery = "SELECT COUNT(*) FROM health_record WHERE patient_id=@patientId";
                    using (var cmdCheck = new MySqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@patientId", patientId);
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        string sql = (count > 0)
                            ? @"UPDATE health_record SET
                                    bp=@bp, temperature=@temperature, pr=@pr, rr=@rr, ht=@ht, wt=@wt,
                                    past_medical_history=@pastMedicalHistory,
                                    allergies=@allergies,
                                    family_history=@familyHistory,
                                    personal_social_history=@personalSocialHistory,
                                    general_appearance=@generalAppearance,
                                    skin=@skin, head_and_face=@headAndFace, eyes=@eyes, neck=@neck,
                                    chest_lungs=@chestLungs, heart=@heart, abdomen=@abdomen,
                                    extremities=@extremities, neurologic=@neurologic,
                                    updated_at=NOW()
                               WHERE patient_id=@patientId"
                            : @"INSERT INTO health_record_history (
                                    patient_id, bp, temperature, pr, rr, ht, wt,
                                    past_medical_history, allergies, family_history, personal_social_history,
                                    general_appearance, skin, head_and_face, eyes, neck,
                                    chest_lungs, heart, abdomen, extremities, neurologic,
                                    created_at, updated_at
                               ) VALUES (
                                    @patientId, @bp, @temperature, @pr, @rr, @ht, @wt,
                                    @pastMedicalHistory, @allergies, @familyHistory, @personalSocialHistory,
                                    @generalAppearance, @skin, @headAndFace, @eyes, @neck,
                                    @chestLungs, @heart, @abdomen, @extremities, @neurologic,
                                    NOW(), NOW()
                               )";

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            // --- Vital Signs ---
                            cmd.Parameters.AddWithValue("@bp", bpTextBox.Text.Trim());
                            cmd.Parameters.AddWithValue("@temperature", temperatureTextBox.Text.Trim());
                            cmd.Parameters.AddWithValue("@pr", prTextBox.Text.Trim());
                            cmd.Parameters.AddWithValue("@rr", rrTextBox.Text.Trim());
                            cmd.Parameters.AddWithValue("@ht", htTextBox.Text.Trim());
                            cmd.Parameters.AddWithValue("@wt", wtTextBox.Text.Trim());

                            // --- Health History (DGVs converted to CSV) ---
                            cmd.Parameters.AddWithValue("@pastMedicalHistory", GetDgvData(pastMedicalHistoryDGV));
                            cmd.Parameters.AddWithValue("@allergies", GetDgvData(allergiesDGV));
                            cmd.Parameters.AddWithValue("@familyHistory", GetDgvData(familyHistoryDGV));
                            cmd.Parameters.AddWithValue("@personalSocialHistory", GetDgvData(personalSocialHistoryDGV));

                            // --- Physical Exam (ComboBoxes) ---
                            cmd.Parameters.AddWithValue("@generalAppearance", generalAppearance.Text.Trim());
                            cmd.Parameters.AddWithValue("@skin", skin.Text.Trim());
                            cmd.Parameters.AddWithValue("@headAndFace", headAndFace.Text.Trim());
                            cmd.Parameters.AddWithValue("@eyes", eyes.Text.Trim());
                            cmd.Parameters.AddWithValue("@neck", neck.Text.Trim());
                            cmd.Parameters.AddWithValue("@chestLungs", chestLungs.Text.Trim());
                            cmd.Parameters.AddWithValue("@heart", heart.Text.Trim());
                            cmd.Parameters.AddWithValue("@abdomen", abdomen.Text.Trim());
                            cmd.Parameters.AddWithValue("@extremities", extremities.Text.Trim());
                            cmd.Parameters.AddWithValue("@neurologic", neurologic.Text.Trim());

                            cmd.Parameters.AddWithValue("@patientId", patientId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save health record: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads an existing patient health record into the UI.
        /// </summary>
        public static void LoadHealthRecord(
            int patientId,
            DataGridView pastMedicalHistoryDGV,
            DataGridView allergiesDGV,
            DataGridView familyHistoryDGV,
            DataGridView personalSocialHistoryDGV

        )
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT * FROM health_record 
                                     WHERE patient_id = @patientId 
                                     ORDER BY health_record_id DESC LIMIT 1";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@patientId", patientId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // --- Health History DGVs ---
                                SetDgvData(pastMedicalHistoryDGV, reader["past_medical_history"]?.ToString());
                                SetDgvData(allergiesDGV, reader["allergies"]?.ToString());
                                SetDgvData(familyHistoryDGV, reader["family_history"]?.ToString());
                                SetDgvData(personalSocialHistoryDGV, reader["personal_social_history"]?.ToString());

                            }
                            else
                            {
                                // No record found → clear all UI
                                ClearAll(pastMedicalHistoryDGV, allergiesDGV, familyHistoryDGV, personalSocialHistoryDGV
                                         );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load health record: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------- Helper Methods ----------------

        private static string GetDgvData(DataGridView dgv)
        {
            string result = "";
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells.Count > 0 && row.Cells[0].Value != null)
                    result += row.Cells[0].Value.ToString().Trim() + ", ";
            }
            return result.TrimEnd(',', ' ');
        }

        private static void SetDgvData(DataGridView dgv, string csv)
        {
            dgv.Rows.Clear();
            if (!string.IsNullOrWhiteSpace(csv))
            {
                string[] items = csv.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in items)
                    dgv.Rows.Add(item);
            }
        }

        private static void ClearAll(
            params Control[] ctrls
        )
        {
            foreach (var ctrl in ctrls)
            {
                switch (ctrl)
                {
                    case TextBox tb:
                        tb.Clear();
                        break;
                    case ComboBox cb:
                        cb.Text = "";
                        break;
                    case DataGridView dgv:
                        dgv.Rows.Clear();
                        break;
                }
            }
        }
        public static string GetPatientName(int patientId)
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT full_name FROM patients WHERE patient_id=@patientId LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@patientId", patientId);
                        var val = cmd.ExecuteScalar();
                        return val != null ? val.ToString() : string.Empty;
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
