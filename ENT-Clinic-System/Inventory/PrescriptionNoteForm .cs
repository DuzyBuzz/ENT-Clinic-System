using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ENT_Clinic_System.Inventory
{
    public partial class PrescriptionNoteForm : Form
    {
        public Dictionary<int, string> ItemNotes { get; private set; } = new Dictionary<int, string>();

        public PrescriptionNoteForm(DataGridView dgvSelectedItems)
        {
            InitializeComponent();
            BuildNotesForm(dgvSelectedItems);
        }

        private void BuildNotesForm(DataGridView dgvSelectedItems)
        {
            int y = 10;

            foreach (DataGridViewRow row in dgvSelectedItems.Rows)
            {
                if (row.IsNewRow) continue;

                int itemId = Convert.ToInt32(row.Cells["item_id"].Value);
                string itemName = row.Cells["item_name"].Value.ToString();
                string description = row.Cells["description"].Value.ToString();
                int qty = Convert.ToInt32(row.Cells["quantity"].Value);

                // Label
                Label lbl = new Label
                {
                    Text = $"{itemName} ({description}) x {qty}",
                    AutoSize = true,
                    Location = new Point(10, y),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold)
                };
                this.Controls.Add(lbl);
                y += 25;

                // TextBox for note
                TextBox txtNote = new TextBox
                {
                    Name = $"note_{itemId}",
                    Width = 350,
                    Location = new Point(10, y),
                    Multiline = true,
                    Height = 50
                };
                this.Controls.Add(txtNote);
                y += 60;
            }

            // Submit button
            Button btnSubmit = new Button
            {
                Text = "OK / Print",
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
                if (ctl is TextBox txt && txt.Name.StartsWith("note_"))
                {
                    int itemId = int.Parse(txt.Name.Split('_')[1]);
                    ItemNotes[itemId] = txt.Text.Trim();
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
