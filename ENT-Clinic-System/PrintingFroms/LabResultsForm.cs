using ENT_Clinic_System.Helpers;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA; // ✅ for scanner
using CommonDialog = WIA.CommonDialog; // ✅ alias for scanner dialog

namespace ENT_Clinic_System.PrintingForms
{
    public partial class LabResultsForm : UserControl
    {
        private int consultationId;
        private int patientId;
        private string attachedFilePath = "";
        private int selectedResultId = -1;

        private readonly string basePath = SettingsHelper.GetSetting("base_path");

        public LabResultsForm(int consultationId, int patientId)
        {
            InitializeComponent();
            this.consultationId = consultationId;
            this.patientId = patientId;

            LoadLabResults();

            dgvLabResults.ContextMenuStrip = cmsDelete;
            deleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
            dgvLabResults.CellContentClick += dgvLabResults_CellContentClick;
            dgvLabResults.CellClick += dgvLabResults_CellClick;
        }

        // Load lab results
        private void LoadLabResults()
        {
            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT result_id, test_name, result_text, result_file, created_at 
                                     FROM lab_results WHERE consultation_id=@consultationId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@consultationId", consultationId);
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvLabResults.DataSource = dt;
                        }
                    }
                }

                RefreshPreviewPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading lab results: " + ex.Message);
            }
        }

        // Refresh preview panel
        private void RefreshPreviewPanel()
        {
            flpPreview.Controls.Clear();
            foreach (DataGridViewRow row in dgvLabResults.Rows)
            {
                string relativeFile = row.Cells["result_file"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(relativeFile))
                {
                    string fullPath = Path.Combine(basePath, relativeFile);
                    if (File.Exists(fullPath))
                    {
                        PictureBox pb = new PictureBox
                        {
                            Width = 150,
                            Height = 150,
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Tag = fullPath,
                            BorderStyle = BorderStyle.FixedSingle
                        };

                        // Load image into memory to avoid locking file
                        string ext = Path.GetExtension(fullPath).ToLower();
                        if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".tif" || ext == ".tiff")
                        {
                            using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                            {
                                Image img = Image.FromStream(fs);
                                pb.Image = new Bitmap(img);
                            }
                        }
                        else
                        {
                            string pdfThumb = Path.Combine(Application.StartupPath, "assets", "images", "pdf.png");
                            if (File.Exists(pdfThumb))
                                pb.Image = Image.FromFile(pdfThumb);
                        }

                        pb.Click += PreviewFile_Click;
                        flpPreview.Controls.Add(pb);
                    }
                }
            }
        }

        private void PreviewFile_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb != null)
            {
                string file = pb.Tag.ToString();
                if (File.Exists(file))
                    Process.Start(file);
            }
        }

        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Title = "Select Lab Result File",
                Filter = "All Files|*.*|PDF|*.pdf|Images|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff",
                Multiselect = false
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string tempFolder = Path.Combine(basePath, "Temp");
                Directory.CreateDirectory(tempFolder);

                string tempFile = Path.Combine(tempFolder, Guid.NewGuid().ToString() + Path.GetExtension(dlg.FileName));
                File.Copy(dlg.FileName, tempFile, true);

                attachedFilePath = tempFile;
                lblFileName.Text = Path.GetFileName(attachedFilePath);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string testName = txtTestName.Text.Trim();
            string resultText = txtResultText.Text.Trim();

            if (string.IsNullOrEmpty(testName))
            {
                MessageBox.Show("Test name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTestName.Focus();
                return;
            }

            try
            {
                progressBar.Visible = true;
                lblStatus.Visible = true;
                lblStatus.Text = "Uploading file...";

                string relativePath = null; // relative path to save in DB

                if (!string.IsNullOrEmpty(attachedFilePath))
                {
                    string finalFolder = Path.Combine(basePath, patientId.ToString(), consultationId.ToString(), "Lab Results");
                    Directory.CreateDirectory(finalFolder);

                    string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(attachedFilePath);
                    string finalFilePath = Path.Combine(finalFolder, uniqueName);

                    File.Move(attachedFilePath, finalFilePath);

                    relativePath = Path.Combine(patientId.ToString(), consultationId.ToString(), "Lab Results", uniqueName);
                }

                lblStatus.Text = "Saving to database...";

                await Task.Run(() =>
                {
                    using (MySqlConnection conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        string query = @"INSERT INTO lab_results 
                                         (consultation_id, test_name, result_text, result_file) 
                                         VALUES (@cid, @test, @text, @file)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@cid", consultationId);
                            cmd.Parameters.AddWithValue("@test", testName);
                            cmd.Parameters.AddWithValue("@text", resultText);
                            cmd.Parameters.AddWithValue("@file", relativePath != null ? (object)relativePath : DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }
                });

                progressBar.Visible = false;
                lblStatus.Visible = false;
                ClearForm();
                LoadLabResults();

                MessageBox.Show("✅ Lab result added successfully!");
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                lblStatus.Visible = false;
                MessageBox.Show("Error adding Laboratory result: " + ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLabResults.CurrentRow == null) return;

            int resultId = Convert.ToInt32(dgvLabResults.CurrentRow.Cells["result_id"].Value);
            string relativeFile = dgvLabResults.CurrentRow.Cells["result_file"].Value?.ToString() ?? "";
            string fullPath = Path.Combine(basePath, relativeFile);

            if (MessageBox.Show("Delete this lab result?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = DBConfig.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM lab_results WHERE result_id=@id";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", resultId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
                        File.Delete(fullPath);

                    LoadLabResults();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting lab result: " + ex.Message);
                }
            }
        }

        private void ClearForm()
        {
            txtTestName.Text = "";
            txtResultText.Text = "";
            lblFileName.Text = "";
            attachedFilePath = "";
            selectedResultId = -1;
        }

        private void dgvLabResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvLabResults.Columns[e.ColumnIndex].Name == "result_file")
            {
                string relativeFile = dgvLabResults.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
                string fullPath = Path.Combine(basePath, relativeFile);

                if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
                    Process.Start("explorer.exe", $"/select,\"{fullPath}\"");
                else
                    MessageBox.Show("File does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvLabResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedResultId = Convert.ToInt32(dgvLabResults.Rows[e.RowIndex].Cells["result_id"].Value);
                txtTestName.Text = dgvLabResults.Rows[e.RowIndex].Cells["test_name"].Value.ToString();
                txtResultText.Text = dgvLabResults.Rows[e.RowIndex].Cells["result_text"].Value.ToString();
                lblFileName.Text = Path.GetFileName(dgvLabResults.Rows[e.RowIndex].Cells["result_file"].Value.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedResultId == -1)
            {
                MessageBox.Show("Please select a lab result to update.");
                return;
            }

            string testName = txtTestName.Text.Trim();
            string resultText = txtResultText.Text.Trim();

            if (string.IsNullOrEmpty(testName))
            {
                MessageBox.Show("Test name is required.");
                return;
            }

            string relativePath = null;

            if (!string.IsNullOrEmpty(attachedFilePath))
            {
                string finalFolder = Path.Combine(basePath, patientId.ToString(), consultationId.ToString(), "Lab Results");
                Directory.CreateDirectory(finalFolder);

                string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(attachedFilePath);
                string finalFilePath = Path.Combine(finalFolder, uniqueName);

                File.Move(attachedFilePath, finalFilePath);

                relativePath = Path.Combine(patientId.ToString(), consultationId.ToString(), "Lab Results", uniqueName);
            }

            try
            {
                using (MySqlConnection conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE lab_results 
                                     SET test_name=@test, result_text=@text, result_file=@file, updated_at=NOW() 
                                     WHERE result_id=@id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedResultId);
                        cmd.Parameters.AddWithValue("@test", testName);
                        cmd.Parameters.AddWithValue("@text", resultText);
                        cmd.Parameters.AddWithValue("@file", relativePath != null ? (object)relativePath : DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }

                ClearForm();
                LoadLabResults();
                MessageBox.Show("Lab result updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating lab result: " + ex.Message);
            }
        }

        private void LabResultsForm_Load(object sender, EventArgs e)
        {
            ComboBoxCollectionHelper.PopulateComboBox(txtTestName, "lab_tests", "test_name");
            AutoCompleteHelper.SetupAutoComplete(txtTestName, "lab_tests", new List<string> { "test_name" });
            ComboBoxCollectionHelper.PopulateComboBox(txtResultText, "lab_results", "result_text");
            AutoCompleteHelper.SetupAutoComplete(txtResultText, "lab_results", new List<string> { "result_text" });
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (patientId <= 0 || consultationId <= 0)
                {
                    MessageBox.Show("⚠️ No patient or consultation selected.", "Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var manager = new WIA.DeviceManager();
                if (manager.DeviceInfos.Count == 0)
                {
                    MessageBox.Show("⚠️ No scanner detected.", "Scanner Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                WIA.CommonDialog dialog = new WIA.CommonDialog();
                WIA.ImageFile image = dialog.ShowAcquireImage(
                    WiaDeviceType.ScannerDeviceType,
                    WiaImageIntent.UnspecifiedIntent,
                    WiaImageBias.MaximizeQuality,
                    WiaFormatIDs.PNG,
                    true, true, false
                );

                if (image == null)
                {
                    MessageBox.Show("Scan cancelled.");
                    return;
                }

                string tempFolder = Path.Combine(basePath, "Temp");
                Directory.CreateDirectory(tempFolder);

                string fileName = $"{Guid.NewGuid():N}.png";
                string savedFilePath = Path.Combine(tempFolder, fileName);

                using (var stream = new MemoryStream((byte[])image.FileData.get_BinaryData()))
                using (Bitmap bmp = new Bitmap(stream))
                {
                    bmp.Save(savedFilePath, System.Drawing.Imaging.ImageFormat.Png);
                }

                attachedFilePath = savedFilePath;
                lblFileName.Text = Path.GetFileName(savedFilePath);

                // Load image into memory to avoid locking file
                PictureBox pb;
                using (var fs = new FileStream(savedFilePath, FileMode.Open, FileAccess.Read))
                {
                    Image img = Image.FromStream(fs);
                    pb = new PictureBox
                    {
                        Width = 150,
                        Height = 150,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Tag = savedFilePath,
                        BorderStyle = BorderStyle.FixedSingle,
                        Image = new Bitmap(img)
                    };
                }
                pb.Click += PreviewFile_Click;
                flpPreview.Controls.Add(pb);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Scanning failed: " + ex.Message);
            }
        }

        private void flpPreview_Paint(object sender, PaintEventArgs e) { }
    }
}

public static class WiaFormatIDs
{
    public const string BMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
    public const string PNG = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
    public const string GIF = "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}";
    public const string JPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
    public const string TIFF = "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}";
}
