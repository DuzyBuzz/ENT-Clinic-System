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
                    SELECT chief_complaint, history, ear_exam, nose_exam, throat_exam,
                           diagnosis, recommendations, notes
                    FROM consultation WHERE consultation_id = @consultation_id";

                using (var cmd = new MySqlCommand(consultSql, conn))
                {
                    cmd.Parameters.AddWithValue("@consultation_id", consultationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            consultationText =
                                $"Chief Complaint: {reader["chief_complaint"]}\n\n" +
                                $"History of Illness: {reader["history"]}\n\n" +
                                $"Ear Exam: {reader["ear_exam"]}\n\n" +
                                $"Nose Exam: {reader["nose_exam"]}\n\n" +
                                $"Throat Exam: {reader["throat_exam"]}\n\n" +
                                $"Diagnosis: {reader["diagnosis"]}\n\n" +
                                $"Recommendations: {reader["recommendations"]}\n\n" +
                                $"Notes: {reader["notes"]}";
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

            string patientInfo =
                $"Name: {patientName}\n" +
                $"Address: {patientAddress}\n" +
                $"Birth Date: {birthDate:MM/dd/yyyy}   Age: {patientAge}\n" +
                $"Sex: {patientSex}   Civil Status: {civilStatus}\n" +
                $"Contact: {patientContact}\n" +
                $"Emergency: {emergencyName} ({emergencyRelationship}) - {emergencyContact}\n";

            printSections.Add(("Patient Information", patientInfo));

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
                    // Doctor's name
                    g.DrawString("MA. CANDIE PEARL O. BASCOS-VILLENA, MD. FPSO-HNS",
                        clinicNameFont, Brushes.Black,
                        new RectangleF(x, y, contentWidth, 30), center);
                    y += 25;

                    // Subtitle
                    g.DrawString("Fellow, Phil. Society of Otolaryngology, Head & Neck Surgery",
                        subTitleFont, Brushes.Black,
                        new RectangleF(x, y, contentWidth, 20), center);
                    y += 20;

                    // Clinic Info (can adjust formatting if needed)
                    string clinicInfo =
                        "Clinic Address: 388 E. Lopez St., Jaro, Iloilo City (Front of Robinsons Jaro)\n" +
                        "Tel: 329-1796   Mobile: 0925-5000149\n" +
                        "Clinic Hours: Mon, Tue, Thu, Fri, Sat  11:00 AM – 2:00 PM\n" +
                        "Hospital Affiliations: St. Paul’s Hospital, Iloilo Doctors’ Hospital, Iloilo Mission Hospital,\n" +
                        "Western Visayas Medical Center, WVSU Med Center, Medicus Ambulatory, Metro Iloilo Hospital";

                    g.DrawString(clinicInfo, clinicInfoFont, Brushes.Black,
                        new RectangleF(x, y, contentWidth, 80));
                    y += 90;

                    // Divider line
                    g.DrawLine(Pens.Black, x, y, x + contentWidth, y);
                    y += 20;

                    // Document title
                    g.DrawString("CONSULTATION HISTORY",
                        docTitleFont, Brushes.Black,
                        new RectangleF(x, y, contentWidth, 25), center);
                    y += 40;
                }

                // ================== PRINT SECTIONS ==================
                StringFormat sf = new StringFormat(StringFormat.GenericTypographic);

                while (currentSectionIndex < printSections.Count)
                {
                    var (title, body) = printSections[currentSectionIndex];

                    SizeF headerSize = g.MeasureString(title, headerFont);
                    if (y + headerSize.Height > pageBottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    g.DrawString(title, headerFont, Brushes.Black, x, y);
                    y += headerSize.Height + 5;

                    string remainingBody = body.Substring(currentSectionCharIndex);
                    int charsFitted, linesFilled;
                    SizeF layoutSize = new SizeF(contentWidth, pageBottom - y);
                    g.MeasureString(remainingBody, bodyFont, layoutSize, sf,
                        out charsFitted, out linesFilled);

                    if (charsFitted > 0)
                    {
                        string toDraw = remainingBody.Substring(0, charsFitted);
                        g.DrawString(toDraw, bodyFont, Brushes.Black,
                            new RectangleF(x, y, contentWidth, layoutSize.Height), sf);

                        y += g.MeasureString(toDraw, bodyFont,
                            new SizeF(contentWidth, layoutSize.Height), sf).Height + 12;

                        currentSectionCharIndex += charsFitted;

                        if (currentSectionCharIndex < body.Length)
                        {
                            e.HasMorePages = true;
                            return;
                        }
                        else
                        {
                            currentSectionCharIndex = 0;
                            currentSectionIndex++;
                            y += 15;
                        }
                    }
                    else
                    {
                        e.HasMorePages = true;
                        return;
                    }
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
