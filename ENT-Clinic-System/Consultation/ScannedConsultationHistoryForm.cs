using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ENT_Clinic_System.Helpers; // For DBConfig

namespace ENT_Clinic_System.Consultation
{
    public partial class ScannedConsultationHistoryForm : UserControl
    {
        private readonly int _patientId;

        public ScannedConsultationHistoryForm(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            this.Load += ScannedConsultationHistoryForm_Load;
        }

        private void ScannedConsultationHistoryForm_Load(object sender, EventArgs e)
        {
            LoadPatientScannedDocuments(_patientId);
            LoadPatientName(_patientId);
        }

        private void LoadPatientName(int patientId)
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT full_name FROM patients WHERE patient_id = @id LIMIT 1;";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", patientId);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            this.Text = $"Scanned Documents for {result.ToString()}";
                        }
                        else
                        {
                            this.Text = "Scanned Documents for Unknown Patient";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patient name: " + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// Loads all scanned documents for the given patient and displays them.
        /// </summary>
        private void LoadPatientScannedDocuments(int patientId)
        {
            try
            {
                scannedDocumentsFlowLayoutPanel.Controls.Clear();

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT image_path
                        FROM patient_documents
                        WHERE patient_id = @patient_id
                        ORDER BY created_at DESC;
                    ";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@patient_id", patientId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            bool hasDocuments = false;

                            while (reader.Read())
                            {
                                string filePath = reader.GetString("image_path");

                                if (File.Exists(filePath))
                                {
                                    AddThumbnailToFlowPanel(filePath);
                                    hasDocuments = true;
                                }
                                else
                                {
                                    // Missing file label
                                    Label missing = new Label
                                    {
                                        Text = $"❌ Missing file:\n{filePath}",
                                        ForeColor = Color.Red,
                                        AutoSize = true,
                                        Margin = new Padding(8)
                                    };
                                    scannedDocumentsFlowLayoutPanel.Controls.Add(missing);
                                }
                            }

                            if (!hasDocuments)
                            {
                                Label emptyLabel = new Label
                                {
                                    Text = "No scanned documents found for this patient.",
                                    AutoSize = true,
                                    Font = new Font("Segoe UI", 11, FontStyle.Italic),
                                    ForeColor = Color.Gray,
                                    Margin = new Padding(20)
                                };
                                scannedDocumentsFlowLayoutPanel.Controls.Add(emptyLabel);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading documents: " + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Adds a thumbnail or icon for the file to the FlowLayoutPanel.
        /// </summary>
        private void AddThumbnailToFlowPanel(string filePath)
        {
            try
            {
                PictureBox pb = new PictureBox
                {
                    Size = new Size(471, 820),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10),
                    Cursor = Cursors.Hand,
                    Tag = filePath
                };

                string ext = Path.GetExtension(filePath).ToLower();

                // 🖼 Try to show image preview if it’s an image file
                if (ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp")
                {
                    using (var img = new Bitmap(filePath))
                    {
                        pb.Image = new Bitmap(img);
                    }
                }
                else
                {
                    // 📄 Non-image file — show default system icon
                    Icon icon = Icon.ExtractAssociatedIcon(filePath);
                    if (icon != null)
                        pb.Image = icon.ToBitmap();
                    else
                        pb.Image = SystemIcons.Application.ToBitmap();
                }

                // 🔹 Single click: open with default app
                pb.Click += (s, e) =>
                {
                    string file = pb.Tag as string;
                    OpenFileWithDefaultApp(file);
                };

                // 🔹 Right-click: delete
                ContextMenuStrip menu = new ContextMenuStrip();
                ToolStripMenuItem deleteItem = new ToolStripMenuItem("🗑 Delete This Document")
                {
                    ForeColor = Color.Red
                };
                deleteItem.Click += (s, e) =>
                {
                    if (MessageBox.Show("Are you sure you want to delete this document?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        DeleteDocumentFromDatabase(filePath);
                        if (File.Exists(filePath))
                            File.Delete(filePath);

                        scannedDocumentsFlowLayoutPanel.Controls.Remove(pb);
                        pb.Dispose();
                    }
                };
                menu.Items.Add(deleteItem);
                pb.ContextMenuStrip = menu;

                scannedDocumentsFlowLayoutPanel.Controls.Add(pb);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying file: " + ex.Message,
                    "Display Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens a file with its default program (supports all file types).
        /// </summary>
        private void OpenFileWithDefaultApp(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("File not found:\n" + path, "Missing File",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Preferred method
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch
            {
                try
                {
                    // Fallback 1: cmd start
                    var startInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c start \"\" \"{path}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch
                {
                    try
                    {
                        // Fallback 2: open folder and highlight file
                        System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{path}\"");
                    }
                    catch (Exception ex3)
                    {
                        MessageBox.Show("Unable to open file: " + ex3.Message,
                            "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Deletes a document record from the database.
        /// </summary>
        private void DeleteDocumentFromDatabase(string imagePath)
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM patient_documents WHERE image_path = @path LIMIT 1;";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@path", imagePath);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScannedConsultationHistoryForm_Load_1(object sender, EventArgs e)
        {

        }

        private void scannedDocumentsFlowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
