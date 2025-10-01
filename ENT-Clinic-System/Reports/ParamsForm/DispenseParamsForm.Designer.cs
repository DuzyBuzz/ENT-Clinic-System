using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Reports.ParamsForm
{
    partial class DispenseParamsForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbPatient;
        private ComboBox cmbCategory;
        private ComboBox cmbItemName;
        private ComboBox cmbDescription;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Label lblPatient;
        private Label lblCategory;
        private Label lblItemName;
        private Label lblDescription;
        private Label lblFrom;
        private Label lblTo;
        private Button btnGenerate;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbPatient = new System.Windows.Forms.ComboBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbItemName = new System.Windows.Forms.ComboBox();
            this.cmbDescription = new System.Windows.Forms.ComboBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblPatient = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbPatient
            // 
            this.cmbPatient.Location = new System.Drawing.Point(150, 17);
            this.cmbPatient.Name = "cmbPatient";
            this.cmbPatient.Size = new System.Drawing.Size(200, 21);
            this.cmbPatient.TabIndex = 1;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Location = new System.Drawing.Point(150, 57);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 21);
            this.cmbCategory.TabIndex = 3;
            // 
            // cmbItemName
            // 
            this.cmbItemName.Location = new System.Drawing.Point(150, 97);
            this.cmbItemName.Name = "cmbItemName";
            this.cmbItemName.Size = new System.Drawing.Size(200, 21);
            this.cmbItemName.TabIndex = 5;
            // 
            // cmbDescription
            // 
            this.cmbDescription.Location = new System.Drawing.Point(150, 137);
            this.cmbDescription.Name = "cmbDescription";
            this.cmbDescription.Size = new System.Drawing.Size(200, 21);
            this.cmbDescription.TabIndex = 7;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(150, 177);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpFrom.TabIndex = 9;
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(150, 217);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 20);
            this.dtpTo.TabIndex = 11;
            // 
            // lblPatient
            // 
            this.lblPatient.Location = new System.Drawing.Point(30, 20);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(100, 23);
            this.lblPatient.TabIndex = 0;
            this.lblPatient.Text = "Select Patient:";
            // 
            // lblCategory
            // 
            this.lblCategory.Location = new System.Drawing.Point(30, 60);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(100, 23);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Select Category:";
            // 
            // lblItemName
            // 
            this.lblItemName.Location = new System.Drawing.Point(30, 100);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(100, 23);
            this.lblItemName.TabIndex = 4;
            this.lblItemName.Text = "Select Item Name:";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(30, 140);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(100, 23);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "Select Description:";
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(30, 180);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(100, 23);
            this.lblFrom.TabIndex = 8;
            this.lblFrom.Text = "From Date:";
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(30, 220);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(100, 23);
            this.lblTo.TabIndex = 10;
            this.lblTo.Text = "To Date:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(150, 260);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 12;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(275, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.ForeColor = System.Drawing.Color.Gray;
            this.lblHint.Location = new System.Drawing.Point(147, 41);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(179, 13);
            this.lblHint.TabIndex = 14;
            this.lblHint.Text = "Leave blank or select \'All\' to fetch all";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(147, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Leave blank or select \'All\' to fetch all";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(147, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Leave blank or select \'All\' to fetch all";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(147, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Leave blank or select \'All\' to fetch all";
            // 
            // DispenseParamsForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 310);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.cmbPatient);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblItemName);
            this.Controls.Add(this.cmbItemName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.cmbDescription);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DispenseParamsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dispensing Report Parameters";
            this.Load += new System.EventHandler(this.DispenseParamsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label lblHint;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
