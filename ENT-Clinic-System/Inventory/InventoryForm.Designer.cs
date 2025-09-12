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
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.txtCostPrice = new System.Windows.Forms.TextBox();
            this.txtSellingPrice = new System.Windows.Forms.TextBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnUpdateItem = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblCostPrice = new System.Windows.Forms.Label();
            this.lblSellingPrice = new System.Windows.Forms.Label();

            this.groupBoxStock = new System.Windows.Forms.GroupBox();
            this.txtItemId = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnStockIn = new System.Windows.Forms.Button();
            this.btnStockOut = new System.Windows.Forms.Button();
            this.lblItemId = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.groupBoxItem.SuspendLayout();
            this.groupBoxStock.SuspendLayout();
            this.SuspendLayout();

            // ========================
            // 🔹 DataGridView
            // ========================
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Location = new System.Drawing.Point(12, 12);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(760, 220);
            this.dgvItems.TabIndex = 0;

            // ========================
            // 🔹 GroupBox Item
            // ========================
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
            this.groupBoxItem.Location = new System.Drawing.Point(12, 250);
            this.groupBoxItem.Name = "groupBoxItem";
            this.groupBoxItem.Size = new System.Drawing.Size(370, 180);
            this.groupBoxItem.TabIndex = 1;
            this.groupBoxItem.TabStop = false;
            this.groupBoxItem.Text = "Add / Update Item";

            // Labels + Textboxes
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(15, 30);
            this.lblItemName.Text = "Item Name:";
            this.txtItemName.Location = new System.Drawing.Point(100, 27);
            this.txtItemName.Size = new System.Drawing.Size(250, 23);

            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(15, 60);
            this.lblCategory.Text = "Category:";
            this.txtCategory.Location = new System.Drawing.Point(100, 57);
            this.txtCategory.Size = new System.Drawing.Size(250, 23);

            this.lblCostPrice.AutoSize = true;
            this.lblCostPrice.Location = new System.Drawing.Point(15, 90);
            this.lblCostPrice.Text = "Cost Price:";
            this.txtCostPrice.Location = new System.Drawing.Point(100, 87);
            this.txtCostPrice.Size = new System.Drawing.Size(100, 23);

            this.lblSellingPrice.AutoSize = true;
            this.lblSellingPrice.Location = new System.Drawing.Point(15, 120);
            this.lblSellingPrice.Text = "Selling Price:";
            this.txtSellingPrice.Location = new System.Drawing.Point(100, 117);
            this.txtSellingPrice.Size = new System.Drawing.Size(100, 23);

            // Buttons
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.Location = new System.Drawing.Point(220, 85);
            this.btnAddItem.Size = new System.Drawing.Size(120, 25);

            this.btnUpdateItem.Text = "Update Item";
            this.btnUpdateItem.Location = new System.Drawing.Point(220, 115);
            this.btnUpdateItem.Size = new System.Drawing.Size(120, 25);

            this.btnDeleteItem.Text = "Delete Item";
            this.btnDeleteItem.Location = new System.Drawing.Point(220, 145);
            this.btnDeleteItem.Size = new System.Drawing.Size(120, 25);

            // ========================
            // 🔹 GroupBox Stock
            // ========================
            this.groupBoxStock.Controls.Add(this.lblItemId);
            this.groupBoxStock.Controls.Add(this.lblQuantity);
            this.groupBoxStock.Controls.Add(this.txtItemId);
            this.groupBoxStock.Controls.Add(this.txtQuantity);
            this.groupBoxStock.Controls.Add(this.btnStockIn);
            this.groupBoxStock.Controls.Add(this.btnStockOut);
            this.groupBoxStock.Location = new System.Drawing.Point(400, 250);
            this.groupBoxStock.Name = "groupBoxStock";
            this.groupBoxStock.Size = new System.Drawing.Size(370, 180);
            this.groupBoxStock.TabIndex = 2;
            this.groupBoxStock.TabStop = false;
            this.groupBoxStock.Text = "Stock In / Out";

            this.lblItemId.AutoSize = true;
            this.lblItemId.Location = new System.Drawing.Point(15, 40);
            this.lblItemId.Text = "Item ID:";
            this.txtItemId.Location = new System.Drawing.Point(100, 37);
            this.txtItemId.Size = new System.Drawing.Size(100, 23);

            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(15, 70);
            this.lblQuantity.Text = "Quantity:";
            this.txtQuantity.Location = new System.Drawing.Point(100, 67);
            this.txtQuantity.Size = new System.Drawing.Size(100, 23);

            this.btnStockIn.Text = "Stock In";
            this.btnStockIn.Location = new System.Drawing.Point(220, 35);
            this.btnStockIn.Size = new System.Drawing.Size(120, 25);

            this.btnStockOut.Text = "Stock Out";
            this.btnStockOut.Location = new System.Drawing.Point(220, 65);
            this.btnStockOut.Size = new System.Drawing.Size(120, 25);

            // ========================
            // 🔹 Form Layout
            // ========================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
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
    }
}
