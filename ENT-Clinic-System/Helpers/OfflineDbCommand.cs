//using Microsoft.Data.Sqlite;
//using MySql.Data.MySqlClient;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SQLite;
//using System.Text.Json;

//namespace ENT_Clinic_System.Helpers
//{
//    public class OfflineDbCommand
//    {
//        private string _query;
//        private Dictionary<string, object> _parameters = new Dictionary<string, object>();
//        public OfflineDbCommand(string query) { _query = query; }

//        public void AddParameter(string name, object value)
//        {
//            _parameters[name] = value;
//        }

//        /// <summary>
//        /// Execute non-query (INSERT, UPDATE, DELETE) offline-first
//        /// </summary>
//        public int ExecuteNonQuery()
//        {
//            try
//            {
//                using (var mysqlConn = DBConfig.GetConnection())
//                {
//                    mysqlConn.Open();
//                    using (var cmd = new MySqlCommand(_query, mysqlConn))
//                    {
//                        foreach (var p in _parameters)
//                            cmd.Parameters.AddWithValue(p.Key, p.Value);
//                        return cmd.ExecuteNonQuery();
//                    }
//                }
//            }
//            catch
//            {
//                // MySQL offline → execute locally
//                ExecuteLocally();
//                QueueForSync();
//                return 1;
//            }
//        }

//        /// <summary>
//        /// Execute query and return DataTable (SELECT)
//        /// </summary>
//        public DataTable ExecuteQuery()
//        {
//            try
//            {
//                using (var mysqlConn = DBConfig.GetConnection())
//                {
//                    mysqlConn.Open();
//                    using (var adapter = new MySqlDataAdapter(_query, mysqlConn))
//                    {
//                        foreach (var p in _parameters)
//                            adapter.SelectCommand.Parameters.AddWithValue(p.Key, p.Value);

//                        var dt = new DataTable();
//                        adapter.Fill(dt);
//                        return dt;
//                    }
//                }
//            }
//            catch
//            {
//                // Server offline → read from SQLite cache
//                return ExecuteQueryLocally();
//            }
//        }

//        private void ExecuteLocally()
//        {
//            using (var conn = new SQLiteConnection($"Data Source={OfflineDatabase.LocalDbPath};Version=3;"))
//            {
//                conn.Open();
//                string offlineQuery = _query; // optionally replace table names with _cache
//                using (var cmd = new SQLiteCommand(offlineQuery, conn))
//                {
//                    foreach (var p in _parameters)
//                        cmd.Parameters.AddWithValue(p.Key, p.Value);
//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }

//        private DataTable ExecuteQueryLocally()
//        {
//            using (var conn = new SQLiteConnection($"Data Source={OfflineDatabase.LocalDbPath};Version=3;"))
//            {
//                conn.Open();
//                string offlineQuery = _query; // optionally replace table names with _cache
//                using (var adapter = new SQLiteDataAdapter(offlineQuery, conn))
//                {
//                    var dt = new DataTable();
//                    adapter.Fill(dt);
//                    return dt;
//                }
//            }
//        }

//        private void QueueForSync()
//        {
//            OfflineDatabase.QueueOperation("generic_table", "operation", _query, _parameters);
//        }
//    }
//}
