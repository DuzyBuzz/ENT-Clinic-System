//using ENT_Clinic_System.Helpers;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Windows.Forms;
//using MySql.Data.MySqlClient;
//using System.Diagnostics;

//namespace ENT_Clinic_System.Helpers
//{
//    public static class ConsultationSaver
//    {
//        /// <summary>
//        /// Saves a consultation with attachments for a patient.
//        /// </summary>
//        public static void SaveConsultation(
//            int patientId,
//            string doctorName,
//            DateTime consultationDate,
//            DateTime? followUpDate,
//            ConsultationInputs inputs,
//            FlowLayoutHelper imageHelper,
//            VideoFlowHelper videoHelper) 
//        {
//            // 1️⃣ Save consultation data and get inserted consultation ID
//            int consultationId = InsertConsultation(patientId, doctorName, consultationDate, followUpDate, inputs);

//            // 2️⃣ Save Images
//            var allImages = imageHelper.GetAllImages(); 
//            Debug.WriteLine($"Found {allImages.Count} images to save.");

//            foreach (var (image, note, category) in allImages)
//            {
//                try
//                {
//                    // Generate unique file name
//                    string fileName = Guid.NewGuid().ToString() + ".png";

//                    // Save the image to patient folder
//                    string savedPath = PatientFileHelper.SaveImage(
//                        patientId,
//                        consultationDate,
//                        image,
//                        fileName
//                    );

//                    Debug.WriteLine($"✅ Saved image: {savedPath} | Note: {note} | Category: {category}");

//                    // Insert into DB
//                    InsertAttachment(
//                        consultationId,
//                        patientId,
//                        "Image",
//                        savedPath,
//                        string.IsNullOrWhiteSpace(category) ? "General" : category,
//                        string.IsNullOrWhiteSpace(note) ? "" : note
//                    );
//                }
//                catch (Exception ex)
//                {
//                    Debug.WriteLine($"❌ Failed to save image: {ex.Message}");
//                }
//            }

//            // 3️⃣ Save Videos (unchanged)
//            foreach (var (videoPath, note, category) in videoHelper.GetAllVideos())
//            {
//                string fileName = Path.GetFileName(videoPath);
//                string savedPath = PatientFileHelper.SaveVideo(
//                    patientId,
//                    consultationDate,
//                    videoPath,
//                    fileName
//                );

//                InsertAttachment(consultationId, patientId, "Video", savedPath, category, note);
//            }
//        }

//        #region Database Helpers

//        private static int InsertConsultation(int patientId, string doctorName, DateTime consultationDate, DateTime? followUpDate, ConsultationInputs inputs)
//        {
//            int consultationId = 0;

//            using (MySqlConnection conn = DBConfig.GetConnection())
//            {
//                conn.Open();

//                string sql = @"
//                INSERT INTO consultation 
//                    (patient_id, doctor_name, consultation_date, chief_complaint, history, ear_exam, nose_exam, throat_exam, diagnosis, recommendations, notes, follow_up_date, follow_up_notes)
//                VALUES
//                    (@patient_id, @doctor_name, @consultation_date, @chief_complaint, @history, @ear_exam, @nose_exam, @throat_exam, @diagnosis, @recommendations, @notes, @follow_up_date, @follow_up_notes);
//                SELECT LAST_INSERT_ID();
//            ";

//                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
//                {
//                    cmd.Parameters.AddWithValue("@patient_id", patientId);
//                    cmd.Parameters.AddWithValue("@doctor_name", doctorName ?? "");
//                    cmd.Parameters.AddWithValue("@consultation_date", consultationDate);

//                    cmd.Parameters.AddWithValue("@chief_complaint", inputs.ComplaintsRichText.Text);
//                    cmd.Parameters.AddWithValue("@history", inputs.IllnessHistoryRichText.Text);
//                    cmd.Parameters.AddWithValue("@ear_exam", inputs.EarsRichText.Text);
//                    cmd.Parameters.AddWithValue("@nose_exam", inputs.NoseRichText.Text);
//                    cmd.Parameters.AddWithValue("@throat_exam", inputs.ThroatRichText.Text);
//                    cmd.Parameters.AddWithValue("@diagnosis", inputs.DiagnosisRichText.Text);
//                    cmd.Parameters.AddWithValue("@recommendations", inputs.RecommendationRichText.Text);
//                    cmd.Parameters.AddWithValue("@notes", inputs.NoteRichText.Text);

//                    cmd.Parameters.AddWithValue("@follow_up_date", followUpDate.HasValue ? followUpDate.Value : (object)DBNull.Value);
//                    cmd.Parameters.AddWithValue("@follow_up_notes", DBNull.Value);

//                    consultationId = Convert.ToInt32(cmd.ExecuteScalar());
//                }
//            }

//            return consultationId;
//        }

//        private static void InsertAttachment(int consultationId, int patientId, string fileType, string path, string category, string note)
//        {
//            using (MySqlConnection conn = DBConfig.GetConnection())
//            {
//                conn.Open();

//                string sql = @"
//        INSERT INTO attachments 
//            (consultation_id, patient_id, file_type, file_path, category, note)
//        VALUES
//            (@consultation_id, @patient_id, @file_type, @file_path, @category, @note);
//        ";

//                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
//                {
//                    cmd.Parameters.AddWithValue("@consultation_id", consultationId);
//                    cmd.Parameters.AddWithValue("@patient_id", patientId);
//                    cmd.Parameters.AddWithValue("@file_type", fileType);
//                    cmd.Parameters.AddWithValue("@file_path", path);
//                    cmd.Parameters.AddWithValue("@category", category ?? "General");
//                    cmd.Parameters.AddWithValue("@note", note ?? "");

//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }

//        #endregion
//    }

//    /// <summary>
//    /// Container for user input controls in the consultation form
//    /// </summary>
//    public class ConsultationInputs
//    {
//        public RichTextBox ComplaintsRichText { get; set; }
//        public RichTextBox IllnessHistoryRichText { get; set; }
//        public RichTextBox EarsRichText { get; set; }
//        public RichTextBox NoseRichText { get; set; }
//        public RichTextBox ThroatRichText { get; set; }
//        public RichTextBox DiagnosisRichText { get; set; }
//        public RichTextBox RecommendationRichText { get; set; }
//        public RichTextBox NoteRichText { get; set; }

//        public FlowLayoutPanel ImageFlowLayout { get; set; }
//        public FlowLayoutPanel VideoFlowLayout { get; set; }
//    }
//}
