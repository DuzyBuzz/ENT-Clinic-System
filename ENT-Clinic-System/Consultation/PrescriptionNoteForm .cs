using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Consultation
{
    public partial class PrescriptionNoteForm : Form
    {
        // Holds Sig instructions for both medicines and other items
        public Dictionary<int, string> ItemSigs { get; private set; } = new Dictionary<int, string>();
        public Dictionary<int, string> OtherItemSigs { get; private set; } = new Dictionary<int, string>();

        public PrescriptionNoteForm(DataGridView dgvSelectedItems, DataGridView selectedOtherDGV)
        {
            InitializeComponent();
            BuildSigForm(dgvSelectedItems, selectedOtherDGV);
        }

        // =========================================================
        // BUILD DYNAMIC SIG FORM
        // =========================================================
        private void BuildSigForm(DataGridView dgvSelectedItems, DataGridView selectedOtherDGV)
        {
            int y = 10;

            // 🔹 Section Header for Main Clinic Items
            Label headerMain = new Label
            {
                Text = "CLINIC ITEMS",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline),
                Location = new Point(10, y)
            };
            this.Controls.Add(headerMain);
            y += 30;

            // =========================================================
            // SECTION 1: MEDICINES
            // =========================================================
            foreach (DataGridViewRow row in dgvSelectedItems.Rows)
            {
                if (row.IsNewRow) continue;

                int itemId = SafeInt(row.Cells["item_id"].Value);
                string genericName = SafeString(row.Cells["generic_name"].Value);
                string brandName = SafeString(row.Cells["brand_name"].Value);
                string strength = SafeString(row.Cells["strength"].Value);
                string dosage = SafeString(row.Cells["dosage"].Value);
                string description = SafeString(row.Cells["description"].Value);
                int qty = SafeInt(row.Cells["quantity"].Value);

                // 🧠 Determine display name
                string itemDisplay = !string.IsNullOrWhiteSpace(genericName)
                    ? genericName
                    : (!string.IsNullOrWhiteSpace(brandName) ? brandName : description);

                Label lbl = new Label
                {
                    Text = $"{itemDisplay} ({strength} {dosage}) x {qty} - Sig:",
                    AutoSize = true,
                    Location = new Point(10, y),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };
                this.Controls.Add(lbl);
                y += 25;

                // Textbox for Sig
                TextBox txtSig = new TextBox
                {
                    Name = $"sig_item_{itemId}",
                    Tag = itemId,
                    Width = 350,
                    Location = new Point(10, y),
                    Multiline = true,
                    Height = 50,
                    Font = new Font("Segoe UI", 9F)
                };
                this.Controls.Add(txtSig);
                y += 60;
            }

            // =========================================================
            // SECTION 2: OTHER ITEMS (for non-medicine items)
            // =========================================================
            if (selectedOtherDGV.Rows.Count > 0)
            {
                y += 20;

                Label headerOther = new Label
                {
                    Text = "OTHER ITEMS",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline),
                    Location = new Point(10, y)
                };
                this.Controls.Add(headerOther);
                y += 30;

                foreach (DataGridViewRow row in selectedOtherDGV.Rows)
                {
                    if (row.IsNewRow) continue;

                    int itemId = SafeInt(row.Cells["item_id"].Value);
                    string genericName = SafeString(row.Cells["generic_name"].Value);
                    string brandName = SafeString(row.Cells["brand_name"].Value);
                    string strength = SafeString(row.Cells["strength"].Value);
                    string dosage = SafeString(row.Cells["dosage"].Value);
                    string category = SafeString(row.Cells["category"].Value);
                    string description = SafeString(row.Cells["description"].Value);
                    int qty = SafeInt(row.Cells["quantity"].Value);

                    // 🧠 Display name logic
                    string itemDisplay = !string.IsNullOrWhiteSpace(genericName)
                        ? genericName
                        : (!string.IsNullOrWhiteSpace(brandName) ? brandName : "(Unnamed Item)");

                    Label lbl = new Label
                    {
                        Text = $"{itemDisplay} ({strength} {dosage}) [{category}] - {description} x {qty} - Sig:",
                        AutoSize = true,
                        Location = new Point(10, y),
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                    };
                    this.Controls.Add(lbl);
                    y += 25;

                    TextBox txtSig = new TextBox
                    {
                        Name = $"sig_other_{itemId}",
                        Tag = itemId,
                        Width = 350,
                        Location = new Point(10, y),
                        Multiline = true,
                        Height = 50,
                        Font = new Font("Segoe UI", 9F)
                    };
                    this.Controls.Add(txtSig);
                    y += 60;
                }
            }

            // =========================================================
            // SAVE AND PRINT BUTTON
            // =========================================================
            Button btnSubmit = new Button
            {
                Text = "Save and Print",
                Location = new Point(10, y),
                Width = 120,
                Height = 30
            };
            btnSubmit.Click += BtnSubmit_Click;
            this.Controls.Add(btnSubmit);

            // Enable scrolling for long lists
            this.ClientSize = new Size(400, Math.Min(y + 50, 600));
            this.AutoScroll = true;
        }

        // =========================================================
        // EVENT: SAVE AND CLOSE
        // =========================================================
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is TextBox txt)
                {
                    if (txt.Name.StartsWith("sig_item_"))
                    {
                        int itemId = SafeInt(txt.Tag);
                        ItemSigs[itemId] = txt.Text.Trim();
                    }
                    else if (txt.Name.StartsWith("sig_other_"))
                    {
                        int itemId = SafeInt(txt.Tag);
                        OtherItemSigs[itemId] = txt.Text.Trim();
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // =========================================================
        // HELPER METHODS
        // =========================================================
        private string SafeString(object value)
        {
            return value == null || value == DBNull.Value ? string.Empty : value.ToString().Trim();
        }

        private int SafeInt(object value)
        {
            try
            {
                return value == null || value == DBNull.Value ? 0 : Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }
    }
}
