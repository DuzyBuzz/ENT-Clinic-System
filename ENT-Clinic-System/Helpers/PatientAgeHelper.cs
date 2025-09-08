using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class PatientAgeHelper
    {
        /// <summary>
        /// Calls the MySQL procedure to update the age column for all patients.
        /// </summary>
        public static void UpdatePatientAges()
        {
            try
            {
                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand("UpdatePatientAges", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update patient ages: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
