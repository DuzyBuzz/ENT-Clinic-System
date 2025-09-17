namespace ENT_Clinic_System.Inventory
{
    partial class PrescriptionNoteForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PrescriptionNoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true; // allow scroll if many items
            this.ClientSize = new System.Drawing.Size(400, 500);
            this.Name = "PrescriptionNoteForm";
            this.Text = "Enter Notes for Prescription";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
