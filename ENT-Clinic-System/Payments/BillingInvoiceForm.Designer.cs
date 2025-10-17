using System.Windows.Forms;

namespace ENT_Clinic_System.Payments
{
    partial class BillingInvoiceForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillingInvoiceForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxRecords = new System.Windows.Forms.GroupBox();
            this.billingDataGridView = new System.Windows.Forms.DataGridView();
            this.billing_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patient_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discount_percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.discount_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payment_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount_paid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updated_at = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.consultation_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.billingtDateToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.billingDateFromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.refreshPatientsButton = new System.Windows.Forms.Button();
            this.searchPatientButton = new System.Windows.Forms.Button();
            this.searchPatientTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.paymentHistoryDataGridView = new System.Windows.Forms.DataGridView();
            this.panelRight = new System.Windows.Forms.Panel();
            this.groupBoxPayment = new System.Windows.Forms.GroupBox();
            this.balanceTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.amountRecievedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new System.Windows.Forms.Button();
            this.changeTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxSummary = new System.Windows.Forms.GroupBox();
            this.paymentStatusLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.totalBillTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.discountAmountTextBox = new System.Windows.Forms.TextBox();
            this.discountPercentLabel = new System.Windows.Forms.Label();
            this.doctorsFeeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelRemainingBalance = new System.Windows.Forms.Label();
            this.remainingBalanceTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxRecords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.billingDataGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paymentHistoryDataGridView)).BeginInit();
            this.panelRight.SuspendLayout();
            this.groupBoxPayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountRecievedNumericUpDown)).BeginInit();
            this.groupBoxSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxRecords);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel4);
            this.splitContainer1.Size = new System.Drawing.Size(1557, 961);
            this.splitContainer1.SplitterDistance = 1140;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBoxRecords
            // 
            this.groupBoxRecords.Controls.Add(this.billingDataGridView);
            this.groupBoxRecords.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRecords.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxRecords.Location = new System.Drawing.Point(0, 0);
            this.groupBoxRecords.Name = "groupBoxRecords";
            this.groupBoxRecords.Size = new System.Drawing.Size(1140, 961);
            this.groupBoxRecords.TabIndex = 0;
            this.groupBoxRecords.TabStop = false;
            this.groupBoxRecords.Text = "Billing Records";
            // 
            // billingDataGridView
            // 
            this.billingDataGridView.AllowUserToAddRows = false;
            this.billingDataGridView.AllowUserToDeleteRows = false;
            this.billingDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.billingDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.billingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.billingDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.billing_id,
            this.patient_name,
            this.fee,
            this.discount_percent,
            this.discount_amount,
            this.total_amount,
            this.note,
            this.payment_status,
            this.amount_paid,
            this.balance,
            this.updated_at,
            this.consultation_id});
            this.billingDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.billingDataGridView.Location = new System.Drawing.Point(3, 72);
            this.billingDataGridView.MultiSelect = false;
            this.billingDataGridView.Name = "billingDataGridView";
            this.billingDataGridView.ReadOnly = true;
            this.billingDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.billingDataGridView.Size = new System.Drawing.Size(1134, 886);
            this.billingDataGridView.TabIndex = 7;
            // 
            // billing_id
            // 
            this.billing_id.DataPropertyName = "billing_id";
            this.billing_id.HeaderText = "Billing ID";
            this.billing_id.Name = "billing_id";
            this.billing_id.ReadOnly = true;
            // 
            // patient_name
            // 
            this.patient_name.DataPropertyName = "patient_name";
            this.patient_name.HeaderText = "Patient Name";
            this.patient_name.Name = "patient_name";
            this.patient_name.ReadOnly = true;
            // 
            // fee
            // 
            this.fee.DataPropertyName = "fee";
            this.fee.HeaderText = "Doctors Fee";
            this.fee.Name = "fee";
            this.fee.ReadOnly = true;
            // 
            // discount_percent
            // 
            this.discount_percent.DataPropertyName = "discount_percent";
            this.discount_percent.HeaderText = "Discount (%)";
            this.discount_percent.Name = "discount_percent";
            this.discount_percent.ReadOnly = true;
            // 
            // discount_amount
            // 
            this.discount_amount.DataPropertyName = "discount_amount";
            this.discount_amount.HeaderText = "Discount Amount";
            this.discount_amount.Name = "discount_amount";
            this.discount_amount.ReadOnly = true;
            // 
            // total_amount
            // 
            this.total_amount.DataPropertyName = "total_amount";
            this.total_amount.HeaderText = "Total Amount";
            this.total_amount.Name = "total_amount";
            this.total_amount.ReadOnly = true;
            // 
            // note
            // 
            this.note.DataPropertyName = "note";
            this.note.HeaderText = "Note";
            this.note.Name = "note";
            this.note.ReadOnly = true;
            // 
            // payment_status
            // 
            this.payment_status.DataPropertyName = "payment_status";
            this.payment_status.HeaderText = "Payment Status";
            this.payment_status.Name = "payment_status";
            this.payment_status.ReadOnly = true;
            // 
            // amount_paid
            // 
            this.amount_paid.DataPropertyName = "amount_paid";
            this.amount_paid.HeaderText = "Amount Paid";
            this.amount_paid.Name = "amount_paid";
            this.amount_paid.ReadOnly = true;
            // 
            // balance
            // 
            this.balance.DataPropertyName = "balance";
            this.balance.HeaderText = "Balance";
            this.balance.Name = "balance";
            this.balance.ReadOnly = true;
            // 
            // updated_at
            // 
            this.updated_at.DataPropertyName = "updated_at";
            this.updated_at.HeaderText = "Payment Date";
            this.updated_at.Name = "updated_at";
            this.updated_at.ReadOnly = true;
            // 
            // consultation_id
            // 
            this.consultation_id.DataPropertyName = "consultation_id";
            this.consultation_id.HeaderText = "Consultation ID";
            this.consultation_id.Name = "consultation_id";
            this.consultation_id.ReadOnly = true;
            this.consultation_id.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1134, 53);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.53521F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.22066F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.7277F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.28169F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.label11, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.billingtDateToDateTimePicker, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.billingDateFromDateTimePicker, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(573, 8);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(555, 37);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(302, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 37);
            this.label11.TabIndex = 14;
            this.label11.Text = "Date To:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 37);
            this.label2.TabIndex = 13;
            this.label2.Text = "Date From:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // billingtDateToDateTimePicker
            // 
            this.billingtDateToDateTimePicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.billingtDateToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.billingtDateToDateTimePicker.Location = new System.Drawing.Point(395, 10);
            this.billingtDateToDateTimePicker.Margin = new System.Windows.Forms.Padding(10);
            this.billingtDateToDateTimePicker.Name = "billingtDateToDateTimePicker";
            this.billingtDateToDateTimePicker.Size = new System.Drawing.Size(110, 23);
            this.billingtDateToDateTimePicker.TabIndex = 6;
            this.billingtDateToDateTimePicker.ValueChanged += new System.EventHandler(this.billingtDateToDateTimePicker_ValueChanged);
            // 
            // billingDateFromDateTimePicker
            // 
            this.billingDateFromDateTimePicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.billingDateFromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.billingDateFromDateTimePicker.Location = new System.Drawing.Point(135, 10);
            this.billingDateFromDateTimePicker.Margin = new System.Windows.Forms.Padding(10);
            this.billingDateFromDateTimePicker.Name = "billingDateFromDateTimePicker";
            this.billingDateFromDateTimePicker.Size = new System.Drawing.Size(110, 23);
            this.billingDateFromDateTimePicker.TabIndex = 8;
            this.billingDateFromDateTimePicker.ValueChanged += new System.EventHandler(this.billingDateFromDateTimePicker_ValueChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.38461F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.34978F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.250305F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.494505F));
            this.tableLayoutPanel2.Controls.Add(this.refreshPatientsButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchPatientButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchPatientTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 8);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(555, 37);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // refreshPatientsButton
            // 
            this.refreshPatientsButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.refreshPatientsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.refreshPatientsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshPatientsButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshPatientsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.refreshPatientsButton.Location = new System.Drawing.Point(504, 5);
            this.refreshPatientsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.refreshPatientsButton.Name = "refreshPatientsButton";
            this.refreshPatientsButton.Size = new System.Drawing.Size(47, 27);
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
            this.searchPatientButton.Location = new System.Drawing.Point(454, 5);
            this.searchPatientButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchPatientButton.Name = "searchPatientButton";
            this.searchPatientButton.Size = new System.Drawing.Size(42, 27);
            this.searchPatientButton.TabIndex = 0;
            this.searchPatientButton.Text = "🔎";
            this.searchPatientButton.UseVisualStyleBackColor = false;
            this.searchPatientButton.Click += new System.EventHandler(this.searchPatientButton_Click);
            // 
            // searchPatientTextBox
            // 
            this.searchPatientTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchPatientTextBox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchPatientTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchPatientTextBox.Location = new System.Drawing.Point(152, 6);
            this.searchPatientTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchPatientTextBox.Name = "searchPatientTextBox";
            this.searchPatientTextBox.Size = new System.Drawing.Size(294, 26);
            this.searchPatientTextBox.TabIndex = 1;
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
            this.label8.Size = new System.Drawing.Size(140, 37);
            this.label8.TabIndex = 2;
            this.label8.Text = "Search Patient:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panelRight, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.91988F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.08012F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(414, 961);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.paymentHistoryDataGridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 541);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Payment History";
            // 
            // paymentHistoryDataGridView
            // 
            this.paymentHistoryDataGridView.AllowUserToAddRows = false;
            this.paymentHistoryDataGridView.AllowUserToDeleteRows = false;
            this.paymentHistoryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.paymentHistoryDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.paymentHistoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.paymentHistoryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paymentHistoryDataGridView.Location = new System.Drawing.Point(3, 19);
            this.paymentHistoryDataGridView.MultiSelect = false;
            this.paymentHistoryDataGridView.Name = "paymentHistoryDataGridView";
            this.paymentHistoryDataGridView.ReadOnly = true;
            this.paymentHistoryDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.paymentHistoryDataGridView.Size = new System.Drawing.Size(402, 519);
            this.paymentHistoryDataGridView.TabIndex = 8;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.groupBoxPayment);
            this.panelRight.Controls.Add(this.groupBoxSummary);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(3, 550);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(408, 408);
            this.panelRight.TabIndex = 0;
            // 
            // groupBoxPayment
            // 
            this.groupBoxPayment.Controls.Add(this.balanceTextBox);
            this.groupBoxPayment.Controls.Add(this.label10);
            this.groupBoxPayment.Controls.Add(this.amountRecievedNumericUpDown);
            this.groupBoxPayment.Controls.Add(this.saveButton);
            this.groupBoxPayment.Controls.Add(this.changeTextBox);
            this.groupBoxPayment.Controls.Add(this.label7);
            this.groupBoxPayment.Controls.Add(this.label6);
            this.groupBoxPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPayment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxPayment.Location = new System.Drawing.Point(0, 225);
            this.groupBoxPayment.Name = "groupBoxPayment";
            this.groupBoxPayment.Size = new System.Drawing.Size(408, 183);
            this.groupBoxPayment.TabIndex = 1;
            this.groupBoxPayment.TabStop = false;
            this.groupBoxPayment.Text = "Payment Entry";
            // 
            // balanceTextBox
            // 
            this.balanceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.balanceTextBox.Location = new System.Drawing.Point(224, 95);
            this.balanceTextBox.Name = "balanceTextBox";
            this.balanceTextBox.ReadOnly = true;
            this.balanceTextBox.Size = new System.Drawing.Size(150, 23);
            this.balanceTextBox.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 15);
            this.label10.TabIndex = 30;
            this.label10.Text = "Balance:";
            // 
            // amountRecievedNumericUpDown
            // 
            this.amountRecievedNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.amountRecievedNumericUpDown.Location = new System.Drawing.Point(225, 31);
            this.amountRecievedNumericUpDown.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.amountRecievedNumericUpDown.Name = "amountRecievedNumericUpDown";
            this.amountRecievedNumericUpDown.Size = new System.Drawing.Size(150, 23);
            this.amountRecievedNumericUpDown.TabIndex = 29;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveButton.BackColor = System.Drawing.SystemColors.Control;
            this.saveButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveButton.Location = new System.Drawing.Point(262, 144);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(112, 30);
            this.saveButton.TabIndex = 28;
            this.saveButton.Text = "Submit Payment";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // changeTextBox
            // 
            this.changeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.changeTextBox.Location = new System.Drawing.Point(225, 65);
            this.changeTextBox.Name = "changeTextBox";
            this.changeTextBox.ReadOnly = true;
            this.changeTextBox.Size = new System.Drawing.Size(150, 23);
            this.changeTextBox.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "Change:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 15);
            this.label6.TabIndex = 24;
            this.label6.Text = "Amount Received:";
            // 
            // groupBoxSummary
            // 
            this.groupBoxSummary.Controls.Add(this.paymentStatusLabel);
            this.groupBoxSummary.Controls.Add(this.label9);
            this.groupBoxSummary.Controls.Add(this.noteTextBox);
            this.groupBoxSummary.Controls.Add(this.label4);
            this.groupBoxSummary.Controls.Add(this.totalBillTextBox);
            this.groupBoxSummary.Controls.Add(this.label3);
            this.groupBoxSummary.Controls.Add(this.discountAmountTextBox);
            this.groupBoxSummary.Controls.Add(this.discountPercentLabel);
            this.groupBoxSummary.Controls.Add(this.doctorsFeeTextBox);
            this.groupBoxSummary.Controls.Add(this.label1);
            this.groupBoxSummary.Controls.Add(this.labelRemainingBalance);
            this.groupBoxSummary.Controls.Add(this.remainingBalanceTextBox);
            this.groupBoxSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSummary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxSummary.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSummary.Name = "groupBoxSummary";
            this.groupBoxSummary.Size = new System.Drawing.Size(408, 225);
            this.groupBoxSummary.TabIndex = 0;
            this.groupBoxSummary.TabStop = false;
            this.groupBoxSummary.Text = "Billing Summary";
            // 
            // paymentStatusLabel
            // 
            this.paymentStatusLabel.AutoSize = true;
            this.paymentStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.paymentStatusLabel.Location = new System.Drawing.Point(222, 157);
            this.paymentStatusLabel.Name = "paymentStatusLabel";
            this.paymentStatusLabel.Size = new System.Drawing.Size(29, 15);
            this.paymentStatusLabel.TabIndex = 10;
            this.paymentStatusLabel.Text = "N/A";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 15);
            this.label9.TabIndex = 9;
            this.label9.Text = "Payment Status:";
            // 
            // noteTextBox
            // 
            this.noteTextBox.Location = new System.Drawing.Point(224, 123);
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.ReadOnly = true;
            this.noteTextBox.Size = new System.Drawing.Size(150, 23);
            this.noteTextBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Note:";
            // 
            // totalBillTextBox
            // 
            this.totalBillTextBox.Location = new System.Drawing.Point(224, 93);
            this.totalBillTextBox.Name = "totalBillTextBox";
            this.totalBillTextBox.ReadOnly = true;
            this.totalBillTextBox.Size = new System.Drawing.Size(150, 23);
            this.totalBillTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Total Bill:";
            // 
            // discountAmountTextBox
            // 
            this.discountAmountTextBox.Location = new System.Drawing.Point(224, 62);
            this.discountAmountTextBox.Name = "discountAmountTextBox";
            this.discountAmountTextBox.ReadOnly = true;
            this.discountAmountTextBox.Size = new System.Drawing.Size(150, 23);
            this.discountAmountTextBox.TabIndex = 4;
            // 
            // discountPercentLabel
            // 
            this.discountPercentLabel.AutoSize = true;
            this.discountPercentLabel.Location = new System.Drawing.Point(17, 63);
            this.discountPercentLabel.Name = "discountPercentLabel";
            this.discountPercentLabel.Size = new System.Drawing.Size(87, 15);
            this.discountPercentLabel.TabIndex = 3;
            this.discountPercentLabel.Text = "Discount (0%):";
            // 
            // doctorsFeeTextBox
            // 
            this.doctorsFeeTextBox.Location = new System.Drawing.Point(225, 33);
            this.doctorsFeeTextBox.Name = "doctorsFeeTextBox";
            this.doctorsFeeTextBox.ReadOnly = true;
            this.doctorsFeeTextBox.Size = new System.Drawing.Size(150, 23);
            this.doctorsFeeTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Doctor\'s Fee:";
            // 
            // labelRemainingBalance
            // 
            this.labelRemainingBalance.AutoSize = true;
            this.labelRemainingBalance.Location = new System.Drawing.Point(17, 192);
            this.labelRemainingBalance.Name = "labelRemainingBalance";
            this.labelRemainingBalance.Size = new System.Drawing.Size(115, 15);
            this.labelRemainingBalance.TabIndex = 11;
            this.labelRemainingBalance.Text = "Remaining Balance:";
            this.labelRemainingBalance.Visible = false;
            this.labelRemainingBalance.Click += new System.EventHandler(this.labelRemainingBalance_Click);
            // 
            // remainingBalanceTextBox
            // 
            this.remainingBalanceTextBox.Location = new System.Drawing.Point(224, 186);
            this.remainingBalanceTextBox.Name = "remainingBalanceTextBox";
            this.remainingBalanceTextBox.ReadOnly = true;
            this.remainingBalanceTextBox.Size = new System.Drawing.Size(150, 23);
            this.remainingBalanceTextBox.TabIndex = 12;
            this.remainingBalanceTextBox.Visible = false;
            // 
            // BillingInvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1557, 961);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BillingInvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Billing Invoice";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BillingInvoiceForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxRecords.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.billingDataGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.paymentHistoryDataGridView)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.groupBoxPayment.ResumeLayout(false);
            this.groupBoxPayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountRecievedNumericUpDown)).EndInit();
            this.groupBoxSummary.ResumeLayout(false);
            this.groupBoxSummary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxRecords;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.GroupBox groupBoxSummary;
        private System.Windows.Forms.Label paymentStatusLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox totalBillTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox discountAmountTextBox;
        private System.Windows.Forms.Label discountPercentLabel;
        private System.Windows.Forms.TextBox doctorsFeeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxPayment;
        private System.Windows.Forms.TextBox balanceTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown amountRecievedNumericUpDown;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox changeTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button refreshPatientsButton;
        private System.Windows.Forms.Button searchPatientButton;
        private System.Windows.Forms.TextBox searchPatientTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView billingDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker billingtDateToDateTimePicker;
        private System.Windows.Forms.DateTimePicker billingDateFromDateTimePicker;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView paymentHistoryDataGridView;
        private Label labelRemainingBalance;
        private TextBox remainingBalanceTextBox;
        private DataGridViewTextBoxColumn billing_id;
        private DataGridViewTextBoxColumn patient_name;
        private DataGridViewTextBoxColumn fee;
        private DataGridViewTextBoxColumn discount_percent;
        private DataGridViewTextBoxColumn discount_amount;
        private DataGridViewTextBoxColumn total_amount;
        private DataGridViewTextBoxColumn note;
        private DataGridViewTextBoxColumn payment_status;
        private DataGridViewTextBoxColumn amount_paid;
        private DataGridViewTextBoxColumn balance;
        private DataGridViewTextBoxColumn updated_at;
        private DataGridViewTextBoxColumn consultation_id;
        private GroupBox groupBox1;
    }
}
