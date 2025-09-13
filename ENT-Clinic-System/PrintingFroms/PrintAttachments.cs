using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using ENT_Clinic_System.Helpers;

namespace ENT_Clinic_System.PrintingFroms
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

        public PrintAttachments(int consultationId, int patientId, string fullName, string consultationDate)
        {
            InitializeComponent();
            this.consultationId = consultationId;
            this.patientId = patientId;
            this.fullName = fullName;
            this.consultationDate = consultationDate;

            imageHelper = new ImageFlowHelper(imagesPanel);
            videoHelper = new VideoFlowHelper(videosPanel);

            LoadAttachmentsFromDatabase();
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
                                    // Add to UI and prepare for printing
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

            // ✅ Collect images from imagesPanel if not already collected
            if (imagesToPrint.Count == 0)
            {
                foreach (Control ctrl in imagesPanel.Controls)
                {
                    if (ctrl is PictureBox pb && pb.Image != null)
                        imagesToPrint.Add(pb.Image);
                }
            }

            using (Font headerFont = new Font("Arial", 14, FontStyle.Bold))
            {
                // Print "Attachments" title only once at the start of the print job
                if (currentImageIndex == 0)
                {
                    string attachTitle = "Attachments";
                    SizeF headerSize = g.MeasureString(attachTitle, headerFont);

                    g.DrawString(
                        attachTitle,
                        headerFont,
                        Brushes.Black,
                        x + (contentWidth - headerSize.Width) / 2,
                        y
                    );

                    y += headerSize.Height + 20; // add spacing after title
                }
            }

            // ✅ Print images one by one with pagination
            while (currentImageIndex < imagesToPrint.Count)
            {
                Image img = imagesToPrint[currentImageIndex];

                // Scale image to fit inside page bounds while maintaining aspect ratio
                float ratio = Math.Min(
                    contentWidth / img.Width,
                    (e.MarginBounds.Height - (y - e.MarginBounds.Top)) / (float)img.Height
                );

                int drawWidth = (int)(img.Width * ratio);
                int drawHeight = (int)(img.Height * ratio);

                // Check if image fits in current page, else go to next
                if (y + drawHeight > pageBottom)
                {
                    e.HasMorePages = true;
                    return; // keep currentImageIndex unchanged → continue next page
                }

                // Center the image horizontally
                float drawX = x + (contentWidth - drawWidth) / 2;
                g.DrawImage(img, new RectangleF(drawX, y, drawWidth, drawHeight));

                y += drawHeight + 25; // spacing between images
                currentImageIndex++;
            }

            // ✅ No more pages → reset index
            e.HasMorePages = false;
            currentImageIndex = 0;
        }

        private void PrintAttachments_Load(object sender, EventArgs e)
        {
            this.Text = $"{fullName} - {consultationDate}";
        }
    }
}
