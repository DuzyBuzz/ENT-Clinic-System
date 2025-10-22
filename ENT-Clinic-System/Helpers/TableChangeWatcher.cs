using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// Watches multiple tables for data changes (row count or timestamp).
    /// Usage: new TableChangeWatcher(new[] { "queue", "patients" }, OnChanged).Start();
    /// </summary>
    public sealed class TableChangeWatcher : IDisposable
    {
        private readonly string[] tableNames;
        private readonly Action onChange;
        private readonly SynchronizationContext syncContext;
        private readonly int intervalMs = 2000; // check every 2 seconds

        private Timer timer;
        private readonly object stateLock = new object();
        private bool disposed;

        // Tracks last snapshot per table (row count + max timestamp)
        private readonly Dictionary<string, Tuple<int, DateTime?>> lastSnapshots =
            new Dictionary<string, Tuple<int, DateTime?>>();

        // Candidate timestamp columns to detect change
        private static readonly string[] TimestampCandidates =
            { "updated_at", "modified_at", "finished_at", "called_at", "created_at" };

        /// <summary>
        /// Watch one or more tables for changes.
        /// </summary>
        public TableChangeWatcher(string[] tableNames, Action onChange)
        {
            if (tableNames == null || tableNames.Length == 0)
                throw new ArgumentException("At least one table name required", nameof(tableNames));
            if (onChange == null)
                throw new ArgumentNullException(nameof(onChange));

            this.tableNames = tableNames;
            this.onChange = onChange;
            this.syncContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// Starts watching for changes.
        /// </summary>
        public void Start()
        {
            ThrowIfDisposed();

            lock (stateLock)
            {
                if (timer != null) return;

                // Take initial snapshot asynchronously
                Task.Run(() => InitializeSnapshots());

                // Start periodic timer
                timer = new Timer(TimerTick, null, intervalMs, intervalMs);
            }
        }

        /// <summary>
        /// Stops watching.
        /// </summary>
        public void Stop()
        {
            lock (stateLock)
            {
                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }
            }
        }

        private void TimerTick(object state)
        {
            Task.Run(() =>
            {
                bool anyChanged = false;

                try
                {
                    foreach (var table in tableNames)
                    {
                        var snap = GetSnapshot(table);
                        bool changed = false;

                        lock (stateLock)
                        {
                            Tuple<int, DateTime?> prev;
                            if (!lastSnapshots.TryGetValue(table, out prev))
                            {
                                // First-time entry
                                lastSnapshots[table] = Tuple.Create(snap.Item1, snap.Item2);
                                continue;
                            }

                            int prevCount = prev.Item1;
                            DateTime? prevMax = prev.Item2;

                            if (snap.Item1 != prevCount)
                                changed = true;
                            else if (snap.Item2.HasValue && (!prevMax.HasValue || snap.Item2.Value != prevMax.Value))
                                changed = true;

                            if (changed)
                                lastSnapshots[table] = Tuple.Create(snap.Item1, snap.Item2);
                        }

                        if (changed)
                            anyChanged = true;
                    }

                    if (anyChanged)
                    {
                        if (syncContext != null)
                            syncContext.Post(_ => SafeInvokeOnChange(), null);
                        else
                            SafeInvokeOnChange();
                    }
                }
                catch
                {
                    // Ignore transient errors (e.g., temporary MySQL disconnections)
                }
            });
        }

        private void InitializeSnapshots()
        {
            try
            {
                foreach (var table in tableNames)
                {
                    var snap = GetSnapshot(table);
                    lock (stateLock)
                    {
                        lastSnapshots[table] = Tuple.Create(snap.Item1, snap.Item2);
                    }
                }
            }
            catch
            {
                // Ignore startup errors
            }
        }

        private void SafeInvokeOnChange()
        {
            try
            {
                onChange();
            }
            catch
            {
                // Swallow exceptions from user callback
            }
        }

        /// <summary>
        /// Returns (count, maxTimestamp) snapshot for a given table.
        /// </summary>
        private Tuple<int, DateTime?> GetSnapshot(string tableName)
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // Find available timestamp columns
                var existing = new List<string>();
                using (var colCmd = new MySqlCommand(@"
                    SELECT COLUMN_NAME
                    FROM information_schema.columns
                    WHERE TABLE_SCHEMA = DATABASE()
                      AND TABLE_NAME = @tbl
                      AND COLUMN_NAME IN ('updated_at','modified_at','finished_at','called_at','created_at')", conn))
                {
                    colCmd.Parameters.AddWithValue("@tbl", tableName);
                    using (var r = colCmd.ExecuteReader())
                    {
                        while (r.Read())
                            existing.Add(r.GetString(0));
                    }
                }

                if (existing.Count == 0)
                {
                    string sql = string.Format("SELECT COUNT(*) FROM `{0}`", tableName);
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        var cnt = Convert.ToInt32(cmd.ExecuteScalar());
                        return Tuple.Create(cnt, (DateTime?)null);
                    }
                }
                else
                {
                    string[] parts = existing.Select(c => string.Format("IFNULL(`{0}`,'1970-01-01')", c)).ToArray();
                    string greatest = "GREATEST(" + string.Join(",", parts) + ")";

                    string sql = string.Format(@"
                        SELECT COUNT(*) AS cnt,
                               MAX({0}) AS mx
                        FROM `{1}`", greatest, tableName);

                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            int cnt = r.IsDBNull(0) ? 0 : Convert.ToInt32(r.GetValue(0));
                            DateTime? mx = r.IsDBNull(1) ? (DateTime?)null : Convert.ToDateTime(r.GetValue(1));
                            return Tuple.Create(cnt, mx);
                        }
                    }

                    return Tuple.Create(0, (DateTime?)null);
                }
            }
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(nameof(TableChangeWatcher));
        }

        public void Dispose()
        {
            if (disposed) return;
            Stop();
            disposed = true;
        }
    }
}
