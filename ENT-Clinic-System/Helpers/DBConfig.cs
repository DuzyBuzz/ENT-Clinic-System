using MySql.Data.MySqlClient;
using System;

namespace ENT_Clinic_System.Helpers
{
    internal static class DBConfig
    {
        // Only use localhost connection
        private static readonly string connectionString =
            "server=localhost;port=3306;user=root;password=password;database=ent_clinic_db";

        /// <summary>
        /// Returns a new MySQL connection (closed by default).
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Tests the database connection.
        /// </summary>
        public static bool TestConnection(out string message)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open(); // open only for testing
                    message = "✅ Connection successful!";
                    return true;
                }
                catch (Exception ex)
                {
                    message = "❌ Connection failed: " + ex.Message;
                    return false;
                }
            }
        }
    }
}
