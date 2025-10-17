using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    partial class SelectPhotoOptionForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnCamera;
        private Button btnFile;
        private Button btnCancel;
        private Label lblMessage;

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
            this.btnCamera = new System.Windows.Forms.Button();
            this.btnFile = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCamera
            // 
            this.btnCamera.Location = new System.Drawing.Point(19, 39);
            this.btnCamera.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCamera.Name = "btnCamera";
            this.btnCamera.Size = new System.Drawing.Size(180, 23);
            this.btnCamera.TabIndex = 1;
            this.btnCamera.Text = "Capture from Camera";
            this.btnCamera.UseVisualStyleBackColor = true;
            this.btnCamera.Click += new System.EventHandler(this.btnCamera_Click);
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(19, 68);
            this.btnFile.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(180, 23);
            this.btnFile.TabIndex = 2;
            this.btnFile.Text = "Upload from File";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(19, 98);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(15, 13);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(177, 13);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "How would you like to add a photo?";
            // 
            // SelectPhotoOptionForm
            // 
            this.AcceptButton = this.btnCamera;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(218, 136);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.btnCamera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectPhotoOptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Photo Option";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
