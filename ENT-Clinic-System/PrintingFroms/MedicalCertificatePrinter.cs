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
        private string diagnosis, recommendations, chief_complaint, requester;

        private PrintDocument printDocument;

        public PrintDocument Document => printDocument;

        public MedicalCertificatePrinter(int patientId, int consultationId, string requester)
        {
            this.patientId = patientId;
            this.consultationId = consultationId;
            this.requester = requester;

            LoadData();

            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        /// <summary>
        /// Helper method to clean bullet-style text
        /// Example: "• Fever\n• Cough\n• Headache" -> "Fever, Cough, Headache"
        /// </summary>
        private string CleanBullets(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "N/A";

            // Remove bullet characters (•, -, *, etc.) and trim spaces
            string cleaned = Regex.Replace(text, @"^[•\-\*]\s*", "", RegexOptions.Multiline);

            // Replace new lines with comma + space
            cleaned = cleaned.Replace("\r\n", ", ").Replace("\n", ", ");

            // Ensure no trailing commas
            return cleaned.Trim().TrimEnd(',');
        }

        private void LoadData()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // 🔹 Load patient
                string patientSql = @"
                    SELECT full_name, sex, civil_status, age, address
                    FROM patients WHERE patient_id = @patient_id";

                using (var cmd = new MySqlCommand(patientSql, conn))
                {
                    cmd.Parameters.AddWithValue("@patient_id", patientId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patientSex = reader["sex"]?.ToString() ?? "Male";
                            patientCivilStatus = reader["civil_status"]?.ToString() ?? "Single";

                            // Determine salutation
                            string salutation = "Mr.";
                            if (patientCivilStatus.ToLower() == "married")
                                salutation = (patientSex.ToLower() == "female") ? "Mrs." : "Mr.";
                            else
                                salutation = (patientSex.ToLower() == "female") ? "Ms." : "Mr.";

                            patientName = salutation + " " + reader["full_name"]?.ToString() ?? "";
                            patientAddress = reader["address"]?.ToString() ?? "";
                            int.TryParse(reader["age"]?.ToString(), out patientAge);
                        }
                    }
                }

                // 🔹 Load consultation
                string consultSql = @"
                    SELECT diagnosis, recommendations, chief_complaint
                    FROM consultation 
                    WHERE consultation_id = @consultation_id";

                using (var cmd = new MySqlCommand(consultSql, conn))
                {
                    cmd.Parameters.AddWithValue("@consultation_id", consultationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            diagnosis = CleanBullets(reader["diagnosis"]?.ToString());
                            recommendations = CleanBullets(reader["recommendations"]?.ToString());
                            chief_complaint = CleanBullets(reader["chief_complaint"]?.ToString());
                        }
                    }
                }
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // ===========================
            // PAGE SETUP
            // ===========================
            float leftMargin = 50;  // 50px from left
            float rightMargin = 50; // 50px from right
            float contentWidth = e.PageBounds.Width - leftMargin - rightMargin;
            float y = 50; // initial top margin

            // ===========================
            // FONTS & STYLES
            // ===========================
            Font titleFont = new Font("Arial", 14, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 11, FontStyle.Regular);
            Font highlightFont = new Font("Arial", 11, FontStyle.Bold);
            Brush highlightBrush = Brushes.Black;

            // Centered text format
            StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center };

            // Wrap format to prevent word splitting
            StringFormat wrapFormat = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.Word
            };

            // ===========================
            // HEADER
            // ===========================
            y = WaterMarkHelper.PrintHeader(g, (int)leftMargin, (int)y, e.PageBounds.Width);

            // Date top-right
            g.DrawString($"{DateTime.Now:MMMM dd, yyyy}", bodyFont, Brushes.Black, e.PageBounds.Width - rightMargin - 150, y);

            // Title
            g.DrawString("MEDICAL CERTIFICATE", titleFont, Brushes.Black, new RectangleF(leftMargin, y, contentWidth, 30), centerFormat);
            y += 50;

            // ===========================
            // SALUTATION / PRONOUNS
            // ===========================
            string salutation = "Mr./Ms.";
            string pronounSubject = "He/She";
            string pronounObject = "him/her";

            if (!string.IsNullOrEmpty(patientSex))
            {
                if (patientSex.ToLower() == "male")
                {
                    salutation = "Mr.";
                    pronounSubject = "He";
                    pronounObject = "him";
                }
                else if (patientSex.ToLower() == "female")
                {
                    salutation = (patientCivilStatus?.ToLower() == "married") ? "Mrs." : "Ms.";
                    pronounSubject = "She";
                    pronounObject = "her";
                }
            }

            // ===========================
            // BODY
            // ===========================
            // Greeting
            g.DrawString("To Whom It May Concern,", bodyFont, Brushes.Black, leftMargin, y);
            y += 25;

            // Patient Info
            g.DrawString($"This is to certify that {salutation} {patientName}", highlightFont, highlightBrush, new RectangleF(leftMargin, y, contentWidth, 50), wrapFormat);
            y += 25;

            g.DrawString($"of {patientAddress},", highlightFont, highlightBrush, new RectangleF(leftMargin, y, contentWidth, 50), wrapFormat);
            y += 30;

            // Consultation reason
            g.DrawString("consulted my clinic due to:", bodyFont, Brushes.Black, leftMargin, y);
            y += 20;

            SizeF diagSize = g.MeasureString(diagnosis, highlightFont, (int)contentWidth, wrapFormat);
            g.DrawString(diagnosis, highlightFont, highlightBrush, new RectangleF(leftMargin, y, contentWidth, diagSize.Height), wrapFormat);
            y += diagSize.Height + 10;

            // Diagnosis
            g.DrawString($"{pronounSubject} was diagnosed and/or managed as a case of:", bodyFont, Brushes.Black, leftMargin, y);
            y += 20;

            SizeF diag2Size = g.MeasureString(diagnosis, highlightFont, (int)contentWidth, wrapFormat);
            g.DrawString(diagnosis, highlightFont, highlightBrush, new RectangleF(leftMargin, y, contentWidth, diag2Size.Height), wrapFormat);
            y += diag2Size.Height + 10;

            // Recommendations
            g.DrawString($"{pronounSubject} was advised to:", bodyFont, Brushes.Black, leftMargin, y);
            y += 20;

            SizeF recSize = g.MeasureString(recommendations, highlightFont, (int)contentWidth, wrapFormat);
            g.DrawString(recommendations, highlightFont, highlightBrush, new RectangleF(leftMargin, y, contentWidth, recSize.Height), wrapFormat);
            y += recSize.Height + 10;

            // Requester
            g.DrawString("This certificate is issued upon the request of:", bodyFont, Brushes.Black, leftMargin, y);
            g.DrawString(requester, highlightFont, highlightBrush, leftMargin + 310, y);
            y += 30;

            // Closing statement
            g.DrawString($"Serve {pronounObject} best.", bodyFont, Brushes.Black, leftMargin, y);
            y += 30;


            // General statement
            g.DrawString("For whatever purpose it may serve.", bodyFont, Brushes.Black, leftMargin, y);
            y += 50;

            // ===========================
            // FOOTER
            // ===========================
            WaterMarkHelper.PrintFooter(g, (int)leftMargin, (int)(e.PageBounds.Bottom - 80));
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
                foreach (Control ctrl in preview.Controls)
                {
                    if (ctrl is ToolStrip toolStrip)
                    {
                        foreach (ToolStripItem item in toolStrip.Items)
                        {
                            if (item is ToolStripButton btn && btn.ToolTipText.ToLower().Contains("print"))
                            {
                                btn.Visible = false; // hide default Print button
                            }
                        }
                    }
                }

                // Add a custom Print button to the ToolStrip
                ToolStrip tool = preview.Controls.OfType<ToolStrip>().FirstOrDefault();
                if (tool != null)
                {
                    ToolStripButton customPrint = new ToolStripButton("Print");
                    customPrint.Click += (sender, args) =>
                    {
                        using (PrintDialog printDialog = new PrintDialog())
                        {
                            printDialog.Document = printDocument;
                            printDialog.AllowSomePages = true;
                            printDialog.AllowSelection = true;

                            if (printDialog.ShowDialog() == DialogResult.OK)
                            {
                                printDocument.Print();
                            }
                        }
                    };
                    // Insert at the left-most position
                    tool.Items.Insert(0, customPrint);
                }
            };

            preview.ShowDialog();
        }





    }
}
