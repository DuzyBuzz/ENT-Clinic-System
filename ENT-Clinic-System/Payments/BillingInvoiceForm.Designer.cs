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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillingInvoiceForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxRecords = new System.Windows.Forms.GroupBox();
            this.billingDataGridView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.billingtDateToDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.billingDateFromDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.refreshPatientsButton = new System.Windows.Forms.Button();
            this.searchPatientTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.paymentHistoryDataGridView = new System.Windows.Forms.DataGridView();
            this.panelRight = new System.Windows.Forms.Panel();
            this.groupBoxPayment = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.balanceTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.amountRecievedNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.changeTextBox = new System.Windows.Forms.TextBox();
            this.groupBoxSummary = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.labelRemainingBalance = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.paymentStatusLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.remainingBalanceTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.doctorsFeeTextBox = new System.Windows.Forms.TextBox();
            this.discountPercentLabel = new System.Windows.Forms.Label();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.discountAmountTextBox = new System.Windows.Forms.TextBox();
            this.totalBillTextBox = new System.Windows.Forms.TextBox();
            this.searchItemtButton = new System.Windows.Forms.Button();
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
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
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
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountRecievedNumericUpDown)).BeginInit();
            this.groupBoxSummary.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(1625, 610);
            this.splitContainer1.SplitterDistance = 1284;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // groupBoxRecords
            // 
            this.groupBoxRecords.Controls.Add(this.billingDataGridView);
            this.groupBoxRecords.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxRecords.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxRecords.Location = new System.Drawing.Point(0, 0);
            this.groupBoxRecords.Name = "groupBoxRecords";
            this.groupBoxRecords.Size = new System.Drawing.Size(1284, 610);
            this.groupBoxRecords.TabIndex = 0;
            this.groupBoxRecords.TabStop = false;
            this.groupBoxRecords.Text = "Billing Records";
            // 
            // billingDataGridView
            // 
            this.billingDataGridView.AllowUserToAddRows = false;
            this.billingDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.billingDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
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
            this.billingDataGridView.Location = new System.Drawing.Point(3, 78);
            this.billingDataGridView.MultiSelect = false;
            this.billingDataGridView.Name = "billingDataGridView";
            this.billingDataGridView.ReadOnly = true;
            this.billingDataGridView.Size = new System.Drawing.Size(1278, 529);
            this.billingDataGridView.TabIndex = 7;
            this.billingDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.billingDataGridView_CellContentClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.63736F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.36264F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1278, 59);
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
            this.tableLayoutPanel3.Location = new System.Drawing.Point(806, 8);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(466, 43);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(254, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 43);
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
            this.label2.Size = new System.Drawing.Size(97, 43);
            this.label2.TabIndex = 13;
            this.label2.Text = "Date From:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // billingtDateToDateTimePicker
            // 
            this.billingtDateToDateTimePicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.billingtDateToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.billingtDateToDateTimePicker.Location = new System.Drawing.Point(333, 10);
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
            this.billingDateFromDateTimePicker.Location = new System.Drawing.Point(115, 10);
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
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.39643F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.79698F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.8546F));
            this.tableLayoutPanel2.Controls.Add(this.searchItemtButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.refreshPatientsButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchPatientTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 8);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(788, 43);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // refreshPatientsButton
            // 
            this.refreshPatientsButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.refreshPatientsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.refreshPatientsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshPatientsButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshPatientsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.refreshPatientsButton.Location = new System.Drawing.Point(682, 5);
            this.refreshPatientsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.refreshPatientsButton.Name = "refreshPatientsButton";
            this.refreshPatientsButton.Size = new System.Drawing.Size(102, 33);
            this.refreshPatientsButton.TabIndex = 3;
            this.refreshPatientsButton.Text = "Refresh";
            this.refreshPatientsButton.UseVisualStyleBackColor = false;
            this.refreshPatientsButton.Click += new System.EventHandler(this.refreshPatientsButton_Click);
            // 
            // searchPatientTextBox
            // 
            this.searchPatientTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchPatientTextBox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchPatientTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchPatientTextBox.Location = new System.Drawing.Point(124, 12);
            this.searchPatientTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchPatientTextBox.Name = "searchPatientTextBox";
            this.searchPatientTextBox.Size = new System.Drawing.Size(458, 26);
            this.searchPatientTextBox.TabIndex = 1;
            this.searchPatientTextBox.TextChanged += new System.EventHandler(this.searchPatientTextBox_TextChanged);
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
            this.label8.Size = new System.Drawing.Size(112, 43);
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
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.16393F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.83607F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(338, 610);
            this.tableLayoutPanel4.TabIndex = 2;
            this.tableLayoutPanel4.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel4_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.paymentHistoryDataGridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 178);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Payment History";
            // 
            // paymentHistoryDataGridView
            // 
            this.paymentHistoryDataGridView.AllowUserToAddRows = false;
            this.paymentHistoryDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.paymentHistoryDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.paymentHistoryDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.paymentHistoryDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.paymentHistoryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.paymentHistoryDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paymentHistoryDataGridView.Location = new System.Drawing.Point(3, 19);
            this.paymentHistoryDataGridView.MultiSelect = false;
            this.paymentHistoryDataGridView.Name = "paymentHistoryDataGridView";
            this.paymentHistoryDataGridView.ReadOnly = true;
            this.paymentHistoryDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.paymentHistoryDataGridView.Size = new System.Drawing.Size(326, 156);
            this.paymentHistoryDataGridView.TabIndex = 8;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.tableLayoutPanel7);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(3, 187);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(332, 420);
            this.panelRight.TabIndex = 0;
            // 
            // groupBoxPayment
            // 
            this.groupBoxPayment.AutoSize = true;
            this.groupBoxPayment.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxPayment.Controls.Add(this.tableLayoutPanel6);
            this.groupBoxPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPayment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxPayment.Location = new System.Drawing.Point(3, 245);
            this.groupBoxPayment.Name = "groupBoxPayment";
            this.groupBoxPayment.Size = new System.Drawing.Size(326, 172);
            this.groupBoxPayment.TabIndex = 1;
            this.groupBoxPayment.TabStop = false;
            this.groupBoxPayment.Text = "Payment Entry";
            this.groupBoxPayment.Enter += new System.EventHandler(this.groupBoxPayment_Enter);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.saveButton, 1, 3);
            this.tableLayoutPanel6.Controls.Add(this.balanceTextBox, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label10, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.amountRecievedNumericUpDown, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.changeTextBox, 1, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(320, 150);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.SystemColors.Control;
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveButton.Location = new System.Drawing.Point(163, 114);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(154, 33);
            this.saveButton.TabIndex = 28;
            this.saveButton.Text = "Submit Payment";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // balanceTextBox
            // 
            this.balanceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.balanceTextBox.Location = new System.Drawing.Point(163, 77);
            this.balanceTextBox.Name = "balanceTextBox";
            this.balanceTextBox.ReadOnly = true;
            this.balanceTextBox.Size = new System.Drawing.Size(154, 23);
            this.balanceTextBox.TabIndex = 31;
            this.balanceTextBox.TextChanged += new System.EventHandler(this.balanceTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 37);
            this.label6.TabIndex = 24;
            this.label6.Text = "Amount Received:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(154, 37);
            this.label10.TabIndex = 30;
            this.label10.Text = "Balance:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // amountRecievedNumericUpDown
            // 
            this.amountRecievedNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.amountRecievedNumericUpDown.Location = new System.Drawing.Point(163, 3);
            this.amountRecievedNumericUpDown.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.amountRecievedNumericUpDown.Name = "amountRecievedNumericUpDown";
            this.amountRecievedNumericUpDown.Size = new System.Drawing.Size(154, 23);
            this.amountRecievedNumericUpDown.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(154, 37);
            this.label7.TabIndex = 25;
            this.label7.Text = "Change:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // changeTextBox
            // 
            this.changeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.changeTextBox.Location = new System.Drawing.Point(163, 40);
            this.changeTextBox.Name = "changeTextBox";
            this.changeTextBox.ReadOnly = true;
            this.changeTextBox.Size = new System.Drawing.Size(154, 23);
            this.changeTextBox.TabIndex = 26;
            // 
            // groupBoxSummary
            // 
            this.groupBoxSummary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxSummary.Controls.Add(this.tableLayoutPanel5);
            this.groupBoxSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSummary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxSummary.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSummary.Name = "groupBoxSummary";
            this.groupBoxSummary.Size = new System.Drawing.Size(326, 236);
            this.groupBoxSummary.TabIndex = 0;
            this.groupBoxSummary.TabStop = false;
            this.groupBoxSummary.Text = "Billing Summary";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.labelRemainingBalance, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.label9, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.paymentStatusLabel, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.remainingBalanceTextBox, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.doctorsFeeTextBox, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.discountPercentLabel, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.noteTextBox, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.discountAmountTextBox, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.totalBillTextBox, 1, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(320, 214);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // labelRemainingBalance
            // 
            this.labelRemainingBalance.AutoSize = true;
            this.labelRemainingBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRemainingBalance.Location = new System.Drawing.Point(3, 175);
            this.labelRemainingBalance.Name = "labelRemainingBalance";
            this.labelRemainingBalance.Size = new System.Drawing.Size(154, 39);
            this.labelRemainingBalance.TabIndex = 11;
            this.labelRemainingBalance.Text = "Remaining Balance:";
            this.labelRemainingBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelRemainingBalance.Visible = false;
            this.labelRemainingBalance.Click += new System.EventHandler(this.labelRemainingBalance_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(154, 35);
            this.label9.TabIndex = 9;
            this.label9.Text = "Payment Status:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // paymentStatusLabel
            // 
            this.paymentStatusLabel.AutoSize = true;
            this.paymentStatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paymentStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.paymentStatusLabel.Location = new System.Drawing.Point(163, 140);
            this.paymentStatusLabel.Name = "paymentStatusLabel";
            this.paymentStatusLabel.Size = new System.Drawing.Size(154, 35);
            this.paymentStatusLabel.TabIndex = 10;
            this.paymentStatusLabel.Text = "N/A";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 35);
            this.label4.TabIndex = 7;
            this.label4.Text = "Note:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // remainingBalanceTextBox
            // 
            this.remainingBalanceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remainingBalanceTextBox.Location = new System.Drawing.Point(163, 178);
            this.remainingBalanceTextBox.Name = "remainingBalanceTextBox";
            this.remainingBalanceTextBox.ReadOnly = true;
            this.remainingBalanceTextBox.Size = new System.Drawing.Size(154, 23);
            this.remainingBalanceTextBox.TabIndex = 12;
            this.remainingBalanceTextBox.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "Doctor\'s Fee:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 35);
            this.label3.TabIndex = 5;
            this.label3.Text = "Total Bill:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // doctorsFeeTextBox
            // 
            this.doctorsFeeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doctorsFeeTextBox.Location = new System.Drawing.Point(163, 3);
            this.doctorsFeeTextBox.Name = "doctorsFeeTextBox";
            this.doctorsFeeTextBox.ReadOnly = true;
            this.doctorsFeeTextBox.Size = new System.Drawing.Size(154, 23);
            this.doctorsFeeTextBox.TabIndex = 2;
            // 
            // discountPercentLabel
            // 
            this.discountPercentLabel.AutoSize = true;
            this.discountPercentLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discountPercentLabel.Location = new System.Drawing.Point(3, 35);
            this.discountPercentLabel.Name = "discountPercentLabel";
            this.discountPercentLabel.Size = new System.Drawing.Size(154, 35);
            this.discountPercentLabel.TabIndex = 3;
            this.discountPercentLabel.Text = "Discount (0%):";
            this.discountPercentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // noteTextBox
            // 
            this.noteTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.noteTextBox.Location = new System.Drawing.Point(163, 108);
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.ReadOnly = true;
            this.noteTextBox.Size = new System.Drawing.Size(154, 23);
            this.noteTextBox.TabIndex = 8;
            // 
            // discountAmountTextBox
            // 
            this.discountAmountTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discountAmountTextBox.Location = new System.Drawing.Point(163, 38);
            this.discountAmountTextBox.Name = "discountAmountTextBox";
            this.discountAmountTextBox.ReadOnly = true;
            this.discountAmountTextBox.Size = new System.Drawing.Size(154, 23);
            this.discountAmountTextBox.TabIndex = 4;
            // 
            // totalBillTextBox
            // 
            this.totalBillTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalBillTextBox.Location = new System.Drawing.Point(163, 73);
            this.totalBillTextBox.Name = "totalBillTextBox";
            this.totalBillTextBox.ReadOnly = true;
            this.totalBillTextBox.Size = new System.Drawing.Size(154, 23);
            this.totalBillTextBox.TabIndex = 6;
            // 
            // searchItemtButton
            // 
            this.searchItemtButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.searchItemtButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchItemtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchItemtButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchItemtButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchItemtButton.Location = new System.Drawing.Point(590, 5);
            this.searchItemtButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchItemtButton.Name = "searchItemtButton";
            this.searchItemtButton.Size = new System.Drawing.Size(84, 33);
            this.searchItemtButton.TabIndex = 4;
            this.searchItemtButton.Text = "Search";
            this.searchItemtButton.UseVisualStyleBackColor = false;
            this.searchItemtButton.Click += new System.EventHandler(this.searchPatientButton_Click);
            // 
            // billing_id
            // 
            this.billing_id.DataPropertyName = "billing_id";
            this.billing_id.HeaderText = "Billing ID";
            this.billing_id.Name = "billing_id";
            this.billing_id.ReadOnly = true;
            this.billing_id.Visible = false;
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
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.updated_at.DefaultCellStyle = dataGridViewCellStyle2;
            this.updated_at.HeaderText = "Date Billed";
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
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Controls.Add(this.groupBoxSummary, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.groupBoxPayment, 0, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.61905F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.38095F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(332, 420);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // BillingInvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1625, 610);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BillingInvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Billing Invoice";
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
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountRecievedNumericUpDown)).EndInit();
            this.groupBoxSummary.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
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
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private Button searchItemtButton;
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
        private TableLayoutPanel tableLayoutPanel7;
    }
}
