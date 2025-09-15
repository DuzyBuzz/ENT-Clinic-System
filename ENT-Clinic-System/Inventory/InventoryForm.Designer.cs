namespace ENT_Clinic_System.Inventory
{
    partial class InventoryForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.GroupBox groupBoxItem;
        private System.Windows.Forms.TextBox costPriceTextBox;
        private System.Windows.Forms.TextBox sellingPriceTextBox;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnUpdateItem;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblCostPrice;
        private System.Windows.Forms.Label lblSellingPrice;

        private System.Windows.Forms.GroupBox groupBoxStock;
        private System.Windows.Forms.TextBox itemIdTextBox;
        private System.Windows.Forms.TextBox quantityTextBox;
        private System.Windows.Forms.Button btnStockIn;
        private System.Windows.Forms.Button btnStockOut;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryForm));
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.groupBoxItem = new System.Windows.Forms.GroupBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.addItemNameComboBox = new System.Windows.Forms.ComboBox();
            this.addCategoryComboBox = new System.Windows.Forms.ComboBox();
            this.addDescriptionComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblCostPrice = new System.Windows.Forms.Label();
            this.lblSellingPrice = new System.Windows.Forms.Label();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnUpdateItem = new System.Windows.Forms.Button();
            this.costPriceTextBox = new System.Windows.Forms.TextBox();
            this.sellingPriceTextBox = new System.Windows.Forms.TextBox();
            this.groupBoxStock = new System.Windows.Forms.GroupBox();
            this.expirationDateCheckBox = new System.Windows.Forms.CheckBox();
            this.expirationDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.discountCheckBox = new System.Windows.Forms.CheckBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.itemIdTextBox = new System.Windows.Forms.TextBox();
            this.quantityTextBox = new System.Windows.Forms.TextBox();
            this.btnStockIn = new System.Windows.Forms.Button();
            this.btnStockOut = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.refreshPatientsButton = new System.Windows.Forms.Button();
            this.searchPatientButton = new System.Windows.Forms.Button();
            this.searchItemsTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.categoryCombobox = new System.Windows.Forms.ComboBox();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.groupBoxItem.SuspendLayout();
            this.groupBoxStock.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.dgvItems.Location = new System.Drawing.Point(12, 73);
            this.dgvItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(795, 346);
            this.dgvItems.TabIndex = 0;
            this.dgvItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellClick);
            // 
            // groupBoxItem
            // 
            this.groupBoxItem.Controls.Add(this.clearButton);
            this.groupBoxItem.Controls.Add(this.addItemNameComboBox);
            this.groupBoxItem.Controls.Add(this.addCategoryComboBox);
            this.groupBoxItem.Controls.Add(this.addDescriptionComboBox);
            this.groupBoxItem.Controls.Add(this.label1);
            this.groupBoxItem.Controls.Add(this.lblItemName);
            this.groupBoxItem.Controls.Add(this.lblCategory);
            this.groupBoxItem.Controls.Add(this.lblCostPrice);
            this.groupBoxItem.Controls.Add(this.lblSellingPrice);
            this.groupBoxItem.Controls.Add(this.btnAddItem);
            this.groupBoxItem.Controls.Add(this.btnUpdateItem);
            this.groupBoxItem.Controls.Add(this.costPriceTextBox);
            this.groupBoxItem.Controls.Add(this.sellingPriceTextBox);
            this.groupBoxItem.Location = new System.Drawing.Point(12, 429);
            this.groupBoxItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxItem.Name = "groupBoxItem";
            this.groupBoxItem.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxItem.Size = new System.Drawing.Size(406, 295);
            this.groupBoxItem.TabIndex = 1;
            this.groupBoxItem.TabStop = false;
            this.groupBoxItem.Text = "Add / Update Item";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(299, 245);
            this.clearButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(90, 34);
            this.clearButton.TabIndex = 15;
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // addItemNameComboBox
            // 
            this.addItemNameComboBox.FormattingEnabled = true;
            this.addItemNameComboBox.Location = new System.Drawing.Point(118, 34);
            this.addItemNameComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.addItemNameComboBox.Name = "addItemNameComboBox";
            this.addItemNameComboBox.Size = new System.Drawing.Size(269, 28);
            this.addItemNameComboBox.TabIndex = 14;
            this.addItemNameComboBox.TextChanged += new System.EventHandler(this.addItemNameComboBox_TextChanged);
            // 
            // addCategoryComboBox
            // 
            this.addCategoryComboBox.FormattingEnabled = true;
            this.addCategoryComboBox.Location = new System.Drawing.Point(118, 115);
            this.addCategoryComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.addCategoryComboBox.Name = "addCategoryComboBox";
            this.addCategoryComboBox.Size = new System.Drawing.Size(269, 28);
            this.addCategoryComboBox.TabIndex = 13;
            this.addCategoryComboBox.TextChanged += new System.EventHandler(this.addCategoryComboBox_TextChanged);
            // 
            // addDescriptionComboBox
            // 
            this.addDescriptionComboBox.FormattingEnabled = true;
            this.addDescriptionComboBox.Location = new System.Drawing.Point(118, 74);
            this.addDescriptionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.addDescriptionComboBox.Name = "addDescriptionComboBox";
            this.addDescriptionComboBox.Size = new System.Drawing.Size(269, 28);
            this.addDescriptionComboBox.TabIndex = 12;
            this.addDescriptionComboBox.TextChanged += new System.EventHandler(this.addDescriptionComboBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 120);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Category:";
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(15, 40);
            this.lblItemName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(73, 20);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "Item Name:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(15, 80);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(75, 20);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Description:";
            // 
            // lblCostPrice
            // 
            this.lblCostPrice.AutoSize = true;
            this.lblCostPrice.Location = new System.Drawing.Point(15, 160);
            this.lblCostPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCostPrice.Name = "lblCostPrice";
            this.lblCostPrice.Size = new System.Drawing.Size(69, 20);
            this.lblCostPrice.TabIndex = 2;
            this.lblCostPrice.Text = "Cost Price:";
            // 
            // lblSellingPrice
            // 
            this.lblSellingPrice.AutoSize = true;
            this.lblSellingPrice.Location = new System.Drawing.Point(15, 200);
            this.lblSellingPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSellingPrice.Name = "lblSellingPrice";
            this.lblSellingPrice.Size = new System.Drawing.Size(82, 20);
            this.lblSellingPrice.TabIndex = 3;
            this.lblSellingPrice.Text = "Selling Price:";
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(49, 245);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(90, 34);
            this.btnAddItem.TabIndex = 8;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click_1);
            // 
            // btnUpdateItem
            // 
            this.btnUpdateItem.Location = new System.Drawing.Point(174, 245);
            this.btnUpdateItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUpdateItem.Name = "btnUpdateItem";
            this.btnUpdateItem.Size = new System.Drawing.Size(90, 34);
            this.btnUpdateItem.TabIndex = 9;
            this.btnUpdateItem.Text = "Update Item";
            // 
            // costPriceTextBox
            // 
            this.costPriceTextBox.Location = new System.Drawing.Point(118, 155);
            this.costPriceTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.costPriceTextBox.Name = "costPriceTextBox";
            this.costPriceTextBox.Size = new System.Drawing.Size(269, 25);
            this.costPriceTextBox.TabIndex = 6;
            // 
            // sellingPriceTextBox
            // 
            this.sellingPriceTextBox.Location = new System.Drawing.Point(118, 195);
            this.sellingPriceTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sellingPriceTextBox.Name = "sellingPriceTextBox";
            this.sellingPriceTextBox.Size = new System.Drawing.Size(269, 25);
            this.sellingPriceTextBox.TabIndex = 7;
            // 
            // groupBoxStock
            // 
            this.groupBoxStock.Controls.Add(this.expirationDateCheckBox);
            this.groupBoxStock.Controls.Add(this.expirationDateTimePicker);
            this.groupBoxStock.Controls.Add(this.label7);
            this.groupBoxStock.Controls.Add(this.label3);
            this.groupBoxStock.Controls.Add(this.discountCheckBox);
            this.groupBoxStock.Controls.Add(this.lblQuantity);
            this.groupBoxStock.Controls.Add(this.itemIdTextBox);
            this.groupBoxStock.Controls.Add(this.quantityTextBox);
            this.groupBoxStock.Controls.Add(this.btnStockIn);
            this.groupBoxStock.Controls.Add(this.btnStockOut);
            this.groupBoxStock.Location = new System.Drawing.Point(426, 429);
            this.groupBoxStock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxStock.Name = "groupBoxStock";
            this.groupBoxStock.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxStock.Size = new System.Drawing.Size(381, 295);
            this.groupBoxStock.TabIndex = 2;
            this.groupBoxStock.TabStop = false;
            this.groupBoxStock.Text = "Stock In";
            // 
            // expirationDateCheckBox
            // 
            this.expirationDateCheckBox.AutoSize = true;
            this.expirationDateCheckBox.Location = new System.Drawing.Point(119, 120);
            this.expirationDateCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.expirationDateCheckBox.Name = "expirationDateCheckBox";
            this.expirationDateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.expirationDateCheckBox.TabIndex = 24;
            this.expirationDateCheckBox.UseVisualStyleBackColor = true;
            this.expirationDateCheckBox.CheckedChanged += new System.EventHandler(this.expirationDateCheckBox_CheckedChanged);
            // 
            // expirationDateTimePicker
            // 
            this.expirationDateTimePicker.Enabled = false;
            this.expirationDateTimePicker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.expirationDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.expirationDateTimePicker.Location = new System.Drawing.Point(141, 113);
            this.expirationDateTimePicker.Name = "expirationDateTimePicker";
            this.expirationDateTimePicker.Size = new System.Drawing.Size(214, 25);
            this.expirationDateTimePicker.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 40);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 20);
            this.label7.TabIndex = 23;
            this.label7.Text = "Item Id:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 117);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Expiration Date:";
            // 
            // discountCheckBox
            // 
            this.discountCheckBox.AutoSize = true;
            this.discountCheckBox.Location = new System.Drawing.Point(20, 195);
            this.discountCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.discountCheckBox.Name = "discountCheckBox";
            this.discountCheckBox.Size = new System.Drawing.Size(89, 24);
            this.discountCheckBox.TabIndex = 6;
            this.discountCheckBox.Text = "discounted";
            this.discountCheckBox.UseVisualStyleBackColor = true;
            this.discountCheckBox.Visible = false;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(16, 80);
            this.lblQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(57, 20);
            this.lblQuantity.TabIndex = 1;
            this.lblQuantity.Text = "Quantity:";
            // 
            // itemIdTextBox
            // 
            this.itemIdTextBox.Enabled = false;
            this.itemIdTextBox.Location = new System.Drawing.Point(119, 37);
            this.itemIdTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.itemIdTextBox.Name = "itemIdTextBox";
            this.itemIdTextBox.Size = new System.Drawing.Size(236, 25);
            this.itemIdTextBox.TabIndex = 2;
            // 
            // quantityTextBox
            // 
            this.quantityTextBox.Location = new System.Drawing.Point(119, 77);
            this.quantityTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.quantityTextBox.Name = "quantityTextBox";
            this.quantityTextBox.Size = new System.Drawing.Size(236, 25);
            this.quantityTextBox.TabIndex = 3;
            // 
            // btnStockIn
            // 
            this.btnStockIn.Location = new System.Drawing.Point(277, 153);
            this.btnStockIn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStockIn.Name = "btnStockIn";
            this.btnStockIn.Size = new System.Drawing.Size(78, 34);
            this.btnStockIn.TabIndex = 4;
            this.btnStockIn.Text = "Stock In";
            // 
            // btnStockOut
            // 
            this.btnStockOut.Location = new System.Drawing.Point(20, 229);
            this.btnStockOut.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStockOut.Name = "btnStockOut";
            this.btnStockOut.Size = new System.Drawing.Size(120, 34);
            this.btnStockOut.TabIndex = 5;
            this.btnStockOut.Text = "Stock Out";
            this.btnStockOut.Visible = false;
            this.btnStockOut.Click += new System.EventHandler(this.btnStockOut_Click_1);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.72161F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.16484F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.250305F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.494505F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.40171F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.42002F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.424909F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.refreshPatientsButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchPatientButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchItemsTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.categoryCombobox, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(819, 60);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(476, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Category:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // refreshPatientsButton
            // 
            this.refreshPatientsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.refreshPatientsButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshPatientsButton.Location = new System.Drawing.Point(431, 20);
            this.refreshPatientsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.refreshPatientsButton.Name = "refreshPatientsButton";
            this.refreshPatientsButton.Size = new System.Drawing.Size(37, 35);
            this.refreshPatientsButton.TabIndex = 3;
            this.refreshPatientsButton.Text = "⟳";
            this.refreshPatientsButton.UseVisualStyleBackColor = true;
            this.refreshPatientsButton.Click += new System.EventHandler(this.refreshPatientsButton_Click);
            // 
            // searchPatientButton
            // 
            this.searchPatientButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchPatientButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchPatientButton.Location = new System.Drawing.Point(388, 20);
            this.searchPatientButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchPatientButton.Name = "searchPatientButton";
            this.searchPatientButton.Size = new System.Drawing.Size(35, 35);
            this.searchPatientButton.TabIndex = 0;
            this.searchPatientButton.Text = "🔎";
            this.searchPatientButton.UseVisualStyleBackColor = true;
            this.searchPatientButton.Click += new System.EventHandler(this.searchPatientButton_Click);
            // 
            // searchItemsTextBox
            // 
            this.searchItemsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchItemsTextBox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchItemsTextBox.Location = new System.Drawing.Point(100, 29);
            this.searchItemsTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchItemsTextBox.Name = "searchItemsTextBox";
            this.searchItemsTextBox.Size = new System.Drawing.Size(280, 26);
            this.searchItemsTextBox.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 40);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "Search Item:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // categoryCombobox
            // 
            this.categoryCombobox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.categoryCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryCombobox.FormattingEnabled = true;
            this.categoryCombobox.Location = new System.Drawing.Point(553, 31);
            this.categoryCombobox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.categoryCombobox.Name = "categoryCombobox";
            this.categoryCombobox.Size = new System.Drawing.Size(192, 28);
            this.categoryCombobox.TabIndex = 4;
            this.categoryCombobox.SelectedIndexChanged += new System.EventHandler(this.categoryCombobox_SelectedIndexChanged);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(269, 748);
            this.btnDeleteItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(90, 34);
            this.btnDeleteItem.TabIndex = 10;
            this.btnDeleteItem.Text = "Delete Item";
            this.btnDeleteItem.Visible = false;
            // 
            // InventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 741);
            this.Controls.Add(this.groupBoxStock);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.groupBoxItem);
            this.Controls.Add(this.btnDeleteItem);
            this.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "InventoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory Management - ENT Clinic";
            this.Load += new System.EventHandler(this.InventoryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.groupBoxItem.ResumeLayout(false);
            this.groupBoxItem.PerformLayout();
            this.groupBoxStock.ResumeLayout(false);
            this.groupBoxStock.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox discountCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button refreshPatientsButton;
        private System.Windows.Forms.Button searchPatientButton;
        private System.Windows.Forms.TextBox searchItemsTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox categoryCombobox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox addDescriptionComboBox;
        private System.Windows.Forms.ComboBox addCategoryComboBox;
        private System.Windows.Forms.ComboBox addItemNameComboBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker expirationDateTimePicker;
        private System.Windows.Forms.CheckBox expirationDateCheckBox;
        private System.Windows.Forms.Button btnDeleteItem;
    }
}
