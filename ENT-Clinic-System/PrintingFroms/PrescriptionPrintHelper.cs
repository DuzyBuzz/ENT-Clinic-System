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
    public class PrescriptionPrintHelper
    {
        private int _consultationId;
        private PrintDocument _printDocument;

        // Patient info
        private string _patientName = "";
        private string _patientAddress = "";
        private string _patientAge = "";
        private string _patientGender = "";
        private DateTime _prescriptionDate = DateTime.Now;

        // 🔹 NEW: Follow-up date field
        private DateTime? _followUpDate = null;

        // Prescription items
        private readonly List<(string GenericName, string BrandName, string Strength, string Dosage, int Quantity, string Sig)>
            _items = new List<(string, string, string, string, int, string)>();

        public PrescriptionPrintHelper(int consultationId)
        {
            _consultationId = consultationId;
            LoadData();

            _printDocument = new PrintDocument();
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        // =========================
        // LOAD DATA FROM DATABASE
        // =========================
        private void LoadData()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // ---------------------------------------------
                    // 1️⃣ Get patient_id and created_at from consultation
                    // ---------------------------------------------
                    int patientId = 0;
                    string queryConsultation = @"
                SELECT patient_id, consultation_date, follow_up_date
                FROM consultation
                WHERE consultation_id = @consultationId
                LIMIT 1";

                    using (var cmd = new MySqlCommand(queryConsultation, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultationId", _consultationId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                patientId = SafeInt(reader["patient_id"]);
                                DateTime.TryParse(SafeString(reader["consultation_date"]), out _prescriptionDate);

                                // 🔹 Optional follow-up date
                                if (reader["follow_up_date"] != DBNull.Value)
                                    _followUpDate = Convert.ToDateTime(reader["follow_up_date"]);
                            }
                        }
                    }

                    if (patientId == 0)
                        throw new Exception("No patient linked to this consultation.");

                    // ---------------------------------------------
                    // 2️⃣ Load patient info using patient_id
                    // ---------------------------------------------
                    string queryPatient = @"
                SELECT full_name, address, sex, TIMESTAMPDIFF(YEAR, birth_date, CURDATE()) AS age
                FROM patients
                WHERE patient_id = @patientId
                LIMIT 1";

                    using (var cmd = new MySqlCommand(queryPatient, conn))
                    {
                        cmd.Parameters.AddWithValue("@patientId", patientId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _patientName = SafeString(reader["full_name"]);
                                _patientAddress = SafeString(reader["address"]);
                                _patientGender = SafeString(reader["sex"]);
                                _patientAge = SafeString(reader["age"]);
                            }
                        }
                    }

                    // ---------------------------------------------
                    // 3️⃣ Load medicines from `prescription`
                    // ---------------------------------------------
                    string queryMedicines = @"
                SELECT 
                    i.generic_name,
                    i.brand_name,
                    i.strength,
                    i.dosage,
                    pr.quantity,
                    pr.sig
                FROM prescription pr
                JOIN items i ON pr.item_id = i.item_id
                WHERE pr.consultation_id = @consultationId
                ORDER BY i.generic_name;";

                    using (var cmd = new MySqlCommand(queryMedicines, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultationId", _consultationId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _items.Add((
                                    SafeString(reader["generic_name"]),
                                    SafeString(reader["brand_name"]),
                                    SafeString(reader["strength"]),
                                    SafeString(reader["dosage"]),
                                    SafeInt(reader["quantity"]),
                                    SafeString(reader["sig"])
                                ));
                            }
                        }
                    }

                    // ---------------------------------------------
                    // 4️⃣ Load other medicines from `prescription_other`
                    // ---------------------------------------------
                    string queryOthers = @"
                SELECT 
                    o.generic_name,
                    o.brand_name,
                    o.strength,
                    o.dosage,
                    po.quantity,
                    po.sig
                FROM prescription_other po
                JOIN other_items o ON po.item_id = o.item_id
                WHERE po.consultation_id = @consultationId
                ORDER BY o.generic_name;";

                    using (var cmd = new MySqlCommand(queryOthers, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultationId", _consultationId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _items.Add((
                                    SafeString(reader["generic_name"]),
                                    SafeString(reader["brand_name"]),
                                    SafeString(reader["strength"]),
                                    SafeString(reader["dosage"]),
                                    SafeInt(reader["quantity"]),
                                    SafeString(reader["sig"])
                                ));
                            }
                        }
                    }

                    // ---------------------------------------------
                    // 5️⃣ Check if there are any prescription items
                    // ---------------------------------------------
                    if (_items.Count == 0)
                        throw new Exception("No prescription items found for this consultation.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading prescription data:\n" + ex.Message,
                    "Prescription Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ✅ Safe conversion helpers
        private string SafeString(object value)
            => value == null || value == DBNull.Value ? string.Empty : value.ToString().Trim();

        private int SafeInt(object value)
        {
            try { return value == null || value == DBNull.Value ? 0 : Convert.ToInt32(value); }
            catch { return 0; }
        }

        // =========================
        // PRINT PAGE
        // =========================
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int leftMargin = 10;
            int y = 10;

            // Header
            y = WaterMarkHelper.PrintHeader(g, leftMargin, y, e.PageBounds.Width);

            // Patient Info
            // 🧾 Patient Info Section (with underlined values)
            using (Font labelFont = new Font("Arial", 8, FontStyle.Bold))
            using (Font valueFont = new Font("Arial", 8, FontStyle.Underline))
            {
                // --- Name ---
                g.DrawString("Name:", labelFont, Brushes.Black, leftMargin, y);
                g.DrawString(_patientName, valueFont, Brushes.Black, leftMargin + 100, y);

                // --- Age ---
                g.DrawString("Age:", labelFont, Brushes.Black, leftMargin + 400, y);
                g.DrawString(_patientAge, valueFont, Brushes.Black, leftMargin + 430, y);

                // --- Sex ---
                g.DrawString("Sex:", labelFont, Brushes.Black, leftMargin + 470, y);
                g.DrawString(_patientGender, valueFont, Brushes.Black, leftMargin + 500, y);

                y += 20;

                // --- Address ---
                g.DrawString("Address:", labelFont, Brushes.Black, leftMargin, y);
                g.DrawString(_patientAddress, valueFont, Brushes.Black, leftMargin + 100, y);

                // --- Date ---
                g.DrawString("Date:", labelFont, Brushes.Black, leftMargin + 400, y);
                string formattedDate = _prescriptionDate.ToString("MMMM dd, yyyy");
                g.DrawString(formattedDate, valueFont, Brushes.Black, leftMargin + 435, y);

                y += 15;
            }


            // Prescription items
            using (Font rxFont = new Font("Times New Roman", 50, FontStyle.Bold))
            using (Font itemFont = new Font("Arial", 8))
            using (Font sigFont = new Font("Arial", 8, FontStyle.Italic))
            {
                bool rxPrinted = false;

                foreach (var item in _items)
                {
                    if (!rxPrinted)
                    {
                        g.DrawString("\u211E", rxFont, Brushes.Black, leftMargin - 15, y);
                        rxPrinted = true;
                        y += 50;
                    }

                    int xOffset = leftMargin + 90;
                    int lineSpacing = 15;

                    // --- Medicine name and details ---
                    g.DrawString($"{item.GenericName} - {item.Strength}", itemFont, Brushes.Black, xOffset, y);
                    y += lineSpacing;

                    g.DrawString("(" + item.BrandName + ")", itemFont, Brushes.Black, xOffset + 10, y);
                    g.DrawString("#" + item.Quantity, itemFont, Brushes.Black, xOffset + 180, y);
                    g.DrawString(item.Dosage, itemFont, Brushes.Black, xOffset + 240, y);
                    y += lineSpacing - 2;

                    // --- Sig: multi-line ---
                    if (!string.IsNullOrEmpty(item.Sig))
                    {
                        // Define the area where the Sig will be printed
                        float sigX = xOffset + 10;
                        float sigWidth = e.PageBounds.Width - sigX - leftMargin - 30; // dynamic width based on page
                        float sigHeight = 100; // max height allowed before overflow

                        RectangleF sigRect = new RectangleF(sigX, y, sigWidth, sigHeight);

                        // StringFormat for wrapping text
                        StringFormat format = new StringFormat
                        {
                            Alignment = StringAlignment.Near,
                            LineAlignment = StringAlignment.Near,
                            Trimming = StringTrimming.Word,
                            FormatFlags = StringFormatFlags.LineLimit
                        };

                        // Draw Sig text (auto-wraps within the rectangle)
                        g.DrawString("Sig: " + item.Sig.ToLower(), sigFont, Brushes.Black, sigRect, format);

                        // Measure how much space it used
                        SizeF sigSize = g.MeasureString("Sig: " + item.Sig, sigFont, (int)sigWidth, format);

                        // Advance Y position correctly
                        y += (int)sigSize.Height + 5;

                        // Optional: draw separator line after Sig
                        using (Pen lightPen = new Pen(Color.FromArgb(60, 100, 100, 100), 0.8f))
                        {
                            g.DrawLine(lightPen, leftMargin + 30, y, e.PageBounds.Width - leftMargin - 30, y);
                        }
                    }

                    y += 8; // small gap before next item
                }
            }


            // Footer
            WaterMarkHelper.PrintFooter(g, leftMargin, e.MarginBounds.Bottom, e.MarginBounds.Width + 150);
            // 🔹 Always show the "Follow-up visit on:" label
            using (Font labelFont = new Font("Arial", 9, FontStyle.Bold))
            {
                //string labelText = "Follow-up visit on:";
                float footerY = e.PageBounds.Bottom - 70; // position near footer

                // Draw label text
                //g.DrawString(labelText, labelFont, Brushes.Black, leftMargin + 30, footerY);

                // 🔹 If follow-up date exists, draw it below the label and underline it
                if (_followUpDate.HasValue)
                {
                    using (Font dateFont = new Font("Arial", 9, FontStyle.Underline))
                    {
                        string dateText = _followUpDate.Value.ToString("MMMM dd, yyyy");
                        float dateY = footerY + 18; // put date below the label
                        g.DrawString(dateText, dateFont, Brushes.Black, leftMargin + 30, dateY);
                    }
                }
            }


        }

        // =========================
        // SHOW PRINT PREVIEW
        // =========================
        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = _printDocument,
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
                        using (PrintDialog dlg = new PrintDialog { Document = _printDocument })
                        {
                            if (dlg.ShowDialog() == DialogResult.OK)
                                _printDocument.Print();
                        }
                    };
                    tool.Items.Insert(0, customPrint);
                }
            };

            preview.ShowDialog();
        }
    }
}
