namespace ENT_Clinic_System.Inventory
{
    partial class ReturnForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvReturns;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Form settings
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "ReturnForm";

            // Label Item Name
            lblItem = new System.Windows.Forms.Label();
            lblItem.Text = "Item Name:";
            lblItem.Location = new System.Drawing.Point(20, 20);
            lblItem.AutoSize = true;
            this.Controls.Add(lblItem);

            // TextBox Item Name
            txtItemName = new System.Windows.Forms.TextBox();
            txtItemName.Location = new System.Drawing.Point(100, 18);
            txtItemName.Width = 200;
            this.Controls.Add(txtItemName);

            // Label Quantity
            lblQty = new System.Windows.Forms.Label();
            lblQty.Text = "Quantity:";
            lblQty.Location = new System.Drawing.Point(20, 60);
            lblQty.AutoSize = true;
            this.Controls.Add(lblQty);

            // TextBox Quantity
            txtQuantity = new System.Windows.Forms.TextBox();
            txtQuantity.Location = new System.Drawing.Point(100, 58);
            txtQuantity.Width = 100;
            this.Controls.Add(txtQuantity);

            // Button Return
            btnReturn = new System.Windows.Forms.Button();
            btnReturn.Text = "Return Item";
            btnReturn.Location = new System.Drawing.Point(20, 100);
            btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            this.Controls.Add(btnReturn);

            // Button Cancel
            btnCancel = new System.Windows.Forms.Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new System.Drawing.Point(150, 100);
            btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.Controls.Add(btnCancel);

            // DataGridView Returns
            dgvReturns = new System.Windows.Forms.DataGridView();
            dgvReturns.Location = new System.Drawing.Point(20, 150);
            dgvReturns.Size = new System.Drawing.Size(750, 250);
            dgvReturns.AllowUserToAddRows = false;
            dgvReturns.ReadOnly = true;
            dgvReturns.ColumnCount = 3;
            dgvReturns.Columns[0].Name = "Item Name";
            dgvReturns.Columns[1].Name = "Quantity";
            dgvReturns.Columns[2].Name = "Return Date";
            this.Controls.Add(dgvReturns);
        }
    }
}
