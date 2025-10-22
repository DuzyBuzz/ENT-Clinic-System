namespace ENT_Clinic_System.InsertForms
{
    partial class DoctorPatientsQueu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvPatients;
        private System.Windows.Forms.DataGridView dgvQueue;
        private System.Windows.Forms.TextBox txtSearchPatient;
        private System.Windows.Forms.Button btnSearchPatient;
        private System.Windows.Forms.Button btnAddToQueue;
        private System.Windows.Forms.Button btnRemoveFromQueue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoctorPatientsQueu));
            this.dgvPatients = new System.Windows.Forms.DataGridView();
            this.dgvQueue = new System.Windows.Forms.DataGridView();
            this.txtSearchPatient = new System.Windows.Forms.TextBox();
            this.btnSearchPatient = new System.Windows.Forms.Button();
            this.btnAddToQueue = new System.Windows.Forms.Button();
            this.btnRemoveFromQueue = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPatients
            // 
            this.dgvPatients.AllowUserToAddRows = false;
            this.dgvPatients.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvPatients.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPatients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPatients.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPatients.Location = new System.Drawing.Point(3, 20);
            this.dgvPatients.MultiSelect = false;
            this.dgvPatients.Name = "dgvPatients";
            this.dgvPatients.ReadOnly = true;
            this.dgvPatients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPatients.Size = new System.Drawing.Size(322, 455);
            this.dgvPatients.TabIndex = 0;
            this.dgvPatients.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatients_CellContentClick);
            this.dgvPatients.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPatients_CellDoubleClick);
            // 
            // dgvQueue
            // 
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvQueue.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueue.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueue.Location = new System.Drawing.Point(3, 20);
            this.dgvQueue.MultiSelect = false;
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.Size = new System.Drawing.Size(1295, 784);
            this.dgvQueue.TabIndex = 1;
            this.dgvQueue.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQueue_CellContentClick);
            this.dgvQueue.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvQueue_CellFormatting);
            this.dgvQueue.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQueue_CellValueChanged_1);
            this.dgvQueue.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvQueue_CurrentCellDirtyStateChanged_1);
            this.dgvQueue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvQueue_MouseDown);
            // 
            // txtSearchPatient
            // 
            this.txtSearchPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchPatient.Location = new System.Drawing.Point(3, 3);
            this.txtSearchPatient.Name = "txtSearchPatient";
            this.txtSearchPatient.Size = new System.Drawing.Size(276, 24);
            this.txtSearchPatient.TabIndex = 2;
            this.txtSearchPatient.TextChanged += new System.EventHandler(this.txtSearchPatient_TextChanged);
            // 
            // btnSearchPatient
            // 
            this.btnSearchPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearchPatient.Location = new System.Drawing.Point(285, 3);
            this.btnSearchPatient.Name = "btnSearchPatient";
            this.btnSearchPatient.Size = new System.Drawing.Size(126, 28);
            this.btnSearchPatient.TabIndex = 3;
            this.btnSearchPatient.Text = "Search";
            this.btnSearchPatient.UseVisualStyleBackColor = true;
            this.btnSearchPatient.Click += new System.EventHandler(this.btnSearchPatient_Click);
            // 
            // btnAddToQueue
            // 
            this.btnAddToQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddToQueue.Location = new System.Drawing.Point(417, 3);
            this.btnAddToQueue.Name = "btnAddToQueue";
            this.btnAddToQueue.Size = new System.Drawing.Size(103, 28);
            this.btnAddToQueue.TabIndex = 4;
            this.btnAddToQueue.Text = "Add to Queue";
            this.btnAddToQueue.UseVisualStyleBackColor = true;
            this.btnAddToQueue.Click += new System.EventHandler(this.btnAddToQueue_Click);
            // 
            // btnRemoveFromQueue
            // 
            this.btnRemoveFromQueue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemoveFromQueue.ForeColor = System.Drawing.Color.Red;
            this.btnRemoveFromQueue.Location = new System.Drawing.Point(526, 3);
            this.btnRemoveFromQueue.Name = "btnRemoveFromQueue";
            this.btnRemoveFromQueue.Size = new System.Drawing.Size(117, 28);
            this.btnRemoveFromQueue.TabIndex = 5;
            this.btnRemoveFromQueue.Text = "Remove from Queue";
            this.btnRemoveFromQueue.UseVisualStyleBackColor = true;
            this.btnRemoveFromQueue.Click += new System.EventHandler(this.btnRemoveFromQueue_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(1036, 20);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(90, 26);
            this.refreshButton.TabIndex = 8;
            this.refreshButton.Text = "Refesh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(181, 209);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1120, 598);
            this.tableLayoutPanel1.TabIndex = 9;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 43);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1114, 484);
            this.tableLayoutPanel2.TabIndex = 0;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvQueue);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1301, 807);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Queue";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvPatients);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 478);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Patients";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.4417F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.92579F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.805654F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.08126F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.90732F));
            this.tableLayoutPanel3.Controls.Add(this.btnSearchPatient, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnAddToQueue, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtSearchPatient, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnRemoveFromQueue, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1114, 34);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // DoctorPatientsQueu
            // 
            this.ClientSize = new System.Drawing.Size(1301, 807);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.refreshButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DoctorPatientsQueu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patients Queue";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.PatientsQueue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}
