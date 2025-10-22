using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ENT_Clinic_System.Helpers
{
    internal static class OfflineDatabase
    {
        public static string LocalDbPath = "local_cache.db";

        /// <summary>
        /// Create local SQLite database and sync queue table
        /// </summary>
        public static void InitializeLocalDb()
        {
            if (!File.Exists(LocalDbPath))
                SQLiteConnection.CreateFile(LocalDbPath);

            using (var conn = new SQLiteConnection($"Data Source={LocalDbPath};Version=3;"))
            {
                conn.Open();
                string sql = @"
                CREATE TABLE IF NOT EXISTS sync_queue (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    table_name TEXT,
                    operation TEXT,
                    query TEXT,
                    parameters TEXT,
                    timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
                    status TEXT DEFAULT 'PENDING'
                );";
                using (var cmd = new SQLiteCommand(sql, conn))
                    cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Queue operation for later syncing to MySQL
        /// </summary>
        public static void QueueOperation(string table, string operation, string query, object parameters)
        {
            using (var conn = new SQLiteConnection($"Data Source={LocalDbPath};Version=3;"))
            {
                conn.Open();
                string sql = @"INSERT INTO sync_queue(table_name, operation, query, parameters)
                               VALUES(@table, @operation, @query, @params)";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@table", table);
                    cmd.Parameters.AddWithValue("@operation", operation);
                    cmd.Parameters.AddWithValue("@query", query);
                    cmd.Parameters.AddWithValue("@params", JsonSerializer.Serialize(parameters));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Execute non-query locally in SQLite
        /// </summary>
        public static void ExecuteNonQueryLocal(string query, object parameters)
        {
            using (var conn = new SQLiteConnection($"Data Source={LocalDbPath};Version=3;"))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var p in parameters.GetType().GetProperties())
                        {
                            cmd.Parameters.AddWithValue("@" + p.Name, p.GetValue(parameters));
                        }
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Execute SELECT locally in SQLite
        /// </summary>
        public static DataTable ExecuteQueryLocal(string query, Dictionary<string, object> parameters = null)
        {
            using (var conn = new SQLiteConnection($"Data Source={LocalDbPath};Version=3;"))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    if (parameters != null)
                        foreach (var p in parameters)
                            cmd.Parameters.AddWithValue(p.Key, p.Value);

                    var dt = new DataTable();
                    using (var adapter = new SQLiteDataAdapter(cmd))
                        adapter.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// Checks if MySQL is online
        /// </summary>
        public static bool IsOnline(string connectionString)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Sync pending queued operations to MySQL
        /// </summary>
        public static async Task SyncPendingOperations()
        {
            using (var conn = new SQLiteConnection($"Data Source={LocalDbPath};Version=3;"))
            {
                conn.Open();
                string sql = "SELECT id, table_name, query, parameters FROM sync_queue WHERE status='PENDING' ORDER BY timestamp";
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int queueId = reader.GetInt32(0);
                        string table = reader.GetString(1);
                        string query = reader.GetString(2);
                        string paramJson = reader.GetString(3);

                        var parameters = JsonSerializer.Deserialize<Dictionary<string, object>>(paramJson);

                        try
                        {
                            using (var mysqlConn = new MySqlConnection(UserCredentials.ConnectionString))
                            {
                                mysqlConn.Open();
                                using (var mysqlCmd = new MySqlCommand(query, mysqlConn))
                                {
                                    foreach (var p in parameters)
                                        mysqlCmd.Parameters.AddWithValue(p.Key, p.Value);

                                    mysqlCmd.ExecuteNonQuery();
                                }
                            }

                            // Mark as synced
                            using (var updateCmd = new SQLiteCommand("UPDATE sync_queue SET status='SYNCED' WHERE id=@id", conn))
                            {
                                updateCmd.Parameters.AddWithValue("@id", queueId);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                            // still offline → leave as PENDING
                        }
                    }
                }
            }
        }
    }
}
