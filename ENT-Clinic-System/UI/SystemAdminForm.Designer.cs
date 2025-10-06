namespace ENT_Clinic_System.UI
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
        private System.Windows.Forms.TextBox txtSearchSettings;
        private System.Windows.Forms.Label lblSearchSettings;

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
        private System.Windows.Forms.TextBox txtSearchUsers;
        private System.Windows.Forms.Label lblSearchUsers;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblRole;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.lblSearchUsers = new System.Windows.Forms.Label();
            this.txtSearchUsers = new System.Windows.Forms.TextBox();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.user_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.full_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelUserControls = new System.Windows.Forms.Panel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnUpdateUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnRefreshUsers = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.lblSearchSettings = new System.Windows.Forms.Label();
            this.txtSearchSettings = new System.Windows.Forms.TextBox();
            this.dgvSettings = new System.Windows.Forms.DataGridView();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.btnRefreshSettings = new System.Windows.Forms.Button();
            this.tabControlMain.SuspendLayout();
            this.tabUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.panelUserControls.SuspendLayout();
            this.tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabUsers);
            this.tabControlMain.Controls.Add(this.tabSettings);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(900, 550);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.lblSearchUsers);
            this.tabUsers.Controls.Add(this.txtSearchUsers);
            this.tabUsers.Controls.Add(this.dgvUsers);
            this.tabUsers.Controls.Add(this.panelUserControls);
            this.tabUsers.Location = new System.Drawing.Point(4, 22);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Size = new System.Drawing.Size(892, 524);
            this.tabUsers.TabIndex = 1;
            this.tabUsers.Text = "Users Management";
            this.tabUsers.UseVisualStyleBackColor = true;
            // 
            // lblSearchUsers
            // 
            this.lblSearchUsers.AutoSize = true;
            this.lblSearchUsers.Location = new System.Drawing.Point(17, 18);
            this.lblSearchUsers.Name = "lblSearchUsers";
            this.lblSearchUsers.Size = new System.Drawing.Size(44, 13);
            this.lblSearchUsers.TabIndex = 0;
            this.lblSearchUsers.Text = "Search:";
            // 
            // txtSearchUsers
            // 
            this.txtSearchUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchUsers.Location = new System.Drawing.Point(103, 15);
            this.txtSearchUsers.Name = "txtSearchUsers";
            this.txtSearchUsers.Size = new System.Drawing.Size(467, 20);
            this.txtSearchUsers.TabIndex = 1;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.user_id,
            this.username,
            this.password,
            this.full_name,
            this.role});
            this.dgvUsers.Location = new System.Drawing.Point(20, 45);
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(550, 450);
            this.dgvUsers.TabIndex = 2;
            // 
            // user_id
            // 
            this.user_id.DataPropertyName = "user_id";
            this.user_id.HeaderText = "User ID";
            this.user_id.Name = "user_id";
            this.user_id.ReadOnly = true;
            // 
            // username
            // 
            this.username.DataPropertyName = "username";
            this.username.HeaderText = "User Name";
            this.username.Name = "username";
            // 
            // password
            // 
            this.password.DataPropertyName = "password";
            this.password.HeaderText = "Password";
            this.password.Name = "password";
            // 
            // full_name
            // 
            this.full_name.DataPropertyName = "full_name";
            this.full_name.HeaderText = "Full Name";
            this.full_name.Name = "full_name";
            // 
            // role
            // 
            this.role.DataPropertyName = "role";
            this.role.HeaderText = "Role";
            this.role.Name = "role";
            // 
            // panelUserControls
            // 
            this.panelUserControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelUserControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUserControls.Controls.Add(this.lblUsername);
            this.panelUserControls.Controls.Add(this.lblPassword);
            this.panelUserControls.Controls.Add(this.lblFullName);
            this.panelUserControls.Controls.Add(this.lblRole);
            this.panelUserControls.Controls.Add(this.txtUsername);
            this.panelUserControls.Controls.Add(this.txtPassword);
            this.panelUserControls.Controls.Add(this.txtFullName);
            this.panelUserControls.Controls.Add(this.cmbRole);
            this.panelUserControls.Controls.Add(this.btnAddUser);
            this.panelUserControls.Controls.Add(this.btnUpdateUser);
            this.panelUserControls.Controls.Add(this.btnDeleteUser);
            this.panelUserControls.Controls.Add(this.btnRefreshUsers);
            this.panelUserControls.Location = new System.Drawing.Point(590, 15);
            this.panelUserControls.Name = "panelUserControls";
            this.panelUserControls.Size = new System.Drawing.Size(280, 480);
            this.panelUserControls.TabIndex = 3;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(17, 27);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(17, 67);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password:";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(17, 107);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(57, 13);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "Full Name:";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(17, 147);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(32, 13);
            this.lblRole.TabIndex = 3;
            this.lblRole.Text = "Role:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(130, 20);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(130, 20);
            this.txtUsername.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(130, 60);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(130, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(130, 100);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(130, 20);
            this.txtFullName.TabIndex = 6;
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Items.AddRange(new object[] {
            "Doctor",
            "Receptionist",
            "Admin"});
            this.cmbRole.Location = new System.Drawing.Point(130, 140);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(130, 21);
            this.cmbRole.TabIndex = 7;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(20, 180);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(100, 30);
            this.btnAddUser.TabIndex = 8;
            this.btnAddUser.Text = "Add User";
            // 
            // btnUpdateUser
            // 
            this.btnUpdateUser.Location = new System.Drawing.Point(160, 180);
            this.btnUpdateUser.Name = "btnUpdateUser";
            this.btnUpdateUser.Size = new System.Drawing.Size(100, 30);
            this.btnUpdateUser.TabIndex = 9;
            this.btnUpdateUser.Text = "Update User";
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(20, 220);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteUser.TabIndex = 10;
            this.btnDeleteUser.Text = "Delete User";
            // 
            // btnRefreshUsers
            // 
            this.btnRefreshUsers.Location = new System.Drawing.Point(160, 220);
            this.btnRefreshUsers.Name = "btnRefreshUsers";
            this.btnRefreshUsers.Size = new System.Drawing.Size(100, 30);
            this.btnRefreshUsers.TabIndex = 11;
            this.btnRefreshUsers.Text = "Refresh Users";
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.lblSearchSettings);
            this.tabSettings.Controls.Add(this.txtSearchSettings);
            this.tabSettings.Controls.Add(this.dgvSettings);
            this.tabSettings.Controls.Add(this.btnSaveSettings);
            this.tabSettings.Controls.Add(this.btnRefreshSettings);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(892, 524);
            this.tabSettings.TabIndex = 0;
            this.tabSettings.Text = "System Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // lblSearchSettings
            // 
            this.lblSearchSettings.AutoSize = true;
            this.lblSearchSettings.Location = new System.Drawing.Point(17, 18);
            this.lblSearchSettings.Name = "lblSearchSettings";
            this.lblSearchSettings.Size = new System.Drawing.Size(44, 13);
            this.lblSearchSettings.TabIndex = 0;
            this.lblSearchSettings.Text = "Search:";
            // 
            // txtSearchSettings
            // 
            this.txtSearchSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchSettings.Location = new System.Drawing.Point(99, 15);
            this.txtSearchSettings.Name = "txtSearchSettings";
            this.txtSearchSettings.Size = new System.Drawing.Size(771, 20);
            this.txtSearchSettings.TabIndex = 1;
            // 
            // dgvSettings
            // 
            this.dgvSettings.AllowUserToAddRows = false;
            this.dgvSettings.AllowUserToDeleteRows = false;
            this.dgvSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSettings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSettings.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvSettings.Location = new System.Drawing.Point(20, 45);
            this.dgvSettings.Name = "dgvSettings";
            this.dgvSettings.Size = new System.Drawing.Size(850, 400);
            this.dgvSettings.TabIndex = 2;
            this.dgvSettings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSettings_CellContentClick);
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSettings.Location = new System.Drawing.Point(770, 460);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(100, 30);
            this.btnSaveSettings.TabIndex = 3;
            this.btnSaveSettings.Text = "Save";
            // 
            // btnRefreshSettings
            // 
            this.btnRefreshSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefreshSettings.Location = new System.Drawing.Point(20, 460);
            this.btnRefreshSettings.Name = "btnRefreshSettings";
            this.btnRefreshSettings.Size = new System.Drawing.Size(100, 30);
            this.btnRefreshSettings.TabIndex = 4;
            this.btnRefreshSettings.Text = "Refresh";
            // 
            // SystemAdminForm
            // 
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.tabControlMain);
            this.Name = "SystemAdminForm";
            this.Text = "System Admin Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SystemAdminForm_FormClosing);
            this.tabControlMain.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.tabUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.panelUserControls.ResumeLayout(false);
            this.panelUserControls.PerformLayout();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.DataGridViewTextBoxColumn user_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewTextBoxColumn full_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn role;
    }
}
