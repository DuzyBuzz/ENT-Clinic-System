

using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
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
                                doctorName = reader["doctor_full_name"]?.ToString() ?? "";
                                chiefComplaint = reader["chief_complaint"]?.ToString() ?? "";
                                history = reader["history"]?.ToString() ?? "";
                                earExam = reader["ear_exam"]?.ToString();
                                noseExam = reader["nose_exam"]?.ToString();
                                throatExam = reader["throat_exam"]?.ToString();
                                neckExam = reader["neck_exam"]?.ToString();
                                othersExam = reader["others_exam"]?.ToString();
                                diagnosis = reader["diagnosis"]?.ToString() ?? "";
                                recommendations = reader["recommendations"]?.ToString() ?? "";
                                notes = reader["notes"]?.ToString() ?? "";
                                followUpNotes = reader["follow_up_notes"]?.ToString();
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
                                pastMedicalHistory = reader["past_medical_history"]?.ToString();
                                familyHistory = reader["family_history"]?.ToString();
                                personalSocialHistory = reader["personal_social_history"]?.ToString();
                                bp = reader["bp"]?.ToString();
                                temperature = reader["temperature"]?.ToString();
                                pr = reader["pr"]?.ToString();
                                rr = reader["rr"]?.ToString();
                                ht = reader["ht"]?.ToString();
                                wt = reader["wt"]?.ToString();
                                generalAppearance = reader["general_appearance"]?.ToString();
                                skin = reader["skin"]?.ToString();
                                headAndFace = reader["head_and_face"]?.ToString();
                                eyes = reader["eyes"]?.ToString();
                                neck = reader["neck"]?.ToString();
                                chestLungs = reader["chest_lungs"]?.ToString();
                                heart = reader["heart"]?.ToString();
                                abdomen = reader["abdomen"]?.ToString();
                                extremities = reader["extremities"]?.ToString();
                                neurologic = reader["neurologic"]?.ToString();
                                allergies = reader["allergies"]?.ToString();
                            }
                        }
                    }
                }
            }

        //--- Print logic will be updated with footer spacing, ENT exam filtered by value, professional layout, 4-column exams ---
        //--- I can provide the full ready code next with all improvements implemented ---



        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float leftMargin = 40;
            float rightMargin = 40;
            float topMargin = 40;
            float footerMargin = 100; // reserve space for footer
            float contentWidth = e.PageBounds.Width - leftMargin - rightMargin;
            float y = topMargin;

            Font titleFont = new Font("Arial", 12, FontStyle.Bold);
            Font sectionFont = new Font("Arial", 10, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 9, FontStyle.Regular);
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

            // --- Patient Info (3 columns) ---
            float colWidth3 = contentWidth / 3;

            void DrawPatientInfoRow(string c1Label, string c1Val, string c2Label = null, string c2Val = null, string c3Label = null, string c3Val = null)
            {
                if (!string.IsNullOrEmpty(c1Label)) g.DrawString($"{c1Label}: {c1Val}", bodyFont, Brushes.Black, leftMargin, y);
                if (!string.IsNullOrEmpty(c2Label)) g.DrawString($"{c2Label}: {c2Val}", bodyFont, Brushes.Black, leftMargin + colWidth3, y);
                if (!string.IsNullOrEmpty(c3Label)) g.DrawString($"{c3Label}: {c3Val}", bodyFont, Brushes.Black, leftMargin + 2 * colWidth3, y);
                y += 20;
                g.DrawLine(Pens.LightGray, leftMargin, y, e.PageBounds.Width - rightMargin, y);
                y += 5;
            }

            DrawPatientInfoRow("Name", patientName, "Age", patientAge.ToString(), "Sex", patientSex);
            DrawPatientInfoRow("Address", patientAddress, "Civil Status", civilStatus);
            if (!string.IsNullOrEmpty(emergencyName))
                DrawPatientInfoRow("Contact Number", patientContact, "Emergency", $"{emergencyName} ({emergencyRelationship}) - {emergencyContact}");
            else
                DrawPatientInfoRow("Contact Number", patientContact);

            y += 10;

            // --- Section Header Helper ---
            void DrawSectionHeader(string text)
            {
                g.FillRectangle(Brushes.LightGray, leftMargin, y, contentWidth, 22);
                g.DrawRectangle(Pens.Gray, leftMargin, y, contentWidth, 22);
                g.DrawString(text, sectionFont, Brushes.Black, leftMargin + 5, y + 3);
                y += 25;
            }
            void DrawTwoColumnSection(string leftHeader, string rightHeader, string leftText, string rightText)
            {
                // Highlighted header
                g.FillRectangle(Brushes.LightGray, leftMargin, y, contentWidth, 22);
                g.DrawRectangle(Pens.Gray, leftMargin, y, contentWidth, 22);
                g.DrawString(leftHeader, sectionFont, Brushes.Black, leftMargin + 5, y + 3);
                g.DrawString(rightHeader, sectionFont, Brushes.Black, leftMargin + contentWidth / 2 + 5, y + 3);
                y += 25;

                // Split values into bullets
                var leftItems = string.IsNullOrWhiteSpace(leftText) ? new string[0] : leftText.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                var rightItems = string.IsNullOrWhiteSpace(rightText) ? new string[0] : rightText.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                int maxLines = Math.Max(leftItems.Length, rightItems.Length);

                for (int i = 0; i < maxLines; i++)
                {
                    string l = i < leftItems.Length ? "• " + leftItems[i] : "";
                    string r = i < rightItems.Length ? "• " + rightItems[i] : "";
                    g.DrawString(l, bodyFont, Brushes.Black, leftMargin, y);
                    g.DrawString(r, bodyFont, Brushes.Black, leftMargin + contentWidth / 2, y);
                    y += 18;
                }
                y += 5; // space after the section
            }
            // --- Consultation Details 2-columns ---
            DrawSectionHeader("Consultation Details");

            // Doctor and Date
            DrawPatientInfoRow("Doctor", doctorName, "Date", consultationDate.ToString("MMMM dd, yyyy"));

            // Chief Complaint and History as 2-column section
            DrawTwoColumnSection("Chief Complaint", "History", chiefComplaint, history);




            // --- Vital Signs ---
            DrawSectionHeader("Vital Signs");
            g.DrawString($"BP: {bp}                      Temp: {temperature}                    PR: {pr}                   RR: {rr}                   Ht: {ht}                   Wt: {wt}", bodyFont, Brushes.Black, leftMargin, y);
            y += 30;




            // --- Usage ---

            DrawTwoColumnSection("Medical History", "Family History", pastMedicalHistory, familyHistory);
            DrawTwoColumnSection("Social History", "Allergies", personalSocialHistory, allergies);


            // --- Physical Exam 4 columns ---
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
                        g.DrawString(examTitles[i + c] + ":", sectionFont, Brushes.Black, leftMargin + c * colWidth4, y);
                        var bullets = val.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                        float lineOffset = 18;
                        foreach (var b in bullets)
                        {
                            g.DrawString("• " + b, bodyFont, Brushes.Black, leftMargin + c * colWidth4 + 5, y + lineOffset);
                            lineOffset += 16;
                        }
                        if (lineOffset > maxHeight) maxHeight = lineOffset;
                    }
                }
                y += maxHeight + 10;
            }

            // --- ENT Examination 4 columns ---
            DrawSectionHeader("ENT Examination");
            string[] entLabels = { "Ear Exam", "Nose Exam", "Throat Exam", "Other Exam" };
            string[] entValues = { earExam, noseExam, throatExam, othersExam };
            for (int i = 0; i < 4; i++)
            {
                if (!string.IsNullOrWhiteSpace(entValues[i]))
                {
                    g.DrawString(entLabels[i] + ":", sectionFont, Brushes.Black, leftMargin + i * colWidth4, y);
                    var bullets = entValues[i].Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                    float lineOffset = 18;
                    foreach (var b in bullets)
                    {
                        g.DrawString("• " + b, bodyFont, Brushes.Black, leftMargin + i * colWidth4 + 5, y + lineOffset);
                        lineOffset += 16;
                    }
                }
            }
            y += 40;

            DrawTwoColumnSection("Diagnosis", "Recommendations", diagnosis, recommendations);



            using (Font labelFont = new Font("Arial", 9, FontStyle.Bold))
            {
                string labelText = "Follow-up visit on:";
                float footerY = e.PageBounds.Bottom - 70; // position near footer

                // Draw label text
                g.DrawString(labelText, labelFont, Brushes.Black, leftMargin + 30, footerY);

                // 🔹 If follow-up date exists, draw it below the label and underline it
                if (followUpDate.HasValue)
                {
                    using (Font dateFont = new Font("Arial", 9, FontStyle.Underline))
                    {
                        string dateText = followUpDate.Value.ToString("MMMM dd, yyyy");
                        float dateY = footerY + 18; // put date below the label
                        g.DrawString(dateText, dateFont, Brushes.Black, leftMargin + 30, dateY);
                    }
                }
            }
            // --- Footer ---
            WaterMarkHelper.PrintFooter(g, 0, (int)(e.PageBounds.Height - footerMargin), e.PageBounds.Width - 75);
        }


        // --- Helper for simple vertical list ---
        void DrawListItems(Graphics g, ref float y, Font bodyFont, string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var items = text.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                foreach (var item in items)
                {
                    g.DrawString("• " + item, bodyFont, Brushes.Black, 50, y);
                    y += 18;
                }
                y += 5;
            }
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
