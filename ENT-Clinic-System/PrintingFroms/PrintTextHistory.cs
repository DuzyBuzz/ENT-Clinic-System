using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
        private DateTime birthDate;
        private int patientAge;

        // Consultation info
        private DateTime consultationDate;
        private string doctorName, chiefComplaint, history, earExam, noseExam, throatExam, neckExam,
                       diagnosis, recommendations, notes, followUpNotes;
        private DateTime? followUpDate;

        private List<(string Title, string Body)> printSections;
        private int currentSectionIndex = 0;

        private PrintDocument printDocument;
        private int pageNumber = 1;

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

        private void LoadData()
        {
            using (var conn = DBConfig.GetConnection())
            {
                conn.Open();

                // Load patient info
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

                // Load consultation info
                using (var cmd = new MySqlCommand(@"
                    SELECT consultation_date, doctor_name, chief_complaint, history, ear_exam, nose_exam, throat_exam, neck_exam,
                           diagnosis, recommendations, notes, follow_up_date, follow_up_notes
                    FROM consultation WHERE consultation_id=@consultationId", conn))
                {
                    cmd.Parameters.AddWithValue("@consultationId", consultationId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime.TryParse(reader["consultation_date"]?.ToString(), out consultationDate);
                            doctorName = reader["doctor_name"]?.ToString() ?? "";
                            chiefComplaint = reader["chief_complaint"]?.ToString() ?? "";
                            history = reader["history"]?.ToString() ?? "";
                            earExam = reader["ear_exam"]?.ToString();
                            noseExam = reader["nose_exam"]?.ToString();
                            throatExam = reader["throat_exam"]?.ToString();
                            neckExam = reader["neck_exam"]?.ToString();
                            diagnosis = reader["diagnosis"]?.ToString() ?? "";
                            recommendations = reader["recommendations"]?.ToString() ?? "";
                            notes = reader["notes"]?.ToString() ?? "";
                            followUpNotes = reader["follow_up_notes"]?.ToString();

                            DateTime tempDate;
                            if (DateTime.TryParse(reader["follow_up_date"]?.ToString(), out tempDate))
                                followUpDate = tempDate;
                            else
                                followUpDate = null;
                        }
                    }
                }
            }
        }

        private void BuildPrintSections()
        {
            printSections = new List<(string Title, string Body)>
            {
                ("Patient Name", patientName),
                ("Address", patientAddress),
                ("Age", patientAge.ToString()),
                ("Sex", patientSex),
                ("Civil Status", civilStatus),
                ("Contact", patientContact),
                ("Emergency Contact", string.IsNullOrEmpty(emergencyName) ? "" : $"{emergencyName} ({emergencyRelationship}): {emergencyContact}"),
                ("Doctor", doctorName),
                ("Consultation Date", consultationDate.ToString("MMMM dd, yyyy")),
                ("Chief Complaint", chiefComplaint),
                ("History of Illness", history)
            };

            // ENT/Neck exams (only if not empty)
            if (!string.IsNullOrWhiteSpace(earExam)) printSections.Add(("Ear Examination", earExam));
            if (!string.IsNullOrWhiteSpace(noseExam)) printSections.Add(("Nose Examination", noseExam));
            if (!string.IsNullOrWhiteSpace(throatExam)) printSections.Add(("Throat Examination", throatExam));
            if (!string.IsNullOrWhiteSpace(neckExam)) printSections.Add(("Neck Examination", neckExam));

            if (!string.IsNullOrWhiteSpace(diagnosis)) printSections.Add(("Diagnosis", diagnosis));
            if (!string.IsNullOrWhiteSpace(recommendations)) printSections.Add(("Recommendations", recommendations));
            if (!string.IsNullOrWhiteSpace(notes)) printSections.Add(("Notes", notes));
            if (followUpDate.HasValue || !string.IsNullOrWhiteSpace(followUpNotes))
                printSections.Add(("Follow-Up", $"{followUpDate?.ToString("MMMM dd, yyyy") ?? ""}\n{followUpNotes ?? ""}"));
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float leftMargin = 50;
            float rightMargin = 50;
            float contentWidth = e.PageBounds.Width - leftMargin - rightMargin;
            float y = 50;
            float pageBottom = e.MarginBounds.Bottom;

            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font headerFont = new Font("Arial", 11, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 11, FontStyle.Regular);

            StringFormat wrapFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.Word
            };

            // Header
            y = WaterMarkHelper.PrintHeader(g, (int)leftMargin, (int)y, e.PageBounds.Width);

            // Title
            g.DrawString("ENT CONSULTATION REPORT", titleFont, Brushes.Black,
                new RectangleF(leftMargin, y, contentWidth, 30),
                new StringFormat { Alignment = StringAlignment.Center });
            y += 50;

            // Sections
            while (currentSectionIndex < printSections.Count)
            {
                var (label, value) = printSections[currentSectionIndex];

                if (string.IsNullOrWhiteSpace(value))
                {
                    currentSectionIndex++;
                    continue;
                }

                // Bold major headers for exams and diagnosis
                bool isMajorHeader = label.Contains("Examination") || label == "Diagnosis" || label == "Recommendations";
                Font useFont = isMajorHeader ? new Font(headerFont, FontStyle.Bold) : bodyFont;

                SizeF labelSize = g.MeasureString(label + ":", headerFont);
                SizeF valueSize = g.MeasureString(value, bodyFont, new SizeF(contentWidth - 150, float.MaxValue), wrapFormat);

                // Page-break check
                if (y + Math.Max(labelSize.Height, valueSize.Height) > pageBottom - 80)
                {
                    e.HasMorePages = true;
                    return;
                }

                g.DrawString(label + ":", headerFont, Brushes.Black, leftMargin, y);
                g.DrawString(value, useFont, Brushes.Black,
                    new RectangleF(leftMargin + 150, y, contentWidth - 150, valueSize.Height), wrapFormat);

                y += Math.Max(labelSize.Height, valueSize.Height) + 15; // extra spacing
                currentSectionIndex++;
            }

            // Footer with page number
            WaterMarkHelper.PrintFooter(g, (int)leftMargin, e.MarginBounds.Bottom - 60, e.MarginBounds.Width);
            g.DrawString($"Page {pageNumber}", new Font("Arial", 9), Brushes.Gray,
                e.PageBounds.Width - rightMargin - 50, e.MarginBounds.Bottom + 10);
            pageNumber++;

            e.HasMorePages = false;
            currentSectionIndex = 0;
            pageNumber = 1;
        }

        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1000,
                Height = 700
            };

            preview.Shown += delegate
            {
                ToolStrip tool = preview.Controls.OfType<ToolStrip>().FirstOrDefault();
                if (tool != null)
                {
                    foreach (ToolStripItem item in tool.Items)
                        if (item is ToolStripButton btn && btn.ToolTipText.ToLower().Contains("print"))
                            btn.Visible = false;

                    ToolStripButton customPrint = new ToolStripButton("Print");
                    customPrint.Click += delegate
                    {
                        using (PrintDialog printDialog = new PrintDialog
                        {
                            Document = printDocument,
                            AllowSomePages = true,
                            AllowSelection = true
                        })
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
