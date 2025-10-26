using ENT_Clinic_System.CustomUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ENT_Clinic_System.Helpers
{
    public static class ConsultationSaver
    {
        /// <summary>
        /// Saves a consultation for a patient, including notes, DGV data, and attachments.
        /// Returns a list of saved files with their type and path.
        /// </summary>
        public static List<Tuple<string, string>> SaveConsultation(
            int patientId,
            string doctorName,
            DateTime consultationDate,
            DateTime? followUpDate,
            ConsultationInputs inputs,
            ImageFlowHelper imageHelper,
            VideoFlowHelper videoHelper
        )
        {
            if (inputs == null) throw new ArgumentNullException("inputs");
            if (imageHelper == null) throw new ArgumentNullException("imageHelper");
            if (videoHelper == null) throw new ArgumentNullException("videoHelper");

            List<Tuple<string, string>> savedFiles = new List<Tuple<string, string>>();

            // 1️⃣ Insert consultation record into DB
            int consultationId = InsertConsultation(patientId, doctorName, consultationDate, followUpDate, inputs);

            // 2️⃣ Prepare base folder for attachments
            string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
            string baseFolder = Path.Combine(SettingsHelper.GetSetting("base_path"), patientId.ToString(), dateFolder);

            // 3️⃣ Save Images
            foreach (var imageInfo in imageHelper.GetAllImages())
            {
                string imagePath = imageInfo.Item1;
                string note = imageInfo.Item2;
                string category = imageInfo.Item3;

                try
                {
                    if (!File.Exists(imagePath)) continue;

                    string folder = Path.Combine(baseFolder, "Images");
                    Directory.CreateDirectory(folder);

                    string fileName = Path.GetFileNameWithoutExtension(imagePath);
                    string extension = Path.GetExtension(imagePath);
                    string timestamp = DateTime.Now.ToString("HHmmssfff");
                    string destPath = Path.Combine(folder, fileName + "_" + timestamp + extension);

                    File.Copy(imagePath, destPath, true);

                    InsertAttachment(consultationId, patientId, "Image", destPath,
                        string.IsNullOrWhiteSpace(category) ? "General" : category,
                        note ?? "");

                    savedFiles.Add(new Tuple<string, string>("Image", destPath));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Failed to save image: " + ex.Message);
                }
            }

            // 4️⃣ Save Videos
            foreach (var videoInfo in videoHelper.GetAllVideos())
            {
                string videoPath = videoInfo.Item1;
                string note = videoInfo.Item2;
                string category = videoInfo.Item3;

                try
                {
                    if (!File.Exists(videoPath)) continue;

                    string folder = Path.Combine(baseFolder, "Videos");
                    Directory.CreateDirectory(folder);

                    string fileName = Path.GetFileNameWithoutExtension(videoPath);
                    string extension = Path.GetExtension(videoPath);
                    string timestamp = DateTime.Now.ToString("HHmmssfff");
                    string destPath = Path.Combine(folder, fileName + "_" + timestamp + extension);

                    File.Copy(videoPath, destPath, true);

                    InsertAttachment(consultationId, patientId, "Video", destPath,
                        string.IsNullOrWhiteSpace(category) ? "General" : category,
                        note ?? "");

                    savedFiles.Add(new Tuple<string, string>("Video", destPath));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Failed to save video: " + ex.Message);
                }
            }

            return savedFiles;
        }

        #region 🗄️ Database Helpers

        /// <summary>
        /// Inserts a new consultation record into the database and returns the inserted ID.
        /// </summary>
        private static int InsertConsultation(
            int patientId,
            string doctorName,
            DateTime consultationDate,
            DateTime? followUpDate,
            ConsultationInputs inputs)
        {
            int consultationId = 0;

            using (MySqlConnection conn = DBConfig.GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO consultation
                        (patient_id, doctor_name, consultation_date, 
                         chief_complaint, history, ear_exam, nose_exam, throat_exam,
                         diagnosis, recommendations, notes, follow_up_date, 
                         follow_up_notes, age, others_exam)
                    VALUES
                        (@patient_id, @doctor_name, @consultation_date, 
                         @chief_complaint, @history, @ear_exam, @nose_exam, @throat_exam,
                         @diagnosis, @recommendations, @notes, @follow_up_date, 
                         @follow_up_notes, @age, @others_exam);
                    SELECT LAST_INSERT_ID();
                ";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@patient_id", patientId);
                    cmd.Parameters.AddWithValue("@doctor_name", doctorName ?? "");
                    cmd.Parameters.AddWithValue("@consultation_date", consultationDate);

                    // RichText fields
                    cmd.Parameters.AddWithValue("@chief_complaint", inputs.ComplaintsText ?? "");
                    cmd.Parameters.AddWithValue("@history", inputs.RecentIllnessText ?? "");

                    // DGV CSVs
                    cmd.Parameters.AddWithValue("@ear_exam", inputs.EarsCsv ?? "");
                    cmd.Parameters.AddWithValue("@nose_exam", inputs.NoseCsv ?? "");
                    cmd.Parameters.AddWithValue("@throat_exam", inputs.ThroatCsv ?? "");
                    cmd.Parameters.AddWithValue("@others_exam", inputs.OthersCsv ?? "");
                    cmd.Parameters.AddWithValue("@diagnosis", inputs.DiagnosisCsv ?? "");
                    cmd.Parameters.AddWithValue("@recommendations", inputs.RecommendationsCsv ?? "");

                    // Notes & follow-up
                    string notesText = inputs.NoteRichText != null ? inputs.NoteRichText.Text : "";
                    cmd.Parameters.AddWithValue("@notes", notesText);
                    cmd.Parameters.AddWithValue("@follow_up_date", followUpDate.HasValue ? (object)followUpDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@follow_up_notes", notesText);
                    cmd.Parameters.AddWithValue("@age", inputs.ageLabel != null ? inputs.ageLabel.Text : "");

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        consultationId = Convert.ToInt32(result);
                }
            }

            return consultationId;
        }

        /// <summary>
        /// Combines recent illness and past medical history text into one clean string.
        /// </summary>
        private static string CombineHistory(string recentIllness, string pastHistoryCsv)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(recentIllness))
                sb.Append(recentIllness.Trim());

            if (!string.IsNullOrWhiteSpace(pastHistoryCsv))
            {
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.Append(pastHistoryCsv.Trim());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts DataGridView values to a CSV string (optional helper).
        /// </summary>
        private static string DgvToCsv(DataGridView dgv)
        {
            if (dgv == null || dgv.Rows.Count == 0) return "";

            List<string> values = new List<string>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    string val = cell.Value != null ? cell.Value.ToString().Trim() : null;
                    if (!string.IsNullOrEmpty(val) && !values.Contains(val))
                        values.Add(val);
                }
            }

            return string.Join(", ", values.ToArray());
        }

        /// <summary>
        /// Inserts attachment metadata (Image/Video) into the database.
        /// </summary>
        private static void InsertAttachment(
            int consultationId,
            int patientId,
            string fileType,
            string path,
            string category,
            string note)
        {
            using (MySqlConnection conn = DBConfig.GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO attachments 
                        (consultation_id, patient_id, file_type, file_path, category, note)
                    VALUES
                        (@consultation_id, @patient_id, @file_type, @file_path, @category, @note);
                ";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@consultation_id", consultationId);
                    cmd.Parameters.AddWithValue("@patient_id", patientId);
                    cmd.Parameters.AddWithValue("@file_type", fileType);
                    cmd.Parameters.AddWithValue("@file_path", path);
                    cmd.Parameters.AddWithValue("@category", string.IsNullOrWhiteSpace(category) ? "General" : category);
                    cmd.Parameters.AddWithValue("@note", note ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Container for user input controls in the consultation form.
    /// </summary>
    public class ConsultationInputs
    {
        // 🩺 UI Components
        public Label ageLabel { get; set; }

        // 🔹 RichText fields
        public string ComplaintsText { get; set; }
        public string RecentIllnessText { get; set; }

        // 🔹 DataGridViews (structured input)
        public DataGridView PastMedicalHistoryDGV { get; set; }
        public DataGridView EarsDGV { get; set; }
        public DataGridView NoseDGV { get; set; }
        public DataGridView ThroatDGV { get; set; }
        public DataGridView DiagnosisDGV { get; set; }
        public DataGridView ProceduresDGV { get; set; }
        public DataGridView RecommendationsDGV { get; set; }

        // 🔹 Pre-converted CSV strings
        public string PastMedicalHistoryCsv { get; set; }
        public string EarsCsv { get; set; }
        public string NoseCsv { get; set; }
        public string ThroatCsv { get; set; }
        public string OthersCsv { get; set; }
        public string DiagnosisCsv { get; set; }
        public string ProceduresCsv { get; set; }
        public string RecommendationsCsv { get; set; }

        // 🔹 Notes and Media
        public RichTextBox NoteRichText { get; set; }
        public FlowLayoutPanel ImageFlowLayout { get; set; }
        public FlowLayoutPanel VideoFlowLayout { get; set; }
    }
}
