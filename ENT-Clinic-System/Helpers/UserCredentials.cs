using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    internal static class UserCredentials
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string Fullname { get; set; }
        public static string Role { get; set; }
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Sets the connection string based on the user role.
        /// </summary>
        public static void SetConnectionString()
        {
            if (Role == "Receptionist")
                ConnectionString = "server=192.168.1.25;user=lanuser;password=password;database=ent_clinic_db;";
            else if (Role == "Doctor")
                ConnectionString = "server=localhost;user=root;password=password;database=ent_clinic_db;";
            else if (Role == "Admin")
                ConnectionString = "server=localhost;user=root;password=password;database=ent_clinic_db;";
        }

        /// <summary>
        /// Validates the user credentials against the database.
        /// Case-sensitive login using BINARY comparison.
        /// </summary>
        public static bool ValidateLogin(string username, string password, out string message)
        {
            SetConnectionString(); // ensure connection string is ready

            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();

                    // Use BINARY to enforce case-sensitive comparison
                    string query = @"
SELECT user_id, full_name, role 
FROM user 
WHERE BINARY username=@username AND BINARY password=@password AND role=@role 
LIMIT 1";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@role", Role);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UserId = reader.GetInt32("user_id");
                                Username = username;
                                Fullname = reader.GetString("full_name");
                                Role = reader.GetString("role");

                                message = $"✅ Login successful. Welcome {Fullname}!";
                                Debug.WriteLine(message);
                                return true;
                            }
                            else
                            {
                                message = "❌ Invalid username or password.";
                                return false;
                            }
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                message = "⚠️ Unable to connect to the database. Please check your internet or server connection.";
                return false;
            }
            catch (Exception ex)
            {
                message = "⚠️ Unexpected error: " + ex.Message;
                return false;
            }
        }
    }
}
//using MySql.Data.MySqlClient;
//using System;
//using System.Diagnostics;
//using System.Windows.Forms;

//namespace ENT_Clinic_System.Helpers
//{
//    internal static class UserCredentials
//    {
//        public static int UserId { get; set; }
//        public static string Username { get; set; }
//        public static string Fullname { get; set; }
//        public static string Role { get; set; }
//        public static string ConnectionString { get; set; }

//        /// <summary>
//        /// Sets the connection string based on the user role.
//        /// </summary>
//        public static void SetConnectionString()
//        {
//            try
//            {
//                string localConn = "server=localhost;user=root;password=password;database=ent_clinic_db;";
//                string receptionistIp = SettingsHelper.GetSetting("receptionist_ip");

//                if (string.Equals(Role, "Receptionist", StringComparison.OrdinalIgnoreCase))
//                {
//                    if (!string.IsNullOrWhiteSpace(receptionistIp))
//                        ConnectionString = $"server={receptionistIp};user=lanuser;password=password;database=ent_clinic_db;";
//                    else
//                    {
//                        // fallback if IP missing
//                        ConnectionString = localConn;
//                        Debug.WriteLine("Receptionist IP missing, fallback to localhost.");
//                    }
//                }
//                else if (string.Equals(Role, "Doctor", StringComparison.OrdinalIgnoreCase) ||
//                         string.Equals(Role, "Admin", StringComparison.OrdinalIgnoreCase))
//                {
//                    ConnectionString = localConn;
//                }
//                else
//                {
//                    // fallback if Role not recognized
//                    ConnectionString = localConn;
//                    Debug.WriteLine($"Unknown role '{Role}', defaulting to localhost.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // fallback for safety
//                ConnectionString = "server=localhost;user=root;password=password;database=ent_clinic_db;";
//                Debug.WriteLine("Failed to set connection string: " + ex);
//            }
//        }


//        /// <summary>
//        /// Validates the user credentials against the database.
//        /// Case-sensitive login using BINARY comparison.
//        /// </summary>
//        public static bool ValidateLogin(string username, string password, out string message)
//        {
//            SetConnectionString(); // ensure connection string is ready

//            try
//            {
//                using (var conn = new MySqlConnection(ConnectionString))
//                {
//                    conn.Open();

//                    // Use BINARY to enforce case-sensitive comparison
//                    string query = @"
//SELECT user_id, full_name, role 
//FROM user 
//WHERE BINARY username=@username AND BINARY password=@password AND role=@role 
//LIMIT 1";

//                    using (var cmd = new MySqlCommand(query, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@username", username);
//                        cmd.Parameters.AddWithValue("@password", password);
//                        cmd.Parameters.AddWithValue("@role", Role);

//                        using (var reader = cmd.ExecuteReader())
//                        {
//                            if (reader.Read())
//                            {
//                                UserId = reader.GetInt32("user_id");
//                                Username = username;
//                                Fullname = reader.GetString("full_name");
//                                Role = reader.GetString("role");

//                                message = $"✅ Login successful. Welcome {Fullname}!";
//                                Debug.WriteLine(message);
//                                return true;
//                            }
//                            else
//                            {
//                                message = "❌ Invalid username or password.";
//                                return false;
//                            }
//                        }
//                    }
//                }
//            }
//            catch (MySqlException)
//            {
//                message = "⚠️ Unable to connect to the database. Please check your internet or server connection.";
//                return false;
//            }
//            catch (Exception ex)
//            {
//                message = "⚠️ Unexpected error: " + ex.Message;
//                return false;
//            }
//        }
//    }
//}
