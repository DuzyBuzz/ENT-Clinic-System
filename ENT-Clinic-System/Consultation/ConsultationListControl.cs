using ENT_Clinic_System.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System.Consultation
{
    public partial class ConsultationListControl : UserControl
    {
        private SimpleDGVCRUDHelper consultationHelper;
        private ContextMenuStrip dgvContextMenu;

        public ConsultationListControl()
        {
            InitializeComponent();
        }

        #region Load
        private void ConsultationListControl_Load(object sender, EventArgs e)
        {
            // Specify read-only columns (IDs, Patient ID, Patient Name)
            List<string> readonlyCols = new List<string> { "consultation_id", "patient_id", "patient_name", "consultation_date", "age"};

            AutoCompleteHelper.SetupAutoComplete(
                searchPatientNameTextBox,
                "patients",
                new List<string> { "full_name" }
            );

            // Initialize helper
            consultationHelper = new SimpleDGVCRUDHelper(
                dgv: consultationDataGridView,
                tableName: "consultation",
                primaryKeyColumn: "consultation_id",
                readonlyColumns: readonlyCols
            );

            consultationHelper.SetCustomSelectQuery(@"
    SELECT 
        c.consultation_id,
        c.patient_id,
        c.consultation_date,
        p.full_name AS patient_name,
        c.chief_complaint,
        c.history,
        c.ear_exam,
        c.nose_exam,
        c.throat_exam,
        c.others_exam,
        c.diagnosis,
        c.recommendations,
        c.notes,
        c.follow_up_date,
        c.age,
        c.doctor_id,
        u.full_name AS doctor_name  -- replace doctor_id with actual name
    FROM consultation c
    INNER JOIN patients p ON c.patient_id = p.patient_id
    LEFT JOIN user u ON c.doctor_name = u.user_id  -- join using doctor_name column which actually stores the id
    ORDER BY c.consultation_date DESC
");


            consultationHelper.SetPageInfoLabel(pageLabel);
            consultationHelper.LoadData();

            InitializeContextMenu();
            RenameHeaders();
            DGVColumnHeaderFilterHelper.Attach(consultationDataGridView);
        }

        private void RenameHeaders()
        {
            var headers = new Dictionary<string, string>
    {
        {"consultation_id", "ID"},
        {"patient_id", "Patient ID"},
        {"consultation_date", "Consultation Date"},
        {"patient_name", "Patient Name"},
        {"chief_complaint", "Chief Complaint"},
        {"history", "Recent Illness"},
        {"ear_exam", "Ear Exam"},
        {"nose_exam", "Nose Exam"},
        {"throat_exam", "Throat Exam"},
        {"diagnosis", "Diagnosis"},
        {"recommendations", "Recommendations"},
        {"notes", "Notes"},
        {"follow_up_date", "Follow-Up Date"},
        {"age", "Age"},
        {"doctor_id", "Doctor ID"},
        {"doctor_name", "Doctor Name"},
        {"others_exam", "Other Exams"}
    };

            foreach (DataGridViewColumn col in consultationDataGridView.Columns)
            {
                if (headers.ContainsKey(col.Name))
                    col.HeaderText = headers[col.Name];

                // Hide ID columns from user
                if (col.Name == "consultation_id" || col.Name == "patient_id" || col.Name == "doctor_id")
                    col.Visible = false;
                // Hide ID columns from user
                if (col.Name == "patient_name" || col.Name == "consultation_date")
                    col.ReadOnly = true;
            }
        }

        #endregion

        #region Context Menu
        private void InitializeContextMenu()
        {
            dgvContextMenu = new ContextMenuStrip();
            var deleteItem = new ToolStripMenuItem("Delete Consultation");
            deleteItem.ForeColor = Color.Red;
            deleteItem.Click += DeleteItem_Click;

            dgvContextMenu.Items.Add(deleteItem);

            consultationDataGridView.CellMouseDown += ConsultationDataGridView_CellMouseDown;
        }

        private void ConsultationDataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                consultationDataGridView.ClearSelection();
                consultationDataGridView.Rows[e.RowIndex].Selected = true;
                dgvContextMenu.Show(Cursor.Position);
            }
        }

        private void DeleteItem_Click(object sender, EventArgs e)
        {
            if (consultationDataGridView.SelectedRows.Count > 0)
            {
                var row = consultationDataGridView.SelectedRows[0];
                var id = row.Cells["consultation_id"].Value;

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this consultation?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        consultationHelper.DeleteRow(id);
                        consultationHelper.Refresh();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Delete failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #region Refresh
        private void refreshPatientsButton_Click(object sender, EventArgs e)
        {
            consultationHelper.Refresh();
        }
        #endregion

        #region Pagination
        private void nextButton_Click(object sender, EventArgs e)
        {
            consultationHelper.NextPage();
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            consultationHelper.PreviousPage();
        }
        #endregion

        private void searchPatientNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string filterText = searchPatientNameTextBox.Text.Trim().Replace("'", "''"); // escape quotes
            if (consultationDataGridView.DataSource == null) return;

            DataTable dt = consultationDataGridView.DataSource as DataTable;
            if (dt == null) return;

            if (string.IsNullOrEmpty(filterText))
            {
                // Show all rows
                dt.DefaultView.RowFilter = "";
            }
            else
            {
                // Filter rows where patient_name contains the search text
                dt.DefaultView.RowFilter = $"patient_name LIKE '%{filterText}%'";
            }
        }
    }
}
