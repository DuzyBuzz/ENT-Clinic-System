namespace ENT_Clinic_System.Helpers.ReportHelpers
{
    partial class MonthYearSelectionForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox cboMonth;
        private System.Windows.Forms.NumericUpDown numYear;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.numYear = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
            this.SuspendLayout();

            // lblMonth
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMonth.Location = new System.Drawing.Point(18, 18);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(47, 15);
            this.lblMonth.TabIndex = 0;
            this.lblMonth.Text = "Month:";

            // cboMonth
            this.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Items.AddRange(new object[]
            {
                "January","February","March","April","May","June",
                "July","August","September","October","November","December"
            });
            this.cboMonth.Location = new System.Drawing.Point(70, 14);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(130, 23);
            this.cboMonth.TabIndex = 1;

            // lblYear
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblYear.Location = new System.Drawing.Point(18, 52);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(35, 15);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "Year:";

            // numYear
            this.numYear.Location = new System.Drawing.Point(70, 48);
            this.numYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.numYear.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.numYear.Name = "numYear";
            this.numYear.Size = new System.Drawing.Size(130, 23);
            this.numYear.TabIndex = 3;

            // btnOK
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnOK.Location = new System.Drawing.Point(20, 85);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(86, 30);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            // btnCancel
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(114, 85);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Form settings
            this.AcceptButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(220, 130);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.cboMonth);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.numYear);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Month & Year";
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
