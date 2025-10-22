using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Payments
{
    public partial class PaymentForm : Form
    {
        private BillingInvoiceForm billingForm;
        private InvoiceForm invoiceForm;

        public PaymentForm()
        {
            InitializeComponent();

            // Create instances of existing forms
            billingForm = new BillingInvoiceForm();
            invoiceForm = new InvoiceForm();

            // Embed them into the split container panels
            EmbedFormIntoPanel(billingForm, splitContainerMain.Panel1);
            EmbedFormIntoPanel(invoiceForm, splitContainerMain.Panel2);
        }

        private void EmbedFormIntoPanel(Form form, Control parent)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            parent.Controls.Add(form);
            form.Show();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Ensure embedded forms are disposed
            if (billingForm != null && !billingForm.IsDisposed) billingForm.Close();
            if (invoiceForm != null && !invoiceForm.IsDisposed) invoiceForm.Close();
            base.OnFormClosed(e);
        }
    }
}
