namespace ENT_Clinic_System.Inventory
{
    partial class InventoryForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.GroupBox groupBoxItem;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.TextBox txtCostPrice;
        private System.Windows.Forms.TextBox txtSellingPrice;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnUpdateItem;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblCostPrice;
        private System.Windows.Forms.Label lblSellingPrice;

        private System.Windows.Forms.GroupBox groupBoxStock;
        private System.Windows.Forms.TextBox txtItemId;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnStockIn;
        private System.Windows.Forms.Button btnStockOut;
        private System.Windows.Forms.Label lblItemId;
        private System.Windows.Forms.Label lblQuantity;

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
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.groupBoxItem = new System.Windows.Forms.GroupBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblCostPrice = new System.Windows.Forms.Label();
            this.lblSellingPrice = new System.Windows.Forms.Label();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.txtCostPrice = new System.Windows.Forms.TextBox();
            this.txtSellingPrice = new System.Windows.Forms.TextBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnUpdateItem = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.groupBoxStock = new System.Windows.Forms.GroupBox();
            this.discountCheckBox = new System.Windows.Forms.CheckBox();
            this.lblItemId = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtItemId = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnStockIn = new System.Windows.Forms.Button();
            this.btnStockOut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.groupBoxItem.SuspendLayout();
            this.groupBoxStock.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Location = new System.Drawing.Point(10, 10);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(651, 191);
            this.dgvItems.TabIndex = 0;
            // 
            // groupBoxItem
            // 
            this.groupBoxItem.Controls.Add(this.label1);
            this.groupBoxItem.Controls.Add(this.txtDescription);
            this.groupBoxItem.Controls.Add(this.lblItemName);
            this.groupBoxItem.Controls.Add(this.lblCategory);
            this.groupBoxItem.Controls.Add(this.lblCostPrice);
            this.groupBoxItem.Controls.Add(this.lblSellingPrice);
            this.groupBoxItem.Controls.Add(this.txtItemName);
            this.groupBoxItem.Controls.Add(this.txtCategory);
            this.groupBoxItem.Controls.Add(this.txtCostPrice);
            this.groupBoxItem.Controls.Add(this.txtSellingPrice);
            this.groupBoxItem.Controls.Add(this.btnAddItem);
            this.groupBoxItem.Controls.Add(this.btnUpdateItem);
            this.groupBoxItem.Controls.Add(this.btnDeleteItem);
            this.groupBoxItem.Location = new System.Drawing.Point(10, 217);
            this.groupBoxItem.Name = "groupBoxItem";
            this.groupBoxItem.Size = new System.Drawing.Size(317, 189);
            this.groupBoxItem.TabIndex = 1;
            this.groupBoxItem.TabStop = false;
            this.groupBoxItem.Text = "Add / Update Item";
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(13, 26);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(61, 13);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "Item Name:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(13, 52);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(63, 13);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Description:";
            // 
            // lblCostPrice
            // 
            this.lblCostPrice.AutoSize = true;
            this.lblCostPrice.Location = new System.Drawing.Point(13, 107);
            this.lblCostPrice.Name = "lblCostPrice";
            this.lblCostPrice.Size = new System.Drawing.Size(58, 13);
            this.lblCostPrice.TabIndex = 2;
            this.lblCostPrice.Text = "Cost Price:";
            // 
            // lblSellingPrice
            // 
            this.lblSellingPrice.AutoSize = true;
            this.lblSellingPrice.Location = new System.Drawing.Point(13, 133);
            this.lblSellingPrice.Name = "lblSellingPrice";
            this.lblSellingPrice.Size = new System.Drawing.Size(68, 13);
            this.lblSellingPrice.TabIndex = 3;
            this.lblSellingPrice.Text = "Selling Price:";
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(86, 23);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(215, 20);
            this.txtItemName.TabIndex = 4;
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(86, 75);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(215, 20);
            this.txtCategory.TabIndex = 5;
            // 
            // txtCostPrice
            // 
            this.txtCostPrice.Location = new System.Drawing.Point(86, 104);
            this.txtCostPrice.Name = "txtCostPrice";
            this.txtCostPrice.Size = new System.Drawing.Size(86, 20);
            this.txtCostPrice.TabIndex = 6;
            // 
            // txtSellingPrice
            // 
            this.txtSellingPrice.Location = new System.Drawing.Point(86, 130);
            this.txtSellingPrice.Name = "txtSellingPrice";
            this.txtSellingPrice.Size = new System.Drawing.Size(86, 20);
            this.txtSellingPrice.TabIndex = 7;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(189, 103);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(103, 22);
            this.btnAddItem.TabIndex = 8;
            this.btnAddItem.Text = "Add Item";
            // 
            // btnUpdateItem
            // 
            this.btnUpdateItem.Location = new System.Drawing.Point(189, 129);
            this.btnUpdateItem.Name = "btnUpdateItem";
            this.btnUpdateItem.Size = new System.Drawing.Size(103, 22);
            this.btnUpdateItem.TabIndex = 9;
            this.btnUpdateItem.Text = "Update Item";
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(189, 155);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(103, 22);
            this.btnDeleteItem.TabIndex = 10;
            this.btnDeleteItem.Text = "Delete Item";
            // 
            // groupBoxStock
            // 
            this.groupBoxStock.Controls.Add(this.discountCheckBox);
            this.groupBoxStock.Controls.Add(this.lblItemId);
            this.groupBoxStock.Controls.Add(this.lblQuantity);
            this.groupBoxStock.Controls.Add(this.txtItemId);
            this.groupBoxStock.Controls.Add(this.txtQuantity);
            this.groupBoxStock.Controls.Add(this.btnStockIn);
            this.groupBoxStock.Controls.Add(this.btnStockOut);
            this.groupBoxStock.Location = new System.Drawing.Point(343, 217);
            this.groupBoxStock.Name = "groupBoxStock";
            this.groupBoxStock.Size = new System.Drawing.Size(317, 189);
            this.groupBoxStock.TabIndex = 2;
            this.groupBoxStock.TabStop = false;
            this.groupBoxStock.Text = "Stock In / Out";
            // 
            // discountCheckBox
            // 
            this.discountCheckBox.AutoSize = true;
            this.discountCheckBox.Location = new System.Drawing.Point(16, 84);
            this.discountCheckBox.Name = "discountCheckBox";
            this.discountCheckBox.Size = new System.Drawing.Size(78, 17);
            this.discountCheckBox.TabIndex = 6;
            this.discountCheckBox.Text = "discounted";
            this.discountCheckBox.UseVisualStyleBackColor = true;
            // 
            // lblItemId
            // 
            this.lblItemId.AutoSize = true;
            this.lblItemId.Location = new System.Drawing.Point(13, 35);
            this.lblItemId.Name = "lblItemId";
            this.lblItemId.Size = new System.Drawing.Size(44, 13);
            this.lblItemId.TabIndex = 0;
            this.lblItemId.Text = "Item ID:";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(13, 61);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(49, 13);
            this.lblQuantity.TabIndex = 1;
            this.lblQuantity.Text = "Quantity:";
            // 
            // txtItemId
            // 
            this.txtItemId.Location = new System.Drawing.Point(86, 32);
            this.txtItemId.Name = "txtItemId";
            this.txtItemId.Size = new System.Drawing.Size(86, 20);
            this.txtItemId.TabIndex = 2;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(86, 58);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(86, 20);
            this.txtQuantity.TabIndex = 3;
            // 
            // btnStockIn
            // 
            this.btnStockIn.Location = new System.Drawing.Point(189, 30);
            this.btnStockIn.Name = "btnStockIn";
            this.btnStockIn.Size = new System.Drawing.Size(103, 22);
            this.btnStockIn.TabIndex = 4;
            this.btnStockIn.Text = "Stock In";
            // 
            // btnStockOut
            // 
            this.btnStockOut.Location = new System.Drawing.Point(189, 56);
            this.btnStockOut.Name = "btnStockOut";
            this.btnStockOut.Size = new System.Drawing.Size(103, 22);
            this.btnStockOut.TabIndex = 5;
            this.btnStockOut.Text = "Stock Out";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Category:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(86, 49);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(215, 20);
            this.txtDescription.TabIndex = 12;
            // 
            // InventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 418);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.groupBoxItem);
            this.Controls.Add(this.groupBoxStock);
            this.Name = "InventoryForm";
            this.Text = "Inventory Management - ENT Clinic";
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.groupBoxItem.ResumeLayout(false);
            this.groupBoxItem.PerformLayout();
            this.groupBoxStock.ResumeLayout(false);
            this.groupBoxStock.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox discountCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescription;
    }
}
