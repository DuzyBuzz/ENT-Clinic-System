using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace ENT_Clinic_System.Helpers
{
    /// <summary>
    /// RichTextBox formatter helper compatible with .NET Framework4.8.
    /// Usage:
    /// RichTextBoxFormatterHelper.Attach(myRichTextBox, this); // full toolbar
    /// RichTextBoxFormatterHelper.Attach(myRichTextBox, this, compact: true); // compact toolbar
    ///
    /// Adds a ToolStrip with Font and Paragraph features similar to Word's Font/Paragraph groups
    /// using only WinForms / RichTextBox APIs. Some Word features (true justify, exact line spacing
    /// using RTF paragraph flags) are not fully available via the managed RichTextBox without
    /// P/Invoke; this helper implements close approximations using safe managed APIs.
    /// </summary>
    public static class RichTextBoxFormatterHelper
    {
        public static void Attach(RichTextBox rtb, Form owner = null, bool compact = false)
        {
            if (rtb == null) throw new ArgumentNullException(nameof(rtb));

            // Prevent attaching twice
            if (rtb.Tag is string s && s == "RTB_FORMATTER_ATTACHED") return;
            rtb.Tag = "RTB_FORMATTER_ATTACHED";

            var fontDialog = new FontDialog();
            var colorDialog = new ColorDialog();

            var toolStrip = new ToolStrip
            {
                GripStyle = ToolStripGripStyle.Hidden,
                RenderMode = ToolStripRenderMode.System,
                Stretch = true
            };

            // --- Font group ---
            var fontFamilyBox = new ToolStripComboBox { Width =160, DropDownStyle = ComboBoxStyle.DropDownList };
            try
            {
                foreach (var ff in FontFamily.Families)
                    fontFamilyBox.Items.Add(ff.Name);
            }
            catch { }

            fontFamilyBox.SelectedIndexChanged += (s1, e1) =>
            {
                if (fontFamilyBox.SelectedItem != null)
                    ApplyFontFamily(rtb, fontFamilyBox.SelectedItem.ToString());
            };

            var fontSizeBox = new ToolStripComboBox { Width =70, DropDownStyle = ComboBoxStyle.DropDown };
            fontSizeBox.Items.AddRange(new object[] { "8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" });
            fontSizeBox.Leave += (s1, e1) => { if (float.TryParse(fontSizeBox.Text, out float size)) ApplyFontSize(rtb, size); };
            fontSizeBox.KeyDown += (s1, e1) => { if (e1.KeyCode == Keys.Enter && float.TryParse(fontSizeBox.Text, out float size)) ApplyFontSize(rtb, size); };

            var btnBold = new ToolStripButton("B") { CheckOnClick = true, ToolTipText = "Bold (Ctrl+B)" };
            btnBold.Click += (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Bold, btnBold.Checked);

            var btnItalic = new ToolStripButton("I") { CheckOnClick = true, ToolTipText = "Italic (Ctrl+I)" };
            btnItalic.Click += (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Italic, btnItalic.Checked);

            var btnUnderline = new ToolStripButton("U") { CheckOnClick = true, ToolTipText = "Underline (Ctrl+U)" };
            btnUnderline.Click += (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Underline, btnUnderline.Checked);

            var btnStrike = new ToolStripButton("abc") { CheckOnClick = true, ToolTipText = "Strikethrough" };
            btnStrike.Click += (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Strikeout, btnStrike.Checked);

            var btnSup = new ToolStripButton("xⁿ") { CheckOnClick = true, ToolTipText = "Superscript" };
            var btnSub = new ToolStripButton("xₙ") { CheckOnClick = true, ToolTipText = "Subscript" };

            btnSup.Click += (s1, e1) =>
            {
                if (btnSup.Checked)
                {
                    btnSub.Checked = false;
                    ApplySuperScript(rtb, true);
                }
                else ApplySuperScript(rtb, false);
            };

            btnSub.Click += (s1, e1) =>
            {
                if (btnSub.Checked)
                {
                    btnSup.Checked = false;
                    ApplySubScript(rtb, true);
                }
                else ApplySubScript(rtb, false);
            };

            var btnFontColor = new ToolStripButton("A") { ToolTipText = "Font Color" };
            btnFontColor.Click += (s1, e1) =>
            {
                if (owner != null) colorDialog.ShowDialog(owner); else colorDialog.ShowDialog();
                ApplyColor(rtb, colorDialog.Color);
            };

            var btnHighlight = new ToolStripButton("▣") { ToolTipText = "Highlight" };
            btnHighlight.Click += (s1, e1) =>
            {
                if (owner != null) colorDialog.ShowDialog(owner); else colorDialog.ShowDialog();
                ApplyHighlight(rtb, colorDialog.Color);
            };

            var caseDrop = new ToolStripDropDownButton("Aa") { ToolTipText = "Change Case" };
            caseDrop.DropDownItems.Add("UPPERCASE", null, (s1, e1) => ChangeCase(rtb, CaseTransform.Upper));
            caseDrop.DropDownItems.Add("lowercase", null, (s1, e1) => ChangeCase(rtb, CaseTransform.Lower));
            caseDrop.DropDownItems.Add("Sentence case", null, (s1, e1) => ChangeCase(rtb, CaseTransform.Sentence));
            caseDrop.DropDownItems.Add("Title Case", null, (s1, e1) => ChangeCase(rtb, CaseTransform.Title));
            caseDrop.DropDownItems.Add("tOGGLE cASE", null, (s1, e1) => ChangeCase(rtb, CaseTransform.Toggle));

            var btnClear = new ToolStripButton("Clear") { ToolTipText = "Clear Formatting" };
            btnClear.Click += (s1, e1) => ClearFormatting(rtb);

            // --- Paragraph group ---
            var btnBullets = new ToolStripButton("•") { CheckOnClick = true, ToolTipText = "Bullets" };
            btnBullets.Click += (s1, e1) => rtb.SelectionBullet = btnBullets.Checked;

            var btnNumbering = new ToolStripButton("1.") { ToolTipText = "Numbering (simple)" };
            btnNumbering.Click += (s1, e1) => ToggleNumbering(rtb);

            var btnIndent = new ToolStripButton("→") { ToolTipText = "Increase Indent" };
            btnIndent.Click += (s1, e1) => rtb.SelectionIndent +=20;

            var btnOutdent = new ToolStripButton("←") { ToolTipText = "Decrease Indent" };
            btnOutdent.Click += (s1, e1) => rtb.SelectionIndent = Math.Max(0, rtb.SelectionIndent -20);

            var btnAlignLeft = new ToolStripButton("L") { ToolTipText = "Align Left" };
            btnAlignLeft.Click += (s1, e1) => rtb.SelectionAlignment = HorizontalAlignment.Left;

            var btnAlignCenter = new ToolStripButton("C") { ToolTipText = "Center" };
            btnAlignCenter.Click += (s1, e1) => rtb.SelectionAlignment = HorizontalAlignment.Center;

            var btnAlignRight = new ToolStripButton("R") { ToolTipText = "Align Right" };
            btnAlignRight.Click += (s1, e1) => rtb.SelectionAlignment = HorizontalAlignment.Right;

            var spacingBox = new ToolStripComboBox { Width =90, DropDownStyle = ComboBoxStyle.DropDownList };
            spacingBox.Items.AddRange(new object[] { "Single", "1.5", "Double" });
            spacingBox.SelectedIndexChanged += (s1, e1) =>
            {
                var sel = spacingBox.SelectedItem?.ToString();
                if (sel == "Single") AdjustLineSpacingApprox(rtb, LineSpacing.Single);
                else if (sel == "1.5") AdjustLineSpacingApprox(rtb, LineSpacing.OnePointFive);
                else if (sel == "Double") AdjustLineSpacingApprox(rtb, LineSpacing.Double);
            };
            spacingBox.Text = "Single";

            // Build toolbar layout
            if (compact)
            {
                toolStrip.Items.AddRange(new ToolStripItem[]
                {
                    fontSizeBox,
                    new ToolStripSeparator(),
                    btnBold, btnItalic, btnUnderline,
                    new ToolStripSeparator(),
                    btnBullets, btnNumbering,
                    new ToolStripSeparator(),
                    btnAlignLeft, btnAlignCenter, btnAlignRight,
                    new ToolStripSeparator(),
                    btnSaveLoadPlaceholder()
                });
            }
            else
            {
                toolStrip.Items.AddRange(new ToolStripItem[]
                {
                    fontFamilyBox, fontSizeBox,
                    new ToolStripSeparator(),
                    btnBold, btnItalic, btnUnderline, btnStrike,
                    new ToolStripSeparator(),
                    btnSup, btnSub, caseDrop,
                    new ToolStripSeparator(),
                    btnFontColor, btnHighlight,
                    new ToolStripSeparator(),
                    btnClear,
                    new ToolStripSeparator(),
                    btnBullets, btnNumbering, btnIndent, btnOutdent,
                    new ToolStripSeparator(),
                    btnAlignLeft, btnAlignCenter, btnAlignRight,
                    new ToolStripSeparator(),
                    spacingBox,
                    new ToolStripSeparator(),
                    btnSaveLoadPlaceholder()
                });
            }

            // Attach toolstrip above RichTextBox
            var parent = rtb.Parent;
            if (parent != null)
            {
                toolStrip.Dock = DockStyle.Top;
                // Create wrapper panel that will host the ToolStrip (top) and the RichTextBox (fill)
                var wrapper = new Panel();
                wrapper.SuspendLayout();

                if (rtb.Dock == DockStyle.Fill)
                // Preserve some layout properties from the RTB
                wrapper.Anchor = rtb.Anchor;
                wrapper.Location = rtb.Location;
                wrapper.Size = rtb.Size;
                wrapper.Margin = rtb.Margin;

                // Prepare toolStrip inside the wrapper
                toolStrip.Dock = DockStyle.Bottom;

                // Special-case TableLayoutPanel to keep RTB in the same cell
                var tlp = parent as TableLayoutPanel;
                if (tlp != null)
                {
                    var wrapper2 = new Panel { Dock = DockStyle.Fill };
                    parent.Controls.Remove(rtb);
                    var pos = tlp.GetPositionFromControl(rtb);
                    int colSpan = tlp.GetColumnSpan(rtb);
                    int rowSpan = tlp.GetRowSpan(rtb);

                    tlp.SuspendLayout();
                    tlp.Controls.Remove(rtb);

                    // Make wrapper fill the table cell
                    wrapper2.Dock = DockStyle.Fill;
                    tlp.Controls.Add(wrapper2, pos.Column, pos.Row);
                    try { tlp.SetColumnSpan(wrapper2, colSpan); tlp.SetRowSpan(wrapper2, rowSpan); } catch { }

                    wrapper2.Controls.Add(toolStrip);
                    wrapper2.Controls.Add(rtb);
                    rtb.Dock = DockStyle.Fill;
                    parent.Controls.Add(wrapper2);
                    parent.Controls.Add(toolStrip);
                    parent.Controls.SetChildIndex(toolStrip,0);
                    parent.Controls.SetChildIndex(wrapper2,1);

                    tlp.ResumeLayout();
                }
                else
                {
                    parent.Controls.Add(toolStrip);
                    toolStrip.BringToFront();
                    rtb.Top += toolStrip.Height;
                    // General parent: replace RTB with wrapper at the same z-order index
                    int originalIndex = parent.Controls.GetChildIndex(rtb);
                    parent.Controls.Remove(rtb);

                    parent.Controls.Add(wrapper);
                    try { parent.Controls.SetChildIndex(wrapper, originalIndex); } catch { }

                    wrapper.Controls.Add(toolStrip);
                    wrapper.Controls.Add(rtb);
                    rtb.Dock = DockStyle.Fill;
                }

                wrapper.ResumeLayout();
            }

            // Context menu
            var ctx = new ContextMenuStrip();
            ctx.Items.Add("Cut", null, (s1, e1) => rtb.Cut());
            ctx.Items.Add("Copy", null, (s1, e1) => rtb.Copy());
            ctx.Items.Add("Paste", null, (s1, e1) => rtb.Paste());
            ctx.Items.Add(new ToolStripSeparator());
            ctx.Items.Add("Bold", null, (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Bold, true));
            ctx.Items.Add("Italic", null, (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Italic, true));
            ctx.Items.Add("Underline", null, (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Underline, true));
            ctx.Items.Add("Strikethrough", null, (s1, e1) => ToggleSelectionFontStyle(rtb, FontStyle.Strikeout, true));
            ctx.Items.Add("Font Color...", null, (s1, e1) => { if (owner != null) colorDialog.ShowDialog(owner); else colorDialog.ShowDialog(); ApplyColor(rtb, colorDialog.Color); });
            ctx.Items.Add("Highlight...", null, (s1, e1) => { if (owner != null) colorDialog.ShowDialog(owner); else colorDialog.ShowDialog(); ApplyHighlight(rtb, colorDialog.Color); });
            ctx.Items.Add(new ToolStripSeparator());
            ctx.Items.Add("Clear Formatting", null, (s1, e1) => ClearFormatting(rtb));
            rtb.ContextMenuStrip = ctx;

            rtb.Disposed += (s1, e1) => { try { toolStrip.Dispose(); ctx.Dispose(); } catch { } };

            rtb.SelectionChanged += (s1, e1) =>
            {
                var f = rtb.SelectionFont ?? rtb.Font;
                btnBold.Checked = (f.Style & FontStyle.Bold) == FontStyle.Bold;
                btnItalic.Checked = (f.Style & FontStyle.Italic) == FontStyle.Italic;
                btnUnderline.Checked = (f.Style & FontStyle.Underline) == FontStyle.Underline;
                btnStrike.Checked = (f.Style & FontStyle.Strikeout) == FontStyle.Strikeout;

                try { fontFamilyBox.SelectedItem = f.FontFamily.Name; } catch { }
                fontSizeBox.Text = f.Size.ToString("0.##");

                btnBullets.Checked = rtb.SelectionBullet;
                btnSup.Checked = rtb.SelectionCharOffset >0;
                btnSub.Checked = rtb.SelectionCharOffset <0;
            };

            ToolStripItem btnSaveLoadPlaceholder()
            {
                var save = new ToolStripButton("Save") { ToolTipText = "Save to RTF" };
                save.Click += (s1, e1) =>
                {
                    using (var sfd = new SaveFileDialog { Filter = "Rich Text Format (*.rtf)|*.rtf|Text File (*.txt)|*.txt" })
                    {
                        if (sfd.ShowDialog(owner ?? rtb.FindForm()) == DialogResult.OK)
                        {
                            if (sfd.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                                System.IO.File.WriteAllText(sfd.FileName, rtb.Text);
                            else
                                rtb.SaveFile(sfd.FileName, RichTextBoxStreamType.RichText);
                        }
                    }
                };
                var open = new ToolStripButton("Open") { ToolTipText = "Load RTF / Text / Word" };
                open.Click += (s1, e1) =>
                {
                    using (var ofd = new OpenFileDialog
                    {
                        Filter =
                            "Word Document (*.docx;*.doc)|*.docx;*.doc|" +
                            "Text File (*.txt)|*.txt|" +
                            "Rich Text Format (*.rtf)|*.rtf|" +
                            "All Files (*.*)|*.*"
                    })
                    {
                        if (ofd.ShowDialog(owner ?? rtb.FindForm()) == DialogResult.OK)
                        {
                            var file = ofd.FileName;
                            try
                            {
                                if (file.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                                {
                                    rtb.Text = System.IO.File.ReadAllText(file);
                                }
                                else if (file.EndsWith(".rtf", StringComparison.OrdinalIgnoreCase))
                                {
                                    rtb.LoadFile(file, RichTextBoxStreamType.RichText);
                                }
                                else if (file.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Try Word COM automation to convert to RTF and load
                                    if (!TryLoadWordViaCom(rtb, file))
                                    {
                                        // Fallback to plain text if Word isn't available
                                        rtb.Text = System.IO.File.ReadAllText(file);
                                    }
                                }
                                else if (file.EndsWith(".doc", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Try Word COM automation to convert to RTF and load
                                    if (!TryLoadWordViaCom(rtb, file))
                                    {
                                        MessageBox.Show(owner ?? rtb.FindForm(), "Opening .doc files requires Microsoft Word to be installed.", "Unsupported format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    // Unknown - attempt to load as text
                                    rtb.Text = System.IO.File.ReadAllText(file);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(owner ?? rtb.FindForm(), "Failed to open file: " + ex.Message, "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };

                var container = new ToolStripDropDownButton("File");
                container.DropDownItems.Add(new ToolStripMenuItem("Save", null, (s2, e2) => save.PerformClick()));
                container.DropDownItems.Add(new ToolStripMenuItem("Open", null, (s2, e2) => open.PerformClick()));
                return container;

                // Local helpers
                bool TryLoadWordViaCom(RichTextBox rtbTarget, string path)
                {
                    try
                    {
                        var prog = Type.GetTypeFromProgID("Word.Application");
                        if (prog == null) return false;
                        dynamic word = Activator.CreateInstance(prog);
                        word.Visible = false;
                        dynamic doc = word.Documents.Open(path, ReadOnly: true);
                        var tmp = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + ".rtf");
                        //6 = wdFormatRTF
                        doc.SaveAs(tmp,6);
                        doc.Close(false);
                        word.Quit(false);
                        rtbTarget.LoadFile(tmp, RichTextBoxStreamType.RichText);
                        try { System.IO.File.Delete(tmp); } catch { }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        #region Helpers & Implementations

        private enum CaseTransform { Upper, Lower, Sentence, Title, Toggle }
        private enum LineSpacing { Single, OnePointFive, Double }

        private static void ToggleSelectionFontStyle(RichTextBox rtb, FontStyle style, bool enable)
        {
            if (rtb.SelectionLength ==0)
            {
                var cur = rtb.SelectionFont ?? rtb.Font;
                var newStyle = enable ? (cur.Style | style) : (cur.Style & ~style);
                rtb.SelectionFont = new Font(cur.FontFamily, cur.Size, newStyle);
            }
            else
            {
                int selStart = rtb.SelectionStart;
                int selLen = rtb.SelectionLength;
                for (int i =0; i < selLen; i++)
                {
                    rtb.Select(selStart + i,1);
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
            if (size <=0) return;
            var cur = rtb.SelectionFont ?? rtb.Font;
            try { rtb.SelectionFont = new Font(cur.FontFamily, size, cur.Style); } catch { }
            rtb.Focus();
        }

        private static void ApplyColor(RichTextBox rtb, Color c)
        {
            rtb.SelectionColor = c;
            rtb.Focus();
        }

        private static void ApplyHighlight(RichTextBox rtb, Color c)
        {
            try { rtb.SelectionBackColor = c; } catch { }
            rtb.Focus();
        }

        private static void ClearFormatting(RichTextBox rtb)
        {
            var text = rtb.Text;
            var selStart = rtb.SelectionStart;
            rtb.SelectAll();
            rtb.SelectionFont = rtb.Font;
            rtb.SelectionColor = rtb.ForeColor;
            try { rtb.SelectionBackColor = rtb.BackColor; } catch { }
            rtb.SelectionBullet = false;
            rtb.SelectionAlignment = HorizontalAlignment.Left;
            rtb.SelectionIndent =0;
            rtb.DeselectAll();
            rtb.Text = text;
            if (selStart <= rtb.Text.Length) rtb.SelectionStart = selStart;
            rtb.Focus();
        }

        private static void ApplySuperScript(RichTextBox rtb, bool enable)
        {
            if (rtb.SelectionLength ==0)
            {
                var cur = rtb.SelectionFont ?? rtb.Font;
                if (enable)
                {
                    rtb.SelectionFont = new Font(cur.FontFamily, Math.Max(6f, cur.Size *0.75f), cur.Style);
                    rtb.SelectionCharOffset = Math.Max(1, (int)(cur.Size /2));
                }
                else
                {
                    rtb.SelectionFont = new Font(cur.FontFamily, cur.Size /0.75f, cur.Style);
                    rtb.SelectionCharOffset =0;
                }
            }
            else
            {
                int start = rtb.SelectionStart;
                int len = rtb.SelectionLength;
                for (int i =0; i < len; i++)
                {
                    rtb.Select(start + i,1);
                    var f = rtb.SelectionFont ?? rtb.Font;
                    if (enable)
                    {
                        rtb.SelectionFont = new Font(f.FontFamily, Math.Max(6f, f.Size *0.75f), f.Style);
                        rtb.SelectionCharOffset = Math.Max(1, (int)(f.Size /2));
                    }
                    else
                    {
                        rtb.SelectionFont = new Font(f.FontFamily, Math.Min(72f, f.Size /0.75f), f.Style);
                        rtb.SelectionCharOffset =0;
                    }
                }
                rtb.Select(start, len);
            }
            rtb.Focus();
        }

        private static void ApplySubScript(RichTextBox rtb, bool enable)
        {
            if (rtb.SelectionLength ==0)
            {
                var cur = rtb.SelectionFont ?? rtb.Font;
                if (enable)
                {
                    rtb.SelectionFont = new Font(cur.FontFamily, Math.Max(6f, cur.Size *0.75f), cur.Style);
                    rtb.SelectionCharOffset = -Math.Max(1, (int)(cur.Size /3));
                }
                else
                {
                    rtb.SelectionFont = new Font(cur.FontFamily, cur.Size /0.75f, cur.Style);
                    rtb.SelectionCharOffset =0;
                }
            }
            else
            {
                int start = rtb.SelectionStart;
                int len = rtb.SelectionLength;
                for (int i =0; i < len; i++)
                {
                    rtb.Select(start + i,1);
                    var f = rtb.SelectionFont ?? rtb.Font;
                    if (enable)
                    {
                        rtb.SelectionFont = new Font(f.FontFamily, Math.Max(6f, f.Size *0.75f), f.Style);
                        rtb.SelectionCharOffset = -Math.Max(1, (int)(f.Size /3));
                    }
                    else
                    {
                        rtb.SelectionFont = new Font(f.FontFamily, Math.Min(72f, f.Size /0.75f), f.Style);
                        rtb.SelectionCharOffset =0;
                    }
                }
                rtb.Select(start, len);
            }
            rtb.Focus();
        }

        private static void ToggleNumbering(RichTextBox rtb)
        {
            if (rtb.SelectionLength ==0)
            {
                int lineIndex = rtb.GetLineFromCharIndex(rtb.SelectionStart);
                int lineStart = rtb.GetFirstCharIndexFromLine(lineIndex);
                rtb.Select(lineStart,0);
                rtb.SelectedText = "1. ";
                rtb.SelectionStart = lineStart +3;
            }
            else
            {
                var selText = rtb.SelectedText.Replace("\r\n", "\n");
                var lines = selText.Split(new[] { '\n' }, StringSplitOptions.None);
                bool alreadyNumbered = lines.All(l => l.TrimStart().Length ==0 || System.Text.RegularExpressions.Regex.IsMatch(l.TrimStart(), "^\\d+\\.\\s+"));
                if (alreadyNumbered)
                {
                    for (int i =0; i < lines.Length; i++)
                        lines[i] = System.Text.RegularExpressions.Regex.Replace(lines[i], @"^\s*\d+\.\s+", string.Empty);
                }
                else
                {
                    for (int i =0; i < lines.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(lines[i])) continue;
                        lines[i] = (i +1).ToString() + ". " + lines[i];
                    }
                }
                rtb.SelectedText = string.Join("\r\n", lines);
            }
            rtb.Focus();
        }

        private static void ChangeCase(RichTextBox rtb, CaseTransform transform)
        {
            if (rtb.SelectionLength ==0) return;
            string text = rtb.SelectedText;
            string changed = text;
            switch (transform)
            {
                case CaseTransform.Upper: changed = text.ToUpperInvariant(); break;
                case CaseTransform.Lower: changed = text.ToLowerInvariant(); break;
                case CaseTransform.Title: changed = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLowerInvariant()); break;
                case CaseTransform.Sentence:
                {
                    var sentences = System.Text.RegularExpressions.Regex.Split(text, @"(?<=[\.\!\?])\s+");
                    for (int i =0; i < sentences.Length; i++)
                    {
                        var s = sentences[i].Trim();
                        if (s.Length >0)
                            sentences[i] = char.ToUpperInvariant(s[0]) + (s.Length >1 ? s.Substring(1).ToLowerInvariant() : "");
                    }
                    changed = string.Join(" ", sentences);
                    break;
                }
                case CaseTransform.Toggle:
                    changed = string.Concat(text.Select(c => char.IsUpper(c) ? char.ToLowerInvariant(c) : char.ToUpperInvariant(c)));
                    break;
            }
            int selStart = rtb.SelectionStart;
            rtb.SelectedText = changed;
            rtb.Select(selStart, changed.Length);
            rtb.Focus();
        }

        // Approximate line spacing by ensuring blank lines between paragraphs.
        private static void AdjustLineSpacingApprox(RichTextBox rtb, LineSpacing spacing)
        {
            int selStart = rtb.SelectionStart;
            int selLen = rtb.SelectionLength;
            string text = rtb.SelectedText;
            if (string.IsNullOrEmpty(text))
            {
                text = rtb.Text;
                selStart =0;
                selLen = text.Length;
            }

            var paragraphs = text.Replace("\r\n", "\n").Split(new[] { '\n' }, StringSplitOptions.None);
            for (int i =0; i < paragraphs.Length; i++) paragraphs[i] = paragraphs[i].TrimEnd('\r', '\n');

            string separator = "\r\n";
            if (spacing == LineSpacing.OnePointFive) separator = "\r\n\r\n";
            else if (spacing == LineSpacing.Double) separator = "\r\n\r\n";
            string newText = string.Join(separator, paragraphs);

            if (rtb.SelectionLength ==0)
            {
                rtb.Text = newText;
            }
            else
            {
                rtb.SelectedText = newText;
                rtb.Select(selStart, newText.Length);
            }
            rtb.Focus();
        }

        #endregion
    }
}
