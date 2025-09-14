using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    internal class MedicalCertificatePrinter
    {
        private int patientId;
        private int consultationId;

        // Patient info
        private string patientName, patientSex, address;
        private int patientAge;

        // Consultation info
        private string diagnosis, recommendations, requester;

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
                    SELECT full_name, sex, age, address
                    FROM patients WHERE patient_id = @patient_id";

                using (var cmd = new MySqlCommand(patientSql, conn))
                {
                    cmd.Parameters.AddWithValue("@patient_id", patientId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patientName = reader["full_name"]?.ToString() ?? "";
                            patientSex = reader["sex"]?.ToString() ?? "";
                            address = reader["address"]?.ToString() ?? "";
                            int.TryParse(reader["age"]?.ToString(), out patientAge);
                        }
                    }
                }

                // 🔹 Load consultation
                string consultSql = @"
                    SELECT diagnosis, recommendations
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
                        }
                    }
                }
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Define fonts
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font subHeaderFont = new Font("Arial", 12, FontStyle.Regular);
            Font bodyFont = new Font("Arial", 11, FontStyle.Regular);
            Font footerFont = new Font("Arial", 10, FontStyle.Italic);
            Font clinicInfoFont = new Font("Arial", 9);

            // Define margins and drawing area
            float contentWidth = e.MarginBounds.Width;
            float leftMargin = e.MarginBounds.Left;
            float rightMargin = e.MarginBounds.Right;
            float y = e.MarginBounds.Top;

            Graphics g = e.Graphics;
            StringFormat center = new StringFormat() { Alignment = StringAlignment.Center };

            // 🔹 Header (Doctor’s Info)
            g.DrawString("MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS",
                headerFont, Brushes.Black, new RectangleF(leftMargin, y, contentWidth, 30), center);
            y += 25;

            g.DrawString("Fellow, Phil. Society of Otolaryngology, Head & Neck Surgery",
                subHeaderFont, Brushes.Black, new RectangleF(leftMargin, y, contentWidth, 20), center);
            y += 20;

            string clinicInfo =
                "Clinic Address: 388 E. Lopez St., Jaro, Iloilo City (Front of Robinsons Jaro)\n" +
                "Tel: 329-1796   Mobile: 0925-5000149\n" +
                "Clinic Hours: Mon, Tue, Thu, Fri, Sat  11:00 AM – 2:00 PM\n" +
                "Hospital Affiliations: St. Paul’s Hospital, Iloilo Doctors’ Hospital, Iloilo Mission Hospital,\n" +
                "Western Visayas Medical Center, WVSU Med Center, Medicus Ambulatory, Metro Iloilo Hospital";

            g.DrawString(clinicInfo, clinicInfoFont, Brushes.Black, new RectangleF(leftMargin, y, contentWidth, 80));
            y += 85;

            // 🔹 Divider line
            g.DrawLine(Pens.Black, leftMargin, y, leftMargin + contentWidth, y);
            y += 10;

            // 🔹 Title
            g.DrawString("Medical Certificate", new Font("Arial", 12, FontStyle.Bold),
                Brushes.Black, new RectangleF(leftMargin, y, contentWidth, 25), center);

            y += 20;



            // 🔹 Date
            g.DrawString($"{DateTime.Now:MMMM dd, yyyy}", subHeaderFont, Brushes.Black, leftMargin + 450, y);
            y += 60;

            // 🔹 Body of certificate
            string body =
                $"To Whom it May Concern,\n\n" +
                $"This is to certify that Mr./Ms./Mrs. {patientName} of {address} consulted me due to. " +
                $"He/She was diagnosed and/or managed as a case of\n\n" +
                $"{diagnosis}\n\n" +
                $"He/She was advised: {recommendations}\n\n" +
                $"This medical certificate is issued upon the request of {requester} " +
                $"for whatever legal purpose it may serve him/her best.\n\n" +
                $"Thank you.";

            g.DrawString(body, bodyFont, Brushes.Black,
                new RectangleF(leftMargin, y, rightMargin - leftMargin, e.MarginBounds.Height - 200));
            y += 200;

            // 🔹 Footer (Doctor Signature & License)
            float footerY = e.MarginBounds.Bottom - 80;
            g.DrawString("MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS",
                bodyFont, Brushes.Black, leftMargin + 200, footerY);
            footerY += 20;
            g.DrawString("LICENSE # 99566", footerFont, Brushes.Black, leftMargin + 400, footerY);
        }

        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1000,
                Height = 700
            };
            preview.ShowDialog();
        }
    }
}
