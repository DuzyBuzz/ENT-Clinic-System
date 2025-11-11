using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    public class AdmitOrdersPrintHelper
    {
        private readonly int _admitOrderId;
        private readonly RichTextBox _rtfBox;
        private readonly PrintDocument _printDocument;

        // Patient info
        private string _patientName = "";
        private string _patientAddress = "";
        private string _patientAge = "";
        private string _patientGender = "";
        private DateTime _admitDate = DateTime.Now;

        // Used for RTF printing
        private int _checkPrint;

        public AdmitOrdersPrintHelper(int admitOrderId, RichTextBox rtb)
        {
            _admitOrderId = admitOrderId;
            _rtfBox = rtb ?? new RichTextBox();

            LoadData();

            _printDocument = new PrintDocument();
            _printDocument.DefaultPageSettings.Margins = new Margins(40, 40, 60, 60);
            _printDocument.PrintPage += PrintDocument_PrintPage;
        }

        #region Load Data
        private void LoadData()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT ao.admit_date, p.full_name, p.address, p.sex, TIMESTAMPDIFF(YEAR, p.birth_date, CURDATE()) AS age
                        FROM admit_orders ao
                        JOIN patients p ON ao.patient_id = p.patient_id
                        WHERE ao.admit_order_id = @admitOrderId
                        LIMIT 1";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@admitOrderId", _admitOrderId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read()) throw new Exception("Admit order not found.");
                            _admitDate = reader["admit_date"] != DBNull.Value
                                ? Convert.ToDateTime(reader["admit_date"])
                                : DateTime.Now;
                            _patientName = SafeString(reader["full_name"]);
                            _patientAddress = SafeString(reader["address"]);
                            _patientGender = SafeString(reader["sex"]);
                            _patientAge = SafeString(reader["age"]);
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
        #endregion

        #region Printing
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int leftMargin = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;

            // --- Header ---
            y = WaterMarkHelper.PrintHeader(g, leftMargin, y, e.PageBounds.Width);

            // --- Patient Info ---
            using (Font labelFont = new Font("Arial", 8, FontStyle.Bold))
            using (Font valueFont = new Font("Arial", 8))
            {
                int underlineOffset = 2;
                void DrawField(string label, string value, int xLabel, int xValue)
                {
                    g.DrawString(label, labelFont, Brushes.Black, xLabel, y);
                    g.DrawString(value, valueFont, Brushes.Black, xValue, y);
                    SizeF size = g.MeasureString(value, valueFont);
                    g.DrawLine(Pens.Black, xValue, y + size.Height + underlineOffset,
                               xValue + size.Width, y + size.Height + underlineOffset);
                }

                DrawField("Name:", _patientName, leftMargin, leftMargin + 100);
                DrawField("Age:", _patientAge, leftMargin + 350, leftMargin + 380);
                DrawField("Sex:", _patientGender, leftMargin + 420, leftMargin + 450);

                y += 20;
                DrawField("Address:", _patientAddress, leftMargin, leftMargin + 100);
                DrawField("Date:", _admitDate.ToString("MMMM dd, yyyy"), leftMargin + 345, leftMargin + 380);

                y += 25;
            }

            // --- RTF content ---
            if (_rtfBox != null && !string.IsNullOrWhiteSpace(_rtfBox.Rtf))
            {
                _checkPrint = 0;
                y = PrintRtfToGraphics(_rtfBox, g, e.MarginBounds, y);
            }

            // --- Footer ---
            WaterMarkHelper.PrintFooter(g, e.MarginBounds.Left, e.MarginBounds.Bottom - 30, e.MarginBounds.Width);

            e.HasMorePages = false;
        }
        #endregion

        #region RTF Printing Helper
        private int PrintRtfToGraphics(RichTextBox rtb, Graphics g, Rectangle marginBounds, int startY)
        {
            if (rtb == null || string.IsNullOrWhiteSpace(rtb.Rtf))
                return startY;

            // Convert pixels to twips (1 twip = 1/1440 inch)
            float inchToTwips = 1440f;
            float dpiX = g.DpiX;
            float dpiY = g.DpiY;

            RECT rect = new RECT
            {
                Left = (int)(marginBounds.Left / dpiX * inchToTwips),
                Top = (int)(startY / dpiY * inchToTwips),
                Right = (int)(marginBounds.Right / dpiX * inchToTwips),
                Bottom = (int)(marginBounds.Bottom / dpiY * inchToTwips)
            };

            IntPtr hdc = g.GetHdc(); // get HDC once
            try
            {
                FORMATRANGE fmtRange = new FORMATRANGE
                {
                    chrg = new CHARRANGE { cpMin = _checkPrint, cpMax = rtb.TextLength },
                    hdc = hdc,
                    hdcTarget = hdc,
                    rc = rect,
                    rcPage = rect
                };

                IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
                Marshal.StructureToPtr(fmtRange, lParam, false);

                // EM_FORMATRANGE wParam = 1 to render, returns chars printed
                IntPtr res = SendMessage(rtb.Handle, EM_FORMATRANGE, new IntPtr(1), lParam);
                int charsPrinted = res.ToInt32();

                // Update the starting point for next print if multi-page
                _checkPrint += charsPrinted;

                Marshal.FreeCoTaskMem(lParam);

                // Clear cached format range
                SendMessage(rtb.Handle, EM_FORMATRANGE, IntPtr.Zero, IntPtr.Zero);

                // Estimate printed height based on proportion of text printed
                double ratio = (double)charsPrinted / Math.Max(1, rtb.TextLength);
                int usedHeight = (int)((marginBounds.Bottom - startY) * ratio);
                if (usedHeight < 20) usedHeight = 20; // minimal height
                return startY + usedHeight;
            }
            finally
            {
                g.ReleaseHdc(hdc); // release HDC once
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct RECT { public int Left, Top, Right, Bottom; }

        [StructLayout(LayoutKind.Sequential)]
        private struct CHARRANGE { public int cpMin, cpMax; }

        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE
        {
            public CHARRANGE chrg;
            public IntPtr hdc;
            public IntPtr hdcTarget;
            public RECT rc;
            public RECT rcPage;
        }

        private const int EM_FORMATRANGE = 0x439;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
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
            preview.ShowDialog();
        }

        public void Print()
        {
            using (PrintDialog dlg = new PrintDialog { Document = _printDocument })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    _printDocument.Print();
            }
        }
        #endregion
    }
}
