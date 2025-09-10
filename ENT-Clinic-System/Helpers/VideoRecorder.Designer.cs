namespace ENT_Clinic_System.Helpers
{
    partial class VideoRecorder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.ComboBox cameraComboBox;
        private System.Windows.Forms.Button startRecordingButton;
        private System.Windows.Forms.PictureBox livePreviewPictureBox; // NEW

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.cameraComboBox = new System.Windows.Forms.ComboBox();
            this.startRecordingButton = new System.Windows.Forms.Button();
            this.livePreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.recordCapturedFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.livePreviewPictureBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cameraComboBox
            // 
            this.cameraComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraComboBox.Location = new System.Drawing.Point(158, 3);
            this.cameraComboBox.Name = "cameraComboBox";
            this.cameraComboBox.Size = new System.Drawing.Size(374, 32);
            this.cameraComboBox.TabIndex = 0;
            // 
            // startRecordingButton
            // 
            this.startRecordingButton.Location = new System.Drawing.Point(561, 3);
            this.startRecordingButton.Name = "startRecordingButton";
            this.startRecordingButton.Size = new System.Drawing.Size(120, 30);
            this.startRecordingButton.TabIndex = 1;
            this.startRecordingButton.Text = "Start Recording";
            this.startRecordingButton.Click += new System.EventHandler(this.startRecordingButton_Click);
            // 
            // livePreviewPictureBox
            // 
            this.livePreviewPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.livePreviewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.livePreviewPictureBox.Location = new System.Drawing.Point(3, 51);
            this.livePreviewPictureBox.Name = "livePreviewPictureBox";
            this.livePreviewPictureBox.Size = new System.Drawing.Size(1882, 819);
            this.livePreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.livePreviewPictureBox.TabIndex = 2;
            this.livePreviewPictureBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.livePreviewPictureBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.recordCapturedFlowLayoutPanel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1888, 971);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.235919F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.41339F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.27099F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.startRecordingButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cameraComboBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1882, 42);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Device:";
            // 
            // recordCapturedFlowLayoutPanel
            // 
            this.recordCapturedFlowLayoutPanel.AutoScroll = true;
            this.recordCapturedFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recordCapturedFlowLayoutPanel.Location = new System.Drawing.Point(3, 876);
            this.recordCapturedFlowLayoutPanel.Name = "recordCapturedFlowLayoutPanel";
            this.recordCapturedFlowLayoutPanel.Size = new System.Drawing.Size(1882, 92);
            this.recordCapturedFlowLayoutPanel.TabIndex = 3;
            // 
            // VideoRecorder
            // 
            this.ClientSize = new System.Drawing.Size(1888, 971);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "VideoRecorder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Video Recorder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VideoRecorder_FormClosing);
            this.Load += new System.EventHandler(this.VideoRecorder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.livePreviewPictureBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel recordCapturedFlowLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
    }
}
