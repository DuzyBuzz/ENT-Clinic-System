using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    internal class PrintTextHistory
    {
        // Identifiers
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
        private string earExam, noseExam, throatExam, othersExam;
        private string maxillofacialExam, headAndNeckExam;
        private string diagnosis, recommendations, notes, followUpNotes;
        private DateTime? followUpDate;

        // Health record info
        private string pastMedicalHistory, familyHistory, personalSocialHistory, allergies;
        private string bp, temperature, pr, rr, ht, wt;
        private string generalAppearance, skin, headAndFace, neck, eyes, chestLungs, heart, abdomen, extremities, neurologic;

        // Prescriptions
        private readonly List<(string GenericName, string BrandName, string Strength, string Dosage, int Quantity, string Sig)> prescriptions
            = new List<(string, string, string, string, int, string)>();

        // Printing
        private PrintDocument printDocument;
        public PrintDocument Document => printDocument;

        // Pagination / state machine
        // stages: 0 header & patient info, 1 doctor/date & chief/history, 2 vitals, 3 medical/family/social/allergy,
        // 4 physical exam, 5 ENT exams, 6 diagnosis/recommendations, 7 prescriptions, 8 notes, 9 footer/done
        private int printStage = 0;

        // resume indices for lists & multi-blocks
        private int patientInfoRowIndex = 0;
        private int physicalExamOuterIndex = 0; // iterate the physical exam items
        private int entExamIndex = 0;
        private int prescriptionsIndex = 0;

        // print cursor
        private float currentY = 0f;
        private bool headerPrintedThisPage = false;

        // INTERNAL FLAG: whether to force long bond paper layout.
        // This is used internally to adjust spacing/margins; it is NOT printed on the document.
        private bool _isLongBondPaper = false;

        public PrintTextHistory(int patientId, int consultationId)
        {
            this.patientId = patientId;
            this.consultationId = consultationId;

            LoadData();

            printDocument = new PrintDocument();

            // FORCE Long Bond Paper (8.5" x 13") in printer units (hundredths of inch)
            // 8.5 * 100 = 850, 13 * 100 = 1300
            PaperSize longBond = new PaperSize("LongBond", 850, 1300);
            printDocument.DefaultPageSettings.PaperSize = longBond;

            // Optionally adjust margins for long bond
            printDocument.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);

            // Set internal flag (used to adapt bottom space / footer reserve)
            _isLongBondPaper = true;

            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        #region Data Loading
        private void LoadData()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // --- Patient Info ---
                using (var cmd = new MySqlCommand(@"
                    SELECT full_name, address, birth_date, age, sex, civil_status,
                           patient_contact_number, emergency_name, emergency_contact_number, emergency_relationship
                    FROM patients WHERE patient_id=@patientId LIMIT 1", conn))
                {
                    cmd.Parameters.AddWithValue("@patientId", patientId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patientName = ToTitleCaseSafe(reader["full_name"]);
                            patientAddress = ToTitleCaseSafe(reader["address"]);
                            DateTime.TryParse(reader["birth_date"]?.ToString(), out birthDate);
                            int.TryParse(reader["age"]?.ToString(), out patientAge);
                            patientSex = ToTitleCaseSafe(reader["sex"]);
                            civilStatus = ToTitleCaseSafe(reader["civil_status"]);
                            patientContact = reader["patient_contact_number"]?.ToString() ?? "";
                            emergencyName = ToTitleCaseSafe(reader["emergency_name"]);
                            emergencyContact = reader["emergency_contact_number"]?.ToString() ?? "";
                            emergencyRelationship = ToTitleCaseSafe(reader["emergency_relationship"]);
                        }
                    }
                }

                // --- Consultation Info ---
                using (var cmd = new MySqlCommand(@"
                    SELECT c.consultation_date, u.full_name AS doctor_full_name, c.chief_complaint, c.history,
                           c.ear_exam, c.nose_exam, c.throat_exam, c.others_exam, c.head_and_neck_exam, c.maxillofacial_exam,
                           c.diagnosis, c.recommendations, c.notes, c.follow_up_date, c.follow_up_notes, c.age
                    FROM consultation c
                    LEFT JOIN user u ON c.doctor_name = u.user_id
                    WHERE c.consultation_id=@consultationId LIMIT 1", conn))
                {
                    cmd.Parameters.AddWithValue("@consultationId", consultationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime.TryParse(reader["consultation_date"]?.ToString(), out consultationDate);
                            doctorName = ToTitleCaseSafe(reader["doctor_full_name"]);
                            chiefComplaint = ToTitleCaseSafe(reader["chief_complaint"]);
                            history = ToTitleCaseSafe(reader["history"]);
                            earExam = ToTitleCaseSafe(reader["ear_exam"]);
                            noseExam = ToTitleCaseSafe(reader["nose_exam"]);
                            throatExam = ToTitleCaseSafe(reader["throat_exam"]);
                            othersExam = ToTitleCaseSafe(reader["others_exam"]);
                            headAndNeckExam = ToTitleCaseSafe(reader["head_and_neck_exam"]);
                            maxillofacialExam = ToTitleCaseSafe(reader["maxillofacial_exam"]);
                            diagnosis = ToTitleCaseSafe(reader["diagnosis"]);
                            recommendations = ToTitleCaseSafe(reader["recommendations"]);
                            notes = ToTitleCaseSafe(reader["notes"]);
                            followUpNotes = ToTitleCaseSafe(reader["follow_up_notes"]);
                            int.TryParse(reader["age"]?.ToString(), out patientAge);

                            DateTime temp;
                            followUpDate = DateTime.TryParse(reader["follow_up_date"]?.ToString(), out temp) ? temp : (DateTime?)null;
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
                    WHERE patient_id=@patientId AND consultation_id=@consultationId LIMIT 1", conn))
                {
                    cmd.Parameters.AddWithValue("@patientId", patientId);
                    cmd.Parameters.AddWithValue("@consultationId", consultationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pastMedicalHistory = ToTitleCaseSafe(reader["past_medical_history"]);
                            familyHistory = ToTitleCaseSafe(reader["family_history"]);
                            personalSocialHistory = ToTitleCaseSafe(reader["personal_social_history"]);
                            bp = reader["bp"]?.ToString() ?? "";
                            temperature = reader["temperature"]?.ToString() ?? "";
                            pr = reader["pr"]?.ToString() ?? "";
                            rr = reader["rr"]?.ToString() ?? "";
                            ht = reader["ht"]?.ToString() ?? "";
                            wt = reader["wt"]?.ToString() ?? "";
                            generalAppearance = ToTitleCaseSafe(reader["general_appearance"]);
                            skin = ToTitleCaseSafe(reader["skin"]);
                            headAndFace = ToTitleCaseSafe(reader["head_and_face"]);
                            eyes = ToTitleCaseSafe(reader["eyes"]);
                            neck = ToTitleCaseSafe(reader["neck"]);
                            chestLungs = ToTitleCaseSafe(reader["chest_lungs"]);
                            heart = ToTitleCaseSafe(reader["heart"]);
                            abdomen = ToTitleCaseSafe(reader["abdomen"]);
                            extremities = ToTitleCaseSafe(reader["extremities"]);
                            neurologic = ToTitleCaseSafe(reader["neurologic"]);
                            allergies = ToTitleCaseSafe(reader["allergies"]);
                        }
                    }
                }

                // --- PRESCRIPTIONS ---
                using (var cmd = new MySqlCommand(@"
                    SELECT i.generic_name, i.brand_name, i.strength, i.dosage, p.quantity, p.sig
                    FROM prescription p
                    JOIN items i ON p.item_id = i.item_id
                    WHERE p.consultation_id = @consultationId
                    UNION ALL
                    SELECT o.generic_name, o.brand_name, o.strength, o.dosage, po.quantity, po.sig
                    FROM prescription_other po
                    JOIN other_items o ON po.item_id = o.item_id
                    WHERE po.consultation_id = @consultationId
                    ORDER BY 1;", conn))
                {
                    cmd.Parameters.AddWithValue("@consultationId", consultationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            prescriptions.Add((
                                ToTitleCaseSafe(reader["generic_name"]),
                                ToTitleCaseSafe(reader["brand_name"]),
                                reader["strength"]?.ToString() ?? "",
                                reader["dosage"]?.ToString() ?? "",
                                reader["quantity"] != DBNull.Value ? Convert.ToInt32(reader["quantity"]) : 0,
                                reader["sig"]?.ToString() ?? ""
                            ));
                        }
                    }
                }
            }
        }

        private static string ToTitleCaseSafe(object input)
        {
            if (input == null) return string.Empty;
            string text = input.ToString().Trim();
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }
        #endregion

        #region Small Draw Helpers & Spacing

        private bool EnsureSpace(Graphics g, PrintPageEventArgs e, ref float y, float needed, float bottomLimit)
        {
            // If not enough space for 'needed', request new page (don't advance indices)
            if (y + needed > bottomLimit)
            {
                // Prepare next page - preserve currentY so we continue where we left
                e.HasMorePages = true;
                headerPrintedThisPage = false;
                return false;
            }
            return true;
        }

        private void DrawUnderlinedTitle(Graphics g, string title, Font titleFont, float left, ref float y, float contentWidth)
        {
            // Draw title and a thin underline below
            g.DrawString(title, titleFont, Brushes.Black, left, y);
            SizeF titleSize = g.MeasureString(title, titleFont);
            float underlineY = y + titleSize.Height + 2f;
            g.DrawLine(Pens.Black, left, underlineY, left + Math.Min(contentWidth, titleSize.Width), underlineY);
            y = underlineY + 6f;
        }

        private void DrawWrappedText(Graphics g, string text, Font font, float left, float width, ref float y, PrintPageEventArgs e, float bottomLimit)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near, Trimming = StringTrimming.Word };
            SizeF size = g.MeasureString(text, font, (int)width, sf);

            if (!EnsureSpace(g, e, ref y, size.Height + 4f, bottomLimit)) return;

            RectangleF rect = new RectangleF(left, y, width, size.Height);
            g.DrawString(text, font, Brushes.Black, rect, sf);
            y += size.Height + 6f;
        }

        private void DrawTwoColumnBulletedLists(Graphics g, string leftTextCsv, string rightTextCsv, Font bodyFont, float left, float mid, float widthPerCol, ref float y, PrintPageEventArgs e, float bottomLimit)
        {
            string[] leftItems = string.IsNullOrWhiteSpace(leftTextCsv) ? new string[0] : leftTextCsv.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
            string[] rightItems = string.IsNullOrWhiteSpace(rightTextCsv) ? new string[0] : rightTextCsv.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
            int maxLines = Math.Max(leftItems.Length, rightItems.Length);

            StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near, Trimming = StringTrimming.Word };

            for (int i = 0; i < maxLines; i++)
            {
                string l = i < leftItems.Length ? "• " + leftItems[i] : "";
                string r = i < rightItems.Length ? "• " + rightItems[i] : "";

                SizeF leftSize = g.MeasureString(l, bodyFont, (int)widthPerCol, sf);
                SizeF rightSize = g.MeasureString(r, bodyFont, (int)widthPerCol, sf);
                float lineHeight = Math.Max(leftSize.Height, rightSize.Height);

                if (!EnsureSpace(g, e, ref y, lineHeight + 4f, bottomLimit)) return;

                RectangleF leftRect = new RectangleF(left, y, widthPerCol, lineHeight);
                RectangleF rightRect = new RectangleF(mid, y, widthPerCol, lineHeight);

                if (!string.IsNullOrEmpty(l)) g.DrawString(l, bodyFont, Brushes.Black, leftRect, sf);
                if (!string.IsNullOrEmpty(r)) g.DrawString(r, bodyFont, Brushes.Black, rightRect, sf);

                y += lineHeight + 4f;
            }
        }

        #endregion

        #region Main Print Logic (pagination safe)
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // layout
            float leftMargin = e.MarginBounds.Left;
            float rightMargin = e.MarginBounds.Right;
            float topMargin = e.MarginBounds.Top;
            float contentWidth = e.MarginBounds.Width;

            // adapt bottom reserve based on long bond flag (gives more space for content on long paper)
            float footerReserve = _isLongBondPaper ? 100f : 60f;
            float bottomLimit = e.MarginBounds.Bottom - footerReserve; // reserve for footer

            // fonts
            using (Font headerFont = new Font("Segoe UI", 11f, FontStyle.Bold))
            using (Font sectionFont = new Font("Segoe UI", 9f, FontStyle.Bold))
            using (Font bodyFont = new Font("Segoe UI", 9f, FontStyle.Regular))
            using (Font smallFont = new Font("Segoe UI", 8f, FontStyle.Regular))
            {
                // header per page
                // header per page
                if (!headerPrintedThisPage)
                {
                    currentY = topMargin;
                    // print page header (clinic header & centered title)
                    currentY = WaterMarkHelper.PrintHeader(g, (int)leftMargin, (int)currentY, e.PageBounds.Width);

                    // Fonts
                        float titleY = currentY + 6f;

                        // Draw left-aligned title
                        DrawUnderlinedTitle(g, "CONSULTATION HISTORY", headerFont, leftMargin, ref titleY, contentWidth);

                        // Draw consultation date on the right
                        string dateText = consultationDate == DateTime.MinValue ? "" : consultationDate.ToString("F");
                        SizeF dateSize = g.MeasureString(dateText, headerFont);
                        float dateX = e.MarginBounds.Right - dateSize.Width;
                        g.DrawString(dateText, headerFont, Brushes.Black, dateX, currentY + 6f);

                        currentY = titleY + 6f;

                    headerPrintedThisPage = true;
                }


                bool stopNow = false;

                // state machine: continue from current stage and resume indices
                while (!stopNow)
                {
                    switch (printStage)
                    {
                        case 0: // Patient basic info block
                            {
                                DrawUnderlinedTitle(g, "Patient Information", sectionFont, leftMargin, ref currentY, contentWidth);

                                // First row: Name | Age | Sex | Civil Status
                                var infoRow1 = new Dictionary<string, string>
    {
        { "Name", patientName },
        { "Age", patientAge.ToString() },
        { "Sex", patientSex },
        { "Civil Status", civilStatus }
    };

                                float colWidth1 = contentWidth / infoRow1.Count;
                                float x1 = leftMargin;
                                foreach (var kv in infoRow1)
                                {
                                    string text = $"{kv.Key}: {kv.Value}";
                                    g.DrawString(text, bodyFont, Brushes.Black, x1, currentY);
                                    x1 += colWidth1;
                                }
                                currentY += bodyFont.GetHeight(g) + 4f;

                                // Second row: Address
                                g.DrawString($"Address: {patientAddress}", bodyFont, Brushes.Black, leftMargin, currentY);
                                currentY += bodyFont.GetHeight(g) + 4f;

                                // Third row: Emergency | Contact
                                var infoRow3 = new Dictionary<string, string>
    {
        { "Emergency", $"{emergencyName} ({emergencyRelationship}) {emergencyContact}" },
        { "Contact", patientContact }
    };
                                float colWidth3 = contentWidth / infoRow3.Count;
                                float x3 = leftMargin;
                                foreach (var kv in infoRow3)
                                {
                                    string text = $"{kv.Key}: {kv.Value}";
                                    g.DrawString(text, bodyFont, Brushes.Black, x3, currentY);
                                    x3 += colWidth3;
                                }
                                currentY += bodyFont.GetHeight(g) + 6f;

                                printStage = 1;
                                break;
                            }


                        case 1: // Doctor, Date, Chief Complaint & History
                            {
                                DrawUnderlinedTitle(g, "Consultation", sectionFont, leftMargin, ref currentY, contentWidth);

                                string docDate = $"Doctor: {doctorName}    Date: {(consultationDate == DateTime.MinValue ? "" : consultationDate.ToString("MMMM dd, yyyy"))}";
                                DrawWrappedText(g, docDate, bodyFont, leftMargin, contentWidth, ref currentY, e, bottomLimit);

                                // Two-column: Chief Complaint | Recent Illness (history)
                                DrawUnderlinedTitle(g, "Problems / Recent Illness", sectionFont, leftMargin, ref currentY, contentWidth);
                                float colWidth = (contentWidth / 2) - 10f;
                                float mid = leftMargin + contentWidth / 2 + 10f;
                                DrawTwoColumnBulletedLists(g, chiefComplaint, history, bodyFont, leftMargin, mid, colWidth, ref currentY, e, bottomLimit);

                                printStage = 2;
                                break;
                            }

                        case 2: // Vitals
                            {
                                DrawUnderlinedTitle(g, "Vitals", sectionFont, leftMargin, ref currentY, contentWidth);

                                // Define each vital and its value
                                 var vitalsDict = new Dictionary<string, string>
                                    {
                                        { "BP", bp },
                                        { "Temp", temperature },
                                        { "PR", pr },
                                        { "RR", rr },
                                        { "Ht", ht },
                                        { "Wt", wt }
                                    };

                                // Calculate width per column
                                float colWidth = contentWidth / vitalsDict.Count;
                                float x = leftMargin;

                                foreach (var kv in vitalsDict)
                                {
                                    string text = $"{kv.Key}: {kv.Value}";
                                    SizeF size = g.MeasureString(text, bodyFont);
                                    g.DrawString(text, bodyFont, Brushes.Black, x, currentY);
                                    x += colWidth; // move to next column
                                }

                                // advance Y by a line height
                                currentY += bodyFont.GetHeight(g) + 6f;

                                printStage = 3;
                                break;
                            }


                        case 3: // Medical / Family / Social / Allergies
                            {
                                DrawUnderlinedTitle(g, "Medical & Family History", sectionFont, leftMargin, ref currentY, contentWidth);
                                float colW = (contentWidth / 2) - 10f;
                                float mid = leftMargin + contentWidth / 2 + 10f;
                                DrawTwoColumnBulletedLists(g, pastMedicalHistory, familyHistory, bodyFont, leftMargin, mid, colW, ref currentY, e, bottomLimit);

                                DrawUnderlinedTitle(g, "Social History & Allergies", sectionFont, leftMargin, ref currentY, contentWidth);
                                DrawTwoColumnBulletedLists(g, personalSocialHistory, allergies, bodyFont, leftMargin, mid, colW, ref currentY, e, bottomLimit);

                                printStage = 4;
                                break;
                            }

                        case 4: // Physical Exam (General -> Neurologic)
                            {
                                DrawUnderlinedTitle(g, "Physical Examination", sectionFont, leftMargin, ref currentY, contentWidth);

                                string[] examTitles = { "General Appearance", "Skin", "Head & Face", "Eyes", "Neck", "Chest & Lungs", "Heart", "Abdomen", "Extremities", "Neurologic" };
                                string[] examValues = { generalAppearance, skin, headAndFace, eyes, neck, chestLungs, heart, abdomen, extremities, neurologic };
                                float colWidth5 = contentWidth / 5f;

                                // iterate blocks of up to 5 columns per row
                                for (; physicalExamOuterIndex < examTitles.Length; physicalExamOuterIndex += 5)
                                {
                                    // Estimate needed space conservatively
                                    float needed = 40f;
                                    if (!EnsureSpace(g, e, ref currentY, needed, bottomLimit))
                                    {
                                        stopNow = true; break;
                                    }

                                    int cMax = Math.Min(5, examTitles.Length - physicalExamOuterIndex);
                                    float startX = leftMargin;
                                    float blockMaxHeight = 0f;

                                    for (int c = 0; c < cMax; c++)
                                    {
                                        int idx = physicalExamOuterIndex + c;
                                        string title = examTitles[idx];
                                        string val = examValues[idx];

                                        // draw title (small, underlined)
                                        float colX = startX + (colWidth5 * c);
                                        // title (no full box, just bold title)
                                        g.DrawString(title + ":", sectionFont, Brushes.Black, colX, currentY);

                                        // bullets for value
                                        if (!string.IsNullOrWhiteSpace(val))
                                        {
                                            string[] bullets = val.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                                            float localY = currentY + 16f;
                                            foreach (var b in bullets)
                                            {
                                                SizeF s = g.MeasureString("• " + b, bodyFont, (int)colWidth5);
                                                // check page space for the largest bullet in this column
                                                if (localY + s.Height > bottomLimit)
                                                {
                                                    // force new page before printing this column block
                                                    stopNow = true;
                                                    break;
                                                }
                                                g.DrawString("• " + b, bodyFont, Brushes.Black, new RectangleF(colX + 4f, localY, colWidth5 - 6f, s.Height));
                                                localY += s.Height + 2f;
                                                if (localY - currentY > blockMaxHeight) blockMaxHeight = localY - currentY;
                                            }
                                        }
                                    }

                                    if (stopNow) break;

                                    // advance Y by block height
                                    currentY += Math.Max(28f, blockMaxHeight + 8f);
                                }

                                if (stopNow) break;

                                // finished physical exam
                                physicalExamOuterIndex = 0;
                                printStage = 5;
                                break;
                            }

                        case 5: // ENT Exams (Ear, Nose, Throat, Maxillo, Head&Neck, Others)
                            {
                                DrawUnderlinedTitle(g, "ENT Examination", sectionFont, leftMargin, ref currentY, contentWidth);

                                string[] entLabels = { "Ear Exam", "Nose Exam", "Throat Exam", "Maxillofacial Exam", "Head & Neck Exam", "Other Exam" };
                                string[] entValues = { earExam, noseExam, throatExam, maxillofacialExam, headAndNeckExam, othersExam };

                                for (; entExamIndex < entLabels.Length; entExamIndex++)
                                {
                                    string label = entLabels[entExamIndex];
                                    string val = entValues[entExamIndex];
                                    if (string.IsNullOrWhiteSpace(val))
                                        continue;

                                    // draw title (underlined)
                                    DrawUnderlinedTitle(g, label, sectionFont, leftMargin, ref currentY, contentWidth);

                                    // bullets with wrapping
                                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near, Trimming = StringTrimming.Word };
                                    string[] bullets = val.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();

                                    foreach (var b in bullets)
                                    {
                                        string bulletText = "• " + b;
                                        SizeF size = g.MeasureString(bulletText, bodyFont, (int)(contentWidth - 20f));
                                        if (!EnsureSpace(g, e, ref currentY, size.Height + 4f, bottomLimit))
                                        {
                                            stopNow = true; break;
                                        }
                                        g.DrawString(bulletText, bodyFont, Brushes.Black, new RectangleF(leftMargin + 10f, currentY, contentWidth - 20f, size.Height), sf);
                                        currentY += size.Height + 4f;
                                    }

                                    if (stopNow) break;
                                    currentY += 6f; // small gap
                                }

                                if (stopNow) break;

                                // done
                                entExamIndex = 0;
                                printStage = 6;
                                break;
                            }

                        case 6: // Diagnosis & Recommendations
                            {
                                DrawUnderlinedTitle(g, "Diagnosis & Recommendations", sectionFont, leftMargin, ref currentY, contentWidth);

                                float colWidth = (contentWidth / 2) - 10f;
                                float mid = leftMargin + contentWidth / 2 + 10f;

                                // Measure height of diagnosis
                                SizeF diagSize = g.MeasureString(diagnosis ?? "", bodyFont, (int)colWidth);
                                float diagHeight = diagSize.Height;

                                // Draw Diagnosis
                                g.DrawString("Diagnosis:", sectionFont, Brushes.Black, leftMargin, currentY);
                                g.DrawString(diagnosis ?? "", bodyFont, Brushes.Black, new RectangleF(leftMargin, currentY + 16f, colWidth, diagHeight));

                                // Draw Recommendations inline, starting at the same Y as Diagnosis
                                g.DrawString("Recommendations:", sectionFont, Brushes.Black, mid, currentY);
                                g.DrawString(recommendations ?? "", bodyFont, Brushes.Black, new RectangleF(mid, currentY + 16f, colWidth, diagHeight));

                                // Advance currentY by max height of the two columns + spacing
                                float recHeight = g.MeasureString(recommendations ?? "", bodyFont, (int)colWidth).Height;
                                currentY += Math.Max(diagHeight, recHeight) + 32f; // extra spacing

                                printStage = 7;
                                break;
                            }


                        case 7: // Prescriptions
                            {
                                if (prescriptions.Count > 0)
                                {
                                    DrawUnderlinedTitle(g, "Prescriptions", sectionFont, leftMargin, ref currentY, contentWidth);

                                    using (Font itemFont = new Font("Segoe UI", 9f, FontStyle.Regular))
                                    using (Font sigFont = new Font("Segoe UI", 8f, FontStyle.Italic))
                                    {
                                        for (; prescriptionsIndex < prescriptions.Count; prescriptionsIndex++)
                                        {
                                            var p = prescriptions[prescriptionsIndex];
                                            // estimate size for item
                                            SizeF nameSize = g.MeasureString(p.GenericName + " (" + p.BrandName + ")", itemFont, (int)(contentWidth - 20f));
                                            SizeF infoSize = g.MeasureString($"{p.Strength} - {p.Dosage}    Qty: {p.Quantity}", itemFont, (int)(contentWidth - 20f));
                                            SizeF sigSize = g.MeasureString("Sig: " + p.Sig, sigFont, (int)(contentWidth - 40f));

                                            float needed = nameSize.Height + infoSize.Height + (string.IsNullOrWhiteSpace(p.Sig) ? 6f : sigSize.Height + 10f) + 8f;

                                            if (!EnsureSpace(g, e, ref currentY, needed, bottomLimit))
                                            {
                                                stopNow = true;
                                                break;
                                            }

                                            g.DrawString(p.GenericName + (string.IsNullOrWhiteSpace(p.BrandName) ? "" : $" ({p.BrandName})"), itemFont, Brushes.Black, leftMargin, currentY);
                                            currentY += nameSize.Height + 2f;

                                            g.DrawString($"{p.Strength} - {p.Dosage}    Qty: {p.Quantity}", itemFont, Brushes.Black, leftMargin + 8f, currentY);
                                            currentY += infoSize.Height + 4f;

                                            if (!string.IsNullOrEmpty(p.Sig))
                                            {
                                                RectangleF sigRect = new RectangleF(leftMargin + 12f, currentY, contentWidth - 24f, sigSize.Height);
                                                g.DrawString("Sig: " + p.Sig, sigFont, Brushes.Black, sigRect);
                                                currentY += sigSize.Height + 6f;
                                            }

                                            g.DrawLine(Pens.LightGray, leftMargin, currentY, leftMargin + contentWidth, currentY);
                                            currentY += 8f;
                                        }
                                    }
                                }

                                if (stopNow) break;

                                prescriptionsIndex = 0;
                                printStage = 8;
                                break;
                            }

                        case 8: // Notes & Follow-up
                            {
                                if (!string.IsNullOrWhiteSpace(notes))
                                {
                                    DrawUnderlinedTitle(g, "Notes", sectionFont, leftMargin, ref currentY, contentWidth);
                                    DrawWrappedText(g, notes, bodyFont, leftMargin, contentWidth, ref currentY, e, bottomLimit);
                                }

                                if (!string.IsNullOrWhiteSpace(followUpNotes) || followUpDate.HasValue)
                                {
                                    DrawUnderlinedTitle(g, "Follow-up", sectionFont, leftMargin, ref currentY, contentWidth);
                                    if (followUpDate.HasValue)
                                        DrawWrappedText(g, "Follow-up Date: " + followUpDate.Value.ToString("MMMM dd, yyyy"), bodyFont, leftMargin, contentWidth, ref currentY, e, bottomLimit);

                                    //if (!string.IsNullOrWhiteSpace(followUpNotes))
                                    //    DrawWrappedText(g, followUpNotes, bodyFont, leftMargin, contentWidth, ref currentY, e, bottomLimit);
                                }

                                printStage = 9;
                                break;
                            }

                        case 9: // Footer (ensure footer prints on this last page)
                            {
                                // If we don't have room for footer at bottom, request a new page to leave footer on final page
                                float footerNeeded = _isLongBondPaper ? 110f : 90f;
                                if (currentY + footerNeeded > e.MarginBounds.Bottom)
                                {
                                    // request next page; headerPrintedThisPage set by EnsureSpace path
                                    e.HasMorePages = true;
                                    headerPrintedThisPage = false;
                                    stopNow = true;
                                    break;
                                }

                                // Draw footer footer area and watermark at bottom
                                float footerY = e.PageBounds.Bottom - (_isLongBondPaper ? 110f : 80f);
                                using (Font footerTitle = new Font("Segoe UI", 9f, FontStyle.Bold))
                                using (Font footerFont = new Font("Segoe UI", 8f, FontStyle.Regular))
                                {


                                    // clinic footer note if any
                                    //g.DrawString("Patient consultation record – clinic copy.", footerFont, Brushes.Gray, leftMargin, footerY + 50f);


                                }

                                // draw watermark footer using helper (on the final page)
                                WaterMarkHelper.PrintFooter(g, 0, (int)(e.PageBounds.Height - 100), e.PageBounds.Width - 75);

                                // done printing
                                e.HasMorePages = false;
                                headerPrintedThisPage = false;
                                // reset state to allow reuse of object
                                ResetState();
                                stopNow = true;
                                break;
                            }

                        default:
                            {
                                // safety: stop and avoid infinite loops
                                e.HasMorePages = false;
                                ResetState();
                                stopNow = true;
                                break;
                            }
                    } // switch
                } // while
            } // using fonts
        }

        private void ResetState()
        {
            printStage = 0;
            patientInfoRowIndex = 0;
            physicalExamOuterIndex = 0;
            entExamIndex = 0;
            prescriptionsIndex = 0;
            currentY = 0f;
            headerPrintedThisPage = false;
        }
        #endregion

        #region Preview helper
        public void ShowPreview()
        {
            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = printDocument;
                preview.Width = 900;
                preview.Height = 700;

                preview.Shown += delegate
                {
                    // Add a custom Print button that shows PrintDialog (safer than the preview default)
                    ToolStrip tool = preview.Controls.OfType<ToolStrip>().FirstOrDefault();
                    if (tool != null)
                    {
                        // hide default print button if present
                        foreach (ToolStripItem it in tool.Items)
                        {
                            if (it is ToolStripButton btn && btn.ToolTipText != null && btn.ToolTipText.ToLower().Contains("print"))
                                it.Visible = false;
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
        #endregion
    }
}
