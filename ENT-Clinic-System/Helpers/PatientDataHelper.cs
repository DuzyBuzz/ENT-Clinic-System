using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;

namespace ENT_Clinic_System.Helpers
{
    internal static class PatientDataHelper
    {
        /// <summary>
        /// Get a single column value from patients table by patient_id.
        /// </summary>
        public static string GetPatientValue(int patientId, string columnName)
        {
            string[] allowedColumns = {
                "patient_id", "full_name", "address", "birth_date", "age",
                "sex", "civil_status", "patient_contact_number",
                "emergency_name", "emergency_contact_number", "emergency_relationship"
            };

            if (Array.IndexOf(allowedColumns, columnName) == -1)
                throw new ArgumentException("Invalid column name requested.");

            string value = string.Empty;

            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand($"SELECT {columnName} FROM patients WHERE patient_id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", patientId);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        value = result.ToString();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching patient data: " + ex.Message);
                }
            }

            return value;
        }

        /// <summary>
        /// Get patient photo as Image by patient_id
        /// </summary>
        public static Image GetPatientPhoto(int patientId)
        {
            byte[] photoBytes = null;

            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand("SELECT photo FROM patients WHERE patient_id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", patientId);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                        photoBytes = (byte[])result;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching patient photo: " + ex.Message);
                }
            }

            if (photoBytes != null)
            {
                using (var ms = new MemoryStream(photoBytes))
                {
                    return Image.FromStream(ms);
                }
            }

            return null; // return null if no photo
        }
    }
}
