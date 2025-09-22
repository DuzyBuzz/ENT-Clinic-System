using ENT_Clinic_System.CustomUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ENT_Clinic_System.Helpers
{
    public static class ConsultationSaver
    {
        /// <summary>
        /// Saves a consultation with images and videos for a patient.
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

            // Insert consultation record into DB
            int consultationId = InsertConsultation(patientId, doctorName, consultationDate, followUpDate, inputs);

            // Current date string for folder organization
            string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");

            // Base path for attachments
            string baseFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "ENT_CLINIC_Attachments",
                patientId.ToString(),
                dateFolder
            );

            // -----------------------
            // Save Images
            // -----------------------
            foreach (var (imagePath, note, category) in imageHelper.GetAllImages())
            {
                try
                {
                    if (!File.Exists(imagePath))
                        continue;

                    // Create folder: .../{patient_id}/{date}/Images
                    string folder = Path.Combine(baseFolder, "Images");
                    Directory.CreateDirectory(folder);

                    // Append timestamp to file name to prevent overwriting
                    string fileName = Path.GetFileNameWithoutExtension(imagePath);
                    string extension = Path.GetExtension(imagePath);
                    string timestamp = DateTime.Now.ToString("HHmmssfff"); // hours, minutes, seconds, milliseconds
                    string destPath = Path.Combine(folder, $"{fileName}_{timestamp}{extension}");

                    File.Copy(imagePath, destPath, true);

                    InsertAttachment(
                        consultationId,
                        patientId,
                        "Image",
                        destPath,
                        string.IsNullOrWhiteSpace(category) ? "General" : category,
                        note ?? ""
                    );

                    savedFiles.Add(("Image", destPath));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to save image: {ex.Message}");
                }
            }

            // -----------------------
            // Save Videos
            // -----------------------
            foreach (var (videoPath, note, category) in videoHelper.GetAllVideos())
            {
                try
                {
                    // Create folder: .../{patient_id}/{date}/Videos
                    string folder = Path.Combine(baseFolder, "Videos");
                    Directory.CreateDirectory(folder);

                    // Append timestamp to file name to prevent overwriting
                    string fileName = Path.GetFileNameWithoutExtension(videoPath);
                    string extension = Path.GetExtension(videoPath);
                    string timestamp = DateTime.Now.ToString("HHmmssfff");
                    string destPath = Path.Combine(folder, $"{fileName}_{timestamp}{extension}");

                    File.Copy(videoPath, destPath, true);

                    InsertAttachment(
                        consultationId,
                        patientId,
                        "Video",
                        destPath,
                        string.IsNullOrWhiteSpace(category) ? "General" : category,
                        note ?? ""
                    );

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
                        (patient_id, doctor_name, consultation_date, chief_complaint, history, ear_exam, nose_exam, throat_exam, diagnosis, recommendations, notes, follow_up_date, follow_up_notes)
                    VALUES
                        (@patient_id, @doctor_name, @consultation_date, @chief_complaint, @history, @ear_exam, @nose_exam, @throat_exam, @diagnosis, @recommendations, @notes, @follow_up_date, @follow_up_notes);
                    SELECT LAST_INSERT_ID();
                ";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@patient_id", patientId);
                    cmd.Parameters.AddWithValue("@doctor_name", doctorName ?? "");
                    cmd.Parameters.AddWithValue("@consultation_date", consultationDate);
                    cmd.Parameters.AddWithValue("@chief_complaint", inputs.ComplaintsRichText.Text);
                    cmd.Parameters.AddWithValue("@history", inputs.IllnessHistoryRichText.Text);
                    cmd.Parameters.AddWithValue("@ear_exam", inputs.EarsRichText.Text);
                    cmd.Parameters.AddWithValue("@nose_exam", inputs.NoseRichText.Text);
                    cmd.Parameters.AddWithValue("@throat_exam", inputs.ThroatRichText.Text);
                    cmd.Parameters.AddWithValue("@diagnosis", inputs.DiagnosisRichText.Text);
                    cmd.Parameters.AddWithValue("@recommendations", inputs.RecommendationRichText.Text);
                    cmd.Parameters.AddWithValue("@notes", inputs.NoteRichText.Text);
                    cmd.Parameters.AddWithValue("@follow_up_date", followUpDate.HasValue ? followUpDate.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@follow_up_notes", DBNull.Value);

                    consultationId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return consultationId;
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
        public RichTextBox ComplaintsRichText { get; set; }
        public RichTextBox IllnessHistoryRichText { get; set; }
        public RichTextBox EarsRichText { get; set; }
        public RichTextBox NoseRichText { get; set; }
        public RichTextBox ThroatRichText { get; set; }
        public RichTextBox DiagnosisRichText { get; set; }
        public RichTextBox RecommendationRichText { get; set; }
        public RichTextBox NoteRichText { get; set; }

        public FlowLayoutPanel ImageFlowLayout { get; set; }
        public FlowLayoutPanel VideoFlowLayout { get; set; }
    }
}
