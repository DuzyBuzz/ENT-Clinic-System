using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ENT_Clinic_System.Helpers;

namespace ENT_Clinic_System.Consultation
{
    public partial class PrescriptionNoteForm : Form
    {
        // Holds SIG values for both clinic items and other items
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

            // 🔹 Header: Clinic Items Section
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
            // SECTION 1: CLINIC ITEMS
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

                string itemDisplay = !string.IsNullOrWhiteSpace(genericName)
                    ? genericName
                    : (!string.IsNullOrWhiteSpace(brandName) ? brandName : description);

                // Label for the item
                Label lbl = new Label
                {
                    Text = $"{itemDisplay} ({strength} {dosage}) x {qty} - Sig:",
                    AutoSize = true,
                    Location = new Point(10, y),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };
                this.Controls.Add(lbl);
                y += 25;

                // ComboBox for SIG (auto-suggest + auto-complete)
                ComboBox comboSig = new ComboBox
                {
                    Name = $"sig_item_{itemId}",
                    Tag = itemId,
                    Width = 350,
                    Location = new Point(10, y),
                    Font = new Font("Segoe UI", 9F),
                    DropDownStyle = ComboBoxStyle.DropDown
                };

                // 🔹 Load smart SIG suggestions
                var suggestions = SigSuggestionHelper.GetSigSuggestions(itemId);
                comboSig.Items.AddRange(suggestions.ToArray());
                comboSig.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboSig.AutoCompleteSource = AutoCompleteSource.ListItems;

                this.Controls.Add(comboSig);
                y += 60;
            }

            // =========================================================
            // SECTION 2: OTHER ITEMS
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

                    ComboBox comboSig = new ComboBox
                    {
                        Name = $"sig_other_{itemId}",
                        Tag = itemId,
                        Width = 350,
                        Location = new Point(10, y),
                        Font = new Font("Segoe UI", 9F),
                        DropDownStyle = ComboBoxStyle.DropDown
                    };

                    // 🔹 Load smart SIG suggestions for OTHER ITEMS
                    var suggestions = SigSuggestionHelper.GetSigSuggestions(itemId);
                    comboSig.Items.AddRange(suggestions.ToArray());
                    comboSig.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboSig.AutoCompleteSource = AutoCompleteSource.ListItems;

                    this.Controls.Add(comboSig);
                    y += 60;
                }
            }

            // =========================================================
            // SAVE BUTTON
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
                if (ctl is ComboBox combo)
                {
                    if (combo.Name.StartsWith("sig_item_"))
                    {
                        int itemId = SafeInt(combo.Tag);
                        ItemSigs[itemId] = combo.Text.Trim();
                    }
                    else if (combo.Name.StartsWith("sig_other_"))
                    {
                        int itemId = SafeInt(combo.Tag);
                        OtherItemSigs[itemId] = combo.Text.Trim();
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
