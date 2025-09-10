using ENT_Clinic_System.CustomUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    internal class FlowLayoutHelper
    {
        private readonly FlowLayoutPanel panel;
        private readonly FlowLayoutPanel capturedPanel;

        // Store PictureBox, Labels, Notes, Category, and saved file path
        private readonly Dictionary<Panel, (PictureBox Pb, Label NoteLabel, Label CategoryLabel, string Note, string Category, string FilePath)> imageNotes
            = new Dictionary<Panel, (PictureBox, Label, Label, string, string, string)>();

        private readonly string[] categories = new[] { "Nose", "Ears", "Throat" };

        public FlowLayoutHelper(FlowLayoutPanel flowPanel, FlowLayoutPanel capturedImagesPanel = null)
        {
            panel = flowPanel ?? throw new ArgumentNullException(nameof(flowPanel));
            capturedPanel = capturedImagesPanel;

            panel.AutoScroll = true;
            panel.WrapContents = true;

            if (capturedPanel != null)
            {
                capturedPanel.AutoScroll = true;
                capturedPanel.WrapContents = true;
            }
        }

        public Panel AddImage(Bitmap image, string initialNote = "", string initialCategory = "", string filePath = "")
        {
            if (image == null) return null;
            if (string.IsNullOrEmpty(initialCategory)) initialCategory = "(no category)";

            Panel container = new Panel
            {
                Width = 150,
                Height = 220,
                Margin = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            PictureBox pb = new PictureBox
            {
                Image = (Bitmap)image.Clone(),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 150,
                Height = 150,
                Dock = DockStyle.Top,
                Cursor = Cursors.Hand
            };

            Label noteLabel = new Label
            {
                Text = string.IsNullOrEmpty(initialNote) ? "(double-click to add note)" : initialNote,
                Dock = DockStyle.Bottom,
                Height = 35,
                TextAlign = ContentAlignment.TopCenter,
                AutoEllipsis = true
            };

            Label categoryLabel = new Label
            {
                Text = initialCategory,
                Dock = DockStyle.Bottom,
                Height = 20,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                ForeColor = Color.Gray
            };

            imageNotes[container] = (pb, noteLabel, categoryLabel, initialNote, initialCategory, filePath);

            // Context menu: Delete
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete") { ForeColor = Color.Red };
            deleteItem.Click += (s, e) => DeleteImage(container);
            menu.Items.Add(deleteItem);

            pb.ContextMenuStrip = menu;
            noteLabel.ContextMenuStrip = menu;
            categoryLabel.ContextMenuStrip = menu;

            // Double-click handlers
            pb.DoubleClick += (s, e) =>
            {
                if (pb.Image != null)
                    OpenImageInDefaultViewer(new Bitmap(pb.Image));
            };
            noteLabel.DoubleClick += (s, e) => EditNoteAndCategory(container);
            categoryLabel.DoubleClick += (s, e) => EditNoteAndCategory(container);

            container.Controls.Add(noteLabel);
            container.Controls.Add(categoryLabel);
            container.Controls.Add(pb);

            panel.Controls.Add(container);

            // Captured panel (optional, read-only)
            if (capturedPanel != null)
            {
                Panel capturedContainer = new Panel
                {
                    Width = 120,
                    Height = 140,
                    Margin = new Padding(5),
                    BorderStyle = BorderStyle.FixedSingle
                };

                PictureBox capturedPb = new PictureBox
                {
                    Image = (Bitmap)image.Clone(),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill
                };

                capturedPb.DoubleClick += (s, e) =>
                {
                    if (capturedPb.Image != null)
                        OpenImageInDefaultViewer(new Bitmap(capturedPb.Image));
                };

                ContextMenuStrip capturedMenu = new ContextMenuStrip();
                ToolStripMenuItem capturedDelete = new ToolStripMenuItem("Delete") { ForeColor = Color.Red };
                capturedDelete.Click += (s, e) =>
                {
                    capturedPanel.Controls.Remove(capturedContainer);
                    capturedPb.Image?.Dispose();
                    capturedContainer.Dispose();
                };
                capturedMenu.Items.Add(capturedDelete);
                capturedPb.ContextMenuStrip = capturedMenu;

                capturedContainer.Controls.Add(capturedPb);
                capturedPanel.Controls.Add(capturedContainer);
            }

            return container;
        }

        private void EditNoteAndCategory(Panel container)
        {
            if (!imageNotes.ContainsKey(container)) return;

            var data = imageNotes[container];

            using (Form editForm = new BaseForm())
            {
                editForm.Text = "Image Note & Category";
                editForm.Size = new Size(700, 700);
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;

                TableLayoutPanel layout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    RowCount = 5,
                    ColumnCount = 2,
                    Padding = new Padding(10),
                    AutoScroll = true
                };

                layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                PictureBox previewPb = new PictureBox
                {
                    Image = (Bitmap)data.Pb.Image.Clone(),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill,
                    Height = 400
                };
                layout.Controls.Add(previewPb, 0, 0);
                layout.SetColumnSpan(previewPb, 2);

                Label lblNote = new Label
                {
                    Text = "Note:",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                layout.Controls.Add(lblNote, 0, 1);
                layout.SetColumnSpan(lblNote, 2);

                TextBox tbNote = new TextBox
                {
                    Multiline = true,
                    Text = data.Note,
                    Dock = DockStyle.Fill,
                    Height = 80
                };
                layout.Controls.Add(tbNote, 0, 2);
                layout.SetColumnSpan(tbNote, 2);

                Label lblCategory = new Label
                {
                    Text = "Category:",
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                };
                layout.Controls.Add(lblCategory, 0, 3);

                ComboBox cbCategory = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Dock = DockStyle.Fill
                };
                cbCategory.Items.AddRange(categories);
                cbCategory.SelectedItem = categories.Contains(data.Category) ? data.Category : categories[0];
                layout.Controls.Add(cbCategory, 1, 3);

                Button saveBtn = new Button
                {
                    Text = "Save",
                    Dock = DockStyle.Bottom,
                    Height = 35
                };
                saveBtn.Click += (s, e) =>
                {
                    string newNote = tbNote.Text;
                    string newCategory = cbCategory.SelectedItem?.ToString() ?? categories[0];

                    imageNotes[container] = (data.Pb, data.NoteLabel, data.CategoryLabel, newNote, newCategory, data.FilePath);

                    data.NoteLabel.Text = string.IsNullOrEmpty(newNote) ? "(add note and category)" : newNote;
                    data.CategoryLabel.Text = newCategory;

                    editForm.Close();
                };

                layout.Controls.Add(saveBtn, 0, 4);
                layout.SetColumnSpan(saveBtn, 2);

                editForm.Controls.Add(layout);
                editForm.ShowDialog();
            }
        }

        private void OpenImageInDefaultViewer(Bitmap image)
        {
            if (image == null) return;

            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                image.Save(tempPath, System.Drawing.Imaging.ImageFormat.Png);

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = tempPath,
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteImage(Panel container)
        {
            if (panel.Controls.Contains(container))
            {
                var data = imageNotes[container];
                data.Pb.Image?.Dispose();

                panel.Controls.Remove(container);
                container.Dispose();

                // Delete the saved file
                try
                {
                    if (!string.IsNullOrEmpty(data.FilePath) && File.Exists(data.FilePath))
                        File.Delete(data.FilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete image file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                imageNotes.Remove(container);
            }
        }


        public List<(Bitmap Image, string Note, string Category)> GetAllImages()
        {
            List<(Bitmap, string, string)> list = new List<(Bitmap, string, string)>();
            foreach (var kvp in imageNotes)
            {
                list.Add(((Bitmap)kvp.Value.Pb.Image.Clone(), kvp.Value.Note, kvp.Value.Category));
            }
            return list;
        }
    }
}
