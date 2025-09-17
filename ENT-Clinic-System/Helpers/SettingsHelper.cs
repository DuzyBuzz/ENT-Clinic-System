using MySql.Data.MySqlClient;
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
    }
}
