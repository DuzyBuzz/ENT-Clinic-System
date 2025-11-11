using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.AdmitingOrders
{
    public partial class SetAdmitTemplateForm : Form
    {
        public SetAdmitTemplateForm()
        {
            InitializeComponent();
        }

        private void SetAdmitTemplateForm_Load(object sender, EventArgs e)
        {
            LoadTemplate();
            RichTextBoxFormatterHelper.Attach(richTextBoxTemplate, this);
        }

        // =========================
        // Load template from DB
        // =========================
        private void LoadTemplate()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT template_text FROM admit_template LIMIT 1", conn))
                    {
                        var val = cmd.ExecuteScalar();
                        if (val != null && val != DBNull.Value)
                        {
                            richTextBoxTemplate.Rtf = val.ToString();
                        }
                        else
                        {
                            richTextBoxTemplate.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load template:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // Save template to DB
        // =========================
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string rtf = richTextBoxTemplate.Rtf ?? string.Empty;

                using (var conn = DBConfig.GetConnection())
                {
                    conn.Open();
                    using (var tx = conn.BeginTransaction())
                    {
                        // Delete any existing template
                        using (var del = new MySqlCommand("DELETE FROM admit_template", conn, tx))
                        {
                            del.ExecuteNonQuery();
                        }

                        // Insert new template
                        using (var ins = new MySqlCommand("INSERT INTO admit_template (template_text) VALUES (@rtf)", conn, tx))
                        {
                            ins.Parameters.AddWithValue("@rtf", rtf);
                            ins.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                }

                MessageBox.Show("Template saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save template:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================
        // Clear template
        // =========================
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear the template?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                richTextBoxTemplate.Clear();
            }
        }

        private void richTextBoxTemplate_TextChanged(object sender, EventArgs e)
        {
            // Optional: you can enable/disable Save button here
        }
    }
}
