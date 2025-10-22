using System;

namespace ENT_Clinic_System.Consultation
{
    partial class PrescriptionForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView dgvAvailableItems;
        private System.Windows.Forms.DataGridView dgvSelectedItems;
        private System.Windows.Forms.Button btnSubmit;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrescriptionForm));
            this.dgvAvailableItems = new System.Windows.Forms.DataGridView();
            this.dgvSelectedItems = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.selectedOtherDGV = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.descriptionComboBox = new System.Windows.Forms.ComboBox();
            this.updateItemButton = new System.Windows.Forms.Button();
            this.addItemButton = new System.Windows.Forms.Button();
            this.genericNameComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblItemName = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.brandNameComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.dosageComboBox = new System.Windows.Forms.ComboBox();
            this.stregnthComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvOtherItems = new System.Windows.Forms.DataGridView();
            this.groupBoxAvailable = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.sortCategoryCombobox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.refreshPatientsButton = new System.Windows.Forms.Button();
            this.searchItemtButton = new System.Windows.Forms.Button();
            this.searchItemsTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedItems)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectedOtherDGV)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherItems)).BeginInit();
            this.groupBoxAvailable.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAvailableItems
            // 
            this.dgvAvailableItems.AllowUserToAddRows = false;
            this.dgvAvailableItems.AllowUserToDeleteRows = false;
            this.dgvAvailableItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAvailableItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvAvailableItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailableItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAvailableItems.Location = new System.Drawing.Point(3, 19);
            this.dgvAvailableItems.MultiSelect = false;
            this.dgvAvailableItems.Name = "dgvAvailableItems";
            this.dgvAvailableItems.ReadOnly = true;
            this.dgvAvailableItems.RowHeadersVisible = false;
            this.dgvAvailableItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvailableItems.Size = new System.Drawing.Size(760, 428);
            this.dgvAvailableItems.TabIndex = 0;
            // 
            // dgvSelectedItems
            // 
            this.dgvSelectedItems.AllowUserToAddRows = false;
            this.dgvSelectedItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSelectedItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvSelectedItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelectedItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelectedItems.Location = new System.Drawing.Point(3, 21);
            this.dgvSelectedItems.MultiSelect = false;
            this.dgvSelectedItems.Name = "dgvSelectedItems";
            this.dgvSelectedItems.RowHeadersVisible = false;
            this.dgvSelectedItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelectedItems.Size = new System.Drawing.Size(755, 422);
            this.dgvSelectedItems.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSubmit.Location = new System.Drawing.Point(540, 3);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(102, 32);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1557, 961);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(781, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.60154F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(773, 955);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(767, 905);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.selectedOtherDGV);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(3, 455);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(761, 447);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Selected  Other Items";
            // 
            // selectedOtherDGV
            // 
            this.selectedOtherDGV.AllowUserToAddRows = false;
            this.selectedOtherDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.selectedOtherDGV.BackgroundColor = System.Drawing.Color.White;
            this.selectedOtherDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectedOtherDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedOtherDGV.Location = new System.Drawing.Point(3, 21);
            this.selectedOtherDGV.MultiSelect = false;
            this.selectedOtherDGV.Name = "selectedOtherDGV";
            this.selectedOtherDGV.RowHeadersVisible = false;
            this.selectedOtherDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.selectedOtherDGV.Size = new System.Drawing.Size(755, 423);
            this.selectedOtherDGV.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.dgvSelectedItems);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(761, 446);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Items";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.18123F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.81878F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel4.Controls.Add(this.button2, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.clearButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSubmit, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 914);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(767, 38);
            this.tableLayoutPanel4.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.Location = new System.Drawing.Point(661, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 32);
            this.button2.TabIndex = 4;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // clearButton
            // 
            this.clearButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.clearButton.Location = new System.Drawing.Point(414, 3);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(120, 32);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel5);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(772, 955);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.groupBoxAvailable, 0, 0);
            this.tableLayoutPanel5.Cursor = System.Windows.Forms.Cursors.Default;
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 42);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(772, 913);
            this.tableLayoutPanel5.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel7);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(3, 459);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 451);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Others Items";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.dgvOtherItems, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.57377F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 166F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(760, 429);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel8);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 266);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(754, 160);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "(Others) Item Management";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 5;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.80645F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.19355F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.80645F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.19355F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.descriptionComboBox, 3, 1);
            this.tableLayoutPanel8.Controls.Add(this.updateItemButton, 4, 1);
            this.tableLayoutPanel8.Controls.Add(this.addItemButton, 4, 0);
            this.tableLayoutPanel8.Controls.Add(this.genericNameComboBox, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel8.Controls.Add(this.lblItemName, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.categoryComboBox, 3, 0);
            this.tableLayoutPanel8.Controls.Add(this.brandNameComboBox, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblCategory, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.dosageComboBox, 1, 3);
            this.tableLayoutPanel8.Controls.Add(this.stregnthComboBox, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.button1, 4, 2);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 4;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(748, 138);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 34);
            this.label1.TabIndex = 11;
            this.label1.Text = "Generic Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // descriptionComboBox
            // 
            this.descriptionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionComboBox.FormattingEnabled = true;
            this.descriptionComboBox.Location = new System.Drawing.Point(405, 39);
            this.descriptionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.descriptionComboBox.Name = "descriptionComboBox";
            this.descriptionComboBox.Size = new System.Drawing.Size(187, 24);
            this.descriptionComboBox.TabIndex = 24;
            // 
            // updateItemButton
            // 
            this.updateItemButton.BackColor = System.Drawing.SystemColors.Control;
            this.updateItemButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateItemButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateItemButton.Location = new System.Drawing.Point(600, 39);
            this.updateItemButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.updateItemButton.Name = "updateItemButton";
            this.updateItemButton.Size = new System.Drawing.Size(144, 24);
            this.updateItemButton.TabIndex = 9;
            this.updateItemButton.Text = "Update Item";
            this.updateItemButton.UseVisualStyleBackColor = false;
            this.updateItemButton.Click += new System.EventHandler(this.updateItemButton_Click);
            // 
            // addItemButton
            // 
            this.addItemButton.BackColor = System.Drawing.SystemColors.Control;
            this.addItemButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addItemButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addItemButton.Location = new System.Drawing.Point(600, 5);
            this.addItemButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.addItemButton.Name = "addItemButton";
            this.addItemButton.Size = new System.Drawing.Size(144, 24);
            this.addItemButton.TabIndex = 8;
            this.addItemButton.Text = "Add Item";
            this.addItemButton.UseVisualStyleBackColor = false;
            this.addItemButton.Click += new System.EventHandler(this.addItemButton_Click);
            // 
            // genericNameComboBox
            // 
            this.genericNameComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.genericNameComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genericNameComboBox.FormattingEnabled = true;
            this.genericNameComboBox.Location = new System.Drawing.Point(107, 5);
            this.genericNameComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.genericNameComboBox.Name = "genericNameComboBox";
            this.genericNameComboBox.Size = new System.Drawing.Size(187, 24);
            this.genericNameComboBox.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(302, 34);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 34);
            this.label6.TabIndex = 23;
            this.label6.Text = "Description:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemName.Location = new System.Drawing.Point(4, 34);
            this.lblItemName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(95, 34);
            this.lblItemName.TabIndex = 0;
            this.lblItemName.Text = "Brand Name:";
            this.lblItemName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(405, 5);
            this.categoryComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(187, 24);
            this.categoryComboBox.TabIndex = 22;
            // 
            // brandNameComboBox
            // 
            this.brandNameComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brandNameComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brandNameComboBox.FormattingEnabled = true;
            this.brandNameComboBox.Location = new System.Drawing.Point(107, 39);
            this.brandNameComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.brandNameComboBox.Name = "brandNameComboBox";
            this.brandNameComboBox.Size = new System.Drawing.Size(187, 24);
            this.brandNameComboBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(302, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 34);
            this.label5.TabIndex = 21;
            this.label5.Text = "Category:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(4, 68);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(95, 34);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "Strength:";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dosageComboBox
            // 
            this.dosageComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dosageComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dosageComboBox.FormattingEnabled = true;
            this.dosageComboBox.Location = new System.Drawing.Point(107, 107);
            this.dosageComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.dosageComboBox.Name = "dosageComboBox";
            this.dosageComboBox.Size = new System.Drawing.Size(187, 24);
            this.dosageComboBox.TabIndex = 20;
            // 
            // stregnthComboBox
            // 
            this.stregnthComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stregnthComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stregnthComboBox.FormattingEnabled = true;
            this.stregnthComboBox.Location = new System.Drawing.Point(107, 73);
            this.stregnthComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.stregnthComboBox.Name = "stregnthComboBox";
            this.stregnthComboBox.Size = new System.Drawing.Size(187, 24);
            this.stregnthComboBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 102);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 36);
            this.label4.TabIndex = 19;
            this.label4.Text = "Dosage:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(600, 73);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 24);
            this.button1.TabIndex = 25;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvOtherItems
            // 
            this.dgvOtherItems.AllowUserToAddRows = false;
            this.dgvOtherItems.AllowUserToDeleteRows = false;
            this.dgvOtherItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOtherItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvOtherItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOtherItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOtherItems.Location = new System.Drawing.Point(3, 3);
            this.dgvOtherItems.MultiSelect = false;
            this.dgvOtherItems.Name = "dgvOtherItems";
            this.dgvOtherItems.ReadOnly = true;
            this.dgvOtherItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOtherItems.Size = new System.Drawing.Size(754, 257);
            this.dgvOtherItems.TabIndex = 1;
            // 
            // groupBoxAvailable
            // 
            this.groupBoxAvailable.Controls.Add(this.dgvAvailableItems);
            this.groupBoxAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAvailable.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxAvailable.Location = new System.Drawing.Point(3, 3);
            this.groupBoxAvailable.Name = "groupBoxAvailable";
            this.groupBoxAvailable.Size = new System.Drawing.Size(766, 450);
            this.groupBoxAvailable.TabIndex = 7;
            this.groupBoxAvailable.TabStop = false;
            this.groupBoxAvailable.Text = "Available Items (Inventory)";
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
            this.tableLayoutPanel2.Controls.Add(this.sortCategoryCombobox, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.refreshPatientsButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchItemtButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchItemsTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(772, 42);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // sortCategoryCombobox
            // 
            this.sortCategoryCombobox.BackColor = System.Drawing.SystemColors.Control;
            this.sortCategoryCombobox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sortCategoryCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortCategoryCombobox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sortCategoryCombobox.FormattingEnabled = true;
            this.sortCategoryCombobox.Location = new System.Drawing.Point(568, 6);
            this.sortCategoryCombobox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 8);
            this.sortCategoryCombobox.Name = "sortCategoryCombobox";
            this.sortCategoryCombobox.Size = new System.Drawing.Size(200, 28);
            this.sortCategoryCombobox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(489, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 42);
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
            this.refreshPatientsButton.Location = new System.Drawing.Point(443, 5);
            this.refreshPatientsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.refreshPatientsButton.Name = "refreshPatientsButton";
            this.refreshPatientsButton.Size = new System.Drawing.Size(38, 32);
            this.refreshPatientsButton.TabIndex = 3;
            this.refreshPatientsButton.Text = "⟳";
            this.refreshPatientsButton.UseVisualStyleBackColor = false;
            // 
            // searchItemtButton
            // 
            this.searchItemtButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.searchItemtButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchItemtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchItemtButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchItemtButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchItemtButton.Location = new System.Drawing.Point(399, 5);
            this.searchItemtButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchItemtButton.Name = "searchItemtButton";
            this.searchItemtButton.Size = new System.Drawing.Size(36, 32);
            this.searchItemtButton.TabIndex = 0;
            this.searchItemtButton.Text = "🔎";
            this.searchItemtButton.UseVisualStyleBackColor = false;
            // 
            // searchItemsTextBox
            // 
            this.searchItemsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchItemsTextBox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchItemsTextBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchItemsTextBox.Location = new System.Drawing.Point(134, 11);
            this.searchItemsTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.searchItemsTextBox.Name = "searchItemsTextBox";
            this.searchItemsTextBox.Size = new System.Drawing.Size(257, 26);
            this.searchItemsTextBox.TabIndex = 1;
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
            this.label8.Size = new System.Drawing.Size(122, 42);
            this.label8.TabIndex = 2;
            this.label8.Text = "Search Item:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PrescriptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1557, 961);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Arial Narrow", 11.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PrescriptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prescription - ENT Clinic";
            this.Load += new System.EventHandler(this.PrescriptionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedItems)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.selectedOtherDGV)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherItems)).EndInit();
            this.groupBoxAvailable.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBoxAvailable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button refreshPatientsButton;
        private System.Windows.Forms.Button searchItemtButton;
        private System.Windows.Forms.TextBox searchItemsTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ComboBox sortCategoryCombobox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvOtherItems;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView selectedOtherDGV;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox descriptionComboBox;
        private System.Windows.Forms.Button updateItemButton;
        private System.Windows.Forms.Button addItemButton;
        private System.Windows.Forms.ComboBox genericNameComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.ComboBox brandNameComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox dosageComboBox;
        private System.Windows.Forms.ComboBox stregnthComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}
