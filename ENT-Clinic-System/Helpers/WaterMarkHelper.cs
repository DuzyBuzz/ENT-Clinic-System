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

            // Fetch settings safely
            string clinicName = SettingsHelper.GetSetting("clinic_name") ?? "ENT Clinic";
            string clinicSubtitle = SettingsHelper.GetSetting("clinic_subtitle") ?? "";
            string clinicAddress = SettingsHelper.GetSetting("clinic_address") ?? "";
            string clinicTel = SettingsHelper.GetSetting("clinic_tel") ?? "";
            string clinicMobile = SettingsHelper.GetSetting("clinic_mobile") ?? "";
            string clinicEmailAdd = SettingsHelper.GetSetting("clinic_email") ?? "";
            string clinicHours = SettingsHelper.GetSetting("clinic_hours") ?? "";
            string clinicAffiliations = SettingsHelper.GetSetting("clinic_affiliations") ?? "";

            // Fonts for A5
            using (Font titleFont = new Font("Arial", 12, FontStyle.Bold))
            using (Font subtitleFont = new Font("Arial", 7, FontStyle.Regular))
            using (Font columnTitleFont = new Font("Arial", 7, FontStyle.Bold))
            using (Font columnFont = new Font("Arial", 7, FontStyle.Regular))
            {
                // 1. Main title (centered)
                SizeF titleSize = g.MeasureString(clinicName, titleFont);
                float titleX = Math.Max((pageWidth - titleSize.Width) / 2, 0);
                g.DrawString(clinicName, titleFont, Brushes.Black, titleX, y);
                y += 16;

                // 2. Subtitle (centered)
                if (!string.IsNullOrWhiteSpace(clinicSubtitle))
                {
                    string[] subtitleLines = clinicSubtitle.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in subtitleLines)
                    {
                        SizeF size = g.MeasureString(line, subtitleFont);
                        float x = Math.Max((pageWidth - size.Width) / 2, 0);
                        g.DrawString(line, subtitleFont, Brushes.Black, x, y);
                        y += 10;
                    }
                    y += 6;
                }

                // 3. Columns
                int availableWidth = pageWidth - leftMargin * 2;
                int colCount = 3;
                int colWidth = availableWidth / colCount;
                int col1X = leftMargin;
                int col2X = leftMargin + colWidth;
                int col3X = leftMargin + colWidth * 2;

                g.DrawString("CLINIC ADDRESS:", columnTitleFont, Brushes.Black, col1X, y);
                g.DrawString("CLINIC HOURS:", columnTitleFont, Brushes.Black, col2X, y);
                g.DrawString("HOSPITAL AFFILIATIONS:", columnTitleFont, Brushes.Black, col3X, y);
                y += 12;

                // Column contents
                // Column contents
                List<string> addressLines = new List<string>()
                    {
                        clinicAddress,
                        $"Tel: {clinicTel}",
                        $"Mobile: {clinicMobile}",
                        $"Email: {clinicEmailAdd}"
                    };

                // Split hours by newline (\n) instead of comma
                string[] hoursLines = string.IsNullOrEmpty(clinicHours)
                    ? new string[0]
                    : clinicHours.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Split affiliations by newline (\n)
                string[] affiliationsLines = string.IsNullOrEmpty(clinicAffiliations)
                    ? new string[0]
                    : clinicAffiliations.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Determine max lines
                int maxLines = Math.Max(Math.Max(addressLines.Count, hoursLines.Length), affiliationsLines.Length);

                // Extend shorter columns
                while (addressLines.Count < maxLines) addressLines.Add("");
                while (hoursLines.Length < maxLines) Array.Resize(ref hoursLines, maxLines);
                if (affiliationsLines.Length < maxLines) Array.Resize(ref affiliationsLines, maxLines);

                // Draw all lines aligned
                for (int i = 0; i < maxLines; i++)
                {
                    DrawWrappedText(g, addressLines[i], columnFont, col1X, y, colWidth - 4);
                    DrawWrappedText(g, hoursLines[i]?.Trim() ?? "", columnFont, col2X, y, colWidth - 4);
                    DrawWrappedText(g, affiliationsLines[i]?.Trim() ?? "", columnFont, col3X, y, colWidth - 4);
                    y += 10;
                }


                y += 50;

                // Horizontal line under header
                using (Pen pen = new Pen(Color.Black, 1))
                {
                    g.DrawLine(pen, leftMargin, y, pageWidth - leftMargin, y);
                }
                y += 6;
            }

            return y;
        }

        /// <summary>
        /// Prints the report footer and returns the new Y position.
        /// </summary>
        public static int PrintFooter(Graphics g, int leftMargin, int startY, int pageWidth)
        {
            int y = startY ;

            string clinicName = SettingsHelper.GetSetting("clinic_name") ?? "Unknown Clinic Name";
            string licenseNumber = SettingsHelper.GetSetting("license_number") ?? "";
            string ptrNumber = SettingsHelper.GetSetting("ptr") ?? "";
            string s2Number = SettingsHelper.GetSetting("stwo") ?? "";

            using (Font nameFont = new Font("Arial", 8, FontStyle.Bold))
            using (Font labelFont = new Font("Arial", 8))
            using (Font numberFont = new Font("Arial", 8, FontStyle.Bold))
            using (Pen linePen = new Pen(Color.Black, 1))
            {
                int colX = pageWidth - 150;

                g.DrawString(clinicName, nameFont, Brushes.Black, colX - 150, y);
                y += 18;

                // License number
                g.DrawString("Lic. No.", labelFont, Brushes.Black, colX - 150, y);
                g.DrawLine(linePen, colX - 100, y + 10, colX + 160, y + 10);
                g.DrawString(licenseNumber, numberFont, Brushes.Black, colX , y - 2);
                y += 16;

                // PTR
                g.DrawString("PTR No.", labelFont, Brushes.Black, colX - 150, y);
                g.DrawLine(linePen, colX - 100, y + 10, colX + 160, y + 10);
                g.DrawString(ptrNumber, numberFont, Brushes.Black, colX, y - 2);
                y += 16;

                // S2
                g.DrawString("S2 No.", labelFont, Brushes.Black, colX - 150, y);
                g.DrawLine(linePen, colX - 100, y + 10, colX + 160, y + 10);
                g.DrawString(s2Number, numberFont, Brushes.Black, colX, y - 2);
                y += 16;
            }

            return y;
        }

        /// <summary>
        /// Draw text wrapped to max width
        /// </summary>
        private static void DrawWrappedText(Graphics g, string text, Font font, int x, int y, int maxWidth)
        {
            if (string.IsNullOrEmpty(text)) return;

            RectangleF rect = new RectangleF(x, y, maxWidth, 1000);
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                Trimming = StringTrimming.Word,
                FormatFlags = StringFormatFlags.LineLimit
            };

            g.DrawString(text, font, Brushes.Black, rect, sf);
        }
    }
}
