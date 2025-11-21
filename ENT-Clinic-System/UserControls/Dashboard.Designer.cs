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
        private System.Windows.Forms.TabPage tabQueueDaily;
        private System.Windows.Forms.TabPage tabQueueMonthly;
        private System.Windows.Forms.TabPage tabPatientStats;

        // Monthly controls
        private System.Windows.Forms.SplitContainer splitMonthly;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEnt;
        private System.Windows.Forms.DataGridView dgvEnt;

        // Daily controls
        private System.Windows.Forms.SplitContainer splitDaily;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDaily;
        private System.Windows.Forms.DataGridView dgvDaily;

        // Queue Daily controls
        private System.Windows.Forms.SplitContainer splitQueueDaily;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartQueueDaily;
        private System.Windows.Forms.DataGridView dgvQueueDaily;

        // Queue Monthly controls
        private System.Windows.Forms.SplitContainer splitQueueMonthly;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartQueueMonthly;
        private System.Windows.Forms.DataGridView dgvQueueMonthly;

        // Patient Stats controls
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
            this.tabConsultation = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmbExam = new System.Windows.Forms.ComboBox();
            this.chartEntOverview = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvEntOverview = new System.Windows.Forms.DataGridView();
            this.tabDaily = new System.Windows.Forms.TabPage();
            this.splitDaily = new System.Windows.Forms.SplitContainer();
            this.chartDaily = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvDaily = new System.Windows.Forms.DataGridView();
            this.tabMonthly = new System.Windows.Forms.TabPage();
            this.splitMonthly = new System.Windows.Forms.SplitContainer();
            this.chartEnt = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvEnt = new System.Windows.Forms.DataGridView();
            this.tabQueueDaily = new System.Windows.Forms.TabPage();
            this.splitQueueDaily = new System.Windows.Forms.SplitContainer();
            this.chartQueueDaily = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvQueueDaily = new System.Windows.Forms.DataGridView();
            this.QDayDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QDayLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTotalQueued = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCalledCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QFinishedCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WaitingCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SkippedCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CancelledCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabQueueMonthly = new System.Windows.Forms.TabPage();
            this.splitQueueMonthly = new System.Windows.Forms.SplitContainer();
            this.chartQueueMonthly = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvQueueMonthly = new System.Windows.Forms.DataGridView();
            this.QMonthNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QMonthName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTotalQueuedM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QCalledCountM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QFinishedCountM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WaitingCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SkippedCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CancelledCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPatientStats = new System.Windows.Forms.TabPage();
            this.splitPatientStats = new System.Windows.Forms.SplitContainer();
            this.dgvPatientStats = new System.Windows.Forms.DataGridView();
            this.AgeGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountPatients = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartPatientStats = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabMostBoughtItems = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chartMostBought = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvMostBoughtItems = new System.Windows.Forms.DataGridView();
            this.item_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.generic_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.brand_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.strength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_quantity_sold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_revenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabBilling = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.chartBilling = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvBilling = new System.Windows.Forms.DataGridView();
            this.DayDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DayLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EarCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoseCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThroatCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxillofacialCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeadNeckCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OthersCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalConsultss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonthNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonthName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EarCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoseCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThroatCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxillofacialCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeadNeckCounts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OthersCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalConsults = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabConsultation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEntOverview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntOverview)).BeginInit();
            this.tabDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDaily)).BeginInit();
            this.splitDaily.Panel1.SuspendLayout();
            this.splitDaily.Panel2.SuspendLayout();
            this.splitDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaily)).BeginInit();
            this.tabMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMonthly)).BeginInit();
            this.splitMonthly.Panel1.SuspendLayout();
            this.splitMonthly.Panel2.SuspendLayout();
            this.splitMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartEnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnt)).BeginInit();
            this.tabQueueDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitQueueDaily)).BeginInit();
            this.splitQueueDaily.Panel1.SuspendLayout();
            this.splitQueueDaily.Panel2.SuspendLayout();
            this.splitQueueDaily.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartQueueDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueDaily)).BeginInit();
            this.tabQueueMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitQueueMonthly)).BeginInit();
            this.splitQueueMonthly.Panel1.SuspendLayout();
            this.splitQueueMonthly.Panel2.SuspendLayout();
            this.splitQueueMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartQueueMonthly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueMonthly)).BeginInit();
            this.tabPatientStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPatientStats)).BeginInit();
            this.splitPatientStats.Panel1.SuspendLayout();
            this.splitPatientStats.Panel2.SuspendLayout();
            this.splitPatientStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPatientStats)).BeginInit();
            this.tabMostBoughtItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMostBought)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMostBoughtItems)).BeginInit();
            this.tabBilling.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBilling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBilling)).BeginInit();
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
            this.pnlTop.TabIndex = 0;
            this.pnlTop.Visible = false;
            // 
            // cboZoneFilter
            // 
            this.cboZoneFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboZoneFilter.FormattingEnabled = true;
            this.cboZoneFilter.Location = new System.Drawing.Point(8, 8);
            this.cboZoneFilter.Name = "cboZoneFilter";
            this.cboZoneFilter.Size = new System.Drawing.Size(160, 21);
            this.cboZoneFilter.TabIndex = 0;
            // 
            // cboServiceFilter
            // 
            this.cboServiceFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServiceFilter.FormattingEnabled = true;
            this.cboServiceFilter.Location = new System.Drawing.Point(176, 8);
            this.cboServiceFilter.Name = "cboServiceFilter";
            this.cboServiceFilter.Size = new System.Drawing.Size(160, 21);
            this.cboServiceFilter.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(344, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 26);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Location = new System.Drawing.Point(438, 8);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(100, 26);
            this.btnExportCsv.TabIndex = 3;
            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.BtnExportCsv_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabConsultation);
            this.tabControl.Controls.Add(this.tabDaily);
            this.tabControl.Controls.Add(this.tabMonthly);
            this.tabControl.Controls.Add(this.tabQueueDaily);
            this.tabControl.Controls.Add(this.tabQueueMonthly);
            this.tabControl.Controls.Add(this.tabPatientStats);
            this.tabControl.Controls.Add(this.tabMostBoughtItems);
            this.tabControl.Controls.Add(this.tabBilling);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 10);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(980, 670);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabConsultation
            // 
            this.tabConsultation.Controls.Add(this.splitContainer1);
            this.tabConsultation.Location = new System.Drawing.Point(4, 22);
            this.tabConsultation.Name = "tabConsultation";
            this.tabConsultation.Size = new System.Drawing.Size(972, 644);
            this.tabConsultation.TabIndex = 5;
            this.tabConsultation.Text = "ENT Consultation Overview";
            this.tabConsultation.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmbExam);
            this.splitContainer1.Panel1.Controls.Add(this.chartEntOverview);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvEntOverview);
            this.splitContainer1.Size = new System.Drawing.Size(972, 644);
            this.splitContainer1.SplitterDistance = 609;
            this.splitContainer1.TabIndex = 1;
            // 
            // cmbExam
            // 
            this.cmbExam.DisplayMember = "1";
            this.cmbExam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExam.FormattingEnabled = true;
            this.cmbExam.Items.AddRange(new object[] {
            "Ear Exam",
            "Nose Exam",
            "Throat Exam",
            "Maxillofacial Exam",
            "Head & Neck Exam",
            "Others Exam"});
            this.cmbExam.Location = new System.Drawing.Point(4, 3);
            this.cmbExam.Name = "cmbExam";
            this.cmbExam.Size = new System.Drawing.Size(121, 21);
            this.cmbExam.TabIndex = 1;
            this.cmbExam.ValueMember = "1";
            this.cmbExam.SelectedIndexChanged += new System.EventHandler(this.cmbExam_SelectedIndexChanged);
            // 
            // chartEntOverview
            // 
            this.chartEntOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEntOverview.Location = new System.Drawing.Point(0, 0);
            this.chartEntOverview.Name = "chartEntOverview";
            this.chartEntOverview.Size = new System.Drawing.Size(972, 609);
            this.chartEntOverview.TabIndex = 0;
            // 
            // dgvEntOverview
            // 
            this.dgvEntOverview.AllowUserToAddRows = false;
            this.dgvEntOverview.AllowUserToDeleteRows = false;
            this.dgvEntOverview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEntOverview.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvEntOverview.ColumnHeadersHeight = 30;
            this.dgvEntOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEntOverview.Location = new System.Drawing.Point(0, 0);
            this.dgvEntOverview.Name = "dgvEntOverview";
            this.dgvEntOverview.ReadOnly = true;
            this.dgvEntOverview.Size = new System.Drawing.Size(972, 31);
            this.dgvEntOverview.TabIndex = 0;
            this.dgvEntOverview.Visible = false;
            // 
            // tabDaily
            // 
            this.tabDaily.Controls.Add(this.splitDaily);
            this.tabDaily.Location = new System.Drawing.Point(4, 22);
            this.tabDaily.Name = "tabDaily";
            this.tabDaily.Size = new System.Drawing.Size(972, 644);
            this.tabDaily.TabIndex = 1;
            this.tabDaily.Text = "Daily Consultation Summary";
            this.tabDaily.UseVisualStyleBackColor = true;
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
            this.splitDaily.SplitterDistance = 400;
            this.splitDaily.TabIndex = 0;
            // 
            // chartDaily
            // 
            this.chartDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDaily.Location = new System.Drawing.Point(0, 0);
            this.chartDaily.Name = "chartDaily";
            this.chartDaily.Size = new System.Drawing.Size(972, 400);
            this.chartDaily.TabIndex = 0;
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
            this.MaxillofacialCount,
            this.HeadNeckCount,
            this.OthersCounts,
            this.TotalConsultss});
            this.dgvDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDaily.Location = new System.Drawing.Point(0, 0);
            this.dgvDaily.Name = "dgvDaily";
            this.dgvDaily.ReadOnly = true;
            this.dgvDaily.Size = new System.Drawing.Size(972, 240);
            this.dgvDaily.TabIndex = 0;
            // 
            // tabMonthly
            // 
            this.tabMonthly.Controls.Add(this.splitMonthly);
            this.tabMonthly.Location = new System.Drawing.Point(4, 22);
            this.tabMonthly.Name = "tabMonthly";
            this.tabMonthly.Padding = new System.Windows.Forms.Padding(3);
            this.tabMonthly.Size = new System.Drawing.Size(972, 644);
            this.tabMonthly.TabIndex = 0;
            this.tabMonthly.Text = "Monthly Consultation  Summary";
            this.tabMonthly.UseVisualStyleBackColor = true;
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
            this.splitMonthly.SplitterDistance = 401;
            this.splitMonthly.TabIndex = 0;
            // 
            // chartEnt
            // 
            this.chartEnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartEnt.Location = new System.Drawing.Point(0, 0);
            this.chartEnt.Name = "chartEnt";
            this.chartEnt.Size = new System.Drawing.Size(966, 401);
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
            this.MaxillofacialCounts,
            this.HeadNeckCounts,
            this.OthersCount,
            this.TotalConsults});
            this.dgvEnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEnt.Location = new System.Drawing.Point(0, 0);
            this.dgvEnt.Name = "dgvEnt";
            this.dgvEnt.ReadOnly = true;
            this.dgvEnt.Size = new System.Drawing.Size(966, 233);
            this.dgvEnt.TabIndex = 0;
            // 
            // tabQueueDaily
            // 
            this.tabQueueDaily.Controls.Add(this.splitQueueDaily);
            this.tabQueueDaily.Location = new System.Drawing.Point(4, 22);
            this.tabQueueDaily.Name = "tabQueueDaily";
            this.tabQueueDaily.Size = new System.Drawing.Size(972, 644);
            this.tabQueueDaily.TabIndex = 2;
            this.tabQueueDaily.Text = "Queue Daily Summary";
            this.tabQueueDaily.UseVisualStyleBackColor = true;
            // 
            // splitQueueDaily
            // 
            this.splitQueueDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitQueueDaily.Location = new System.Drawing.Point(0, 0);
            this.splitQueueDaily.Name = "splitQueueDaily";
            this.splitQueueDaily.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitQueueDaily.Panel1
            // 
            this.splitQueueDaily.Panel1.Controls.Add(this.chartQueueDaily);
            // 
            // splitQueueDaily.Panel2
            // 
            this.splitQueueDaily.Panel2.Controls.Add(this.dgvQueueDaily);
            this.splitQueueDaily.Size = new System.Drawing.Size(972, 644);
            this.splitQueueDaily.SplitterDistance = 400;
            this.splitQueueDaily.TabIndex = 0;
            // 
            // chartQueueDaily
            // 
            this.chartQueueDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartQueueDaily.Location = new System.Drawing.Point(0, 0);
            this.chartQueueDaily.Name = "chartQueueDaily";
            this.chartQueueDaily.Size = new System.Drawing.Size(972, 400);
            this.chartQueueDaily.TabIndex = 0;
            // 
            // dgvQueueDaily
            // 
            this.dgvQueueDaily.AllowUserToAddRows = false;
            this.dgvQueueDaily.AllowUserToDeleteRows = false;
            this.dgvQueueDaily.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueueDaily.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvQueueDaily.ColumnHeadersHeight = 30;
            this.dgvQueueDaily.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QDayDate,
            this.QDayLabel,
            this.QTotalQueued,
            this.QCalledCount,
            this.QFinishedCount,
            this.WaitingCount,
            this.SkippedCount,
            this.CancelledCount});
            this.dgvQueueDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueueDaily.Location = new System.Drawing.Point(0, 0);
            this.dgvQueueDaily.Name = "dgvQueueDaily";
            this.dgvQueueDaily.ReadOnly = true;
            this.dgvQueueDaily.Size = new System.Drawing.Size(972, 240);
            this.dgvQueueDaily.TabIndex = 0;
            // 
            // QDayDate
            // 
            this.QDayDate.DataPropertyName = "DayDate";
            this.QDayDate.HeaderText = "DayDate";
            this.QDayDate.Name = "QDayDate";
            this.QDayDate.ReadOnly = true;
            this.QDayDate.Visible = false;
            // 
            // QDayLabel
            // 
            this.QDayLabel.DataPropertyName = "DayLabel";
            this.QDayLabel.HeaderText = "Date";
            this.QDayLabel.Name = "QDayLabel";
            this.QDayLabel.ReadOnly = true;
            // 
            // QTotalQueued
            // 
            this.QTotalQueued.DataPropertyName = "TotalQueued";
            this.QTotalQueued.HeaderText = "Total Queued";
            this.QTotalQueued.Name = "QTotalQueued";
            this.QTotalQueued.ReadOnly = true;
            // 
            // QCalledCount
            // 
            this.QCalledCount.DataPropertyName = "CalledCount";
            this.QCalledCount.HeaderText = "Called";
            this.QCalledCount.Name = "QCalledCount";
            this.QCalledCount.ReadOnly = true;
            // 
            // QFinishedCount
            // 
            this.QFinishedCount.DataPropertyName = "FinishedCount";
            this.QFinishedCount.HeaderText = "Finished";
            this.QFinishedCount.Name = "QFinishedCount";
            this.QFinishedCount.ReadOnly = true;
            // 
            // WaitingCount
            // 
            this.WaitingCount.DataPropertyName = "WaitingCount";
            this.WaitingCount.HeaderText = "Waiting";
            this.WaitingCount.Name = "WaitingCount";
            this.WaitingCount.ReadOnly = true;
            // 
            // SkippedCount
            // 
            this.SkippedCount.DataPropertyName = "SkippedCount";
            this.SkippedCount.HeaderText = "Skipped";
            this.SkippedCount.Name = "SkippedCount";
            this.SkippedCount.ReadOnly = true;
            // 
            // CancelledCount
            // 
            this.CancelledCount.DataPropertyName = "CancelledCount";
            this.CancelledCount.HeaderText = "Cancelled";
            this.CancelledCount.Name = "CancelledCount";
            this.CancelledCount.ReadOnly = true;
            // 
            // tabQueueMonthly
            // 
            this.tabQueueMonthly.Controls.Add(this.splitQueueMonthly);
            this.tabQueueMonthly.Location = new System.Drawing.Point(4, 22);
            this.tabQueueMonthly.Name = "tabQueueMonthly";
            this.tabQueueMonthly.Size = new System.Drawing.Size(972, 644);
            this.tabQueueMonthly.TabIndex = 3;
            this.tabQueueMonthly.Text = "Queue Monthly Summary";
            this.tabQueueMonthly.UseVisualStyleBackColor = true;
            // 
            // splitQueueMonthly
            // 
            this.splitQueueMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitQueueMonthly.Location = new System.Drawing.Point(0, 0);
            this.splitQueueMonthly.Name = "splitQueueMonthly";
            this.splitQueueMonthly.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitQueueMonthly.Panel1
            // 
            this.splitQueueMonthly.Panel1.Controls.Add(this.chartQueueMonthly);
            // 
            // splitQueueMonthly.Panel2
            // 
            this.splitQueueMonthly.Panel2.Controls.Add(this.dgvQueueMonthly);
            this.splitQueueMonthly.Size = new System.Drawing.Size(972, 644);
            this.splitQueueMonthly.SplitterDistance = 400;
            this.splitQueueMonthly.TabIndex = 0;
            // 
            // chartQueueMonthly
            // 
            this.chartQueueMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartQueueMonthly.Location = new System.Drawing.Point(0, 0);
            this.chartQueueMonthly.Name = "chartQueueMonthly";
            this.chartQueueMonthly.Size = new System.Drawing.Size(972, 400);
            this.chartQueueMonthly.TabIndex = 0;
            this.chartQueueMonthly.Click += new System.EventHandler(this.chartQueueMonthly_Click);
            // 
            // dgvQueueMonthly
            // 
            this.dgvQueueMonthly.AllowUserToAddRows = false;
            this.dgvQueueMonthly.AllowUserToDeleteRows = false;
            this.dgvQueueMonthly.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueueMonthly.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvQueueMonthly.ColumnHeadersHeight = 30;
            this.dgvQueueMonthly.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QMonthNumber,
            this.QMonthName,
            this.QTotalQueuedM,
            this.QCalledCountM,
            this.QFinishedCountM,
            this.WaitingCounts,
            this.SkippedCounts,
            this.CancelledCounts});
            this.dgvQueueMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQueueMonthly.Location = new System.Drawing.Point(0, 0);
            this.dgvQueueMonthly.Name = "dgvQueueMonthly";
            this.dgvQueueMonthly.ReadOnly = true;
            this.dgvQueueMonthly.Size = new System.Drawing.Size(972, 240);
            this.dgvQueueMonthly.TabIndex = 0;
            // 
            // QMonthNumber
            // 
            this.QMonthNumber.DataPropertyName = "MonthNumber";
            this.QMonthNumber.HeaderText = "MonthNumber";
            this.QMonthNumber.Name = "QMonthNumber";
            this.QMonthNumber.ReadOnly = true;
            this.QMonthNumber.Visible = false;
            // 
            // QMonthName
            // 
            this.QMonthName.DataPropertyName = "MonthName";
            this.QMonthName.HeaderText = "Month";
            this.QMonthName.Name = "QMonthName";
            this.QMonthName.ReadOnly = true;
            // 
            // QTotalQueuedM
            // 
            this.QTotalQueuedM.DataPropertyName = "TotalQueued";
            this.QTotalQueuedM.HeaderText = "Total Queued";
            this.QTotalQueuedM.Name = "QTotalQueuedM";
            this.QTotalQueuedM.ReadOnly = true;
            // 
            // QCalledCountM
            // 
            this.QCalledCountM.DataPropertyName = "CalledCount";
            this.QCalledCountM.HeaderText = "Called";
            this.QCalledCountM.Name = "QCalledCountM";
            this.QCalledCountM.ReadOnly = true;
            // 
            // QFinishedCountM
            // 
            this.QFinishedCountM.DataPropertyName = "FinishedCount";
            this.QFinishedCountM.HeaderText = "Finished";
            this.QFinishedCountM.Name = "QFinishedCountM";
            this.QFinishedCountM.ReadOnly = true;
            // 
            // WaitingCounts
            // 
            this.WaitingCounts.DataPropertyName = "WaitingCount";
            this.WaitingCounts.HeaderText = "Waiting";
            this.WaitingCounts.Name = "WaitingCounts";
            this.WaitingCounts.ReadOnly = true;
            // 
            // SkippedCounts
            // 
            this.SkippedCounts.DataPropertyName = "SkippedCount";
            this.SkippedCounts.HeaderText = "Skipped";
            this.SkippedCounts.Name = "SkippedCounts";
            this.SkippedCounts.ReadOnly = true;
            // 
            // CancelledCounts
            // 
            this.CancelledCounts.DataPropertyName = "CancelledCount";
            this.CancelledCounts.HeaderText = "Cancelled";
            this.CancelledCounts.Name = "CancelledCounts";
            this.CancelledCounts.ReadOnly = true;
            // 
            // tabPatientStats
            // 
            this.tabPatientStats.Controls.Add(this.splitPatientStats);
            this.tabPatientStats.Location = new System.Drawing.Point(4, 22);
            this.tabPatientStats.Name = "tabPatientStats";
            this.tabPatientStats.Size = new System.Drawing.Size(972, 644);
            this.tabPatientStats.TabIndex = 4;
            this.tabPatientStats.Text = "Age Group";
            this.tabPatientStats.UseVisualStyleBackColor = true;
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
            this.splitPatientStats.SplitterDistance = 220;
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
            this.dgvPatientStats.Size = new System.Drawing.Size(220, 644);
            this.dgvPatientStats.TabIndex = 0;
            // 
            // AgeGroup
            // 
            this.AgeGroup.DataPropertyName = "AgeGroup";
            this.AgeGroup.HeaderText = "Age";
            this.AgeGroup.Name = "AgeGroup";
            this.AgeGroup.ReadOnly = true;
            // 
            // CountPatients
            // 
            this.CountPatients.DataPropertyName = "CountPatients";
            this.CountPatients.HeaderText = "Number of Patients";
            this.CountPatients.Name = "CountPatients";
            this.CountPatients.ReadOnly = true;
            // 
            // chartPatientStats
            // 
            this.chartPatientStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPatientStats.Location = new System.Drawing.Point(0, 0);
            this.chartPatientStats.Name = "chartPatientStats";
            this.chartPatientStats.Size = new System.Drawing.Size(748, 644);
            this.chartPatientStats.TabIndex = 0;
            // 
            // tabMostBoughtItems
            // 
            this.tabMostBoughtItems.Controls.Add(this.splitContainer2);
            this.tabMostBoughtItems.Location = new System.Drawing.Point(4, 22);
            this.tabMostBoughtItems.Name = "tabMostBoughtItems";
            this.tabMostBoughtItems.Size = new System.Drawing.Size(972, 644);
            this.tabMostBoughtItems.TabIndex = 6;
            this.tabMostBoughtItems.Text = "Dispensing Summary";
            this.tabMostBoughtItems.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chartMostBought);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvMostBoughtItems);
            this.splitContainer2.Size = new System.Drawing.Size(972, 644);
            this.splitContainer2.SplitterDistance = 400;
            this.splitContainer2.TabIndex = 1;
            // 
            // chartMostBought
            // 
            this.chartMostBought.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMostBought.Location = new System.Drawing.Point(0, 0);
            this.chartMostBought.Name = "chartMostBought";
            this.chartMostBought.Size = new System.Drawing.Size(972, 400);
            this.chartMostBought.TabIndex = 0;
            // 
            // dgvMostBoughtItems
            // 
            this.dgvMostBoughtItems.AllowUserToAddRows = false;
            this.dgvMostBoughtItems.AllowUserToDeleteRows = false;
            this.dgvMostBoughtItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMostBoughtItems.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMostBoughtItems.ColumnHeadersHeight = 30;
            this.dgvMostBoughtItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item_id,
            this.generic_name,
            this.brand_name,
            this.strength,
            this.dosage,
            this.category,
            this.total_quantity_sold,
            this.total_revenue});
            this.dgvMostBoughtItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMostBoughtItems.Location = new System.Drawing.Point(0, 0);
            this.dgvMostBoughtItems.Name = "dgvMostBoughtItems";
            this.dgvMostBoughtItems.ReadOnly = true;
            this.dgvMostBoughtItems.Size = new System.Drawing.Size(972, 240);
            this.dgvMostBoughtItems.TabIndex = 0;
            // 
            // item_id
            // 
            this.item_id.DataPropertyName = "item_id";
            this.item_id.HeaderText = "Column1";
            this.item_id.Name = "item_id";
            this.item_id.ReadOnly = true;
            this.item_id.Visible = false;
            // 
            // generic_name
            // 
            this.generic_name.DataPropertyName = "generic_name";
            this.generic_name.HeaderText = "Generic Name";
            this.generic_name.Name = "generic_name";
            this.generic_name.ReadOnly = true;
            // 
            // brand_name
            // 
            this.brand_name.DataPropertyName = "brand_name";
            this.brand_name.HeaderText = "Brand Name";
            this.brand_name.Name = "brand_name";
            this.brand_name.ReadOnly = true;
            // 
            // strength
            // 
            this.strength.DataPropertyName = "strength";
            this.strength.HeaderText = "Strength";
            this.strength.Name = "strength";
            this.strength.ReadOnly = true;
            // 
            // dosage
            // 
            this.dosage.DataPropertyName = "dosage";
            this.dosage.HeaderText = "Dosage";
            this.dosage.Name = "dosage";
            this.dosage.ReadOnly = true;
            // 
            // category
            // 
            this.category.DataPropertyName = "category";
            this.category.HeaderText = "Category";
            this.category.Name = "category";
            this.category.ReadOnly = true;
            // 
            // total_quantity_sold
            // 
            this.total_quantity_sold.DataPropertyName = "total_quantity_sold";
            this.total_quantity_sold.HeaderText = "Total Quantity Sold";
            this.total_quantity_sold.Name = "total_quantity_sold";
            this.total_quantity_sold.ReadOnly = true;
            // 
            // total_revenue
            // 
            this.total_revenue.DataPropertyName = "total_revenue";
            this.total_revenue.HeaderText = "Total Revenue";
            this.total_revenue.Name = "total_revenue";
            this.total_revenue.ReadOnly = true;
            // 
            // tabBilling
            // 
            this.tabBilling.Controls.Add(this.splitContainer3);
            this.tabBilling.Location = new System.Drawing.Point(4, 22);
            this.tabBilling.Name = "tabBilling";
            this.tabBilling.Size = new System.Drawing.Size(972, 644);
            this.tabBilling.TabIndex = 7;
            this.tabBilling.Text = "Billing Summary";
            this.tabBilling.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.chartBilling);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.dgvBilling);
            this.splitContainer3.Size = new System.Drawing.Size(972, 644);
            this.splitContainer3.SplitterDistance = 400;
            this.splitContainer3.TabIndex = 2;
            // 
            // chartBilling
            // 
            this.chartBilling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBilling.Location = new System.Drawing.Point(0, 0);
            this.chartBilling.Name = "chartBilling";
            this.chartBilling.Size = new System.Drawing.Size(972, 400);
            this.chartBilling.TabIndex = 0;
            // 
            // dgvBilling
            // 
            this.dgvBilling.AllowUserToAddRows = false;
            this.dgvBilling.AllowUserToDeleteRows = false;
            this.dgvBilling.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBilling.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvBilling.ColumnHeadersHeight = 30;
            this.dgvBilling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBilling.Location = new System.Drawing.Point(0, 0);
            this.dgvBilling.Name = "dgvBilling";
            this.dgvBilling.ReadOnly = true;
            this.dgvBilling.Size = new System.Drawing.Size(972, 240);
            this.dgvBilling.TabIndex = 0;
            // 
            // DayDate
            // 
            this.DayDate.DataPropertyName = "DayDate";
            this.DayDate.HeaderText = "DayDate";
            this.DayDate.Name = "DayDate";
            this.DayDate.ReadOnly = true;
            this.DayDate.Visible = false;
            // 
            // DayLabel
            // 
            this.DayLabel.DataPropertyName = "DayLabel";
            this.DayLabel.HeaderText = "Date";
            this.DayLabel.Name = "DayLabel";
            this.DayLabel.ReadOnly = true;
            // 
            // EarCounts
            // 
            this.EarCounts.DataPropertyName = "EarCount";
            this.EarCounts.HeaderText = "Ears";
            this.EarCounts.Name = "EarCounts";
            this.EarCounts.ReadOnly = true;
            // 
            // NoseCounts
            // 
            this.NoseCounts.DataPropertyName = "NoseCount";
            this.NoseCounts.HeaderText = "Nose";
            this.NoseCounts.Name = "NoseCounts";
            this.NoseCounts.ReadOnly = true;
            // 
            // ThroatCounts
            // 
            this.ThroatCounts.DataPropertyName = "ThroatCount";
            this.ThroatCounts.HeaderText = "Throat";
            this.ThroatCounts.Name = "ThroatCounts";
            this.ThroatCounts.ReadOnly = true;
            // 
            // MaxillofacialCount
            // 
            this.MaxillofacialCount.DataPropertyName = "MaxillofacialCount";
            this.MaxillofacialCount.HeaderText = "Maxillofacial";
            this.MaxillofacialCount.Name = "MaxillofacialCount";
            this.MaxillofacialCount.ReadOnly = true;
            // 
            // HeadNeckCount
            // 
            this.HeadNeckCount.DataPropertyName = "HeadNeckCount";
            this.HeadNeckCount.HeaderText = "Head & Neck";
            this.HeadNeckCount.Name = "HeadNeckCount";
            this.HeadNeckCount.ReadOnly = true;
            // 
            // OthersCounts
            // 
            this.OthersCounts.DataPropertyName = "OthersCount";
            this.OthersCounts.HeaderText = "Others";
            this.OthersCounts.Name = "OthersCounts";
            this.OthersCounts.ReadOnly = true;
            // 
            // TotalConsultss
            // 
            this.TotalConsultss.DataPropertyName = "TotalConsults";
            this.TotalConsultss.HeaderText = "Number of Consultations";
            this.TotalConsultss.Name = "TotalConsultss";
            this.TotalConsultss.ReadOnly = true;
            // 
            // MonthNumber
            // 
            this.MonthNumber.DataPropertyName = "MonthNumber";
            this.MonthNumber.HeaderText = "MonthNumber";
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
            // MaxillofacialCounts
            // 
            this.MaxillofacialCounts.DataPropertyName = "MaxillofacialCount";
            this.MaxillofacialCounts.HeaderText = "Maxillofacial";
            this.MaxillofacialCounts.Name = "MaxillofacialCounts";
            this.MaxillofacialCounts.ReadOnly = true;
            // 
            // HeadNeckCounts
            // 
            this.HeadNeckCounts.DataPropertyName = "HeadNeckCount";
            this.HeadNeckCounts.HeaderText = "Head & Neck";
            this.HeadNeckCounts.Name = "HeadNeckCounts";
            this.HeadNeckCounts.ReadOnly = true;
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
            // Dashboard
            // 
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlTop);
            this.Name = "Dashboard";
            this.Size = new System.Drawing.Size(980, 680);
            this.pnlTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabConsultation.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartEntOverview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEntOverview)).EndInit();
            this.tabDaily.ResumeLayout(false);
            this.splitDaily.Panel1.ResumeLayout(false);
            this.splitDaily.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDaily)).EndInit();
            this.splitDaily.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaily)).EndInit();
            this.tabMonthly.ResumeLayout(false);
            this.splitMonthly.Panel1.ResumeLayout(false);
            this.splitMonthly.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMonthly)).EndInit();
            this.splitMonthly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartEnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnt)).EndInit();
            this.tabQueueDaily.ResumeLayout(false);
            this.splitQueueDaily.Panel1.ResumeLayout(false);
            this.splitQueueDaily.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitQueueDaily)).EndInit();
            this.splitQueueDaily.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartQueueDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueDaily)).EndInit();
            this.tabQueueMonthly.ResumeLayout(false);
            this.splitQueueMonthly.Panel1.ResumeLayout(false);
            this.splitQueueMonthly.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitQueueMonthly)).EndInit();
            this.splitQueueMonthly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartQueueMonthly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueueMonthly)).EndInit();
            this.tabPatientStats.ResumeLayout(false);
            this.splitPatientStats.Panel1.ResumeLayout(false);
            this.splitPatientStats.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPatientStats)).EndInit();
            this.splitPatientStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatientStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPatientStats)).EndInit();
            this.tabMostBoughtItems.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMostBought)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMostBoughtItems)).EndInit();
            this.tabBilling.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartBilling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBilling)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabConsultation;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEntOverview;
        private System.Windows.Forms.DataGridView dgvEntOverview;
        private System.Windows.Forms.ComboBox cmbExam;
        private System.Windows.Forms.DataGridViewTextBoxColumn QDayDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn QDayLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTotalQueued;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCalledCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn QFinishedCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn WaitingCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SkippedCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CancelledCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn QMonthNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn QMonthName;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTotalQueuedM;
        private System.Windows.Forms.DataGridViewTextBoxColumn QCalledCountM;
        private System.Windows.Forms.DataGridViewTextBoxColumn QFinishedCountM;
        private System.Windows.Forms.DataGridViewTextBoxColumn WaitingCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn SkippedCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn CancelledCounts;
        private System.Windows.Forms.TabPage tabMostBoughtItems;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMostBought;
        private System.Windows.Forms.DataGridView dgvMostBoughtItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn generic_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn brand_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn strength;
        private System.Windows.Forms.DataGridViewTextBoxColumn dosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn category;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_quantity_sold;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_revenue;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgeGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountPatients;
        private System.Windows.Forms.TabPage tabBilling;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBilling;
        private System.Windows.Forms.DataGridView dgvBilling;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DayLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn EarCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoseCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThroatCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxillofacialCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeadNeckCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OthersCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalConsultss;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonthNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn MonthName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EarCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoseCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThroatCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxillofacialCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeadNeckCounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn OthersCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalConsults;
    }
}
