namespace ENT_Clinic_System.InsertForms
{
    partial class DoctorPatientQueue
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvQueue;
        private System.Windows.Forms.Label lblQueue;

        /// <summary> 
        /// Dispose resources.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoctorPatientQueue));
            this.dgvQueue = new System.Windows.Forms.DataGridView();
            this.lblQueue = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvQueue
            // 
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.AllowUserToDeleteRows = false;
            this.dgvQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueue.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgvQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQueue.Location = new System.Drawing.Point(12, 45);
            this.dgvQueue.MultiSelect = false;
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.RowHeadersVisible = false;
            this.dgvQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueue.Size = new System.Drawing.Size(760, 400);
            this.dgvQueue.TabIndex = 0;
            this.dgvQueue.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQueue_CellValueChanged);
            this.dgvQueue.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvQueue_CurrentCellDirtyStateChanged);
            // 
            // lblQueue
            // 
            this.lblQueue.AutoSize = true;
            this.lblQueue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblQueue.Location = new System.Drawing.Point(12, 15);
            this.lblQueue.Name = "lblQueue";
            this.lblQueue.Size = new System.Drawing.Size(149, 21);
            this.lblQueue.TabIndex = 1;
            this.lblQueue.Text = "Patient Queue List";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(682, 10);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(90, 26);
            this.refreshButton.TabIndex = 9;
            this.refreshButton.Text = "Refesh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click_1);
            // 
            // DoctorPatientQueue
            // 
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.lblQueue);
            this.Controls.Add(this.dgvQueue);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DoctorPatientQueue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doctor Queue Management";
            this.Load += new System.EventHandler(this.DoctorPatientQueue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button refreshButton;
    }
}
