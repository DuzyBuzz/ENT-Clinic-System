using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace ENT_Clinic_System.Helpers
{
    internal static class DBConfig
    {
        /// <summary>
        /// Holds the active connection string used throughout the system.
        /// </summary>
        public static string ConnectionString { get; private set; }

        /// <summary>
        /// Assigns the connection string based on the user role.
        /// Uses names from App.config.
        /// </summary>
        public static void SetConnectionString(string role)
        {
            string keyName;

            switch (role)
            {
                case "Receptionist":
                    keyName = "ReceptionistConnection";
                    break;

                case "Doctor":
                    keyName = "DoctorConnection";
                    break;

                case "Admin":
                    keyName = "AdminConnection";
                    break;

                default:
                    throw new ArgumentException("Invalid role specified.");
            }

            var configValue = ConfigurationManager.ConnectionStrings[keyName];

            if (configValue == null)
                throw new Exception($"Connection string '{keyName}' not found in App.config.");

            ConnectionString = configValue.ConnectionString;
        }

        /// <summary>
        /// Creates a new MySQL connection using the active connection string.
        /// </summary>
        public static MySqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(ConnectionString))
                throw new InvalidOperationException("Connection string not set. Call SetConnectionString() first.");

            return new MySqlConnection(ConnectionString);
        }

        /// <summary>
        /// Tests if the connection can be opened.
        /// </summary>
        public static bool TestConnection(out string message)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    message = "Connection successful.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = "Connection failed: " + ex.Message;
                return false;
            }
        }
    }
}
