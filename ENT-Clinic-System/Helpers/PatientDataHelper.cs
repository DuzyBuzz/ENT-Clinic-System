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
        /// Returns empty string if column is missing or value is null.
        /// </summary>
        public static string GetPatientValue(int patientId, string columnName)
        {
            string[] allowedColumns = {
                "patient_id", "full_name", "address", "birth_date", "age",
                "sex", "civil_status", "patient_contact_number",
                "emergency_name", "emergency_contact_number", "emergency_relationship", "referred_by"
            };

            if (Array.IndexOf(allowedColumns, columnName) == -1)
            {
                // Log warning and return empty string
                Console.WriteLine($"Warning: Requested invalid column '{columnName}' for patient {patientId}");
                return string.Empty;
            }

            try
            {
                 var conn = DBConfig.GetConnection();
                 var cmd = new MySqlCommand($"SELECT {columnName} FROM patients WHERE patient_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", patientId);

                conn.Open();
                var result = cmd.ExecuteScalar();

                return result?.ToString() ?? string.Empty; // Safe null handling
            }
            catch (Exception ex)
            {
                // Log error and return empty string
                Console.WriteLine($"Error fetching patient data for ID {patientId}, column {columnName}: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Get patient photo as Image by patient_id.
        /// Returns null if no photo or an error occurs.
        /// </summary>
        public static Image GetPatientPhoto(int patientId)
        {
            try
            {
                 var conn = DBConfig.GetConnection();
                 var cmd = new MySqlCommand("SELECT photo FROM patients WHERE patient_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", patientId);

                conn.Open();
                var result = cmd.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    return null; // No photo

                var photoBytes = result as byte[];
                if (photoBytes == null || photoBytes.Length == 0)
                    return null;

                 var ms = new MemoryStream(photoBytes);
                return Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                // Log error and return null
                Console.WriteLine($"Error fetching photo for patient {patientId}: {ex.Message}");
                return null;
            }
        }
    }
}
