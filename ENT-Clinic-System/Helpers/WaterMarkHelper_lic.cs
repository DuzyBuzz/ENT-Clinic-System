using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ENT_Clinic_System.Helpers
{
    internal class WaterMarkHelper_lic
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
            string landMark = SettingsHelper.GetSetting("land_mark") ?? "";

            // Fonts for A5
            using (Font titleFont = new Font("Arial", 12, FontStyle.Bold))
            using (Font subtitleFont = new Font("Arial", 10, FontStyle.Bold))
            using (Font columnTitleFont = new Font("Arial",8, FontStyle.Bold))
            using (Font columnFont = new Font("Arial", 7, FontStyle.Regular))
            {
                // 1. Main title (centered)
                SizeF titleSize = g.MeasureString(clinicName, titleFont);
                float titleX = Math.Max((pageWidth - titleSize.Width) / 2, 0);
                g.DrawString(clinicName, titleFont, Brushes.Black, titleX, y);
                y += 22;

                // 2. Subtitle (centered)
                if (!string.IsNullOrWhiteSpace(clinicSubtitle))
                {
                    string[] subtitleLines = clinicSubtitle.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in subtitleLines)
                    {
                        SizeF size = g.MeasureString(line, subtitleFont);
                        float x = Math.Max((pageWidth - size.Width) / 2, 0);
                        g.DrawString(line, subtitleFont, Brushes.Black, x, y);
                        y += 20;
                    }
                    y += 6;
                }

                // 3. Columns
                int availableWidth = pageWidth - leftMargin * 1;
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
                List<string> addressLines = new List<string>()
        {
            clinicAddress,
            $"{landMark}",

            $"Tel: {clinicTel}",
            $"Mobile: {clinicMobile}",
            $"Email: {clinicEmailAdd}",
        };

                // 🔹 Smart formatting for Clinic Hours (CSV style)
                // 🔹 Smart formatting for Clinic Hours (CSV style)
                string[] hoursLines;

                if (string.IsNullOrEmpty(clinicHours))
                {
                    hoursLines = new string[0];
                }
                else
                {
                    string[] parts = clinicHours.Split(',')
                        .Select(p => p.Trim())
                        .Where(p => !string.IsNullOrEmpty(p))
                        .ToArray();

                    string timePart = "";
                    List<string> days = new List<string>();

                    foreach (var p in parts)
                    {
                        // Detect time by AM/PM presence
                        if (p.IndexOf("AM", StringComparison.OrdinalIgnoreCase) >= 0 ||
                            p.IndexOf("PM", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            timePart = p;
                        }
                        else
                        {
                            days.Add(p);
                        }
                    }

                    List<string> formattedLines = new List<string>();

                    if (days.Count > 3)
                    {
                        // Split long day list into two lines
                        int splitIndex = (int)Math.Ceiling(days.Count / 2.0);
                        formattedLines.Add(string.Join(", ", days.Take(splitIndex)) + ",");
                        formattedLines.Add(string.Join(", ", days.Skip(splitIndex)));
                    }
                    else
                    {
                        formattedLines.Add(string.Join(", ", days));
                    }

                    // Add time below the days
                    if (!string.IsNullOrEmpty(timePart))
                        formattedLines.Add(timePart);

                    hoursLines = formattedLines.ToArray();
                }



                // Split affiliations by comma only, but if no comma, keep as single line
                string[] affiliationsLines;
                if (string.IsNullOrEmpty(clinicAffiliations))
                {
                    affiliationsLines = new string[0];
                }
                else if (!clinicAffiliations.Contains(","))
                {
                    // No comma → single line
                    affiliationsLines = new string[] { clinicAffiliations.Trim() };
                }
                else
                {
                    // Comma exists → split by comma
                    affiliationsLines = clinicAffiliations
                        .Split(',')
                        .Select(a => a.Trim())
                        .Where(a => !string.IsNullOrEmpty(a))
                        .ToArray();
                }


                // Determine max lines
                int maxLines = Math.Max(Math.Max(addressLines.Count, hoursLines.Length), affiliationsLines.Length);

                // Extend shorter columns
                while (addressLines.Count < maxLines) addressLines.Add("");
                if (hoursLines.Length < maxLines) Array.Resize(ref hoursLines, maxLines);
                if (affiliationsLines.Length < maxLines) Array.Resize(ref affiliationsLines, maxLines);

                // Draw all lines aligned
                for (int i = 0; i < maxLines; i++)
                {
                    DrawWrappedText(g, addressLines[i], columnFont, col1X, y, colWidth - 4);
                    DrawWrappedText(g, hoursLines[i]?.Trim() ?? "", columnFont, col2X, y, colWidth - 4);
                    DrawWrappedText(g, affiliationsLines[i]?.Trim() ?? "", columnFont, col3X, y, colWidth - 4);
                    y += 10;
                }

                y += 25;

                // Double line under header
                using (Pen thickPen = new Pen(Color.Black, 2)) // thick top line
                using (Pen thinPen = new Pen(Color.Black, 1))  // lighter/thinner bottom line
                {
                    // Top thick line
                    g.DrawLine(thickPen, leftMargin, y, pageWidth - leftMargin, y);

                    // Bottom thin line, a few pixels below
                    g.DrawLine(thinPen, leftMargin, y + 3, pageWidth - leftMargin, y + 3);

                    // Move y below the double line
                    y += 10;
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
            int y = startY;

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
                g.DrawString(licenseNumber, numberFont, Brushes.Black, colX, y - 2);
                y += 16;

                //// PTR
                //g.DrawString("PTR No.", labelFont, Brushes.Black, colX - 150, y);
                //g.DrawLine(linePen, colX - 100, y + 10, colX + 160, y + 10);
                //g.DrawString(ptrNumber, numberFont, Brushes.Black, colX, y - 2);
                //y += 16;

                //// S2
                //g.DrawString("S2 No.", labelFont, Brushes.Black, colX - 150, y);
                //g.DrawLine(linePen, colX - 100, y + 10, colX + 160, y + 10);
                //g.DrawString(s2Number, numberFont, Brushes.Black, colX, y - 2);
                //y += 16;
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
