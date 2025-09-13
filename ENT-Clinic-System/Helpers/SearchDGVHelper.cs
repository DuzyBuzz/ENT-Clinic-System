using ENT_Clinic_System.Helpers;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ENT_Clinic_System.Helpers
{
    public static class SearchHelper
    {
        /// <summary>
        /// Searches a table and updates the DataGridView based on a filter control or a date range.
        /// </summary>
        /// <param name="dgv">The DataGridView to display results.</param>
        /// <param name="tableName">The database table to search.</param>
        /// <param name="columnName">The column to filter by.</param>
        /// <param name="filterControl">The input control: TextBox, ComboBox, DateTimePicker, or null if using date range.</param>
        /// <param name="fromDate">Start date for range (optional).</param>
        /// <param name="toDate">End date for range (optional).</param>
        /// <param name="columns">Optional: columns to select. Defaults to *.</param>
        public static void Search(
            DataGridView dgv,
            string tableName,
            string[] columnNames,           // <-- now an array
            Control filterControl = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string[] columns = null)
        {
            try
            {
                string columnsString = columns != null && columns.Length > 0 ? string.Join(",", columns) : "*";
                string sql = $"SELECT {columnsString} FROM {tableName}";

                string filterValue = "";

                if (filterControl != null)
                {
                    if (filterControl is TextBox tb)
                        filterValue = tb.Text.Trim();
                    else if (filterControl is ComboBox cb)
                        filterValue = cb.Text.Trim();
                    else if (filterControl is DateTimePicker dtp)
                        filterValue = dtp.Value.ToString("yyyy-MM-dd");
                    else
                        throw new ArgumentException("Unsupported control type.");
                }

                // Build WHERE clause
                var conditions = new System.Collections.Generic.List<string>();

                if (!string.IsNullOrEmpty(filterValue) && columnNames.Length > 0)
                {
                    if (filterControl is DateTimePicker)
                    {
                        foreach (var col in columnNames)
                            conditions.Add($"DATE({col}) = @filterValue");
                    }
                    else
                    {
                        var orConditions = new System.Collections.Generic.List<string>();
                        foreach (var col in columnNames)
                            orConditions.Add($"{col} LIKE @filterValue");
                        conditions.Add("(" + string.Join(" OR ", orConditions) + ")");
                    }
                }

                if (fromDate.HasValue)
                    conditions.Add($"DATE({columnNames[0]}) >= @fromDate"); // you can choose which column applies for date range
                if (toDate.HasValue)
                    conditions.Add($"DATE({columnNames[0]}) <= @toDate");

                if (conditions.Count > 0)
                    sql += " WHERE " + string.Join(" AND ", conditions);

                using (var conn = DBConfig.GetConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(filterValue))
                    {
                        if (filterControl is DateTimePicker)
                            cmd.Parameters.AddWithValue("@filterValue", filterValue);
                        else
                            cmd.Parameters.AddWithValue("@filterValue", $"%{filterValue}%");
                    }
                    if (fromDate.HasValue)
                        cmd.Parameters.AddWithValue("@fromDate", fromDate.Value.ToString("yyyy-MM-dd"));
                    if (toDate.HasValue)
                        cmd.Parameters.AddWithValue("@toDate", toDate.Value.ToString("yyyy-MM-dd"));

                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgv.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search failed: " + ex.Message);
            }
        }

    }
}


//## 1️⃣ Search using a TextBox (e.g., patient name)

//```csharp
//private void patientNameTextBox_TextChanged(object sender, EventArgs e)
//{
//    // Search 'patients' table by 'full_name' column
//    SearchHelper.Search(
//        dgv: dgv,
//        tableName: "patients",
//        columnName: "full_name",
//        filterControl: patientNameTextBox,
//        columns: new string[] { "patient_id", "full_name", "age", "gender" }
//    );
//}
//```

//✅ **Behavior:**Partial search on `full_name` as user types.

//---

//## 2️⃣ Search using a ComboBox (e.g., gender)

//```csharp
//private void genderComboBox_SelectedIndexChanged(object sender, EventArgs e)
//{
//    // Search 'patients' table by 'gender' column
//    SearchHelper.Search(
//        dgv: dgv,
//        tableName: "patients",
//        columnName: "gender",
//        filterControl: genderComboBox
//    );
//}
//```

//✅ **Behavior:**Matches the selected gender exactly (partial match still supported with `LIKE`).

//---

//## 3️⃣ Search using a single DateTimePicker (e.g., birthdate)

//```csharp
//private void dobDateTimePicker_ValueChanged(object sender, EventArgs e)
//{
//    // Search 'patients' table by exact birth_date
//    SearchHelper.Search(
//        dgv: dgv,
//        tableName: "patients",
//        columnName: "birth_date",
//        filterControl: dobDateTimePicker,
//        columns: new string[] { "patient_id", "full_name", "birth_date" }
//    );
//}
//```

//✅ **Behavior:**Shows only patients with the exact selected birth date.

//---

//## 4️⃣ Search using From / To DateTimePickers (e.g., birthdate range)

//```csharp
//private void btnSearch_Click(object sender, EventArgs e)
//{
//    // Search patients born between two dates
//    SearchHelper.Search(
//        dgv: dgv,
//        tableName: "patients",
//        columnName: "birth_date",
//        fromDate: fromDateTimePicker.Value,
//        toDate: toDateTimePicker.Value,
//        columns: new string[] { "patient_id", "full_name", "birth_date", "age" }
//    );
//}
//```

//✅ **Behavior:**Shows patients whose `birth_date` falls between `fromDate` and `toDate`.

//---

//## 5️⃣ Combine TextBox + Date Range (optional filter)

//```csharp
//private void btnSearch_Click(object sender, EventArgs e)
//{
//    // Filter by name AND birthdate range
//    SearchHelper.Search(
//        dgv: dgv,
//        tableName: "patients",
//        columnName: "birth_date",
//        filterControl: patientNameTextBox, // TextBox for partial name
//        fromDate: fromDateTimePicker.Value,
//        toDate: toDateTimePicker.Value,
//        columns: new string[] { "patient_id", "full_name", "birth_date", "age" }
//    );
//}

