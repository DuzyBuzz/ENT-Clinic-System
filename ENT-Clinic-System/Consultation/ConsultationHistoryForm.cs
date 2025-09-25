using ENT_Clinic_System.Helpers;
using ENT_Clinic_System.PrintingForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.Consultation
{
    public partial class ConsultationHistoryForm : Form
    {
        private DGVViewHelper viewHelper;
        private int patientID;
        public ConsultationHistoryForm(int patientId)
        {
            this.patientID = patientId;
            InitializeComponent();
        }

        private void printConsultationHistoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (consultationDateDataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a consultation to print.",
                        "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (DataGridViewRow row in consultationDateDataGridView.SelectedRows)
                {
                    if (row.Cells["consultation_id"].Value == null ||
                        row.Cells["patient_id"].Value == null ||
                            row.Cells["consultation_date"].Value == null)
                        continue;

                    int consultationId = Convert.ToInt32(row.Cells["consultation_id"].Value);
                    int patientId = Convert.ToInt32(row.Cells["patient_id"].Value);
                    string consultationDate = Convert.ToString(row.Cells["consultation_date"].Value);

                    // Create the helper
                    PrintTextHistory printer = new PrintTextHistory(patientId, consultationId);
                    string fullName = PatientDataHelper.GetPatientValue(patientId, "full_name");
                    // Use custom MultiPrintPreviewDialog (non-modal, taskbar visible)
                    MultiPrintPreviewDialog previewDialog = new MultiPrintPreviewDialog
                    {
                        Document = printer.Document,
                        StartPosition = FormStartPosition.CenterScreen,
                        ShowInTaskbar = true,
                        Text = $"{fullName} - {consultationDate}",
                        ShowIcon = false,



                    };

                    previewDialog.Show(); // Non-modal: multiple dialogs can be opened
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening consultation record: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printAttachmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (consultationDateDataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a consultation to print.",
                        "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (DataGridViewRow row in consultationDateDataGridView.SelectedRows)
                {
                    if (row.Cells["consultation_id"].Value == null ||
                        row.Cells["patient_id"].Value == null ||
                        row.Cells["consultation_date"].Value == null)
                        continue;

                    int consultationId = Convert.ToInt32(row.Cells["consultation_id"].Value);
                    int patientId = Convert.ToInt32(row.Cells["patient_id"].Value);
                    string consultationDate = Convert.ToString(row.Cells["consultation_date"].Value);

                    // Open the PrintConsultationHistory form and pass the IDs
                    PrintAttachments printForm = new PrintAttachments(consultationId);
                    printForm.Show();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening consultation record: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printMedicalCertificateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (consultationDateDataGridView.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a consultation to print.",
                        "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (DataGridViewRow row in consultationDateDataGridView.SelectedRows)
                {
                    if (row.Cells["consultation_id"].Value == null ||
                        row.Cells["patient_id"].Value == null ||
                        row.Cells["consultation_date"].Value == null)
                        continue;

                    int consultationId = Convert.ToInt32(row.Cells["consultation_id"].Value);
                    int patientId = Convert.ToInt32(row.Cells["patient_id"].Value);

                    // 🔹 Show input dialog before printing
                    using (var inputForm = new PurposeInputForm())
                    {
                        if (inputForm.ShowDialog() == DialogResult.OK)
                        {
                            string requestName = inputForm.PurposeText;

                            // Pass user input to MedicalCertificatePrinter
                            var printer = new MedicalCertificatePrinter(patientId, consultationId, requestName);
                            printer.ShowPreview();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening consultation record: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConsultationHistoryForm_Load(object sender, EventArgs e)
        {
            LoadConsultationDate(patientID);
        }

        private void LoadConsultationDate(int patientID)
        {
            List<string> consultationColumns = new List<string>
            {
                "consultation_id",
                "patient_id",
                "consultation_date",
            };

            viewHelper = new DGVViewHelper(
                consultationDateDataGridView,
                "consultation",
                consultationColumns,
                "patient_id"
            );

            viewHelper.LoadData(patientID);

            // Hide ID and patient ID columns
            if (consultationDateDataGridView.Columns.Contains("consultation_id"))
                consultationDateDataGridView.Columns["consultation_id"].Visible = false;

            if (consultationDateDataGridView.Columns.Contains("patient_id"))
                consultationDateDataGridView.Columns["patient_id"].Visible = false;
        }

        private void createLaboratoryRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LabRequestForm labForm = new LabRequestForm(patientID);
            labForm.ShowDialog();
            this.Close();
        }
    }
}
