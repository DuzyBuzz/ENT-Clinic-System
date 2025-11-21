using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class VideoFlowHelper
{
    private readonly FlowLayoutPanel panel;
    private readonly string[] categories = new[] { "", "Nose", "Ears", "Throat", "Maxillofacial", "Head & Neck", "Others" };

    // Map Panel -> (VideoPath, NoteLabel, CategoryLabel, Note, Category)
    private readonly Dictionary<Panel, (string VideoPath, Label NoteLabel, Label CategoryLabel, string Note, string Category)> videoNotes
        = new Dictionary<Panel, (string, Label, Label, string, string)>();

    public VideoFlowHelper(FlowLayoutPanel flowPanel)
    {
        panel = flowPanel ?? throw new ArgumentNullException(nameof(flowPanel));
        panel.AutoScroll = true;
        panel.WrapContents = true;
    }

    public Panel AddVideo(string videoPath, string initialNote = "", string initialCategory = "")
    {
        if (!File.Exists(videoPath)) return null;
        if (string.IsNullOrEmpty(initialCategory)) initialCategory = "";

        Panel container = new Panel
        {
            Width = 300,
            Height = 300,
            Margin = new Padding(5),
            BorderStyle = BorderStyle.FixedSingle
        };

        // 🔹 Use PictureBox for thumbnail instead of Panel
        PictureBox videoThumb = new PictureBox
        {
            Dock = DockStyle.Top,
            Width = 300,
            Height = 290,
            SizeMode = PictureBoxSizeMode.Zoom,
            BackColor = Color.Black,
            Cursor = Cursors.Hand
        };

        try
        {
            using (var reader = new Accord.Video.FFMPEG.VideoFileReader())
            {
                reader.Open(videoPath);
                if (reader.IsOpen)
                {
                    Bitmap frame = reader.ReadVideoFrame(); // first frame
                    if (frame != null)
                    {
                        videoThumb.Image = new Bitmap(frame);
                        frame.Dispose();
                    }
                }
                reader.Close();
            }
        }
        catch
        {
            // fallback if thumbnail extraction fails
            Bitmap bmp = new Bitmap(150, 120);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);
                g.DrawString("Video", new Font("Segoe UI", 10), Brushes.White, new PointF(30, 50));
            }
            videoThumb.Image = bmp;
        }

        Label noteLabel = new Label
        {
            Text = string.IsNullOrEmpty(initialNote) ? "" : initialNote,
            Dock = DockStyle.Bottom,
            Height = 40,
            TextAlign = ContentAlignment.MiddleCenter,
            AutoEllipsis = true,
            Cursor = Cursors.Hand
        };

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

        videoNotes[container] = (videoPath, noteLabel, categoryLabel, initialNote, initialCategory);

        // Context menu for delete
        ContextMenuStrip menu = new ContextMenuStrip();
        ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete") { ForeColor = Color.Red };
        deleteItem.Click += (s, e) => DeleteVideo(container);
        menu.Items.Add(deleteItem);

        videoThumb.ContextMenuStrip = menu;
        noteLabel.ContextMenuStrip = menu;
        categoryLabel.ContextMenuStrip = menu;

        // Double click → edit note/category
        videoThumb.DoubleClick += (s, e) => EditVideoNoteAndCategory(container);
        noteLabel.DoubleClick += (s, e) => EditVideoNoteAndCategory(container);
        categoryLabel.DoubleClick += (s, e) => EditVideoNoteAndCategory(container);

        // 🔹 Open video when clicked
        videoThumb.Click += (s, e) =>
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = videoPath,
                UseShellExecute = true
            });
        };

        container.Controls.Add(noteLabel);
        container.Controls.Add(categoryLabel);
        container.Controls.Add(videoThumb);
        panel.Controls.Add(container);

        return container;
    }



    private void EditVideoNoteAndCategory(Panel container)
    {
        if (!videoNotes.ContainsKey(container)) return;

        var data = videoNotes[container];

        using (Form editForm = new Form())
        {
            editForm.Text = "Video Note & Category";
            editForm.Size = new Size(500, 400);
            editForm.StartPosition = FormStartPosition.CenterParent;
            editForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 2,
                Padding = new Padding(10),
                AutoScroll = true
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

            Label lblNote = new Label { Text = "Note:", AutoSize = true, Anchor = AnchorStyles.Left };
            layout.Controls.Add(lblNote, 0, 0);
            layout.SetColumnSpan(lblNote, 2);

            TextBox tbNote = new TextBox { Multiline = true, Text = data.Note, Dock = DockStyle.Fill };
            layout.Controls.Add(tbNote, 0, 1);
            layout.SetColumnSpan(tbNote, 2);

            Label lblCategory = new Label { Text = "Category:", AutoSize = true, Anchor = AnchorStyles.Left };
            layout.Controls.Add(lblCategory, 0, 2);

            ComboBox cbCategory = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
            cbCategory.Items.AddRange(categories);
            cbCategory.SelectedItem = cbCategory.Items.Contains(data.Category) ? data.Category : categories[0];
            layout.Controls.Add(cbCategory, 1, 2);

            Button saveBtn = new Button { Text = "Save", Dock = DockStyle.Bottom };
            saveBtn.Click += (s, e) =>
            {
                string newNote = tbNote.Text;
                string newCategory = cbCategory.SelectedItem?.ToString() ?? categories[0];

                videoNotes[container] = (data.VideoPath, data.NoteLabel, data.CategoryLabel, newNote, newCategory);
                data.NoteLabel.Text = string.IsNullOrEmpty(newNote) ? "" : newNote;
                data.CategoryLabel.Text = newCategory;

                editForm.Close();
            };
            layout.Controls.Add(saveBtn, 0, 3);
            layout.SetColumnSpan(saveBtn, 2);

            editForm.Controls.Add(layout);
            editForm.ShowDialog();
        }
    }

    public void DeleteVideo(Panel container)
    {
        if (panel.Controls.Contains(container))
        {
            panel.Controls.Remove(container);
            container.Dispose();
            videoNotes.Remove(container);
        }
    }

    public List<(string VideoPath, string Note, string Category)> GetAllVideos()
    {
        List<(string, string, string)> list = new List<(string, string, string)>();
        foreach (var kvp in videoNotes)
        {
            list.Add((kvp.Value.VideoPath, kvp.Value.Note, kvp.Value.Category));
        }
        return list;
    }
}
