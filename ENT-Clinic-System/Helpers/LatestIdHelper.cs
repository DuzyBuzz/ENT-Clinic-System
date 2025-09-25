using MySql.Data.MySqlClient;
using System;

namespace ENT_Clinic_System.Helpers
{
    public static class LatestIdHelper
    {
        /// <summary>
        /// Gets the latest (highest) ID from a given table and column.
        /// </summary>
        /// <param name="tableName">The table name (e.g., "consultation").</param>
        /// <param name="idColumnName">The column name of the ID (e.g., "consultation_id").</param>
        /// <returns>The latest ID, or 0 if no records exist.</returns>
        public static int GetLatestId(string tableName, string idColumnName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name cannot be null or empty.", nameof(tableName));

            if (string.IsNullOrWhiteSpace(idColumnName))
                throw new ArgumentException("ID column name cannot be null or empty.", nameof(idColumnName));

            int latestId = 0;

            using (MySqlConnection conn = DBConfig.GetConnection())
            {
                conn.Open();

                string sql = $"SELECT MAX({idColumnName}) FROM {tableName}";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        latestId = Convert.ToInt32(result);
                    }
                }
            }

            return latestId;
        }

        /// <summary>
        /// Deletes the record with the latest (highest) ID from a given table.
        /// </summary>
        /// <param name="tableName">The table name (e.g., "consultation").</param>
        /// <param name="idColumnName">The column name of the ID (e.g., "consultation_id").</param>
        /// <returns>True if a record was deleted, false if none found.</returns>
        public static bool DeleteLatest(string tableName, string idColumnName)
        {
            int latestId = GetLatestId(tableName, idColumnName);

            if (latestId == 0)
                return false; // No rows to delete

            return DeleteById(tableName, idColumnName, latestId);
        }

        /// <summary>
        /// Deletes a record by its specific ID.
        /// </summary>
        /// <param name="tableName">The table name (e.g., "consultation").</param>
        /// <param name="idColumnName">The column name of the ID (e.g., "consultation_id").</param>
        /// <param name="id">The ID value to delete.</param>
        /// <returns>True if a record was deleted, false if not found.</returns>
        public static bool DeleteById(string tableName, string idColumnName, int id)
        {
            if (id <= 0)
                return false;

            using (MySqlConnection conn = DBConfig.GetConnection())
            {
                conn.Open();

                string sql = $"DELETE FROM {tableName} WHERE {idColumnName} = @id";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }
    }
}
