using System.Windows.Forms;

namespace ENT_Clinic_Receptionist.Helpers
{
    public static class DGVDeleteHelper
    {
        /// <summary>
        /// Enables delete key functionality for a DataGridView.
        /// Selected rows will be removed when Delete is pressed.
        /// </summary>
        /// <param name="dgv">The DataGridView to enable delete on.</param>
        public static void EnableDeleteRows(DataGridView dgv)
        {
            if (dgv == null) return;

            dgv.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Delete)
                {
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        if (!row.IsNewRow) // Ignore the "new row" placeholder
                        {
                            dgv.Rows.Remove(row);
                        }
                    }
                }
            };
        }
    }
}
