using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    public class AdmitOrdersPrintHelper
    {
        private readonly int _admitOrderId;
        private readonly PrintDocument _printDocument;

        // Patient info
        private string _patientName = "";
        private string _patientAddress = "";
        private string _patientAge = "";
        private string _patientGender = "";
        private DateTime _admitDate = DateTime.Now;

        // Admitting order fields
        private string _diagnosis = "";
        private string _chief_complaints = "";
        private string _vitalSigns = "";
        private string _diet = "";
        private string _activity = "";
        private string _medications = "";
        private string _ivFluids = "";
        private string _laboratory = "";
        private string _imaging = "";
        private string _nursingInstructions = "";
        private string _specialInstructions = "";

        public AdmitOrdersPrintHelper(int admitOrderId)
        {
            _admitOrderId = admitOrderId;
            LoadData();
            _printDocument = new PrintDocument();
            var a5 = new PaperSize("A5", 583, 827);
            _printDocument.DefaultPageSettings.PaperSize = a5;
            _printDocument.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        #region Load Data
        private void LoadData()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(@"
         SELECT
               ao.admitting_order_id, ao.patient_id, ao.diagnosis,
      ao.`chief_complaints`, ao.vital_signs, ao.diet, ao.activity,
  ao.medications, ao.iv_fluids, ao.laboratory,
      ao.imaging, ao.nursing_instructions, ao.special_instructions,
                ao.created_at, ao.updated_at,
 p.full_name, p.address, p.sex, p.birth_date
       FROM `admitting_orders` ao
                 LEFT JOIN `patients` p ON ao.patient_id = p.patient_id
       WHERE ao.admitting_order_id = @id
          LIMIT 1
       ", conn))
                {
                    cmd.Parameters.AddWithValue("@id", _admitOrderId);
                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            _patientName = SafeString(dr["full_name"]);
                            _patientAddress = SafeString(dr["address"]);
                            _patientGender = SafeString(dr["sex"]);
                            if (dr["birth_date"] != DBNull.Value)
                            {
                                try
                                {
                                    var bd = Convert.ToDateTime(dr["birth_date"]);
                                    _patientAge = CalculateAgeString(bd);
                                }
                                catch { _patientAge = ""; }
                            }
                            if (dr["created_at"] != DBNull.Value)
                                _admitDate = Convert.ToDateTime(dr["created_at"]);

                            _diagnosis = SafeString(dr["diagnosis"]);
                            _chief_complaints = SafeString(dr["chief_complaints"]);
                            _vitalSigns = SafeString(dr["vital_signs"]);
                            _diet = SafeString(dr["diet"]);
                            _activity = SafeString(dr["activity"]);
                            _medications = SafeString(dr["medications"]);
                            _ivFluids = SafeString(dr["iv_fluids"]);
                            _laboratory = SafeString(dr["laboratory"]);
                            _imaging = SafeString(dr["imaging"]);
                            _nursingInstructions = SafeString(dr["nursing_instructions"]);
                            _specialInstructions = SafeString(dr["special_instructions"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading admit order data:\n" + ex.Message,
                 "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string SafeString(object value) => value == null || value == DBNull.Value ? "" : value.ToString().Trim();

        private string CalculateAgeString(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age.ToString();
        }
        #endregion

        #region Printing
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle margins = new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height);
            int left = margins.Left;
            float y = margins.Top;

            try
            {
                y = WaterMarkHelper.PrintHeader(g, left, (int)y, e.PageBounds.Width);
            }
            catch { }

            y += 6; // spacing after header

            using (Font titleFont = new Font("Segoe UI", 14F, FontStyle.Bold))
            using (Font sectionTitleFont = new Font("Arial", 9F, FontStyle.Bold))
            using (Font labelFont = new Font("Arial", 8F, FontStyle.Bold))
            using (Font italicFont = new Font("Arial", 9F, FontStyle.Italic))
            using (Font valueFont = new Font("Arial", 8F, FontStyle.Underline))
            using (Font bodyFont = new Font("Arial", 8F, FontStyle.Regular))
            {
                float contentLeft = left;
                float contentWidth = margins.Width;

                // ===== TITLE =====
                g.DrawString("ADMITTING ORDERS", titleFont, Brushes.Black, contentLeft, y);
                y += titleFont.Height + 10;

                // ===== PATIENT INFO SECTION =====
                g.DrawString("Name:", labelFont, Brushes.Black, contentLeft, y);
                g.DrawString(_patientName, valueFont, Brushes.Black, contentLeft + 100, y);
                g.DrawString("Age:", labelFont, Brushes.Black, contentLeft + 400, y);
                g.DrawString(_patientAge, valueFont, Brushes.Black, contentLeft + 430, y);
                g.DrawString("Sex:", labelFont, Brushes.Black, contentLeft + 470, y);
                g.DrawString(_patientGender, valueFont, Brushes.Black, contentLeft + 500, y);
                y += 20;

                g.DrawString("Address:", labelFont, Brushes.Black, contentLeft, y);
                g.DrawString(_patientAddress, valueFont, Brushes.Black, contentLeft + 100, y);
                g.DrawString("Date:", labelFont, Brushes.Black, contentLeft + 400, y);
                g.DrawString(_admitDate.ToString("MM/dd/yyyy"), valueFont, Brushes.Black, contentLeft + 440, y);
                y += 20;

                g.DrawLine(Pens.Black, left, y, left + contentWidth, y);
                y += 10;

                // ===== ADMITTING ORDERS SECTIONS (full-width, wrapped text) =====
                void DrawSection(string title, string content)
                {
                    if (string.IsNullOrWhiteSpace(content)) return;

                    g.DrawString(title + ":", sectionTitleFont, Brushes.Black, contentLeft, y);
                    y += sectionTitleFont.Height + 2;

                    RectangleF drawRect = new RectangleF(contentLeft, y, contentWidth, 1000); // large height to allow wrapping
                    DrawWrappedString(g, content, bodyFont, drawRect);

                    y += g.MeasureString(content, bodyFont, (int)contentWidth).Height + 8; // add spacing after section
                }

                DrawSection("Chief Complaints", _chief_complaints);
                DrawSection("Impression", _diagnosis);
                g.DrawString("Please admit to room of choice under my service. TPR q shift and record", italicFont, Brushes.DarkGray, contentLeft, y);
                y += 15;
                DrawSection("Diet", _diet);
                DrawSection("Activity", _activity);
                DrawSection("Vital Signs", _vitalSigns);
                DrawSection("IV Fluids", _ivFluids);
                DrawSection("Medications", _medications);
                DrawSection("Laboratory", _laboratory);
                DrawSection("Imaging", _imaging);
                DrawSection("Nursing Instructions", _nursingInstructions);
                DrawSection("Special Orders", _specialInstructions);

                // ===== FOOTER =====
                float footerY = e.MarginBounds.Bottom - 25;
                try
                {
                    WaterMarkHelper.PrintFooter(g, left, (int)footerY, (int)contentWidth + 50);
                }
                catch { }
            }

            e.HasMorePages = false;
        }

        #endregion

        #region Drawing Helpers
        private void DrawWrappedString(Graphics g, string text, Font font, RectangleF rect)
        {
            if (string.IsNullOrEmpty(text)) return;

            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                Trimming = StringTrimming.Word,
                FormatFlags = StringFormatFlags.LineLimit
            };

            g.DrawString(text, font, Brushes.Black, rect, sf);
        }
        #endregion

        #region Public Methods
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

        public void Print()
        {
            try
            {
                using (var dlg = new PrintDialog())
                {
                    dlg.Document = _printDocument;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        _printDocument.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print failed: " + ex.Message, "Print", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
