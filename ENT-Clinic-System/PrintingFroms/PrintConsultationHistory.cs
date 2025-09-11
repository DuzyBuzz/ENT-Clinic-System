using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using ENT_Clinic_System.Helpers;

namespace ENT_Clinic_System.PrintingFroms
{
    public partial class PrintConsultationHistory : Form
    {
        private int consultationId;
        private int patientId;

        // Patient fields
        private string patientName, patientAddress, patientSex, civilStatus,
                       patientContact, emergencyName, emergencyContact, emergencyRelationship;
        private DateTime birthDate;
        private int patientAge;

        // Consultation full text
        private string consultationText;

        // PrintDocument objects
        private PrintDocument printDocText;
        private PrintDocument printDocImages;

        // State for paginated text printing
        private List<(string Title, string Body)> printSections;
        private int currentSectionIndex = 0;
        private int currentSectionCharIndex = 0;

        // State for image printing
        private int currentImageIndex = 0;

        public PrintConsultationHistory(int consultationId, int patientId)
        {
            InitializeComponent();
            this.consultationId = consultationId;
            this.patientId = patientId;

            LoadPatientAndConsultation();
            BuildPrintSections();
            SetupPrintDocuments();
        }

        /// <summary>
        /// Load patient, consultation and attachments (images)
        /// </summary>
        private void LoadPatientAndConsultation()
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

                // Load consultation
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
                            // Helper function to safely get values
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

                            // Structured & well-spaced text for printing
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


