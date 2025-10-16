namespace ENT_Clinic_System.UI
{
    partial class AutoCompleteManager
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoCompleteManager));
            this.autoCompleteDataGridView = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.autoCompleteDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // autoCompleteDataGridView
            // 
            this.autoCompleteDataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.autoCompleteDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.autoCompleteDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.autoCompleteDataGridView.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.autoCompleteDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.autoCompleteDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.autoCompleteDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.column_name,
            this.value});
            this.autoCompleteDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoCompleteDataGridView.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.autoCompleteDataGridView.Location = new System.Drawing.Point(0, 0);
            this.autoCompleteDataGridView.Name = "autoCompleteDataGridView";
            this.autoCompleteDataGridView.Size = new System.Drawing.Size(800, 450);
            this.autoCompleteDataGridView.TabIndex = 2;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.FillWeight = 50.41844F;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // column_name
            // 
            this.column_name.DataPropertyName = "column_name";
            this.column_name.FillWeight = 76.14214F;
            this.column_name.HeaderText = "Field Name";
            this.column_name.Name = "column_name";
            this.column_name.ReadOnly = true;
            // 
            // value
            // 
            this.value.DataPropertyName = "value";
            this.value.FillWeight = 173.4394F;
            this.value.HeaderText = "Value";
            this.value.Name = "value";
            // 
            // AutoCompleteManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.autoCompleteDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutoCompleteManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Complete Manager";
            this.Load += new System.EventHandler(this.AutoCompleteManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.autoCompleteDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView autoCompleteDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
    }
}