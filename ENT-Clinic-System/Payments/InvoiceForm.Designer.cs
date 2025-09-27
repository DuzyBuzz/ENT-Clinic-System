namespace ENT_Clinic_System.Inventory
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
            this.availableItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.selectedItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBoxAvailable = new System.Windows.Forms.GroupBox();
            this.groupBoxSelected = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.noteComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.discountPercentComboBox = new System.Windows.Forms.ComboBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.changeTextBox = new System.Windows.Forms.TextBox();
            this.subTotalTextBox = new System.Windows.Forms.TextBox();
            this.lblChange = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.itemsAmountRecievedTextBox = new System.Windows.Forms.TextBox();
            this.discountTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.lblAmountReceived = new System.Windows.Forms.Label();
            this.totalAmountTextBox = new System.Windows.Forms.TextBox();
            this.lblNetTotal = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.prescriptionDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.patientsDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.availableItemsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedItemsDataGridView)).BeginInit();
            this.groupBoxAvailable.SuspendLayout();
            this.groupBoxSelected.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prescriptionDataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGridView)).BeginInit();
            this.SuspendLayout();
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
            this.availableItemsDataGridView.Size = new System.Drawing.Size(694, 416);
            this.availableItemsDataGridView.TabIndex = 0;
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
            this.selectedItemsDataGridView.Size = new System.Drawing.Size(529, 403);
            this.selectedItemsDataGridView.TabIndex = 0;
            // 
            // groupBoxAvailable
            // 
            this.groupBoxAvailable.Controls.Add(this.availableItemsDataGridView);
            this.groupBoxAvailable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxAvailable.Location = new System.Drawing.Point(12, 12);
            this.groupBoxAvailable.Name = "groupBoxAvailable";
            this.groupBoxAvailable.Size = new System.Drawing.Size(700, 438);
            this.groupBoxAvailable.TabIndex = 0;
            this.groupBoxAvailable.TabStop = false;
            this.groupBoxAvailable.Text = "Available Items (Inventory)";
            // 
            // groupBoxSelected
            // 
            this.groupBoxSelected.Controls.Add(this.selectedItemsDataGridView);
            this.groupBoxSelected.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxSelected.Location = new System.Drawing.Point(733, 22);
            this.groupBoxSelected.Name = "groupBoxSelected";
            this.groupBoxSelected.Size = new System.Drawing.Size(535, 425);
            this.groupBoxSelected.TabIndex = 1;
            this.groupBoxSelected.TabStop = false;
            this.groupBoxSelected.Text = "Selected Items (Invoice)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.noteComboBox);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.discountPercentComboBox);
            this.groupBox3.Controls.Add(this.lblSubtotal);
            this.groupBox3.Controls.Add(this.changeTextBox);
            this.groupBox3.Controls.Add(this.subTotalTextBox);
            this.groupBox3.Controls.Add(this.lblChange);
            this.groupBox3.Controls.Add(this.lblDiscount);
            this.groupBox3.Controls.Add(this.itemsAmountRecievedTextBox);
            this.groupBox3.Controls.Add(this.discountTextBox);
            this.groupBox3.Controls.Add(this.saveButton);
            this.groupBox3.Controls.Add(this.lblAmountReceived);
            this.groupBox3.Controls.Add(this.totalAmountTextBox);
            this.groupBox3.Controls.Add(this.lblNetTotal);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(736, 456);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(529, 231);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Payment";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "Note:";
            // 
            // noteComboBox
            // 
            this.noteComboBox.FormattingEnabled = true;
            this.noteComboBox.Location = new System.Drawing.Point(319, 129);
            this.noteComboBox.Name = "noteComboBox";
            this.noteComboBox.Size = new System.Drawing.Size(189, 23);
            this.noteComboBox.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Discount:";
            // 
            // discountPercentComboBox
            // 
            this.discountPercentComboBox.FormattingEnabled = true;
            this.discountPercentComboBox.Location = new System.Drawing.Point(119, 61);
            this.discountPercentComboBox.Name = "discountPercentComboBox";
            this.discountPercentComboBox.Size = new System.Drawing.Size(58, 23);
            this.discountPercentComboBox.TabIndex = 13;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(6, 34);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(57, 15);
            this.lblSubtotal.TabIndex = 1;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // changeTextBox
            // 
            this.changeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.changeTextBox.Location = new System.Drawing.Point(374, 61);
            this.changeTextBox.Name = "changeTextBox";
            this.changeTextBox.ReadOnly = true;
            this.changeTextBox.Size = new System.Drawing.Size(120, 23);
            this.changeTextBox.TabIndex = 12;
            // 
            // subTotalTextBox
            // 
            this.subTotalTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subTotalTextBox.Location = new System.Drawing.Point(119, 31);
            this.subTotalTextBox.Name = "subTotalTextBox";
            this.subTotalTextBox.ReadOnly = true;
            this.subTotalTextBox.Size = new System.Drawing.Size(120, 23);
            this.subTotalTextBox.TabIndex = 2;
            // 
            // lblChange
            // 
            this.lblChange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChange.AutoSize = true;
            this.lblChange.Location = new System.Drawing.Point(259, 69);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new System.Drawing.Size(77, 15);
            this.lblChange.TabIndex = 11;
            this.lblChange.Text = "Change Due:";
            // 
            // lblDiscount
            // 
            this.lblDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(6, 99);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(107, 15);
            this.lblDiscount.TabIndex = 3;
            this.lblDiscount.Text = "Discount Amount:";
            // 
            // itemsAmountRecievedTextBox
            // 
            this.itemsAmountRecievedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsAmountRecievedTextBox.Location = new System.Drawing.Point(374, 26);
            this.itemsAmountRecievedTextBox.Name = "itemsAmountRecievedTextBox";
            this.itemsAmountRecievedTextBox.Size = new System.Drawing.Size(120, 23);
            this.itemsAmountRecievedTextBox.TabIndex = 10;
            // 
            // discountTextBox
            // 
            this.discountTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.discountTextBox.Location = new System.Drawing.Point(119, 96);
            this.discountTextBox.Name = "discountTextBox";
            this.discountTextBox.Size = new System.Drawing.Size(120, 23);
            this.discountTextBox.TabIndex = 4;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.BackColor = System.Drawing.Color.SeaGreen;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(427, 177);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(81, 32);
            this.saveButton.TabIndex = 13;
            this.saveButton.Text = "Submit";
            this.saveButton.UseVisualStyleBackColor = false;
            // 
            // lblAmountReceived
            // 
            this.lblAmountReceived.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAmountReceived.AutoSize = true;
            this.lblAmountReceived.Location = new System.Drawing.Point(259, 34);
            this.lblAmountReceived.Name = "lblAmountReceived";
            this.lblAmountReceived.Size = new System.Drawing.Size(110, 15);
            this.lblAmountReceived.TabIndex = 9;
            this.lblAmountReceived.Text = "Amount Received:";
            // 
            // totalAmountTextBox
            // 
            this.totalAmountTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalAmountTextBox.Location = new System.Drawing.Point(119, 129);
            this.totalAmountTextBox.Name = "totalAmountTextBox";
            this.totalAmountTextBox.ReadOnly = true;
            this.totalAmountTextBox.Size = new System.Drawing.Size(120, 23);
            this.totalAmountTextBox.TabIndex = 8;
            // 
            // lblNetTotal
            // 
            this.lblNetTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNetTotal.AutoSize = true;
            this.lblNetTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNetTotal.Location = new System.Drawing.Point(6, 137);
            this.lblNetTotal.Name = "lblNetTotal";
            this.lblNetTotal.Size = new System.Drawing.Size(74, 15);
            this.lblNetTotal.TabIndex = 7;
            this.lblNetTotal.Text = "Grand Total:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.prescriptionDataGridView);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(314, 456);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 234);
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
            this.prescriptionDataGridView.Size = new System.Drawing.Size(392, 212);
            this.prescriptionDataGridView.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.patientsDataGridView);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(12, 456);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 234);
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
            this.patientsDataGridView.Size = new System.Drawing.Size(278, 212);
            this.patientsDataGridView.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "%";
            // 
            // InvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1287, 710);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxAvailable);
            this.Controls.Add(this.groupBoxSelected);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "InvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Items Invoice Manager";
            ((System.ComponentModel.ISupportInitialize)(this.availableItemsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedItemsDataGridView)).EndInit();
            this.groupBoxAvailable.ResumeLayout(false);
            this.groupBoxSelected.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.prescriptionDataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.patientsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView availableItemsDataGridView;
        private System.Windows.Forms.DataGridView selectedItemsDataGridView;
        private System.Windows.Forms.GroupBox groupBoxAvailable;
        private System.Windows.Forms.GroupBox groupBoxSelected;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblNetTotal;
        private System.Windows.Forms.TextBox subTotalTextBox;
        private System.Windows.Forms.TextBox discountTextBox;
        private System.Windows.Forms.TextBox totalAmountTextBox;
        private System.Windows.Forms.Label lblAmountReceived;
        private System.Windows.Forms.TextBox itemsAmountRecievedTextBox;
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
    }
}
