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
    public partial class LabResultsForm : Form
    {
        private int consultationId;
        private int patientId;
        private string attachedFilePath = "";
        private int selectedResultId = -1;

        public LabResultsForm(int consultationId, int patientId)
        {
            InitializeComponent();
            this.consultationId = consultationId;
            this.patientId = patientId;

            LoadLabResults();

            // Right-click delete
            dgvLabResults.ContextMenuStrip = cmsDelete;
            deleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
            dgvLabResults.CellContentClick += dgvLabResults_CellContentClick;


        }

        // Load lab results from database
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

        // Refresh FlowLayoutPanel previews
        private void RefreshPreviewPanel()
        {
            flpPreview.Controls.Clear();
            foreach (DataGridViewRow row in dgvLabResults.Rows)
            {
                string file = row.Cells["result_file"].Value != DBNull.Value
                    ? row.Cells["result_file"].Value.ToString()
                    : "";

                if (!string.IsNullOrEmpty(file) && File.Exists(file))
                {
                    PictureBox pb = new PictureBox();
                    pb.Width = 150;
                    pb.Height = 150;
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Tag = file;
                    pb.BorderStyle = BorderStyle.FixedSingle;

                    string ext = Path.GetExtension(file).ToLower();
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".tif" || ext == ".tiff")
                    {
                        pb.Image = System.Drawing.Image.FromFile(file);
                    }
                    else
                    {
                        // Use your PDF thumbnail from Assets
                        string pdfThumb = Path.Combine(Application.StartupPath, "assets", "images", "pdf.png");
                        if (File.Exists(pdfThumb))
                            pb.Image = System.Drawing.Image.FromFile(pdfThumb);
                    }

                    pb.Click += PreviewFile_Click;
                    flpPreview.Controls.Add(pb);
                }
            }
        }

        // Open file on click
        private void PreviewFile_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb != null)
            {
                string file = pb.Tag.ToString();
                if (File.Exists(file))
                {
                    Process.Start(file);
                }
            }
        }

        // Attach file button click
        private void btnAttachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select Lab Result File";
            dlg.Filter = "All Files|*.*|PDF|*.pdf|Images|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff";
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                attachedFilePath = dlg.FileName;
                lblFileName.Text = Path.GetFileName(attachedFilePath);
            }
        }

        // Add new lab result
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            string testName = txtTestName.Text.Trim();
            string resultText = txtResultText.Text.Trim();

            if (string.IsNullOrEmpty(testName))
            {
                MessageBox.Show("Test name is required. Please enter the test name before adding.",
                                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTestName.Focus();
                return;
            }

            try
            {
                progressBar.Visible = true;
                lblStatus.Visible = true;
                lblStatus.Text = "Uploading file...";

                string savedFile = null;

                if (!string.IsNullOrEmpty(attachedFilePath))
                {
                    string baseFolder = Path.Combine(@"D:\ENT_CLINIC_Attachments", patientId.ToString(), consultationId.ToString());
                    if (!Directory.Exists(baseFolder))
                        Directory.CreateDirectory(baseFolder);

                    string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(attachedFilePath);
                    savedFile = Path.Combine(baseFolder, uniqueName);

                    await Task.Run(() =>
                    {
                        const int bufferSize = 1024 * 1024; // 1 MB
                        byte[] buffer = new byte[bufferSize];
                        using (FileStream source = new FileStream(attachedFilePath, FileMode.Open, FileAccess.Read))
                        using (FileStream dest = new FileStream(savedFile, FileMode.Create, FileAccess.Write))
                        {
                            long totalBytes = source.Length;
                            long bytesCopied = 0;
                            int bytesRead;
                            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                dest.Write(buffer, 0, bytesRead);
                                bytesCopied += bytesRead;
                                int percent = (int)((bytesCopied * 100) / totalBytes);
                                this.Invoke((Action)(() => progressBar.Value = percent));
                            }
                        }
                    });
                }

                lblStatus.Text = "Saving to database...";

                // Insert into DB
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
                            cmd.Parameters.AddWithValue("@file", savedFile != null ? (object)savedFile : DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }
                });

                progressBar.Value = 0;
                progressBar.Visible = false;
                lblStatus.Text = "Laboratory result uploaded successfully!";
                await Task.Delay(1500);
                lblStatus.Visible = false;

                ClearForm();
                LoadLabResults();
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                lblStatus.Visible = false;
                MessageBox.Show("Error adding Laboratory result: " + ex.Message,
                                "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete via right-click
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLabResults.CurrentRow == null) return;

            int resultId = Convert.ToInt32(dgvLabResults.CurrentRow.Cells["result_id"].Value);
            string filePath = dgvLabResults.CurrentRow.Cells["result_file"].Value != DBNull.Value
                ? dgvLabResults.CurrentRow.Cells["result_file"].Value.ToString()
                : "";

            if (MessageBox.Show("Delete this lab result?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Delete record from database
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

                    // Delete the attached file if it exists
                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting lab result: " + ex.Message);
                }

                LoadLabResults(); // Refresh the grid and preview
            }
        }

        // Clear form inputs
        private void ClearForm()
        {
            txtTestName.Text = string.Empty;
            txtResultText.Text = string.Empty;
            lblFileName.Text = "";
            attachedFilePath = "";
            selectedResultId = -1;
        }
        private void dgvLabResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvLabResults.Columns[e.ColumnIndex].Name == "result_file")
            {
                string filePath = dgvLabResults.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    // Open File Explorer and select the file
                    Process.Start("explorer.exe", $"/select,\"{filePath}\"");
                }
                else
                {
                    MessageBox.Show("File does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        // Select row for update
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

        // Update existing lab result
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

            string savedFile = null;
            if (!string.IsNullOrEmpty(attachedFilePath))
            {
                string baseFolder = Path.Combine(@"D:\ENT_CLINIC_Attachments", patientId.ToString(), consultationId.ToString(), "Lab Results");
                if (!Directory.Exists(baseFolder))
                    Directory.CreateDirectory(baseFolder);

                string uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(attachedFilePath);
                savedFile = Path.Combine(baseFolder, uniqueName);
                File.Copy(attachedFilePath, savedFile, true);
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
                        cmd.Parameters.AddWithValue("@file", savedFile != null ? (object)savedFile : DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating lab result: " + ex.Message);
                return;
            }

            ClearForm();
            LoadLabResults();
            MessageBox.Show("Lab result updated successfully.");
        }

        private void LabResultsForm_Load(object sender, EventArgs e)
        {
            ComboBoxCollectionHelper.PopulateComboBox(
                txtTestName,
                "lab_tests",
                "test_name"
            );
            AutoCompleteHelper.SetupAutoComplete(
                txtTestName,
                "lab_tests",
                new List<string> { "test_name" }
            );
            ComboBoxCollectionHelper.PopulateComboBox(
                txtResultText,
                "lab_results",
                "result_text"
            );
            AutoCompleteHelper.SetupAutoComplete(
                txtResultText,
                "lab_results",
                new List<string> { "result_text" }
            );
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            try
            {
                // 🔒 Make sure patient & consultation are valid
                if (patientId <= 0 || consultationId <= 0)
                {
                    MessageBox.Show("⚠️ No patient or consultation selected.", "Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Initialize WIA Device Manager
                var manager = new WIA.DeviceManager();
                if (manager.DeviceInfos.Count == 0)
                {
                    MessageBox.Show("⚠️ No scanner detected. Please connect a scanner.", "Scanner Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Let user pick scanner and scan
                WIA.CommonDialog dialog = new WIA.CommonDialog();
                WIA.ImageFile image = null;

                try
                {
                    image = dialog.ShowAcquireImage(
                        WiaDeviceType.ScannerDeviceType,
                        WiaImageIntent.UnspecifiedIntent,
                        WiaImageBias.MaximizeQuality,
                        WiaFormatIDs.PNG,// output as PNG
                        true,  // show scanner UI
                        true,  // allow preview
                        false  // single page
                    );
                }
                catch (System.Runtime.InteropServices.COMException comEx)
                {
                    MessageBox.Show($"Scanning failed: {comEx.Message}", "Scan Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (image == null)
                {
                    MessageBox.Show("Scan cancelled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // ✅ Prepare save folder
                string baseFolder = Path.Combine(@"D:\ENT_CLINIC_Attachments", patientId.ToString(), consultationId.ToString(), "Lab Results");
                if (!Directory.Exists(baseFolder))
                    Directory.CreateDirectory(baseFolder);

                // ✅ Save scanned image as PNG
                string fileName = $"{Guid.NewGuid():N}.png";
                string savedFilePath = Path.Combine(baseFolder, fileName);

                using (var stream = new MemoryStream((byte[])image.FileData.get_BinaryData()))
                using (Bitmap bmp = new Bitmap(stream))
                {
                    bmp.Save(savedFilePath, System.Drawing.Imaging.ImageFormat.Png);
                }

                // ✅ Show scanned image immediately in flpPreview
                PictureBox pb = new PictureBox
                {
                    Width = 150,
                    Height = 150,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Tag = savedFilePath,
                    BorderStyle = BorderStyle.FixedSingle,
                    Image = System.Drawing.Image.FromFile(savedFilePath)
                };

                pb.Click += PreviewFile_Click; // reuse your existing click handler
                flpPreview.Controls.Add(pb);

            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Scanning failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void flpPreview_Paint(object sender, PaintEventArgs e)
        {

        }
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