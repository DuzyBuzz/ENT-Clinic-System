using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Reports
{
    public class BillingReport
    {
        private string patientFilter;
        private DateTime fromDate;
        private DateTime toDate;
        private PrintDocument printDocument;

        private float yPosition = 0;
        private int lastPrintedIndex = 0;
        private int lastPaymentIndex = 0;

        private class Payment
        {
            public DateTime PaymentDate;
            public decimal Amount;
            public decimal ChangeDue;
            public string Note;
        }

        private class Billing
        {
            public int BillingID;
            public string PatientName;
            public decimal Fee;
            public decimal DiscountPercent;
            public decimal DiscountAmount;
            public decimal TotalAmount;
            public decimal AmountPaid;
            public decimal Balance;
            public string Note;
            public List<Payment> Payments = new List<Payment>();
        }

        private List<Billing> billings = new List<Billing>();

        public BillingReport(string patient, DateTime from, DateTime to)
        {
            patientFilter = patient;
            fromDate = from.Date;
            toDate = to.Date;

            LoadBillings();

            printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Landscape = true;
            printDocument.PrintPage += PrintDocument_PrintPage;
        }

        private void LoadBillings()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT billing_id, patient_name, fee, discount_percent, discount_amount, total_amount,
                               amount_paid, billing_balance, billing_note, payment_date, payment_amount, change_due, payment_note
                        FROM billing_report
                        WHERE (@patient = '' OR patient_name = @patient)
                          AND DATE(created_at) BETWEEN @from AND @to
                        ORDER BY billing_id, payment_date";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@patient", patientFilter);
                        cmd.Parameters.AddWithValue("@from", fromDate);
                        cmd.Parameters.AddWithValue("@to", toDate);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int billingId = Convert.ToInt32(reader["billing_id"]);
                                var billing = billings.Find(b => b.BillingID == billingId);
                                if (billing == null)
                                {
                                    billing = new Billing
                                    {
                                        BillingID = billingId,
                                        PatientName = reader["patient_name"].ToString(),
                                        Fee = reader["fee"] != DBNull.Value ? Convert.ToDecimal(reader["fee"]) : 0,
                                        DiscountPercent = reader["discount_percent"] != DBNull.Value ? Convert.ToDecimal(reader["discount_percent"]) : 0,
                                        DiscountAmount = reader["discount_amount"] != DBNull.Value ? Convert.ToDecimal(reader["discount_amount"]) : 0,
                                        TotalAmount = reader["total_amount"] != DBNull.Value ? Convert.ToDecimal(reader["total_amount"]) : 0,
                                        AmountPaid = reader["amount_paid"] != DBNull.Value ? Convert.ToDecimal(reader["amount_paid"]) : 0,
                                        Balance = reader["billing_balance"] != DBNull.Value ? Convert.ToDecimal(reader["billing_balance"]) : 0,
                                        Note = reader["billing_note"] != DBNull.Value ? reader["billing_note"].ToString() : ""
                                    };
                                    billings.Add(billing);
                                }

                                if (reader["payment_date"] != DBNull.Value)
                                {
                                    billing.Payments.Add(new Payment
                                    {
                                        PaymentDate = Convert.ToDateTime(reader["payment_date"]),
                                        Amount = reader["payment_amount"] != DBNull.Value ? Convert.ToDecimal(reader["payment_amount"]) : 0,
                                        ChangeDue = reader["change_due"] != DBNull.Value ? Convert.ToDecimal(reader["change_due"]) : 0,
                                        Note = reader["payment_note"] != DBNull.Value ? reader["payment_note"].ToString() : ""
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading billing report: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int left = 40;
            int width = e.PageBounds.Width;
            float y = yPosition > 0 ? yPosition : 40;

            using (Font headerFont = new Font("Segoe UI", 14, FontStyle.Bold))
            using (Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold))
            using (Font regularFont = new Font("Segoe UI", 10))
            using (Pen lightPen = new Pen(Color.LightGray))
            {
                // ---------- Header ----------
                if (yPosition == 0)
                {
                    g.DrawString("BILLING REPORT", headerFont, Brushes.Black,
                        new RectangleF(left, y, width - 2 * left, 30),
                        new StringFormat() { Alignment = StringAlignment.Center });
                    y += 40;

                    g.DrawString($"From: {fromDate:MM/dd/yyyy}  To: {toDate:MM/dd/yyyy}", regularFont, Brushes.Black, left, y);
                    y += 30;
                }

                while (lastPrintedIndex < billings.Count)
                {
                    var b = billings[lastPrintedIndex];

                    // ---------- Billing row ----------
                    string billingNo = "BILL-" + b.BillingID.ToString("D6");
                    string status = b.Balance == 0 ? "FULLY PAID" : b.Balance > 0 ? "PARTIALLY PAID" : "UNPAID";

                    // First line: Billing No, Patient, Fee, Total, Status
                    g.DrawString("Billing No:", boldFont, Brushes.Black, left, y);
                    g.DrawString(billingNo, regularFont, Brushes.Black, left + 80, y);

                    g.DrawString("Patient:", boldFont, Brushes.Black, left + 300, y);
                    g.DrawString(b.PatientName, regularFont, Brushes.Black, left + 360, y); // moved slightly left

                    g.DrawString("Fee:", boldFont, Brushes.Black, left + 600, y);
                    g.DrawString(b.Fee.ToString("N2"), regularFont, Brushes.Black, left + 640, y);

                    g.DrawString("Total:", boldFont, Brushes.Black, left + 740, y);
                    g.DrawString(b.TotalAmount.ToString("N2"), regularFont, Brushes.Black, left + 780, y);

                    g.DrawString("Status:", boldFont, Brushes.Black, left + 900, y);
                    g.DrawString(status, regularFont, Brushes.Black, left + 950, y);

                    y += 20;

                    // ---------- Discount and Note ----------
                    g.DrawString("Discount:", boldFont, Brushes.Black, left + 40, y);
                    g.DrawString($"{b.DiscountPercent:N0}%", regularFont, Brushes.Black, left + 120, y); // 10%

                    if (!string.IsNullOrWhiteSpace(b.Note))
                    {
                        g.DrawString("Billing Note:", boldFont, Brushes.Black, left + 220, y);
                        g.DrawString(b.Note, regularFont, Brushes.Black, left + 320, y);
                    }

                    y += 20;

                    // Light separator after each payment
                    g.DrawLine(lightPen, left + 30, y, width - left - 30, y);
                    y += 5;
                    // ---------- Payments ----------
                    bool showPayments = !(b.Balance == 0 && b.DiscountAmount == b.TotalAmount);
                    if (showPayments && b.Payments.Count > 0)
                    {
                        g.DrawString("Payments:", boldFont, Brushes.Black, left + 20, y);
                        y += 20;

                        for (int i = lastPaymentIndex; i < b.Payments.Count; i++)
                        {
                            var p = b.Payments[i];

                            g.DrawString("Date:", regularFont, Brushes.Black, left + 40, y);
                            g.DrawString(p.PaymentDate.ToString("MM/dd/yyyy"), regularFont, Brushes.Black, left + 90, y);

                            g.DrawString("Amount:", regularFont, Brushes.Black, left + 200, y);
                            g.DrawString(p.Amount.ToString("N2"), regularFont, Brushes.Black, left + 260, y);

                            g.DrawString("Change:", regularFont, Brushes.Black, left + 370, y);
                            g.DrawString(p.ChangeDue.ToString("N2"), regularFont, Brushes.Black, left + 430, y);

                            g.DrawString("Note:", regularFont, Brushes.Black, left + 520, y);
                            g.DrawString(p.Note, regularFont, Brushes.Black, left + 560, y);

                            y += 20;

                            // Light separator after each payment
                            g.DrawLine(lightPen, left + 30, y, width - left - 30, y);
                            y += 5;

                            // Page overflow
                            if (y > e.PageBounds.Height - 100)
                            {
                                lastPaymentIndex = i + 1;
                                yPosition = y;
                                e.HasMorePages = true;
                                return;
                            }
                        }
                        lastPaymentIndex = 0; // reset for next billing
                    }

                    // Separator line between billings
                    y += 10;
                    g.DrawLine(Pens.Black, left, y, width - left, y);
                    y += 20;

                    // Page overflow
                    if (y > e.PageBounds.Height - 100)
                    {
                        lastPrintedIndex++;
                        yPosition = y;
                        e.HasMorePages = true;
                        return;
                    }

                    lastPrintedIndex++;
                }

                // ---------- Footer ----------
                g.DrawString("Printed By: " + UserCredentials.Fullname, regularFont, Brushes.Black, left, e.PageBounds.Bottom - 80);
                g.DrawString("Date/Time: " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"), regularFont, Brushes.Black,
                    width - 300, e.PageBounds.Bottom - 80);

                // Reset counters
                lastPrintedIndex = 0;
                lastPaymentIndex = 0;
                yPosition = 0;
            }
        }


        public void ShowPreview()
        {
            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 1000,
                Height = 700,
                Text = "Billing Report Preview"
            };

            preview.Shown += (s, e) =>
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
                        using (PrintDialog pd = new PrintDialog { Document = printDocument })
                        {
                            if (pd.ShowDialog() == DialogResult.OK)
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
