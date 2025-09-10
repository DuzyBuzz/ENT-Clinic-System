using ENT_Clinic_System.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

public class VideoFlowHelper
{
    private readonly FlowLayoutPanel panel;
    private readonly string[] categories = new[] { "Nose", "Ears", "Throat" };

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
        if (string.IsNullOrEmpty(initialCategory)) initialCategory = "(no category)";

        Panel container = new Panel
        {
            Width = 150,
            Height = 200,
            Margin = new Padding(5),
            BorderStyle = BorderStyle.FixedSingle
        };

        // ====== Video Thumbnail ======
        VideoClipControl clip = new VideoClipControl(videoPath)
        {
            Dock = DockStyle.Top,
            Height = 120
        };

        Label noteLabel = new Label
        {
            Text = string.IsNullOrEmpty(initialNote) ? "(double-click to add note)" : initialNote,
            Dock = DockStyle.Bottom,
            Height = 40,
            TextAlign = ContentAlignment.MiddleCenter,
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

        videoNotes[container] = (videoPath, noteLabel, categoryLabel, initialNote, initialCategory);

        // Context menu for delete
        ContextMenuStrip menu = new ContextMenuStrip();
        ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete") { ForeColor = Color.Red };
        deleteItem.Click += (s, e) => DeleteVideo(container);
        menu.Items.Add(deleteItem);

        clip.ContextMenuStrip = menu;
        noteLabel.ContextMenuStrip = menu;
        categoryLabel.ContextMenuStrip = menu;

        // Double-click to edit note/category
        clip.DoubleClick += (s, e) => EditVideoNoteAndCategory(container);
        noteLabel.DoubleClick += (s, e) => EditVideoNoteAndCategory(container);
        categoryLabel.DoubleClick += (s, e) => EditVideoNoteAndCategory(container);

        container.Controls.Add(noteLabel);
        container.Controls.Add(categoryLabel);
        container.Controls.Add(clip);
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

            // Column style: first auto, second takes remaining space
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Row styles
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Thumbnail
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Note label
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 40)); // Note textbox
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Category inline
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Save button

            // ====== Thumbnail Preview (spans 2 columns) ======
            PictureBox thumbnailPb = new PictureBox
            {
                Image = GenerateVideoThumbnail(data.VideoPath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                Height = 400
            };
            layout.Controls.Add(thumbnailPb, 0, 0);
            layout.SetColumnSpan(thumbnailPb, 2);

            // ====== Note Label (spans 2 columns, above textbox) ======
            Label lblNote = new Label
            {
                Text = "Note:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            layout.Controls.Add(lblNote, 0, 1);
            layout.SetColumnSpan(lblNote, 2);

            // ====== Note Textbox (spans 2 columns) ======
            TextBox tbNote = new TextBox
            {
                Multiline = true,
                Text = data.Note,
                Dock = DockStyle.Fill,
                Height = 80
            };
            layout.Controls.Add(tbNote, 0, 2);
            layout.SetColumnSpan(tbNote, 2);

            // ====== Category Inline ======
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
            cbCategory.Items.AddRange(new string[] { "Nose", "Ears", "Throat" });
            cbCategory.SelectedItem = cbCategory.Items.Contains(data.Category) ? data.Category : "Nose";
            layout.Controls.Add(cbCategory, 1, 3);

            // ====== Save Button (spans 2 columns) ======
            Button saveBtn = new Button
            {
                Text = "Save",
                Dock = DockStyle.Bottom,
                Height = 35
            };
            saveBtn.Click += (s, e) =>
            {
                string newNote = tbNote.Text;
                string newCategory = cbCategory.SelectedItem?.ToString() ?? "Nose";

                videoNotes[container] = (data.VideoPath, data.NoteLabel, data.CategoryLabel, newNote, newCategory);
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

    // Generate thumbnail helper
    private Bitmap GenerateVideoThumbnail(string videoPath)
    {
        try
        {
            using (var reader = new Accord.Video.FFMPEG.VideoFileReader())
            {
                reader.Open(videoPath);
                Bitmap frame = reader.ReadVideoFrame();
                reader.Close();
                return frame;
            }
        }
        catch
        {
            Bitmap bmp = new Bitmap(150, 150);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);
                g.DrawString("Video", new Font("Segoe UI", 14), Brushes.White, new PointF(20, 50));
            }
            return bmp;
        }
    }



    public void DeleteVideo(Panel container)
    {
        if (panel.Controls.Contains(container))
        {
            var data = videoNotes[container];
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
