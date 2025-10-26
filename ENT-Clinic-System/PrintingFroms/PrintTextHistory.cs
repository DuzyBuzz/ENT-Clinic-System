using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    internal class PrintTextHistory
    {
        private int patientId;
        private int consultationId;

        // Patient info
        private string patientName, patientAddress, patientSex, civilStatus, patientContact;
        private string emergencyName, emergencyContact, emergencyRelationship;
        private int patientAge;
        private DateTime birthDate;

        // Consultation info
        private DateTime consultationDate;
        private string doctorName, chiefComplaint, history;
        private string earExam, noseExam, throatExam, neckExam, othersExam;
        private string diagnosis, recommendations, notes, followUpNotes;
        private DateTime? followUpDate;
        private float currentY;          // Tracks vertical position across pages
        private bool isFirstPage = true; // Used to reset margins per print job
        // Health record info
        private string pastMedicalHistory, familyHistory, personalSocialHistory, allergies;
        private string bp, temperature, pr, rr, ht, wt;
        private string generalAppearance, skin, headAndFace, eyes, neck, chestLungs, heart, abdomen, extremities, neurologic;

        private PrintDocument printDocument;
        public PrintDocument Document => printDocument;

        public PrintTextHistory(int patientId, int consultationId)
        {
            this.patientId = patientId;
            this.consultationId = consultationId;

            LoadData();

            printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void LoadData()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // --- Patient Info ---
                using (var cmd = new MySqlCommand(@"
                    SELECT full_name, address, birth_date, age, sex, civil_status,
                           patient_contact_number, emergency_name, emergency_contact_number, emergency_relationship
                    FROM patients WHERE patient_id=@patientId", conn))
                {
                    cmd.Parameters.AddWithValue("@patientId", patientId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patientName = ToTitleCase(reader["full_name"]?.ToString());
                            patientAddress = ToTitleCase(reader["address"]?.ToString());
                            DateTime.TryParse(reader["birth_date"]?.ToString(), out birthDate);
                            int.TryParse(reader["age"]?.ToString(), out patientAge);
                            patientSex = ToTitleCase(reader["sex"]?.ToString());
                            civilStatus = ToTitleCase(reader["civil_status"]?.ToString());
                            patientContact = reader["patient_contact_number"]?.ToString();
                            emergencyName = ToTitleCase(reader["emergency_name"]?.ToString());
                            emergencyContact = reader["emergency_contact_number"]?.ToString();
                            emergencyRelationship = ToTitleCase(reader["emergency_relationship"]?.ToString());
                        }
                    }
                }

                // --- Consultation Info ---
                using (var cmd = new MySqlCommand(@"
                    SELECT c.consultation_date, c.doctor_name, c.chief_complaint, c.history,
                           c.ear_exam, c.nose_exam, c.throat_exam, c.neck_exam, c.others_exam,
                           c.diagnosis, c.recommendations, c.notes, c.follow_up_date, c.follow_up_notes, c.age,
                           u.full_name as doctor_full_name
                    FROM consultation c
                    LEFT JOIN user u ON c.doctor_name = u.user_id
                    WHERE c.consultation_id=@consultationId", conn))
                {
                    cmd.Parameters.AddWithValue("@consultationId", consultationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime.TryParse(reader["consultation_date"]?.ToString(), out consultationDate);
                            doctorName = ToTitleCase(reader["doctor_full_name"]?.ToString());
                            chiefComplaint = ToTitleCase(reader["chief_complaint"]?.ToString());
                            history = ToTitleCase(reader["history"]?.ToString());
                            earExam = ToTitleCase(reader["ear_exam"]?.ToString());
                            noseExam = ToTitleCase(reader["nose_exam"]?.ToString());
                            throatExam = ToTitleCase(reader["throat_exam"]?.ToString());
                            neckExam = ToTitleCase(reader["neck_exam"]?.ToString());
                            othersExam = ToTitleCase(reader["others_exam"]?.ToString());
                            diagnosis = ToTitleCase(reader["diagnosis"]?.ToString());
                            recommendations = ToTitleCase(reader["recommendations"]?.ToString());
                            notes = ToTitleCase(reader["notes"]?.ToString());
                            followUpNotes = ToTitleCase(reader["follow_up_notes"]?.ToString());
                            int.TryParse(reader["age"]?.ToString(), out patientAge);

                            DateTime tempDate;
                            followUpDate = DateTime.TryParse(reader["follow_up_date"]?.ToString(), out tempDate) ? tempDate : (DateTime?)null;
                        }
                    }
                }

                // --- Health Record Info ---
                using (var cmd = new MySqlCommand(@"
                    SELECT past_medical_history, family_history, personal_social_history,
                           bp, temperature, pr, rr, ht, wt,
                           general_appearance, skin, head_and_face, eyes, neck, chest_lungs, heart,
                           abdomen, extremities, neurologic, allergies
                    FROM health_record_history
                    WHERE patient_id=@patientId AND consultation_id=@consultationId", conn))
                {
                    cmd.Parameters.AddWithValue("@patientId", patientId);
                    cmd.Parameters.AddWithValue("@consultationId", consultationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pastMedicalHistory = ToTitleCase(reader["past_medical_history"]?.ToString());
                            familyHistory = ToTitleCase(reader["family_history"]?.ToString());
                            personalSocialHistory = ToTitleCase(reader["personal_social_history"]?.ToString());
                            bp = reader["bp"]?.ToString();
                            temperature = reader["temperature"]?.ToString();
                            pr = reader["pr"]?.ToString();
                            rr = reader["rr"]?.ToString();
                            ht = reader["ht"]?.ToString();
                            wt = reader["wt"]?.ToString();
                            generalAppearance = ToTitleCase(reader["general_appearance"]?.ToString());
                            skin = ToTitleCase(reader["skin"]?.ToString());
                            headAndFace = ToTitleCase(reader["head_and_face"]?.ToString());
                            eyes = ToTitleCase(reader["eyes"]?.ToString());
                            neck = ToTitleCase(reader["neck"]?.ToString());
                            chestLungs = ToTitleCase(reader["chest_lungs"]?.ToString());
                            heart = ToTitleCase(reader["heart"]?.ToString());
                            abdomen = ToTitleCase(reader["abdomen"]?.ToString());
                            extremities = ToTitleCase(reader["extremities"]?.ToString());
                            neurologic = ToTitleCase(reader["neurologic"]?.ToString());
                            allergies = ToTitleCase(reader["allergies"]?.ToString());
                        }
                    }
                }
            }
        }

        // ✅ Helper: Convert to Title Case
        private static string ToTitleCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }

        // --- Print Page ---
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float leftMargin = 40;
            float rightMargin = 40;
            float topMargin = 40;
            float footerMargin = 100;
            float contentWidth = e.PageBounds.Width - leftMargin - rightMargin;
            float y = topMargin;

            Font titleFont = new Font("Arial", 12, FontStyle.Bold);
            Font sectionFont = new Font("Arial", 8, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 8, FontStyle.Regular);
            StringFormat wrapFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.Word
            };

            // --- Header ---
            y = WaterMarkHelper.PrintHeader(g, (int)leftMargin, (int)y, e.PageBounds.Width);

            // --- Title ---
            g.DrawString("CONSULTATION HISTORY", titleFont, Brushes.Black,
                new RectangleF(leftMargin, y, contentWidth, 25),
                new StringFormat { Alignment = StringAlignment.Center });
            y += 35;

            // --- Patient Info ---
            float colWidth3 = contentWidth / 3;

            void DrawPatientInfoRow(
                string c1Label, string c1Val,
                string c2Label = null, string c2Val = null,
                string c3Label = null, string c3Val = null,
                string c4Label = null, string c4Val = null)
            {
                // Divide the available width into 4 equal parts
                float colWidth4s = contentWidth / 4;

                // --- Column 1 ---
                if (!string.IsNullOrEmpty(c1Label))
                    g.DrawString($"{c1Label}: {c1Val}", bodyFont, Brushes.Black, leftMargin, y);

                // --- Column 2 ---
                if (!string.IsNullOrEmpty(c2Label))
                    g.DrawString($"{c2Label}: {c2Val}", bodyFont, Brushes.Black, leftMargin + colWidth4s, y);

                // --- Column 3 ---
                if (!string.IsNullOrEmpty(c3Label))
                    g.DrawString($"{c3Label}: {c3Val}", bodyFont, Brushes.Black, leftMargin + (colWidth4s * 2), y);

                // --- Column 4 ---
                if (!string.IsNullOrEmpty(c4Label))
                    g.DrawString($"{c4Label}: {c4Val}", bodyFont, Brushes.Black, leftMargin + (colWidth4s * 3), y);

                // Move down for next line
                y += 20;

                // Optional line separator
                g.DrawLine(Pens.LightGray, leftMargin, y, e.PageBounds.Width - rightMargin, y);
                y += 5;
            }


            DrawPatientInfoRow("Name", patientName, "Age", patientAge.ToString(), "Sex", patientSex, "Civil Status", civilStatus);

            DrawPatientInfoRow("Address", patientAddress, null, null, "Contact Number", patientContact);
            if (!string.IsNullOrEmpty(emergencyName))
                DrawPatientInfoRow("Contact in case of Emergency", $"{emergencyName} {emergencyRelationship}  {emergencyContact}");
            else
                DrawPatientInfoRow(null, null,null,null);


            y += 10;

            // --- Section Headers and Body ---
            void DrawSectionHeader(string text)
            {
                g.FillRectangle(Brushes.LightGray, leftMargin, y, contentWidth, 22);
                g.DrawRectangle(Pens.Gray, leftMargin, y, contentWidth, 22);
                g.DrawString(text, sectionFont, Brushes.Black, leftMargin + 5, y + 3);
                y += 25;
            }
            void DrawTwoColumnSection(string leftHeader, string rightHeader, string leftText, string rightText)
            {
                // Draw section header background
                g.FillRectangle(Brushes.LightGray, leftMargin, y, contentWidth, 22);
                g.DrawRectangle(Pens.Gray, leftMargin, y, contentWidth, 22);
                g.DrawString(leftHeader, sectionFont, Brushes.Black, leftMargin + 5, y + 3);
                g.DrawString(rightHeader, sectionFont, Brushes.Black, leftMargin + contentWidth / 2 + 5, y + 3);
                y += 25;

                // Split items
                var leftItems = string.IsNullOrWhiteSpace(leftText)
                    ? new string[0]
                    : leftText.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();

                var rightItems = string.IsNullOrWhiteSpace(rightText)
                    ? new string[0]
                    : rightText.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();

                int maxLines = Math.Max(leftItems.Length, rightItems.Length);

                // Column width (split the total width in half)
                float columnWidth = contentWidth / 2 - 10; // padding between text and border

                // Enable wrapping
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.LineLimit
                };

                for (int i = 0; i < maxLines; i++)
                {
                    string leftBullet = i < leftItems.Length ? "• " + leftItems[i] : "";
                    string rightBullet = i < rightItems.Length ? "• " + rightItems[i] : "";

                    // Measure both sides’ wrapped height
                    SizeF leftSize = g.MeasureString(leftBullet, bodyFont, (int)columnWidth, sf);
                    SizeF rightSize = g.MeasureString(rightBullet, bodyFont, (int)columnWidth, sf);
                    float lineHeight = Math.Max(leftSize.Height, rightSize.Height);

                    // Define drawing rectangles for wrapping
                    RectangleF leftRect = new RectangleF(leftMargin + 5, y, columnWidth, lineHeight);
                    RectangleF rightRect = new RectangleF(leftMargin + contentWidth / 2 + 5, y, columnWidth, lineHeight);

                    // Draw wrapped text
                    if (!string.IsNullOrEmpty(leftBullet))
                        g.DrawString(leftBullet, bodyFont, Brushes.Black, leftRect, sf);

                    if (!string.IsNullOrEmpty(rightBullet))
                        g.DrawString(rightBullet, bodyFont, Brushes.Black, rightRect, sf);

                    // Move down for next line
                    y += lineHeight + 4;
                }

                y += 5; // spacing after section
            }

            void DrawTwoColumnSectionText(string leftHeader, string rightHeader, string leftText, string rightText)
            {
                // Header background and borders
                g.FillRectangle(Brushes.LightGray, leftMargin, y, contentWidth, 22);
                g.DrawRectangle(Pens.Gray, leftMargin, y, contentWidth, 22);
                g.DrawString(leftHeader, sectionFont, Brushes.Black, leftMargin + 5, y + 3);
                g.DrawString(rightHeader, sectionFont, Brushes.Black, leftMargin + contentWidth / 2 + 5, y + 3);
                y += 25;

                // Split text items (comma separated)
                var leftItems = string.IsNullOrWhiteSpace(leftText)
                    ? new string[0]
                    : leftText.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                var rightItems = string.IsNullOrWhiteSpace(rightText)
                    ? new string[0]
                    : rightText.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();

                int maxLines = Math.Max(leftItems.Length, rightItems.Length);

                // Define column widths
                float columnWidth = contentWidth / 2 - 10; // padding
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.LineLimit
                };

                for (int i = 0; i < maxLines; i++)
                {
                    string l = i < leftItems.Length ? leftItems[i] : "";
                    string r = i < rightItems.Length ? rightItems[i] : "";

                    // Measure text height for wrapping
                    SizeF leftSize = g.MeasureString(l, bodyFont, (int)columnWidth, sf);
                    SizeF rightSize = g.MeasureString(r, bodyFont, (int)columnWidth, sf);
                    float lineHeight = Math.Max(leftSize.Height, rightSize.Height);

                    // Draw wrapped text
                    RectangleF leftRect = new RectangleF(leftMargin, y, columnWidth, lineHeight);
                    RectangleF rightRect = new RectangleF(leftMargin + contentWidth / 2, y, columnWidth, lineHeight);

                    g.DrawString(l, bodyFont, Brushes.Black, leftRect, sf);
                    g.DrawString(r, bodyFont, Brushes.Black, rightRect, sf);

                    y += lineHeight + 5; // spacing between rows
                }

                y += 5; // extra bottom space
            }

            // --- Consultation Details ---
            DrawSectionHeader("Consultation Details");
            DrawPatientInfoRow("Doctor", doctorName, null, "Date", consultationDate.ToString("MMMM dd, yyyy"));
            DrawTwoColumnSectionText("Chief Complaint", "Recent Illness", chiefComplaint, history);// wrapped

            // --- Vital Signs ---
            DrawSectionHeader("Vital Signs");
            g.DrawString($" BP: {bp}                Temp: {temperature}                         PR: {pr}                    RR: {rr}                        Ht: {ht}                    Wt: {wt}", bodyFont, Brushes.Black, leftMargin, y);
            y += 30;

            DrawTwoColumnSection("Medical History", "Family History", pastMedicalHistory, familyHistory);
            DrawTwoColumnSection("Social History", "Allergies", personalSocialHistory, allergies);

            // --- Physical Exam ---
            DrawSectionHeader("Physical Examination");
            string[] examTitles = { "General Appearance", "Skin", "Head & Face", "Eyes", "Neck", "Chest & Lungs", "Heart", "Abdomen", "Extremities", "Neurologic" };
            string[] examValues = { generalAppearance, skin, headAndFace, eyes, neck, chestLungs, heart, abdomen, extremities, neurologic };
            float colWidth4 = contentWidth / 5;

            for (int i = 0; i < examTitles.Length; i += 5)
            {
                float maxHeight = 0;
                for (int c = 0; c < 5 && i + c < examTitles.Length; c++)
                {
                    var val = examValues[i + c];
                    if (!string.IsNullOrWhiteSpace(val))
                    {
                        g.DrawString(examTitles[i + c] + "", sectionFont, Brushes.Black, leftMargin + c * colWidth4, y);
                        var bullets = val.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                        float lineOffset = 18;
                        foreach (var b in bullets)
                        {
                            g.DrawString("" + b, bodyFont, Brushes.Black, leftMargin + c * colWidth4 + 5, y + lineOffset);
                            lineOffset += 16;
                        }
                        if (lineOffset > maxHeight) maxHeight = lineOffset;
                    }
                }
                y += maxHeight + 10;
            }

            // --- ENT Examination ---
            DrawSectionHeader("ENT Examination");
            // ENT Exam Section - Vertical Layout with Wrapping
            string[] entLabels = { "Ear Exam", "Nose Exam", "Throat Exam", "Other Exam" };
            string[] entValues = { earExam, noseExam, throatExam, othersExam };

            // StringFormat to enable wrapping
            StringFormat sfs = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.LineLimit
            };

            // Define text area width (full printable width)
            float textWidth = contentWidth - 10; // 5px padding on each side

            for (int i = 0; i < entLabels.Length; i++)
            {
                // Skip if exam is empty
                if (string.IsNullOrWhiteSpace(entValues[i]))
                    continue;

                // Draw section label
                g.DrawString(entLabels[i] + ":", sectionFont, Brushes.Black, leftMargin, y);
                y += 22; // space below label

                // Split bullets (comma separated)
                var bullets = entValues[i]
                    .Split(',')
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToArray();

                foreach (var b in bullets)
                {
                    string bulletText = "• " + b;

                    // Measure how tall the text will be when wrapped
                    SizeF textSize = g.MeasureString(bulletText, bodyFont, (int)textWidth, sfs);

                    // Define bounding box for wrapped text
                    RectangleF textRect = new RectangleF(leftMargin + 10, y, textWidth, textSize.Height);

                    // Draw text inside the box (auto-wrap)
                    g.DrawString(bulletText, bodyFont, Brushes.Black, textRect, sfs);

                    // Move Y down according to the wrapped height
                    y += textSize.Height + 4; // spacing between bullets
                }

                // Extra space after each exam section
                y += 10;
            }

            y += 40;

            DrawTwoColumnSection("Diagnosis", "Recommendations", diagnosis, recommendations);

            using (Font labelFont = new Font("Arial", 9, FontStyle.Bold))
            {
                string labelText = "Follow-up visit on:";
                float footerY = e.PageBounds.Bottom - 70;
                g.DrawString(labelText, labelFont, Brushes.Black, leftMargin + 30, footerY);

                if (followUpDate.HasValue)
                {
                    using (Font dateFont = new Font("Arial", 9, FontStyle.Underline))
                    {
                        string dateText = followUpDate.Value.ToString("MMMM dd, yyyy");
                        float dateY = footerY + 18;
                        g.DrawString(dateText, dateFont, Brushes.Black, leftMargin + 30, dateY);
                    }
                }
            }

            WaterMarkHelper.PrintFooter(g, 0, (int)(e.PageBounds.Height - footerMargin), e.PageBounds.Width - 75);
        }

        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 900,
                Height = 700
            };

            preview.Shown += delegate
            {
                ToolStrip tool = preview.Controls.OfType<ToolStrip>().FirstOrDefault();
                if (tool != null)
                {
                    foreach (ToolStripItem item in tool.Items)
                    {
                        if (item is ToolStripButton btn && btn.ToolTipText.ToLower().Contains("print"))
                            btn.Visible = false;
                    }

                    ToolStripButton customPrint = new ToolStripButton("Print");
                    customPrint.Click += delegate
                    {
                        using (PrintDialog dlg = new PrintDialog { Document = printDocument })
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
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
