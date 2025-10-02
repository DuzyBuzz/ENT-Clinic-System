using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WMPLib;

namespace ENT_Clinic_System.PrintingForms
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
            Graphics g = e.Graphics;

            // ===========================
            // PAGE SETUP
            // ===========================
            float leftMargin = 50;
            float rightMargin = 50;
            float contentWidth = e.PageBounds.Width - leftMargin - rightMargin;
            float y = 50; // start margin
            float pageBottom = e.MarginBounds.Bottom;

            // ===========================
            // FONTS & STYLES
            // ===========================
            Font titleFont = new Font("Arial", 14, FontStyle.Bold);
            Font headerFont = new Font("Arial", 11, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 11, FontStyle.Regular);

            // Centered text format
            StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center };

            // Wrap text format
            StringFormat wrapFormat = new StringFormat()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.Word
            };

            // ===========================
            // HEADER (same style as certificate)
            // ===========================
            y = WaterMarkHelper.PrintHeader(g, (int)leftMargin, (int)y, e.PageBounds.Width);

            // Title
            g.DrawString("CONSULTATION HISTORY", titleFont, Brushes.Black,
                new RectangleF(leftMargin, y, contentWidth, 30), centerFormat);
            y += 50;

            // ===========================
            // BODY CONTENT
            // ===========================
            bool patientInfoDone = false; // Add line after patient info

            while (currentSectionIndex < printSections.Count)
            {
                var (label, value) = printSections[currentSectionIndex];

                // Draw a separator line after patient info (after Emergency line, index 5)
                if (!patientInfoDone && currentSectionIndex >= 5)
                {
                    g.DrawLine(Pens.Black, leftMargin, y, leftMargin + contentWidth, y);
                    y += 10;
                    patientInfoDone = true;
                }

                // Measure sizes
                SizeF labelSize = g.MeasureString(label + ":", headerFont);
                SizeF valueSize = g.MeasureString(value, bodyFont, new SizeF(contentWidth - 150, float.MaxValue), wrapFormat);

                // Page-break check (reserve space for footer)
                if (y + Math.Max(labelSize.Height, valueSize.Height) > pageBottom - 80)
                {
                    e.HasMorePages = true;
                    return;
                }

                // Draw label
                g.DrawString(label + ":", headerFont, Brushes.Black, leftMargin, y);

                // Draw value (with wrapping support)
                g.DrawString(value, bodyFont, Brushes.Black,
                    new RectangleF(leftMargin + 150, y, contentWidth - 150, valueSize.Height), wrapFormat);

                y += Math.Max(labelSize.Height, valueSize.Height) + 5;
                currentSectionIndex++;
            }

            // ===========================
            // FOOTER (same style as certificate)
            // ===========================
            WaterMarkHelper.PrintFooter(g, (int)leftMargin, (int)(e.PageBounds.Bottom - 80));

            // Reset for next print
            e.HasMorePages = false;
            currentSectionIndex = 5;
        }
        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1000,
                Height = 700
            };

            preview.Shown += (s, e) =>
            {
                foreach (Control ctrl in preview.Controls)
                {
                    if (ctrl is ToolStrip toolStrip)
                    {
                        foreach (ToolStripItem item in toolStrip.Items)
                        {
                            if (item is ToolStripButton btn && btn.ToolTipText.ToLower().Contains("print"))
                            {
                                btn.Visible = false; // hide default Print button
                            }
                        }
                    }
                }

                // Add a custom Print button to the ToolStrip
                ToolStrip tool = preview.Controls.OfType<ToolStrip>().FirstOrDefault();
                if (tool != null)
                {
                    ToolStripButton customPrint = new ToolStripButton("Print");
                    customPrint.Click += (sender, args) =>
                    {
                        using (PrintDialog printDialog = new PrintDialog())
                        {
                            printDialog.Document = printDocument;
                            printDialog.AllowSomePages = true;
                            printDialog.AllowSelection = true;

                            if (printDialog.ShowDialog() == DialogResult.OK)
                            {
                                printDocument.Print();
                            }
                        }
                    };
                    // Insert at the left-most position
                    tool.Items.Insert(0, customPrint);
                }
            };

            preview.ShowDialog();
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
