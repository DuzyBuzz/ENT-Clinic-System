using System;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.CustomUI
{
    public class TextEditorToolStrip
    {
        private RichTextBox targetRichTextBox;
        private ToolStrip toolStrip;

        public TextEditorToolStrip(ToolStrip toolStrip, RichTextBox richTextBox)
        {
            this.toolStrip = toolStrip ?? throw new ArgumentNullException(nameof(toolStrip));
            this.targetRichTextBox = richTextBox ?? throw new ArgumentNullException(nameof(richTextBox));

            InitializeControls();
        }

        private void InitializeControls()
        {
            // ========== Bold ==========
            var boldButton = new ToolStripButton("B") { CheckOnClick = true, Font = new Font("Arial", 9, FontStyle.Bold) };
            boldButton.Click += (s, e) => ToggleStyle(FontStyle.Bold);

            // ========== Italic ==========
            var italicButton = new ToolStripButton("I") { CheckOnClick = true, Font = new Font("Arial", 9, FontStyle.Italic) };
            italicButton.Click += (s, e) => ToggleStyle(FontStyle.Italic);

            // ========== Underline ==========
            var underlineButton = new ToolStripButton("U") { CheckOnClick = true, Font = new Font("Arial", 9, FontStyle.Underline) };

            underlineButton.Click += (s, e) => ToggleStyle(FontStyle.Underline);




            // ========== Font Family ==========
            var fontComboBox = new ToolStripComboBox
            {
                AutoSize = false,
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Add all installed fonts
            foreach (FontFamily font in FontFamily.Families)
                fontComboBox.Items.Add(font.Name);

            // Default selection
            fontComboBox.Text = "Arial";
            fontComboBox.SelectedIndexChanged += (s, e) =>
            {
                if (targetRichTextBox.SelectionFont != null)
                {
                    string fontName = fontComboBox.SelectedItem.ToString();
                    float size = targetRichTextBox.SelectionFont.Size;
                    targetRichTextBox.SelectionFont = new Font(fontName, size, targetRichTextBox.SelectionFont.Style);
                }
            };

            // ===== Owner-draw using the inner ComboBox =====
            fontComboBox.ComboBox.DrawMode = DrawMode.OwnerDrawFixed; // Use the underlying ComboBox
                                                                      // Access the inner ComboBox
            fontComboBox.ComboBox.DropDownHeight = 50; // height in pixels

            fontComboBox.ComboBox.DrawItem += (sender, e) =>
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    string fontName = fontComboBox.Items[e.Index].ToString();
                    Font itemFont;

                    try
                    {
                        itemFont = new Font(fontName, 9); // Display font in its own font
                    }
                    catch
                    {
                        itemFont = e.Font; // fallback
                    }

                    using (Brush brush = new SolidBrush(e.ForeColor))
                    {
                        e.Graphics.DrawString(fontName, itemFont, brush, e.Bounds);
                    }
                }

                e.DrawFocusRectangle();
            };


            // ========== Font Size ==========
            var fontSizeComboBox = new ToolStripComboBox();
            fontSizeComboBox.Items.AddRange(new object[] { "8", "10", "12", "14", "16", "18", "20" });
            fontSizeComboBox.AutoSize = false;
            fontSizeComboBox.Width = 50;
            fontSizeComboBox.SelectedIndexChanged += (s, e) =>
            {
                if (targetRichTextBox.SelectionFont != null)
                {
                    float newSize = float.Parse(fontSizeComboBox.SelectedItem.ToString());
                    targetRichTextBox.SelectionFont = new Font(
                        targetRichTextBox.SelectionFont.FontFamily,
                        newSize,
                        targetRichTextBox.SelectionFont.Style);
                }
            };

            // ========== Alignment ==========
            var alignLeftButton = new ToolStripButton("Left");
            alignLeftButton.Click += (s, e) => targetRichTextBox.SelectionAlignment = HorizontalAlignment.Left;

            var alignCenterButton = new ToolStripButton("Center");
            alignCenterButton.Click += (s, e) => targetRichTextBox.SelectionAlignment = HorizontalAlignment.Center;

            var alignRightButton = new ToolStripButton("Right");
            alignRightButton.Click += (s, e) => targetRichTextBox.SelectionAlignment = HorizontalAlignment.Right;

            // ========== Bullets ==========
            var bulletButton = new ToolStripButton("•");
            bulletButton.Click += (s, e) => targetRichTextBox.SelectionBullet = !targetRichTextBox.SelectionBullet;

            // Add to the existing ToolStrip
            toolStrip.Items.Clear();
            toolStrip.Items.AddRange(new ToolStripItem[]
            {
                boldButton, italicButton, underlineButton,
                new ToolStripSeparator(),
                fontComboBox, fontSizeComboBox,
                new ToolStripSeparator(),
                alignLeftButton, alignCenterButton, alignRightButton,
                new ToolStripSeparator(),
                bulletButton
            });
        }

        private void ToggleStyle(FontStyle style)
        {
            if (targetRichTextBox.SelectionFont != null)
            {
                FontStyle newStyle = targetRichTextBox.SelectionFont.Style ^ style;
                targetRichTextBox.SelectionFont = new Font(targetRichTextBox.SelectionFont, newStyle);
            }
        }
    }
}
