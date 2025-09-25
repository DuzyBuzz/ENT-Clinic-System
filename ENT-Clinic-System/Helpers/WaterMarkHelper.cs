using System;
using System.Collections.Generic;
using System.Drawing;

namespace ENT_Clinic_System.Helpers
{
    internal class WaterMarkHelper
    {
        /// <summary>
        /// Prints the clinic header with title, subtitle, and columned information.
        /// Returns the new Y position after printing.
        /// </summary>
        public static int PrintHeader(Graphics g, int leftMargin, int startY, int pageWidth)
        {
            int y = startY;

            // Fetch settings values
            string clinicName = SettingsHelper.GetSetting("clinic_name") ?? "ENT Clinic";
            string clinicSubtitle = SettingsHelper.GetSetting("clinic_subtitle") ?? "";
            string clinicAddress = SettingsHelper.GetSetting("clinic_address") ?? "";
            string clinicTel = SettingsHelper.GetSetting("clinic_tel") ?? "";
            string clinicMobile = SettingsHelper.GetSetting("clinic_mobile") ?? "";
            string clinicEmailAdd = SettingsHelper.GetSetting("clinic_email") ?? "";
            string clinicHours = SettingsHelper.GetSetting("clinic_hours") ?? "";
            string clinicAffiliations = SettingsHelper.GetSetting("clinic_affiliations") ?? "";

            // Fonts
            using (Font titleFont = new Font("Segoe UI", 12, FontStyle.Bold))
            using (Font subtitleFont = new Font("Segoe UI", 9, FontStyle.Regular))
            using (Font columnTitleFont = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Font columnFont = new Font("Segoe UI", 9, FontStyle.Regular))
            {
                // 1. Main title (centered)
                SizeF titleSize = g.MeasureString(clinicName, titleFont);
                float titleX = Math.Max((pageWidth - titleSize.Width) / 2, 0);
                g.DrawString(clinicName, titleFont, Brushes.Black, titleX, y);
                y += 20;

                // 2. Subtitle (centered)
                if (!string.IsNullOrWhiteSpace(clinicSubtitle))
                {
                    string[] subtitleLines = clinicSubtitle.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in subtitleLines)
                    {
                        SizeF size = g.MeasureString(line, subtitleFont);
                        float x = Math.Max((pageWidth - size.Width) / 2, 0);
                        g.DrawString(line, subtitleFont, Brushes.Black, x, y);
                        y += 15;
                    }
                    y += 10; // spacing after subtitle
                }

                // 3. Column titles (left-aligned)
                int col1X = leftMargin;
                int col2X = leftMargin + 220;
                int col3X = leftMargin + 440;

                g.DrawString("CLINIC ADDRESS:", columnTitleFont, Brushes.Black, col1X, y);
                g.DrawString("CLINIC HOURS:", columnTitleFont, Brushes.Black, col2X, y);
                g.DrawString("HOSPITAL AFFILIATIONS:", columnTitleFont, Brushes.Black, col3X, y);
                y += 18;

                // 4. Column contents
                string[] addressLines = new string[]
                {
            clinicAddress,
            $"Tel No.: {clinicTel}",
            $"Mobile No.: {clinicMobile}",
            $"Email: {clinicEmailAdd}"
                };

                string[] hoursParts = clinicHours.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                List<string> clinicHoursLines = new List<string>();
                for (int i = 0; i < hoursParts.Length; i += 2)
                {
                    string line = hoursParts[i].Trim();
                    if (i + 1 < hoursParts.Length)
                        line += ", " + hoursParts[i + 1].Trim();
                    clinicHoursLines.Add(line);
                }

                string[] affiliationsLines = clinicAffiliations.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                int maxLines = Math.Max(Math.Max(addressLines.Length, clinicHoursLines.Count), affiliationsLines.Length);

                for (int i = 0; i < maxLines; i++)
                {
                    if (i < addressLines.Length)
                        g.DrawString(addressLines[i].Trim(), columnFont, Brushes.Black, col1X, y);

                    if (i < clinicHoursLines.Count)
                        g.DrawString(clinicHoursLines[i].Trim(), columnFont, Brushes.Black, col2X, y);

                    if (i < affiliationsLines.Length)
                        g.DrawString(affiliationsLines[i].Trim(), columnFont, Brushes.Black, col3X, y);

                    y += 15;
                }

                y += 10; // spacing after header

                // 5. Draw horizontal line under header
                using (Pen pen = new Pen(Color.Black, 1))
                {
                    g.DrawLine(pen, leftMargin, y, pageWidth - leftMargin, y);
                }

                y += 10; // spacing after line
            }

            return y;
        }



        /// <summary>
        /// Prints the report footer and returns the new Y position.
        /// </summary>
        public static int PrintFooter(Graphics g, int leftMargin, int startY)
        {
            int y = startY;
            string reportFooter = SettingsHelper.GetSetting("report_footer") ?? "ENT Clinic System @2025";

            using (Font footerFont = new Font("Segoe UI", 9))
            {
                g.DrawString(reportFooter, footerFont, Brushes.Black, leftMargin, y);
                y += 20;
            }

            return y;
        }
    }
}
