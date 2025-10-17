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
            this.panelContainer = new System.Windows.Forms.Panel();
            this.grpItemDetails = new System.Windows.Forms.GroupBox();
            this.lblBrandLabel = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblGenericLabel = new System.Windows.Forms.Label();
            this.lblGeneric = new System.Windows.Forms.Label();
            this.lblStrengthLabel = new System.Windows.Forms.Label();
            this.lblStrength = new System.Windows.Forms.Label();
            this.lblDosageLabel = new System.Windows.Forms.Label();
            this.lblDosage = new System.Windows.Forms.Label();
            this.lblCategoryLabel = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblDescriptionLabel = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblStockQtyLabel = new System.Windows.Forms.Label();
            this.lblStockQty = new System.Windows.Forms.Label();
            this.grpWriteOff = new System.Windows.Forms.GroupBox();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.numericQuantity = new System.Windows.Forms.NumericUpDown();
            this.labelReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.btnAddWriteOff = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            this.grpItemDetails.SuspendLayout();
            this.grpWriteOff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.grpItemDetails);
            this.panelContainer.Controls.Add(this.grpWriteOff);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(406, 420);
            this.panelContainer.TabIndex = 0;
            // 
            // grpItemDetails
            // 
            this.grpItemDetails.Controls.Add(this.lblBrandLabel);
            this.grpItemDetails.Controls.Add(this.lblBrand);
            this.grpItemDetails.Controls.Add(this.lblGenericLabel);
            this.grpItemDetails.Controls.Add(this.lblGeneric);
            this.grpItemDetails.Controls.Add(this.lblStrengthLabel);
            this.grpItemDetails.Controls.Add(this.lblStrength);
            this.grpItemDetails.Controls.Add(this.lblDosageLabel);
            this.grpItemDetails.Controls.Add(this.lblDosage);
            this.grpItemDetails.Controls.Add(this.lblCategoryLabel);
            this.grpItemDetails.Controls.Add(this.lblCategory);
            this.grpItemDetails.Controls.Add(this.lblDescriptionLabel);
            this.grpItemDetails.Controls.Add(this.lblDescription);
            this.grpItemDetails.Controls.Add(this.lblStockQtyLabel);
            this.grpItemDetails.Controls.Add(this.lblStockQty);
            this.grpItemDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpItemDetails.Location = new System.Drawing.Point(0, 0);
            this.grpItemDetails.Name = "grpItemDetails";
            this.grpItemDetails.Size = new System.Drawing.Size(406, 222);
            this.grpItemDetails.TabIndex = 0;
            this.grpItemDetails.TabStop = false;
            this.grpItemDetails.Text = "Item Details";
            // 
            // lblBrandLabel
            // 
            this.lblBrandLabel.Location = new System.Drawing.Point(10, 25);
            this.lblBrandLabel.Name = "lblBrandLabel";
            this.lblBrandLabel.Size = new System.Drawing.Size(100, 23);
            this.lblBrandLabel.TabIndex = 0;
            this.lblBrandLabel.Text = "Brand:";
            // 
            // lblBrand
            // 
            this.lblBrand.Location = new System.Drawing.Point(120, 25);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(100, 23);
            this.lblBrand.TabIndex = 1;
            this.lblBrand.Text = "N/A";
            // 
            // lblGenericLabel
            // 
            this.lblGenericLabel.Location = new System.Drawing.Point(10, 50);
            this.lblGenericLabel.Name = "lblGenericLabel";
            this.lblGenericLabel.Size = new System.Drawing.Size(100, 23);
            this.lblGenericLabel.TabIndex = 2;
            this.lblGenericLabel.Text = "Generic:";
            // 
            // lblGeneric
            // 
            this.lblGeneric.Location = new System.Drawing.Point(120, 50);
            this.lblGeneric.Name = "lblGeneric";
            this.lblGeneric.Size = new System.Drawing.Size(100, 23);
            this.lblGeneric.TabIndex = 3;
            this.lblGeneric.Text = "N/A";
            // 
            // lblStrengthLabel
            // 
            this.lblStrengthLabel.Location = new System.Drawing.Point(10, 75);
            this.lblStrengthLabel.Name = "lblStrengthLabel";
            this.lblStrengthLabel.Size = new System.Drawing.Size(100, 23);
            this.lblStrengthLabel.TabIndex = 4;
            this.lblStrengthLabel.Text = "Strength:";
            // 
            // lblStrength
            // 
            this.lblStrength.Location = new System.Drawing.Point(120, 75);
            this.lblStrength.Name = "lblStrength";
            this.lblStrength.Size = new System.Drawing.Size(100, 23);
            this.lblStrength.TabIndex = 5;
            this.lblStrength.Text = "N/A";
            // 
            // lblDosageLabel
            // 
            this.lblDosageLabel.Location = new System.Drawing.Point(10, 100);
            this.lblDosageLabel.Name = "lblDosageLabel";
            this.lblDosageLabel.Size = new System.Drawing.Size(100, 23);
            this.lblDosageLabel.TabIndex = 6;
            this.lblDosageLabel.Text = "Dosage:";
            // 
            // lblDosage
            // 
            this.lblDosage.Location = new System.Drawing.Point(120, 100);
            this.lblDosage.Name = "lblDosage";
            this.lblDosage.Size = new System.Drawing.Size(100, 23);
            this.lblDosage.TabIndex = 7;
            this.lblDosage.Text = "N/A";
            // 
            // lblCategoryLabel
            // 
            this.lblCategoryLabel.Location = new System.Drawing.Point(10, 125);
            this.lblCategoryLabel.Name = "lblCategoryLabel";
            this.lblCategoryLabel.Size = new System.Drawing.Size(100, 23);
            this.lblCategoryLabel.TabIndex = 8;
            this.lblCategoryLabel.Text = "Category:";
            // 
            // lblCategory
            // 
            this.lblCategory.Location = new System.Drawing.Point(120, 125);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(100, 23);
            this.lblCategory.TabIndex = 9;
            this.lblCategory.Text = "N/A";
            // 
            // lblDescriptionLabel
            // 
            this.lblDescriptionLabel.Location = new System.Drawing.Point(10, 150);
            this.lblDescriptionLabel.Name = "lblDescriptionLabel";
            this.lblDescriptionLabel.Size = new System.Drawing.Size(100, 23);
            this.lblDescriptionLabel.TabIndex = 10;
            this.lblDescriptionLabel.Text = "Description:";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(120, 150);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(100, 23);
            this.lblDescription.TabIndex = 11;
            this.lblDescription.Text = "N/A";
            // 
            // lblStockQtyLabel
            // 
            this.lblStockQtyLabel.Location = new System.Drawing.Point(10, 175);
            this.lblStockQtyLabel.Name = "lblStockQtyLabel";
            this.lblStockQtyLabel.Size = new System.Drawing.Size(100, 23);
            this.lblStockQtyLabel.TabIndex = 12;
            this.lblStockQtyLabel.Text = "Stock Qty:";
            // 
            // lblStockQty
            // 
            this.lblStockQty.Location = new System.Drawing.Point(120, 175);
            this.lblStockQty.Name = "lblStockQty";
            this.lblStockQty.Size = new System.Drawing.Size(100, 23);
            this.lblStockQty.TabIndex = 13;
            this.lblStockQty.Text = "0";
            // 
            // grpWriteOff
            // 
            this.grpWriteOff.Controls.Add(this.labelQuantity);
            this.grpWriteOff.Controls.Add(this.numericQuantity);
            this.grpWriteOff.Controls.Add(this.labelReason);
            this.grpWriteOff.Controls.Add(this.txtReason);
            this.grpWriteOff.Controls.Add(this.btnAddWriteOff);
            this.grpWriteOff.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpWriteOff.Location = new System.Drawing.Point(0, 222);
            this.grpWriteOff.Name = "grpWriteOff";
            this.grpWriteOff.Size = new System.Drawing.Size(406, 198);
            this.grpWriteOff.TabIndex = 1;
            this.grpWriteOff.TabStop = false;
            this.grpWriteOff.Text = "Write-Off";
            // 
            // labelQuantity
            // 
            this.labelQuantity.Location = new System.Drawing.Point(10, 30);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(100, 23);
            this.labelQuantity.TabIndex = 0;
            this.labelQuantity.Text = "Quantity:";
            // 
            // numericQuantity
            // 
            this.numericQuantity.Location = new System.Drawing.Point(120, 30);
            this.numericQuantity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericQuantity.Name = "numericQuantity";
            this.numericQuantity.Size = new System.Drawing.Size(120, 20);
            this.numericQuantity.TabIndex = 1;
            this.numericQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelReason
            // 
            this.labelReason.Location = new System.Drawing.Point(10, 70);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(100, 23);
            this.labelReason.TabIndex = 2;
            this.labelReason.Text = "Reason:";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(120, 70);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(250, 64);
            this.txtReason.TabIndex = 3;
            // 
            // btnAddWriteOff
            // 
            this.btnAddWriteOff.Location = new System.Drawing.Point(295, 140);
            this.btnAddWriteOff.Name = "btnAddWriteOff";
            this.btnAddWriteOff.Size = new System.Drawing.Size(75, 23);
            this.btnAddWriteOff.TabIndex = 4;
            this.btnAddWriteOff.Text = "Add Write-Off";
            this.btnAddWriteOff.Click += new System.EventHandler(this.btnAddWriteOff_Click);
            // 
            // WriteOffForm
            // 
            this.ClientSize = new System.Drawing.Size(406, 420);
            this.Controls.Add(this.panelContainer);
            this.Name = "WriteOffForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Write-Off Item";
            this.panelContainer.ResumeLayout(false);
            this.grpItemDetails.ResumeLayout(false);
            this.grpWriteOff.ResumeLayout(false);
            this.grpWriteOff.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).EndInit();
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
