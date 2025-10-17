namespace ENT_Clinic_System.Payments
{
    partial class InvoiceForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceForm));
            this.selectedItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBoxSelected = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.noteComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.itemsAmountRecievedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.subTotalTextBox = new System.Windows.Forms.TextBox();
            this.changeTextBox = new System.Windows.Forms.TextBox();
            this.lblNetTotal = new System.Windows.Forms.Label();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblAmountReceived = new System.Windows.Forms.Label();
            this.totalAmountTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.discountPercentComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.discountTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.prescriptionDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.patientsDataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBoxAvailable = new System.Windows.Forms.GroupBox();
            this.availableItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.refreshPatientsButton = new System.Windows.Forms.Button();
            this.searchItemtButton = new System.Windows.Forms.Button();
            this.searchItemsTextBox = new System.Windows.Forms.TextBox();
            this.categoryCombobox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.selectedItemsDataGridView)).BeginInit();
            this.groupBoxSelected.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemsAmountRecievedNumericUpDown)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prescriptionDataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxAvailable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.availableItemsDataGridView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectedItemsDataGridView
            // 
            this.selectedItemsDataGridView.AllowUserToAddRows = false;
            this.selectedItemsDataGridView.AllowUserToDeleteRows = false;
            this.selectedItemsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.selectedItemsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.selectedItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectedItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedItemsDataGridView.Location = new System.Drawing.Point(3, 19);
            this.selectedItemsDataGridView.MultiSelect = false;
            this.selectedItemsDataGridView.Name = "selectedItemsDataGridView";
            this.selectedItemsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.selectedItemsDataGridView.Size = new System.Drawing.Size(623, 769);
            this.selectedItemsDataGridView.TabIndex = 0;
            // 
            // groupBoxSelected
            // 
            this.groupBoxSelected.Controls.Add(this.selectedItemsDataGridView);
            this.groupBoxSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSelected.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxSelected.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSelected.Name = "groupBoxSelected";
            this.groupBoxSelected.Size = new System.Drawing.Size(629, 791);
            this.groupBoxSelected.TabIndex = 1;
            this.groupBoxSelected.TabStop = false;
            this.groupBoxSelected.Text = "Selected Items (Invoice)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(3, 800);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(629, 152);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Payment";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.saveButton, 3, 3);
            this.tableLayoutPanel4.Controls.Add(this.noteComboBox, 3, 2);
            this.tableLayoutPanel4.Controls.Add(this.label5, 2, 2);
            this.tableLayoutPanel4.Controls.Add(this.itemsAmountRecievedNumericUpDown, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblSubtotal, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.subTotalTextBox, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.changeTextBox, 3, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblNetTotal, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.lblChange, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblAmountReceived, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.totalAmountTextBox, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblDiscount, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.discountTextBox, 1, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(623, 130);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.SeaGreen;
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(468, 99);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(152, 28);
            this.saveButton.TabIndex = 13;
            this.saveButton.Text = "Submit";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click_1);
            // 
            // noteComboBox
            // 
            this.noteComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noteComboBox.FormattingEnabled = true;
            this.noteComboBox.Location = new System.Drawing.Point(468, 67);
            this.noteComboBox.Name = "noteComboBox";
            this.noteComboBox.Size = new System.Drawing.Size(152, 23);
            this.noteComboBox.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(313, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 32);
            this.label5.TabIndex = 16;
            this.label5.Text = "Note:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // itemsAmountRecievedNumericUpDown
            // 
            this.itemsAmountRecievedNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemsAmountRecievedNumericUpDown.Location = new System.Drawing.Point(468, 3);
            this.itemsAmountRecievedNumericUpDown.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.itemsAmountRecievedNumericUpDown.Name = "itemsAmountRecievedNumericUpDown";
            this.itemsAmountRecievedNumericUpDown.Size = new System.Drawing.Size(152, 23);
            this.itemsAmountRecievedNumericUpDown.TabIndex = 19;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubtotal.Location = new System.Drawing.Point(3, 0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(149, 32);
            this.lblSubtotal.TabIndex = 1;
            this.lblSubtotal.Text = "Subtotal:";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // subTotalTextBox
            // 
            this.subTotalTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subTotalTextBox.Location = new System.Drawing.Point(158, 3);
            this.subTotalTextBox.Name = "subTotalTextBox";
            this.subTotalTextBox.ReadOnly = true;
            this.subTotalTextBox.Size = new System.Drawing.Size(149, 23);
            this.subTotalTextBox.TabIndex = 2;
            // 
            // changeTextBox
            // 
            this.changeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeTextBox.Location = new System.Drawing.Point(468, 35);
            this.changeTextBox.Name = "changeTextBox";
            this.changeTextBox.ReadOnly = true;
            this.changeTextBox.Size = new System.Drawing.Size(152, 23);
            this.changeTextBox.TabIndex = 12;
            // 
            // lblNetTotal
            // 
            this.lblNetTotal.AutoSize = true;
            this.lblNetTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNetTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNetTotal.Location = new System.Drawing.Point(3, 96);
            this.lblNetTotal.Name = "lblNetTotal";
            this.lblNetTotal.Size = new System.Drawing.Size(149, 34);
            this.lblNetTotal.TabIndex = 7;
            this.lblNetTotal.Text = "Grand Total:";
            this.lblNetTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChange
            // 
            this.lblChange.AutoSize = true;
            this.lblChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChange.Location = new System.Drawing.Point(313, 32);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(149, 32);
            this.lblChange.TabIndex = 11;
            this.lblChange.Text = "Change Due:";
            this.lblChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAmountReceived
            // 
            this.lblAmountReceived.AutoSize = true;
            this.lblAmountReceived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAmountReceived.Location = new System.Drawing.Point(313, 0);
            this.lblAmountReceived.Name = "lblAmountReceived";
            this.lblAmountReceived.Size = new System.Drawing.Size(149, 32);
            this.lblAmountReceived.TabIndex = 9;
            this.lblAmountReceived.Text = "Amount Received:";
            this.lblAmountReceived.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalAmountTextBox
            // 
            this.totalAmountTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalAmountTextBox.Location = new System.Drawing.Point(158, 99);
            this.totalAmountTextBox.Name = "totalAmountTextBox";
            this.totalAmountTextBox.ReadOnly = true;
            this.totalAmountTextBox.Size = new System.Drawing.Size(149, 23);
            this.totalAmountTextBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 32);
            this.label2.TabIndex = 14;
            this.label2.Text = "Discount:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.38931F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.61069F));
            this.tableLayoutPanel5.Controls.Add(this.discountPercentComboBox, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(158, 35);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(149, 26);
            this.tableLayoutPanel5.TabIndex = 15;
            // 
            // discountPercentComboBox
            // 
            this.discountPercentComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discountPercentComboBox.FormattingEnabled = true;
            this.discountPercentComboBox.Location = new System.Drawing.Point(3, 3);
            this.discountPercentComboBox.Name = "discountPercentComboBox";
            this.discountPercentComboBox.Size = new System.Drawing.Size(112, 23);
            this.discountPercentComboBox.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(121, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 26);
            this.label1.TabIndex = 18;
            this.label1.Text = "%";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiscount.Location = new System.Drawing.Point(3, 64);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(149, 32);
            this.lblDiscount.TabIndex = 3;
            this.lblDiscount.Text = "Discount Amount:";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // discountTextBox
            // 
            this.discountTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discountTextBox.Location = new System.Drawing.Point(158, 67);
            this.discountTextBox.Name = "discountTextBox";
            this.discountTextBox.Size = new System.Drawing.Size(149, 23);
            this.discountTextBox.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.prescriptionDataGridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(303, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(598, 283);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prescriptions";
            // 
            // prescriptionDataGridView
            // 
            this.prescriptionDataGridView.AllowUserToAddRows = false;
            this.prescriptionDataGridView.AllowUserToDeleteRows = false;
            this.prescriptionDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.prescriptionDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.prescriptionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.prescriptionDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prescriptionDataGridView.Location = new System.Drawing.Point(3, 19);
            this.prescriptionDataGridView.MultiSelect = false;
            this.prescriptionDataGridView.Name = "prescriptionDataGridView";
            this.prescriptionDataGridView.ReadOnly = true;
            this.prescriptionDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.prescriptionDataGridView.Size = new System.Drawing.Size(592, 261);
            this.prescriptionDataGridView.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.patientsDataGridView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 283);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Patients";
            // 
            // patientsDataGridView
            // 
            this.patientsDataGridView.AllowUserToAddRows = false;
            this.patientsDataGridView.AllowUserToDeleteRows = false;
            this.patientsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.patientsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.patientsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patientsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientsDataGridView.Location = new System.Drawing.Point(3, 19);
            this.patientsDataGridView.MultiSelect = false;
            this.patientsDataGridView.Name = "patientsDataGridView";
            this.patientsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.patientsDataGridView.Size = new System.Drawing.Size(288, 261);
            this.patientsDataGridView.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.96917F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.20052F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.79948F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(910, 955);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBoxAvailable);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(904, 654);
            this.panel1.TabIndex = 0;
            // 
            // groupBoxAvailable
            // 
            this.groupBoxAvailable.Controls.Add(this.availableItemsDataGridView);
            this.groupBoxAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAvailable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxAvailable.Location = new System.Drawing.Point(0, 42);
            this.groupBoxAvailable.Name = "groupBoxAvailable";
            this.groupBoxAvailable.Size = new System.Drawing.Size(904, 612);
            this.groupBoxAvailable.TabIndex = 5;
            this.groupBoxAvailable.TabStop = false;
            this.groupBoxAvailable.Text = "Available Items (Inventory)";
            // 
            // availableItemsDataGridView
            // 
            this.availableItemsDataGridView.AllowUserToAddRows = false;
            this.availableItemsDataGridView.AllowUserToDeleteRows = false;
            this.availableItemsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.availableItemsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.availableItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.availableItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.availableItemsDataGridView.Location = new System.Drawing.Point(3, 19);
            this.availableItemsDataGridView.MultiSelect = false;
            this.availableItemsDataGridView.Name = "availableItemsDataGridView";
            this.availableItemsDataGridView.ReadOnly = true;
            this.availableItemsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.availableItemsDataGridView.Size = new System.Drawing.Size(898, 590);
            this.availableItemsDataGridView.TabIndex = 2;
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
            this.tableLayoutPanel2.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.refreshPatientsButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchItemtButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchItemsTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.categoryCombobox, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(904, 42);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(571, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 42);
            this.label3.TabIndex = 5;
            this.label3.Text = "Category:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // refreshPatientsButton
            // 
            this.refreshPatientsButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.refreshPatientsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.refreshPatientsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshPatientsButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshPatientsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.refreshPatientsButton.Location = new System.Drawing.Point(517, 5);
            this.refreshPatientsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.refreshPatientsButton.Name = "refreshPatientsButton";
            this.refreshPatientsButton.Size = new System.Drawing.Size(46, 32);
            this.refreshPatientsButton.TabIndex = 3;
            this.refreshPatientsButton.Text = "⟳";
            this.refreshPatientsButton.UseVisualStyleBackColor = false;
            this.refreshPatientsButton.Click += new System.EventHandler(this.refreshPatientsButton_Click);
            // 
            // searchItemtButton
            // 
            this.searchItemtButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.searchItemtButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchItemtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchItemtButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchItemtButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchItemtButton.Location = new System.Drawing.Point(466, 5);
            this.searchItemtButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchItemtButton.Name = "searchItemtButton";
            this.searchItemtButton.Size = new System.Drawing.Size(43, 32);
            this.searchItemtButton.TabIndex = 0;
            this.searchItemtButton.Text = "🔎";
            this.searchItemtButton.UseVisualStyleBackColor = false;
            this.searchItemtButton.Click += new System.EventHandler(this.searchItemtButton_Click);
            // 
            // searchItemsTextBox
            // 
            this.searchItemsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchItemsTextBox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchItemsTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchItemsTextBox.Location = new System.Drawing.Point(156, 11);
            this.searchItemsTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchItemsTextBox.Name = "searchItemsTextBox";
            this.searchItemsTextBox.Size = new System.Drawing.Size(302, 26);
            this.searchItemsTextBox.TabIndex = 1;
            // 
            // categoryCombobox
            // 
            this.categoryCombobox.BackColor = System.Drawing.SystemColors.Control;
            this.categoryCombobox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.categoryCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.categoryCombobox.FormattingEnabled = true;
            this.categoryCombobox.Location = new System.Drawing.Point(664, 11);
            this.categoryCombobox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.categoryCombobox.Name = "categoryCombobox";
            this.categoryCombobox.Size = new System.Drawing.Size(236, 23);
            this.categoryCombobox.TabIndex = 4;
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
            this.label8.Size = new System.Drawing.Size(144, 42);
            this.label8.TabIndex = 2;
            this.label8.Text = "Search Item:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.28982F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.71018F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 663);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(904, 289);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.83109F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.16891F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1557, 961);
            this.tableLayoutPanel6.TabIndex = 16;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.groupBoxSelected, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(919, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.4555F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.5445F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(635, 955);
            this.tableLayoutPanel7.TabIndex = 16;
            // 
            // InvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1557, 961);
            this.Controls.Add(this.tableLayoutPanel6);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Items Dispensing & Payment";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.InvoiceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.selectedItemsDataGridView)).EndInit();
            this.groupBoxSelected.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.itemsAmountRecievedNumericUpDown)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.prescriptionDataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBoxAvailable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.availableItemsDataGridView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView selectedItemsDataGridView;
        private System.Windows.Forms.GroupBox groupBoxSelected;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblNetTotal;
        private System.Windows.Forms.TextBox subTotalTextBox;
        private System.Windows.Forms.TextBox discountTextBox;
        private System.Windows.Forms.TextBox totalAmountTextBox;
        private System.Windows.Forms.Label lblAmountReceived;
        private System.Windows.Forms.Label lblChange;
        private System.Windows.Forms.TextBox changeTextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView prescriptionDataGridView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView patientsDataGridView;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox noteComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox discountPercentComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown itemsAmountRecievedNumericUpDown;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBoxAvailable;
        private System.Windows.Forms.DataGridView availableItemsDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button refreshPatientsButton;
        private System.Windows.Forms.Button searchItemtButton;
        private System.Windows.Forms.TextBox searchItemsTextBox;
        private System.Windows.Forms.ComboBox categoryCombobox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    }
}
