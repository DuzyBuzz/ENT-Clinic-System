namespace ENT_Clinic_System.PrintingFroms
{
    partial class PrintConsultationHistory
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox consultationRichTextBox;
        private System.Windows.Forms.FlowLayoutPanel attachmentsPanel;
        private System.Windows.Forms.Button printTextButton;
        private System.Windows.Forms.Button printImageButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.consultationRichTextBox = new System.Windows.Forms.RichTextBox();
            this.attachmentsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.printTextButton = new System.Windows.Forms.Button();
            this.printImageButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // consultationRichTextBox
            // 
            this.consultationRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.consultationRichTextBox.Name = "consultationRichTextBox";
            this.consultationRichTextBox.Size = new System.Drawing.Size(600, 250);
            this.consultationRichTextBox.TabIndex = 0;
            this.consultationRichTextBox.Text = "";
            this.consultationRichTextBox.Visible = false;
            // 
            // attachmentsPanel
            // 
            this.attachmentsPanel.AutoScroll = true;
            this.attachmentsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.attachmentsPanel.Location = new System.Drawing.Point(12, 12);
            this.attachmentsPanel.Name = "attachmentsPanel";
            this.attachmentsPanel.Size = new System.Drawing.Size(776, 428);
            this.attachmentsPanel.TabIndex = 1;
            // 
            // printTextButton
            // 
            this.printTextButton.Location = new System.Drawing.Point(606, 446);
            this.printTextButton.Name = "printTextButton";
            this.printTextButton.Size = new System.Drawing.Size(182, 40);
            this.printTextButton.TabIndex = 2;
            this.printTextButton.Text = "Print Consultation History";
            this.printTextButton.UseVisualStyleBackColor = true;
            this.printTextButton.Click += new System.EventHandler(this.printTextButton_Click);
            // 
            // printImageButton
            // 
            this.printImageButton.Location = new System.Drawing.Point(450, 446);
            this.printImageButton.Name = "printImageButton";
            this.printImageButton.Size = new System.Drawing.Size(120, 40);
            this.printImageButton.TabIndex = 3;
            this.printImageButton.Text = "Print Images";
            this.printImageButton.UseVisualStyleBackColor = true;
            this.printImageButton.Click += new System.EventHandler(this.printImageButton_Click);
            // 
            // PrintConsultationHistory
            // 
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.printImageButton);
            this.Controls.Add(this.printTextButton);
            this.Controls.Add(this.attachmentsPanel);
            this.Controls.Add(this.consultationRichTextBox);
            this.Name = "PrintConsultationHistory";
            this.Text = "Print Consultation History";
            this.ResumeLayout(false);

        }
    }
}
