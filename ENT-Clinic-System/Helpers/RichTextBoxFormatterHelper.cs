using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// RichTextBox formatter helper compatible with .NET Framework 4.8.
    /// Usage:
    ///     RichTextBoxFormatterHelper.Attach(myRichTextBox, this);
    ///
    /// Adds a ToolStrip with common formatting tools and a ContextMenuStrip.
    /// Designed to avoid APIs that are unavailable on .NET Framework 4.8.
    /// </summary>
    public static class RichTextBoxFormatterHelper
    {
        public static void Attach(RichTextBox rtb, Form owner = null)
        {
            if (rtb == null) throw new ArgumentNullException("rtb");

            // Prevent attaching twice
            if (rtb.Tag is string s && s == "RTB_FORMATTER_ATTACHED") return;
            rtb.Tag = "RTB_FORMATTER_ATTACHED";

            var fontDialog = new FontDialog();
            var colorDialog = new ColorDialog();

            var toolStrip = new ToolStrip();
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip.RenderMode = ToolStripRenderMode.System;
            toolStrip.Stretch = true;

            var btnBold = new ToolStripButton("B") { CheckOnClick = true };
            btnBold.ToolTipText = "Bold";
            btnBold.Click += (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Bold, btnBold.Checked);

            var btnItalic = new ToolStripButton("I") { CheckOnClick = true };
            btnItalic.ToolTipText = "Italic";
            btnItalic.Click += (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Italic, btnItalic.Checked);

            var btnUnderline = new ToolStripButton("U") { CheckOnClick = true };
            btnUnderline.ToolTipText = "Underline";
            btnUnderline.Click += (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Underline, btnUnderline.Checked);

            var fontFamilyBox = new ToolStripComboBox();
            fontFamilyBox.Width = 160;
            fontFamilyBox.DropDownStyle = ComboBoxStyle.DropDownList;
            try
            {
                var families = System.Drawing.FontFamily.Families;
                foreach (var ff in families)
                {
                    fontFamilyBox.Items.Add(ff.Name);
                }
            }
            catch { }

            fontFamilyBox.SelectedIndexChanged += (sender, e) =>
            {
                if (fontFamilyBox.SelectedItem != null)
                {
                    ApplyFontFamily(rtb, fontFamilyBox.SelectedItem.ToString());
                }
            };

            var fontSizeBox = new ToolStripComboBox();
            fontSizeBox.Width = 70;
            fontSizeBox.DropDownStyle = ComboBoxStyle.DropDown;
            fontSizeBox.Items.AddRange(new object[] { "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" });
            fontSizeBox.Leave += (s1, e1) => { float size; if (float.TryParse(fontSizeBox.Text, out size)) ApplyFontSize(rtb, size); };
            fontSizeBox.KeyDown += (s1, e1) => { if (e1.KeyCode == Keys.Enter) { float size; if (float.TryParse(fontSizeBox.Text, out size)) ApplyFontSize(rtb, size); } };

            var btnColor = new ToolStripButton("A");
            btnColor.ToolTipText = "Color";
            btnColor.Click += (s1, e1) => { if (owner != null) colorDialog.ShowDialog(owner); else colorDialog.ShowDialog(); ApplyColor(rtb, colorDialog.Color); };

            var btnAlignLeft = new ToolStripButton("L"); btnAlignLeft.ToolTipText = "Align Left"; btnAlignLeft.Click += (s1, e1) => rtb.SelectionAlignment = HorizontalAlignment.Left;
            var btnAlignCenter = new ToolStripButton("C"); btnAlignCenter.ToolTipText = "Center"; btnAlignCenter.Click += (s1, e1) => rtb.SelectionAlignment = HorizontalAlignment.Center;
            var btnAlignRight = new ToolStripButton("R"); btnAlignRight.ToolTipText = "Align Right"; btnAlignRight.Click += (s1, e1) => rtb.SelectionAlignment = HorizontalAlignment.Right;

            var btnBullets = new ToolStripButton("•") { CheckOnClick = true }; btnBullets.ToolTipText = "Toggle Bullets"; btnBullets.Click += (s1, e1) => rtb.SelectionBullet = btnBullets.Checked;

            var btnIndent = new ToolStripButton("→"); btnIndent.ToolTipText = "Increase Indent"; btnIndent.Click += (s1, e1) => rtb.SelectionIndent += 20;
            var btnOutdent = new ToolStripButton("←"); btnOutdent.ToolTipText = "Decrease Indent"; btnOutdent.Click += (s1, e1) => rtb.SelectionIndent = Math.Max(0, rtb.SelectionIndent - 20);

            var btnWordWrap = new ToolStripButton("Wrap") { CheckOnClick = true }; btnWordWrap.ToolTipText = "Toggle Word Wrap";
            btnWordWrap.Checked = rtb.WordWrap;
            btnWordWrap.Click += (s1, e1) => { rtb.WordWrap = btnWordWrap.Checked; rtb.ScrollBars = rtb.WordWrap ? RichTextBoxScrollBars.Vertical : RichTextBoxScrollBars.Both; };

            var btnInsertDate = new ToolStripButton("Date"); btnInsertDate.ToolTipText = "Insert Date/Time"; btnInsertDate.Click += (s1, e1) => rtb.SelectedText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var btnClear = new ToolStripButton("Clear"); btnClear.ToolTipText = "Clear Formatting (keep text)"; btnClear.Click += (s1, e1) => ClearFormatting(rtb);

            var btnSave = new ToolStripButton("Save"); btnSave.ToolTipText = "Save to RTF"; btnSave.Click += (s1, e1) =>
            {
                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Rich Text Format (*.rtf)|*.rtf|Text File (*.txt)|*.txt";
                    if (sfd.ShowDialog(owner ?? rtb.FindForm()) == DialogResult.OK)
                    {
                        if (sfd.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                            System.IO.File.WriteAllText(sfd.FileName, rtb.Text);
                        else
                            rtb.SaveFile(sfd.FileName, RichTextBoxStreamType.RichText);
                    }
                }
            };

            var btnLoad = new ToolStripButton("Open"); btnLoad.ToolTipText = "Load RTF / Text"; btnLoad.Click += (s1, e1) =>
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Rich Text Format (*.rtf)|*.rtf|Text File (*.txt)|*.txt";
                    if (ofd.ShowDialog(owner ?? rtb.FindForm()) == DialogResult.OK)
                    {
                        if (ofd.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                            rtb.Text = System.IO.File.ReadAllText(ofd.FileName);
                        else
                            rtb.LoadFile(ofd.FileName, RichTextBoxStreamType.RichText);
                    }
                }
            };

            var btnUndo = new ToolStripButton("↶"); btnUndo.ToolTipText = "Undo"; btnUndo.Click += (s1, e1) => { if (rtb.CanUndo) rtb.Undo(); };
            var btnRedo = new ToolStripButton("↷"); btnRedo.ToolTipText = "Redo"; btnRedo.Click += (s1, e1) => { try { rtb.Redo(); } catch { } };

            var btnFind = new ToolStripButton("Find"); btnFind.ToolTipText = "Find Text"; btnFind.Click += (s1, e1) => SimpleFind(rtb, owner);
            var btnReplace = new ToolStripButton("Replace"); btnReplace.ToolTipText = "Find and Replace"; btnReplace.Click += (s1, e1) => SimpleReplace(rtb, owner);

            var btnColumns = new ToolStripButton("Columns"); btnColumns.ToolTipText = "Insert columns using tabs"; btnColumns.Click += (s1, e1) => InsertColumnsDialog(rtb, owner);

            toolStrip.Items.AddRange(new ToolStripItem[]
            {
                btnBold, btnItalic, btnUnderline,
                new ToolStripSeparator(),
                fontFamilyBox, fontSizeBox,
                new ToolStripSeparator(),
                btnColor,
                new ToolStripSeparator(),
                btnAlignLeft, btnAlignCenter, btnAlignRight,
                new ToolStripSeparator(),
                btnBullets, btnIndent, btnOutdent,
                new ToolStripSeparator(),
                btnWordWrap, btnInsertDate, btnColumns,
                new ToolStripSeparator(),
                btnFind, btnReplace,
                new ToolStripSeparator(),
                btnUndo, btnRedo, btnClear,
                new ToolStripSeparator(),
                btnSave, btnLoad
            });

            var parent = rtb.Parent;
            if (parent != null)
            {
                toolStrip.Dock = DockStyle.Top;

                // If RTB is docked Fill, wrap it so the toolstrip can be placed above without changing layout
                if (rtb.Dock == DockStyle.Fill)
                {
                    var wrapper = new Panel();
                    wrapper.Dock = DockStyle.Fill;
                    parent.Controls.Remove(rtb);
                    wrapper.Controls.Add(rtb);
                    rtb.Dock = DockStyle.Fill;
                    parent.Controls.Add(wrapper);
                    parent.Controls.Add(toolStrip);
                    parent.Controls.SetChildIndex(toolStrip, 0);
                    parent.Controls.SetChildIndex(wrapper, 1);
                }
                else
                {
                    parent.Controls.Add(toolStrip);
                    toolStrip.BringToFront();
                    // move RTB down if needed
                    rtb.Top += toolStrip.Height;
                }
            }

            var ctx = new ContextMenuStrip();
            ctx.Items.Add("Cut", null, (s1, e1) => rtb.Cut());
            ctx.Items.Add("Copy", null, (s1, e1) => rtb.Copy());
            ctx.Items.Add("Paste", null, (s1, e1) => rtb.Paste());
            ctx.Items.Add(new ToolStripSeparator());
            ctx.Items.Add("Bold", null, (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Bold, true));
            ctx.Items.Add("Italic", null, (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Italic, true));
            ctx.Items.Add("Underline", null, (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Underline, true));
            ctx.Items.Add("Color...", null, (s1, e1) => { if (owner != null) colorDialog.ShowDialog(owner); else colorDialog.ShowDialog(); ApplyColor(rtb, colorDialog.Color); });
            ctx.Items.Add("Select All", null, (s1, e1) => rtb.SelectAll());
            ctx.Items.Add("Clear Formatting", null, (s1, e1) => ClearFormatting(rtb));
            rtb.ContextMenuStrip = ctx;

            rtb.Disposed += (s1, e1) => { toolStrip.Dispose(); ctx.Dispose(); };

            rtb.SelectionChanged += (s1, e1) =>
            {
                var f = rtb.SelectionFont ?? rtb.Font;
                btnBold.Checked = (f.Style & FontStyle.Bold) == FontStyle.Bold;
                btnItalic.Checked = (f.Style & FontStyle.Italic) == FontStyle.Italic;
                btnUnderline.Checked = (f.Style & FontStyle.Underline) == FontStyle.Underline;

                try { fontFamilyBox.SelectedItem = f.FontFamily.Name; } catch { }
                fontSizeBox.Text = f.Size.ToString("0.##");

                btnBullets.Checked = rtb.SelectionBullet;
                btnWordWrap.Checked = rtb.WordWrap;
            };
        }

        #region Helpers
        private static void ToggleSelectionFontStyle(RichTextBox rtb, FontStyle style, bool enable)
        {
            if (rtb.SelectionLength == 0)
            {
                var cur = rtb.SelectionFont ?? rtb.Font;
                var newStyle = enable ? (cur.Style | style) : (cur.Style & ~style);
                rtb.SelectionFont = new Font(cur.FontFamily, cur.Size, newStyle);
            }
            else
            {
                int selStart = rtb.SelectionStart;
                int selLen = rtb.SelectionLength;
                for (int i = 0; i < selLen; i++)
                {
                    rtb.Select(selStart + i, 1);
                    var cur = rtb.SelectionFont ?? rtb.Font;
                    var newStyle = enable ? (cur.Style | style) : (cur.Style & ~style);
                    rtb.SelectionFont = new Font(cur.FontFamily, cur.Size, newStyle);
                }
                rtb.Select(selStart, selLen);
            }
            rtb.Focus();
        }

        private static void ApplyFontFamily(RichTextBox rtb, string family)
        {
            if (string.IsNullOrEmpty(family)) return;
            var cur = rtb.SelectionFont ?? rtb.Font;
            try { rtb.SelectionFont = new Font(family, cur.Size, cur.Style); } catch { }
            rtb.Focus();
        }

        private static void ApplyFontSize(RichTextBox rtb, float size)
        {
            if (size <= 0) return;
            var cur = rtb.SelectionFont ?? rtb.Font;
            try { rtb.SelectionFont = new Font(cur.FontFamily, size, cur.Style); } catch { }
            rtb.Focus();
        }

        private static void ApplyColor(RichTextBox rtb, Color c)
        {
            rtb.SelectionColor = c;
            rtb.Focus();
        }

        private static void ClearFormatting(RichTextBox rtb)
        {
            var text = rtb.Text;
            var selStart = rtb.SelectionStart;
            rtb.SelectAll();
            rtb.SelectionFont = rtb.Font;
            rtb.SelectionColor = rtb.ForeColor;
            rtb.SelectionBullet = false;
            rtb.SelectionAlignment = HorizontalAlignment.Left;
            rtb.SelectionIndent = 0;
            rtb.DeselectAll();
            rtb.Text = text;
            if (selStart <= rtb.Text.Length) rtb.SelectionStart = selStart;
            rtb.Focus();
        }

        private static void SimpleFind(RichTextBox rtb, Form owner)
        {
            using (var dlg = new Form())
            {
                dlg.Width = 360; dlg.Height = 120; dlg.FormBorderStyle = FormBorderStyle.FixedDialog; dlg.StartPosition = FormStartPosition.CenterParent;
                var tb = new TextBox { Left = 10, Top = 10, Width = 320 };
                var btn = new Button { Text = "Find Next", Left = 10, Top = 40, Width = 100 };
                btn.Click += (s, e) =>
                {
                    var search = tb.Text;
                    if (string.IsNullOrEmpty(search)) return;
                    var idx = rtb.Find(search, rtb.SelectionStart + rtb.SelectionLength, RichTextBoxFinds.None);
                    if (idx >= 0) rtb.Select(idx, search.Length);
                    else MessageBox.Show(dlg, "Text not found.");
                };
                dlg.Controls.Add(tb); dlg.Controls.Add(btn);
                dlg.Text = "Find"; dlg.ShowDialog(owner ?? rtb.FindForm());
            }
        }

        private static void SimpleReplace(RichTextBox rtb, Form owner)
        {
            using (var dlg = new Form())
            {
                dlg.Width = 420; dlg.Height = 160; dlg.FormBorderStyle = FormBorderStyle.FixedDialog; dlg.StartPosition = FormStartPosition.CenterParent;
                var lbl1 = new Label { Text = "Find:", Left = 10, Top = 10 };
                var tbFind = new TextBox { Left = 70, Top = 8, Width = 330 };
                var lbl2 = new Label { Text = "Replace:", Left = 10, Top = 40 };
                var tbReplace = new TextBox { Left = 70, Top = 38, Width = 330 };
                var btnFindNext = new Button { Text = "Find Next", Left = 70, Top = 70, Width = 100 };
                var btnReplace = new Button { Text = "Replace", Left = 180, Top = 70, Width = 100 };
                btnFindNext.Click += (s, e) =>
                {
                    var search = tbFind.Text;
                    if (string.IsNullOrEmpty(search)) return;
                    var idx = rtb.Find(search, rtb.SelectionStart + rtb.SelectionLength, RichTextBoxFinds.None);
                    if (idx >= 0) rtb.Select(idx, search.Length);
                    else MessageBox.Show(dlg, "Text not found.");
                };
                btnReplace.Click += (s, e) =>
                {
                    if (rtb.SelectionLength > 0 && rtb.SelectedText == tbFind.Text)
                    {
                        rtb.SelectedText = tbReplace.Text;
                    }
                };
                dlg.Controls.AddRange(new Control[] { lbl1, tbFind, lbl2, tbReplace, btnFindNext, btnReplace });
                dlg.Text = "Find and Replace"; dlg.ShowDialog(owner ?? rtb.FindForm());
            }
        }

        private static void InsertColumnsDialog(RichTextBox rtb, Form owner)
        {
            using (var dlg = new Form())
            {
                dlg.Width = 360; dlg.Height = 180; dlg.FormBorderStyle = FormBorderStyle.FixedDialog; dlg.StartPosition = FormStartPosition.CenterParent; dlg.Text = "Insert Columns";
                var lblCols = new Label { Text = "Columns (comma separated):", Left = 10, Top = 10, Width = 320 };
                var tbCols = new TextBox { Left = 10, Top = 30, Width = 320, Text = "Column1,Column2,Column3" };
                var lblWidths = new Label { Text = "Widths (px) optional (comma):", Left = 10, Top = 60, Width = 320 };
                var tbWidths = new TextBox { Left = 10, Top = 80, Width = 320, Text = "150,150,150" };
                var btn = new Button { Text = "Insert", Left = 10, Top = 110, Width = 80 };
                btn.Click += (s, e) =>
                {
                    var cols = tbCols.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();
                    if (cols.Length == 0) return;
                    int[] widths = null;
                    try { widths = tbWidths.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => int.Parse(t.Trim())).ToArray(); } catch { widths = null; }
                    int[] tabs = null;
                    if (widths != null && widths.Length >= cols.Length)
                    {
                        tabs = new int[cols.Length];
                        int acc = 0;
                        for (int i = 0; i < cols.Length; i++) { acc += widths[i]; tabs[i] = acc; }
                    }
                    InsertColumns(rtb, cols, tabs);
                    dlg.Close();
                };
                dlg.Controls.AddRange(new Control[] { lblCols, tbCols, lblWidths, tbWidths, btn });
                dlg.ShowDialog(owner ?? rtb.FindForm());
            }
        }

        private static void InsertColumns(RichTextBox rtb, string[] cols, int[] tabStopsPx)
        {
            if (cols == null || cols.Length == 0) return;
            try { if (tabStopsPx != null && tabStopsPx.Length >= cols.Length) rtb.SelectionTabs = tabStopsPx; } catch { }
            var line = string.Join("\t", cols) + "\n";
            rtb.SelectedText = line;
            rtb.Focus();
        }

        #endregion
    }
}
