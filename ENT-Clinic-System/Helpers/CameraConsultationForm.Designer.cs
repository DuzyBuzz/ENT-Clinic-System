namespace ENT_Clinic_System.UserControls
{
    partial class CameraConsultationForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel cameraPreviewPanel;
        private System.Windows.Forms.Button captureImageButton;
        private System.Windows.Forms.Button captureVideoButton;
        private System.Windows.Forms.ComboBox cameraComboBox;
        private System.Windows.Forms.Label selectCameraLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraConsultationForm));
            this.cameraPreviewPanel = new System.Windows.Forms.Panel();
            this.captureImageButton = new System.Windows.Forms.Button();
            this.captureVideoButton = new System.Windows.Forms.Button();
            this.imageVideoFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cameraComboBox = new System.Windows.Forms.ComboBox();
            this.selectCameraLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cameraPreviewPanel
            // 
            this.cameraPreviewPanel.BackColor = System.Drawing.Color.Black;
            this.cameraPreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cameraPreviewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraPreviewPanel.Location = new System.Drawing.Point(2, 2);
            this.cameraPreviewPanel.Margin = new System.Windows.Forms.Padding(2);
            this.cameraPreviewPanel.Name = "cameraPreviewPanel";
            this.cameraPreviewPanel.Size = new System.Drawing.Size(1083, 587);
            this.cameraPreviewPanel.TabIndex = 0;
            // 
            // captureImageButton
            // 
            this.captureImageButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captureImageButton.Location = new System.Drawing.Point(2, 2);
            this.captureImageButton.Margin = new System.Windows.Forms.Padding(2);
            this.captureImageButton.Name = "captureImageButton";
            this.captureImageButton.Size = new System.Drawing.Size(114, 29);
            this.captureImageButton.TabIndex = 1;
            this.captureImageButton.Text = "Capture Image";
            this.captureImageButton.UseVisualStyleBackColor = true;
            this.captureImageButton.Click += new System.EventHandler(this.captureImageButton_Click);
            // 
            // captureVideoButton
            // 
            this.captureVideoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.captureVideoButton.Location = new System.Drawing.Point(120, 2);
            this.captureVideoButton.Margin = new System.Windows.Forms.Padding(2);
            this.captureVideoButton.Name = "captureVideoButton";
            this.captureVideoButton.Size = new System.Drawing.Size(115, 29);
            this.captureVideoButton.TabIndex = 2;
            this.captureVideoButton.Text = "Record Video";
            this.captureVideoButton.UseVisualStyleBackColor = true;
            this.captureVideoButton.Click += new System.EventHandler(this.captureVideoButton_Click);
            // 
            // imageVideoFlowPanel
            // 
            this.imageVideoFlowPanel.AutoScroll = true;
            this.imageVideoFlowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageVideoFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageVideoFlowPanel.Location = new System.Drawing.Point(2, 632);
            this.imageVideoFlowPanel.Margin = new System.Windows.Forms.Padding(2);
            this.imageVideoFlowPanel.Name = "imageVideoFlowPanel";
            this.imageVideoFlowPanel.Size = new System.Drawing.Size(1083, 155);
            this.imageVideoFlowPanel.TabIndex = 3;
            // 
            // cameraComboBox
            // 
            this.cameraComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cameraComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cameraComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraComboBox.FormattingEnabled = true;
            this.cameraComboBox.Location = new System.Drawing.Point(322, 2);
            this.cameraComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.cameraComboBox.Name = "cameraComboBox";
            this.cameraComboBox.Size = new System.Drawing.Size(462, 26);
            this.cameraComboBox.TabIndex = 4;
            this.cameraComboBox.SelectedIndexChanged += new System.EventHandler(this.cameraComboBox_SelectedIndexChanged);
            // 
            // selectCameraLabel
            // 
            this.selectCameraLabel.AutoSize = true;
            this.selectCameraLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectCameraLabel.Location = new System.Drawing.Point(239, 0);
            this.selectCameraLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.selectCameraLabel.Name = "selectCameraLabel";
            this.selectCameraLabel.Size = new System.Drawing.Size(79, 33);
            this.selectCameraLabel.TabIndex = 5;
            this.selectCameraLabel.Text = "Select Camera:";
            this.selectCameraLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.00833F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.10083F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.770583F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.29325F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.10453F));
            this.tableLayoutPanel1.Controls.Add(this.captureImageButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cameraComboBox, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.selectCameraLabel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.captureVideoButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 594);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1081, 33);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.imageVideoFlowPanel, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cameraPreviewPanel, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1087, 789);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // CameraConsultationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 789);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CameraConsultationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consultation Camera ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraConsultationForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.FlowLayoutPanel imageVideoFlowPanel;
    }
}