using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.UI
{
    public partial class UserLoginForm : Form
    {
        public string EnteredUsername { get; private set; }
        public string EnteredPassword { get; private set; }

        public UserLoginForm(string role)
        {
            InitializeComponent();
            roleLabel.Text = $"Login as {role}";
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            EnteredUsername = usernameTextBox.Text;
            EnteredPassword = passwordTextBox.Text;

            if (string.IsNullOrEmpty(EnteredUsername) || string.IsNullOrEmpty(EnteredPassword))
            {
                MessageBox.Show("Please enter both username and password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK; // success
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
