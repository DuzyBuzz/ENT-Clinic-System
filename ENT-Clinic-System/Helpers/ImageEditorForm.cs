using System;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public partial class ImageEditorForm : Form
    {
        public Bitmap ImageToEdit { get; private set; }
        public string Notes { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        // Parameterless constructor for Designer
        public ImageEditorForm()
        {
            InitializeComponent();
        }

        // Runtime constructor with image
        public void LoadImage(Bitmap image)
        {
            if (image != null)
            {
                ImageToEdit = (Bitmap)image.Clone();
                pictureBox.Image = ImageToEdit;
            }
        }

        private void ImageEditorForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(notesTextBox.Text))
            {
                notesTextBox.ForeColor = Color.Gray;
                notesTextBox.Text = "Enter notes here...";
                notesTextBox.GotFocus += (s, ev) =>
                {
                    if (notesTextBox.Text == "Enter notes here...")
                    {
                        notesTextBox.Text = "";
                        notesTextBox.ForeColor = Color.Black;
                    }
                };
                notesTextBox.LostFocus += (s, ev) =>
                {
                    if (string.IsNullOrEmpty(notesTextBox.Text))
                    {
                        notesTextBox.Text = "Enter notes here...";
                        notesTextBox.ForeColor = Color.Gray;
                    }
                };
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Notes = notesTextBox.Text == "Enter notes here..." ? "" : notesTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            IsDeleted = true;
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }
    }
}
