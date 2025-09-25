using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using ENT_Clinic_System.Helpers;

namespace ENT_Clinic_System.Consultation
{
    public partial class PrintAttachments : Form
    {
        private int consultationId;
        private int patientId;
        private string fullName;
        private string consultationDate;

        private ImageFlowHelper imageHelper;
        private VideoFlowHelper videoHelper;
        private List<Image> imagesToPrint = new List<Image>();

        // Keep this field at class-level so pagination works
        private int currentImageIndex = 0;

        public PrintAttachments(int consultationId)
        {
            InitializeComponent();
            this.consultationId = consultationId;

            // Load patient info automatically from DB
            LoadPatientInfo();

            imageHelper = new ImageFlowHelper(imagesPanel);
            videoHelper = new VideoFlowHelper(videosPanel);

            LoadAttachmentsFromDatabase();
        }

        /// <summary>
        /// Queries the database to get the patient's full name and consultation date
        /// </summary>
        private void LoadPatientInfo()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        SELECT c.consultation_date, p.full_name, p.patient_id
                        FROM consultation c
                        INNER JOIN patients p ON c.patient_id = p.patient_id
                        WHERE c.consultation_id = @consultationId
                        LIMIT 1";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultationId", consultationId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                consultationDate = reader["consultation_date"]?.ToString() ?? "";
                                fullName = reader["full_name"]?.ToString() ?? "";
                                patientId = Convert.ToInt32(reader["patient_id"]);
                            }
                            else
                            {
                                MessageBox.Show("Consultation not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }

                // Update form title
                this.Text = $"{fullName} - {consultationDate}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patient info: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary>
        /// Loads image and video attachments for the consultation from the database
        /// </summary>
        private void LoadAttachmentsFromDatabase()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT file_path, category, note, file_type
                        FROM attachments
                        WHERE consultation_id = @consultation_id";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultation_id", consultationId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string path = reader["file_path"]?.ToString() ?? "";
                                string category = reader["category"]?.ToString() ?? "";
                                string note = reader["note"]?.ToString() ?? "";
                                string type = reader["file_type"]?.ToString() ?? "";

                                if (!File.Exists(path)) continue;

                                if (type == "Image")
                                {
                                    imageHelper.AddImage(path, note, category);
                                    imagesToPrint.Add(Image.FromFile(path));
                                }
                                else if (type == "Video")
                                {
                                    videoHelper.AddVideo(path, note, category);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attachments: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Print button click → opens preview and prints images
        /// </summary>
        private void printButton_Click(object sender, EventArgs e)
        {
            if (imagesToPrint.Count == 0)
            {
                MessageBox.Show("No images to print.");
                return;
            }

            PrintDocument doc = new PrintDocument();
            doc.PrintPage += PrintDocImages_PrintPage;

            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = doc;
                preview.Width = 800;
                preview.Height = 600;
                preview.ShowDialog();
            }
        }

        /// <summary>
        /// Handles printing of consultation images with pagination and title
        /// </summary>
        private void PrintDocImages_PrintPage(object sender, PrintPageEventArgs e)
        {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float contentWidth = e.MarginBounds.Width;
            float pageBottom = e.MarginBounds.Bottom;

            Graphics g = e.Graphics;

            using (Font headerFont = new Font("Arial", 14, FontStyle.Bold))
            {
                if (currentImageIndex == 0)
                {
                    string attachTitle = $"Attachments - {fullName} ({consultationDate})";
                    SizeF headerSize = g.MeasureString(attachTitle, headerFont);

                    g.DrawString(
                        attachTitle,
                        headerFont,
                        Brushes.Black,
                        x + (contentWidth - headerSize.Width) / 2,
                        y
                    );

                    y += headerSize.Height + 20;
                }
            }

            while (currentImageIndex < imagesToPrint.Count)
            {
                Image img = imagesToPrint[currentImageIndex];

                float ratio = Math.Min(
                    contentWidth / img.Width,
                    (e.MarginBounds.Height - (y - e.MarginBounds.Top)) / (float)img.Height
                );

                int drawWidth = (int)(img.Width * ratio);
                int drawHeight = (int)(img.Height * ratio);

                if (y + drawHeight > pageBottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                float drawX = x + (contentWidth - drawWidth) / 2;
                g.DrawImage(img, new RectangleF(drawX, y, drawWidth, drawHeight));

                y += drawHeight + 25;
                currentImageIndex++;
            }

            e.HasMorePages = false;
            currentImageIndex = 0;
        }

        private void PrintAttachments_Load(object sender, EventArgs e)
        {
        }
    }
}
