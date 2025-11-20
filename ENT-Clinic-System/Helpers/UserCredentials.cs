using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;

namespace ENT_Clinic_System.Helpers
{
    internal static class UserCredentials
    {
        public static int UserId { get; private set; }
        public static string Username { get; internal set; }
        public static string Fullname { get; internal set; }
        public static string Role { get; set; }


        /// <summary>
        /// Validates the login credentials and loads user info into memory.
        /// </summary>
        public static bool ValidateLogin(string username, string password, out string message)
        {
            // ---------- BASIC VALIDATION ----------
            if (string.IsNullOrWhiteSpace(Role))
            {
                message = "Please select a role.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                message = "Username and password are required.";
                return false;
            }

            try
            {
                // ---------- LOAD ROLE-BASED CONNECTION ----------
                DBConfig.SetConnectionString(Role);

                using (var conn = new MySqlConnection(DBConfig.ConnectionString))
                {
                    conn.Open();

                    const string query = @"
                        SELECT user_id, full_name, role 
                        FROM user
                        WHERE 
                            BINARY username = @username AND 
                            BINARY password = @password AND 
                            role = @role
                        LIMIT 1;
                    ";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username.Trim());
                        cmd.Parameters.AddWithValue("@password", password.Trim());
                        cmd.Parameters.AddWithValue("@role", Role);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Load user session
                                UserId = reader.GetInt32("user_id");
                                Username = username.Trim();
                                Fullname = reader.GetString("full_name");
                                Role = reader.GetString("role");

                                message = $"Login successful. Welcome {Fullname}.";
                                Debug.WriteLine(message);

                                return true;
                            }
                            else
                            {
                                message = "Invalid username, password, or role.";
                                return false;
                            }
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                message = "Cannot connect to the database. Check your network or server.";
                return false;
            }
            catch (Exception ex)
            {
                message = "Unexpected error: " + ex.Message;
                return false;
            }
        }
    }
}
