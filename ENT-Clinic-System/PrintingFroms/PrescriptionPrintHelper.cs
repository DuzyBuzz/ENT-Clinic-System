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

        // Prescription items (combined list)
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

                    // 🔹 Load medicines
                    string queryMedicines = @"
                        SELECT p.full_name, p.address, p.age, p.sex, pr.created_at,
                               i.generic_name, i.brand_name, i.strength, i.dosage, pr.quantity, pr.sig
                        FROM prescription pr
                        JOIN patients p ON pr.patient_id = p.patient_id
                        JOIN items i ON pr.item_id = i.item_id
                        WHERE pr.consultation_id = @consultationId
                        ORDER BY i.generic_name";

                    using (var cmd = new MySqlCommand(queryMedicines, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultationId", _consultationId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            bool firstRow = true;

                            while (reader.Read())
                            {
                                if (firstRow)
                                {
                                    _patientName = SafeString(reader["full_name"]);
                                    _patientAddress = SafeString(reader["address"]);
                                    _patientAge = SafeString(reader["age"]);
                                    _patientGender = SafeString(reader["sex"]);
                                    DateTime.TryParse(SafeString(reader["created_at"]), out _prescriptionDate);
                                    firstRow = false;
                                }

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

                    // 🔹 Load other items (use generic_name instead of item_name)
                    string queryOthers = @"
                        SELECT o.generic_name, o.brand_name, o.strength, o.dosage,
                               po.quantity, po.sig
                        FROM prescription_other po
                        JOIN other_items o ON po.item_id = o.item_id
                        WHERE po.consultation_id = @consultationId
                        ORDER BY o.generic_name";

                    using (var cmdOther = new MySqlCommand(queryOthers, conn))
                    {
                        cmdOther.Parameters.AddWithValue("@consultationId", _consultationId);
                        using (var reader2 = cmdOther.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                _items.Add((
                                    SafeString(reader2["generic_name"]),
                                    SafeString(reader2["brand_name"]),
                                    SafeString(reader2["strength"]),
                                    SafeString(reader2["dosage"]),
                                    SafeInt(reader2["quantity"]),
                                    SafeString(reader2["sig"])
                                ));
                            }
                        }
                    }

                    if (_items.Count == 0)
                    {
                        throw new Exception("No prescription items found for this consultation.");
                    }
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
        {
            return value == null || value == DBNull.Value ? string.Empty : value.ToString().Trim();
        }

        private int SafeInt(object value)
        {
            try
            {
                return value == null || value == DBNull.Value ? 0 : Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        // =========================
        // PRINT PAGE
        // =========================
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int leftMargin = 10;
            int y = 10;

            // 1️⃣ Header (Watermark + clinic info)
            y = WaterMarkHelper.PrintHeader(g, leftMargin, y, e.PageBounds.Width);

            // 2️⃣ Patient Info
            using (Font labelFont = new Font("Arial", 8, FontStyle.Bold))
            using (Font valueFont = new Font("Arial", 8))
            {
                int underlineOffset = 2;

                // --- Name ---
                g.DrawString("Name:", labelFont, Brushes.Black, leftMargin, y);
                g.DrawString(_patientName, valueFont, Brushes.Black, leftMargin + 100, y);

                SizeF nameSize = g.MeasureString(_patientName, valueFont);
                g.DrawLine(Pens.Black,
                    leftMargin + 100,
                    y + nameSize.Height + underlineOffset,
                    leftMargin + 100 + nameSize.Width,
                    y + nameSize.Height + underlineOffset);

                // --- Age ---
                g.DrawString("Age:", labelFont, Brushes.Black, leftMargin + 350, y);
                g.DrawString(_patientAge, valueFont, Brushes.Black, leftMargin + 380, y);

                SizeF ageSize = g.MeasureString(_patientAge, valueFont);
                g.DrawLine(Pens.Black,
                    leftMargin + 380,
                    y + ageSize.Height + underlineOffset,
                    leftMargin + 380 + ageSize.Width,
                    y + ageSize.Height + underlineOffset);

                // --- Sex ---
                g.DrawString("Sex:", labelFont, Brushes.Black, leftMargin + 420, y);
                g.DrawString(_patientGender, valueFont, Brushes.Black, leftMargin + 450, y);

                SizeF sexSize = g.MeasureString(_patientGender, valueFont);
                g.DrawLine(Pens.Black,
                    leftMargin + 450,
                    y + sexSize.Height + underlineOffset,
                    leftMargin + 450 + sexSize.Width,
                    y + sexSize.Height + underlineOffset);

                y += 20;

                // --- Address ---
                g.DrawString("Address:", labelFont, Brushes.Black, leftMargin, y);
                g.DrawString(_patientAddress, valueFont, Brushes.Black, leftMargin + 100, y);

                SizeF addressSize = g.MeasureString(_patientAddress, valueFont);
                g.DrawLine(Pens.Black,
                    leftMargin + 100,
                    y + addressSize.Height + underlineOffset,
                    leftMargin + 100 + addressSize.Width,
                    y + addressSize.Height + underlineOffset);

                // --- Date ---
                g.DrawString("Date:", labelFont, Brushes.Black, leftMargin + 345, y);
                string formattedDate = _prescriptionDate.ToString("MMMM dd, yyyy");
                g.DrawString(formattedDate, valueFont, Brushes.Black, leftMargin + 380, y);

                SizeF dateSize = g.MeasureString(formattedDate, valueFont);
                g.DrawLine(Pens.Black,
                    leftMargin + 380,
                    y + dateSize.Height + underlineOffset,
                    leftMargin + 380 + dateSize.Width,
                    y + dateSize.Height + underlineOffset);

                y += 15;
            }


            // 3️⃣ Prescription Items
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

                    g.DrawString(item.GenericName, itemFont, Brushes.Black, xOffset, y);
                    g.DrawString(item.Strength, itemFont, Brushes.Black, xOffset + 180, y);
                    y += lineSpacing;

                    g.DrawString("(" + item.BrandName + ")", itemFont, Brushes.Black, xOffset + 10, y);
                    g.DrawString("#" + item.Quantity, itemFont, Brushes.Black, xOffset + 180, y);
                    g.DrawString(item.Dosage, itemFont, Brushes.Black, xOffset + 240, y);
                    y += lineSpacing - 2;

                    if (!string.IsNullOrEmpty(item.Sig))
                    {
                        g.DrawString("Sig: " + item.Sig, sigFont, Brushes.Black, xOffset + 10, y);
                        y += lineSpacing;

                        using (Pen lightPen = new Pen(Color.FromArgb(128, 0, 0, 0), 1))
                        {
                            g.DrawLine(lightPen, leftMargin + 30, y, e.PageBounds.Width - leftMargin - 30, y);
                        }
                    }

                    y += 8;
                }
            }

            // 4️⃣ Footer
            WaterMarkHelper.PrintFooter(g, leftMargin, e.MarginBounds.Bottom, e.MarginBounds.Width + 150);
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
