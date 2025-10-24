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
            this.Text = "Enter Name of Requester";
            this.Width = 600;
            this.Height = 150;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Label
            Label lbl = new Label()
            {
                Text = "This certificate is issued upon the request of ",
                AutoSize = true
            };
            this.Controls.Add(lbl);

            // TextBox
            TextBox txtPurpose = new TextBox()
            {
                Name = "txtPurpose",
                Width = 300
            };
            this.Controls.Add(txtPurpose);

            // OK button
            Button btnOk = new Button()
            {
                Text = "OK",
                Width = 100,
                DialogResult = DialogResult.OK
            };
            this.Controls.Add(btnOk);

            // Cancel button
            Button btnCancel = new Button()
            {
                Text = "Cancel",
                Width = 100,
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;

            // === Center elements horizontally ===
            int formWidth = this.ClientSize.Width;

            // Label position
            lbl.Top = 20;
            lbl.Left = (formWidth - (lbl.PreferredWidth + txtPurpose.Width)) / 2;

            // TextBox position (next to label)
            txtPurpose.Top = lbl.Top - 2;
            txtPurpose.Left = lbl.Left + lbl.PreferredWidth + 5;

            // Buttons centered below
            int totalButtonWidth = btnOk.Width + btnCancel.Width + 20;
            int buttonsLeft = (formWidth - totalButtonWidth) / 2;

            btnOk.Top = 60;
            btnOk.Left = buttonsLeft;

            btnCancel.Top = 60;
            btnCancel.Left = btnOk.Right + 20;

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
