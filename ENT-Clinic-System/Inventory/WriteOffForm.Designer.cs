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
            this.SuspendLayout();
            // 
            // WriteOffForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "WriteOffForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.GroupBox grpItemDetails;
        private System.Windows.Forms.Label lblBrandLabel;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblGenericLabel;
        private System.Windows.Forms.Label lblGeneric;
        private System.Windows.Forms.Label lblStrengthLabel;
        private System.Windows.Forms.Label lblStrength;
        private System.Windows.Forms.Label lblDosageLabel;
        private System.Windows.Forms.Label lblDosage;
        private System.Windows.Forms.Label lblCategoryLabel;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblDescriptionLabel;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblStockQtyLabel;
        private System.Windows.Forms.Label lblStockQty;

        private System.Windows.Forms.GroupBox grpWriteOff;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.NumericUpDown numericQuantity;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Button btnAddWriteOff;
    }
}
