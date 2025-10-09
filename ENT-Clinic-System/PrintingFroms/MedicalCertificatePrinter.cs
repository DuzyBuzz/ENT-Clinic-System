using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
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
            this.requester = requester;

            LoadData();

            printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("Letter", 850, 1100);

            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private string CleanBullets(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "N/A";

            string cleaned = Regex.Replace(text, @"^[•\-\*]\s*", "", RegexOptions.Multiline);
            cleaned = cleaned.Replace("\r\n", ", ").Replace("\n", ", ");
            return cleaned.Trim().TrimEnd(',');
        }

        private void LoadData()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // Load patient info
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
                            patientCivilStatus = reader["civil_status"]?.ToString() ?? "Single";

                            string salutation = (patientSex.ToLower() == "f")
                                ? ((patientCivilStatus.ToLower() == "married") ? "Mrs." : "Ms.")
                                : "Mr.";

                            patientName = salutation + " " + reader["full_name"]?.ToString();
                            patientAddress = reader["address"]?.ToString() ?? "";
                            int.TryParse(reader["age"]?.ToString(), out patientAge);
                        }
                    }
                }

                // Load consultation info
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

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // Page setup
            float leftMargin = 50;
            float rightMargin = 50;
            float contentWidth = e.PageBounds.Width - leftMargin - rightMargin;
            float y = 50;

            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 11, FontStyle.Regular);
            Font boldFont = new Font("Arial", 11, FontStyle.Bold);
            Brush brush = Brushes.Black;

            StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center };

            // Header
            y = WaterMarkHelperA4.PrintHeader(g, (int)leftMargin, (int)y, e.PageBounds.Width);

            // Title
            g.DrawString("MEDICAL CERTIFICATE", titleFont, brush, new RectangleF(leftMargin, y, contentWidth, 30), centerFormat);
            y += 60;

            // Date (top-right)
            string currentDate = DateTime.Now.ToString("MMMM dd, yyyy");
            SizeF dateSize = g.MeasureString(currentDate, bodyFont);
            g.DrawString(currentDate, bodyFont, brush, e.PageBounds.Width - rightMargin - dateSize.Width, y);
            y += 40;

            // Salutation & pronouns
            string pronounSubject = (patientSex.ToLower() == "f") ? "She" : "He";
            string pronounObject = (patientSex.ToLower() == "f") ? "her" : "him";

            // Letter body
            g.DrawString("To Whom It May Concern,", bodyFont, brush, leftMargin, y);
            y += 30;

            y = DrawUnderlinedText(g, $"This is to certify that", patientName, bodyFont, brush, leftMargin, y, contentWidth);
            y = DrawUnderlinedText(g, "of", patientAddress, bodyFont, brush, leftMargin, y, contentWidth);
            y = DrawUnderlinedText(g, "consulted my clinic due to", chiefComplaint, bodyFont, brush, leftMargin, y, contentWidth);
            y = DrawUnderlinedText(g, $"{pronounSubject} was diagnosed and/or managed as a case of", diagnosis, bodyFont, brush, leftMargin, y, contentWidth);
            y = DrawUnderlinedText(g, $"{pronounSubject} was advised", recommendations, bodyFont, brush, leftMargin, y, contentWidth);
            y = DrawUnderlinedText(g, "This certificate is issued upon the request of", requester, bodyFont, brush, leftMargin, y, contentWidth);

            g.DrawString($"for whatever purpose it may serve {pronounObject} best.", bodyFont, brush, leftMargin, y + 20);
            y += 50;
            g.DrawString("Thank you.", bodyFont, brush, leftMargin, y);

            // Footer (aligned to the right)
            int footerWidth = 200; // adjust based on footer text
            int footerX = (int)(e.PageBounds.Width - footerWidth - rightMargin);
            WaterMarkHelperA4.PrintFooter(g, (int)leftMargin, e.MarginBounds.Bottom - 60, e.MarginBounds.Width);
        }

        private float DrawUnderlinedText(Graphics g, string label, string value, Font font, Brush brush, float x, float y, float lineWidth)
        {
            // Draw label
            g.DrawString(label, font, brush, x, y);

            float labelWidth = g.MeasureString(label + " ", font).Width;
            float valueX = x + labelWidth;

            g.DrawString(value, font, brush, valueX, y);

            SizeF valueSize = g.MeasureString(value, font);
            float underlineY = y + valueSize.Height;
            g.DrawLine(Pens.Black, valueX, underlineY, x + lineWidth, underlineY);

            return y + valueSize.Height + 20;
        }

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
                    // Hide default Print button
                    foreach (ToolStripItem item in tool.Items)
                        if (item is ToolStripButton btn && btn.ToolTipText.ToLower().Contains("print"))
                            btn.Visible = false;

                    // Add custom Print button
                    ToolStripButton customPrint = new ToolStripButton("Print");
                    customPrint.Click += (sender, args) =>
                    {
                        using (PrintDialog printDialog = new PrintDialog { Document = printDocument, AllowSomePages = true, AllowSelection = true })
                        {
                            if (printDialog.ShowDialog() == DialogResult.OK)
                                printDocument.Print();
                        }
                    };
                    tool.Items.Insert(0, customPrint);
                }
            };

            preview.ShowDialog();
        }
    }
}
