using System.Windows.Forms;

namespace ENT_Clinic_System.Reports.ParamsForm
{
    partial class StockOnHandReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox cmbCategory;
        private DateTimePicker dtpAsOfDate;
        private Label lblCategory;
        private Label lblAsOfDate;
        private Button btnGenerate;
        private Button btnCancel;
        private Label lblHint; // New label for guidance

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.dtpAsOfDate = new System.Windows.Forms.DateTimePicker();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblAsOfDate = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblHint = new System.Windows.Forms.Label(); // Initialize the hint label
            this.SuspendLayout();
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Location = new System.Drawing.Point(150, 27);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 21);
            this.cmbCategory.TabIndex = 1;
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.ForeColor = System.Drawing.Color.Gray;
            this.lblHint.Location = new System.Drawing.Point(150, 50);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(150, 13);
            this.lblHint.TabIndex = 6;
            this.lblHint.Text = "Leave blank or select 'All' to fetch all";
            // 
            // dtpAsOfDate
            // 
            this.dtpAsOfDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAsOfDate.Location = new System.Drawing.Point(150, 70);
            this.dtpAsOfDate.Name = "dtpAsOfDate";
            this.dtpAsOfDate.Size = new System.Drawing.Size(200, 20);
            this.dtpAsOfDate.TabIndex = 3;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(30, 30);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(85, 13);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "Select Category:";
            // 
            // lblAsOfDate
            // 
            this.lblAsOfDate.AutoSize = true;
            this.lblAsOfDate.Location = new System.Drawing.Point(30, 70);
            this.lblAsOfDate.Name = "lblAsOfDate";
            this.lblAsOfDate.Size = new System.Drawing.Size(62, 13);
            this.lblAsOfDate.TabIndex = 2;
            this.lblAsOfDate.Text = "As Of Date:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(150, 110);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(260, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // StockOnHandReportForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 160);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblHint); // Add hint label to form
            this.Controls.Add(this.lblAsOfDate);
            this.Controls.Add(this.dtpAsOfDate);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StockOnHandReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stock On Hand Parameters";
            this.Load += new System.EventHandler(this.StockOnHandReportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
