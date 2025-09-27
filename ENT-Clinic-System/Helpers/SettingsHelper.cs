using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;

namespace ENT_Clinic_System.Helpers
{
    public static class SettingsHelper
    {
        private static Dictionary<string, string> settingsCache;

        /// <summary>
        /// Get a system setting value by key.
        /// </summary>
        /// <param name="key">The setting key to fetch.</param>
        /// <returns>Setting value as string, or null if not found.</returns>
        public static string GetSetting(string key)
        {
            if (settingsCache == null)
            {
                LoadSettings();
            }

            settingsCache.TryGetValue(key, out string value);
            return value;
        }

        /// <summary>
        /// Load all system settings from the database into a cache.
        /// </summary>
        private static void LoadSettings()
        {
            settingsCache = new Dictionary<string, string>();

            string sql = "SELECT setting_key, setting_value FROM system_settings";

            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand(sql, conn))
            {
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string key = reader["setting_key"].ToString();
                            string value = reader["setting_value"].ToString();
                            settingsCache[key] = value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to load system settings: " + ex.Message);
                }
            }
        }
        /// <summary>
        /// Update or insert a system setting value.
        /// </summary>
        public static void UpdateSetting(string key, string value)
        {
            using (var conn = DBConfig.GetConnection())
            using (var cmd = new MySqlCommand(@"
        INSERT INTO system_settings (setting_key, setting_value)
        VALUES (@key, @value)
        ON DUPLICATE KEY UPDATE setting_value = @value;", conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@key", key);
                cmd.Parameters.AddWithValue("@value", value);
                cmd.ExecuteNonQuery();
            }

            // 🔹 Refresh cache immediately so new value is returned
            LoadSettings();
        }


    }
}

//INSERT INTO system_settings (setting_key, setting_value) VALUES
//('allow_negative_stock', '0'),
//('low_stock_threshold', '10'),
//('clinic_name', 'MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS'),
//('clinic_address', '388 E. Lopez St., Jaro, Iloilo City (Front of Robinsons Jaro)'),
//('clinic_tel', '329-1796'),
//('clinic_mobile', '0925-5000149'),
//('clinic_hours', 'Monday, Tuesday, Thursday, Friday, Saturday 11:00 AM – 2:00 PM'),
//('clinic_affiliations', 'St. Paul’s Hospital, Iloilo Doctors’ Hospital, Iloilo Mission Hospital, Western Visayas Medical Center, WVSU Med Center, Medicus Ambulatory, Metro Iloilo Hospital & Med. Center, Inc.'),
//('report_header', 'ENT CLINIC - OFFICIAL REPORT'),
//('report_footer', 'ENT Clinic System @2025'),
//('date_format', 'yyyy-MM-dd'),
//('time_format', 'hh:mm tt'),
//('records_per_page', '20'),
//('markup_percentage', '50');
