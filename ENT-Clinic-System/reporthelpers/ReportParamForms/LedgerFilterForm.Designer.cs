namespace ENT_Clinic_System.Helpers.ReportHelpers
{
    partial class LedgerFilterForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblConcessionaire;
        private System.Windows.Forms.TextBox txtConcessionaire;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
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
            this.lblConcessionaire = new System.Windows.Forms.Label();
            this.txtConcessionaire = new System.Windows.Forms.TextBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblConcessionaire
            this.lblConcessionaire.AutoSize = true;
            this.lblConcessionaire.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblConcessionaire.Location = new System.Drawing.Point(18, 18);
            this.lblConcessionaire.Name = "lblConcessionaire";
            this.lblConcessionaire.Size = new System.Drawing.Size(91, 15);
            this.lblConcessionaire.TabIndex = 0;
            this.lblConcessionaire.Text = "Concessionaire:";

            // txtConcessionaire
            this.txtConcessionaire.Location = new System.Drawing.Point(120, 14);
            this.txtConcessionaire.Name = "txtConcessionaire";
            this.txtConcessionaire.Size = new System.Drawing.Size(200, 23);
            this.txtConcessionaire.TabIndex = 1;

            // lblFrom
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFrom.Location = new System.Drawing.Point(18, 52);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(67, 15);
            this.lblFrom.TabIndex = 2;
            this.lblFrom.Text = "From Date:";

            // dtpFrom
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(120, 48);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 23);
            this.dtpFrom.TabIndex = 3;

            // lblTo
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTo.Location = new System.Drawing.Point(18, 86);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(56, 15);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To Date:";

            // dtpTo
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(120, 82);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 23);
            this.dtpTo.TabIndex = 5;

            // btnOK
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnOK.Location = new System.Drawing.Point(120, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 30);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            // btnCancel
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(230, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // LedgerFilterForm
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(350, 170);
            this.Controls.Add(this.lblConcessionaire);
            this.Controls.Add(this.txtConcessionaire);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ledger Filter";
            this.Load += new System.EventHandler(this.LedgerFilterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
