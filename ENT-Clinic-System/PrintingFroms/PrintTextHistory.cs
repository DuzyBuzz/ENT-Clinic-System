using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace ENT_Clinic_System.PrintingFroms
{
    internal class PrintTextHistory
    {
        private int patientId;
        private int consultationId;
        public string consultationDate;
        // Patient fields
        private string patientName, patientAddress, patientSex, civilStatus,
                       patientContact, emergencyName, emergencyContact, emergencyRelationship;
        private DateTime birthDate;
        private int patientAge;

        // Consultation text
        private string consultationText;

        // Sections for printing
        private List<(string Title, string Body)> printSections;
        private int currentSectionIndex = 0;

        private PrintDocument printDocument;

        public PrintDocument Document => printDocument;

        public PrintTextHistory(int patientId, int consultationId)
        {
            this.patientId = patientId;
            this.consultationId = consultationId;

            LoadData();
            BuildPrintSections();

            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        /// <summary>
        /// Show a print preview dialog with full toolbar
        /// </summary>


        private void LoadData()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // Load patient info
                string patientSql = @"
                    SELECT full_name, address, birth_date, age, sex, civil_status,
                           patient_contact_number, emergency_name, emergency_contact_number, emergency_relationship
                    FROM patients WHERE patient_id = @patient_id";

                using (var cmd = new MySqlCommand(patientSql, conn))
                {
                    cmd.Parameters.AddWithValue("@patient_id", patientId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patientName = reader["full_name"]?.ToString() ?? "";
                            patientAddress = reader["address"]?.ToString() ?? "";
                            DateTime.TryParse(reader["birth_date"]?.ToString(), out birthDate);
                            int.TryParse(reader["age"]?.ToString(), out patientAge);
                            patientSex = reader["sex"]?.ToString() ?? "";
                            civilStatus = reader["civil_status"]?.ToString() ?? "";
                            patientContact = reader["patient_contact_number"]?.ToString() ?? "";
                            emergencyName = reader["emergency_name"]?.ToString() ?? "";
                            emergencyContact = reader["emergency_contact_number"]?.ToString() ?? "";
                            emergencyRelationship = reader["emergency_relationship"]?.ToString() ?? "";
                        }
                    }
                }

                // Load consultation info
                string consultSql = @"
                    SELECT consultation_date, chief_complaint, history, ear_exam, nose_exam, throat_exam,
                           diagnosis, recommendations, follow_up_date, follow_up_notes
                    FROM consultation 
                    WHERE consultation_id = @consultation_id";

                using (var cmd = new MySqlCommand(consultSql, conn))
                {
                    cmd.Parameters.AddWithValue("@consultation_id", consultationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string SafeGet(object value) => value == DBNull.Value ? "N/A" : value.ToString();

                            string consultationDate = SafeGet(reader["consultation_date"]);
                            string chiefComplaint = SafeGet(reader["chief_complaint"]);
                            string history = SafeGet(reader["history"]);
                            string earExam = SafeGet(reader["ear_exam"]);
                            string noseExam = SafeGet(reader["nose_exam"]);
                            string throatExam = SafeGet(reader["throat_exam"]);
                            string diagnosis = SafeGet(reader["diagnosis"]);
                            string recommendations = SafeGet(reader["recommendations"]);
                            string followUpDate = SafeGet(reader["follow_up_date"]);
                            string followUpNotes = SafeGet(reader["follow_up_notes"]);

                            consultationText =
                                $"Consultation Date: {consultationDate}\n\n" +
                                $"Chief Complaint:\n   {chiefComplaint}\n\n" +
                                $"History of Illness:\n   {history}\n\n" +
                                $"Ear Exam:\n   {earExam}\n\n" +
                                $"Nose Exam:\n   {noseExam}\n\n" +
                                $"Throat Exam:\n   {throatExam}\n\n" +
                                $"Diagnosis:\n   {diagnosis}\n\n" +
                                $"Recommendations:\n   {recommendations}\n\n" +
                                $"Follow-up Date: {followUpDate}\n\n" +
                                $"Follow-up Notes:\n   {followUpNotes}";


                        }

                    }
                }
            }
        }

        private void BuildPrintSections()
        {

            printSections = new List<(string Title, string Body)>
            {
                ("Name", patientName),
                ("Address", patientAddress),
                ("Age", patientAge.ToString()),
                ("Sex", patientSex),
                ("Civil Status", civilStatus)
            };


            string[] parts = consultationText.Split(new[] { "\n\n" }, StringSplitOptions.None);
            foreach (var part in parts)
            {
                int colonIndex = part.IndexOf(':');
                if (colonIndex > 0 && colonIndex < 40)
                {
                    string title = part.Substring(0, colonIndex).Trim();
                    string body = part.Substring(colonIndex + 1).Trim();
                    printSections.Add((title, body));
                }
                else
                {
                    printSections.Add(("Details", part.Trim()));
                }
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float contentWidth = e.MarginBounds.Width;
            float pageBottom = e.MarginBounds.Bottom;

            using (Font clinicNameFont = new Font("Arial", 14, FontStyle.Bold))
            using (Font subTitleFont = new Font("Arial", 10, FontStyle.Italic))
            using (Font clinicInfoFont = new Font("Arial", 9))
            using (Font docTitleFont = new Font("Arial", 12, FontStyle.Bold))
            using (Font headerFont = new Font("Arial", 11, FontStyle.Bold))
            using (Font bodyFont = new Font("Arial", 11))
            {
                Graphics g = e.Graphics;
                StringFormat center = new StringFormat() { Alignment = StringAlignment.Center };

                if (currentSectionIndex == 0)
                {
                    g.DrawString("MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS",
                        clinicNameFont, Brushes.Black, new RectangleF(x, y, contentWidth, 30), center);
                    y += 25;

                    g.DrawString("Fellow, Phil. Society of Otolaryngology, Head & Neck Surgery",
                        subTitleFont, Brushes.Black, new RectangleF(x, y, contentWidth, 20), center);
                    y += 20;

                    string clinicInfo =
                        "Clinic Address: 388 E. Lopez St., Jaro, Iloilo City (Front of Robinsons Jaro)\n" +
                        "Tel: 329-1796   Mobile: 0925-5000149\n" +
                        "Clinic Hours: Mon, Tue, Thu, Fri, Sat  11:00 AM – 2:00 PM\n" +
                        "Hospital Affiliations: St. Paul’s Hospital, Iloilo Doctors’ Hospital, Iloilo Mission Hospital,\n" +
                        "Western Visayas Medical Center, WVSU Med Center, Medicus Ambulatory, Metro Iloilo Hospital";

                    g.DrawString(clinicInfo, clinicInfoFont, Brushes.Black, new RectangleF(x, y, contentWidth, 80));
                    y += 75;

                    g.DrawLine(Pens.Black, x, y, x + contentWidth, y);
                    y += 0;

                    g.DrawString("CONSULTATION HISTORY", docTitleFont, Brushes.Black,
                        new RectangleF(x, y, contentWidth, 25), center);
                    y += 20;

                    g.DrawLine(Pens.Black, x, y, x + contentWidth, y);
                    y += 0;
                    g.DrawString("", docTitleFont, Brushes.Black,
                        new RectangleF(x, y, contentWidth, 25), center);
                    y += 0;
                }
                bool patientInfoDone = false; // To add line after patient info

                while (currentSectionIndex < printSections.Count)
                {

                    var (label, value) = printSections[currentSectionIndex];

                    // Draw a line separator after patient info
                    if (!patientInfoDone && currentSectionIndex >= 5) // after Emergency line
                    {
                        g.DrawLine(Pens.Black, x, y, x + contentWidth, y); // separator line
                        y += 0;
                        patientInfoDone = true;
                    }

                    SizeF labelSize = g.MeasureString(label + ":", headerFont);
                    SizeF valueSize = g.MeasureString(value, bodyFont, new SizeF(contentWidth - 150, float.MaxValue));

                    if (y + Math.Max(labelSize.Height, valueSize.Height) > pageBottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    g.DrawString(label + ":", headerFont, Brushes.Black, x, y);

                    g.DrawString(value, bodyFont, Brushes.Black, new RectangleF(x + 150, y, contentWidth - 150, valueSize.Height));


                    y += Math.Max(labelSize.Height, valueSize.Height) + 5;
                    currentSectionIndex++;
                }

                e.HasMorePages = false;
                currentSectionIndex = 5;
            }
        }
    }

    // Multi-print preview dialog for multiple non-modal previews
    internal class MultiPrintPreviewDialog : PrintPreviewDialog
    {
        public MultiPrintPreviewDialog()
        {
            this.TopLevel = true;
            this.ShowInTaskbar = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 1200;
            this.Height = 800;
            // Make the form full screen
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable; // optional: removes title bar and borders
            this.TopMost = true;

        }

        public new void Show()
        {
            base.Show();
        }
    }
}
