using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public partial class SelectPhotoOptionForm : Form
    {
        public enum PhotoOption
        {
            None,
            Camera,
            File
        }

        public PhotoOption SelectedOption { get; private set; } = PhotoOption.None;

        public SelectPhotoOptionForm()
        {
            InitializeComponent();
        }

        private void btnCamera_Click(object sender, EventArgs e)
        {
            SelectedOption = PhotoOption.Camera;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            SelectedOption = PhotoOption.File;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedOption = PhotoOption.None;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
