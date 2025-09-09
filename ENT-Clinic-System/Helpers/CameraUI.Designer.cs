namespace ENT_Clinic_System.CustomUI
{
    partial class CameraUI
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox previewBox;
        private System.Windows.Forms.ComboBox cameraComboBox;
        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.FlowLayoutPanel capturedImagesPanel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label cameraLabel;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.cameraLabel = new System.Windows.Forms.Label();
            this.cameraComboBox = new System.Windows.Forms.ComboBox();
            this.captureButton = new System.Windows.Forms.Button();
            this.capturedImagesPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // previewBox
            // 
            this.previewBox.BackColor = System.Drawing.Color.Black;
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.previewBox.Location = new System.Drawing.Point(0, 0);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(1888, 665);
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.previewBox.TabIndex = 0;
            this.previewBox.TabStop = false;
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.topPanel.Controls.Add(this.cameraLabel);
            this.topPanel.Controls.Add(this.cameraComboBox);
            this.topPanel.Controls.Add(this.captureButton);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 665);
            this.topPanel.Name = "topPanel";
            this.topPanel.Padding = new System.Windows.Forms.Padding(10);
            this.topPanel.Size = new System.Drawing.Size(1888, 50);
            this.topPanel.TabIndex = 1;
            // 
            // cameraLabel
            // 
            this.cameraLabel.AutoSize = true;
            this.cameraLabel.Location = new System.Drawing.Point(10, 15);
            this.cameraLabel.Name = "cameraLabel";
            this.cameraLabel.Size = new System.Drawing.Size(79, 13);
            this.cameraLabel.TabIndex = 0;
            this.cameraLabel.Text = "Select Camera:";
            // 
            // cameraComboBox
            // 
            this.cameraComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cameraComboBox.Location = new System.Drawing.Point(100, 12);
            this.cameraComboBox.Name = "cameraComboBox";
            this.cameraComboBox.Size = new System.Drawing.Size(250, 21);
            this.cameraComboBox.TabIndex = 1;
            // 
            // captureButton
            // 
            this.captureButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.captureButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.captureButton.ForeColor = System.Drawing.Color.White;
            this.captureButton.Location = new System.Drawing.Point(400, 10);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(100, 30);
            this.captureButton.TabIndex = 2;
            this.captureButton.Text = "Capture";
            this.captureButton.UseVisualStyleBackColor = false;
            // 
            // capturedImagesPanel
            // 
            this.capturedImagesPanel.AutoScroll = true;
            this.capturedImagesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.capturedImagesPanel.Location = new System.Drawing.Point(0, 715);
            this.capturedImagesPanel.Name = "capturedImagesPanel";
            this.capturedImagesPanel.Padding = new System.Windows.Forms.Padding(10);
            this.capturedImagesPanel.Size = new System.Drawing.Size(1888, 256);
            this.capturedImagesPanel.TabIndex = 2;
            // 
            // CameraUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1888, 971);
            this.Controls.Add(this.capturedImagesPanel);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.previewBox);
            this.Name = "CameraUI";
            this.Text = "Camera Capture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraUI_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
