using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;

namespace ENT_Clinic_System.Helpers
{
    internal static class UserCredentials
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string Fullname { get; set; }
        public static string Role { get; set; }
        public static string ConnectionString { get; set; }

        public static void SetConnectionString()
        {
            if (Role == "Receptionist")
            {
                ConnectionString = "server=localhost;user=root;password=password;database=ent_clinic_db;";
            }
            else if (Role == "Doctor")
            {
                ConnectionString = "server=localhost;user=root;password=password;database=ent_clinic_db;";
            }
            else if (Role == "Admin")
            {
                ConnectionString = "server=localhost;user=root;password=password;database=ent_clinic_db;";
            }
        }

        /// <summary>
        /// Validates the user credentials against the database.
        /// Assumes there is a `users` table with columns: id, username, password, fullname, role
        /// </summary>
        public static bool ValidateLogin(string username, string password, out string message)
        {
            SetConnectionString(); // ensure connection string is ready

            using (var conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT user_id, full_name, role FROM user WHERE username=@username AND password=@password AND role=@role LIMIT 1";


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
                catch (Exception ex)
                {
                    message = "❌ Database error: " + ex.Message;
                    return false;
                }
            }
        }
    }
}
