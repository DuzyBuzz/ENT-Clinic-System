namespace ENT_Clinic_System.Consultation
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.updateButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imagesPanel
            // 
            this.imagesPanel.AutoScroll = true;
            this.imagesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagesPanel.Location = new System.Drawing.Point(3, 51);
            this.imagesPanel.Name = "imagesPanel";
            this.imagesPanel.Size = new System.Drawing.Size(840, 749);
            this.imagesPanel.TabIndex = 0;
            // 
            // videosPanel
            // 
            this.videosPanel.AutoScroll = true;
            this.videosPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.videosPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videosPanel.Location = new System.Drawing.Point(849, 51);
            this.videosPanel.Name = "videosPanel";
            this.videosPanel.Size = new System.Drawing.Size(841, 749);
            this.videosPanel.TabIndex = 1;
            // 
            // printButton
            // 
            this.printButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.printButton.Location = new System.Drawing.Point(652, 3);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(186, 51);
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
            this.labelImages.Size = new System.Drawing.Size(91, 48);
            this.labelImages.TabIndex = 3;
            this.labelImages.Text = "Image Files";
            this.labelImages.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // labelVideos
            // 
            this.labelVideos.AutoSize = true;
            this.labelVideos.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelVideos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVideos.Location = new System.Drawing.Point(849, 0);
            this.labelVideos.Name = "labelVideos";
            this.labelVideos.Size = new System.Drawing.Size(87, 48);
            this.labelVideos.TabIndex = 4;
            this.labelVideos.Text = "Video Files";
            this.labelVideos.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelImages, 0, 0);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1693, 866);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.17004F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.82996F));
            this.tableLayoutPanel2.Controls.Add(this.printButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.updateButton, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(849, 806);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(841, 57);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // updateButton
            // 
            this.updateButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.updateButton.Location = new System.Drawing.Point(460, 3);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(186, 51);
            this.updateButton.TabIndex = 5;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // PrintAttachments
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PrintAttachments";
            this.Size = new System.Drawing.Size(1693, 866);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
