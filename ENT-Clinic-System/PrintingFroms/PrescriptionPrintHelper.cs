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
        private DateTime _prescriptionDate;

        // Prescription items (medicines + other items combined)
        private List<(string GenericName, string BrandName, string Strength, string Dosage, int Quantity, string Sig)> _items
            = new List<(string, string, string, string, int, string)>();

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

                    var cmd = new MySqlCommand(queryMedicines, conn);
                    cmd.Parameters.AddWithValue("@consultationId", _consultationId);
                    var reader = cmd.ExecuteReader();
                    bool firstRow = true;

                    while (reader.Read())
                    {
                        if (firstRow)
                        {
                            _patientName = reader["full_name"].ToString();
                            _patientAddress = reader["address"].ToString();
                            _patientAge = reader["age"].ToString();
                            _patientGender = reader["sex"].ToString();
                            _prescriptionDate = Convert.ToDateTime(reader["created_at"]);
                            firstRow = false;
                        }

                        _items.Add((
                            reader["generic_name"].ToString(),
                            reader["brand_name"].ToString(),
                            reader["strength"].ToString(),
                            reader["dosage"]?.ToString() ?? "",
                            Convert.ToInt32(reader["quantity"]),
                            reader["sig"]?.ToString() ?? ""
                        ));
                    }
                    reader.Close();

                    // 🔹 Load other items
                    string queryOthers = @"
                        SELECT o.item_name AS generic_name, o.item_name AS brand_name, '' AS strength, '' AS dosage,
                               po.quantity, po.sig
                        FROM prescription_other po
                        JOIN other_items o ON po.item_id = o.item_id
                        WHERE po.consultation_id = @consultationId
                        ORDER BY o.category, o.item_name";

                    var cmdOther = new MySqlCommand(queryOthers, conn);
                    cmdOther.Parameters.AddWithValue("@consultationId", _consultationId);
                    var reader2 = cmdOther.ExecuteReader();

                    while (reader2.Read())
                    {
                        _items.Add((
                            reader2["generic_name"].ToString(),
                            reader2["brand_name"].ToString(),
                            reader2["strength"].ToString(),
                            reader2["dosage"].ToString(),
                            Convert.ToInt32(reader2["quantity"]),
                            reader2["sig"]?.ToString() ?? ""
                        ));
                    }
                    reader2.Close();

                    if (_items.Count == 0)
                        throw new Exception("No prescription found for this consultation.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading prescription data: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // PRINT PAGE
        // =========================
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int leftMargin =10;
            int y = 10;

            // 1️⃣ Header (Watermark + clinic info)
            y = WaterMarkHelper.PrintHeader(g, leftMargin, y, e.PageBounds.Width);

            // 2️⃣ Patient Info (2 columns)
            using (Font labelFont = new Font("Arial", 8, FontStyle.Bold))
            using (Font valueFont = new Font("Arial", 8))
            {
                int underlineOffset = 2; // distance below text for underline

                // Patient Name
                g.DrawString("Name:", labelFont, Brushes.Black, leftMargin, y);
                g.DrawString(_patientName, valueFont, Brushes.Black, leftMargin + 100, y);
                SizeF nameSize = g.MeasureString(_patientName, valueFont);
                g.DrawLine(Pens.Black, leftMargin + 100, y + nameSize.Height + underlineOffset,
                           leftMargin + 100 + nameSize.Width, y + nameSize.Height + underlineOffset);

                // Age
                g.DrawString("Age:", labelFont, Brushes.Black, leftMargin + 350, y);
                g.DrawString(_patientAge, valueFont, Brushes.Black, leftMargin + 380, y);
                SizeF ageSize = g.MeasureString(_patientAge, valueFont);
                g.DrawLine(Pens.Black, leftMargin + 380, y + ageSize.Height + underlineOffset,
                           leftMargin + 380 + ageSize.Width, y + ageSize.Height + underlineOffset);

                // Sex
                g.DrawString("Sex:", labelFont, Brushes.Black, leftMargin + 420, y);
                g.DrawString(_patientGender, valueFont, Brushes.Black, leftMargin + 450, y);
                SizeF sexSize = g.MeasureString(_patientGender, valueFont);
                g.DrawLine(Pens.Black, leftMargin + 450, y + sexSize.Height + underlineOffset,
                           leftMargin + 450 + sexSize.Width, y + sexSize.Height + underlineOffset);

                y += 20;

                // Address
                g.DrawString("Address:", labelFont, Brushes.Black, leftMargin, y);
                g.DrawString(_patientAddress, valueFont, Brushes.Black, leftMargin + 100, y);
                SizeF addressSize = g.MeasureString(_patientAddress, valueFont);
                g.DrawLine(Pens.Black, leftMargin + 100, y + addressSize.Height + underlineOffset,
                           leftMargin + 100 + addressSize.Width, y + addressSize.Height + underlineOffset);

                // Date
                g.DrawString("Date:", labelFont, Brushes.Black, leftMargin + 345, y);
                string formattedDate = _prescriptionDate.ToString("MMMM dd, yyyy"); // e.g., October 17, 2025
                g.DrawString(formattedDate, valueFont, Brushes.Black, leftMargin + 380, y);

                // Draw underline
                SizeF dateSize = g.MeasureString(formattedDate, valueFont);
                g.DrawLine(Pens.Black, leftMargin + 380, y + dateSize.Height + underlineOffset,
                           leftMargin + 380 + dateSize.Width, y + dateSize.Height + underlineOffset);


                y += 10;
            }


            // 3️⃣ Prescription Items
            using (Font rxFont = new Font("Times New Roman", 50, FontStyle.Bold))
            using (Font itemFont = new Font("Arial",8))
            using (Font sigFont = new Font("Arial",8, FontStyle.Italic))
            {
                bool rxPrinted = false;
                foreach (var item in _items)
                {
                    // Print ℞ only once for first item
                    if (!rxPrinted)
                    {
                        g.DrawString("\u211E", rxFont, Brushes.Black, leftMargin - 15, y);
                        rxPrinted = true;
                        y +=50;
                    }

                    int xOffset = leftMargin + 90;
                    // Define consistent vertical and horizontal spacing
                    int lineSpacing = 20;
                    int indentBrand = 10;  // indentation for brand name
                    int indentQty = 180;   // horizontal position for quantity
                    int indentDosage = 240; // horizontal position for dosage
                    int indentSig = 10;    // deeper indent for Sig note

                    // 1️⃣ Generic Name + Strength
                    g.DrawString(item.GenericName, itemFont, Brushes.Black, xOffset, y);
                    g.DrawString(item.Strength, itemFont, Brushes.Black, xOffset + 180, y); // adjust 120 as needed
                    y += lineSpacing;

                    // 2️⃣ Brand Name, Quantity, Dosage — all individually placed
                    g.DrawString($"({item.BrandName})", itemFont, Brushes.Black, xOffset + indentBrand, y);
                    g.DrawString($"#{item.Quantity}", itemFont, Brushes.Black, xOffset + indentQty, y);
                    g.DrawString(item.Dosage, itemFont, Brushes.Black, xOffset + indentDosage, y);
                    y += lineSpacing - 2;

                    // 3️⃣ Sig (optional)
                    if (!string.IsNullOrEmpty(item.Sig))
                    {
                        g.DrawString($"- Sig: {item.Sig}", sigFont, Brushes.Black, xOffset + indentSig, y);
                        y += lineSpacing;
                        // 🔹 Add a subtle 50% opacity divider line under the prescription entry
                        using (Pen lightPen = new Pen(Color.FromArgb(128, 0, 0, 0), 1)) // 128 = 50% opacity
                        {
                            int lineStartX = leftMargin + 30;
                            int lineEndX = e.PageBounds.Width - leftMargin - 30;
                            g.DrawLine(lightPen, lineStartX, y, lineEndX, y);
                        }
                    }

                    // 4️⃣ Extra spacing between prescription items
                    y += 8;

                }
            }

            // 4️⃣ Footer
            WaterMarkHelper.PrintFooter(g, leftMargin, e.MarginBounds.Bottom , e.MarginBounds.Width + 150);
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
                        if (item is ToolStripButton btn && btn.ToolTipText.ToLower().Contains("print"))
                            btn.Visible = false;

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
