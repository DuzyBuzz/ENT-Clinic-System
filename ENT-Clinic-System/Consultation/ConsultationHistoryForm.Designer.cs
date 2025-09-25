namespace ENT_Clinic_System.Consultation
{
    partial class ConsultationHistoryForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.consultationDateDataGridView = new System.Windows.Forms.DataGridView();
            this.consultation_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.consultationHistoryContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printConsultationHistoryButton = new System.Windows.Forms.ToolStripMenuItem();
            this.printAttachmentButton = new System.Windows.Forms.ToolStripMenuItem();
            this.printMedicalCertificateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createLaboratoryRequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.consultationDateDataGridView)).BeginInit();
            this.consultationHistoryContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // consultationDateDataGridView
            // 
            this.consultationDateDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.consultationDateDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.consultationDateDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.consultationDateDataGridView.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.consultationDateDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.consultationDateDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.consultationDateDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.consultation_date});
            this.consultationDateDataGridView.ContextMenuStrip = this.consultationHistoryContextMenuStrip;
            this.consultationDateDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consultationDateDataGridView.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.consultationDateDataGridView.Location = new System.Drawing.Point(0, 0);
            this.consultationDateDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.consultationDateDataGridView.Name = "consultationDateDataGridView";
            this.consultationDateDataGridView.RowHeadersVisible = false;
            this.consultationDateDataGridView.Size = new System.Drawing.Size(275, 473);
            this.consultationDateDataGridView.TabIndex = 3;
            // 
            // consultation_date
            // 
            this.consultation_date.DataPropertyName = "consultation_date";
            this.consultation_date.HeaderText = "Consultation History";
            this.consultation_date.Name = "consultation_date";
            this.consultation_date.ReadOnly = true;
            // 
            // consultationHistoryContextMenuStrip
            // 
            this.consultationHistoryContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printConsultationHistoryButton,
            this.printAttachmentButton,
            this.printMedicalCertificateToolStripMenuItem,
            this.createLaboratoryRequestToolStripMenuItem});
            this.consultationHistoryContextMenuStrip.Name = "consultationHistoryContextMenuStrip";
            this.consultationHistoryContextMenuStrip.Size = new System.Drawing.Size(216, 114);
            // 
            // printConsultationHistoryButton
            // 
            this.printConsultationHistoryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.printConsultationHistoryButton.Name = "printConsultationHistoryButton";
            this.printConsultationHistoryButton.Size = new System.Drawing.Size(215, 22);
            this.printConsultationHistoryButton.Text = "Show Consultation History";
            this.printConsultationHistoryButton.Click += new System.EventHandler(this.printConsultationHistoryButton_Click);
            // 
            // printAttachmentButton
            // 
            this.printAttachmentButton.Name = "printAttachmentButton";
            this.printAttachmentButton.Size = new System.Drawing.Size(215, 22);
            this.printAttachmentButton.Text = "Show Attachments";
            this.printAttachmentButton.Click += new System.EventHandler(this.printAttachmentButton_Click);
            // 
            // printMedicalCertificateToolStripMenuItem
            // 
            this.printMedicalCertificateToolStripMenuItem.Name = "printMedicalCertificateToolStripMenuItem";
            this.printMedicalCertificateToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.printMedicalCertificateToolStripMenuItem.Text = "Print Medical Certificate";
            this.printMedicalCertificateToolStripMenuItem.Click += new System.EventHandler(this.printMedicalCertificateToolStripMenuItem_Click);
            // 
            // createLaboratoryRequestToolStripMenuItem
            // 
            this.createLaboratoryRequestToolStripMenuItem.Name = "createLaboratoryRequestToolStripMenuItem";
            this.createLaboratoryRequestToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.createLaboratoryRequestToolStripMenuItem.Text = "Create Laboratory Request";
            this.createLaboratoryRequestToolStripMenuItem.Click += new System.EventHandler(this.createLaboratoryRequestToolStripMenuItem_Click);
            // 
            // ConsultationHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 473);
            this.Controls.Add(this.consultationDateDataGridView);
            this.Name = "ConsultationHistoryForm";
            this.Text = "ConsultationHistoryForm";
            this.Load += new System.EventHandler(this.ConsultationHistoryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.consultationDateDataGridView)).EndInit();
            this.consultationHistoryContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView consultationDateDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn consultation_date;
        private System.Windows.Forms.ContextMenuStrip consultationHistoryContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem printConsultationHistoryButton;
        private System.Windows.Forms.ToolStripMenuItem printAttachmentButton;
        private System.Windows.Forms.ToolStripMenuItem printMedicalCertificateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createLaboratoryRequestToolStripMenuItem;
    }
}