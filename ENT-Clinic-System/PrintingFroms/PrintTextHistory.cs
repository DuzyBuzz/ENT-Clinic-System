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
        // --- Identifiers ---
        private int patientId;
        private int consultationId;

        // --- Patient info ---
        private string patientName, patientAddress, patientSex, civilStatus, patientContact;
        private string emergencyName, emergencyContact, emergencyRelationship;
        private int patientAge;
        private DateTime birthDate;

        // --- Consultation info ---
        private DateTime consultationDate;
        private string doctorName, chiefComplaint, history;
        private string earExam, noseExam, throatExam, neckExam, othersExam;
        private string diagnosis, recommendations, notes, followUpNotes;
        private DateTime? followUpDate;

        // --- Health record info ---
        private string pastMedicalHistory, familyHistory, personalSocialHistory, allergies;
        private string bp, temperature, pr, rr, ht, wt;
        private string generalAppearance, skin, headAndFace, eyes, neck, chestLungs, heart, abdomen, extremities, neurologic;

        // --- Prescriptions (combined from prescription + prescription_other) ---
        private readonly List<(string GenericName, string BrandName, string Strength, string Dosage, int Quantity, string Sig)> prescriptions
            = new List<(string, string, string, string, int, string)>();

        // --- Print and pagination state ---
        private PrintDocument printDocument;
        public PrintDocument Document => printDocument;

        // Pagination state
        // stage indicates which major block we are printing (so we can resume)
        // stages:
        // 0 = start/header/title, 1 = patient info rows, 2 = consultation details (doctor/date), 3 = chief/history,
        // 4 = vitals, 5 = medical/family/social/allergy, 6 = physical exam block, 7 = ENT exam block,
        // 8 = diagnosis/recommendations, 9 = prescriptions, 10 = footer/done
        private int printStage = 0;

        // Sub indices used to resume lists across pages
        private int patientInfoRowIndex = 0;
        private int physicalExamOuterIndex = 0; // for 5-per-row loop
        private int physicalExamInnerMax = 0;
        private int entExamIndex = 0;
        private int entBulletIndex = 0;
        private int prescriptionsIndex = 0;
        private bool headerPrintedThisPage = false;
        private float currentY = 0f;

        public PrintTextHistory(int patientId, int consultationId)
        {
            this.patientId = patientId;
            this.consultationId = consultationId;

            LoadData();

            printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
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

                // --- PRESCRIPTIONS: combine prescription + prescription_other (same as your PrescriptionPrintHelper) ---
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
                                ToTitleCase(reader["generic_name"]?.ToString()),
                                ToTitleCase(reader["brand_name"]?.ToString()),
                                reader["strength"]?.ToString(),
                                reader["dosage"]?.ToString(),
                                reader["quantity"] != DBNull.Value ? Convert.ToInt32(reader["quantity"]) : 0,
                                reader["sig"]?.ToString()
                            ));
                        }
                    }
                }
            }
        }

        private static string ToTitleCase(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }
        #endregion

        #region Print Helpers (space checks and small drawers)

        /// <summary>
        /// Checks if there is room for 'needed' vertical space. If not, set HasMorePages and save currentY.
        /// Returns true if enough space, false if no space and HasMorePages set.
        /// </summary>
        private bool EnsureSpace(Graphics g, PrintPageEventArgs e, ref float y, float needed, float bottomLimit)
        {
            if (y + needed > bottomLimit)
            {
                // Save the position and indicate another page is required
                currentY = y;
                e.HasMorePages = true;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Simple wrapper to draw a labelled row with optional 4 columns like your original DrawPatientInfoRow.
        /// This does not check all wrap situations — keep rows small.
        /// </summary>
        private void DrawPatientInfoRow(Graphics g, ref float y, float leftMargin, float contentWidth, PrintPageEventArgs e,
            string c1Label, string c1Val,
            string c2Label = null, string c2Val = null,
            string c3Label = null, string c3Val = null,
            string c4Label = null, string c4Val = null,
            float bottomLimit = float.MaxValue)
        {
            using (Font bodyFont = new Font("Arial", 8, FontStyle.Regular))
            {
                float colWidth4s = contentWidth / 4f;

                // Estimate height needed
                float needed = 20f;
                if (!EnsureSpace(g, e, ref y, needed, bottomLimit)) return;

                if (!string.IsNullOrEmpty(c1Label))
                    g.DrawString($"{c1Label}: {c1Val}", bodyFont, Brushes.Black, leftMargin, y);

                if (!string.IsNullOrEmpty(c2Label))
                    g.DrawString($"{c2Label}: {c2Val}", bodyFont, Brushes.Black, leftMargin + colWidth4s, y);

                if (!string.IsNullOrEmpty(c3Label))
                    g.DrawString($"{c3Label}: {c3Val}", bodyFont, Brushes.Black, leftMargin + (colWidth4s * 2), y);

                if (!string.IsNullOrEmpty(c4Label))
                    g.DrawString($"{c4Label}: {c4Val}", bodyFont, Brushes.Black, leftMargin + (colWidth4s * 3), y);

                y += 20;
                g.DrawLine(Pens.LightGray, leftMargin, y, leftMargin + contentWidth, y);
                y += 5;
            }
        }

        #endregion

        #region Main Print Logic with Pagination State Machine

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // Margins and layout
            float leftMargin = 40f;
            float rightMargin = 40f;
            float topMargin = 40f;
            float footerHeightReserve = 110f; // reserve area so footer can be placed cleanly on last page
            float contentWidth = e.PageBounds.Width - leftMargin - rightMargin;
            float pageTop = topMargin;
            float pageBottom = e.MarginBounds.Bottom - 20f; // general bottom limit
            float bottomForContent = pageBottom - footerHeightReserve; // avoid overwriting footer area

            // Fonts
            Font titleFont = new Font("Arial", 12, FontStyle.Bold);
            Font sectionFont = new Font("Arial", 8, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 8, FontStyle.Regular);

            // If this is the first time PrintPage is called for the job, reset state
            if (printStage == 0 && currentY == 0f)
            {
                headerPrintedThisPage = false;
                currentY = pageTop;
            }
            else if (!headerPrintedThisPage)
            {
                // if moving to a new page in middle of job, reset header flag
                currentY = pageTop;
            }

            // Ensure header printed each page
            if (!headerPrintedThisPage)
            {
                // Print header and title at top of each page
                currentY = WaterMarkHelper.PrintHeader(g, (int)leftMargin, (int)currentY, e.PageBounds.Width);
                g.DrawString("CONSULTATION HISTORY", titleFont, Brushes.Black,
                    new RectangleF(leftMargin, currentY, contentWidth, 25),
                    new StringFormat { Alignment = StringAlignment.Center });
                currentY += 35;
                headerPrintedThisPage = true;
            }

            // We'll repeatedly attempt to draw from the current printStage until we either
            // finish all stages or we run out of vertical space and set e.HasMorePages = true.
            bool pageFull = false;

            // Helper lambda to stop printing on page and request another
            Action stopPage = () =>
            {
                currentY = currentY; // preserved
                e.HasMorePages = true;
                pageFull = true;
            };

            // Stage-driven printing: do not reinitialize variables that track indices between pages.
            while (!pageFull && printStage <= 10)
            {
                switch (printStage)
                {
                    case 0: // Patient Info block (rows)
                        {
                            // We'll print a few patient rows. We maintain patientInfoRowIndex to resume.
                            List<Action> patientRows = new List<Action>
                            {
                                () => DrawPatientInfoRow(g, ref currentY, leftMargin, contentWidth, e,
                                    "Name", patientName, "Age", patientAge.ToString(), "Sex", patientSex, "Civil Status", civilStatus, bottomLimit: bottomForContent),
                                () => DrawPatientInfoRow(g, ref currentY, leftMargin, contentWidth, e,
                                    "Address", patientAddress, null, null, "Contact Number", patientContact, null, null, bottomLimit: bottomForContent),
                                () => {
                                    if (!string.IsNullOrEmpty(emergencyName))
                                        DrawPatientInfoRow(g, ref currentY, leftMargin, contentWidth, e,
                                            "Contact in case of Emergency", $"{emergencyName} {emergencyRelationship} {emergencyContact}", null, null, null, null, null, null, bottomLimit: bottomForContent);
                                    else
                                        DrawPatientInfoRow(g, ref currentY, leftMargin, contentWidth, e, null, null, null, null, null, null, null, null, bottomLimit: bottomForContent);
                                }
                            };

                            for (; patientInfoRowIndex < patientRows.Count; patientInfoRowIndex++)
                            {
                                // before invoking, check minimal space
                                if (!EnsureSpace(g, e, ref currentY, 30f, bottomForContent))
                                {
                                    stopPage();
                                    break;
                                }

                                patientRows[patientInfoRowIndex].Invoke();

                                if (e.HasMorePages) { stopPage(); break; }
                            }

                            if (!pageFull && patientInfoRowIndex >= patientRows.Count)
                            {
                                // advance stage
                                printStage = 1;
                                patientInfoRowIndex = 0;
                            }
                        }
                        break;

                    case 1: // Consultation Details (Doctor, Date) & Chief Complaint / Recent Illness
                        {
                            // Doctor & Date row
                            if (!EnsureSpace(g, e, ref currentY, 30f, bottomForContent))
                            {
                                stopPage(); break;
                            }

                            // Doctor and Date
                            g.DrawString("Doctor: " + doctorName, bodyFont, Brushes.Black, leftMargin, currentY);
                            g.DrawString("Date: " + consultationDate.ToString("MMMM dd, yyyy"), bodyFont, Brushes.Black, leftMargin + contentWidth / 2, currentY);
                            currentY += 25;

                            // Chief complaint (two column style previously used)
                            // We'll draw headings then the wrapped text in two halves
                            if (!EnsureSpace(g, e, ref currentY, 30f, bottomForContent))
                            { stopPage(); break; }

                            // Draw Section Header
                            g.FillRectangle(Brushes.LightGray, leftMargin, currentY, contentWidth, 22);
                            g.DrawRectangle(Pens.Gray, leftMargin, currentY, contentWidth, 22);
                            g.DrawString("Chief Complaint", sectionFont, Brushes.Black, leftMargin + 5, currentY + 3);
                            g.DrawString("Recent Illness", sectionFont, Brushes.Black, leftMargin + contentWidth / 2 + 5, currentY + 3);
                            currentY += 25;

                            // Draw chief complaint and history as comma split bullets in columns with wrapping
                            var leftItems = string.IsNullOrWhiteSpace(chiefComplaint) ? new string[0] : chiefComplaint.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                            var rightItems = string.IsNullOrWhiteSpace(history) ? new string[0] : history.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                            int maxLines = Math.Max(leftItems.Length, rightItems.Length);
                            float columnWidth = contentWidth / 2 - 10;
                            StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near, FormatFlags = StringFormatFlags.LineLimit };

                            for (int i = 0; i < maxLines; i++)
                            {
                                string l = i < leftItems.Length ? "• " + leftItems[i] : "";
                                string r = i < rightItems.Length ? "• " + rightItems[i] : "";

                                SizeF leftSize = g.MeasureString(l, bodyFont, (int)columnWidth, sf);
                                SizeF rightSize = g.MeasureString(r, bodyFont, (int)columnWidth, sf);
                                float lineHeight = Math.Max(leftSize.Height, rightSize.Height);

                                if (!EnsureSpace(g, e, ref currentY, lineHeight + 6f, bottomForContent))
                                { stopPage(); break; }

                                RectangleF leftRect = new RectangleF(leftMargin + 5, currentY, columnWidth, lineHeight);
                                RectangleF rightRect = new RectangleF(leftMargin + contentWidth / 2 + 5, currentY, columnWidth, lineHeight);

                                if (!string.IsNullOrEmpty(l)) g.DrawString(l, bodyFont, Brushes.Black, leftRect, sf);
                                if (!string.IsNullOrEmpty(r)) g.DrawString(r, bodyFont, Brushes.Black, rightRect, sf);

                                currentY += lineHeight + 4;
                            }

                            if (!pageFull)
                            {
                                printStage = 2;
                            }
                        }
                        break;

                    case 2: // Vital Signs
                        {
                            if (!EnsureSpace(g, e, ref currentY, 30f, bottomForContent))
                            { stopPage(); break; }

                            g.DrawString($" BP: {bp}                Temp: {temperature}                         PR: {pr}                    RR: {rr}                        Ht: {ht}                    Wt: {wt}", bodyFont, Brushes.Black, leftMargin, currentY);
                            currentY += 30;
                            printStage = 3;
                        }
                        break;

                    case 3: // Medical / Family / Social / Allergies
                        {
                            // Medical & Family (two column list)
                            if (!EnsureSpace(g, e, ref currentY, 30f, bottomForContent))
                            { stopPage(); break; }

                            // Medical / Family
                            {
                                // Draw header for the two-column section
                                g.FillRectangle(Brushes.LightGray, leftMargin, currentY, contentWidth, 22);
                                g.DrawRectangle(Pens.Gray, leftMargin, currentY, contentWidth, 22);
                                g.DrawString("Medical History", sectionFont, Brushes.Black, leftMargin + 5, currentY + 3);
                                g.DrawString("Family History", sectionFont, Brushes.Black, leftMargin + contentWidth / 2 + 5, currentY + 3);
                                currentY += 25;

                                var leftItems = string.IsNullOrWhiteSpace(pastMedicalHistory) ? new string[0] : pastMedicalHistory.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                                var rightItems = string.IsNullOrWhiteSpace(familyHistory) ? new string[0] : familyHistory.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                                int maxLines = Math.Max(leftItems.Length, rightItems.Length);
                                float columnWidth = contentWidth / 2 - 10;
                                StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near, FormatFlags = StringFormatFlags.LineLimit };

                                for (int i = 0; i < maxLines; i++)
                                {
                                    string l = i < leftItems.Length ? "• " + leftItems[i] : "";
                                    string r = i < rightItems.Length ? "• " + rightItems[i] : "";

                                    SizeF leftSize = g.MeasureString(l, bodyFont, (int)columnWidth, sf);
                                    SizeF rightSize = g.MeasureString(r, bodyFont, (int)columnWidth, sf);
                                    float lineHeight = Math.Max(leftSize.Height, rightSize.Height);

                                    if (!EnsureSpace(g, e, ref currentY, lineHeight + 6f, bottomForContent))
                                    { stopPage(); break; }

                                    RectangleF leftRect = new RectangleF(leftMargin + 5, currentY, columnWidth, lineHeight);
                                    RectangleF rightRect = new RectangleF(leftMargin + contentWidth / 2 + 5, currentY, columnWidth, lineHeight);

                                    if (!string.IsNullOrEmpty(l)) g.DrawString(l, bodyFont, Brushes.Black, leftRect, sf);
                                    if (!string.IsNullOrEmpty(r)) g.DrawString(r, bodyFont, Brushes.Black, rightRect, sf);

                                    currentY += lineHeight + 4;
                                }
                            }

                            if (e.HasMorePages) { stopPage(); break; }

                            // Social History & Allergies (two column)
                            if (!EnsureSpace(g, e, ref currentY, 30f, bottomForContent))
                            { stopPage(); break; }

                            g.FillRectangle(Brushes.LightGray, leftMargin, currentY, contentWidth, 22);
                            g.DrawRectangle(Pens.Gray, leftMargin, currentY, contentWidth, 22);
                            g.DrawString("Social History", sectionFont, Brushes.Black, leftMargin + 5, currentY + 3);
                            g.DrawString("Allergies", sectionFont, Brushes.Black, leftMargin + contentWidth / 2 + 5, currentY + 3);
                            currentY += 25;

                            {
                                var leftItems = string.IsNullOrWhiteSpace(personalSocialHistory) ? new string[0] : personalSocialHistory.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                                var rightItems = string.IsNullOrWhiteSpace(allergies) ? new string[0] : allergies.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                                int maxLines = Math.Max(leftItems.Length, rightItems.Length);
                                float columnWidth = contentWidth / 2 - 10;
                                StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near, FormatFlags = StringFormatFlags.LineLimit };

                                for (int i = 0; i < maxLines; i++)
                                {
                                    string l = i < leftItems.Length ? "• " + leftItems[i] : "";
                                    string r = i < rightItems.Length ? "• " + rightItems[i] : "";

                                    SizeF leftSize = g.MeasureString(l, bodyFont, (int)columnWidth, sf);
                                    SizeF rightSize = g.MeasureString(r, bodyFont, (int)columnWidth, sf);
                                    float lineHeight = Math.Max(leftSize.Height, rightSize.Height);

                                    if (!EnsureSpace(g, e, ref currentY, lineHeight + 6f, bottomForContent))
                                    { stopPage(); break; }

                                    RectangleF leftRect = new RectangleF(leftMargin + 5, currentY, columnWidth, lineHeight);
                                    RectangleF rightRect = new RectangleF(leftMargin + contentWidth / 2 + 5, currentY, columnWidth, lineHeight);

                                    if (!string.IsNullOrEmpty(l)) g.DrawString(l, bodyFont, Brushes.Black, leftRect, sf);
                                    if (!string.IsNullOrEmpty(r)) g.DrawString(r, bodyFont, Brushes.Black, rightRect, sf);

                                    currentY += lineHeight + 4;
                                }
                            }

                            if (!pageFull)
                                printStage = 4;
                        }
                        break;

                    case 4: // Physical Examination (multi-column in groups of 5 as in your original code)
                        {
                            string[] examTitles = { "General Appearance", "Skin", "Head & Face", "Eyes", "Neck", "Chest & Lungs", "Heart", "Abdomen", "Extremities", "Neurologic" };
                            string[] examValues = { generalAppearance, skin, headAndFace, eyes, neck, chestLungs, heart, abdomen, extremities, neurologic };
                            float colWidth4 = contentWidth / 5f;

                            // We'll iterate in steps of 5 (like your original)
                            for (; physicalExamOuterIndex < examTitles.Length; physicalExamOuterIndex += 5)
                            {
                                // Measure the needed height for this 5-column block conservatively
                                float estimatedNeeded = 40f; // baseline
                                if (!EnsureSpace(g, e, ref currentY, estimatedNeeded, bottomForContent))
                                { stopPage(); break; }

                                float maxHeight = 0;
                                for (int c = 0; c < 5 && physicalExamOuterIndex + c < examTitles.Length; c++)
                                {
                                    var val = examValues[physicalExamOuterIndex + c];
                                    if (!string.IsNullOrWhiteSpace(val))
                                    {
                                        g.DrawString(examTitles[physicalExamOuterIndex + c] + "", sectionFont, Brushes.Black, leftMargin + c * colWidth4, currentY);
                                        var bullets = val.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                                        float lineOffset = 18f;
                                        foreach (var b in bullets)
                                        {
                                            // For each bullet we check space and print
                                            if (!EnsureSpace(g, e, ref currentY, 0f, bottomForContent)) { stopPage(); break; }
                                            g.DrawString("" + b, bodyFont, Brushes.Black, leftMargin + c * colWidth4 + 5, currentY + lineOffset);
                                            lineOffset += 16f;
                                        }
                                        if (lineOffset > maxHeight) maxHeight = lineOffset;
                                    }
                                }

                                currentY += maxHeight + 10f;
                                if (e.HasMorePages) { stopPage(); break; }
                            }

                            if (!pageFull)
                                printStage = 5;
                        }
                        break;

                    case 5: // ENT Examination (Ear, Nose, Throat, Other) — vertical layout with bullets & wrapping
                        {
                            string[] entLabels = { "Ear Exam", "Nose Exam", "Throat Exam", "Other Exam" };
                            string[] entValues = { earExam, noseExam, throatExam, othersExam };
                            StringFormat sfs = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near, FormatFlags = StringFormatFlags.LineLimit };

                            for (; entExamIndex < entLabels.Length; entExamIndex++)
                            {
                                if (string.IsNullOrWhiteSpace(entValues[entExamIndex]))
                                    continue;

                                // section label
                                if (!EnsureSpace(g, e, ref currentY, 26f, bottomForContent)) { stopPage(); break; }
                                g.DrawString(entLabels[entExamIndex] + ":", sectionFont, Brushes.Black, leftMargin, currentY);
                                currentY += 22f;

                                // bullets
                                var bullets = entValues[entExamIndex].Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                                for (int bi = 0; bi < bullets.Length; bi++)
                                {
                                    if (!EnsureSpace(g, e, ref currentY, 18f, bottomForContent)) { stopPage(); break; }

                                    string bulletText = "• " + bullets[bi];
                                    float textWidth = contentWidth - 10f;
                                    SizeF textSize = g.MeasureString(bulletText, bodyFont, (int)textWidth, sfs);

                                    RectangleF textRect = new RectangleF(leftMargin + 10, currentY, textWidth, textSize.Height);
                                    g.DrawString(bulletText, bodyFont, Brushes.Black, textRect, sfs);

                                    currentY += textSize.Height + 4f;
                                }

                                currentY += 10f;
                                if (e.HasMorePages) { stopPage(); break; }
                            }

                            if (!pageFull)
                                printStage = 6;
                        }
                        break;

                    case 6: // Diagnosis & Recommendations (two column text blocks)
                        {
                            // Header
                            if (!EnsureSpace(g, e, ref currentY, 30f, bottomForContent)) { stopPage(); break; }

                            // Draw Section header style then fill left and right columns with wrapped text
                            g.FillRectangle(Brushes.LightGray, leftMargin, currentY, contentWidth, 22);
                            g.DrawRectangle(Pens.Gray, leftMargin, currentY, contentWidth, 22);
                            g.DrawString("Diagnosis", sectionFont, Brushes.Black, leftMargin + 5, currentY + 3);
                            g.DrawString("Recommendations", sectionFont, Brushes.Black, leftMargin + contentWidth / 2 + 5, currentY + 3);
                            currentY += 25;

                            var leftItems = string.IsNullOrWhiteSpace(diagnosis) ? new string[0] : diagnosis.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                            var rightItems = string.IsNullOrWhiteSpace(recommendations) ? new string[0] : recommendations.Split(',').Select(s => s.Trim()).Where(s => s != "").ToArray();
                            int maxLines = Math.Max(leftItems.Length, rightItems.Length);
                            float columnWidth = contentWidth / 2 - 10;
                            StringFormat sf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near, FormatFlags = StringFormatFlags.LineLimit };

                            for (int i = 0; i < maxLines; i++)
                            {
                                string l = i < leftItems.Length ? leftItems[i] : "";
                                string r = i < rightItems.Length ? rightItems[i] : "";

                                SizeF leftSize = g.MeasureString(l, bodyFont, (int)columnWidth, sf);
                                SizeF rightSize = g.MeasureString(r, bodyFont, (int)columnWidth, sf);
                                float lineHeight = Math.Max(leftSize.Height, rightSize.Height);

                                if (!EnsureSpace(g, e, ref currentY, lineHeight + 6f, bottomForContent))
                                { stopPage(); break; }

                                RectangleF leftRect = new RectangleF(leftMargin, currentY, columnWidth, lineHeight);
                                RectangleF rightRect = new RectangleF(leftMargin + contentWidth / 2, currentY, columnWidth, lineHeight);

                                g.DrawString(l, bodyFont, Brushes.Black, leftRect, sf);
                                g.DrawString(r, bodyFont, Brushes.Black, rightRect, sf);

                                currentY += lineHeight + 5f;
                            }

                            if (!pageFull)
                                printStage = 7;
                        }
                        break;

                    case 7: // Prescriptions (the new block inserted after Recommendations)
                        {
                            if (prescriptions.Count > 0)
                            {
                                // Section header
                                if (!EnsureSpace(g, e, ref currentY, 30f, bottomForContent)) { stopPage(); break; }
                                g.FillRectangle(Brushes.LightGray, leftMargin, currentY, contentWidth, 22);
                                g.DrawRectangle(Pens.Gray, leftMargin, currentY, contentWidth, 22);
                                g.DrawString("Prescriptions", sectionFont, Brushes.Black, leftMargin + 5, currentY + 3);
                                currentY += 30;

                                using (Font itemFont = new Font("Arial", 8))
                                using (Font sigFont = new Font("Arial", 8, FontStyle.Italic))
                                {
                                    for (; prescriptionsIndex < prescriptions.Count; prescriptionsIndex++)
                                    {
                                        var item = prescriptions[prescriptionsIndex];

                                        // Before printing each prescription entry, ensure enough minimal space
                                        if (!EnsureSpace(g, e, ref currentY, 40f, bottomForContent))
                                        {
                                            stopPage(); break;
                                        }

                                        // Generic (Brand)
                                        g.DrawString($"{item.GenericName} ({item.BrandName})", itemFont, Brushes.Black, leftMargin, currentY);
                                        currentY += 15f;

                                        // Strength - Dosage  and Qty
                                        g.DrawString($"{item.Strength} - {item.Dosage}     Qty: {item.Quantity}", itemFont, Brushes.Black, leftMargin + 10f, currentY);
                                        currentY += 15f;

                                        // Sig (with wrapping)
                                        if (!string.IsNullOrEmpty(item.Sig))
                                        {
                                            float sigX = leftMargin + 20f;
                                            float sigWidth = contentWidth - 40f;
                                            RectangleF sigRect = new RectangleF(sigX, currentY, sigWidth, 200f);
                                            StringFormat sigFmt = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near, Trimming = StringTrimming.Word, FormatFlags = StringFormatFlags.LineLimit };

                                            SizeF sigSize = g.MeasureString("Sig: " + item.Sig, sigFont, (int)sigWidth, sigFmt);

                                            if (!EnsureSpace(g, e, ref currentY, sigSize.Height + 8f, bottomForContent))
                                            {
                                                stopPage(); break;
                                            }

                                            g.DrawString("Sig: " + item.Sig, sigFont, Brushes.Black, sigRect, sigFmt);
                                            currentY += sigSize.Height + 5f;
                                        }

                                        // Separator line after each item
                                        g.DrawLine(Pens.LightGray, leftMargin, currentY, leftMargin + contentWidth, currentY);
                                        currentY += 10f;
                                    }
                                }
                            }
                            // done with prescriptions block (empty or fully printed)
                            if (!pageFull)
                                printStage = 8;
                        }
                        break;

                    case 8: // Notes / Follow-up notes (if you want to print them before footer)
                        {
                            // Optional notes block printing (if notes exist)
                            if (!string.IsNullOrWhiteSpace(notes))
                            {
                                if (!EnsureSpace(g, e, ref currentY, 30f, bottomForContent))
                                { stopPage(); break; }

                                g.FillRectangle(Brushes.LightGray, leftMargin, currentY, contentWidth, 22);
                                g.DrawRectangle(Pens.Gray, leftMargin, currentY, contentWidth, 22);
                                g.DrawString("Notes", sectionFont, Brushes.Black, leftMargin + 5, currentY + 3);
                                currentY += 25;

                                // draw notes in wrapped rectangle
                                StringFormat nf = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near, Trimming = StringTrimming.Word, FormatFlags = StringFormatFlags.LineLimit };
                                RectangleF notesRect = new RectangleF(leftMargin, currentY, contentWidth, 300f);
                                SizeF notesSize = g.MeasureString(notes, bodyFont, (int)contentWidth, nf);
                                if (!EnsureSpace(g, e, ref currentY, notesSize.Height + 6f, bottomForContent))
                                { stopPage(); break; }

                                g.DrawString(notes, bodyFont, Brushes.Black, notesRect, nf);
                                currentY += notesSize.Height + 5f;
                            }

                            // Follow-up notes area (if any)
                            if (!pageFull)
                                printStage = 9;
                        }
                        break;

                    case 9: // Footer / Follow-up label - BUT only print on last page; if it doesn't fit, request a new page so footer will sit on last page
                        {
                            // To ensure footer prints at bottom of last page, first check if there is room for footer area.
                            // If not, advance to a new blank page so footer sits at fixed bottom.
                            float footerNeeded = 80f; // space for "Follow-up visit on" and watermark footer
                            if (currentY + footerNeeded > pageBottom)
                            {
                                // Force another page so footer will be printed on the very last page
                                stopPage();
                                break;
                            }

                            // Draw follow-up label and value at fixed bottom
                            float footerY = e.PageBounds.Bottom - 70f;
                            using (Font labelFont = new Font("Arial", 9, FontStyle.Bold))
                            {
                                g.DrawString("Follow-up visit on:", labelFont, Brushes.Black, leftMargin + 30f, footerY);

                                if (followUpDate.HasValue)
                                {
                                    using (Font dateFont = new Font("Arial", 9, FontStyle.Underline))
                                    {
                                        string dateText = followUpDate.Value.ToString("MMMM dd, yyyy");
                                        float dateY = footerY + 18f;
                                        g.DrawString(dateText, dateFont, Brushes.Black, leftMargin + 30f, dateY);
                                    }
                                }
                            }

                            // Print watermark footer using your helper at bottom
                            WaterMarkHelper.PrintFooter(g, 0, (int)(e.PageBounds.Height - 100), e.PageBounds.Width - 75);

                            // Mark complete
                            printStage = 10;
                            pageFull = true;
                            e.HasMorePages = false;
                            break;
                        }

                    case 10: // Done printing everything.
                        e.HasMorePages = false;
                        pageFull = true;
                        break;

                    default:
                        e.HasMorePages = false;
                        pageFull = true;
                        break;
                } // switch
            } // while

            // If we set HasMorePages to true, prepare flags so next PrintPage call continues
            if (e.HasMorePages)
            {
                // For the next page, ensure header prints again and keep currentY preserved
                headerPrintedThisPage = false;
            }
            else
            {
                // Reset state at the end of the print job so the object can be reused for another preview/print
                headerPrintedThisPage = false;
                currentY = 0f;
                printStage = 0;
                patientInfoRowIndex = 0;
                physicalExamOuterIndex = 0;
                entExamIndex = 0;
                prescriptionsIndex = 0;
            }
        }

        #endregion

        #region Preview Helper
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
        #endregion
    }
}
