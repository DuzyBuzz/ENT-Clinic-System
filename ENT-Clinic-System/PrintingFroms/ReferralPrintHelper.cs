using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    public class ReferralPrintHelper
    {
        private readonly int _referralId;
        private readonly PrintDocument _printDocument;

        // Patient info
        private string _patientName = "";
        private string _patientAddress = "";
        private string _patientAge = "";
        private string _patientGender = "";
        private DateTime _referralDate = DateTime.Now;

        // Referral fields
        private string _referringDoctor = "";
        private string _referralType = "";
        private string _workingImpression = "";
        private string _plan = "";
        private string _additionalInfo = "";

        public ReferralPrintHelper(int referralId)
        {
            _referralId = referralId;
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
                    SELECT r.referral_id, r.patient_id, r.referring_doctor, r.referral_type, 
                           r.present_working_impression, r.plan, r.additional_info, r.created_at,
                           p.full_name, p.address, p.sex, p.birth_date
                    FROM referrals r
                    LEFT JOIN patients p ON r.patient_id = p.patient_id
                    WHERE r.referral_id = @id
                    LIMIT 1
                ", conn))
                {
                    cmd.Parameters.AddWithValue("@id", _referralId);
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
                                _referralDate = Convert.ToDateTime(dr["created_at"]);

                            _referringDoctor = SafeString(dr["referring_doctor"]);
                            _referralType = SafeString(dr["referral_type"]);
                            _workingImpression = SafeString(dr["present_working_impression"]);
                            _plan = SafeString(dr["plan"]);
                            _additionalInfo = SafeString(dr["additional_info"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading referral data:\n" + ex.Message,
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

            y += 6;

            using (Font titleFont = new Font("Segoe UI", 14F, FontStyle.Bold))
            using (Font labelFont = new Font("Arial", 9F, FontStyle.Bold))
            using (Font valueFont = new Font("Arial", 9F, FontStyle.Underline))
            using (Font bodyFont = new Font("Arial", 8F, FontStyle.Regular))
            using (Font checkboxFont = new Font("Arial", 8F, FontStyle.Regular))
            {
                float contentLeft = left;
                float contentWidth = margins.Width;

                // ===== TITLE =====


                // ===== PATIENT INFO SECTION (with underlined values) =====
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
                g.DrawString(DateTime.Now.ToString("MM/dd/yyyy"), valueFont, Brushes.Black, contentLeft + 440, y);

                y += 20;
                g.DrawString("Referral Form", titleFont, Brushes.Black, contentLeft, y);
                y += titleFont.Height + 4;
                g.DrawLine(Pens.Black, left, y, left + contentWidth, y);
                y += 8;

                // ===== TO: DOCTOR =====
                g.DrawString("To: " + _referringDoctor, labelFont, Brushes.Black, contentLeft, y);
                y += bodyFont.Height +15;

                // ===== INTRO =====
                g.DrawString("Referring the above named patient to you for:", bodyFont, Brushes.Gray, contentLeft, y);
                y += bodyFont.Height + 15;

                // ===== REFERRAL TYPE CHECKBOXES (2 columns, multiple rows) =====
                if (!string.IsNullOrWhiteSpace(_referralType))
                {
                    string[] allOptions = { "Evaluation & Management", "Pre-Op Risk Assessment", "Co-Management", "Emergency" };
                    string[] selectedItems = _referralType.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                          .Select(i => i.Trim())
                                                          .ToArray();

                    int colCount = 2;
                    float colWidth = (contentWidth - 10) / colCount;
                    float startX = contentLeft;
                    float startY = y;
                    float checkboxSize = 10;

                    for (int i = 0; i < allOptions.Length; i++)
                    {
                        float x = startX + (i % colCount) * colWidth;

                        // Draw square box
                        g.DrawRectangle(Pens.Black, x, startY, checkboxSize, checkboxSize);

                        // Check the box if it's in selectedItems
                        if (selectedItems.Contains(allOptions[i]))
                        {
                            float padding = 2; // padding inside the box
                            g.FillRectangle(Brushes.Black, x + padding, startY + padding, checkboxSize - 2 * padding, checkboxSize - 2 * padding);
                        }

                        // Draw text next to box
                        g.DrawString(allOptions[i], checkboxFont, Brushes.Black, x + checkboxSize + 4, startY - 1);

                        // Move to next row if end of column
                        if ((i + 1) % colCount == 0) startY += checkboxFont.Height + 4;
                    }

                    y = startY + (allOptions.Length % colCount != 0 ? checkboxFont.Height + 4 : 0);
                }

                y += +15;


                // ===== PRESENT WORKING IMPRESSION =====
                if (!string.IsNullOrWhiteSpace(_workingImpression))
                {
                    g.DrawString("Present working impression is:", labelFont, Brushes.Black, contentLeft, y);
                    y += labelFont.Height + 2;

                    var sf = new StringFormat { FormatFlags = StringFormatFlags.LineLimit };
                    var measured = g.MeasureString(_workingImpression, bodyFont, (int)contentWidth - 5, sf);
                    g.DrawString(_workingImpression, bodyFont, Brushes.Black, new RectangleF(contentLeft, y, contentWidth, measured.Height), sf);
                    y += measured.Height+15;

                }

                // ===== PLAN (plain wrapped text) =====
                if (!string.IsNullOrWhiteSpace(_plan))
                {
                    g.DrawString("Plan:", labelFont, Brushes.Black, contentLeft, y);
                    y += labelFont.Height + 2;

                    var sf = new StringFormat { FormatFlags = StringFormatFlags.LineLimit };
                    var measured = g.MeasureString(_plan, bodyFont, (int)contentWidth - 5, sf);
                    g.DrawString(_plan, bodyFont, Brushes.Black, new RectangleF(contentLeft, y, contentWidth, measured.Height), sf);
                    y += measured.Height + +15;

                }

                // ===== ADDITIONAL INFORMATION =====
                if (!string.IsNullOrWhiteSpace(_additionalInfo))
                {
                    g.DrawString("Additional Information:", labelFont, Brushes.Black, contentLeft, y);
                    y += labelFont.Height + 2;

                    var sf = new StringFormat { FormatFlags = StringFormatFlags.LineLimit };
                    var measured = g.MeasureString(_additionalInfo, bodyFont, (int)contentWidth - 5, sf);
                    g.DrawString(_additionalInfo, bodyFont, Brushes.Black, new RectangleF(contentLeft, y, contentWidth, measured.Height), sf);
                    y += measured.Height + 6;
                }

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
            var sf = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                FormatFlags = 0,
                Trimming = StringTrimming.Word
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
                        _printDocument.Print();
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
