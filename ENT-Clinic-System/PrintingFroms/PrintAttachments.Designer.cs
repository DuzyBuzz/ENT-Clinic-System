namespace ENT_Clinic_System.PrintingFroms
{
    partial class PrintAttachments
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel imagesPanel;
        private System.Windows.Forms.FlowLayoutPanel videosPanel;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Label labelImages;
        private System.Windows.Forms.Label labelVideos;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.imagesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.videosPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.printButton = new System.Windows.Forms.Button();
            this.labelImages = new System.Windows.Forms.Label();
            this.labelVideos = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imagesPanel
            // 
            this.imagesPanel.AutoScroll = true;
            this.imagesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagesPanel.Location = new System.Drawing.Point(3, 38);
            this.imagesPanel.Name = "imagesPanel";
            this.imagesPanel.Size = new System.Drawing.Size(384, 534);
            this.imagesPanel.TabIndex = 0;
            // 
            // videosPanel
            // 
            this.videosPanel.AutoScroll = true;
            this.videosPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.videosPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videosPanel.Location = new System.Drawing.Point(393, 38);
            this.videosPanel.Name = "videosPanel";
            this.videosPanel.Size = new System.Drawing.Size(384, 534);
            this.videosPanel.TabIndex = 1;
            // 
            // printButton
            // 
            this.printButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.printButton.Location = new System.Drawing.Point(3, 578);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(120, 39);
            this.printButton.TabIndex = 2;
            this.printButton.Text = "Print Images";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // labelImages
            // 
            this.labelImages.AutoSize = true;
            this.labelImages.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImages.Location = new System.Drawing.Point(3, 0);
            this.labelImages.Name = "labelImages";
            this.labelImages.Size = new System.Drawing.Size(91, 35);
            this.labelImages.TabIndex = 3;
            this.labelImages.Text = "Image Files";
            this.labelImages.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // labelVideos
            // 
            this.labelVideos.AutoSize = true;
            this.labelVideos.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelVideos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVideos.Location = new System.Drawing.Point(393, 0);
            this.labelVideos.Name = "labelVideos";
            this.labelVideos.Size = new System.Drawing.Size(87, 35);
            this.labelVideos.TabIndex = 4;
            this.labelVideos.Text = "Video Files";
            this.labelVideos.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.labelImages, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.printButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.videosPanel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.imagesPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelVideos, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.645161F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.09677F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.096774F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 620);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // PrintAttachments
            // 
            this.ClientSize = new System.Drawing.Size(780, 620);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PrintAttachments";
            this.Text = "Print Attachments";
            this.Load += new System.EventHandler(this.PrintAttachments_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
