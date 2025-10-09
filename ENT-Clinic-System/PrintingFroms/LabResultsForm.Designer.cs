using System.Windows.Forms;

namespace ENT_Clinic_System.PrintingForms
{
    partial class LabResultsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvLabResults;
        private System.Windows.Forms.Button btnAttachFile;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.FlowLayoutPanel flpPreview;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ContextMenuStrip cmsDelete;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabResultsForm));
            this.dgvLabResults = new System.Windows.Forms.DataGridView();
            this.btnAttachFile = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.flpPreview = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cmsDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtTestName = new System.Windows.Forms.ComboBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtResultText = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.result_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.consultation_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.test_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.result_text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.result_file = new System.Windows.Forms.DataGridViewLinkColumn();
            this.created_at = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabResults)).BeginInit();
            this.cmsDelete.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLabResults
            // 
            this.dgvLabResults.AllowUserToAddRows = false;
            this.dgvLabResults.AllowUserToDeleteRows = false;
            this.dgvLabResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLabResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.result_id,
            this.consultation_id,
            this.test_name,
            this.result_text,
            this.result_file,
            this.created_at});
            this.dgvLabResults.Location = new System.Drawing.Point(20, 20);
            this.dgvLabResults.MultiSelect = false;
            this.dgvLabResults.Name = "dgvLabResults";
            this.dgvLabResults.ReadOnly = true;
            this.dgvLabResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLabResults.Size = new System.Drawing.Size(1100, 200);
            this.dgvLabResults.TabIndex = 0;
            this.dgvLabResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLabResults_CellClick);
            // 
            // btnAttachFile
            // 
            this.btnAttachFile.Location = new System.Drawing.Point(369, 235);
            this.btnAttachFile.Name = "btnAttachFile";
            this.btnAttachFile.Size = new System.Drawing.Size(80, 30);
            this.btnAttachFile.TabIndex = 3;
            this.btnAttachFile.Text = "Attach File";
            this.btnAttachFile.UseVisualStyleBackColor = true;
            this.btnAttachFile.Click += new System.EventHandler(this.btnAttachFile_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(462, 246);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(63, 17);
            this.lblFileName.TabIndex = 4;
            this.lblFileName.Text = "File name";
            // 
            // flpPreview
            // 
            this.flpPreview.AutoScroll = true;
            this.flpPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpPreview.Location = new System.Drawing.Point(20, 319);
            this.flpPreview.Name = "flpPreview";
            this.flpPreview.Size = new System.Drawing.Size(1100, 329);
            this.flpPreview.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(369, 274);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 30);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(465, 274);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 30);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cmsDelete
            // 
            this.cmsDelete.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // txtTestName
            // 
            this.txtTestName.FormattingEnabled = true;
            this.txtTestName.Location = new System.Drawing.Point(96, 239);
            this.txtTestName.Name = "txtTestName";
            this.txtTestName.Size = new System.Drawing.Size(254, 25);
            this.txtTestName.TabIndex = 8;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(394, 384);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(414, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(394, 361);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(414, 20);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblStatus.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Note:";
            // 
            // txtResultText
            // 
            this.txtResultText.FormattingEnabled = true;
            this.txtResultText.Location = new System.Drawing.Point(96, 277);
            this.txtResultText.Name = "txtResultText";
            this.txtResultText.Size = new System.Drawing.Size(254, 25);
            this.txtResultText.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Test Name:";
            // 
            // result_id
            // 
            this.result_id.DataPropertyName = "result_id";
            this.result_id.HeaderText = "No.";
            this.result_id.Name = "result_id";
            this.result_id.ReadOnly = true;
            this.result_id.Visible = false;
            // 
            // consultation_id
            // 
            this.consultation_id.DataPropertyName = "consultation_id";
            this.consultation_id.HeaderText = "ConsultationId";
            this.consultation_id.Name = "consultation_id";
            this.consultation_id.ReadOnly = true;
            this.consultation_id.Visible = false;
            // 
            // test_name
            // 
            this.test_name.DataPropertyName = "test_name";
            this.test_name.HeaderText = "Note";
            this.test_name.Name = "test_name";
            this.test_name.ReadOnly = true;
            // 
            // result_text
            // 
            this.result_text.DataPropertyName = "result_text";
            this.result_text.HeaderText = "Result Text";
            this.result_text.Name = "result_text";
            this.result_text.ReadOnly = true;
            // 
            // result_file
            // 
            this.result_file.DataPropertyName = "result_file";
            this.result_file.HeaderText = "File Path";
            this.result_file.Name = "result_file";
            this.result_file.ReadOnly = true;
            this.result_file.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.result_file.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // created_at
            // 
            this.created_at.DataPropertyName = "created_at";
            this.created_at.HeaderText = "Created Date";
            this.created_at.Name = "created_at";
            this.created_at.ReadOnly = true;
            // 
            // LabResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 678);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtResultText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.txtTestName);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.flpPreview);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnAttachFile);
            this.Controls.Add(this.dgvLabResults);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LabResultsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Laboratory  Results";
            this.Load += new System.EventHandler(this.LabResultsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabResults)).EndInit();
            this.cmsDelete.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox txtTestName;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private Label label1;
        private ComboBox txtResultText;
        private Label label2;
        private DataGridViewTextBoxColumn result_id;
        private DataGridViewTextBoxColumn consultation_id;
        private DataGridViewTextBoxColumn test_name;
        private DataGridViewTextBoxColumn result_text;
        private DataGridViewLinkColumn result_file;
        private DataGridViewTextBoxColumn created_at;
    }
}
