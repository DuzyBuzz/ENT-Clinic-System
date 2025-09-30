namespace ENT_Clinic_System.Inventory
{
    partial class InventoryForm
    {
        private System.ComponentModel.IContainer components = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryForm));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.refreshPatientsButton = new System.Windows.Forms.Button();
            this.searchPatientButton = new System.Windows.Forms.Button();
            this.searchItemsTextBox = new System.Windows.Forms.TextBox();
            this.categoryCombobox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.item_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cost_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selling_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock_quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxItem = new System.Windows.Forms.GroupBox();
            this.sellingNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.costPriceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.clearButton = new System.Windows.Forms.Button();
            this.addItemNameComboBox = new System.Windows.Forms.ComboBox();
            this.addCategoryComboBox = new System.Windows.Forms.ComboBox();
            this.addDescriptionComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblCostPrice = new System.Windows.Forms.Label();
            this.lblSellingPrice = new System.Windows.Forms.Label();
            this.addItemButton = new System.Windows.Forms.Button();
            this.updateItemButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.movementDataGridView = new System.Windows.Forms.DataGridView();
            this.movement_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.movement_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.movement_quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.movement_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expiration_date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.movementDateToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.movementDateFromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.groupBoxStock = new System.Windows.Forms.GroupBox();
            this.quantityNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.expirationDateCheckBox = new System.Windows.Forms.CheckBox();
            this.expirationDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.discountCheckBox = new System.Windows.Forms.CheckBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.itemIdTextBox = new System.Windows.Forms.TextBox();
            this.stockInButton = new System.Windows.Forms.Button();
            this.stockOutButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.groupBoxItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sellingNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPriceNumericUpDown)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.movementDataGridView)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBoxStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.38461F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.34978F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.250305F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.494505F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.40171F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.42002F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.refreshPatientsButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchPatientButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchItemsTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.categoryCombobox, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(787, 42);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(498, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 42);
            this.label2.TabIndex = 5;
            this.label2.Text = "Category:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // refreshPatientsButton
            // 
            this.refreshPatientsButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.refreshPatientsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.refreshPatientsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshPatientsButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshPatientsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.refreshPatientsButton.Location = new System.Drawing.Point(451, 5);
            this.refreshPatientsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.refreshPatientsButton.Name = "refreshPatientsButton";
            this.refreshPatientsButton.Size = new System.Drawing.Size(39, 32);
            this.refreshPatientsButton.TabIndex = 3;
            this.refreshPatientsButton.Text = "⟳";
            this.refreshPatientsButton.UseVisualStyleBackColor = false;
            this.refreshPatientsButton.Click += new System.EventHandler(this.refreshPatientsButton_Click);
            // 
            // searchPatientButton
            // 
            this.searchPatientButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.searchPatientButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchPatientButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchPatientButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchPatientButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchPatientButton.Location = new System.Drawing.Point(406, 5);
            this.searchPatientButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchPatientButton.Name = "searchPatientButton";
            this.searchPatientButton.Size = new System.Drawing.Size(37, 32);
            this.searchPatientButton.TabIndex = 0;
            this.searchPatientButton.Text = "🔎";
            this.searchPatientButton.UseVisualStyleBackColor = false;
            this.searchPatientButton.Click += new System.EventHandler(this.searchPatientButton_Click);
            // 
            // searchItemsTextBox
            // 
            this.searchItemsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchItemsTextBox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchItemsTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchItemsTextBox.Location = new System.Drawing.Point(136, 11);
            this.searchItemsTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchItemsTextBox.Name = "searchItemsTextBox";
            this.searchItemsTextBox.Size = new System.Drawing.Size(262, 26);
            this.searchItemsTextBox.TabIndex = 1;
            // 
            // categoryCombobox
            // 
            this.categoryCombobox.BackColor = System.Drawing.SystemColors.Control;
            this.categoryCombobox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.categoryCombobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.categoryCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.categoryCombobox.FormattingEnabled = true;
            this.categoryCombobox.Location = new System.Drawing.Point(579, 13);
            this.categoryCombobox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.categoryCombobox.Name = "categoryCombobox";
            this.categoryCombobox.Size = new System.Drawing.Size(204, 28);
            this.categoryCombobox.TabIndex = 4;
            this.categoryCombobox.SelectedIndexChanged += new System.EventHandler(this.categoryCombobox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(4, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 42);
            this.label8.TabIndex = 2;
            this.label8.Text = "Search Item:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 927);
            this.panel1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBoxItem);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(787, 885);
            this.panel2.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.dgvItems);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(787, 714);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clinic Items";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item_id,
            this.item_name,
            this.description,
            this.category,
            this.cost_price,
            this.selling_price,
            this.stock_quantity});
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(3, 21);
            this.dgvItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(781, 690);
            this.dgvItems.TabIndex = 3;
            this.dgvItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellClick);
            // 
            // item_id
            // 
            this.item_id.DataPropertyName = "item_id";
            this.item_id.HeaderText = "Item ID";
            this.item_id.Name = "item_id";
            // 
            // item_name
            // 
            this.item_name.DataPropertyName = "item_name";
            this.item_name.HeaderText = "Item Name";
            this.item_name.Name = "item_name";
            // 
            // description
            // 
            this.description.DataPropertyName = "description";
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            // 
            // category
            // 
            this.category.DataPropertyName = "category";
            this.category.HeaderText = "Category";
            this.category.Name = "category";
            // 
            // cost_price
            // 
            this.cost_price.DataPropertyName = "cost_price";
            this.cost_price.HeaderText = "Cost Price";
            this.cost_price.Name = "cost_price";
            // 
            // selling_price
            // 
            this.selling_price.DataPropertyName = "selling_price";
            this.selling_price.HeaderText = "Selling Price";
            this.selling_price.Name = "selling_price";
            // 
            // stock_quantity
            // 
            this.stock_quantity.DataPropertyName = "stock_quantity";
            this.stock_quantity.HeaderText = "Stock Quantity";
            this.stock_quantity.Name = "stock_quantity";
            // 
            // groupBoxItem
            // 
            this.groupBoxItem.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxItem.Controls.Add(this.sellingNumericUpDown);
            this.groupBoxItem.Controls.Add(this.costPriceNumericUpDown);
            this.groupBoxItem.Controls.Add(this.clearButton);
            this.groupBoxItem.Controls.Add(this.addItemNameComboBox);
            this.groupBoxItem.Controls.Add(this.addCategoryComboBox);
            this.groupBoxItem.Controls.Add(this.addDescriptionComboBox);
            this.groupBoxItem.Controls.Add(this.label1);
            this.groupBoxItem.Controls.Add(this.lblItemName);
            this.groupBoxItem.Controls.Add(this.lblCategory);
            this.groupBoxItem.Controls.Add(this.lblCostPrice);
            this.groupBoxItem.Controls.Add(this.lblSellingPrice);
            this.groupBoxItem.Controls.Add(this.addItemButton);
            this.groupBoxItem.Controls.Add(this.updateItemButton);
            this.groupBoxItem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxItem.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxItem.Location = new System.Drawing.Point(0, 714);
            this.groupBoxItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxItem.Name = "groupBoxItem";
            this.groupBoxItem.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxItem.Size = new System.Drawing.Size(787, 171);
            this.groupBoxItem.TabIndex = 4;
            this.groupBoxItem.TabStop = false;
            this.groupBoxItem.Text = "Add / Update Item";
            // 
            // sellingNumericUpDown
            // 
            this.sellingNumericUpDown.DecimalPlaces = 2;
            this.sellingNumericUpDown.Location = new System.Drawing.Point(507, 69);
            this.sellingNumericUpDown.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.sellingNumericUpDown.Name = "sellingNumericUpDown";
            this.sellingNumericUpDown.Size = new System.Drawing.Size(200, 25);
            this.sellingNumericUpDown.TabIndex = 17;
            // 
            // costPriceNumericUpDown
            // 
            this.costPriceNumericUpDown.DecimalPlaces = 2;
            this.costPriceNumericUpDown.Location = new System.Drawing.Point(507, 28);
            this.costPriceNumericUpDown.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.costPriceNumericUpDown.Name = "costPriceNumericUpDown";
            this.costPriceNumericUpDown.Size = new System.Drawing.Size(200, 25);
            this.costPriceNumericUpDown.TabIndex = 16;
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.SystemColors.Control;
            this.clearButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clearButton.Location = new System.Drawing.Point(648, 109);
            this.clearButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(59, 34);
            this.clearButton.TabIndex = 15;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // addItemNameComboBox
            // 
            this.addItemNameComboBox.FormattingEnabled = true;
            this.addItemNameComboBox.Location = new System.Drawing.Point(118, 67);
            this.addItemNameComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.addItemNameComboBox.Name = "addItemNameComboBox";
            this.addItemNameComboBox.Size = new System.Drawing.Size(200, 26);
            this.addItemNameComboBox.TabIndex = 14;
            // 
            // addCategoryComboBox
            // 
            this.addCategoryComboBox.FormattingEnabled = true;
            this.addCategoryComboBox.Location = new System.Drawing.Point(117, 28);
            this.addCategoryComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.addCategoryComboBox.Name = "addCategoryComboBox";
            this.addCategoryComboBox.Size = new System.Drawing.Size(200, 26);
            this.addCategoryComboBox.TabIndex = 13;
            // 
            // addDescriptionComboBox
            // 
            this.addDescriptionComboBox.FormattingEnabled = true;
            this.addDescriptionComboBox.Location = new System.Drawing.Point(118, 106);
            this.addDescriptionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.addDescriptionComboBox.Name = "addDescriptionComboBox";
            this.addDescriptionComboBox.Size = new System.Drawing.Size(200, 26);
            this.addDescriptionComboBox.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "Category:";
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(15, 73);
            this.lblItemName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(86, 18);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "Item Name:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(15, 112);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(94, 18);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Description:";
            // 
            // lblCostPrice
            // 
            this.lblCostPrice.AutoSize = true;
            this.lblCostPrice.Location = new System.Drawing.Point(404, 31);
            this.lblCostPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCostPrice.Name = "lblCostPrice";
            this.lblCostPrice.Size = new System.Drawing.Size(86, 18);
            this.lblCostPrice.TabIndex = 2;
            this.lblCostPrice.Text = "Cost Price:";
            // 
            // lblSellingPrice
            // 
            this.lblSellingPrice.AutoSize = true;
            this.lblSellingPrice.Location = new System.Drawing.Point(404, 71);
            this.lblSellingPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSellingPrice.Name = "lblSellingPrice";
            this.lblSellingPrice.Size = new System.Drawing.Size(102, 18);
            this.lblSellingPrice.TabIndex = 3;
            this.lblSellingPrice.Text = "Selling Price:";
            // 
            // addItemButton
            // 
            this.addItemButton.BackColor = System.Drawing.SystemColors.Control;
            this.addItemButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addItemButton.Location = new System.Drawing.Point(407, 109);
            this.addItemButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addItemButton.Name = "addItemButton";
            this.addItemButton.Size = new System.Drawing.Size(105, 34);
            this.addItemButton.TabIndex = 8;
            this.addItemButton.Text = "Add Item";
            this.addItemButton.UseVisualStyleBackColor = false;
            this.addItemButton.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // updateItemButton
            // 
            this.updateItemButton.BackColor = System.Drawing.SystemColors.Control;
            this.updateItemButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateItemButton.Location = new System.Drawing.Point(529, 109);
            this.updateItemButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.updateItemButton.Name = "updateItemButton";
            this.updateItemButton.Size = new System.Drawing.Size(105, 34);
            this.updateItemButton.TabIndex = 9;
            this.updateItemButton.Text = "Update Item";
            this.updateItemButton.UseVisualStyleBackColor = false;
            this.updateItemButton.Click += new System.EventHandler(this.btnUpdateItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1586, 933);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.tableLayoutPanel3);
            this.panel3.Controls.Add(this.groupBoxStock);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(796, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(787, 927);
            this.panel3.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.movementDataGridView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(0, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(787, 800);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stock Movements";
            // 
            // movementDataGridView
            // 
            this.movementDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.movementDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.movementDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.movementDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.movementDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.movementDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.movementDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.movement_id,
            this.itemId,
            this.movement_type,
            this.movement_quantity,
            this.movement_date,
            this.expiration_date});
            this.movementDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movementDataGridView.Location = new System.Drawing.Point(3, 21);
            this.movementDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.movementDataGridView.MultiSelect = false;
            this.movementDataGridView.Name = "movementDataGridView";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.movementDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.movementDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.movementDataGridView.Size = new System.Drawing.Size(781, 776);
            this.movementDataGridView.TabIndex = 6;
            // 
            // movement_id
            // 
            this.movement_id.DataPropertyName = "movement_id";
            this.movement_id.HeaderText = "Movement ID";
            this.movement_id.Name = "movement_id";
            this.movement_id.ReadOnly = true;
            // 
            // itemId
            // 
            this.itemId.DataPropertyName = "item_id";
            this.itemId.HeaderText = "Item ID";
            this.itemId.Name = "itemId";
            this.itemId.ReadOnly = true;
            // 
            // movement_type
            // 
            this.movement_type.DataPropertyName = "movement_type";
            this.movement_type.HeaderText = "Movement";
            this.movement_type.Name = "movement_type";
            this.movement_type.ReadOnly = true;
            // 
            // movement_quantity
            // 
            this.movement_quantity.DataPropertyName = "quantity";
            this.movement_quantity.HeaderText = "Quantity";
            this.movement_quantity.Name = "movement_quantity";
            // 
            // movement_date
            // 
            this.movement_date.DataPropertyName = "movement_date";
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.movement_date.DefaultCellStyle = dataGridViewCellStyle4;
            this.movement_date.HeaderText = "Date";
            this.movement_date.Name = "movement_date";
            this.movement_date.ReadOnly = true;
            // 
            // expiration_date
            // 
            this.expiration_date.DataPropertyName = "expiration_date";
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.NullValue = null;
            this.expiration_date.DefaultCellStyle = dataGridViewCellStyle5;
            this.expiration_date.HeaderText = "Expiration Date";
            this.expiration_date.Name = "expiration_date";
            this.expiration_date.ReadOnly = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.68996F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.51207F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.767471F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.90343F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.label11, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.movementDateToDateTimePicker, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.movementDateFromDateTimePicker, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(787, 42);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(344, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 42);
            this.label11.TabIndex = 14;
            this.label11.Text = "Date To:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(4, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 42);
            this.label10.TabIndex = 13;
            this.label10.Text = "Date From:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // movementDateToDateTimePicker
            // 
            this.movementDateToDateTimePicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.movementDateToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.movementDateToDateTimePicker.Location = new System.Drawing.Point(419, 10);
            this.movementDateToDateTimePicker.Margin = new System.Windows.Forms.Padding(10);
            this.movementDateToDateTimePicker.Name = "movementDateToDateTimePicker";
            this.movementDateToDateTimePicker.Size = new System.Drawing.Size(110, 25);
            this.movementDateToDateTimePicker.TabIndex = 6;
            this.movementDateToDateTimePicker.ValueChanged += new System.EventHandler(this.movementDateToDateTimePicker_ValueChanged);
            // 
            // movementDateFromDateTimePicker
            // 
            this.movementDateFromDateTimePicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.movementDateFromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.movementDateFromDateTimePicker.Location = new System.Drawing.Point(102, 10);
            this.movementDateFromDateTimePicker.Margin = new System.Windows.Forms.Padding(10);
            this.movementDateFromDateTimePicker.Name = "movementDateFromDateTimePicker";
            this.movementDateFromDateTimePicker.Size = new System.Drawing.Size(110, 25);
            this.movementDateFromDateTimePicker.TabIndex = 8;
            this.movementDateFromDateTimePicker.ValueChanged += new System.EventHandler(this.movementDateFromDateTimePicker_ValueChanged);
            // 
            // groupBoxStock
            // 
            this.groupBoxStock.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxStock.Controls.Add(this.quantityNumericUpDown);
            this.groupBoxStock.Controls.Add(this.expirationDateCheckBox);
            this.groupBoxStock.Controls.Add(this.expirationDateTimePicker);
            this.groupBoxStock.Controls.Add(this.label7);
            this.groupBoxStock.Controls.Add(this.label3);
            this.groupBoxStock.Controls.Add(this.discountCheckBox);
            this.groupBoxStock.Controls.Add(this.lblQuantity);
            this.groupBoxStock.Controls.Add(this.itemIdTextBox);
            this.groupBoxStock.Controls.Add(this.stockInButton);
            this.groupBoxStock.Controls.Add(this.stockOutButton);
            this.groupBoxStock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxStock.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxStock.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxStock.Location = new System.Drawing.Point(0, 842);
            this.groupBoxStock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxStock.Name = "groupBoxStock";
            this.groupBoxStock.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxStock.Size = new System.Drawing.Size(787, 85);
            this.groupBoxStock.TabIndex = 5;
            this.groupBoxStock.TabStop = false;
            this.groupBoxStock.Text = "Stock In";
            // 
            // quantityNumericUpDown
            // 
            this.quantityNumericUpDown.Location = new System.Drawing.Point(233, 29);
            this.quantityNumericUpDown.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.quantityNumericUpDown.Name = "quantityNumericUpDown";
            this.quantityNumericUpDown.Size = new System.Drawing.Size(104, 25);
            this.quantityNumericUpDown.TabIndex = 25;
            this.quantityNumericUpDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.quantityNumericUpDown_KeyPress);
            // 
            // expirationDateCheckBox
            // 
            this.expirationDateCheckBox.AutoSize = true;
            this.expirationDateCheckBox.Location = new System.Drawing.Point(423, 34);
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
            this.expirationDateTimePicker.Location = new System.Drawing.Point(445, 27);
            this.expirationDateTimePicker.Name = "expirationDateTimePicker";
            this.expirationDateTimePicker.Size = new System.Drawing.Size(191, 25);
            this.expirationDateTimePicker.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 31);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 18);
            this.label7.TabIndex = 23;
            this.label7.Text = "Item ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(344, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 18);
            this.label3.TabIndex = 16;
            this.label3.Text = "Exp Date:";
            // 
            // discountCheckBox
            // 
            this.discountCheckBox.AutoSize = true;
            this.discountCheckBox.Location = new System.Drawing.Point(20, 295);
            this.discountCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.discountCheckBox.Name = "discountCheckBox";
            this.discountCheckBox.Size = new System.Drawing.Size(106, 22);
            this.discountCheckBox.TabIndex = 6;
            this.discountCheckBox.Text = "discounted";
            this.discountCheckBox.UseVisualStyleBackColor = true;
            this.discountCheckBox.Visible = false;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(155, 34);
            this.lblQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(71, 18);
            this.lblQuantity.TabIndex = 1;
            this.lblQuantity.Text = "Quantity:";
            // 
            // itemIdTextBox
            // 
            this.itemIdTextBox.Enabled = false;
            this.itemIdTextBox.Location = new System.Drawing.Point(71, 28);
            this.itemIdTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.itemIdTextBox.Name = "itemIdTextBox";
            this.itemIdTextBox.Size = new System.Drawing.Size(76, 25);
            this.itemIdTextBox.TabIndex = 2;
            // 
            // stockInButton
            // 
            this.stockInButton.BackColor = System.Drawing.SystemColors.Control;
            this.stockInButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stockInButton.Location = new System.Drawing.Point(665, 20);
            this.stockInButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stockInButton.Name = "stockInButton";
            this.stockInButton.Size = new System.Drawing.Size(105, 34);
            this.stockInButton.TabIndex = 4;
            this.stockInButton.Text = "Stock In";
            this.stockInButton.UseVisualStyleBackColor = false;
            this.stockInButton.Click += new System.EventHandler(this.btnStockIn_Click);
            // 
            // stockOutButton
            // 
            this.stockOutButton.BackColor = System.Drawing.SystemColors.Control;
            this.stockOutButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stockOutButton.Location = new System.Drawing.Point(20, 329);
            this.stockOutButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stockOutButton.Name = "stockOutButton";
            this.stockOutButton.Size = new System.Drawing.Size(105, 34);
            this.stockOutButton.TabIndex = 5;
            this.stockOutButton.Text = "Stock Out";
            this.stockOutButton.UseVisualStyleBackColor = false;
            this.stockOutButton.Visible = false;
            // 
            // InventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1586, 933);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "InventoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inventory Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.InventoryForm_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.groupBoxItem.ResumeLayout(false);
            this.groupBoxItem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sellingNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPriceNumericUpDown)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.movementDataGridView)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBoxStock.ResumeLayout(false);
            this.groupBoxStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button refreshPatientsButton;
        private System.Windows.Forms.Button searchPatientButton;
        private System.Windows.Forms.TextBox searchItemsTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox categoryCombobox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.GroupBox groupBoxItem;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ComboBox addItemNameComboBox;
        private System.Windows.Forms.ComboBox addCategoryComboBox;
        private System.Windows.Forms.ComboBox addDescriptionComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblCostPrice;
        private System.Windows.Forms.Label lblSellingPrice;
        private System.Windows.Forms.Button addItemButton;
        private System.Windows.Forms.Button updateItemButton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBoxStock;
        private System.Windows.Forms.CheckBox expirationDateCheckBox;
        private System.Windows.Forms.DateTimePicker expirationDateTimePicker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox discountCheckBox;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox itemIdTextBox;
        private System.Windows.Forms.Button stockInButton;
        private System.Windows.Forms.Button stockOutButton;
        private System.Windows.Forms.DataGridView movementDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker movementDateToDateTimePicker;
        private System.Windows.Forms.DateTimePicker movementDateFromDateTimePicker;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn category;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn selling_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock_quantity;
        private System.Windows.Forms.NumericUpDown sellingNumericUpDown;
        private System.Windows.Forms.NumericUpDown costPriceNumericUpDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn movement_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn movement_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn movement_quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn movement_date;
        private System.Windows.Forms.DataGridViewTextBoxColumn expiration_date;
        private System.Windows.Forms.NumericUpDown quantityNumericUpDown;
    }
}
