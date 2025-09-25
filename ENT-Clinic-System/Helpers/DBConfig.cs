using MySql.Data.MySqlClient;
using System;

namespace ENT_Clinic_System.Helpers
{
    internal static class DBConfig
    {
        /// <summary>
        /// Returns a new MySQL connection using the current UserCredentials.ConnectionString.
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            // Safety check: must select role first
            if (string.IsNullOrWhiteSpace(UserCredentials.ConnectionString))
            {
                throw new InvalidOperationException("❌ Connection string is not set. Please select a role (Doctor or Receptionist) first.");
            }

            return new MySqlConnection(UserCredentials.ConnectionString);
        }

        /// <summary>
        /// Tests the database connection. Returns true if connection succeeds, false otherwise.
        /// </summary>
        public static bool TestConnection(out string message)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open(); // test the connection
                    message = $"✅ Connection successful as {UserCredentials.Role}!";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = "❌ Connection failed: " + ex.Message;
                return false;
            }
        }
    }
}
