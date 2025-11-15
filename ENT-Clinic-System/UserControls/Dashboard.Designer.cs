namespace ENT_Clinic_System.UserControls
{
    partial class Dashboard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Top controls
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.ComboBox cboZoneFilter;
        private System.Windows.Forms.ComboBox cboServiceFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExportCsv;

        // Tabs
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMonthly;
        private System.Windows.Forms.TabPage tabDaily;
        private System.Windows.Forms.TabPage tabPatientStats;

        // Monthly controls
        private System.Windows.Forms.SplitContainer splitMonthly;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEnt;
        private System.Windows.Forms.DataGridView dgvEnt;

        // Daily controls
        private System.Windows.Forms.SplitContainer splitDaily;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDaily;
        private System.Windows.Forms.DataGridView dgvDaily;

        // Patient Stats controls (now vertical split: left grid, right chart)
        private System.Windows.Forms.SplitContainer splitPatientStats;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPatientStats;
        private System.Windows.Forms.DataGridView dgvPatientStats;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.cboZoneFilter = new System.Windows.Forms.ComboBox();
            this.cboServiceFilter = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMonthly = new System.Windows.Forms.TabPage();
            this.splitMonthly = new System.Windows.Forms.SplitContainer();
            this.chartEnt = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvEnt = new System.Windows.Forms.DataGridView();
            this.tabDaily = new System.Windows.Forms.TabPage();
            this.splitDaily = new System.Windows.Forms.SplitContainer();
            this.chartDaily = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvDaily = new System.Windows.Forms.DataGridView();
            this.tabPatientStats = new System.Windows.Forms.TabPage();
            this.splitPatientStats = new System.Windows.Forms.SplitContainer();
            this.dgvPatientStats = new System.Windows.Forms.DataGridView();
            this.chartPatientStats = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.DayDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DayLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EarCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoseCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThroatCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OthersCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalConsultss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonthNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonthName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EarCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoseCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThroatCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OthersCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalConsults = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgeGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountPatients = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMonthly)).BeginInit();
            this.splitMonthly.Panel1.SuspendLayout();
            this.splitMonthly.Panel2.SuspendLayout();
            this.splitMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnt)).BeginInit();
            this.tabDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDaily)).BeginInit();
            this.splitDaily.Panel1.SuspendLayout();
            this.splitDaily.Panel2.SuspendLayout();
            this.splitDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaily)).BeginInit();
            this.tabPatientStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPatientStats)).BeginInit();
            this.splitPatientStats.Panel1.SuspendLayout();
            this.splitPatientStats.Panel2.SuspendLayout();
            this.splitPatientStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPatientStats)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTop.Controls.Add(this.cboZoneFilter);
            this.pnlTop.Controls.Add(this.cboServiceFilter);
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Controls.Add(this.btnExportCsv);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(8);
            this.pnlTop.Size = new System.Drawing.Size(980, 10);
            this.pnlTop.TabIndex = 1;
            this.pnlTop.Visible = false;
            // 
            // cboZoneFilter
            // 
            this.cboZoneFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboZoneFilter.Location = new System.Drawing.Point(8, 12);
            this.cboZoneFilter.Name = "cboZoneFilter";
            this.cboZoneFilter.Size = new System.Drawing.Size(160, 21);
            this.cboZoneFilter.TabIndex = 0;
            // 
            // cboServiceFilter
            // 
            this.cboServiceFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServiceFilter.Location = new System.Drawing.Point(176, 12);
            this.cboServiceFilter.Name = "cboServiceFilter";
            this.cboServiceFilter.Size = new System.Drawing.Size(160, 21);
            this.cboServiceFilter.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(344, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Location = new System.Drawing.Point(438, 10);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(100, 23);
            this.btnExportCsv.TabIndex = 3;
            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.Click += new System.EventHandler(this.BtnExportCsv_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabMonthly);
            this.tabControl.Controls.Add(this.tabDaily);
            this.tabControl.Controls.Add(this.tabPatientStats);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 10);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(980, 670);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabMonthly
            // 
            this.tabMonthly.Controls.Add(this.splitMonthly);
            this.tabMonthly.Location = new System.Drawing.Point(4, 22);
            this.tabMonthly.Name = "tabMonthly";
            this.tabMonthly.Padding = new System.Windows.Forms.Padding(3);
            this.tabMonthly.Size = new System.Drawing.Size(972, 644);
            this.tabMonthly.TabIndex = 0;
            this.tabMonthly.Text = "Monthly Summary";
            // 
            // splitMonthly
            // 
            this.splitMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMonthly.Location = new System.Drawing.Point(3, 3);
            this.splitMonthly.Name = "splitMonthly";
            this.splitMonthly.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMonthly.Panel1
            // 
            this.splitMonthly.Panel1.Controls.Add(this.chartEnt);
            // 
            // splitMonthly.Panel2
            // 
            this.splitMonthly.Panel2.Controls.Add(this.dgvEnt);
            this.splitMonthly.Size = new System.Drawing.Size(966, 638);
            this.splitMonthly.SplitterDistance = 429;
            this.splitMonthly.TabIndex = 0;
            // 
            // chartEnt
            // 
            this.chartEnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEnt.Location = new System.Drawing.Point(0, 0);
            this.chartEnt.Name = "chartEnt";
            this.chartEnt.Size = new System.Drawing.Size(966, 429);
            this.chartEnt.TabIndex = 0;
            this.chartEnt.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChartEnt_MouseClick);
            // 
            // dgvEnt
            // 
            this.dgvEnt.AllowUserToAddRows = false;
            this.dgvEnt.AllowUserToDeleteRows = false;
            this.dgvEnt.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEnt.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvEnt.ColumnHeadersHeight = 30;
            this.dgvEnt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MonthNumber,
            this.MonthName,
            this.EarCount,
            this.NoseCount,
            this.ThroatCount,
            this.OthersCount,
            this.TotalConsults});
            this.dgvEnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEnt.Location = new System.Drawing.Point(0, 0);
            this.dgvEnt.Name = "dgvEnt";
            this.dgvEnt.ReadOnly = true;
            this.dgvEnt.Size = new System.Drawing.Size(966, 205);
            this.dgvEnt.TabIndex = 0;
            // 
            // tabDaily
            // 
            this.tabDaily.Controls.Add(this.splitDaily);
            this.tabDaily.Location = new System.Drawing.Point(4, 22);
            this.tabDaily.Name = "tabDaily";
            this.tabDaily.Size = new System.Drawing.Size(972, 644);
            this.tabDaily.TabIndex = 1;
            this.tabDaily.Text = "Daily Summary";
            // 
            // splitDaily
            // 
            this.splitDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDaily.Location = new System.Drawing.Point(0, 0);
            this.splitDaily.Name = "splitDaily";
            this.splitDaily.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitDaily.Panel1
            // 
            this.splitDaily.Panel1.Controls.Add(this.chartDaily);
            // 
            // splitDaily.Panel2
            // 
            this.splitDaily.Panel2.Controls.Add(this.dgvDaily);
            this.splitDaily.Size = new System.Drawing.Size(972, 644);
            this.splitDaily.SplitterDistance = 456;
            this.splitDaily.TabIndex = 0;
            // 
            // chartDaily
            // 
            this.chartDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDaily.Location = new System.Drawing.Point(0, 0);
            this.chartDaily.Name = "chartDaily";
            this.chartDaily.Size = new System.Drawing.Size(972, 456);
            this.chartDaily.TabIndex = 0;
            this.chartDaily.Text = "30";
            // 
            // dgvDaily
            // 
            this.dgvDaily.AllowUserToAddRows = false;
            this.dgvDaily.AllowUserToDeleteRows = false;
            this.dgvDaily.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDaily.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDaily.ColumnHeadersHeight = 30;
            this.dgvDaily.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DayDate,
            this.DayLabel,
            this.EarCounts,
            this.NoseCounts,
            this.ThroatCounts,
            this.OthersCounts,
            this.TotalConsultss});
            this.dgvDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDaily.Location = new System.Drawing.Point(0, 0);
            this.dgvDaily.Name = "dgvDaily";
            this.dgvDaily.ReadOnly = true;
            this.dgvDaily.Size = new System.Drawing.Size(972, 184);
            this.dgvDaily.TabIndex = 0;
            // 
            // tabPatientStats
            // 
            this.tabPatientStats.Controls.Add(this.splitPatientStats);
            this.tabPatientStats.Location = new System.Drawing.Point(4, 22);
            this.tabPatientStats.Name = "tabPatientStats";
            this.tabPatientStats.Size = new System.Drawing.Size(972, 644);
            this.tabPatientStats.TabIndex = 3;
            this.tabPatientStats.Text = "Age Group";
            // 
            // splitPatientStats
            // 
            this.splitPatientStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPatientStats.Location = new System.Drawing.Point(0, 0);
            this.splitPatientStats.Name = "splitPatientStats";
            // 
            // splitPatientStats.Panel1
            // 
            this.splitPatientStats.Panel1.Controls.Add(this.dgvPatientStats);
            this.splitPatientStats.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitPatientStats_Panel1_Paint);
            // 
            // splitPatientStats.Panel2
            // 
            this.splitPatientStats.Panel2.Controls.Add(this.chartPatientStats);
            this.splitPatientStats.Size = new System.Drawing.Size(972, 644);
            this.splitPatientStats.SplitterDistance = 124;
            this.splitPatientStats.TabIndex = 0;
            // 
            // dgvPatientStats
            // 
            this.dgvPatientStats.AllowUserToAddRows = false;
            this.dgvPatientStats.AllowUserToDeleteRows = false;
            this.dgvPatientStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPatientStats.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPatientStats.ColumnHeadersHeight = 30;
            this.dgvPatientStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AgeGroup,
            this.CountPatients});
            this.dgvPatientStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPatientStats.Location = new System.Drawing.Point(0, 0);
            this.dgvPatientStats.Name = "dgvPatientStats";
            this.dgvPatientStats.ReadOnly = true;
            this.dgvPatientStats.Size = new System.Drawing.Size(124, 644);
            this.dgvPatientStats.TabIndex = 0;
            // 
            // chartPatientStats
            // 
            this.chartPatientStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPatientStats.Location = new System.Drawing.Point(0, 0);
            this.chartPatientStats.Name = "chartPatientStats";
            this.chartPatientStats.Size = new System.Drawing.Size(844, 644);
            this.chartPatientStats.TabIndex = 0;
            // 
            // DayDate
            // 
            this.DayDate.DataPropertyName = "DayDate";
            this.DayDate.HeaderText = "Column1";
            this.DayDate.Name = "DayDate";
            this.DayDate.Visible = false;
            // 
            // DayLabel
            // 
            this.DayLabel.DataPropertyName = "DayLabel";
            this.DayLabel.HeaderText = "Date";
            this.DayLabel.Name = "DayLabel";
            // 
            // EarCounts
            // 
            this.EarCounts.DataPropertyName = "EarCount";
            this.EarCounts.HeaderText = "Ears";
            this.EarCounts.Name = "EarCounts";
            // 
            // NoseCounts
            // 
            this.NoseCounts.DataPropertyName = "NoseCount";
            this.NoseCounts.HeaderText = "Nose";
            this.NoseCounts.Name = "NoseCounts";
            // 
            // ThroatCounts
            // 
            this.ThroatCounts.DataPropertyName = "ThroatCount";
            this.ThroatCounts.HeaderText = "Throat";
            this.ThroatCounts.Name = "ThroatCounts";
            // 
            // OthersCounts
            // 
            this.OthersCounts.DataPropertyName = "OthersCount";
            this.OthersCounts.HeaderText = "Others";
            this.OthersCounts.Name = "OthersCounts";
            // 
            // TotalConsultss
            // 
            this.TotalConsultss.DataPropertyName = "TotalConsults";
            this.TotalConsultss.HeaderText = "Number of Consultations";
            this.TotalConsultss.Name = "TotalConsultss";
            // 
            // MonthNumber
            // 
            this.MonthNumber.DataPropertyName = "MonthNumber";
            this.MonthNumber.HeaderText = "Column1";
            this.MonthNumber.Name = "MonthNumber";
            this.MonthNumber.ReadOnly = true;
            this.MonthNumber.Visible = false;
            // 
            // MonthName
            // 
            this.MonthName.DataPropertyName = "MonthName";
            this.MonthName.HeaderText = "Month";
            this.MonthName.Name = "MonthName";
            this.MonthName.ReadOnly = true;
            // 
            // EarCount
            // 
            this.EarCount.DataPropertyName = "EarCount";
            this.EarCount.HeaderText = "Ears";
            this.EarCount.Name = "EarCount";
            this.EarCount.ReadOnly = true;
            // 
            // NoseCount
            // 
            this.NoseCount.DataPropertyName = "NoseCount";
            this.NoseCount.HeaderText = "Nose";
            this.NoseCount.Name = "NoseCount";
            this.NoseCount.ReadOnly = true;
            // 
            // ThroatCount
            // 
            this.ThroatCount.DataPropertyName = "ThroatCount";
            this.ThroatCount.HeaderText = "Throat";
            this.ThroatCount.Name = "ThroatCount";
            this.ThroatCount.ReadOnly = true;
            // 
            // OthersCount
            // 
            this.OthersCount.DataPropertyName = "OthersCount";
            this.OthersCount.HeaderText = "Others";
            this.OthersCount.Name = "OthersCount";
            this.OthersCount.ReadOnly = true;
            // 
            // TotalConsults
            // 
            this.TotalConsults.DataPropertyName = "TotalConsults";
            this.TotalConsults.HeaderText = "Number of Consultations";
            this.TotalConsults.Name = "TotalConsults";
            this.TotalConsults.ReadOnly = true;
            // 
            // AgeGroup
            // 
            this.AgeGroup.DataPropertyName = "AgeGroup";
            this.AgeGroup.FillWeight = 149.2386F;
            this.AgeGroup.HeaderText = "Age";
            this.AgeGroup.Name = "AgeGroup";
            this.AgeGroup.ReadOnly = true;
            // 
            // CountPatients
            // 
            this.CountPatients.DataPropertyName = "CountPatients";
            this.CountPatients.FillWeight = 50.76142F;
            this.CountPatients.HeaderText = "#";
            this.CountPatients.Name = "CountPatients";
            this.CountPatients.ReadOnly = true;
            // 
            // Dashboard
            // 
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlTop);
            this.Name = "Dashboard";
            this.Size = new System.Drawing.Size(980, 680);
            this.pnlTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabMonthly.ResumeLayout(false);
            this.splitMonthly.Panel1.ResumeLayout(false);
            this.splitMonthly.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMonthly)).EndInit();
            this.splitMonthly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartEnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnt)).EndInit();
            this.tabDaily.ResumeLayout(false);
            this.splitDaily.Panel1.ResumeLayout(false);
            this.splitDaily.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDaily)).EndInit();
            this.splitDaily.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaily)).EndInit();
            this.tabPatientStats.ResumeLayout(false);
            this.splitPatientStats.Panel1.ResumeLayout(false);
            this.splitPatientStats.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPatientStats)).EndInit();
            this.splitPatientStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPatientStats)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn MonthNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonthName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EarCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoseCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThroatCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OthersCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalConsults;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn EarCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoseCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThroatCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn OthersCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalConsultss;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgeGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountPatients;
    }
}
