namespace ENT_Clinic_System.Files
{
    partial class MedicalCertificateList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pageLabel = new System.Windows.Forms.Label();
            this.nextButton = new System.Windows.Forms.Button();
            this.prevButton = new System.Windows.Forms.Button();
            this.refreshPatientsButton = new System.Windows.Forms.Button();
            this.searchPatientButton = new System.Windows.Forms.Button();
            this.searchPatientNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.patientsDataGridView = new System.Windows.Forms.DataGridView();
            this.medical_certificate_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.consultation_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.created_at = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patient_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diagnosis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recommendations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chief_complaint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.requester = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.patientsDataGridView, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1587, 1165);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel2.Controls.Add(this.pageLabel, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.nextButton, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.prevButton, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.refreshPatientsButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchPatientButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchPatientNameTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 6);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1579, 46);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // pageLabel
            // 
            this.pageLabel.AutoSize = true;
            this.pageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageLabel.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageLabel.Location = new System.Drawing.Point(1331, 0);
            this.pageLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Size = new System.Drawing.Size(121, 46);
            this.pageLabel.TabIndex = 9;
            this.pageLabel.Text = "Page";
            this.pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nextButton
            // 
            this.nextButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nextButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextButton.Location = new System.Drawing.Point(1456, 3);
            this.nextButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(121, 40);
            this.nextButton.TabIndex = 7;
            this.nextButton.Text = ">>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // prevButton
            // 
            this.prevButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prevButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prevButton.Location = new System.Drawing.Point(1206, 3);
            this.prevButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(121, 40);
            this.prevButton.TabIndex = 5;
            this.prevButton.Text = "<<";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
            // 
            // refreshPatientsButton
            // 
            this.refreshPatientsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.refreshPatientsButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshPatientsButton.Location = new System.Drawing.Point(502, 3);
            this.refreshPatientsButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.refreshPatientsButton.Name = "refreshPatientsButton";
            this.refreshPatientsButton.Size = new System.Drawing.Size(121, 40);
            this.refreshPatientsButton.TabIndex = 3;
            this.refreshPatientsButton.Text = "Refresh";
            this.refreshPatientsButton.UseVisualStyleBackColor = true;
            // 
            // searchPatientButton
            // 
            this.searchPatientButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPatientButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchPatientButton.Location = new System.Drawing.Point(377, 3);
            this.searchPatientButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.searchPatientButton.Name = "searchPatientButton";
            this.searchPatientButton.Size = new System.Drawing.Size(121, 40);
            this.searchPatientButton.TabIndex = 0;
            this.searchPatientButton.Text = "Search";
            this.searchPatientButton.UseVisualStyleBackColor = true;
            // 
            // searchPatientNameTextBox
            // 
            this.searchPatientNameTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchPatientNameTextBox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchPatientNameTextBox.Location = new System.Drawing.Point(127, 17);
            this.searchPatientNameTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.searchPatientNameTextBox.Name = "searchPatientNameTextBox";
            this.searchPatientNameTextBox.Size = new System.Drawing.Size(246, 26);
            this.searchPatientNameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search Patient:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // patientsDataGridView
            // 
            this.patientsDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.patientsDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.patientsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.patientsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.patientsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.patientsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.medical_certificate_id,
            this.consultation_id,
            this.created_at,
            this.patient_name,
            this.diagnosis,
            this.recommendations,
            this.chief_complaint,
            this.requester});
            this.patientsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientsDataGridView.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.patientsDataGridView.Location = new System.Drawing.Point(2, 61);
            this.patientsDataGridView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.patientsDataGridView.Name = "patientsDataGridView";
            this.patientsDataGridView.Size = new System.Drawing.Size(1583, 1101);
            this.patientsDataGridView.TabIndex = 1;
            // 
            // medical_certificate_id
            // 
            this.medical_certificate_id.DataPropertyName = "medical_certificate_id";
            this.medical_certificate_id.HeaderText = "medical_certificate_id";
            this.medical_certificate_id.Name = "medical_certificate_id";
            this.medical_certificate_id.Visible = false;
            // 
            // consultation_id
            // 
            this.consultation_id.DataPropertyName = "consultation_id";
            this.consultation_id.HeaderText = "consultation_id";
            this.consultation_id.Name = "consultation_id";
            this.consultation_id.Visible = false;
            // 
            // created_at
            // 
            this.created_at.DataPropertyName = "created_at";
            this.created_at.HeaderText = "Date";
            this.created_at.Name = "created_at";
            this.created_at.ReadOnly = true;
            // 
            // patient_name
            // 
            this.patient_name.DataPropertyName = "patient_name";
            this.patient_name.HeaderText = "Patient Name";
            this.patient_name.Name = "patient_name";
            this.patient_name.ReadOnly = true;
            // 
            // diagnosis
            // 
            this.diagnosis.DataPropertyName = "diagnosis";
            this.diagnosis.HeaderText = "Diagnosis";
            this.diagnosis.Name = "diagnosis";
            this.diagnosis.ReadOnly = true;
            // 
            // recommendations
            // 
            this.recommendations.DataPropertyName = "recommendations";
            this.recommendations.HeaderText = "Recommendations";
            this.recommendations.Name = "recommendations";
            this.recommendations.ReadOnly = true;
            // 
            // chief_complaint
            // 
            this.chief_complaint.DataPropertyName = "chief_complaint";
            this.chief_complaint.HeaderText = "Chief Complaint";
            this.chief_complaint.Name = "chief_complaint";
            this.chief_complaint.ReadOnly = true;
            // 
            // requester
            // 
            this.requester.DataPropertyName = "requester";
            this.requester.HeaderText = "Requester";
            this.requester.Name = "requester";
            // 
            // MedicalCertificateList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MedicalCertificateList";
            this.Size = new System.Drawing.Size(1587, 1165);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label pageLabel;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Button refreshPatientsButton;
        private System.Windows.Forms.Button searchPatientButton;
        private System.Windows.Forms.TextBox searchPatientNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView patientsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn medical_certificate_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn consultation_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn created_at;
        private System.Windows.Forms.DataGridViewTextBoxColumn patient_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn diagnosis;
        private System.Windows.Forms.DataGridViewTextBoxColumn recommendations;
        private System.Windows.Forms.DataGridViewTextBoxColumn chief_complaint;
        private System.Windows.Forms.DataGridViewTextBoxColumn requester;
    }
}
