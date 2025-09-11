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
        // Base folder for storing patient files
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ENTClinic");

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
            ImageFlowHelper imageHelper, // updated
            VideoFlowHelper videoHelper
        )
        {
            if (inputs == null) throw new ArgumentNullException(nameof(inputs));
            if (imageHelper == null) throw new ArgumentNullException(nameof(imageHelper));
            if (videoHelper == null) throw new ArgumentNullException(nameof(videoHelper));

            List<(string Type, string Path)> savedFiles = new List<(string Type, string Path)>();

            // 1️⃣ Save consultation
            int consultationId = InsertConsultation(patientId, doctorName, consultationDate, followUpDate, inputs);
            // 2️⃣ Save Images
            foreach (var (imagePath, note, category) in imageHelper.GetAllImages())
            {
                try
                {
                    if (!File.Exists(imagePath))
                        continue; // skip missing files

                    // Create patient folder inside Documents/ENTClinic/PatientID/Images
                    string folder = Path.Combine(
                        BaseFolder,
                        patientId.ToString(),
                        "Images"
                    );
                    Directory.CreateDirectory(folder);

                    string destPath = Path.Combine(folder, Path.GetFileName(imagePath));
                    File.Copy(imagePath, destPath, true);

                    // Optional: insert attachment in DB if you track notes/categories
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



            // 3️⃣ Save Videos
            foreach (var (videoPath, note, category) in videoHelper.GetAllVideos())
            {
                try
                {
                    string folder = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        "ENTClinic",
                        patientId.ToString(),
                        "Videos"
                    );
                    Directory.CreateDirectory(folder);

                    string fileName = Path.GetFileName(videoPath);
                    string savedPath = Path.Combine(folder, fileName);
                    File.Copy(videoPath, savedPath, true);

                    InsertAttachment(
                        consultationId,
                        patientId,
                        "Video",
                        savedPath,
                        string.IsNullOrWhiteSpace(category) ? "General" : category,
                        note ?? ""
                    );

                    savedFiles.Add(("Video", savedPath));
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
