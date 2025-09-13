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

        private Timer checkTimer;
        private bool wasPressedLastTick = false;

        public event EventHandler FireflyButtonPressed;

        public FireflyHelper(int intervalMs = 300)
        {
            InitButton();

            checkTimer = new Timer();
            checkTimer.Interval = intervalMs;
            checkTimer.Tick += CheckTimer_Tick;
            checkTimer.Start();
        }

        private void CheckTimer_Tick(object sender, EventArgs e)
        {
            bool isPressed = false;
            try { isPressed = IsButtonpress(); } catch { }

            if (isPressed && !wasPressedLastTick)
            {
                FireflyButtonPressed?.Invoke(this, EventArgs.Empty);
            }

            wasPressedLastTick = isPressed;
        }

        public void Dispose()
        {
            try
            {
                checkTimer?.Stop();
                checkTimer?.Dispose();
                ReleaseButton();
            }
            catch { }
        }
    }
}
