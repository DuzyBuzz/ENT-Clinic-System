using System;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.Payments
{
    public partial class PaymentsControl : Form
    {
        public PaymentsControl()
        {
            InitializeComponent();
            LoadFormsIntoPanels();
        }

        /// <summary>
        /// Load Billing and Dispensing forms inside their respective panels.
        /// </summary>
        private void LoadFormsIntoPanels()
        {
            // 🔹 Load BillingInvoiceForm into billingPanel
            LoadFormIntoPanel(new BillingInvoiceForm(), billingPanel);

            // 🔹 Load DispensingForm into dispensingPanel
            LoadFormIntoPanel(new InvoiceForm(), dispensingPanel);
        }
        private void LoadFormIntoPanel(Form form, Panel targetPanel)
        {
            if (form == null || targetPanel == null)
                return;

            // Clear previous content to prevent overlap
            targetPanel.Controls.Clear();

            // Setup form for embedding
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;  // fills the entire panel
            form.AutoScroll = true;

            // Add to panel and show
            targetPanel.Controls.Add(form);
            form.Show();
        }
        private void dispensingPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void billingPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
