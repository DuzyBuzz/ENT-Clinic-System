using MySql.Data.MySqlClient; // NuGet: MySql.Data
using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// Centralized database configuration and connection helper.
    /// </summary>
    internal static class DBConfig
    {
        // ⚠️ You can later move this into App.config for flexibility
        private static readonly string connectionString =
            "server=localhost;port=8000;user=root;password=password;database=ent_clinic_db";


         
        /// <summary> 1
        /// Returns a new MySQL connection object.
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Tests the database connection.
        /// </summary>
        /// <param name="message">Output message (success or error)</param>
        /// <returns>True if connection works, otherwise false</returns>
        public static bool TestConnection(out string message)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    message = "✅ Connection successful! Database is reachable.";
                    return true;
                }
                catch (Exception ex)
                {
                    message = "❌ Connection failed: " + ex.Message;
                    return false;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                
                }
            }
        }
    }
}
