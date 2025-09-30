using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
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

        // Prescription items
        private List<(string ItemName, string Description, int Quantity, string Note)> _items
            = new List<(string, string, int, string)>();

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
                var conn = DBConfig.GetConnection();
                conn.Open();

                string query = @"
                    SELECT p.full_name, p.address, p.age, p.sex, pr.created_at,
                           pr.item_id, i.item_name, i.description, pr.quantity, pr.note
                    FROM prescription pr
                    JOIN patients p ON pr.patient_id = p.patient_id
                    JOIN items i ON pr.item_id = i.item_id
                    WHERE pr.consultation_id = @consultationId
                    ORDER BY i.item_name";

                var cmd = new MySqlCommand(query, conn);
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

                    string itemName = reader["item_name"].ToString();
                    string description = reader["description"].ToString();
                    int quantity = Convert.ToInt32(reader["quantity"]);
                    string note = reader["note"]?.ToString() ?? "";

                    _items.Add((itemName, description, quantity, note));
                }

                if (_items.Count == 0)
                    throw new Exception("No prescription found for this consultation.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading prescription: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // PRINT PAGE
        // =========================
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int leftMargin = 50;
            int y = 20;

            // 1️⃣ Header (clinic info)
            y = WaterMarkHelper.PrintHeader(g, leftMargin, y, e.PageBounds.Width);

            // 2️⃣ Patient Info
            using (Font labelFont = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Font valueFont = new Font("Segoe UI", 9))
            {
                g.DrawString("Patient Name: ", labelFont, Brushes.Black, leftMargin, y);
                g.DrawString(_patientName, valueFont, Brushes.Black, leftMargin + 100, y);
                g.DrawString("Age: ", labelFont, Brushes.Black, leftMargin + 400, y);
                g.DrawString(_patientAge, valueFont, Brushes.Black, leftMargin + 440, y);
                y += 25;

                g.DrawString("Gender: ", labelFont, Brushes.Black, leftMargin, y);
                g.DrawString(_patientGender, valueFont, Brushes.Black, leftMargin + 60, y);
                g.DrawString("Date: ", labelFont, Brushes.Black, leftMargin + 400, y);
                g.DrawString(_prescriptionDate.ToShortDateString(), valueFont, Brushes.Black, leftMargin + 440, y);
                y += 40;
            }

            // 3️⃣ Table Header
            using (Font headerFont = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Font rowFont = new Font("Segoe UI", 9))
            {
                g.DrawString("Item Name", headerFont, Brushes.Black, leftMargin, y);
                g.DrawString("Qty", headerFont, Brushes.Black, leftMargin + 250, y);
                g.DrawString("Description", headerFont, Brushes.Black, leftMargin + 300, y);
                y += 20;
                g.DrawLine(Pens.Black, leftMargin, y, e.PageBounds.Width - leftMargin, y);
                y += 10;

                // 4️⃣ Prescription Items (with notes below each item)
                foreach (var item in _items)
                {
                    g.DrawString(item.ItemName, rowFont, Brushes.Black, leftMargin, y);
                    g.DrawString(item.Quantity.ToString(), rowFont, Brushes.Black, leftMargin + 250, y);
                    g.DrawString(item.Description, rowFont, Brushes.Black, leftMargin + 300, y);
                    y += 20;

                    // Note (if exists)
                    if (!string.IsNullOrEmpty(item.Note))
                    {
                        g.DrawString($"- Note: {item.Note}", rowFont, Brushes.Black, leftMargin + 20, y);
                        y += 20;
                    }
                }
            }

            y += 20;
            g.DrawLine(Pens.Black, leftMargin, y, e.PageBounds.Width - leftMargin, y);

            // 5️⃣ Footer (doctor info / signature)
            WaterMarkHelper.PrintFooter(g, leftMargin, e.PageBounds.Bottom - 80);
        }

        // =========================
        // SHOW PRINT PREVIEW
        // =========================
        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = _printDocument,
                Width = 1000,
                Height = 700
            };
            preview.ShowDialog();
        }
    }
}
