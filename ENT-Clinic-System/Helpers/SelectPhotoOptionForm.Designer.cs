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
            this.btnCamera = new Button();
            this.btnFile = new Button();
            this.btnCancel = new Button();
            this.lblMessage = new Label();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(20, 20);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(200, 20);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "How would you like to add a photo?";
            // 
            // btnCamera
            // 
            this.btnCamera.Location = new System.Drawing.Point(25, 60);
            this.btnCamera.Name = "btnCamera";
            this.btnCamera.Size = new System.Drawing.Size(240, 35);
            this.btnCamera.TabIndex = 1;
            this.btnCamera.Text = "Capture from Camera";
            this.btnCamera.UseVisualStyleBackColor = true;
            this.btnCamera.Click += new System.EventHandler(this.btnCamera_Click);
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(25, 105);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(240, 35);
            this.btnFile.TabIndex = 2;
            this.btnFile.Text = "Upload from File";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(25, 150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(240, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SelectPhotoOptionForm
            // 
            this.AcceptButton = this.btnCamera;
            this.CancelButton = this.btnCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 210);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.btnCamera);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectPhotoOptionForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Select Photo Option";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
