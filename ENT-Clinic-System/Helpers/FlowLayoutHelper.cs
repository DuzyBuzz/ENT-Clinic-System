using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    internal class FlowLayoutHelper
    {
        private FlowLayoutPanel panel;

        // Dictionary to store PictureBox -> note mapping
        private Dictionary<PictureBox, string> imageNotes = new Dictionary<PictureBox, string>();

        public FlowLayoutHelper(FlowLayoutPanel flowPanel)
        {
            panel = flowPanel ?? throw new ArgumentNullException(nameof(flowPanel));
        }

        /// <summary>
        /// Add an image to the FlowLayoutPanel with optional initial note
        /// </summary>
        public void AddImage(Bitmap image, string initialNote = "")
        {
            if (image == null) return;

            PictureBox pb = new PictureBox
            {
                Image = (Bitmap)image.Clone(),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 150,
                Height = 150,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            // Save initial note
            imageNotes[pb] = initialNote;

            // Click to edit note
            pb.Click += (s, e) => EditNote(pb);

            // Optional: Context menu to delete
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");
            deleteItem.Click += (s, e) => DeleteImage(pb);
            menu.Items.Add(deleteItem);
            pb.ContextMenuStrip = menu;

            panel.Controls.Add(pb);
        }

        /// <summary>
        /// Edit the note associated with the PictureBox
        /// </summary>
        private void EditNote(PictureBox pb)
        {
            if (!imageNotes.ContainsKey(pb)) return;

            string currentNote = imageNotes[pb];
            using (Form noteForm = new Form())
            {
                noteForm.Text = "Edit Note";
                noteForm.Size = new Size(300, 200);
                noteForm.StartPosition = FormStartPosition.CenterParent;

                TextBox tb = new TextBox
                {
                    Multiline = true,
                    Dock = DockStyle.Fill,
                    Text = currentNote
                };
                noteForm.Controls.Add(tb);

                Button saveBtn = new Button
                {
                    Text = "Save",
                    Dock = DockStyle.Bottom,
                    Height = 30
                };
                saveBtn.Click += (s, e) =>
                {
                    imageNotes[pb] = tb.Text;
                    noteForm.Close();
                };
                noteForm.Controls.Add(saveBtn);

                noteForm.ShowDialog();
            }
        }

        /// <summary>
        /// Delete the image and note
        /// </summary>
        public void DeleteImage(PictureBox pb)
        {
            if (panel.Controls.Contains(pb))
            {
                panel.Controls.Remove(pb);
                pb.Dispose();
                if (imageNotes.ContainsKey(pb))
                    imageNotes.Remove(pb);
            }
        }

        /// <summary>
        /// Get all images with their notes
        /// </summary>
        public List<(Bitmap Image, string Note)> GetAllImages()
        {
            List<(Bitmap, string)> list = new List<(Bitmap, string)>();
            foreach (var kvp in imageNotes)
            {
                list.Add((kvp.Key.Image as Bitmap, kvp.Value));
            }
            return list;
        }
    }
}
