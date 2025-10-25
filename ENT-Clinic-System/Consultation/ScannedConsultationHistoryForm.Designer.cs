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
            this.scannedDocumentsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // scannedDocumentsFlowLayoutPanel
            // 
            this.scannedDocumentsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scannedDocumentsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.scannedDocumentsFlowLayoutPanel.Name = "scannedDocumentsFlowLayoutPanel";
            this.scannedDocumentsFlowLayoutPanel.Size = new System.Drawing.Size(1888, 971);
            this.scannedDocumentsFlowLayoutPanel.TabIndex = 0;
            // 
            // ScannedConsultationHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1888, 971);
            this.Controls.Add(this.scannedDocumentsFlowLayoutPanel);
            this.Name = "ScannedConsultationHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scanned Consultation HistoryForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel scannedDocumentsFlowLayoutPanel;
    }
}