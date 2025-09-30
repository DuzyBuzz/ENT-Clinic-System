namespace ENT_Clinic_System.Consultation
{
    partial class LabRequestForm
    {
        private System.ComponentModel.IContainer components = null;

        // Patient Info Controls
        private System.Windows.Forms.Label patientNameLabel;
        private System.Windows.Forms.TextBox patientNameTextBox;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.TextBox ageTextBox;
        private System.Windows.Forms.Label genderLabel;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.Button deselectAllButton;
        private System.Windows.Forms.Button printButton;

        // Right Panel - CRUD
        private System.Windows.Forms.DataGridView labTestsDGV;
        private System.Windows.Forms.Button prevPageButton;
        private System.Windows.Forms.Button nextPageButton;
        private System.Windows.Forms.Label pageInfoLabel;

        // Bottom Buttons
        private System.Windows.Forms.Button saveRequestButton;
        private System.Windows.Forms.Button addTestsButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabRequestForm));
            this.patientNameLabel = new System.Windows.Forms.Label();
            this.patientNameTextBox = new System.Windows.Forms.TextBox();
            this.ageLabel = new System.Windows.Forms.Label();
            this.ageTextBox = new System.Windows.Forms.TextBox();
            this.genderLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.deselectAllButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.labTestsDGV = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.test_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prevPageButton = new System.Windows.Forms.Button();
            this.nextPageButton = new System.Windows.Forms.Button();
            this.pageInfoLabel = new System.Windows.Forms.Label();
            this.saveRequestButton = new System.Windows.Forms.Button();
            this.addTestsButton = new System.Windows.Forms.Button();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.testNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.genderTextBox = new System.Windows.Forms.TextBox();
            this.groupBoxAvailable = new System.Windows.Forms.GroupBox();
            this.labTestsPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.labTestsDGV)).BeginInit();
            this.groupBoxAvailable.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // patientNameLabel
            // 
            this.patientNameLabel.AutoSize = true;
            this.patientNameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.patientNameLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patientNameLabel.Location = new System.Drawing.Point(3, 0);
            this.patientNameLabel.Name = "patientNameLabel";
            this.patientNameLabel.Size = new System.Drawing.Size(106, 23);
            this.patientNameLabel.TabIndex = 0;
            this.patientNameLabel.Text = "Patient Name:";
            // 
            // patientNameTextBox
            // 
            this.patientNameTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.patientNameTextBox.Location = new System.Drawing.Point(121, 3);
            this.patientNameTextBox.Name = "patientNameTextBox";
            this.patientNameTextBox.ReadOnly = true;
            this.patientNameTextBox.Size = new System.Drawing.Size(183, 20);
            this.patientNameTextBox.TabIndex = 1;
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ageLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageLabel.Location = new System.Drawing.Point(600, 0);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(40, 23);
            this.ageLabel.TabIndex = 2;
            this.ageLabel.Text = "Age:";
            // 
            // ageTextBox
            // 
            this.ageTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ageTextBox.Location = new System.Drawing.Point(658, 3);
            this.ageTextBox.Name = "ageTextBox";
            this.ageTextBox.ReadOnly = true;
            this.ageTextBox.Size = new System.Drawing.Size(50, 20);
            this.ageTextBox.TabIndex = 3;
            // 
            // genderLabel
            // 
            this.genderLabel.AutoSize = true;
            this.genderLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.genderLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genderLabel.Location = new System.Drawing.Point(758, 0);
            this.genderLabel.Name = "genderLabel";
            this.genderLabel.Size = new System.Drawing.Size(64, 23);
            this.genderLabel.TabIndex = 4;
            this.genderLabel.Text = "Gender:";
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLabel.Location = new System.Drawing.Point(971, 0);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(45, 23);
            this.dateLabel.TabIndex = 6;
            this.dateLabel.Text = "Date:";
            // 
            // datePicker
            // 
            this.datePicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.datePicker.Location = new System.Drawing.Point(1035, 3);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(200, 20);
            this.datePicker.TabIndex = 7;
            // 
            // selectAllButton
            // 
            this.selectAllButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.selectAllButton.Location = new System.Drawing.Point(329, 3);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(120, 31);
            this.selectAllButton.TabIndex = 9;
            this.selectAllButton.Text = "Select All";
            // 
            // deselectAllButton
            // 
            this.deselectAllButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.deselectAllButton.Location = new System.Drawing.Point(588, 3);
            this.deselectAllButton.Name = "deselectAllButton";
            this.deselectAllButton.Size = new System.Drawing.Size(105, 31);
            this.deselectAllButton.TabIndex = 10;
            this.deselectAllButton.Text = "Clear";
            // 
            // printButton
            // 
            this.printButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.printButton.Location = new System.Drawing.Point(462, 3);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(120, 31);
            this.printButton.TabIndex = 11;
            this.printButton.Text = "Refresh";
            // 
            // labTestsDGV
            // 
            this.labTestsDGV.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labTestsDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.labTestsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.labTestsDGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.labTestsDGV.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labTestsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.category,
            this.test_name});
            this.labTestsDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTestsDGV.Location = new System.Drawing.Point(0, 0);
            this.labTestsDGV.Name = "labTestsDGV";
            this.labTestsDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.labTestsDGV.Size = new System.Drawing.Size(648, 838);
            this.labTestsDGV.TabIndex = 12;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // category
            // 
            this.category.DataPropertyName = "category";
            this.category.HeaderText = "Category";
            this.category.Name = "category";
            // 
            // test_name
            // 
            this.test_name.DataPropertyName = "test_name";
            this.test_name.HeaderText = "Test Name";
            this.test_name.Name = "test_name";
            // 
            // prevPageButton
            // 
            this.prevPageButton.Location = new System.Drawing.Point(613, 705);
            this.prevPageButton.Name = "prevPageButton";
            this.prevPageButton.Size = new System.Drawing.Size(80, 23);
            this.prevPageButton.TabIndex = 13;
            this.prevPageButton.Text = "Prev";
            this.prevPageButton.Visible = false;
            // 
            // nextPageButton
            // 
            this.nextPageButton.Location = new System.Drawing.Point(613, 705);
            this.nextPageButton.Name = "nextPageButton";
            this.nextPageButton.Size = new System.Drawing.Size(80, 23);
            this.nextPageButton.TabIndex = 14;
            this.nextPageButton.Text = "Next";
            this.nextPageButton.Visible = false;
            // 
            // pageInfoLabel
            // 
            this.pageInfoLabel.AutoSize = true;
            this.pageInfoLabel.Location = new System.Drawing.Point(631, 710);
            this.pageInfoLabel.Name = "pageInfoLabel";
            this.pageInfoLabel.Size = new System.Drawing.Size(62, 13);
            this.pageInfoLabel.TabIndex = 15;
            this.pageInfoLabel.Text = "Page 1 of 1";
            this.pageInfoLabel.Visible = false;
            // 
            // saveRequestButton
            // 
            this.saveRequestButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.saveRequestButton.Location = new System.Drawing.Point(724, 3);
            this.saveRequestButton.Name = "saveRequestButton";
            this.saveRequestButton.Size = new System.Drawing.Size(150, 31);
            this.saveRequestButton.TabIndex = 16;
            this.saveRequestButton.Text = "Save Request";
            this.saveRequestButton.Click += new System.EventHandler(this.saveRequestButton_Click);
            // 
            // addTestsButton
            // 
            this.addTestsButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.addTestsButton.Location = new System.Drawing.Point(547, 29);
            this.addTestsButton.Name = "addTestsButton";
            this.addTestsButton.Size = new System.Drawing.Size(100, 26);
            this.addTestsButton.TabIndex = 17;
            this.addTestsButton.Text = "Add";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.categoryComboBox.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Other"});
            this.categoryComboBox.Location = new System.Drawing.Point(76, 3);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(179, 23);
            this.categoryComboBox.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(310, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 23);
            this.label1.TabIndex = 20;
            this.label1.Text = "Address:";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.addressTextBox.Location = new System.Drawing.Point(386, 3);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.ReadOnly = true;
            this.addressTextBox.Size = new System.Drawing.Size(200, 20);
            this.addressTextBox.TabIndex = 21;
            // 
            // testNameTextBox
            // 
            this.testNameTextBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.testNameTextBox.Location = new System.Drawing.Point(341, 3);
            this.testNameTextBox.Name = "testNameTextBox";
            this.testNameTextBox.Size = new System.Drawing.Size(306, 23);
            this.testNameTextBox.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 26);
            this.label2.TabIndex = 23;
            this.label2.Text = "Category:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Location = new System.Drawing.Point(266, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 26);
            this.label3.TabIndex = 24;
            this.label3.Text = "Test Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // genderTextBox
            // 
            this.genderTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.genderTextBox.Location = new System.Drawing.Point(835, 3);
            this.genderTextBox.Name = "genderTextBox";
            this.genderTextBox.ReadOnly = true;
            this.genderTextBox.Size = new System.Drawing.Size(50, 20);
            this.genderTextBox.TabIndex = 25;
            // 
            // groupBoxAvailable
            // 
            this.groupBoxAvailable.Controls.Add(this.labTestsPanel);
            this.groupBoxAvailable.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAvailable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxAvailable.Location = new System.Drawing.Point(3, 3);
            this.groupBoxAvailable.Name = "groupBoxAvailable";
            this.groupBoxAvailable.Size = new System.Drawing.Size(883, 920);
            this.groupBoxAvailable.TabIndex = 26;
            this.groupBoxAvailable.TabStop = false;
            this.groupBoxAvailable.Text = "Select Laboratory Request";
            this.groupBoxAvailable.Enter += new System.EventHandler(this.groupBoxAvailable_Enter);
            // 
            // labTestsPanel
            // 
            this.labTestsPanel.AutoScroll = true;
            this.labTestsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labTestsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labTestsPanel.Location = new System.Drawing.Point(3, 19);
            this.labTestsPanel.Name = "labTestsPanel";
            this.labTestsPanel.Size = new System.Drawing.Size(877, 861);
            this.labTestsPanel.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.30376F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.69625F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.Controls.Add(this.printButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.deselectAllButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.saveRequestButton, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.selectAllButton, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 880);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(877, 37);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(892, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(656, 920);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Laboratory Requests";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labTestsDGV);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(650, 840);
            this.panel1.TabIndex = 10;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.38461F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.61539F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.46154F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.84615F));
            this.tableLayoutPanel2.Controls.Add(this.addTestsButton, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.categoryComboBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.testNameTextBox, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 859);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(650, 58);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.01769F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96.98231F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1557, 961);
            this.tableLayoutPanel3.TabIndex = 28;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 10;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.672469F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.31464F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.964539F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.9265F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.803998F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.511928F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.029014F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.833011F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.190845F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.39781F));
            this.tableLayoutPanel4.Controls.Add(this.patientNameLabel, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.patientNameTextBox, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.datePicker, 9, 0);
            this.tableLayoutPanel4.Controls.Add(this.dateLabel, 8, 0);
            this.tableLayoutPanel4.Controls.Add(this.genderTextBox, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.addressTextBox, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.genderLabel, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.ageTextBox, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.ageLabel, 4, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1551, 23);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.31786F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.68214F));
            this.tableLayoutPanel5.Controls.Add(this.groupBoxAvailable, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 32);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1551, 926);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // LabRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1557, 961);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.prevPageButton);
            this.Controls.Add(this.nextPageButton);
            this.Controls.Add(this.pageInfoLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LabRequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Laborator Request";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LabRequestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.labTestsDGV)).EndInit();
            this.groupBoxAvailable.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.TextBox testNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox genderTextBox;
        private System.Windows.Forms.GroupBox groupBoxAvailable;
        private System.Windows.Forms.Panel labTestsPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn category;
        private System.Windows.Forms.DataGridViewTextBoxColumn test_name;
    }
}
