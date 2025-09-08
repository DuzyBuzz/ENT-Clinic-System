using System;

namespace ENT_Clinic_System.Helpers
{
    // Custom EventArgs to pass the connection status
    public class ConnectionStatusEventArgs : EventArgs
    {
        public bool IsConnected { get; set; }
        public string Message { get; set; }
    }

    internal static class DBConnectionMonitor
    {
        // Event triggered when connection status changes
        public static event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;

        // Track previous connection state
        private static bool _previouslyConnected = true;

        // Call this method to test the DB connection
        public static void TestConnection()
        {
            string message;
            bool isConnected = DBConfig.TestConnection(out message);

            // Only trigger event if status changed
            if (isConnected != _previouslyConnected)
            {
                ConnectionStatusChanged?.Invoke(null, new ConnectionStatusEventArgs
                {
                    IsConnected = isConnected,
                    Message = message
                });

                _previouslyConnected = isConnected;
            }
        }
    }
}
