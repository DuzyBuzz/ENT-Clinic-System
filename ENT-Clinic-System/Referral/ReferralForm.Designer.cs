namespace ENT_Clinic_System.Referral
{
    partial class ReferralForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.lblSex = new System.Windows.Forms.Label();
            this.txtSex = new System.Windows.Forms.TextBox();
            this.lblAdmitDate = new System.Windows.Forms.Label();
            this.dtAdmitDate = new System.Windows.Forms.DateTimePicker();
            this.lblRefDoctor = new System.Windows.Forms.Label();
            this.grpEvaluation = new System.Windows.Forms.GroupBox();
            this.chkCoManagement = new System.Windows.Forms.CheckBox();
            this.chkEmergency = new System.Windows.Forms.CheckBox();
            this.chkEvalMgmt = new System.Windows.Forms.CheckBox();
            this.chkPreOp = new System.Windows.Forms.CheckBox();
            this.lblWorkingImp = new System.Windows.Forms.Label();
            this.txtWorkingImp = new System.Windows.Forms.TextBox();
            this.lblAdditionalInfo = new System.Windows.Forms.Label();
            this.txtAdditionalInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.planTextBox = new System.Windows.Forms.TextBox();
            this.cmbReferingDoctor = new System.Windows.Forms.ComboBox();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.grpEvaluation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(645, 567);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 19;
            this.btnPrint.Text = "Print";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(535, 567);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 30);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(425, 567);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(322, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(153, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Referral Form";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Location = new System.Drawing.Point(322, 65);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(74, 13);
            this.lblPatientName.TabIndex = 1;
            this.lblPatientName.Text = "Patient Name:";
            // 
            // txtPatientName
            // 
            this.txtPatientName.Location = new System.Drawing.Point(430, 62);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(312, 20);
            this.txtPatientName.TabIndex = 2;
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(427, 103);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(29, 13);
            this.lblAge.TabIndex = 3;
            this.lblAge.Text = "Age:";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(467, 100);
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(50, 20);
            this.txtAge.TabIndex = 4;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(537, 103);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(28, 13);
            this.lblSex.TabIndex = 5;
            this.lblSex.Text = "Sex:";
            // 
            // txtSex
            // 
            this.txtSex.Location = new System.Drawing.Point(577, 100);
            this.txtSex.Name = "txtSex";
            this.txtSex.ReadOnly = true;
            this.txtSex.Size = new System.Drawing.Size(50, 20);
            this.txtSex.TabIndex = 6;
            // 
            // lblAdmitDate
            // 
            this.lblAdmitDate.AutoSize = true;
            this.lblAdmitDate.Location = new System.Drawing.Point(864, 64);
            this.lblAdmitDate.Name = "lblAdmitDate";
            this.lblAdmitDate.Size = new System.Drawing.Size(62, 13);
            this.lblAdmitDate.TabIndex = 7;
            this.lblAdmitDate.Text = "Admit Date:";
            // 
            // dtAdmitDate
            // 
            this.dtAdmitDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtAdmitDate.Location = new System.Drawing.Point(944, 61);
            this.dtAdmitDate.Name = "dtAdmitDate";
            this.dtAdmitDate.Size = new System.Drawing.Size(120, 20);
            this.dtAdmitDate.TabIndex = 8;
            // 
            // lblRefDoctor
            // 
            this.lblRefDoctor.AutoSize = true;
            this.lblRefDoctor.Location = new System.Drawing.Point(322, 141);
            this.lblRefDoctor.Name = "lblRefDoctor";
            this.lblRefDoctor.Size = new System.Drawing.Size(88, 13);
            this.lblRefDoctor.TabIndex = 9;
            this.lblRefDoctor.Text = "Referring Doctor:";
            // 
            // grpEvaluation
            // 
            this.grpEvaluation.Controls.Add(this.chkCoManagement);
            this.grpEvaluation.Controls.Add(this.chkEmergency);
            this.grpEvaluation.Controls.Add(this.chkEvalMgmt);
            this.grpEvaluation.Controls.Add(this.chkPreOp);
            this.grpEvaluation.Location = new System.Drawing.Point(322, 176);
            this.grpEvaluation.Name = "grpEvaluation";
            this.grpEvaluation.Size = new System.Drawing.Size(423, 72);
            this.grpEvaluation.TabIndex = 11;
            this.grpEvaluation.TabStop = false;
            // 
            // chkCoManagement
            // 
            this.chkCoManagement.Location = new System.Drawing.Point(73, 41);
            this.chkCoManagement.Name = "chkCoManagement";
            this.chkCoManagement.Size = new System.Drawing.Size(104, 24);
            this.chkCoManagement.TabIndex = 0;
            this.chkCoManagement.Text = "Co-Management";
            // 
            // chkEmergency
            // 
            this.chkEmergency.Location = new System.Drawing.Point(255, 41);
            this.chkEmergency.Name = "chkEmergency";
            this.chkEmergency.Size = new System.Drawing.Size(104, 24);
            this.chkEmergency.TabIndex = 1;
            this.chkEmergency.Text = "Emergency";
            // 
            // chkEvalMgmt
            // 
            this.chkEvalMgmt.Location = new System.Drawing.Point(73, 19);
            this.chkEvalMgmt.Name = "chkEvalMgmt";
            this.chkEvalMgmt.Size = new System.Drawing.Size(104, 24);
            this.chkEvalMgmt.TabIndex = 0;
            this.chkEvalMgmt.Text = "Evaluation & Management";
            // 
            // chkPreOp
            // 
            this.chkPreOp.Location = new System.Drawing.Point(255, 19);
            this.chkPreOp.Name = "chkPreOp";
            this.chkPreOp.Size = new System.Drawing.Size(104, 24);
            this.chkPreOp.TabIndex = 1;
            this.chkPreOp.Text = "Pre-Op Risk Assessment";
            // 
            // lblWorkingImp
            // 
            this.lblWorkingImp.AutoSize = true;
            this.lblWorkingImp.Location = new System.Drawing.Point(322, 251);
            this.lblWorkingImp.Name = "lblWorkingImp";
            this.lblWorkingImp.Size = new System.Drawing.Size(142, 13);
            this.lblWorkingImp.TabIndex = 12;
            this.lblWorkingImp.Text = "Present Working Impression:";
            // 
            // txtWorkingImp
            // 
            this.txtWorkingImp.Location = new System.Drawing.Point(325, 271);
            this.txtWorkingImp.Multiline = true;
            this.txtWorkingImp.Name = "txtWorkingImp";
            this.txtWorkingImp.Size = new System.Drawing.Size(420, 60);
            this.txtWorkingImp.TabIndex = 13;
            // 
            // lblAdditionalInfo
            // 
            this.lblAdditionalInfo.AutoSize = true;
            this.lblAdditionalInfo.Location = new System.Drawing.Point(322, 461);
            this.lblAdditionalInfo.Name = "lblAdditionalInfo";
            this.lblAdditionalInfo.Size = new System.Drawing.Size(111, 13);
            this.lblAdditionalInfo.TabIndex = 15;
            this.lblAdditionalInfo.Text = "Additional Information:";
            // 
            // txtAdditionalInfo
            // 
            this.txtAdditionalInfo.Location = new System.Drawing.Point(325, 481);
            this.txtAdditionalInfo.Multiline = true;
            this.txtAdditionalInfo.Name = "txtAdditionalInfo";
            this.txtAdditionalInfo.Size = new System.Drawing.Size(420, 60);
            this.txtAdditionalInfo.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(319, 353);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Plan:";
            // 
            // planTextBox
            // 
            this.planTextBox.Location = new System.Drawing.Point(322, 373);
            this.planTextBox.Multiline = true;
            this.planTextBox.Name = "planTextBox";
            this.planTextBox.Size = new System.Drawing.Size(420, 60);
            this.planTextBox.TabIndex = 21;
            // 
            // cmbReferingDoctor
            // 
            this.cmbReferingDoctor.FormattingEnabled = true;
            this.cmbReferingDoctor.Items.AddRange(new object[] {
            "Regular",
            "NPO",
            "Clear Liquid",
            "Soft"});
            this.cmbReferingDoctor.Location = new System.Drawing.Point(430, 138);
            this.cmbReferingDoctor.Name = "cmbReferingDoctor";
            this.cmbReferingDoctor.Size = new System.Drawing.Size(312, 21);
            this.cmbReferingDoctor.TabIndex = 22;
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(18, 20);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(259, 577);
            this.dgvOrders.TabIndex = 35;
            // 
            // ReferralForm
            // 
            this.ClientSize = new System.Drawing.Size(791, 631);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.cmbReferingDoctor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.planTextBox);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblPatientName);
            this.Controls.Add(this.txtPatientName);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.txtSex);
            this.Controls.Add(this.lblAdmitDate);
            this.Controls.Add(this.dtAdmitDate);
            this.Controls.Add(this.lblRefDoctor);
            this.Controls.Add(this.grpEvaluation);
            this.Controls.Add(this.lblWorkingImp);
            this.Controls.Add(this.txtWorkingImp);
            this.Controls.Add(this.lblAdditionalInfo);
            this.Controls.Add(this.txtAdditionalInfo);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPrint);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ReferralForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Referral Form";
            this.Load += new System.EventHandler(this.ReferralForm_Load);
            this.grpEvaluation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.TextBox txtSex;
        private System.Windows.Forms.Label lblAdmitDate;
        private System.Windows.Forms.DateTimePicker dtAdmitDate;

        private System.Windows.Forms.Label lblRefDoctor;

        private System.Windows.Forms.GroupBox grpEvaluation;
        private System.Windows.Forms.CheckBox chkEvalMgmt;
        private System.Windows.Forms.CheckBox chkPreOp;

        private System.Windows.Forms.Label lblWorkingImp;
        private System.Windows.Forms.TextBox txtWorkingImp;
        private System.Windows.Forms.CheckBox chkCoManagement;
        private System.Windows.Forms.CheckBox chkEmergency;

        private System.Windows.Forms.Label lblAdditionalInfo;
        private System.Windows.Forms.TextBox txtAdditionalInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox planTextBox;
        private System.Windows.Forms.ComboBox cmbReferingDoctor;
        private System.Windows.Forms.DataGridView dgvOrders;
    }
}
