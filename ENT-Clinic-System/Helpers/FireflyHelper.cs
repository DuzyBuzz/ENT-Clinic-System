using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// Helper to detect Firefly hardware button presses.
    /// Raises FireflyButtonPressed event once per press.
    /// </summary>
    public class FireflyHelper : IDisposable
    {
        [DllImport("SnapDLL.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void InitButton();

        [DllImport("SnapDLL.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsButtonpress();

        [DllImport("SnapDLL.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void ReleaseButton();

        private readonly Timer checkTimer;
        private bool wasPressedLastTick = false;
        private bool disposed = false;

        public event EventHandler FireflyButtonPressed;

        public FireflyHelper(int intervalMs = 100)
        {
            try
            {
                InitButton();
            }
            catch (DllNotFoundException ex)
            {
                MessageBox.Show($"SnapDLL.dll not found: {ex.Message}", "DLL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Firefly: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            checkTimer = new Timer();
            checkTimer.Interval = intervalMs;
            checkTimer.Tick += CheckTimer_Tick;
            checkTimer.Start();
        }

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            bool isPressed = false;

            try
            {
                isPressed = IsButtonpress();
            }
            catch (Exception)
            {
                // ignore DLL call errors
            }

            if (isPressed && !wasPressedLastTick)
            {
                FireflyButtonPressed?.Invoke(this, EventArgs.Empty);
            }

            wasPressedLastTick = isPressed;
        }

        public void Dispose()
        {
            if (disposed) return;

            try
            {
                checkTimer?.Stop();
                checkTimer?.Dispose();
                ReleaseButton();
            }
            catch { }

            disposed = true;
        }
    }
}
