namespace ENT_Clinic_System.UserControls
{
    partial class CameraConsultationForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel cameraPreviewPanel;
        private System.Windows.Forms.Button captureImageButton;
        private System.Windows.Forms.Button captureVideoButton;
        private System.Windows.Forms.FlowLayoutPanel imageVideoFlowPanel;
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
            this.cameraPreviewPanel = new System.Windows.Forms.Panel();
            this.captureImageButton = new System.Windows.Forms.Button();
            this.captureVideoButton = new System.Windows.Forms.Button();
            this.imageVideoFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cameraComboBox = new System.Windows.Forms.ComboBox();
            this.selectCameraLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cameraPreviewPanel
            // 
            this.cameraPreviewPanel.BackColor = System.Drawing.Color.Black;
            this.cameraPreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cameraPreviewPanel.Location = new System.Drawing.Point(12, 12);
            this.cameraPreviewPanel.Name = "cameraPreviewPanel";
            this.cameraPreviewPanel.Size = new System.Drawing.Size(640, 360);
            this.cameraPreviewPanel.TabIndex = 0;
            // 
            // captureImageButton
            // 
            this.captureImageButton.Location = new System.Drawing.Point(12, 380);
            this.captureImageButton.Name = "captureImageButton";
            this.captureImageButton.Size = new System.Drawing.Size(150, 40);
            this.captureImageButton.TabIndex = 1;
            this.captureImageButton.Text = "Capture Image";
            this.captureImageButton.UseVisualStyleBackColor = true;
            // 
            // captureVideoButton
            // 
            this.captureVideoButton.Location = new System.Drawing.Point(180, 380);
            this.captureVideoButton.Name = "captureVideoButton";
            this.captureVideoButton.Size = new System.Drawing.Size(150, 40);
            this.captureVideoButton.TabIndex = 2;
            this.captureVideoButton.Text = "Record Video";
            this.captureVideoButton.UseVisualStyleBackColor = true;
            // 
            // imageVideoFlowPanel
            // 
            this.imageVideoFlowPanel.AutoScroll = true;
            this.imageVideoFlowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageVideoFlowPanel.Location = new System.Drawing.Point(12, 430);
            this.imageVideoFlowPanel.Name = "imageVideoFlowPanel";
            this.imageVideoFlowPanel.Size = new System.Drawing.Size(640, 200);
            this.imageVideoFlowPanel.TabIndex = 3;
            // 
            // cameraComboBox
            // 
            this.cameraComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cameraComboBox.FormattingEnabled = true;
            this.cameraComboBox.Location = new System.Drawing.Point(540, 392);
            this.cameraComboBox.Name = "cameraComboBox";
            this.cameraComboBox.Size = new System.Drawing.Size(112, 28);
            this.cameraComboBox.TabIndex = 4;
            // 
            // selectCameraLabel
            // 
            this.selectCameraLabel.AutoSize = true;
            this.selectCameraLabel.Location = new System.Drawing.Point(407, 400);
            this.selectCameraLabel.Name = "selectCameraLabel";
            this.selectCameraLabel.Size = new System.Drawing.Size(118, 20);
            this.selectCameraLabel.TabIndex = 5;
            this.selectCameraLabel.Text = "Select Camera:";
            // 
            // CameraConsultationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 642);
            this.Controls.Add(this.selectCameraLabel);
            this.Controls.Add(this.cameraComboBox);
            this.Controls.Add(this.imageVideoFlowPanel);
            this.Controls.Add(this.captureVideoButton);
            this.Controls.Add(this.captureImageButton);
            this.Controls.Add(this.cameraPreviewPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CameraConsultationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Camera Consultation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