                // Load attachments (images)
                string attachSql = @"SELECT file_path FROM attachments 
                                     WHERE consultation_id=@consultation_id AND file_type='Image'";
                using (var cmd = new MySqlCommand(attachSql, conn))
                {
                    cmd.Parameters.AddWithValue("@consultation_id", consultationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                string path = reader["file_path"]?.ToString() ?? "";
                                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                                {
                                    PictureBox pb = new PictureBox
                                    {
                                        Image = Image.FromFile(path),
                                        SizeMode = PictureBoxSizeMode.Zoom,
                                        Width = 200,
                                        Height = 150,
                                        Margin = new Padding(5)
                                    };
                                    attachmentsPanel.Controls.Add(pb);
                                }
                            }
                            catch { }
                        }
                    }
                }
            }

            // Preview text
            consultationRichTextBox.Text =
                "=== Patient Information ===\n" +
                $"Name: {patientName}\n" +
                $"Address: {patientAddress}\n" +
                $"Birth Date: {birthDate:MM/dd/yyyy}   Age: {patientAge}\n" +
                $"Sex: {patientSex}   Civil Status: {civilStatus}\n" +
                $"Contact: {patientContact}\n" +
                $"Emergency: {emergencyName} ({emergencyRelationship}) - {emergencyContact}\n\n" +
                "=== Consultation Details ===\n" +
                consultationText;
        }

        private void BuildPrintSections()
        {
            printSections = new List<(string Title, string Body)>();

            // Use titles for each line; body contains the value
            printSections.Add(("Name", patientName));
            printSections.Add(("Address", patientAddress));
            printSections.Add(("Birth Date", birthDate.ToString("MM/dd/yyyy")));
            printSections.Add(("Age", patientAge.ToString()));
            printSections.Add(("Sex", patientSex));
            printSections.Add(("Civil Status", civilStatus));
            // Split consultation text into sections

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


        private void SetupPrintDocuments()
        {
            printDocText = new PrintDocument();
            printDocText.PrintPage += PrintDocText_PrintPage;

            printDocImages = new PrintDocument();
            printDocImages.PrintPage += PrintDocImages_PrintPage;
        }

        private void printTextButton_Click(object sender, EventArgs e)
        {
            currentSectionIndex = 0;
            currentSectionCharIndex = 0;
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocText,
                Width = 1000,
                Height = 800
            };
            preview.ShowDialog();
        }

        private void printImageButton_Click(object sender, EventArgs e)
        {
            currentImageIndex = 0;
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocImages,
                Width = 1000,
                Height = 800
            };
            preview.ShowDialog();
        }

        private void PrintDocText_PrintPage(object sender, PrintPageEventArgs e)
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

                // ================== CLINIC HEADER ==================
                if (currentSectionIndex == 0 && currentSectionCharIndex == 0)
                {
                    g.DrawString("MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS",
                        clinicNameFont, Brushes.Black,
                        new RectangleF(x, y, contentWidth, 30), center);
                    y += 25;

                    g.DrawString("Fellow, Phil. Society of Otolaryngology, Head & Neck Surgery",
                        subTitleFont, Brushes.Black,
                        new RectangleF(x, y, contentWidth, 20), center);
                    y += 20;

                    string clinicInfo =
                        "Clinic Address: 388 E. Lopez St., Jaro, Iloilo City (Front of Robinsons Jaro)\n" +
                        "Tel: 329-1796   Mobile: 0925-5000149\n" +
                        "Clinic Hours: Mon, Tue, Thu, Fri, Sat  11:00 AM – 2:00 PM\n" +
                        "Hospital Affiliations: St. Paul’s Hospital, Iloilo Doctors’ Hospital, Iloilo Mission Hospital,\n" +
                        "Western Visayas Medical Center, WVSU Med Center, Medicus Ambulatory, Metro Iloilo Hospital";

                    g.DrawString(clinicInfo, clinicInfoFont, Brushes.Black,
                        new RectangleF(x, y, contentWidth, 80));
                    y += 90;

                    g.DrawLine(Pens.Black, x, y, x + contentWidth, y); // top divider
                    y += 20;

                    g.DrawString("CONSULTATION HISTORY",
                        docTitleFont, Brushes.Black,
                        new RectangleF(x, y, contentWidth, 25), center);
                    y += 40;
                }

                StringFormat sf = new StringFormat(StringFormat.GenericTypographic);

                // ================== PRINT SECTIONS ==================
                bool patientInfoDone = false; // To add line after patient info

                while (currentSectionIndex < printSections.Count)
                {
                    var (label, value) = printSections[currentSectionIndex];

                    // Draw a line separator after patient info
                    if (!patientInfoDone && currentSectionIndex >= 6) // after Emergency line
                    {
                        g.DrawLine(Pens.Black, x, y, x + contentWidth, y); // separator line
                        y += 10;
                        patientInfoDone = true;
                    }

                    // Measure label and value
                    SizeF labelSize = g.MeasureString(label + ":", headerFont);
                    SizeF valueSize = g.MeasureString(value, bodyFont, new SizeF(contentWidth - 150, float.MaxValue));

                    if (y + Math.Max(labelSize.Height, valueSize.Height) > pageBottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    g.DrawString(label + ":", headerFont, Brushes.Black, x, y);
                    g.DrawString(value, bodyFont, Brushes.Black, x + 150, y);

                    y += Math.Max(labelSize.Height, valueSize.Height) + 10;
                    currentSectionIndex++;
                }

                e.HasMorePages = false;
                currentSectionIndex = 0;
                currentSectionCharIndex = 0;
            }
        }




        private void PrintDocImages_PrintPage(object sender, PrintPageEventArgs e)
        {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float contentWidth = e.MarginBounds.Width;
            float pageBottom = e.MarginBounds.Bottom;

            Graphics g = e.Graphics;

            List<Image> images = new List<Image>();
            foreach (Control ctrl in attachmentsPanel.Controls)
            {
                if (ctrl is PictureBox pb && pb.Image != null)
                    images.Add(pb.Image);
            }

            using (Font headerFont = new Font("Arial", 14, FontStyle.Bold))
            {
                // Print "Attachments" title once at start
                if (currentImageIndex == 0)
                {
                    string attachTitle = "Attachments";
                    SizeF headerSize = g.MeasureString(attachTitle, headerFont);

                    g.DrawString(attachTitle, headerFont, Brushes.Black,
                        x + (contentWidth - headerSize.Width) / 2, y);

                    y += headerSize.Height + 20;
                }
            }

            while (currentImageIndex < images.Count)
            {
                Image img = images[currentImageIndex];
                float ratio = Math.Min(contentWidth / img.Width,
                                       e.MarginBounds.Height / (float)img.Height);

                int drawWidth = (int)(img.Width * ratio);
                int drawHeight = (int)(img.Height * ratio);

                if (y + drawHeight > pageBottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                float drawX = x + (contentWidth - drawWidth) / 2;
                g.DrawImage(img, new RectangleF(drawX, y, drawWidth, drawHeight));

                y += drawHeight + 25;
                currentImageIndex++;
            }

            e.HasMorePages = false;
            currentImageIndex = 0;
        }

    }
}
