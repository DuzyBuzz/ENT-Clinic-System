namespace ENT_Clinic_System.Consultation
{
    partial class ScannedConsultationHistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScannedConsultationHistoryForm));
            this.scannedDocumentsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // scannedDocumentsFlowLayoutPanel
            // 
            this.scannedDocumentsFlowLayoutPanel.AutoScroll = true;
            this.scannedDocumentsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scannedDocumentsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.scannedDocumentsFlowLayoutPanel.Name = "scannedDocumentsFlowLayoutPanel";
            this.scannedDocumentsFlowLayoutPanel.Size = new System.Drawing.Size(800, 1000);
            this.scannedDocumentsFlowLayoutPanel.TabIndex = 0;
            this.scannedDocumentsFlowLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.scannedDocumentsFlowLayoutPanel_Paint);
            // 
            // ScannedConsultationHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 1000);
            this.Controls.Add(this.scannedDocumentsFlowLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScannedConsultationHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scanned Consultation HistoryForm";
            this.Load += new System.EventHandler(this.ScannedConsultationHistoryForm_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel scannedDocumentsFlowLayoutPanel;
    }
}