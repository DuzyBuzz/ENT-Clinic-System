namespace ENT_Clinic_System.Inventory
{
    partial class WriteOffForm
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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.groupBoxAvailable = new System.Windows.Forms.GroupBox();
            this.availableItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBoxSelected = new System.Windows.Forms.GroupBox();
            this.selectedItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBoxWriteOffDetails = new System.Windows.Forms.GroupBox();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.numericQuantity = new System.Windows.Forms.NumericUpDown();
            this.labelReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.labelDate = new System.Windows.Forms.Label();
            this.dtpWriteOffDate = new System.Windows.Forms.DateTimePicker();
            this.btnAddWriteOff = new System.Windows.Forms.Button();
            this.groupBoxHistory = new System.Windows.Forms.GroupBox();
            this.dgvWriteOffHistory = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.groupBoxAvailable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.availableItemsDataGridView)).BeginInit();
            this.groupBoxSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectedItemsDataGridView)).BeginInit();
            this.groupBoxWriteOffDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).BeginInit();
            this.groupBoxHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWriteOffHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelMain.Controls.Add(this.panelTop, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxSelected, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxWriteOffDetails, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxHistory, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1586, 933);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.groupBoxAvailable);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(3, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(5);
            this.panelTop.Size = new System.Drawing.Size(945, 647);
            this.panelTop.TabIndex = 0;
            // 
            // groupBoxAvailable
            // 
            this.groupBoxAvailable.Controls.Add(this.availableItemsDataGridView);
            this.groupBoxAvailable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxAvailable.Location = new System.Drawing.Point(5, 5);
            this.groupBoxAvailable.Name = "groupBoxAvailable";
            this.groupBoxAvailable.Size = new System.Drawing.Size(935, 637);
            this.groupBoxAvailable.TabIndex = 0;
            this.groupBoxAvailable.TabStop = false;
            this.groupBoxAvailable.Text = "Available Items (Inventory)";
            // 
            // availableItemsDataGridView
            // 
            this.availableItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.availableItemsDataGridView.Location = new System.Drawing.Point(3, 16);
            this.availableItemsDataGridView.Name = "availableItemsDataGridView";
            this.availableItemsDataGridView.ReadOnly = true;
            this.availableItemsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.availableItemsDataGridView.Size = new System.Drawing.Size(929, 618);
            this.availableItemsDataGridView.TabIndex = 0;
            // 
            // groupBoxSelected
            // 
            this.groupBoxSelected.Controls.Add(this.selectedItemsDataGridView);
            this.groupBoxSelected.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxSelected.Location = new System.Drawing.Point(954, 3);
            this.groupBoxSelected.Name = "groupBoxSelected";
            this.groupBoxSelected.Size = new System.Drawing.Size(629, 300);
            this.groupBoxSelected.TabIndex = 1;
            this.groupBoxSelected.TabStop = false;
            this.groupBoxSelected.Text = "Selected Items (Write-Off)";
            // 
            // selectedItemsDataGridView
            // 
            this.selectedItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedItemsDataGridView.Location = new System.Drawing.Point(3, 16);
            this.selectedItemsDataGridView.Name = "selectedItemsDataGridView";
            this.selectedItemsDataGridView.ReadOnly = true;
            this.selectedItemsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.selectedItemsDataGridView.Size = new System.Drawing.Size(623, 281);
            this.selectedItemsDataGridView.TabIndex = 0;
            // 
            // groupBoxWriteOffDetails
            // 
            this.groupBoxWriteOffDetails.Controls.Add(this.labelQuantity);
            this.groupBoxWriteOffDetails.Controls.Add(this.numericQuantity);
            this.groupBoxWriteOffDetails.Controls.Add(this.labelReason);
            this.groupBoxWriteOffDetails.Controls.Add(this.txtReason);
            this.groupBoxWriteOffDetails.Controls.Add(this.labelDate);
            this.groupBoxWriteOffDetails.Controls.Add(this.dtpWriteOffDate);
            this.groupBoxWriteOffDetails.Controls.Add(this.btnAddWriteOff);
            this.groupBoxWriteOffDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxWriteOffDetails.Location = new System.Drawing.Point(954, 656);
            this.groupBoxWriteOffDetails.Name = "groupBoxWriteOffDetails";
            this.groupBoxWriteOffDetails.Size = new System.Drawing.Size(629, 274);
            this.groupBoxWriteOffDetails.TabIndex = 2;
            this.groupBoxWriteOffDetails.TabStop = false;
            this.groupBoxWriteOffDetails.Text = "Write-Off Details";
            // 
            // labelQuantity
            // 
            this.labelQuantity.Location = new System.Drawing.Point(20, 30);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(100, 23);
            this.labelQuantity.TabIndex = 0;
            this.labelQuantity.Text = "Quantity:";
            // 
            // numericQuantity
            // 
            this.numericQuantity.Location = new System.Drawing.Point(126, 28);
            this.numericQuantity.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericQuantity.Name = "numericQuantity";
            this.numericQuantity.Size = new System.Drawing.Size(120, 20);
            this.numericQuantity.TabIndex = 1;
            // 
            // labelReason
            // 
            this.labelReason.Location = new System.Drawing.Point(20, 70);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(100, 23);
            this.labelReason.TabIndex = 2;
            this.labelReason.Text = "Reason:";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(126, 67);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(200, 20);
            this.txtReason.TabIndex = 3;
            // 
            // labelDate
            // 
            this.labelDate.Location = new System.Drawing.Point(20, 110);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(100, 23);
            this.labelDate.TabIndex = 4;
            this.labelDate.Text = "Date:";
            // 
            // dtpWriteOffDate
            // 
            this.dtpWriteOffDate.Location = new System.Drawing.Point(126, 104);
            this.dtpWriteOffDate.Name = "dtpWriteOffDate";
            this.dtpWriteOffDate.Size = new System.Drawing.Size(200, 20);
            this.dtpWriteOffDate.TabIndex = 5;
            // 
            // btnAddWriteOff
            // 
            this.btnAddWriteOff.Location = new System.Drawing.Point(251, 151);
            this.btnAddWriteOff.Name = "btnAddWriteOff";
            this.btnAddWriteOff.Size = new System.Drawing.Size(75, 23);
            this.btnAddWriteOff.TabIndex = 6;
            this.btnAddWriteOff.Text = "Submit Write-Off";
            // 
            // groupBoxHistory
            // 
            this.groupBoxHistory.Controls.Add(this.dgvWriteOffHistory);
            this.groupBoxHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxHistory.Location = new System.Drawing.Point(3, 656);
            this.groupBoxHistory.Name = "groupBoxHistory";
            this.groupBoxHistory.Size = new System.Drawing.Size(945, 274);
            this.groupBoxHistory.TabIndex = 3;
            this.groupBoxHistory.TabStop = false;
            this.groupBoxHistory.Text = "Write-Off History";
            // 
            // dgvWriteOffHistory
            // 
            this.dgvWriteOffHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvWriteOffHistory.Location = new System.Drawing.Point(3, 16);
            this.dgvWriteOffHistory.Name = "dgvWriteOffHistory";
            this.dgvWriteOffHistory.ReadOnly = true;
            this.dgvWriteOffHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWriteOffHistory.Size = new System.Drawing.Size(939, 255);
            this.dgvWriteOffHistory.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottom.Location = new System.Drawing.Point(0, 0);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(5);
            this.panelBottom.Size = new System.Drawing.Size(200, 100);
            this.panelBottom.TabIndex = 0;
            // 
            // WriteOffForm
            // 
            this.ClientSize = new System.Drawing.Size(1586, 933);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "WriteOffForm";
            this.Text = "Inventory Write-Off";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.groupBoxAvailable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.availableItemsDataGridView)).EndInit();
            this.groupBoxSelected.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.selectedItemsDataGridView)).EndInit();
            this.groupBoxWriteOffDetails.ResumeLayout(false);
            this.groupBoxWriteOffDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).EndInit();
            this.groupBoxHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWriteOffHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelBottom;

        private System.Windows.Forms.GroupBox groupBoxAvailable;
        private System.Windows.Forms.DataGridView availableItemsDataGridView;

        private System.Windows.Forms.GroupBox groupBoxSelected;
        private System.Windows.Forms.DataGridView selectedItemsDataGridView;

        private System.Windows.Forms.GroupBox groupBoxWriteOffDetails;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.NumericUpDown numericQuantity;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.DateTimePicker dtpWriteOffDate;
        private System.Windows.Forms.Button btnAddWriteOff;

        private System.Windows.Forms.GroupBox groupBoxHistory;
        private System.Windows.Forms.DataGridView dgvWriteOffHistory;
    }
}
