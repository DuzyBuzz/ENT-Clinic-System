namespace ENT_Clinic_System.Admission
{
    partial class AdmittingOrderForm
 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

    /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
      private void InitializeComponent()
{
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.lblSex = new System.Windows.Forms.Label();
            this.txtSex = new System.Windows.Forms.TextBox();
            this.lblAdmitDate = new System.Windows.Forms.Label();
            this.dtAdmitDate = new System.Windows.Forms.DateTimePicker();
            this.lblRoomNote = new System.Windows.Forms.Label();
            this.lblCC = new System.Windows.Forms.Label();
            this.txtCC = new System.Windows.Forms.TextBox();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.lblDiet = new System.Windows.Forms.Label();
            this.cboDiet = new System.Windows.Forms.ComboBox();
            this.lblActivity = new System.Windows.Forms.Label();
            this.cboActivity = new System.Windows.Forms.ComboBox();
            this.lblIVFluids = new System.Windows.Forms.Label();
            this.txtIVFluids = new System.Windows.Forms.TextBox();
            this.lblLabs = new System.Windows.Forms.Label();
            this.txtLabs = new System.Windows.Forms.TextBox();
            this.lblMedications = new System.Windows.Forms.Label();
            this.txtMedications = new System.Windows.Forms.TextBox();
            this.lblImaging = new System.Windows.Forms.Label();
            this.txtImaging = new System.Windows.Forms.TextBox();
            this.lblNursing = new System.Windows.Forms.Label();
            this.txtNursing = new System.Windows.Forms.TextBox();
            this.lblSpecialOrders = new System.Windows.Forms.Label();
            this.txtSurgery = new System.Windows.Forms.TextBox();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cbmVitalSigns = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(245, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Admitting Order Form";
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Location = new System.Drawing.Point(20, 55);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(74, 13);
            this.lblPatientName.TabIndex = 1;
            this.lblPatientName.Text = "Patient Name:";
            // 
            // txtPatientName
            // 
            this.txtPatientName.Location = new System.Drawing.Point(150, 52);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.ReadOnly = true;
            this.txtPatientName.Size = new System.Drawing.Size(280, 20);
            this.txtPatientName.TabIndex = 2;
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(450, 55);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(29, 13);
            this.lblAge.TabIndex = 3;
            this.lblAge.Text = "Age:";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(490, 52);
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(60, 20);
            this.txtAge.TabIndex = 4;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(560, 55);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(28, 13);
            this.lblSex.TabIndex = 5;
            this.lblSex.Text = "Sex:";
            // 
            // txtSex
            // 
            this.txtSex.Location = new System.Drawing.Point(600, 52);
            this.txtSex.Name = "txtSex";
            this.txtSex.ReadOnly = true;
            this.txtSex.Size = new System.Drawing.Size(60, 20);
            this.txtSex.TabIndex = 6;
            // 
            // lblAdmitDate
            // 
            this.lblAdmitDate.AutoSize = true;
            this.lblAdmitDate.Location = new System.Drawing.Point(670, 55);
            this.lblAdmitDate.Name = "lblAdmitDate";
            this.lblAdmitDate.Size = new System.Drawing.Size(62, 13);
            this.lblAdmitDate.TabIndex = 7;
            this.lblAdmitDate.Text = "Admit Date:";
            // 
            // dtAdmitDate
            // 
            this.dtAdmitDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtAdmitDate.Location = new System.Drawing.Point(745, 52);
            this.dtAdmitDate.Name = "dtAdmitDate";
            this.dtAdmitDate.Size = new System.Drawing.Size(130, 20);
            this.dtAdmitDate.TabIndex = 8;
            // 
            // lblRoomNote
            // 
            this.lblRoomNote.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic);
            this.lblRoomNote.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblRoomNote.Location = new System.Drawing.Point(20, 80);
            this.lblRoomNote.Name = "lblRoomNote";
            this.lblRoomNote.Size = new System.Drawing.Size(855, 25);
            this.lblRoomNote.TabIndex = 9;
            this.lblRoomNote.Text = "Please admit to room of choice under my service. TPR q shift and record";
            // 
            // lblCC
            // 
            this.lblCC.AutoSize = true;
            this.lblCC.Location = new System.Drawing.Point(20, 115);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(83, 13);
            this.lblCC.TabIndex = 10;
            this.lblCC.Text = "Chief Complaint:";
            // 
            // txtCC
            // 
            this.txtCC.Location = new System.Drawing.Point(150, 112);
            this.txtCC.Multiline = true;
            this.txtCC.Name = "txtCC";
            this.txtCC.Size = new System.Drawing.Size(725, 40);
            this.txtCC.TabIndex = 11;
            // 
            // lblDiagnosis
            // 
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.Location = new System.Drawing.Point(20, 160);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Size = new System.Drawing.Size(60, 13);
            this.lblDiagnosis.TabIndex = 12;
            this.lblDiagnosis.Text = "Impression:";
            // 
            // txtDiagnosis
            // 
            this.txtDiagnosis.Location = new System.Drawing.Point(150, 157);
            this.txtDiagnosis.Multiline = true;
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(725, 40);
            this.txtDiagnosis.TabIndex = 13;
            // 
            // lblDiet
            // 
            this.lblDiet.AutoSize = true;
            this.lblDiet.Location = new System.Drawing.Point(20, 216);
            this.lblDiet.Name = "lblDiet";
            this.lblDiet.Size = new System.Drawing.Size(29, 13);
            this.lblDiet.TabIndex = 18;
            this.lblDiet.Text = "Diet:";
            // 
            // cboDiet
            // 
            this.cboDiet.FormattingEnabled = true;
            this.cboDiet.Items.AddRange(new object[] {
            "Regular",
            "NPO",
            "Clear Liquid",
            "Soft"});
            this.cboDiet.Location = new System.Drawing.Point(150, 213);
            this.cboDiet.Name = "cboDiet";
            this.cboDiet.Size = new System.Drawing.Size(150, 21);
            this.cboDiet.TabIndex = 19;
            // 
            // lblActivity
            // 
            this.lblActivity.AutoSize = true;
            this.lblActivity.Location = new System.Drawing.Point(310, 216);
            this.lblActivity.Name = "lblActivity";
            this.lblActivity.Size = new System.Drawing.Size(44, 13);
            this.lblActivity.TabIndex = 20;
            this.lblActivity.Text = "Activity:";
            // 
            // cboActivity
            // 
            this.cboActivity.FormattingEnabled = true;
            this.cboActivity.Items.AddRange(new object[] {
            "Bed rest",
            "Out of bed as tolerated",
            "Ambulate"});
            this.cboActivity.Location = new System.Drawing.Point(370, 213);
            this.cboActivity.Name = "cboActivity";
            this.cboActivity.Size = new System.Drawing.Size(230, 21);
            this.cboActivity.TabIndex = 21;
            // 
            // lblIVFluids
            // 
            this.lblIVFluids.AutoSize = true;
            this.lblIVFluids.Location = new System.Drawing.Point(20, 246);
            this.lblIVFluids.Name = "lblIVFluids";
            this.lblIVFluids.Size = new System.Drawing.Size(50, 13);
            this.lblIVFluids.TabIndex = 22;
            this.lblIVFluids.Text = "IV Fluids:";
            // 
            // txtIVFluids
            // 
            this.txtIVFluids.Location = new System.Drawing.Point(150, 243);
            this.txtIVFluids.Multiline = true;
            this.txtIVFluids.Name = "txtIVFluids";
            this.txtIVFluids.Size = new System.Drawing.Size(725, 35);
            this.txtIVFluids.TabIndex = 23;
            // 
            // lblLabs
            // 
            this.lblLabs.AutoSize = true;
            this.lblLabs.Location = new System.Drawing.Point(20, 286);
            this.lblLabs.Name = "lblLabs";
            this.lblLabs.Size = new System.Drawing.Size(33, 13);
            this.lblLabs.TabIndex = 24;
            this.lblLabs.Text = "Labs:";
            // 
            // txtLabs
            // 
            this.txtLabs.Location = new System.Drawing.Point(150, 283);
            this.txtLabs.Multiline = true;
            this.txtLabs.Name = "txtLabs";
            this.txtLabs.Size = new System.Drawing.Size(725, 35);
            this.txtLabs.TabIndex = 25;
            // 
            // lblMedications
            // 
            this.lblMedications.AutoSize = true;
            this.lblMedications.Location = new System.Drawing.Point(20, 326);
            this.lblMedications.Name = "lblMedications";
            this.lblMedications.Size = new System.Drawing.Size(67, 13);
            this.lblMedications.TabIndex = 26;
            this.lblMedications.Text = "Medications:";
            // 
            // txtMedications
            // 
            this.txtMedications.Location = new System.Drawing.Point(150, 323);
            this.txtMedications.Multiline = true;
            this.txtMedications.Name = "txtMedications";
            this.txtMedications.Size = new System.Drawing.Size(725, 35);
            this.txtMedications.TabIndex = 27;
            // 
            // lblImaging
            // 
            this.lblImaging.AutoSize = true;
            this.lblImaging.Location = new System.Drawing.Point(20, 366);
            this.lblImaging.Name = "lblImaging";
            this.lblImaging.Size = new System.Drawing.Size(47, 13);
            this.lblImaging.TabIndex = 28;
            this.lblImaging.Text = "Imaging:";
            // 
            // txtImaging
            // 
            this.txtImaging.Location = new System.Drawing.Point(150, 363);
            this.txtImaging.Multiline = true;
            this.txtImaging.Name = "txtImaging";
            this.txtImaging.Size = new System.Drawing.Size(725, 35);
            this.txtImaging.TabIndex = 29;
            // 
            // lblNursing
            // 
            this.lblNursing.AutoSize = true;
            this.lblNursing.Location = new System.Drawing.Point(20, 406);
            this.lblNursing.Name = "lblNursing";
            this.lblNursing.Size = new System.Drawing.Size(103, 13);
            this.lblNursing.TabIndex = 30;
            this.lblNursing.Text = "Nursing Instructions:";
            // 
            // txtNursing
            // 
            this.txtNursing.Location = new System.Drawing.Point(150, 403);
            this.txtNursing.Multiline = true;
            this.txtNursing.Name = "txtNursing";
            this.txtNursing.Size = new System.Drawing.Size(725, 35);
            this.txtNursing.TabIndex = 31;
            // 
            // lblSpecialOrders
            // 
            this.lblSpecialOrders.AutoSize = true;
            this.lblSpecialOrders.Location = new System.Drawing.Point(20, 446);
            this.lblSpecialOrders.Name = "lblSpecialOrders";
            this.lblSpecialOrders.Size = new System.Drawing.Size(79, 13);
            this.lblSpecialOrders.TabIndex = 32;
            this.lblSpecialOrders.Text = "Special Orders:";
            // 
            // txtSurgery
            // 
            this.txtSurgery.Location = new System.Drawing.Point(150, 443);
            this.txtSurgery.Multiline = true;
            this.txtSurgery.Name = "txtSurgery";
            this.txtSurgery.Size = new System.Drawing.Size(725, 45);
            this.txtSurgery.TabIndex = 33;
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
            this.dgvOrders.Location = new System.Drawing.Point(20, 558);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(855, 200);
            this.dgvOrders.TabIndex = 34;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(495, 503);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.TabIndex = 35;
            this.btnSave.Text = "Save Order";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(625, 503);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 35);
            this.btnClear.TabIndex = 36;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(755, 503);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 35);
            this.btnPrint.TabIndex = 37;
            this.btnPrint.Text = "Print Order";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // cbmVitalSigns
            // 
            this.cbmVitalSigns.FormattingEnabled = true;
            this.cbmVitalSigns.Items.AddRange(new object[] {
            "Bed rest",
            "Out of bed as tolerated",
            "Ambulate"});
            this.cbmVitalSigns.Location = new System.Drawing.Point(676, 213);
            this.cbmVitalSigns.Name = "cbmVitalSigns";
            this.cbmVitalSigns.Size = new System.Drawing.Size(199, 21);
            this.cbmVitalSigns.TabIndex = 39;
            this.cbmVitalSigns.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(616, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Vital Signs:";
            this.label1.Visible = false;
            // 
            // AdmittingOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(895, 770);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbmVitalSigns);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblPatientName);
            this.Controls.Add(this.txtPatientName);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.txtSex);
            this.Controls.Add(this.lblAdmitDate);
            this.Controls.Add(this.dtAdmitDate);
            this.Controls.Add(this.lblRoomNote);
            this.Controls.Add(this.lblCC);
            this.Controls.Add(this.txtCC);
            this.Controls.Add(this.lblDiagnosis);
            this.Controls.Add(this.txtDiagnosis);
            this.Controls.Add(this.lblDiet);
            this.Controls.Add(this.cboDiet);
            this.Controls.Add(this.lblActivity);
            this.Controls.Add(this.cboActivity);
            this.Controls.Add(this.lblIVFluids);
            this.Controls.Add(this.txtIVFluids);
            this.Controls.Add(this.lblLabs);
            this.Controls.Add(this.txtLabs);
            this.Controls.Add(this.lblMedications);
            this.Controls.Add(this.txtMedications);
            this.Controls.Add(this.lblImaging);
            this.Controls.Add(this.txtImaging);
            this.Controls.Add(this.lblNursing);
            this.Controls.Add(this.txtNursing);
            this.Controls.Add(this.lblSpecialOrders);
            this.Controls.Add(this.txtSurgery);
            this.Name = "AdmittingOrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admitting Order Form";
            this.Load += new System.EventHandler(this.AdmittingOrderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

     }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPatientName;
