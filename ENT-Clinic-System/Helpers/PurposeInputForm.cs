using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public partial class PurposeInputForm : Form
    {
        public string PurposeText { get; private set; }

        public PurposeInputForm()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Enter Purpose";
            this.Width = 400;
            this.Height = 150;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Label
            Label lbl = new Label()
            {
                Text = "For the purpose of:",
                Left = 10,
                Top = 20,
                AutoSize = true
            };
            this.Controls.Add(lbl);

            // TextBox
            TextBox txtPurpose = new TextBox()
            {
                Name = "txtPurpose",
                Left = 150,
                Top = 18,
                Width = 200
            };
            this.Controls.Add(txtPurpose);

            // OK button
            Button btnOk = new Button()
            {
                Text = "OK",
                Left = 150,
                Top = 60,
                DialogResult = DialogResult.OK
            };
            this.Controls.Add(btnOk);

            // Cancel button
            Button btnCancel = new Button()
            {
                Text = "Cancel",
                Left = 230,
                Top = 60,
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;

            // Capture input when OK is pressed
            btnOk.Click += (s, e) =>
            {
                PurposeText = txtPurpose.Text.Trim();
                if (string.IsNullOrEmpty(PurposeText))
                {
                    MessageBox.Show("Please enter a purpose.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None; // stop closing
                }
            };
        }
    }
}
