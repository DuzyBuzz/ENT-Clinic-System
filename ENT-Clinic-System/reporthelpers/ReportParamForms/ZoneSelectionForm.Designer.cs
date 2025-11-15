using System.Windows.Forms;

namespace ENT_Clinic_System.ReportHelpers
{
    partial class ZoneSelectionForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox comboBoxZone;
        private Button btnOK;
        private Button btnCancel;
        private Label label1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.comboBoxZone = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxZone
            // 
            this.comboBoxZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxZone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxZone.FormattingEnabled = true;
            this.comboBoxZone.Location = new System.Drawing.Point(26, 52);
            this.comboBoxZone.Name = "comboBoxZone";
            this.comboBoxZone.Size = new System.Drawing.Size(215, 25);
            this.comboBoxZone.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnOK.Location = new System.Drawing.Point(26, 95);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 30);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(154, 95);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(53, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Zone to Generate";
            // 
            // ZoneSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 147);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.comboBoxZone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ZoneSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Zone";
            this.Load += new System.EventHandler(this.ZoneSelectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
