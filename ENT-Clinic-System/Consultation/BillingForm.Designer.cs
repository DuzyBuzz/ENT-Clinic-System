namespace ENT_Clinic_System.Consultation
{
    partial class BillingForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelFee;
        private System.Windows.Forms.Label labelDiscount;
        private System.Windows.Forms.ComboBox discountComboBox;
        private System.Windows.Forms.CheckBox fullDiscountCheckBox;
        private System.Windows.Forms.Label labelFinal;
        private System.Windows.Forms.Label finalAmountLabel;
        private System.Windows.Forms.Label labelNote;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label discountAmountLabel;

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
            this.labelFee = new System.Windows.Forms.Label();
            this.labelDiscount = new System.Windows.Forms.Label();
            this.discountComboBox = new System.Windows.Forms.ComboBox();
            this.fullDiscountCheckBox = new System.Windows.Forms.CheckBox();
            this.labelFinal = new System.Windows.Forms.Label();
            this.finalAmountLabel = new System.Windows.Forms.Label();
            this.labelNote = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.noteComboBox = new System.Windows.Forms.ComboBox();
            this.feeComboBox = new System.Windows.Forms.ComboBox();
            this.discountAmountLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.discountNameComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.procedureComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelFee
            // 
            this.labelFee.AutoSize = true;
            this.labelFee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelFee.Location = new System.Drawing.Point(48, 61);
            this.labelFee.Name = "labelFee";
            this.labelFee.Size = new System.Drawing.Size(88, 19);
            this.labelFee.TabIndex = 1;
            this.labelFee.Text = "Doctor\'s Fee:";
            // 
            // labelDiscount
            // 
            this.labelDiscount.AutoSize = true;
            this.labelDiscount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelDiscount.Location = new System.Drawing.Point(48, 115);
            this.labelDiscount.Name = "labelDiscount";
            this.labelDiscount.Size = new System.Drawing.Size(66, 19);
            this.labelDiscount.TabIndex = 3;
            this.labelDiscount.Text = "Discount:";
            // 
            // discountComboBox
            // 
            this.discountComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.discountComboBox.FormattingEnabled = true;
            this.discountComboBox.Location = new System.Drawing.Point(160, 112);
            this.discountComboBox.Name = "discountComboBox";
            this.discountComboBox.Size = new System.Drawing.Size(103, 25);
            this.discountComboBox.TabIndex = 4;
            // 
            // fullDiscountCheckBox
            // 
            this.fullDiscountCheckBox.AutoSize = true;
            this.fullDiscountCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.fullDiscountCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fullDiscountCheckBox.Location = new System.Drawing.Point(160, 143);
            this.fullDiscountCheckBox.Name = "fullDiscountCheckBox";
            this.fullDiscountCheckBox.Size = new System.Drawing.Size(109, 19);
            this.fullDiscountCheckBox.TabIndex = 5;
            this.fullDiscountCheckBox.Text = "100% Discount";
            this.fullDiscountCheckBox.UseVisualStyleBackColor = true;
            // 
            // labelFinal
            // 
            this.labelFinal.AutoSize = true;
            this.labelFinal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelFinal.Location = new System.Drawing.Point(48, 246);
            this.labelFinal.Name = "labelFinal";
            this.labelFinal.Size = new System.Drawing.Size(103, 19);
            this.labelFinal.TabIndex = 6;
            this.labelFinal.Text = "Total Amount:";
            // 
            // finalAmountLabel
            // 
            this.finalAmountLabel.AutoSize = true;
            this.finalAmountLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.finalAmountLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.finalAmountLabel.Location = new System.Drawing.Point(151, 246);
            this.finalAmountLabel.Name = "finalAmountLabel";
            this.finalAmountLabel.Size = new System.Drawing.Size(46, 19);
            this.finalAmountLabel.TabIndex = 7;
            this.finalAmountLabel.Text = "₱0.00";
            // 
            // labelNote
            // 
            this.labelNote.AutoSize = true;
            this.labelNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelNote.Location = new System.Drawing.Point(48, 295);
            this.labelNote.Name = "labelNote";
            this.labelNote.Size = new System.Drawing.Size(42, 19);
            this.labelNote.TabIndex = 8;
            this.labelNote.Text = "Note:";
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.SystemColors.Control;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.saveButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveButton.Location = new System.Drawing.Point(160, 338);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(86, 30);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Gray;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(288, 338);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(86, 30);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // noteComboBox
            // 
            this.noteComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.noteComboBox.FormattingEnabled = true;
            this.noteComboBox.Location = new System.Drawing.Point(160, 292);
            this.noteComboBox.Name = "noteComboBox";
            this.noteComboBox.Size = new System.Drawing.Size(214, 25);
            this.noteComboBox.TabIndex = 12;
            // 
            // feeComboBox
            // 
            this.feeComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.feeComboBox.FormattingEnabled = true;
            this.feeComboBox.Location = new System.Drawing.Point(160, 55);
            this.feeComboBox.Name = "feeComboBox";
            this.feeComboBox.Size = new System.Drawing.Size(214, 25);
            this.feeComboBox.TabIndex = 13;
            // 
            // discountAmountLabel
            // 
            this.discountAmountLabel.AutoSize = true;
            this.discountAmountLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.discountAmountLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.discountAmountLabel.Location = new System.Drawing.Point(269, 117);
            this.discountAmountLabel.Name = "discountAmountLabel";
            this.discountAmountLabel.Size = new System.Drawing.Size(35, 15);
            this.discountAmountLabel.TabIndex = 15;
            this.discountAmountLabel.Text = "₱0.00";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.titleLabel.Location = new System.Drawing.Point(10, 13);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(119, 21);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Patient Billing";
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.SystemColors.Control;
            this.headerPanel.Controls.Add(this.titleLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(437, 43);
            this.headerPanel.TabIndex = 0;
            // 
            // discountNameComboBox
            // 
            this.discountNameComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.discountNameComboBox.FormattingEnabled = true;
            this.discountNameComboBox.Location = new System.Drawing.Point(160, 173);
            this.discountNameComboBox.Name = "discountNameComboBox";
            this.discountNameComboBox.Size = new System.Drawing.Size(214, 25);
            this.discountNameComboBox.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(48, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.TabIndex = 16;
            this.label1.Text = "Discount Name:";
            // 
            // procedureComboBox
            // 
            this.procedureComboBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.procedureComboBox.FormattingEnabled = true;
            this.procedureComboBox.Location = new System.Drawing.Point(160, 208);
            this.procedureComboBox.Name = "procedureComboBox";
            this.procedureComboBox.Size = new System.Drawing.Size(214, 25);
            this.procedureComboBox.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(48, 211);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 18;
            this.label2.Text = "Procedure:";
            // 
            // BillingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(437, 395);
            this.ControlBox = false;
            this.Controls.Add(this.procedureComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.discountNameComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.discountAmountLabel);
            this.Controls.Add(this.feeComboBox);
            this.Controls.Add(this.noteComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.labelNote);
            this.Controls.Add(this.finalAmountLabel);
            this.Controls.Add(this.labelFinal);
            this.Controls.Add(this.fullDiscountCheckBox);
            this.Controls.Add(this.discountComboBox);
            this.Controls.Add(this.labelDiscount);
            this.Controls.Add(this.labelFee);
            this.Controls.Add(this.headerPanel);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BillingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.BillingForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox noteComboBox;
        private System.Windows.Forms.ComboBox feeComboBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.ComboBox discountNameComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox procedureComboBox;
        private System.Windows.Forms.Label label2;
    }
}