using MySql.Data.MySqlClient;
using System;

namespace ENT_Clinic_System.Helpers
{
    internal static class DBConfig
    {
        private static readonly string primaryConnectionString =
            "server=192.168.1.100;port=8000;user=root;password=password;database=ent_clinic_db";

        private static readonly string fallbackConnectionString =
            "server=localhost;port=8000;user=root;password=password;database=ent_clinic_db";

        /// <summary>
        /// Returns a new MySQL connection (closed by default).
        /// Fallback to localhost if LAN IP fails.
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            // Try primary connection first
            try
            {
                var conn = new MySqlConnection(primaryConnectionString);
                // Test opening without keeping it open
                conn.Open();
                conn.Close();
                return conn; // connection returned closed
            }
            catch
            {
                // Fallback to localhost
                var conn = new MySqlConnection(fallbackConnectionString);
                conn.Open();
                conn.Close();
                return conn; // returned closed
            }
        }

        /// <summary>
        /// Tests the database connection.
        /// </summary>
        public static bool TestConnection(out string message)
        {
            MySqlConnection conn = null;
            try
            {
                conn = GetConnection();
                conn.Open(); // open only for testing
                message = "✅ Connection successful!";
                return true;
            }
            catch (Exception ex)
            {
                message = "❌ Connection failed: " + ex.Message;
                return false;
            }
            finally
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
