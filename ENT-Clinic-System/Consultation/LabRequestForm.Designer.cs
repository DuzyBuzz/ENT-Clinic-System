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

        // Lab Tests Panel
        private System.Windows.Forms.Panel labTestsPanel;
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
            this.labTestsPanel = new System.Windows.Forms.Panel();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.deselectAllButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.labTestsDGV = new System.Windows.Forms.DataGridView();
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
            this.category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.test_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.labTestsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // patientNameLabel
            // 
            this.patientNameLabel.AutoSize = true;
            this.patientNameLabel.Location = new System.Drawing.Point(10, 17);
            this.patientNameLabel.Name = "patientNameLabel";
            this.patientNameLabel.Size = new System.Drawing.Size(74, 13);
            this.patientNameLabel.TabIndex = 0;
            this.patientNameLabel.Text = "Patient Name:";
            // 
            // patientNameTextBox
            // 
            this.patientNameTextBox.Location = new System.Drawing.Point(120, 12);
            this.patientNameTextBox.Name = "patientNameTextBox";
            this.patientNameTextBox.ReadOnly = true;
            this.patientNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.patientNameTextBox.TabIndex = 1;
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Location = new System.Drawing.Point(598, 15);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(29, 13);
            this.ageLabel.TabIndex = 2;
            this.ageLabel.Text = "Age:";
            // 
            // ageTextBox
            // 
            this.ageTextBox.Location = new System.Drawing.Point(633, 12);
            this.ageTextBox.Name = "ageTextBox";
            this.ageTextBox.ReadOnly = true;
            this.ageTextBox.Size = new System.Drawing.Size(50, 20);
            this.ageTextBox.TabIndex = 3;
            // 
            // genderLabel
            // 
            this.genderLabel.AutoSize = true;
            this.genderLabel.Location = new System.Drawing.Point(703, 17);
            this.genderLabel.Name = "genderLabel";
            this.genderLabel.Size = new System.Drawing.Size(45, 13);
            this.genderLabel.TabIndex = 4;
            this.genderLabel.Text = "Gender:";
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(826, 17);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(33, 13);
            this.dateLabel.TabIndex = 6;
            this.dateLabel.Text = "Date:";
            // 
            // datePicker
            // 
            this.datePicker.Location = new System.Drawing.Point(865, 12);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(200, 20);
            this.datePicker.TabIndex = 7;
            // 
            // labTestsPanel
            // 
            this.labTestsPanel.AutoScroll = true;
            this.labTestsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labTestsPanel.Location = new System.Drawing.Point(10, 62);
            this.labTestsPanel.Name = "labTestsPanel";
            this.labTestsPanel.Size = new System.Drawing.Size(840, 538);
            this.labTestsPanel.TabIndex = 8;
            // 
            // selectAllButton
            // 
            this.selectAllButton.Location = new System.Drawing.Point(10, 619);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(120, 23);
            this.selectAllButton.TabIndex = 9;
            this.selectAllButton.Text = "Select All";
            // 
            // deselectAllButton
            // 
            this.deselectAllButton.Location = new System.Drawing.Point(180, 619);
            this.deselectAllButton.Name = "deselectAllButton";
            this.deselectAllButton.Size = new System.Drawing.Size(120, 23);
            this.deselectAllButton.TabIndex = 10;
            this.deselectAllButton.Text = "Deselect All";
            // 
            // printButton
            // 
            this.printButton.Location = new System.Drawing.Point(507, 619);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(120, 23);
            this.printButton.TabIndex = 11;
            this.printButton.Text = "Print";
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
            this.category,
            this.test_name});
            this.labTestsDGV.Location = new System.Drawing.Point(860, 62);
            this.labTestsDGV.Name = "labTestsDGV";
            this.labTestsDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.labTestsDGV.Size = new System.Drawing.Size(320, 538);
            this.labTestsDGV.TabIndex = 12;
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
            this.saveRequestButton.Location = new System.Drawing.Point(341, 619);
            this.saveRequestButton.Name = "saveRequestButton";
            this.saveRequestButton.Size = new System.Drawing.Size(150, 23);
            this.saveRequestButton.TabIndex = 16;
            this.saveRequestButton.Text = "Save Request";
            // 
            // addTestsButton
            // 
            this.addTestsButton.Location = new System.Drawing.Point(1080, 673);
            this.addTestsButton.Name = "addTestsButton";
            this.addTestsButton.Size = new System.Drawing.Size(100, 23);
            this.addTestsButton.TabIndex = 17;
            this.addTestsButton.Text = "Add";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Other"});
            this.categoryComboBox.Location = new System.Drawing.Point(930, 616);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(250, 21);
            this.categoryComboBox.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(338, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Address:";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(392, 12);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.ReadOnly = true;
            this.addressTextBox.Size = new System.Drawing.Size(200, 20);
            this.addressTextBox.TabIndex = 21;
            // 
            // testNameTextBox
            // 
            this.testNameTextBox.Location = new System.Drawing.Point(930, 647);
            this.testNameTextBox.Name = "testNameTextBox";
            this.testNameTextBox.Size = new System.Drawing.Size(250, 20);
            this.testNameTextBox.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(857, 619);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Category:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(857, 650);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Test Name:";
            // 
            // genderTextBox
            // 
            this.genderTextBox.Location = new System.Drawing.Point(754, 12);
            this.genderTextBox.Name = "genderTextBox";
            this.genderTextBox.ReadOnly = true;
            this.genderTextBox.Size = new System.Drawing.Size(50, 20);
            this.genderTextBox.TabIndex = 25;
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
            // LabRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 700);
            this.Controls.Add(this.genderTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.testNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.patientNameLabel);
            this.Controls.Add(this.patientNameTextBox);
            this.Controls.Add(this.ageLabel);
            this.Controls.Add(this.ageTextBox);
            this.Controls.Add(this.genderLabel);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.labTestsPanel);
            this.Controls.Add(this.selectAllButton);
            this.Controls.Add(this.deselectAllButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.labTestsDGV);
            this.Controls.Add(this.prevPageButton);
            this.Controls.Add(this.nextPageButton);
            this.Controls.Add(this.pageInfoLabel);
            this.Controls.Add(this.saveRequestButton);
            this.Controls.Add(this.addTestsButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LabRequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Laborator Request";
            this.Load += new System.EventHandler(this.LabRequestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.labTestsDGV)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn category;
        private System.Windows.Forms.DataGridViewTextBoxColumn test_name;
    }
}
