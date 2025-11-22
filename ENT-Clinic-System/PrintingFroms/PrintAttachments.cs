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
    public partial class PrintAttachments : UserControl
    {
        private int consultationId;
        private int patientId;
        private string fullName;
        private string consultationDate;

        private ImageFlowHelper imageHelper;
        private VideoFlowHelper videoHelper;

        private int currentPrintIndex = 0;

        public PrintAttachments(int consultationId)
        {
            InitializeComponent();
            this.consultationId = consultationId;

            LoadPatientInfo();

            imageHelper = new ImageFlowHelper(imagesPanel);
            videoHelper = new VideoFlowHelper(videosPanel);

            LoadAttachmentsFromDatabase();
        }

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
                            }
                        }
                    }
                }

                this.Text = $"{fullName} - {consultationDate}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patient info: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                                    imageHelper.AddImage(path, note, category);
                                else if (type == "Video")
                                    videoHelper.AddVideo(path, note, category);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attachments: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            var imagesToPrint = imageHelper.GetAllImages();
            if (imagesToPrint.Count == 0)
            {
                MessageBox.Show("No images to print.");
                return;
            }

            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.Landscape = false;
            doc.PrintPage += (s, f) => PrintDocImages_PrintPage(s, f, imagesToPrint);

            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = doc;
                preview.Width = 900;
                preview.Height = 700;
                preview.ShowDialog();
            }
        }

        private void PrintDocImages_PrintPage(object sender, PrintPageEventArgs e, List<(string ImagePath, string Note, string Category)> images)
        {
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            float pageWidth = e.MarginBounds.Width;
            float pageHeight = e.MarginBounds.Height;
            Graphics g = e.Graphics;

            using (Font headerFont = new Font("Arial", 16, FontStyle.Bold))
            using (Font infoFont = new Font("Arial", 12, FontStyle.Regular))
            {
                if (currentPrintIndex == 0)
                {
                    string title = $"Attachments - {fullName}";
                    SizeF titleSize = g.MeasureString(title, headerFont);
                    g.DrawString(title, headerFont, Brushes.Black, x + (pageWidth - titleSize.Width) / 2, y);
                    y += titleSize.Height + 5;

                    string dateInfo = $"Consultation Date: {consultationDate}";
                    SizeF dateSize = g.MeasureString(dateInfo, infoFont);
                    g.DrawString(dateInfo, infoFont, Brushes.Black, x + (pageWidth - dateSize.Width) / 2, y);
                    y += dateSize.Height + 20;
                }
            }

            float maxImageHeight = 400;
            float maxImageWidth = pageWidth - 40;
            float spacing = 20;

            while (currentPrintIndex < images.Count)
            {
                var imgData = images[currentPrintIndex];
                using (Image img = Image.FromFile(imgData.ImagePath))
                {
                    float scale = Math.Min(maxImageWidth / img.Width, maxImageHeight / img.Height);
                    int drawWidth = (int)(img.Width * scale);
                    int drawHeight = (int)(img.Height * scale);

                    if (y + drawHeight > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    float drawX = x + (pageWidth - drawWidth) / 2;
                    g.DrawImage(img, new RectangleF(drawX, y, drawWidth, drawHeight));
                    y += drawHeight + spacing;

                    using (Font noteFont = new Font("Arial", 10, FontStyle.Italic))
                    {
                        SizeF noteSize = g.MeasureString(imgData.Note, noteFont);
                        g.DrawString(imgData.Note, noteFont, Brushes.Black, x + (pageWidth - noteSize.Width) / 2, y);
                        y += noteSize.Height + spacing;
                    }
                }
                currentPrintIndex++;
            }

            e.HasMorePages = false;
            currentPrintIndex = 0;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    // Update images
                    var images = imageHelper.GetAllImages();
                    foreach (var img in images)
                    {
                        string sql = @"
                    UPDATE attachments 
                    SET note = @note, category = @category 
                    WHERE consultation_id = @consultationId AND file_path = @path AND file_type = 'Image'";

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@note", img.Note);
                            cmd.Parameters.AddWithValue("@category", img.Category);
                            cmd.Parameters.AddWithValue("@consultationId", consultationId);
                            cmd.Parameters.AddWithValue("@path", img.ImagePath);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Update videos
                    var videos = videoHelper.GetAllVideos();
                    foreach (var vid in videos)
                    {
                        string sql = @"
                    UPDATE attachments 
                    SET note = @note, category = @category 
                    WHERE consultation_id = @consultationId AND file_path = @path AND file_type = 'Video'";

                        using (var cmd = new MySqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@note", vid.Note);
                            cmd.Parameters.AddWithValue("@category", vid.Category);
                            cmd.Parameters.AddWithValue("@consultationId", consultationId);
                            cmd.Parameters.AddWithValue("@path", vid.VideoPath);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Changes saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save changes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
