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
        // Class-level fields - add these to your class
        private int _currentSectionIndex = 0;
        private int _currentCharIndex = 0; // index into current section's content
        private List<(string title, string content, bool isItalic)> _sections = null;
        private bool _headerPrintedOnThisJob = false;

        // Main PrintPage handler (replace your existing method)
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle margins = new Rectangle(
                e.MarginBounds.Left,
                e.MarginBounds.Top,
                e.MarginBounds.Width,
                e.MarginBounds.Height);

            int left = margins.Left;
            float y = margins.Top;
            float contentWidth = margins.Width;
            float footerReserve = 40f; // reserve space so footer has room; adjust if your footer is taller

            // Initialize sections once per print job
            if (_sections == null)
            {
                _sections = new List<(string title, string content, bool isItalic)>
        {
            ("Chief Complaints", _chief_complaints ?? string.Empty, false),
            ("Impression", _diagnosis ?? string.Empty, false),
            // special note as its own section (italic)
            ("Notes", "Please admit to room of choice under my service.\nTPR q shift and record", true),
            // flatten two-column items into sequential sections for robust pagination
            ("Diet", _diet ?? string.Empty, false),
            ("IV Fluids", _ivFluids ?? string.Empty, false),
            ("Activity", _activity ?? string.Empty, false),
            ("Medications", _medications ?? string.Empty, false),
            ("Laboratory", _laboratory ?? string.Empty, false),
            ("Imaging", _imaging ?? string.Empty, false),
            ("Nursing Instructions", _nursingInstructions ?? string.Empty, false),
            ("Special Orders", _specialInstructions ?? string.Empty, false)
        };

                // start state
                _currentSectionIndex = 0;
                _currentCharIndex = 0;
                _headerPrintedOnThisJob = false;
            }

            // Fonts
            using (Font titleFont = new Font("Segoe UI", 14F, FontStyle.Bold))
            using (Font sectionTitleFont = new Font("Arial", 9F, FontStyle.Bold))
            using (Font labelFont = new Font("Arial", 8F, FontStyle.Bold))
            using (Font italicFont = new Font("Arial", 9F, FontStyle.Italic))
            using (Font valueFont = new Font("Arial", 8F, FontStyle.Underline))
            using (Font bodyFont = new Font("Arial", 8F, FontStyle.Regular))
            {
                // Print header at top of each page and advance y accordingly
                try
                {
                    y = WaterMarkHelper.PrintHeader(g, left, (int)y, e.PageBounds.Width);
                }
                catch
                {
                    // ignore header errors
                }
                y += 6;

                // Print patient info only once at the very start of the job (first page)
                if (!_headerPrintedOnThisJob)
                {
                    g.DrawString("Name:", labelFont, Brushes.Black, left, y);
                    g.DrawString(_patientName, valueFont, Brushes.Black, left + 100, y);
                    g.DrawString("Age:", labelFont, Brushes.Black, left + 400, y);
                    g.DrawString(_patientAge, valueFont, Brushes.Black, left + 430, y);
                    g.DrawString("Sex:", labelFont, Brushes.Black, left + 470, y);
                    g.DrawString(_patientGender, valueFont, Brushes.Black, left + 500, y);
                    y += 20;

                    g.DrawString("Address:", labelFont, Brushes.Black, left, y);
                    g.DrawString(_patientAddress, valueFont, Brushes.Black, left + 100, y);
                    g.DrawString("Date:", labelFont, Brushes.Black, left + 400, y);
                    g.DrawString(_admitDate.ToString("MM/dd/yyyy"), valueFont, Brushes.Black, left + 440, y);
                    y += 20;

                    g.DrawString("ADMITTING ORDERS", titleFont, Brushes.Black, left, y);
                    y += titleFont.Height + 10;

                    g.DrawLine(Pens.Black, left, y, left + contentWidth, y);
                    y += 10;

                    _headerPrintedOnThisJob = true;
                }

                // Available bottom Y where body must stop (leave room for footer)
                float bottomLimit = margins.Top + margins.Height - footerReserve;

                // Iterate sections, resuming from saved progress
                while (_currentSectionIndex < _sections.Count)
                {
                    var section = _sections[_currentSectionIndex];

                    // Skip empty sections quickly
                    if (string.IsNullOrWhiteSpace(section.content))
                    {
                        _currentSectionIndex++;
                        _currentCharIndex = 0;
                        continue;
                    }

                    // If we are at the start of this section, print the section title first
                    if (_currentCharIndex == 0)
                    {
                        float titleHeight = g.MeasureString(section.title + ":", sectionTitleFont).Height;
                        if (y + titleHeight > bottomLimit)
                        {
                            // Not enough room for title -> next page
                            e.HasMorePages = true;
                            return;
                        }
                        g.DrawString(section.title + ":", sectionTitleFont, Brushes.Black, left, y);
                        y += titleHeight + 2;
                    }

                    // Now draw as much of the section.content as fits
                    float availableHeight = bottomLimit - y;
                    if (availableHeight <= 0)
                    {
                        // No vertical space left -> next page
                        e.HasMorePages = true;
                        return;
                    }

                    // choose font: italic section uses italicFont
                    Font fontToUse = section.isItalic ? italicFont : bodyFont;

                    // Draw chunk of text that fits and get how many chars were drawn
                    int charsDrawn = DrawTextChunkThatFits(g, section.content, fontToUse, left, y, contentWidth, availableHeight, _currentCharIndex, out float heightUsed);

                    if (charsDrawn <= 0)
                    {
                        // If nothing could be drawn on this page and we were at the start of the section,
                        // force-print at least something to avoid infinite loop (very rare).
                        if (_currentCharIndex == 0)
                        {
                            g.DrawString(section.content, fontToUse, Brushes.Black, new RectangleF(left, y, contentWidth, availableHeight));
                            heightUsed = g.MeasureString(section.content, fontToUse, (int)contentWidth).Height;
                            charsDrawn = section.content.Length;
                        }
                        else
                        {
                            // We drew nothing because no room; continue on next page
                            e.HasMorePages = true;
                            return;
                        }
                    }

                    // Advance pointers
                    _currentCharIndex += charsDrawn;
                    y += heightUsed + 8; // spacing after section text

                    // If the entire section content was consumed, move to the next section
                    if (_currentCharIndex >= section.content.Length)
                    {
                        _currentSectionIndex++;
                        _currentCharIndex = 0;
                    }

                    // If we've used up the page space, continue on next page
                    if (y >= bottomLimit - 1)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    // loop continues to next section
                } // end while sections

                // All sections printed — print footer and finish
                try
                {
                    float footerY = margins.Top + margins.Height - footerReserve + 5;
                    WaterMarkHelper_lic.PrintFooter(g, left, (int)footerY, (int)contentWidth + 45);
                }
                catch { /* ignore footer errors */ }

                // Reset state for the next print job
                e.HasMorePages = false;
                _sections = null;
                _currentSectionIndex = 0;
                _currentCharIndex = 0;
                _headerPrintedOnThisJob = false;
            } // end using fonts
        }

        // Helper: draws maximal substring of 'text' starting at startIndex that fits inside the given height.
        // Returns number of characters drawn and sets heightUsed to the drawn block height.
        private int DrawTextChunkThatFits(Graphics g, string text, Font font, float x, float y, float width, float availableHeight, int startIndex, out float heightUsed)
        {
            heightUsed = 0f;
            if (startIndex >= text.Length) return 0;

            int maxLen = text.Length - startIndex;
            // Binary search for largest substring that fits into availableHeight
            int lo = 1, hi = maxLen, best = 0;
            while (lo <= hi)
            {
                int mid = (lo + hi) / 2;
                string candidate = text.Substring(startIndex, mid);
                SizeF size = g.MeasureString(candidate, font, (int)width);
                if (size.Height <= availableHeight)
                {
                    best = mid;
                    lo = mid + 1;
                }
                else
                {
                    hi = mid - 1;
                }
            }

            if (best == 0)
            {
                // nothing fits
                return 0;
            }

            string toDraw = text.Substring(startIndex, best);
            RectangleF drawRect = new RectangleF(x, y, width, availableHeight);
            g.DrawString(toDraw, font, Brushes.Black, drawRect);
            heightUsed = g.MeasureString(toDraw, font, (int)width).Height;
            return best;
        }


        #endregion
        private float DrawTwoColumnSection(
    Graphics g,
    float left,
    float y,
    float totalWidth,
    Font titleFont,
    Font bodyFont,
    (string Title, string Content) col1,
    (string Title, string Content) col2)
        {
            float colWidth = totalWidth / 2f; // two equal columns
            float maxHeight = 0;

            var cols = new[] { col1, col2 }; // two columns only

            for (int i = 0; i < 2; i++) // ONLY 2 ITERATIONS
            {
                float colLeft = left + (colWidth * i);

                // Draw column title
                g.DrawString(cols[i].Title + ":", titleFont, Brushes.Black, colLeft, y);

                // Draw content wrapped
                RectangleF rect = new RectangleF(
                    colLeft,
                    y + titleFont.Height + 2,
                    colWidth - 5,
                    2000 // very tall box for wrapped text
                );

                DrawWrappedString(g, cols[i].Content ?? "", bodyFont, rect);

                // Measure real used height
                float usedHeight =
                    titleFont.Height + 2 +
                    g.MeasureString(cols[i].Content ?? "", bodyFont, (int)(colWidth - 5)).Height;

                if (usedHeight > maxHeight)
                    maxHeight = usedHeight;
            }

            return y + maxHeight + 5;
        }

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

        private float DrawThreeColumnSection(
            Graphics g,
            float left,
            float y,
            float totalWidth,
            Font titleFont,
            Font bodyFont,
            (string Title, string Content) col1,
            (string Title, string Content) col2,
            (string Title, string Content) col3)
        {
            float colWidth = totalWidth / 3f;
            float maxHeight = 0;

            var cols = new[] { col1, col2, col3 };

            for (int i = 0; i < 3; i++)
            {
                float colLeft = left + colWidth * i;

                g.DrawString(cols[i].Title + ":", titleFont, Brushes.Black, colLeft, y);

                RectangleF rect = new RectangleF(colLeft, y + titleFont.Height + 2, colWidth - 5, 2000);
                DrawWrappedString(g, cols[i].Content ?? "", bodyFont, rect);

                float usedHeight =
                    titleFont.Height + 2 +
                    g.MeasureString(cols[i].Content ?? "", bodyFont, (int)(colWidth - 5)).Height;

                if (usedHeight > maxHeight)
                    maxHeight = usedHeight;
            }

            return y + maxHeight + 5;
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
