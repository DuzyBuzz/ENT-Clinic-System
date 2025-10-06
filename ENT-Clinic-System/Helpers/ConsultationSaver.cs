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
        /// Saves a consultation for a patient, including DGV data, notes, age, and attachments.
        /// Returns a list of saved files with type and path.
        /// </summary>
        public static List<(string Type, string Path)> SaveConsultation(
            int patientId,
            string doctorName,
            DateTime consultationDate,
            DateTime? followUpDate,
            ConsultationInputs inputs,
            ImageFlowHelper imageHelper,
            VideoFlowHelper videoHelper
        )
        {
            if (inputs == null) throw new ArgumentNullException(nameof(inputs));
            if (imageHelper == null) throw new ArgumentNullException(nameof(imageHelper));
            if (videoHelper == null) throw new ArgumentNullException(nameof(videoHelper));

            List<(string Type, string Path)> savedFiles = new List<(string Type, string Path)>();

            // 1️⃣ Insert consultation record into DB
            int consultationId = InsertConsultation(patientId, doctorName, consultationDate, followUpDate, inputs);

            // 2️⃣ Prepare folder for attachments
            string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
            string baseFolder = Path.Combine(@"D:\ENT_CLINIC_Attachments", patientId.ToString(), dateFolder);

            // 3️⃣ Save images
            foreach (var (imagePath, note, category) in imageHelper.GetAllImages())
            {
                try
                {
                    if (!File.Exists(imagePath)) continue;

                    string folder = Path.Combine(baseFolder, "Images");
                    Directory.CreateDirectory(folder);

                    string fileName = Path.GetFileNameWithoutExtension(imagePath);
                    string extension = Path.GetExtension(imagePath);
                    string timestamp = DateTime.Now.ToString("HHmmssfff");
                    string destPath = Path.Combine(folder, $"{fileName}_{timestamp}{extension}");

                    File.Copy(imagePath, destPath, true);

                    InsertAttachment(consultationId, patientId, "Image", destPath,
                        string.IsNullOrWhiteSpace(category) ? "General" : category,
                        note ?? "");

                    savedFiles.Add(("Image", destPath));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to save image: {ex.Message}");
                }
            }

            // 4️⃣ Save videos
            foreach (var (videoPath, note, category) in videoHelper.GetAllVideos())
            {
                try
                {
                    string folder = Path.Combine(baseFolder, "Videos");
                    Directory.CreateDirectory(folder);

                    string fileName = Path.GetFileNameWithoutExtension(videoPath);
                    string extension = Path.GetExtension(videoPath);
                    string timestamp = DateTime.Now.ToString("HHmmssfff");
                    string destPath = Path.Combine(folder, $"{fileName}_{timestamp}{extension}");

                    File.Copy(videoPath, destPath, true);

                    InsertAttachment(consultationId, patientId, "Video", destPath,
                        string.IsNullOrWhiteSpace(category) ? "General" : category,
                        note ?? "");

                    savedFiles.Add(("Video", destPath));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to save video: {ex.Message}");
                }
            }

            return savedFiles;
        }

        #region Database Helpers

        private static int InsertConsultation(int patientId, string doctorName, DateTime consultationDate, DateTime? followUpDate, ConsultationInputs inputs)
        {
            int consultationId = 0;

            using (MySqlConnection conn = DBConfig.GetConnection())
            {
                conn.Open();

                string sql = @"
                    INSERT INTO consultation
                        (patient_id, doctor_name, consultation_date, chief_complaint, history, ear_exam, nose_exam, throat_exam, neck_exam,
                         diagnosis, recommendations, notes, follow_up_date, follow_up_notes, age)
                    VALUES
                        (@patient_id, @doctor_name, @consultation_date, @chief_complaint, @history, @ear_exam, @nose_exam, @throat_exam, @neck_exam,
                         @diagnosis, @recommendations, @notes, @follow_up_date, @follow_up_notes, @age);
                    SELECT LAST_INSERT_ID();
                ";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@patient_id", patientId);
                    cmd.Parameters.AddWithValue("@doctor_name", doctorName ?? "");
                    cmd.Parameters.AddWithValue("@consultation_date", consultationDate);

                    // Convert DataGridViews into comma-separated strings
                    cmd.Parameters.AddWithValue("@chief_complaint", DgvToCsv(inputs.ComplaintsDGV));
                    cmd.Parameters.AddWithValue("@history", DgvToCsv(inputs.RecentIllnessDGV) + (string.IsNullOrEmpty(DgvToCsv(inputs.PastMedicalHistoryDGV)) ? "" : ", " + DgvToCsv(inputs.PastMedicalHistoryDGV)));
                    cmd.Parameters.AddWithValue("@ear_exam", DgvToCsv(inputs.EarsDGV));
                    cmd.Parameters.AddWithValue("@nose_exam", DgvToCsv(inputs.NoseDGV));
                    cmd.Parameters.AddWithValue("@throat_exam", DgvToCsv(inputs.ThroatDGV));
                    cmd.Parameters.AddWithValue("@neck_exam", DgvToCsv(inputs.NeckDGV));
                    cmd.Parameters.AddWithValue("@diagnosis", DgvToCsv(inputs.DiagnosisDGV));
                    cmd.Parameters.AddWithValue("@recommendations", DgvToCsv(inputs.RecommendationsDGV));
                    cmd.Parameters.AddWithValue("@notes", inputs.NoteRichText?.Text ?? "");
                    cmd.Parameters.AddWithValue("@follow_up_date", followUpDate.HasValue ? followUpDate.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@follow_up_notes", inputs.NoteRichText?.Text ?? "");
                    cmd.Parameters.AddWithValue("@age", inputs.ageLabel?.Text ?? "");

                    consultationId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return consultationId;
        }

        /// <summary>
        /// Converts DataGridView rows into a comma-separated string
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
                    string val = cell.Value?.ToString().Trim();
                    if (!string.IsNullOrEmpty(val) && !values.Contains(val)) // remove duplicates
                        values.Add(val);
                }
            }

            return string.Join(", ", values);
        }

        private static void InsertAttachment(int consultationId, int patientId, string fileType, string path, string category, string note)
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
    /// Container for user input controls in the consultation form
    /// </summary>
    public class ConsultationInputs
    {
        public Label ageLabel { get; set; }
        public DataGridView ComplaintsDGV { get; set; }
        public DataGridView RecentIllnessDGV { get; set; }
        public DataGridView PastMedicalHistoryDGV { get; set; }
        public DataGridView EarsDGV { get; set; }
        public DataGridView NoseDGV { get; set; }
        public DataGridView ThroatDGV { get; set; }
        public DataGridView NeckDGV { get; set; }
        public DataGridView DiagnosisDGV { get; set; }
        public DataGridView ProceduresDGV { get; set; }
        public DataGridView RecommendationsDGV { get; set; }
        public RichTextBox NoteRichText { get; set; }
        public FlowLayoutPanel ImageFlowLayout { get; set; }
        public FlowLayoutPanel VideoFlowLayout { get; set; }

        // ✅ New properties for CSV strings (optional if you want to store directly as strings)
        public string ComplaintsCsv { get; set; }
        public string RecentIllnessCsv { get; set; }
        public string PastMedicalHistoryCsv { get; set; }
        public string EarsCsv { get; set; }
        public string NoseCsv { get; set; }
        public string ThroatCsv { get; set; }
        public string NeckCsv { get; set; }
        public string DiagnosisCsv { get; set; }
        public string ProceduresCsv { get; set; }
        public string RecommendationsCsv { get; set; }
    }
}
