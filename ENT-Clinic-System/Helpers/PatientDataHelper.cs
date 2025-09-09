using MySql.Data.MySqlClient;
using System;

namespace ENT_Clinic_System.Helpers
{
    internal static class PatientDataHelper
    {
        /// <summary>
        /// Get a single column value from patients table by patient_id.
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <param name="columnName">Column name to fetch</param>
        /// <returns>Value as string, or empty string if not found</returns>
        public static string GetPatientValue(int patientId, string columnName)
        {
            // Safety check - prevents SQL injection by only allowing known column names
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
                    // You can log the error or rethrow
                    throw new Exception("Error fetching patient data: " + ex.Message);
                }
            }

            return value;
        }
    }
}
