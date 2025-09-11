using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class ImageFlowHelper
{
    private readonly FlowLayoutPanel panel;
    private readonly string[] categories = new[] { "Nose", "Ears", "Throat" };

    // Map Panel -> (ImagePath, NoteLabel, CategoryLabel, Note, Category)
    private readonly Dictionary<Panel, (string ImagePath, Label NoteLabel, Label CategoryLabel, string Note, string Category)> imageNotes
        = new Dictionary<Panel, (string, Label, Label, string, string)>();

    public ImageFlowHelper(FlowLayoutPanel flowPanel)
    {
        panel = flowPanel ?? throw new ArgumentNullException(nameof(flowPanel));
        panel.AutoScroll = true;
        panel.WrapContents = true;
    }

    /// <summary>
    /// Adds an image to the FlowLayoutPanel with optional note and category.
    /// </summary>
    public Panel AddImage(string imagePath, string initialNote = "", string initialCategory = "")
    {
        if (!File.Exists(imagePath)) return null;
        if (string.IsNullOrEmpty(initialCategory)) initialCategory = "(no category)";

        // Container for image + note + category
        Panel container = new Panel
        {
            Width = 150,
            Height = 200,
            Margin = new Padding(5),
            BorderStyle = BorderStyle.FixedSingle
        };

        // Image thumbnail
        PictureBox thumbPb = new PictureBox
        {
            Image = LoadImageSafe(imagePath),
            Dock = DockStyle.Top,
            Height = 120,
            SizeMode = PictureBoxSizeMode.Zoom,
            Cursor = Cursors.Hand // show clickable cursor
        };

        // Note label
        Label noteLabel = new Label
        {
            Text = string.IsNullOrEmpty(initialNote) ? "(double-click to add note)" : initialNote,
            Dock = DockStyle.Bottom,
            Height = 40,
            TextAlign = ContentAlignment.MiddleCenter,
            AutoEllipsis = true,
            Cursor = Cursors.Hand
        };

        // Category label
        Label categoryLabel = new Label
        {
            Text = initialCategory,
            Dock = DockStyle.Bottom,
            Height = 20,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Segoe UI", 8, FontStyle.Italic),
            ForeColor = Color.Gray,
            Cursor = Cursors.Hand
        };

        // Store in dictionary
        imageNotes[container] = (imagePath, noteLabel, categoryLabel, initialNote, initialCategory);

        // Context menu for delete
        ContextMenuStrip menu = new ContextMenuStrip();
        ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete") { ForeColor = Color.Red };
        deleteItem.Click += (s, e) => DeleteImage(container);
        menu.Items.Add(deleteItem);

        thumbPb.ContextMenuStrip = menu;
        noteLabel.ContextMenuStrip = menu;
        categoryLabel.ContextMenuStrip = menu;

        // ==== Separate click behaviors ====
        thumbPb.Click += (s, e) =>
        {
            try
            {
                // Open image in default Photos app
                Process.Start(new ProcessStartInfo
                {
                    FileName = imagePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };

        noteLabel.Click += (s, e) => EditImageNoteAndCategory(container);
        categoryLabel.Click += (s, e) => EditImageNoteAndCategory(container);

        // Add controls to container
        container.Controls.Add(noteLabel);
        container.Controls.Add(categoryLabel);
        container.Controls.Add(thumbPb);
        panel.Controls.Add(container);

        return container;
    }


    /// <summary>
    /// Opens a form to edit the note and category of the image.
    /// </summary>
    private void EditImageNoteAndCategory(Panel container)
    {
        if (!imageNotes.ContainsKey(container)) return;

        var data = imageNotes[container];

        using (Form editForm = new Form())
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

            // Image preview
            PictureBox previewPb = new PictureBox
            {
                Image = LoadImageSafe(data.ImagePath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                Height = 400
            };
            layout.Controls.Add(previewPb, 0, 0);
            layout.SetColumnSpan(previewPb, 2);

            // Note label
            Label lblNote = new Label
            {
                Text = "Note:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            layout.Controls.Add(lblNote, 0, 1);
            layout.SetColumnSpan(lblNote, 2);

            // Note textbox
            TextBox tbNote = new TextBox
            {
                Multiline = true,
                Text = data.Note,
                Dock = DockStyle.Fill,
                Height = 80
            };
            layout.Controls.Add(tbNote, 0, 2);
            layout.SetColumnSpan(tbNote, 2);

            // Category label
            Label lblCategory = new Label
            {
                Text = "Category:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            layout.Controls.Add(lblCategory, 0, 3);

            // Category combobox
            ComboBox cbCategory = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill
            };
            cbCategory.Items.AddRange(categories);
            cbCategory.SelectedItem = cbCategory.Items.Contains(data.Category) ? data.Category : categories[0];
            layout.Controls.Add(cbCategory, 1, 3);

            // Save button
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

                imageNotes[container] = (data.ImagePath, data.NoteLabel, data.CategoryLabel, newNote, newCategory);
                data.NoteLabel.Text = string.IsNullOrEmpty(newNote) ? "(double-click to add note)" : newNote;
                data.CategoryLabel.Text = newCategory;

                editForm.Close();
            };
            layout.Controls.Add(saveBtn, 0, 4);
            layout.SetColumnSpan(saveBtn, 2);

            editForm.Controls.Add(layout);
            editForm.ShowDialog();
        }
    }

    /// <summary>
    /// Deletes an image from the panel and dictionary.
    /// </summary>
    public void DeleteImage(Panel container)
    {
        if (panel.Controls.Contains(container))
        {
            panel.Controls.Remove(container);
            container.Dispose();
            imageNotes.Remove(container);
        }
    }

    /// <summary>
    /// Returns all images with their notes and categories.
    /// </summary>
    public List<(string ImagePath, string Note, string Category)> GetAllImages()
    {
        List<(string, string, string)> list = new List<(string, string, string)>();
        foreach (var kvp in imageNotes)
        {
            list.Add((kvp.Value.ImagePath, kvp.Value.Note, kvp.Value.Category));
        }
        return list;
    }

    /// <summary>
    /// Loads an image safely without locking the file.
    /// </summary>
    private Image LoadImageSafe(string path)
    {
        try
        {
            using (var temp = Image.FromFile(path))
            {
                return new Bitmap(temp); // clone to avoid file lock
            }
        }
        catch
        {
            // Return placeholder image if failed
            Bitmap bmp = new Bitmap(150, 150);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Gray);
                g.DrawString("Image", new Font("Segoe UI", 14), Brushes.White, new PointF(20, 50));
            }
            return bmp;
        }
    }
}
