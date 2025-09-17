namespace ENT_Clinic_System.Helpers
{
    partial class PatientCamera
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.comboBoxCameras = new System.Windows.Forms.ComboBox();
            this.pictureBoxLive = new System.Windows.Forms.PictureBox();
            this.pictureBoxCaptured = new System.Windows.Forms.PictureBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptured)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxCameras
            // 
            this.comboBoxCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCameras.FormattingEnabled = true;
            this.comboBoxCameras.Location = new System.Drawing.Point(9, 10);
            this.comboBoxCameras.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBoxCameras.Name = "comboBoxCameras";
            this.comboBoxCameras.Size = new System.Drawing.Size(226, 21);
            this.comboBoxCameras.TabIndex = 0;
            this.comboBoxCameras.SelectedIndexChanged += new System.EventHandler(this.comboBoxCameras_SelectedIndexChanged);
            // 
            // pictureBoxLive
            // 
            this.pictureBoxLive.BackColor = System.Drawing.Color.Black;
            this.pictureBoxLive.Location = new System.Drawing.Point(9, 41);
            this.pictureBoxLive.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxLive.Name = "pictureBoxLive";
            this.pictureBoxLive.Size = new System.Drawing.Size(300, 244);
            this.pictureBoxLive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLive.TabIndex = 1;
            this.pictureBoxLive.TabStop = false;
            // 
            // pictureBoxCaptured
            // 
            this.pictureBoxCaptured.BackColor = System.Drawing.Color.Gray;
            this.pictureBoxCaptured.Location = new System.Drawing.Point(322, 41);
            this.pictureBoxCaptured.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxCaptured.Name = "pictureBoxCaptured";
            this.pictureBoxCaptured.Size = new System.Drawing.Size(144, 156);
            this.pictureBoxCaptured.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCaptured.TabIndex = 2;
            this.pictureBoxCaptured.TabStop = false;
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(322, 211);
            this.btnCapture.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(68, 32);
            this.btnCapture.TabIndex = 3;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(399, 211);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(68, 32);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // PatientCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 301);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.pictureBoxCaptured);
            this.Controls.Add(this.pictureBoxLive);
            this.Controls.Add(this.comboBoxCameras);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatientCamera";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Patient Camera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatientCamera_FormClosing);
            this.Load += new System.EventHandler(this.PatientCamera_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptured)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCameras;
        private System.Windows.Forms.PictureBox pictureBoxLive;
        private System.Windows.Forms.PictureBox pictureBoxCaptured;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnConfirm;
    }
}
