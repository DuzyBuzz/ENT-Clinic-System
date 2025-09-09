using System;
using System.Windows.Forms;

namespace ENT_Clinic_Receptionist.CustomUI
{
    public static class RichTextBoxBulletHelper
    {
        /// <summary>
        /// Enables automatic bullets for a RichTextBox.
        /// </summary>
        /// <param name="rtb">RichTextBox to enable bullets on.</param>
        public static void EnableAutoBullets(RichTextBox rtb)
        {
            if (rtb == null) return;

            // Ensure the first line starts with a bullet
            if (string.IsNullOrEmpty(rtb.Text))
            {
                rtb.Text = "• ";
                rtb.SelectionStart = rtb.Text.Length;
            }

            rtb.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // prevent default Enter

                    int selStart = rtb.SelectionStart;

                    // Insert newline with bullet
                    rtb.Text = rtb.Text.Insert(selStart, Environment.NewLine + "• ");
                    rtb.SelectionStart = selStart + Environment.NewLine.Length + 2; // move cursor after bullet
                }
            };

            rtb.TextChanged += (s, e) =>
            {
                // Ensure first line always has bullet
                if (!rtb.Text.StartsWith("• "))
                {
                    int sel = rtb.SelectionStart;
                    rtb.Text = "• " + rtb.Text;
                    rtb.SelectionStart = sel + 2;
                }
            };
        }
    }
}
