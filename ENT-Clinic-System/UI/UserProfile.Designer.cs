namespace ENT_Clinic_System.UI
{
    partial class UserProfile
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblRole;

        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtRole;   // <- changed from ComboBox to TextBox

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblUserId = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();

            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtRole = new System.Windows.Forms.TextBox(); // <- role textbox

            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // lblUserId
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(50, 30);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(54, 20);
            this.lblUserId.Text = "User ID";

            // txtUserId
            this.txtUserId.Location = new System.Drawing.Point(200, 27);
            this.txtUserId.ReadOnly = true;
            this.txtUserId.Size = new System.Drawing.Size(250, 27);

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(50, 70);
            this.lblUsername.Text = "Username";

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(200, 67);
            this.txtUsername.Size = new System.Drawing.Size(250, 27);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(50, 110);
            this.lblPassword.Text = "Password";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(200, 107);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(250, 27);

            // lblConfirmPassword
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Location = new System.Drawing.Point(50, 150);
            this.lblConfirmPassword.Text = "Confirm Password";

            // txtConfirmPassword
            this.txtConfirmPassword.Location = new System.Drawing.Point(200, 147);
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(250, 27);

            // lblFullName
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(50, 190);
            this.lblFullName.Text = "Full Name";

            // txtFullName
            this.txtFullName.Location = new System.Drawing.Point(200, 187);
            this.txtFullName.Size = new System.Drawing.Size(250, 27);

            // lblRole
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(50, 230);
            this.lblRole.Text = "Role";

            // txtRole
            this.txtRole.Location = new System.Drawing.Point(200, 227);
            this.txtRole.ReadOnly = true; // role is read-only
            this.txtRole.Size = new System.Drawing.Size(250, 27);

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(200, 280);
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(350, 280);
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // UserProfile
            this.ClientSize = new System.Drawing.Size(550, 350);
            this.Controls.Add(this.lblUserId);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.txtRole);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "UserProfile";
            this.Text = "User Profile";
            this.Load += new System.EventHandler(this.UserProfile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}
