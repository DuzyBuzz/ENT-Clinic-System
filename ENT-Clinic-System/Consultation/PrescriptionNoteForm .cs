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

        private void BuildSigForm(DataGridView dgvSelectedItems, DataGridView selectedOtherDGV)
        {
            int y = 10;

            Label headerMain = new Label
            {
                Text = "CLINIC ITEMS",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline),
                Location = new Point(10, y)
            };
            this.Controls.Add(headerMain);
            y += 30;

            // Section 1: Medicines
            foreach (DataGridViewRow row in dgvSelectedItems.Rows)
            {
                if (row.IsNewRow) continue;

                int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                string itemName = $"{row.Cells["generic_name"].Value} {row.Cells["brand_name"].Value}";
                string description = row.Cells["description"].Value.ToString();
                int qty = Convert.ToInt32(row.Cells["quantity"].Value);

                Label lbl = new Label
                {
                    Text = $"{itemName} ({description}) x {qty} - Sig:",
                    AutoSize = true,
                    Location = new Point(10, y),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };
                this.Controls.Add(lbl);
                y += 25;

                TextBox txtSig = new TextBox
                {
                    Name = $"sig_item_{itemId}",
                    Width = 350,
                    Location = new Point(10, y),
                    Multiline = true,
                    Height = 50,
                    Font = new Font("Segoe UI", 9F)
                };
                this.Controls.Add(txtSig);
                y += 60;
            }

            // Section 2: Other Items
            if (selectedOtherDGV.Rows.Count > 0)
            {
                y += 20;
                Label headerOther = new Label
                {
                    Text = "OTHER ITEM SIGS",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline),
                    Location = new Point(10, y)
                };
                this.Controls.Add(headerOther);
                y += 30;

                foreach (DataGridViewRow row in selectedOtherDGV.Rows)
                {
                    if (row.IsNewRow) continue;

                    int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                    string itemName = row.Cells["item_name"].Value.ToString();
                    string description = row.Cells["description"].Value.ToString();
                    string category = row.Cells["category"].Value.ToString();
                    int qty = Convert.ToInt32(row.Cells["quantity"].Value);

                    Label lbl = new Label
                    {
                        Text = $"{itemName} ({category}) - {description} x {qty} - Sig:",
                        AutoSize = true,
                        Location = new Point(10, y),
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                    };
                    this.Controls.Add(lbl);
                    y += 25;

                    TextBox txtSig = new TextBox
                    {
                        Name = $"sig_other_{itemId}",
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

            // OK / Print button
            Button btnSubmit = new Button
            {
                Text = "Save and Print",
                Location = new Point(10, y),
                Width = 120,
                Height = 30
            };
            btnSubmit.Click += BtnSubmit_Click;
            this.Controls.Add(btnSubmit);

            this.ClientSize = new Size(400, Math.Min(y + 50, 600));
            this.AutoScroll = true;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is TextBox txt)
                {
                    if (txt.Name.StartsWith("sig_item_"))
                    {
                        int itemId = int.Parse(txt.Name.Split('_')[2]);
                        ItemSigs[itemId] = txt.Text.Trim();
                    }
                    else if (txt.Name.StartsWith("sig_other_"))
                    {
                        int itemId = int.Parse(txt.Name.Split('_')[2]);
                        OtherItemSigs[itemId] = txt.Text.Trim();
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
