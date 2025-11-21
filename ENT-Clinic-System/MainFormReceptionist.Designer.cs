namespace ENT_Clinic_System
{
    partial class MainFormReceptionist
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormReceptionist));
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientQueueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewPatientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dispensingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nearExpirationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockOnHandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowStockReorderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientVisitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(97, 29);
            this.settingsToolStripMenuItem.Text = "Inventory";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // patientQueueToolStripMenuItem
            // 
            this.patientQueueToolStripMenuItem.Name = "patientQueueToolStripMenuItem";
            this.patientQueueToolStripMenuItem.Size = new System.Drawing.Size(197, 30);
            this.patientQueueToolStripMenuItem.Text = "Patient Queue";
            this.patientQueueToolStripMenuItem.Click += new System.EventHandler(this.patientQueueToolStripMenuItem_Click);
            // 
            // consultationsToolStripMenuItem
            // 
            this.consultationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduleToolStripMenuItem,
            this.patientQueueToolStripMenuItem});
            this.consultationsToolStripMenuItem.Name = "consultationsToolStripMenuItem";
            this.consultationsToolStripMenuItem.Size = new System.Drawing.Size(123, 29);
            this.consultationsToolStripMenuItem.Text = "Appointment";
            this.consultationsToolStripMenuItem.Click += new System.EventHandler(this.consultationsToolStripMenuItem_Click);
            // 
            // scheduleToolStripMenuItem
            // 
            this.scheduleToolStripMenuItem.Name = "scheduleToolStripMenuItem";
            this.scheduleToolStripMenuItem.Size = new System.Drawing.Size(197, 30);
            this.scheduleToolStripMenuItem.Text = "Schedule";
            this.scheduleToolStripMenuItem.Click += new System.EventHandler(this.scheduleToolStripMenuItem_Click);
            // 
            // patientListToolStripMenuItem
            // 
            this.patientListToolStripMenuItem.Name = "patientListToolStripMenuItem";
            this.patientListToolStripMenuItem.Size = new System.Drawing.Size(215, 30);
            this.patientListToolStripMenuItem.Text = "Patient List";
            this.patientListToolStripMenuItem.Click += new System.EventHandler(this.patientListToolStripMenuItem_Click);
            // 
            // addNewPatientToolStripMenuItem
            // 
            this.addNewPatientToolStripMenuItem.Name = "addNewPatientToolStripMenuItem";
            this.addNewPatientToolStripMenuItem.Size = new System.Drawing.Size(215, 30);
            this.addNewPatientToolStripMenuItem.Text = "Add New Patient";
            this.addNewPatientToolStripMenuItem.Click += new System.EventHandler(this.addNewPatientToolStripMenuItem_Click);
            // 
            // patientsToolStripMenuItem
            // 
            this.patientsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewPatientToolStripMenuItem,
            this.patientListToolStripMenuItem});
            this.patientsToolStripMenuItem.Name = "patientsToolStripMenuItem";
            this.patientsToolStripMenuItem.Size = new System.Drawing.Size(79, 29);
            this.patientsToolStripMenuItem.Text = "Patient";
            this.patientsToolStripMenuItem.Click += new System.EventHandler(this.patientsToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.patientsToolStripMenuItem,
            this.consultationsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.paymentToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.accountToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(12, 5, 0, 5);
            this.menuStrip1.Size = new System.Drawing.Size(1484, 39);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(28, 29);
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // paymentToolStripMenuItem
            // 
            this.paymentToolStripMenuItem.Name = "paymentToolStripMenuItem";
            this.paymentToolStripMenuItem.Size = new System.Drawing.Size(93, 29);
            this.paymentToolStripMenuItem.Text = "Payment";
            this.paymentToolStripMenuItem.Click += new System.EventHandler(this.paymentToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.billingToolStripMenuItem,
            this.dispensingToolStripMenuItem,
            this.nearExpirationToolStripMenuItem,
            this.stockOnHandToolStripMenuItem,
            this.writeOffToolStripMenuItem,
            this.lowStockReorderToolStripMenuItem,
            this.patientVisitToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(77, 29);
            this.reportsToolStripMenuItem.Text = "Report";
            this.reportsToolStripMenuItem.Click += new System.EventHandler(this.reportsToolStripMenuItem_Click);
            // 
            // billingToolStripMenuItem
            // 
            this.billingToolStripMenuItem.Name = "billingToolStripMenuItem";
            this.billingToolStripMenuItem.Size = new System.Drawing.Size(245, 30);
            this.billingToolStripMenuItem.Text = "Billing";
            this.billingToolStripMenuItem.Click += new System.EventHandler(this.billingToolStripMenuItem_Click_2);
            // 
            // dispensingToolStripMenuItem
            // 
            this.dispensingToolStripMenuItem.Name = "dispensingToolStripMenuItem";
            this.dispensingToolStripMenuItem.Size = new System.Drawing.Size(245, 30);
            this.dispensingToolStripMenuItem.Text = "Dispensing";
            this.dispensingToolStripMenuItem.Click += new System.EventHandler(this.dispensingToolStripMenuItem_Click);
            // 
            // nearExpirationToolStripMenuItem
            // 
            this.nearExpirationToolStripMenuItem.Name = "nearExpirationToolStripMenuItem";
            this.nearExpirationToolStripMenuItem.Size = new System.Drawing.Size(245, 30);
            this.nearExpirationToolStripMenuItem.Text = "Near Expiration";
            this.nearExpirationToolStripMenuItem.Click += new System.EventHandler(this.nearExpirationToolStripMenuItem_Click);
            // 
            // stockOnHandToolStripMenuItem
            // 
            this.stockOnHandToolStripMenuItem.Name = "stockOnHandToolStripMenuItem";
            this.stockOnHandToolStripMenuItem.Size = new System.Drawing.Size(245, 30);
            this.stockOnHandToolStripMenuItem.Text = "Stock On Hand";
            this.stockOnHandToolStripMenuItem.Click += new System.EventHandler(this.stockOnHandToolStripMenuItem_Click_1);
            // 
            // writeOffToolStripMenuItem
            // 
            this.writeOffToolStripMenuItem.Name = "writeOffToolStripMenuItem";
            this.writeOffToolStripMenuItem.Size = new System.Drawing.Size(245, 30);
            this.writeOffToolStripMenuItem.Text = "Write-Off";
            this.writeOffToolStripMenuItem.Click += new System.EventHandler(this.writeOffToolStripMenuItem_Click);
            // 
            // lowStockReorderToolStripMenuItem
            // 
            this.lowStockReorderToolStripMenuItem.Name = "lowStockReorderToolStripMenuItem";
            this.lowStockReorderToolStripMenuItem.Size = new System.Drawing.Size(245, 30);
            this.lowStockReorderToolStripMenuItem.Text = "Low Stock / Reorder";
            this.lowStockReorderToolStripMenuItem.Click += new System.EventHandler(this.lowStockReorderToolStripMenuItem_Click);
            // 
            // patientVisitToolStripMenuItem
            // 
            this.patientVisitToolStripMenuItem.Name = "patientVisitToolStripMenuItem";
            this.patientVisitToolStripMenuItem.Size = new System.Drawing.Size(245, 30);
            this.patientVisitToolStripMenuItem.Text = "Patient Visit";
            this.patientVisitToolStripMenuItem.Click += new System.EventHandler(this.patientVisitToolStripMenuItem_Click);
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem,
            this.profileToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.accountToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("accountToolStripMenuItem.Image")));
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(28, 29);
            this.accountToolStripMenuItem.Click += new System.EventHandler(this.accountToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(200, 30);
            this.updateToolStripMenuItem.Text = "Auto Complete";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // profileToolStripMenuItem
            // 
            this.profileToolStripMenuItem.Name = "profileToolStripMenuItem";
            this.profileToolStripMenuItem.Size = new System.Drawing.Size(200, 30);
            this.profileToolStripMenuItem.Text = "Profile";
            this.profileToolStripMenuItem.Click += new System.EventHandler(this.profileToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(200, 30);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.White;
            this.MainPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MainPanel.BackgroundImage")));
            this.MainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 39);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1484, 922);
            this.MainPanel.TabIndex = 6;
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            // 
            // MainFormReceptionist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 961);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainFormReceptionist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ENT Clinic System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormReceptionist_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientQueueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPatientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem paymentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem billingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dispensingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nearExpirationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockOnHandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeOffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowStockReorderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientVisitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
    }
}