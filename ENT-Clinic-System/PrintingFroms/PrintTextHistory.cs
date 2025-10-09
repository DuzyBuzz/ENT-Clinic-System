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

        private PrintDocument printDocument;

        public PrintDocument Document => printDocument;

        public PrintTextHistory(int patientId, int consultationId)
        {
            this.patientId = patientId;
            this.consultationId = consultationId;

            LoadData();

            printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169); // A4
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
                           diagnosis, recommendations, notes, follow_up_date, follow_up_notes, age
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
                            int.TryParse(reader["age"]?.ToString(), out patientAge);

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

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float leftMargin = 40;
            float rightMargin = 40;
            float contentWidth = e.PageBounds.Width - leftMargin - rightMargin;
            float y = 40;

            // Fonts: smaller to fit one page
            Font titleFont = new Font("Arial", 11, FontStyle.Bold);
            Font headerFont = new Font("Arial", 10, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 9, FontStyle.Regular);

            StringFormat wrapFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.Word
            };

            // Header
            y = WaterMarkHelperA4.PrintHeader(g, (int)leftMargin, (int)y, e.PageBounds.Width);

            // Title
            g.DrawString("ENT CONSULTATION REPORT", titleFont, Brushes.Black,
                new RectangleF(leftMargin, y, contentWidth, 25),
                new StringFormat { Alignment = StringAlignment.Center });
            y += 35;

            // Patient Info
            g.DrawString("Patient Information", headerFont, Brushes.Black, leftMargin, y);
            y += 20;
            g.DrawString($"Patient Name      : {patientName}", bodyFont, Brushes.Black, leftMargin, y); y += 15;
            g.DrawString($"Address           : {patientAddress}", bodyFont, Brushes.Black, leftMargin, y); y += 15;
            g.DrawString($"Age               : {patientAge}", bodyFont, Brushes.Black, leftMargin, y); y += 15;
            g.DrawString($"Sex               : {patientSex}", bodyFont, Brushes.Black, leftMargin, y); y += 15;
            g.DrawString($"Civil Status      : {civilStatus}", bodyFont, Brushes.Black, leftMargin, y); y += 15;
            g.DrawString($"Contact Number    : {patientContact}", bodyFont, Brushes.Black, leftMargin, y); y += 15;
            if (!string.IsNullOrEmpty(emergencyName))
                g.DrawString($"Emergency Contact : {emergencyName} ({emergencyRelationship}): {emergencyContact}", bodyFont, Brushes.Black, leftMargin, y);
            y += 20;

            // Consultation Info
            g.DrawString("Consultation Details", headerFont, Brushes.Black, leftMargin, y); y += 20;
            g.DrawString($"Doctor           : {doctorName}", bodyFont, Brushes.Black, leftMargin, y); y += 15;
            g.DrawString($"Consultation Date: {consultationDate:MMMM dd, yyyy}", bodyFont, Brushes.Black, leftMargin, y); y += 15;
            g.DrawString($"Chief Complaint  : {chiefComplaint}", bodyFont, Brushes.Black, leftMargin, y); y += 15;
            g.DrawString($"History of Illness: {history}", bodyFont, Brushes.Black, new RectangleF(leftMargin, y, contentWidth, 50), wrapFormat);
            y += 55;

            // ENT & Neck Exams
            void DrawExam(string title, string value)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    g.DrawString(title, headerFont, Brushes.Black, leftMargin, y); y += 18;
                    g.DrawString(value, bodyFont, Brushes.Black, new RectangleF(leftMargin, y, contentWidth, 40), wrapFormat); y += 45;
                }
            }

            DrawExam("Ear Examination", earExam);
            DrawExam("Nose Examination", noseExam);
            DrawExam("Throat Examination", throatExam);
            DrawExam("Neck Examination", neckExam);

            // Diagnosis, Recommendations, Notes
            DrawExam("Diagnosis", diagnosis);
            DrawExam("Recommendations", recommendations);
            DrawExam("Notes", notes);

            // Follow-Up
            if (followUpDate.HasValue || !string.IsNullOrWhiteSpace(followUpNotes))
            {
                string followUpText = $"{followUpDate?.ToString("MMMM dd, yyyy") ?? ""}\n{followUpNotes ?? ""}";
                DrawExam("Follow-Up", followUpText);
            }

            // Footer
            WaterMarkHelperA4.PrintFooter(g, (int)leftMargin, e.MarginBounds.Bottom - 60, e.MarginBounds.Width);
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
