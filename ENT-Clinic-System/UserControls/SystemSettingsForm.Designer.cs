namespace ENT_Clinic_System.UserControls
{
    partial class SystemAdminForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.TabPage tabUsers;

        // Settings controls
        private System.Windows.Forms.DataGridView dgvSettings;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Button btnRefreshSettings;

        // Users controls
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Panel panelUserControls;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnUpdateUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnRefreshUsers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.dgvSettings = new System.Windows.Forms.DataGridView();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.btnRefreshSettings = new System.Windows.Forms.Button();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.panelUserControls = new System.Windows.Forms.Panel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnUpdateUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnRefreshUsers = new System.Windows.Forms.Button();

            this.tabControlMain.SuspendLayout();
            this.tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).BeginInit();
            this.tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.panelUserControls.SuspendLayout();
            this.SuspendLayout();

            // TabControl
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Controls.Add(this.tabSettings);
            this.tabControlMain.Controls.Add(this.tabUsers);

            // tabSettings
            this.tabSettings.Text = "System Settings";
            this.tabSettings.Controls.Add(this.dgvSettings);
            this.tabSettings.Controls.Add(this.btnSaveSettings);
            this.tabSettings.Controls.Add(this.btnRefreshSettings);

            // dgvSettings
            this.dgvSettings.Location = new System.Drawing.Point(12, 12);
            this.dgvSettings.Size = new System.Drawing.Size(740, 350);
            this.dgvSettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvSettings.AllowUserToAddRows = false;
            this.dgvSettings.AllowUserToDeleteRows = false;

            // btnSaveSettings
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.Size = new System.Drawing.Size(75, 30);
            this.btnSaveSettings.Location = new System.Drawing.Point(677, 370);
            this.btnSaveSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);

            // btnRefreshSettings
            this.btnRefreshSettings.Text = "Refresh";
            this.btnRefreshSettings.Size = new System.Drawing.Size(75, 30);
            this.btnRefreshSettings.Location = new System.Drawing.Point(12, 370);
            this.btnRefreshSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.btnRefreshSettings.Click += new System.EventHandler(this.btnRefreshSettings_Click);

            // tabUsers
            this.tabUsers.Text = "Users Management";
            this.tabUsers.Controls.Add(this.dgvUsers);
            this.tabUsers.Controls.Add(this.panelUserControls);

            // dgvUsers
            this.dgvUsers.Location = new System.Drawing.Point(12, 12);
            this.dgvUsers.Size = new System.Drawing.Size(500, 350);
            this.dgvUsers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.MultiSelect = false;

            // panelUserControls
            this.panelUserControls.Location = new System.Drawing.Point(520, 12);
            this.panelUserControls.Size = new System.Drawing.Size(240, 350);
            this.panelUserControls.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.panelUserControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // User input fields
            this.txtUsername.Location = new System.Drawing.Point(10, 10);
            this.txtUsername.Size = new System.Drawing.Size(200, 22);

            this.txtPassword.Location = new System.Drawing.Point(10, 40);
            this.txtPassword.Size = new System.Drawing.Size(200, 22);
            this.txtPassword.UseSystemPasswordChar = true;

            this.txtFullName.Location = new System.Drawing.Point(10, 70);
            this.txtFullName.Size = new System.Drawing.Size(200, 22);

            this.cmbRole.Location = new System.Drawing.Point(10, 100);
            this.cmbRole.Size = new System.Drawing.Size(200, 22);
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Items.AddRange(new object[] { "Admin", "Staff" });

            // Buttons
            this.btnAddUser.Location = new System.Drawing.Point(10, 140);
            this.btnAddUser.Size = new System.Drawing.Size(75, 30);
            this.btnAddUser.Text = "Add";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);

            this.btnUpdateUser.Location = new System.Drawing.Point(130, 140);
            this.btnUpdateUser.Size = new System.Drawing.Size(75, 30);
            this.btnUpdateUser.Text = "Update";
            this.btnUpdateUser.Click += new System.EventHandler(this.btnUpdateUser_Click);

            this.btnDeleteUser.Location = new System.Drawing.Point(10, 180);
            this.btnDeleteUser.Size = new System.Drawing.Size(75, 30);
            this.btnDeleteUser.Text = "Delete";
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);

            this.btnRefreshUsers.Location = new System.Drawing.Point(130, 180);
            this.btnRefreshUsers.Size = new System.Drawing.Size(75, 30);
            this.btnRefreshUsers.Text = "Refresh";
            this.btnRefreshUsers.Click += new System.EventHandler(this.btnRefreshUsers_Click);

            // Add controls to panel
            this.panelUserControls.Controls.Add(this.txtUsername);
            this.panelUserControls.Controls.Add(this.txtPassword);
            this.panelUserControls.Controls.Add(this.txtFullName);
            this.panelUserControls.Controls.Add(this.cmbRole);
            this.panelUserControls.Controls.Add(this.btnAddUser);
            this.panelUserControls.Controls.Add(this.btnUpdateUser);
            this.panelUserControls.Controls.Add(this.btnDeleteUser);
            this.panelUserControls.Controls.Add(this.btnRefreshUsers);

            // Form
            this.ClientSize = new System.Drawing.Size(784, 450);
            this.Controls.Add(this.tabControlMain);
            this.Text = "System Admin Panel";

            this.tabControlMain.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).EndInit();
            this.tabUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.panelUserControls.ResumeLayout(false);
            this.panelUserControls.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
