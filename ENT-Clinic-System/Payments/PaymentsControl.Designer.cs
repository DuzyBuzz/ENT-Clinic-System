namespace ENT_Clinic_System.Payments
{
    partial class PaymentsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaymentsControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.billingPanel = new System.Windows.Forms.Panel();
            this.dispensingPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dispensingPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.billingPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1484, 961);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // billingPanel
            // 
            this.billingPanel.AutoScroll = true;
            this.billingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.billingPanel.Location = new System.Drawing.Point(3, 3);
            this.billingPanel.Name = "billingPanel";
            this.billingPanel.Size = new System.Drawing.Size(1478, 474);
            this.billingPanel.TabIndex = 0;
            // 
            // dispensingPanel
            // 
            this.dispensingPanel.AutoScroll = true;
            this.dispensingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dispensingPanel.Location = new System.Drawing.Point(3, 483);
            this.dispensingPanel.Name = "dispensingPanel";
            this.dispensingPanel.Size = new System.Drawing.Size(1478, 475);
            this.dispensingPanel.TabIndex = 1;
            this.dispensingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.dispensingPanel_Paint);
            // 
            // PaymentsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 961);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PaymentsControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payments Dispensing & Billing";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel dispensingPanel;
        private System.Windows.Forms.Panel billingPanel;
    }
}
