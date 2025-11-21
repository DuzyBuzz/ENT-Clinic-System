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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dispensingPanel = new System.Windows.Forms.Panel();
            this.billingPanel = new System.Windows.Forms.Panel();
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.55569F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.44431F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1814, 853);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dispensingPanel
            // 
            this.dispensingPanel.AutoScroll = true;
            this.dispensingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dispensingPanel.Location = new System.Drawing.Point(0, 363);
            this.dispensingPanel.Margin = new System.Windows.Forms.Padding(0);
            this.dispensingPanel.Name = "dispensingPanel";
            this.dispensingPanel.Size = new System.Drawing.Size(1814, 490);
            this.dispensingPanel.TabIndex = 1;
            this.dispensingPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.dispensingPanel_Paint);
            // 
            // billingPanel
            // 
            this.billingPanel.AutoScroll = true;
            this.billingPanel.BackColor = System.Drawing.Color.White;
            this.billingPanel.Location = new System.Drawing.Point(0, 0);
            this.billingPanel.Margin = new System.Windows.Forms.Padding(0);
            this.billingPanel.Name = "billingPanel";
            this.billingPanel.Size = new System.Drawing.Size(1691, 331);
            this.billingPanel.TabIndex = 0;
            // 
            // PaymentsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PaymentsControl";
            this.Size = new System.Drawing.Size(1814, 853);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel dispensingPanel;
        private System.Windows.Forms.Panel billingPanel;
    }
}
