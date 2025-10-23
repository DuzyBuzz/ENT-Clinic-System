using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    internal class MedicalCertificatePrinter
    {
        private int patientId;
        private int consultationId;

        // Patient info
        private string patientName, patientSex, patientAddress, patientCivilStatus;
        private int patientAge;

        // Consultation info
        private string diagnosis, recommendations, chiefComplaint, requester;

        private PrintDocument printDocument;
        public PrintDocument Document => printDocument;

        public MedicalCertificatePrinter(int patientId, int consultationId, string requester)
        {
            this.patientId = patientId;
            this.consultationId = consultationId;
            // Convert requester to Title Case once at initialization
            this.requester = ToTitleCase(requester);

            LoadData();

            // Set up the PrintDocument
            printDocument = new PrintDocument();

            // A5 = 148mm x 210mm ~ 583x827 pixels at 96 DPI
            PaperSize a5 = new PaperSize("A5", 583, 827);
            printDocument.DefaultPageSettings.PaperSize = a5;
            printDocument.DefaultPageSettings.Landscape = true;

            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        /// <summary>
        /// Converts a text to Title Case safely.
        /// </summary>
        private static string ToTitleCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "N/A";
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower().Trim());
        }

        /// <summary>
        /// Removes bullets/symbols and normalizes text formatting.
        /// </summary>
        private string CleanBullets(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "N/A";

            // Remove bullet points and leading dashes
            string cleaned = Regex.Replace(text, @"^[•\-\*]\s*", "", RegexOptions.Multiline);

            // Replace line breaks with commas
            cleaned = cleaned.Replace("\r\n", ", ").Replace("\n", ", ");

            return ToTitleCase(cleaned.Trim().TrimEnd(',')); // Always title case
        }

        /// <summary>
        /// Loads patient and consultation info from the database.
        /// </summary>
        private void LoadData()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // =======================
                // PATIENT INFORMATION
                // =======================
                string patientSql = @"
                    SELECT full_name, sex, civil_status, age, address
                    FROM patients WHERE patient_id=@patient_id";

                using (var cmd = new MySqlCommand(patientSql, conn))
                {
                    cmd.Parameters.AddWithValue("@patient_id", patientId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patientSex = reader["sex"]?.ToString() ?? "M";
                            patientCivilStatus = ToTitleCase(reader["civil_status"]?.ToString() ?? "Single");

                            // Determine salutation based on sex and civil status
                            string salutation = (patientSex.ToLower() == "f")
                                ? ((patientCivilStatus.ToLower() == "married") ? "Mrs." : "Ms.")
                                : "Mr.";

                            patientName = $"{salutation} {ToTitleCase(reader["full_name"]?.ToString())}";
                            patientAddress = ToTitleCase(reader["address"]?.ToString() ?? "N/A");
                            int.TryParse(reader["age"]?.ToString(), out patientAge);
                        }
                    }
                }

                // =======================
                // CONSULTATION INFORMATION
                // =======================
                string consultSql = @"
                    SELECT diagnosis, recommendations, chief_complaint
                    FROM consultation WHERE consultation_id=@consultation_id";

                using (var cmd = new MySqlCommand(consultSql, conn))
                {
                    cmd.Parameters.AddWithValue("@consultation_id", consultationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            diagnosis = CleanBullets(reader["diagnosis"]?.ToString());
                            recommendations = CleanBullets(reader["recommendations"]?.ToString());
                            chiefComplaint = CleanBullets(reader["chief_complaint"]?.ToString());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the print layout for the certificate.
        /// </summary>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            float leftMargin = 30;
            float rightMargin = 30;
            float contentWidth = e.PageBounds.Width - leftMargin - rightMargin;
            float y = 30;

            Font titleFont = new Font("Arial", 14, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 10, FontStyle.Regular);
            Font boldFont = new Font("Arial", 10, FontStyle.Bold);
            Brush brush = Brushes.Black;

            StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center };

            // Print Header
            y = WaterMarkHelper.PrintHeader(g, (int)leftMargin, (int)y, e.PageBounds.Width);

            // Title
            g.DrawString("MEDICAL CERTIFICATE", titleFont, brush, new RectangleF(leftMargin, y, contentWidth, 25), centerFormat);
            y += 40;

            // Date (Top-Right)
            string currentDate = DateTime.Now.ToString("MMMM dd, yyyy");
            SizeF dateSize = g.MeasureString(currentDate, bodyFont);
            g.DrawString(currentDate, bodyFont, brush, e.PageBounds.Width - rightMargin - dateSize.Width, y);
            y += 30;

            // Pronouns based on gender
            string pronounSubject = (patientSex.ToLower() == "f") ? "She" : "He";
            string pronounObject = (patientSex.ToLower() == "f") ? "her" : "him";

            // Letter body
            g.DrawString("To Whom It May Concern,", bodyFont, brush, leftMargin, y);
            y += 25;

            y = DrawUnderlinedText(g, "This is to certify that", patientName, bodyFont, brush, leftMargin, y, contentWidth);
            y = DrawUnderlinedText(g, "of", patientAddress, bodyFont, brush, leftMargin, y, contentWidth);
            y = DrawUnderlinedText(g, "consulted my clinic due to", chiefComplaint, bodyFont, brush, leftMargin, y, contentWidth);
            y = DrawUnderlinedText(g, $"{pronounSubject} was diagnosed and/or managed as a case of", diagnosis, bodyFont, brush, leftMargin, y, contentWidth);
            y = DrawUnderlinedText(g, $"{pronounSubject} was advised", recommendations, bodyFont, brush, leftMargin, y, contentWidth);
            y = DrawUnderlinedText(g, "This certificate is issued upon the request of", requester, bodyFont, brush, leftMargin, y, contentWidth);

            g.DrawString($"for whatever purpose it may serve {pronounObject} best.", bodyFont, brush, leftMargin, y + 15);
            y += 40;
            g.DrawString("Thank you.", bodyFont, brush, leftMargin, y);

            // Footer
            WaterMarkHelperA4.PrintFooter(g, (int)leftMargin, e.MarginBounds.Bottom + 30, e.MarginBounds.Width);
        }

        /// <summary>
        /// Draws text with a line underneath to indicate a filled-in blank.
        /// </summary>
        private float DrawUnderlinedText(Graphics g, string label, string value, Font font, Brush brush, float x, float y, float lineWidth)
        {
            g.DrawString(label, font, brush, x, y);
            float labelWidth = g.MeasureString(label + " ", font).Width;
            float valueX = x + labelWidth;

            g.DrawString(value, font, brush, valueX, y);
            SizeF valueSize = g.MeasureString(value, font);

            float underlineY = y + valueSize.Height;
            g.DrawLine(Pens.Black, valueX, underlineY, x + lineWidth, underlineY);

            return y + valueSize.Height + 15;
        }

        /// <summary>
        /// Saves an entry in the issued_medical_certificate table.
        /// </summary>
        public static void SaveIssuedMedicalCertificate(int consultationId)
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                string insertSql = @"
                    INSERT INTO issued_medical_certificate (consultation_id)
                    VALUES (@consultationId)";

                using (var cmd = new MySqlCommand(insertSql, conn))
                {
                    cmd.Parameters.AddWithValue("@consultationId", consultationId);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        if (ex.Number != 1062) // Ignore duplicate entries
                            throw;
                    }
                }
            }
        }

        /// <summary>
        /// Displays the print preview with a custom "Print" button.
        /// </summary>
        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1000,
                Height = 700
            };

            preview.Shown += (s, e) =>
            {
                ToolStrip tool = preview.Controls.OfType<ToolStrip>().FirstOrDefault();
                if (tool != null)
                {
                    // Hide default print button
                    foreach (ToolStripItem item in tool.Items)
                    {
                        if (item is ToolStripButton btn && btn.ToolTipText.ToLower().Contains("print"))
                            btn.Visible = false;
                    }

                    // Add custom Print button
                    ToolStripButton customPrint = new ToolStripButton("Print");
                    customPrint.Click += (sender, args) =>
                    {
                        using (PrintDialog printDialog = new PrintDialog { Document = printDocument, AllowSomePages = true, AllowSelection = true })
                        {
                            if (printDialog.ShowDialog() == DialogResult.OK)
                                printDocument.Print();

                            SaveIssuedMedicalCertificate(consultationId);
                        }
                    };
                    tool.Items.Insert(0, customPrint);
                }
            };

            preview.ShowDialog();
        }
    }
}
