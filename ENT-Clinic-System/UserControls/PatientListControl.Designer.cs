namespace ENT_Clinic_System.UserControls
{
    partial class PatientListControl
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
            this.components = new System.ComponentModel.Container();
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
            this.patientsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewConsultationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patient_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.full_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressColumnTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birth_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.civil_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patient_contact_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emergency_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emergency_contact_number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emergency_relationship = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.photo = new System.Windows.Forms.DataGridViewImageColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGridView)).BeginInit();
            this.patientsContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.patientsDataGridView, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.742574F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.25742F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1904, 1010);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.866948F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.49525F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.270327F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.587117F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.8226F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.969705F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.939409F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.969705F));
            this.tableLayoutPanel2.Controls.Add(this.pageLabel, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.nextButton, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.prevButton, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.refreshPatientsButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchPatientButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchPatientNameTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1894, 48);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // pageLabel
            // 
            this.pageLabel.AutoSize = true;
            this.pageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageLabel.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageLabel.Location = new System.Drawing.Point(1613, 0);
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Size = new System.Drawing.Size(182, 48);
            this.pageLabel.TabIndex = 9;
            this.pageLabel.Text = "Page";
            this.pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nextButton
            // 
            this.nextButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.nextButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextButton.Location = new System.Drawing.Point(1801, 10);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(90, 35);
            this.nextButton.TabIndex = 7;
            this.nextButton.Text = ">>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // prevButton
            // 
            this.prevButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.prevButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prevButton.Location = new System.Drawing.Point(1519, 10);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(88, 35);
            this.prevButton.TabIndex = 5;
            this.prevButton.Text = "<<";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.prevButton_Click);
            // 
            // refreshPatientsButton
            // 
            this.refreshPatientsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.refreshPatientsButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshPatientsButton.Location = new System.Drawing.Point(640, 10);
            this.refreshPatientsButton.Name = "refreshPatientsButton";
            this.refreshPatientsButton.Size = new System.Drawing.Size(43, 35);
            this.refreshPatientsButton.TabIndex = 3;
            this.refreshPatientsButton.Text = "⟳";
            this.refreshPatientsButton.UseVisualStyleBackColor = true;
            this.refreshPatientsButton.Click += new System.EventHandler(this.refreshPatientsButton_Click);
            // 
            // searchPatientButton
            // 
            this.searchPatientButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchPatientButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchPatientButton.Location = new System.Drawing.Point(597, 10);
            this.searchPatientButton.Name = "searchPatientButton";
            this.searchPatientButton.Size = new System.Drawing.Size(37, 35);
            this.searchPatientButton.TabIndex = 0;
            this.searchPatientButton.Text = "🔎";
            this.searchPatientButton.UseVisualStyleBackColor = true;
            this.searchPatientButton.Click += new System.EventHandler(this.searchPatientButton_Click);
            // 
            // searchPatientNameTextBox
            // 
            this.searchPatientNameTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchPatientNameTextBox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchPatientNameTextBox.Location = new System.Drawing.Point(152, 19);
            this.searchPatientNameTextBox.Name = "searchPatientNameTextBox";
            this.searchPatientNameTextBox.Size = new System.Drawing.Size(439, 26);
            this.searchPatientNameTextBox.TabIndex = 1;
            this.searchPatientNameTextBox.TextChanged += new System.EventHandler(this.searchPatientNameTextBox_TextChanged);
            this.searchPatientNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchPatientNameTextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
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
            this.patientsDataGridView.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.patientsDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.patientsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.patient_id,
            this.full_name,
            this.addressColumnTextBox,
            this.birth_date,
            this.age,
            this.sex,
            this.civil_status,
            this.patient_contact_number,
            this.emergency_name,
            this.emergency_contact_number,
            this.emergency_relationship,
            this.createdat,
            this.photo});
            this.patientsDataGridView.ContextMenuStrip = this.patientsContextMenuStrip;
            this.patientsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientsDataGridView.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.patientsDataGridView.Location = new System.Drawing.Point(3, 61);
            this.patientsDataGridView.Name = "patientsDataGridView";
            this.patientsDataGridView.Size = new System.Drawing.Size(1898, 946);
            this.patientsDataGridView.TabIndex = 1;
            this.patientsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.patientsDataGridView_CellContentClick);
            this.patientsDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.patientsDataGridView_KeyDown);
            this.patientsDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.patientsDataGridView_MouseDown);
            // 
            // patientsContextMenuStrip
            // 
            this.patientsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewConsultationToolStripMenuItem});
            this.patientsContextMenuStrip.Name = "patientsContextMenuStrip";
            this.patientsContextMenuStrip.Size = new System.Drawing.Size(171, 26);
            // 
            // viewConsultationToolStripMenuItem
            // 
            this.viewConsultationToolStripMenuItem.Name = "viewConsultationToolStripMenuItem";
            this.viewConsultationToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.viewConsultationToolStripMenuItem.Text = "View Consultation";
            this.viewConsultationToolStripMenuItem.Click += new System.EventHandler(this.viewConsultationToolStripMenuItem_Click);
            // 
            // patient_id
            // 
            this.patient_id.DataPropertyName = "patient_id";
            this.patient_id.HeaderText = "Patient ID";
            this.patient_id.Name = "patient_id";
            this.patient_id.ReadOnly = true;
            this.patient_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // full_name
            // 
            this.full_name.DataPropertyName = "full_name";
            this.full_name.HeaderText = "Name";
            this.full_name.Name = "full_name";
            // 
            // addressColumnTextBox
            // 
            this.addressColumnTextBox.DataPropertyName = "address";
            this.addressColumnTextBox.HeaderText = "Address";
            this.addressColumnTextBox.Name = "addressColumnTextBox";
            // 
            // birth_date
            // 
            this.birth_date.DataPropertyName = "birth_date";
            this.birth_date.HeaderText = "Birth Date";
            this.birth_date.Name = "birth_date";
            // 
            // age
            // 
            this.age.DataPropertyName = "age";
            this.age.HeaderText = "Age";
            this.age.Name = "age";
            this.age.ReadOnly = true;
            // 
            // sex
            // 
            this.sex.DataPropertyName = "sex";
            this.sex.HeaderText = "Sex";
            this.sex.Name = "sex";
            // 
            // civil_status
            // 
            this.civil_status.DataPropertyName = "civil_status";
            this.civil_status.HeaderText = "Civil Status";
            this.civil_status.Name = "civil_status";
            // 
            // patient_contact_number
            // 
            this.patient_contact_number.DataPropertyName = "patient_contact_number";
            this.patient_contact_number.HeaderText = "Patient Contact No.";
            this.patient_contact_number.Name = "patient_contact_number";
            // 
            // emergency_name
            // 
            this.emergency_name.DataPropertyName = "emergency_name";
            this.emergency_name.HeaderText = "Emergency Contact Person";
            this.emergency_name.Name = "emergency_name";
            // 
            // emergency_contact_number
            // 
            this.emergency_contact_number.DataPropertyName = "emergency_contact_number";
            this.emergency_contact_number.HeaderText = "Emergency Contact No.";
            this.emergency_contact_number.Name = "emergency_contact_number";
            // 
            // emergency_relationship
            // 
            this.emergency_relationship.DataPropertyName = "emergency_relationship";
            this.emergency_relationship.HeaderText = "Relationship";
            this.emergency_relationship.Name = "emergency_relationship";
            // 
            // createdat
            // 
            this.createdat.DataPropertyName = "created_at";
            this.createdat.HeaderText = "DateCreated";
            this.createdat.Name = "createdat";
            this.createdat.Visible = false;
            // 
            // photo
            // 
            this.photo.DataPropertyName = "photo";
            this.photo.HeaderText = "Photo";
            this.photo.Name = "photo";
            this.photo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.photo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.photo.Visible = false;
            // 
            // PatientListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "PatientListControl";
            this.Size = new System.Drawing.Size(1904, 1010);
            this.Load += new System.EventHandler(this.PatientListControl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PatientListControl_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGridView)).EndInit();
            this.patientsContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button refreshPatientsButton;
        private System.Windows.Forms.Button searchPatientButton;
        private System.Windows.Forms.TextBox searchPatientNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView patientsDataGridView;
        private System.Windows.Forms.Label pageLabel;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.ContextMenuStrip patientsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem viewConsultationToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn patient_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn full_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressColumnTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn birth_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn age;
        private System.Windows.Forms.DataGridViewTextBoxColumn sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn civil_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn patient_contact_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn emergency_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn emergency_contact_number;
        private System.Windows.Forms.DataGridViewTextBoxColumn emergency_relationship;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdat;
        private System.Windows.Forms.DataGridViewImageColumn photo;
    }
}
