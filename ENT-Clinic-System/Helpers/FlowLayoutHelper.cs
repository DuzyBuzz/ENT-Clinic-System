using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    internal class FlowLayoutHelper
    {
        private readonly FlowLayoutPanel panel;

        // Map Panel -> (PictureBox, Label, Note)
        private readonly Dictionary<Panel, (PictureBox Pb, Label NoteLabel, string Note)> imageNotes
            = new Dictionary<Panel, (PictureBox, Label, string)>();

        public FlowLayoutHelper(FlowLayoutPanel flowPanel)
        {
            panel = flowPanel ?? throw new ArgumentNullException(nameof(flowPanel));

            // Configure FlowLayoutPanel for wrapping layout
            panel.AutoScroll = true;
            panel.WrapContents = true;
        }

        /// <summary>
        /// Add an image to the FlowLayoutPanel with optional initial note.
        /// </summary>
        public Panel AddImage(Bitmap image, string initialNote = "")
        {
            if (image == null) return null;

            // --- Container panel ---
            Panel container = new Panel
            {
                Width = 150,
                Height = 190, // 150 image + 40 label
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            // --- Image box ---
            PictureBox pb = new PictureBox
            {
                Image = (Bitmap)image.Clone(),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 150,
                Height = 150,
                Dock = DockStyle.Top,
                Cursor = Cursors.Hand
            };

            // --- Label for notes ---
            Label noteLabel = new Label
            {
                Text = string.IsNullOrEmpty(initialNote) ? "(double-click to add note)" : initialNote,
                Dock = DockStyle.Bottom,
                Height = 35,
                TextAlign = ContentAlignment.TopCenter,
                AutoEllipsis = true // show "..." if text is long
            };

            // Save mapping
            imageNotes[container] = (pb, noteLabel, initialNote);

            // Context menu (delete)
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete")
            {
                ForeColor = Color.Red
            };
            deleteItem.Click += (s, e) => DeleteImage(container);
            menu.Items.Add(deleteItem);
            pb.ContextMenuStrip = menu;
            noteLabel.ContextMenuStrip = menu;

            // Click to edit note
            pb.DoubleClick += (s, e) => EditNote(container);
            noteLabel.DoubleClick += (s, e) => EditNote(container);

            // Build container
            container.Controls.Add(noteLabel);
            container.Controls.Add(pb);

            panel.Controls.Add(container);

            return container;
        }

        /// <summary>
        /// Edit the note associated with an image container.
        /// </summary>
        private void EditNote(Panel container)
        {
            if (!imageNotes.ContainsKey(container)) return;

            var data = imageNotes[container];

            using (Form noteForm = new Form())
            {
                noteForm.Text = "Edit Note";
                noteForm.Size = new Size(300, 200);
                noteForm.StartPosition = FormStartPosition.CenterParent;

                TextBox tb = new TextBox
                {
                    Multiline = true,
                    Dock = DockStyle.Fill,
                    Text = data.Note
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
                    string newNote = tb.Text;
                    imageNotes[container] = (data.Pb, data.NoteLabel, newNote);
                    data.NoteLabel.Text = string.IsNullOrEmpty(newNote) ? "(double-click to add note)" : newNote;
                    noteForm.Close();
                };
                noteForm.Controls.Add(saveBtn);

                noteForm.ShowDialog();
            }
        }

        /// <summary>
        /// Delete an image container and its note.
        /// </summary>
        public void DeleteImage(Panel container)
        {
            if (panel.Controls.Contains(container))
            {
                // Dispose picturebox image
                var data = imageNotes[container];
                data.Pb.Image?.Dispose();

                panel.Controls.Remove(container);
                container.Dispose();
                imageNotes.Remove(container);
            }
        }

        /// <summary>
        /// Get all images with their notes.
        /// </summary>
        public List<(Bitmap Image, string Note)> GetAllImages()
        {
            List<(Bitmap, string)> list = new List<(Bitmap, string)>();
            foreach (var kvp in imageNotes)
            {
                // clone so caller owns it safely
                list.Add(((Bitmap)kvp.Value.Pb.Image.Clone(), kvp.Value.Note));
            }
            return list;
        }
    }
}
