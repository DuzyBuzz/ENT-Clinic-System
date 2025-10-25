using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public partial class DateRangeForm : Form
    {
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }

        public DateRangeForm()
        {
            InitializeComponent();
            this.Text = "Select Date Range";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Width = 300;
            this.Height = 180;

            Label lblFrom = new Label { Text = "From:", Left = 20, Top = 20, Width = 50 };
            Label lblTo = new Label { Text = "To:", Left = 20, Top = 60, Width = 50 };

            DateTimePicker dtpFrom = new DateTimePicker { Left = 80, Top = 15, Width = 180 };
            DateTimePicker dtpTo = new DateTimePicker { Left = 80, Top = 55, Width = 180 };

            Button btnOk = new Button { Text = "OK", Left = 80, Top = 100, Width = 80 };
            Button btnCancel = new Button { Text = "Cancel", Left = 180, Top = 100, Width = 80 };

            btnOk.Click += (s, e) =>
            {
                FromDate = dtpFrom.Value.Date;
                ToDate = dtpTo.Value.Date;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.Add(lblFrom);
            this.Controls.Add(lblTo);
            this.Controls.Add(dtpFrom);
            this.Controls.Add(dtpTo);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);
        }
    }
}
