using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// Helper to detect Firefly hardware button presses.
    /// Supports single click (capture image) and double click (start/stop recording).
    /// </summary>
    public class FireflyHelper : IDisposable
    {
        // -------------------------------
        // DLL Imports from SnapDLL.dll
        // -------------------------------
        [DllImport("SnapDLL.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void InitButton();

        [DllImport("SnapDLL.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsButtonpress();

        [DllImport("SnapDLL.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void ReleaseButton();

        // -------------------------------
        // Private fields
        // -------------------------------
        private readonly Timer checkTimer;
        private bool wasPressedLastTick = false;
        private bool disposed = false;

        private DateTime lastPressTime = DateTime.MinValue;
        private readonly int doubleClickThresholdMs = 400; // adjust if needed

        // -------------------------------
        // Events
        // -------------------------------
        /// <summary>
        /// Raised when Firefly button is pressed once (single click).
        /// </summary>
        public event EventHandler FireflySingleClick;

        /// <summary>
        /// Raised when Firefly button is double-pressed quickly (double click).
        /// </summary>
        public event EventHandler FireflyDoubleClick;

        // -------------------------------
        // Constructor
        // -------------------------------
        public FireflyHelper(int intervalMs = 100)
        {
            try
            {
                InitButton();
            }
            catch (DllNotFoundException ex)
            {
                MessageBox.Show($"SnapDLL.dll not found: {ex.Message}", "DLL Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Firefly: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            checkTimer = new Timer();
            checkTimer.Interval = intervalMs;
            checkTimer.Tick += CheckTimer_Tick;
            checkTimer.Start();
        }

        // -------------------------------
        // Timer Tick → Poll button state
        // -------------------------------
        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            bool isPressed = false;

            try
            {
                isPressed = IsButtonpress();
            }
            catch
            {
                // ignore DLL call errors
            }

            if (isPressed && !wasPressedLastTick)
            {
                DateTime now = DateTime.Now;

                if ((now - lastPressTime).TotalMilliseconds <= doubleClickThresholdMs)
                {
                    // Double click detected
                    FireflyDoubleClick?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    // Single click detected
                    FireflySingleClick?.Invoke(this, EventArgs.Empty);
                }

                lastPressTime = now;
            }

            wasPressedLastTick = isPressed;
        }

        // -------------------------------
        // Dispose
        // -------------------------------
        public void Dispose()
        {
            if (disposed) return;

            try
            {
                checkTimer?.Stop();
                checkTimer?.Dispose();
                ReleaseButton();
            }
            catch
            {
                // ignore cleanup errors
            }

            disposed = true;
        }
    }
}
