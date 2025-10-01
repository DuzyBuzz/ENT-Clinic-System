namespace ENT_Clinic_System.Inventory
{
    partial class WriteOffForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelBottom = new System.Windows.Forms.Panel();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.numericQuantity = new System.Windows.Forms.NumericUpDown();
            this.labelReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.btnAddWriteOff = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(0, 0);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(5);
            this.panelBottom.Size = new System.Drawing.Size(200, 100);
            this.panelBottom.TabIndex = 0;
            // 
            // labelQuantity
            // 
            this.labelQuantity.Location = new System.Drawing.Point(26, 21);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(100, 23);
            this.labelQuantity.TabIndex = 7;
            this.labelQuantity.Text = "Quantity:";
            // 
            // numericQuantity
            // 
            this.numericQuantity.Location = new System.Drawing.Point(132, 19);
            this.numericQuantity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericQuantity.Name = "numericQuantity";
            this.numericQuantity.Size = new System.Drawing.Size(120, 20);
            this.numericQuantity.TabIndex = 8;
            // 
            // labelReason
            // 
            this.labelReason.Location = new System.Drawing.Point(26, 61);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(100, 23);
            this.labelReason.TabIndex = 9;
            this.labelReason.Text = "Reason:";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(132, 58);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(200, 20);
            this.txtReason.TabIndex = 10;
            // 
            // btnAddWriteOff
            // 
            this.btnAddWriteOff.Location = new System.Drawing.Point(257, 94);
            this.btnAddWriteOff.Name = "btnAddWriteOff";
            this.btnAddWriteOff.Size = new System.Drawing.Size(75, 23);
            this.btnAddWriteOff.TabIndex = 11;
            this.btnAddWriteOff.Text = "Submit Write-Off";
            // 
            // WriteOffForm
            // 
            this.ClientSize = new System.Drawing.Size(358, 137);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.numericQuantity);
            this.Controls.Add(this.labelReason);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.btnAddWriteOff);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WriteOffForm";
            this.ShowIcon = false;
            this.Text = "Inventory Write-Off";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.NumericUpDown numericQuantity;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Button btnAddWriteOff;
    }
}
