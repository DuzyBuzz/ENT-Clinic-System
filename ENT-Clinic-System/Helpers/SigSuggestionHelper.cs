using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    internal static class SigSuggestionHelper
    {
        public static List<string> GetSigSuggestions(int itemId)
        {
            List<string> sigList = new List<string>();

            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT sig
                        FROM v_sig_suggestions
                        WHERE item_id = @itemId
                          AND sig <> ''
                        ORDER BY use_count DESC, last_used DESC
                        LIMIT 10;";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@itemId", itemId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string sig = reader["sig"].ToString().Trim();
                                if (!string.IsNullOrEmpty(sig))
                                    sigList.Add(sig);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading SIG suggestions:\n" + ex.Message,
                    "SIG AutoSuggest Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return sigList;
        }
    }
}
