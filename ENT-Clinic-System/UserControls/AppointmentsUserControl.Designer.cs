namespace ENT_Clinic_System.UserControls
{
    partial class AppointmentsUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayoutCalendar;
        private System.Windows.Forms.Label lblMonthYear;
        private System.Windows.Forms.Button btnPrevMonth;
        private System.Windows.Forms.Button btnNextMonth;
        private System.Windows.Forms.DataGridView dgvAppointments;

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

        #region Component Designer generated code

        /// <summary> 
        /// Designer — do not modify with logic/loops here.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutCalendar = new System.Windows.Forms.TableLayoutPanel();
            this.lblMonthYear = new System.Windows.Forms.Label();
            this.btnPrevMonth = new System.Windows.Forms.Button();
            this.btnNextMonth = new System.Windows.Forms.Button();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.patientsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewConsultationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.todayButton = new System.Windows.Forms.Button();
            this.apointmentDateLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.patientsContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutCalendar
            // 
            this.tableLayoutCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutCalendar.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutCalendar.ColumnCount = 7;
            this.tableLayoutCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.Location = new System.Drawing.Point(3, 50);
            this.tableLayoutCalendar.Name = "tableLayoutCalendar";
            this.tableLayoutCalendar.RowCount = 6;
            this.tableLayoutCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutCalendar.Size = new System.Drawing.Size(1416, 940);
            this.tableLayoutCalendar.TabIndex = 0;
            // 
            // lblMonthYear
            // 
            this.lblMonthYear.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMonthYear.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMonthYear.Location = new System.Drawing.Point(554, 17);
            this.lblMonthYear.Name = "lblMonthYear";
            this.lblMonthYear.Size = new System.Drawing.Size(400, 30);
            this.lblMonthYear.TabIndex = 0;
            this.lblMonthYear.Text = "January 2025";
            this.lblMonthYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrevMonth
            // 
            this.btnPrevMonth.Location = new System.Drawing.Point(20, 10);
            this.btnPrevMonth.Name = "btnPrevMonth";
            this.btnPrevMonth.Size = new System.Drawing.Size(100, 30);
            this.btnPrevMonth.TabIndex = 1;
            this.btnPrevMonth.Text = "<< Previous";
            this.btnPrevMonth.UseVisualStyleBackColor = true;
            this.btnPrevMonth.Click += new System.EventHandler(this.BtnPrevMonth_Click);
            // 
            // btnNextMonth
            // 
            this.btnNextMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextMonth.Location = new System.Drawing.Point(1319, 13);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(100, 30);
            this.btnNextMonth.TabIndex = 2;
            this.btnNextMonth.Text = "Next >>";
            this.btnNextMonth.UseVisualStyleBackColor = true;
            this.btnNextMonth.Click += new System.EventHandler(this.BtnNextMonth_Click);
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvAppointments.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAppointments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppointments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAppointments.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAppointments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAppointments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAppointments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAppointments.ColumnHeadersHeight = 40;
            this.dgvAppointments.ContextMenuStrip = this.patientsContextMenuStrip;
            this.dgvAppointments.Location = new System.Drawing.Point(1438, 63);
            this.dgvAppointments.MultiSelect = false;
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.RowHeadersVisible = false;
            this.dgvAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointments.Size = new System.Drawing.Size(455, 927);
            this.dgvAppointments.TabIndex = 3;
            this.dgvAppointments.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAppointments_CellMouseDown);
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
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(1438, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(455, 30);
            this.label1.TabIndex = 4;
            this.label1.Text = "APPOINTMENT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // todayButton
            // 
            this.todayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.todayButton.Location = new System.Drawing.Point(1213, 13);
            this.todayButton.Name = "todayButton";
            this.todayButton.Size = new System.Drawing.Size(100, 30);
            this.todayButton.TabIndex = 5;
            this.todayButton.Text = "Today";
            this.todayButton.UseVisualStyleBackColor = true;
            this.todayButton.Click += new System.EventHandler(this.todayButton_Click);
            // 
            // apointmentDateLabel
            // 
            this.apointmentDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.apointmentDateLabel.Font = new System.Drawing.Font("Segoe UI Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apointmentDateLabel.Location = new System.Drawing.Point(1438, 40);
            this.apointmentDateLabel.Name = "apointmentDateLabel";
            this.apointmentDateLabel.Size = new System.Drawing.Size(455, 20);
            this.apointmentDateLabel.TabIndex = 6;
            this.apointmentDateLabel.Text = "Appointments";
            this.apointmentDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AppointmentsUserControl
            // 
            this.Controls.Add(this.apointmentDateLabel);
            this.Controls.Add(this.todayButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMonthYear);
            this.Controls.Add(this.btnPrevMonth);
            this.Controls.Add(this.btnNextMonth);
            this.Controls.Add(this.tableLayoutCalendar);
            this.Controls.Add(this.dgvAppointments);
            this.Name = "AppointmentsUserControl";
            this.Size = new System.Drawing.Size(1904, 1010);
            this.Load += new System.EventHandler(this.AppointmentsUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.patientsContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip patientsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem viewConsultationToolStripMenuItem;
        private System.Windows.Forms.Button todayButton;
        private System.Windows.Forms.Label apointmentDateLabel;
    }
}
