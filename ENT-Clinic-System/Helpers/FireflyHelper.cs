using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// Helper to detect Firefly hardware button presses.
    /// Supports single click (short press), long press, pressed, and released events.
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

        private DateTime pressStartTime = DateTime.MinValue;
        private readonly int longPressThresholdMs = 1000; // 1 seconds
        private bool longPressFired = false;

        // -------------------------------
        // Events
        // -------------------------------
        /// <summary>
        /// Raised immediately when button is pressed down.
        /// </summary>
        public event EventHandler FireflyPressed;

        /// <summary>
        /// Raised immediately when button is released (after short or long press).
        /// </summary>
        public event EventHandler FireflyReleased;

        /// <summary>
        /// Raised when Firefly button is short-pressed (tap).
        /// </summary>
        public event EventHandler FireflySingleClick;

        /// <summary>
        /// Raised when Firefly button is held down long enough.
        /// </summary>
        public event EventHandler FireflyLongPress;

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
                // Button just pressed
                pressStartTime = DateTime.Now;
                longPressFired = false;
                FireflyPressed?.Invoke(this, EventArgs.Empty);
            }
            else if (isPressed && wasPressedLastTick)
            {
                // Button still held
                if (!longPressFired &&
                    (DateTime.Now - pressStartTime).TotalMilliseconds >= longPressThresholdMs)
                {
                    // Long press detected
                    FireflyLongPress?.Invoke(this, EventArgs.Empty);
                    longPressFired = true;
                }
            }
            else if (!isPressed && wasPressedLastTick)
            {
                // Button just released
                FireflyReleased?.Invoke(this, EventArgs.Empty);

                if (!longPressFired)
                {
                    // Short press → Single click
                    FireflySingleClick?.Invoke(this, EventArgs.Empty);
                }
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
