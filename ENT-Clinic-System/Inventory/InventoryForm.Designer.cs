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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryForm));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.refreshPatientsButton = new System.Windows.Forms.Button();
            this.searchPatientButton = new System.Windows.Forms.Button();
            this.searchItemsTextBox = new System.Windows.Forms.TextBox();
            this.sortCategoryCombobox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.writeOffButton = new System.Windows.Forms.Button();
            this.descriptionComboBox = new System.Windows.Forms.ComboBox();
            this.sellingNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.updateItemButton = new System.Windows.Forms.Button();
            this.addItemButton = new System.Windows.Forms.Button();
            this.genericNameComboBox = new System.Windows.Forms.ComboBox();
            this.costPriceNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblSellingPrice = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.lblCostPrice = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.brandNameComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.dosageComboBox = new System.Windows.Forms.ComboBox();
            this.stregnthComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxStock = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.stockInButton = new System.Windows.Forms.Button();
            this.itemIdTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.expirationDateCheckBox = new System.Windows.Forms.CheckBox();
            this.expirationDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.quantityNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.discountCheckBox = new System.Windows.Forms.CheckBox();
            this.stockOutButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.movementDataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.movementDateToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.movementDateFromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sellingNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPriceNumericUpDown)).BeginInit();
            this.groupBoxStock.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNumericUpDown)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.movementDataGridView)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
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
            this.tableLayoutPanel2.Controls.Add(this.sortCategoryCombobox, 5, 0);
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
            // sortCategoryCombobox
            // 
            this.sortCategoryCombobox.BackColor = System.Drawing.SystemColors.Control;
            this.sortCategoryCombobox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sortCategoryCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortCategoryCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sortCategoryCombobox.FormattingEnabled = true;
            this.sortCategoryCombobox.Location = new System.Drawing.Point(579, 6);
            this.sortCategoryCombobox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.sortCategoryCombobox.Name = "sortCategoryCombobox";
            this.sortCategoryCombobox.Size = new System.Drawing.Size(204, 28);
            this.sortCategoryCombobox.TabIndex = 4;
            this.sortCategoryCombobox.SelectedIndexChanged += new System.EventHandler(this.categoryCombobox_SelectedIndexChanged);
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
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBoxStock);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(787, 885);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clinic Items";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowDrop = true;
            this.dgvItems.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvItems.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(3, 21);
            this.dgvItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(781, 615);
            this.dgvItems.TabIndex = 28;
            this.dgvItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellClick);
            this.dgvItems.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItems_CellContentClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(3, 636);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(781, 181);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Item Management";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.80645F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.19355F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.80645F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.19355F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.clearButton, 4, 3);
            this.tableLayoutPanel4.Controls.Add(this.writeOffButton, 4, 2);
            this.tableLayoutPanel4.Controls.Add(this.descriptionComboBox, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.sellingNumericUpDown, 3, 3);
            this.tableLayoutPanel4.Controls.Add(this.updateItemButton, 4, 1);
            this.tableLayoutPanel4.Controls.Add(this.addItemButton, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.genericNameComboBox, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.costPriceNumericUpDown, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.lblSellingPrice, 2, 3);
            this.tableLayoutPanel4.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblItemName, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblCostPrice, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.categoryComboBox, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.brandNameComboBox, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblCategory, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.dosageComboBox, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.stregnthComboBox, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(775, 157);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 39);
            this.label1.TabIndex = 11;
            this.label1.Text = "Generic Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.SystemColors.Control;
            this.clearButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clearButton.ForeColor = System.Drawing.Color.Red;
            this.clearButton.Location = new System.Drawing.Point(622, 122);
            this.clearButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(149, 30);
            this.clearButton.TabIndex = 15;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // writeOffButton
            // 
            this.writeOffButton.BackColor = System.Drawing.SystemColors.Control;
            this.writeOffButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.writeOffButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.writeOffButton.Location = new System.Drawing.Point(622, 83);
            this.writeOffButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.writeOffButton.Name = "writeOffButton";
            this.writeOffButton.Size = new System.Drawing.Size(149, 29);
            this.writeOffButton.TabIndex = 18;
            this.writeOffButton.Text = "Write-Off";
            this.writeOffButton.UseVisualStyleBackColor = false;
            this.writeOffButton.Click += new System.EventHandler(this.writeOffButton_Click);
            // 
            // descriptionComboBox
            // 
            this.descriptionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionComboBox.FormattingEnabled = true;
            this.descriptionComboBox.Location = new System.Drawing.Point(419, 44);
            this.descriptionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.descriptionComboBox.Name = "descriptionComboBox";
            this.descriptionComboBox.Size = new System.Drawing.Size(195, 24);
            this.descriptionComboBox.TabIndex = 24;
            // 
            // sellingNumericUpDown
            // 
            this.sellingNumericUpDown.DecimalPlaces = 2;
            this.sellingNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sellingNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellingNumericUpDown.Location = new System.Drawing.Point(418, 120);
            this.sellingNumericUpDown.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.sellingNumericUpDown.Name = "sellingNumericUpDown";
            this.sellingNumericUpDown.Size = new System.Drawing.Size(197, 22);
            this.sellingNumericUpDown.TabIndex = 17;
            // 
            // updateItemButton
            // 
            this.updateItemButton.BackColor = System.Drawing.SystemColors.Control;
            this.updateItemButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateItemButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateItemButton.Location = new System.Drawing.Point(622, 44);
            this.updateItemButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.updateItemButton.Name = "updateItemButton";
            this.updateItemButton.Size = new System.Drawing.Size(149, 29);
            this.updateItemButton.TabIndex = 9;
            this.updateItemButton.Text = "Update Item";
            this.updateItemButton.UseVisualStyleBackColor = false;
            this.updateItemButton.Click += new System.EventHandler(this.btnUpdateItem_Click);
            // 
            // addItemButton
            // 
            this.addItemButton.BackColor = System.Drawing.SystemColors.Control;
            this.addItemButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addItemButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addItemButton.Location = new System.Drawing.Point(622, 5);
            this.addItemButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addItemButton.Name = "addItemButton";
            this.addItemButton.Size = new System.Drawing.Size(149, 29);
            this.addItemButton.TabIndex = 8;
            this.addItemButton.Text = "Add Item";
            this.addItemButton.UseVisualStyleBackColor = false;
            this.addItemButton.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // genericNameComboBox
            // 
            this.genericNameComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.genericNameComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genericNameComboBox.FormattingEnabled = true;
            this.genericNameComboBox.Location = new System.Drawing.Point(110, 5);
            this.genericNameComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.genericNameComboBox.Name = "genericNameComboBox";
            this.genericNameComboBox.Size = new System.Drawing.Size(195, 24);
            this.genericNameComboBox.TabIndex = 13;
            // 
            // costPriceNumericUpDown
            // 
            this.costPriceNumericUpDown.DecimalPlaces = 2;
            this.costPriceNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.costPriceNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.costPriceNumericUpDown.Location = new System.Drawing.Point(418, 81);
            this.costPriceNumericUpDown.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.costPriceNumericUpDown.Name = "costPriceNumericUpDown";
            this.costPriceNumericUpDown.Size = new System.Drawing.Size(197, 22);
            this.costPriceNumericUpDown.TabIndex = 16;
            // 
            // lblSellingPrice
            // 
            this.lblSellingPrice.AutoSize = true;
            this.lblSellingPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSellingPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSellingPrice.Location = new System.Drawing.Point(313, 117);
            this.lblSellingPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSellingPrice.Name = "lblSellingPrice";
            this.lblSellingPrice.Size = new System.Drawing.Size(98, 40);
            this.lblSellingPrice.TabIndex = 3;
            this.lblSellingPrice.Text = "Selling Price:";
            this.lblSellingPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(313, 39);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 39);
            this.label6.TabIndex = 23;
            this.label6.Text = "Description:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemName.Location = new System.Drawing.Point(4, 39);
            this.lblItemName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(98, 39);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "Brand Name:";
            this.lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCostPrice
            // 
            this.lblCostPrice.AutoSize = true;
            this.lblCostPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCostPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCostPrice.Location = new System.Drawing.Point(313, 78);
            this.lblCostPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCostPrice.Name = "lblCostPrice";
            this.lblCostPrice.Size = new System.Drawing.Size(98, 39);
            this.lblCostPrice.TabIndex = 2;
            this.lblCostPrice.Text = "Cost Price:";
            this.lblCostPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(419, 5);
            this.categoryComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(195, 24);
            this.categoryComboBox.TabIndex = 22;
            // 
            // brandNameComboBox
            // 
            this.brandNameComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brandNameComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brandNameComboBox.FormattingEnabled = true;
            this.brandNameComboBox.Location = new System.Drawing.Point(110, 44);
            this.brandNameComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.brandNameComboBox.Name = "brandNameComboBox";
            this.brandNameComboBox.Size = new System.Drawing.Size(195, 24);
            this.brandNameComboBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(313, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 39);
            this.label5.TabIndex = 21;
            this.label5.Text = "Category:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(4, 78);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(98, 39);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Strength:";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dosageComboBox
            // 
            this.dosageComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dosageComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dosageComboBox.FormattingEnabled = true;
            this.dosageComboBox.Location = new System.Drawing.Point(110, 122);
            this.dosageComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.dosageComboBox.Name = "dosageComboBox";
            this.dosageComboBox.Size = new System.Drawing.Size(195, 24);
            this.dosageComboBox.TabIndex = 20;
            // 
            // stregnthComboBox
            // 
            this.stregnthComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stregnthComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stregnthComboBox.FormattingEnabled = true;
            this.stregnthComboBox.Location = new System.Drawing.Point(110, 83);
            this.stregnthComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.stregnthComboBox.Name = "stregnthComboBox";
            this.stregnthComboBox.Size = new System.Drawing.Size(195, 24);
            this.stregnthComboBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 117);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 40);
            this.label4.TabIndex = 19;
            this.label4.Text = "Dosage:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxStock
            // 
            this.groupBoxStock.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxStock.Controls.Add(this.tableLayoutPanel5);
            this.groupBoxStock.Controls.Add(this.discountCheckBox);
            this.groupBoxStock.Controls.Add(this.stockOutButton);
            this.groupBoxStock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxStock.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxStock.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBoxStock.Location = new System.Drawing.Point(3, 817);
            this.groupBoxStock.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxStock.Name = "groupBoxStock";
            this.groupBoxStock.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxStock.Size = new System.Drawing.Size(781, 65);
            this.groupBoxStock.TabIndex = 11;
            this.groupBoxStock.TabStop = false;
            this.groupBoxStock.Text = "Stock In";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 7;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.831824F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.62872F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel5.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.stockInButton, 6, 0);
            this.tableLayoutPanel5.Controls.Add(this.itemIdTextBox, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 5, 0);
            this.tableLayoutPanel5.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.quantityNumericUpDown, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblQuantity, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(4, 23);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(773, 37);
            this.tableLayoutPanel5.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 37);
            this.label7.TabIndex = 23;
            this.label7.Text = "Item ID:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stockInButton
            // 
            this.stockInButton.BackColor = System.Drawing.SystemColors.Control;
            this.stockInButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stockInButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stockInButton.Location = new System.Drawing.Point(664, 5);
            this.stockInButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stockInButton.Name = "stockInButton";
            this.stockInButton.Size = new System.Drawing.Size(105, 27);
            this.stockInButton.TabIndex = 4;
            this.stockInButton.Text = "Stock In";
            this.stockInButton.UseVisualStyleBackColor = false;
            this.stockInButton.Click += new System.EventHandler(this.btnStockIn_Click);
            // 
            // itemIdTextBox
            // 
            this.itemIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemIdTextBox.Enabled = false;
            this.itemIdTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemIdTextBox.Location = new System.Drawing.Point(114, 5);
            this.itemIdTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.itemIdTextBox.Name = "itemIdTextBox";
            this.itemIdTextBox.Size = new System.Drawing.Size(102, 22);
            this.itemIdTextBox.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.24324F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.75676F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.expirationDateCheckBox, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.expirationDateTimePicker, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(519, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(138, 31);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // expirationDateCheckBox
            // 
            this.expirationDateCheckBox.AutoSize = true;
            this.expirationDateCheckBox.Checked = true;
            this.expirationDateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.expirationDateCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expirationDateCheckBox.Location = new System.Drawing.Point(4, 5);
            this.expirationDateCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.expirationDateCheckBox.Name = "expirationDateCheckBox";
            this.expirationDateCheckBox.Size = new System.Drawing.Size(17, 21);
            this.expirationDateCheckBox.TabIndex = 24;
            this.expirationDateCheckBox.UseVisualStyleBackColor = true;
            this.expirationDateCheckBox.CheckedChanged += new System.EventHandler(this.expirationDateCheckBox_CheckedChanged);
            // 
            // expirationDateTimePicker
            // 
            this.expirationDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expirationDateTimePicker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.expirationDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.expirationDateTimePicker.Location = new System.Drawing.Point(28, 3);
            this.expirationDateTimePicker.Name = "expirationDateTimePicker";
            this.expirationDateTimePicker.Size = new System.Drawing.Size(107, 25);
            this.expirationDateTimePicker.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(444, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 37);
            this.label3.TabIndex = 16;
            this.label3.Text = "Exp Date:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // quantityNumericUpDown
            // 
            this.quantityNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quantityNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantityNumericUpDown.Location = new System.Drawing.Point(333, 3);
            this.quantityNumericUpDown.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.quantityNumericUpDown.Name = "quantityNumericUpDown";
            this.quantityNumericUpDown.Size = new System.Drawing.Size(104, 22);
            this.quantityNumericUpDown.TabIndex = 25;
            this.quantityNumericUpDown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.quantityNumericUpDown_KeyPress);
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.Location = new System.Drawing.Point(224, 0);
            this.lblQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(102, 37);
            this.lblQuantity.TabIndex = 1;
            this.lblQuantity.Text = "Quantity:";
            this.lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.groupBox2.Size = new System.Drawing.Size(787, 885);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stock Movements";
            // 
            // movementDataGridView
            // 
            this.movementDataGridView.AllowDrop = true;
            this.movementDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.movementDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.movementDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.movementDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.movementDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.movementDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.movementDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.movementDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movementDataGridView.Location = new System.Drawing.Point(3, 21);
            this.movementDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.movementDataGridView.MultiSelect = false;
            this.movementDataGridView.Name = "movementDataGridView";
            this.movementDataGridView.ReadOnly = true;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.movementDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.movementDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.movementDataGridView.Size = new System.Drawing.Size(781, 861);
            this.movementDataGridView.TabIndex = 12;
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
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sellingNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPriceNumericUpDown)).EndInit();
            this.groupBoxStock.ResumeLayout(false);
            this.groupBoxStock.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNumericUpDown)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.movementDataGridView)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button refreshPatientsButton;
        private System.Windows.Forms.Button searchPatientButton;
        private System.Windows.Forms.TextBox searchItemsTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox sortCategoryCombobox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker movementDateToDateTimePicker;
        private System.Windows.Forms.DateTimePicker movementDateFromDateTimePicker;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView movementDataGridView;
        private System.Windows.Forms.GroupBox groupBoxStock;
        private System.Windows.Forms.NumericUpDown quantityNumericUpDown;
        private System.Windows.Forms.CheckBox expirationDateCheckBox;
        private System.Windows.Forms.DateTimePicker expirationDateTimePicker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox discountCheckBox;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox itemIdTextBox;
        private System.Windows.Forms.Button stockInButton;
        private System.Windows.Forms.Button stockOutButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button writeOffButton;
        private System.Windows.Forms.ComboBox descriptionComboBox;
        private System.Windows.Forms.NumericUpDown sellingNumericUpDown;
        private System.Windows.Forms.Button updateItemButton;
        private System.Windows.Forms.Button addItemButton;
        private System.Windows.Forms.ComboBox genericNameComboBox;
        private System.Windows.Forms.NumericUpDown costPriceNumericUpDown;
        private System.Windows.Forms.Label lblSellingPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Label lblCostPrice;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.ComboBox brandNameComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox dosageComboBox;
        private System.Windows.Forms.ComboBox stregnthComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.DataGridView dgvItems;
    }
}
