using System.Windows.Forms;

namespace ENT_Clinic_System.Reports.ParamsForm
{
    partial class BillingParamsForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbPatient;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Label lblPatient;
        private Label lblFrom;
        private Label lblTo;
        private Button btnGenerate;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbPatient = new ComboBox();
            this.dtpFrom = new DateTimePicker();
            this.dtpTo = new DateTimePicker();
            this.lblPatient = new Label();
            this.lblFrom = new Label();
            this.lblTo = new Label();
            this.btnGenerate = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // Patient ComboBox
            this.cmbPatient.Location = new System.Drawing.Point(150, 20);
            this.cmbPatient.Name = "cmbPatient";
            this.cmbPatient.Size = new System.Drawing.Size(200, 21);

            // From Date
            this.dtpFrom.Format = DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(150, 60);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 20);

            // To Date
            this.dtpTo.Format = DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(150, 100);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 20);

            // Labels
            this.lblPatient.AutoSize = true;
            this.lblPatient.Location = new System.Drawing.Point(30, 23);
            this.lblPatient.Text = "Select Patient:";
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(30, 63);
            this.lblFrom.Text = "From Date:";
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(30, 103);
            this.lblTo.Text = "To Date:";

            // Buttons
            this.btnGenerate.Location = new System.Drawing.Point(150, 140);
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);

            this.btnCancel.Location = new System.Drawing.Point(275, 140);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Controls.Add(this.cmbPatient);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Billing Report Parameters";
            this.Load += new System.EventHandler(this.BillingParamsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
