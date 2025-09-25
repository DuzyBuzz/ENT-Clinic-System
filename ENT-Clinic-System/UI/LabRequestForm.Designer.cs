namespace ENT_Clinic_System.UI
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
        private System.Windows.Forms.ComboBox genderComboBox;
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
            this.patientNameLabel = new System.Windows.Forms.Label();
            this.patientNameTextBox = new System.Windows.Forms.TextBox();
            this.ageLabel = new System.Windows.Forms.Label();
            this.ageTextBox = new System.Windows.Forms.TextBox();
            this.genderLabel = new System.Windows.Forms.Label();
            this.genderComboBox = new System.Windows.Forms.ComboBox();
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
            this.testNameComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.labTestsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // patientNameLabel
            // 
            this.patientNameLabel.AutoSize = true;
            this.patientNameLabel.Location = new System.Drawing.Point(10, 10);
            this.patientNameLabel.Name = "patientNameLabel";
            this.patientNameLabel.Size = new System.Drawing.Size(74, 13);
            this.patientNameLabel.TabIndex = 0;
            this.patientNameLabel.Text = "Patient Name:";
            // 
            // patientNameTextBox
            // 
            this.patientNameTextBox.Location = new System.Drawing.Point(120, 5);
            this.patientNameTextBox.Name = "patientNameTextBox";
            this.patientNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.patientNameTextBox.TabIndex = 1;
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Location = new System.Drawing.Point(10, 40);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(29, 13);
            this.ageLabel.TabIndex = 2;
            this.ageLabel.Text = "Age:";
            // 
            // ageTextBox
            // 
            this.ageTextBox.Location = new System.Drawing.Point(120, 35);
            this.ageTextBox.Name = "ageTextBox";
            this.ageTextBox.Size = new System.Drawing.Size(50, 20);
            this.ageTextBox.TabIndex = 3;
            // 
            // genderLabel
            // 
            this.genderLabel.AutoSize = true;
            this.genderLabel.Location = new System.Drawing.Point(200, 40);
            this.genderLabel.Name = "genderLabel";
            this.genderLabel.Size = new System.Drawing.Size(45, 13);
            this.genderLabel.TabIndex = 4;
            this.genderLabel.Text = "Gender:";
            // 
            // genderComboBox
            // 
            this.genderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genderComboBox.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Other"});
            this.genderComboBox.Location = new System.Drawing.Point(270, 35);
            this.genderComboBox.Name = "genderComboBox";
            this.genderComboBox.Size = new System.Drawing.Size(100, 21);
            this.genderComboBox.TabIndex = 5;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(10, 70);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(33, 13);
            this.dateLabel.TabIndex = 6;
            this.dateLabel.Text = "Date:";
            // 
            // datePicker
            // 
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker.Location = new System.Drawing.Point(120, 65);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(200, 20);
            this.datePicker.TabIndex = 7;
            // 
            // labTestsPanel
            // 
            this.labTestsPanel.AutoScroll = true;
            this.labTestsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labTestsPanel.Location = new System.Drawing.Point(10, 100);
            this.labTestsPanel.Name = "labTestsPanel";
            this.labTestsPanel.Size = new System.Drawing.Size(840, 500);
            this.labTestsPanel.TabIndex = 8;
            // 
            // selectAllButton
            // 
            this.selectAllButton.Location = new System.Drawing.Point(10, 665);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(120, 23);
            this.selectAllButton.TabIndex = 9;
            this.selectAllButton.Text = "Select All";
            // 
            // deselectAllButton
            // 
            this.deselectAllButton.Location = new System.Drawing.Point(136, 665);
            this.deselectAllButton.Name = "deselectAllButton";
            this.deselectAllButton.Size = new System.Drawing.Size(120, 23);
            this.deselectAllButton.TabIndex = 10;
            this.deselectAllButton.Text = "Deselect All";
            // 
            // printButton
            // 
            this.printButton.Location = new System.Drawing.Point(418, 665);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(120, 23);
            this.printButton.TabIndex = 11;
            this.printButton.Text = "Print";
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // labTestsDGV
            // 
            this.labTestsDGV.AllowUserToAddRows = false;
            this.labTestsDGV.Location = new System.Drawing.Point(860, 100);
            this.labTestsDGV.Name = "labTestsDGV";
            this.labTestsDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.labTestsDGV.Size = new System.Drawing.Size(320, 500);
            this.labTestsDGV.TabIndex = 12;
            // 
            // prevPageButton
            // 
            this.prevPageButton.Location = new System.Drawing.Point(860, 610);
            this.prevPageButton.Name = "prevPageButton";
            this.prevPageButton.Size = new System.Drawing.Size(80, 23);
            this.prevPageButton.TabIndex = 13;
            this.prevPageButton.Text = "Prev";
            // 
            // nextPageButton
            // 
            this.nextPageButton.Location = new System.Drawing.Point(1100, 610);
            this.nextPageButton.Name = "nextPageButton";
            this.nextPageButton.Size = new System.Drawing.Size(80, 23);
            this.nextPageButton.TabIndex = 14;
            this.nextPageButton.Text = "Next";
            // 
            // pageInfoLabel
            // 
            this.pageInfoLabel.AutoSize = true;
            this.pageInfoLabel.Location = new System.Drawing.Point(995, 615);
            this.pageInfoLabel.Name = "pageInfoLabel";
            this.pageInfoLabel.Size = new System.Drawing.Size(62, 13);
            this.pageInfoLabel.TabIndex = 15;
            this.pageInfoLabel.Text = "Page 1 of 1";
            // 
            // saveRequestButton
            // 
            this.saveRequestButton.Location = new System.Drawing.Point(262, 665);
            this.saveRequestButton.Name = "saveRequestButton";
            this.saveRequestButton.Size = new System.Drawing.Size(150, 23);
            this.saveRequestButton.TabIndex = 16;
            this.saveRequestButton.Text = "Save Request";
            // 
            // addTestsButton
            // 
            this.addTestsButton.Location = new System.Drawing.Point(396, 610);
            this.addTestsButton.Name = "addTestsButton";
            this.addTestsButton.Size = new System.Drawing.Size(100, 23);
            this.addTestsButton.TabIndex = 17;
            this.addTestsButton.Text = "Add";
            this.addTestsButton.Click += new System.EventHandler(this.addTestsButton_Click);
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Other"});
            this.categoryComboBox.Location = new System.Drawing.Point(13, 612);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(177, 21);
            this.categoryComboBox.TabIndex = 18;
            // 
            // testNameComboBox
            // 
            this.testNameComboBox.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Other"});
            this.testNameComboBox.Location = new System.Drawing.Point(193, 612);
            this.testNameComboBox.Name = "testNameComboBox";
            this.testNameComboBox.Size = new System.Drawing.Size(177, 21);
            this.testNameComboBox.TabIndex = 19;
            // 
            // LabRequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 700);
            this.Controls.Add(this.testNameComboBox);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.patientNameLabel);
            this.Controls.Add(this.patientNameTextBox);
            this.Controls.Add(this.ageLabel);
            this.Controls.Add(this.ageTextBox);
            this.Controls.Add(this.genderLabel);
            this.Controls.Add(this.genderComboBox);
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
            this.Name = "LabRequestForm";
            this.Text = "ENT Clinic Lab Request";
            ((System.ComponentModel.ISupportInitialize)(this.labTestsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.ComboBox testNameComboBox;
    }
}
