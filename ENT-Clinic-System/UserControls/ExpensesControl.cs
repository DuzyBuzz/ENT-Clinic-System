using ENT_Clinic_System.Helpers;
using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.UserControls
{
    public partial class ExpensesControl : UserControl
    {
        private DGVViewCrudHelper dgvHelper;

        public ExpensesControl()
        {
            InitializeComponent();
            InitializeHelper();
        }

        private void InitializeHelper()
        {
            // Bind the DataGridView to the helper
            dgvHelper = new DGVViewCrudHelper(
                dgvExpenses,
                "v_expenses",       // VIEW name for read-only display
                "id",               // Primary key column
                "expenses"          // Base table for updates/deletes
            );

            // Optional: Attach search controls if you have them
            dgvHelper.AttachSearchControls(txtSearch, btnSearch, btnRefresh, new[] { "category", "description" });

            // Optional: Attach date range for filtering
            dgvHelper.AttachDateRangeControls(dtpFrom, dtpTo, btnSearchByDate, "date");

            // Load today's expenses by default
            dgvHelper.LoadToday("date");
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "INSERT INTO `expenses` (`date`, `category`, `description`, `amount`) " +
                             "VALUES (@date, @category, @description, @amount)";

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@date", dtpDate.Value.Date);
                    cmd.Parameters.AddWithValue("@category", txtCategory.Text.Trim());
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@amount", nudAmount.Value);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                // Refresh the grid after adding
                dgvHelper.Refresh();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add expense: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            dgvHelper.Refresh();
        }

        private void ClearInputs()
        {
            txtCategory.Clear();
            txtDescription.Clear();
            nudAmount.Value = 0;
            dtpDate.Value = DateTime.Today;
        }
    }
}