private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.Label lblAge;
   private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.TextBox txtSex;
      private System.Windows.Forms.Label lblAdmitDate;
   private System.Windows.Forms.DateTimePicker dtAdmitDate;
        private System.Windows.Forms.Label lblRoomNote;
        private System.Windows.Forms.Label lblCC;
        private System.Windows.Forms.TextBox txtCC;
        private System.Windows.Forms.Label lblDiagnosis;
    private System.Windows.Forms.TextBox txtDiagnosis;
      private System.Windows.Forms.Label lblDiet;
        private System.Windows.Forms.ComboBox cboDiet;
        private System.Windows.Forms.Label lblActivity;
     private System.Windows.Forms.ComboBox cboActivity;
        private System.Windows.Forms.Label lblIVFluids;
        private System.Windows.Forms.TextBox txtIVFluids;
    private System.Windows.Forms.Label lblLabs;
   private System.Windows.Forms.TextBox txtLabs;
        private System.Windows.Forms.Label lblMedications;
        private System.Windows.Forms.TextBox txtMedications;
   private System.Windows.Forms.Label lblImaging;
        private System.Windows.Forms.TextBox txtImaging;
        private System.Windows.Forms.Label lblNursing;
        private System.Windows.Forms.TextBox txtNursing;
        private System.Windows.Forms.Label lblSpecialOrders;
        private System.Windows.Forms.TextBox txtSurgery;
     private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Button btnSave;
   private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cbmVitalSigns;
        private System.Windows.Forms.Label label1;
    }
}
