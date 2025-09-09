using ENT_Clinic_Receptionist.CustomUI;
using ENT_Clinic_Receptionist.Helpers;
using ENT_Clinic_System.CustomUI;
using ENT_Clinic_System.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.UserControls
{
    public partial class ConsultationControl : UserControl
    {
        private int _patientId;
        private TextEditorToolStrip editorToolbar;
        private CameraToolStrip cameraToolStrip;
        private FlowLayoutHelper flowHelper;
        public ConsultationControl(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            Debug.WriteLine(patientId);

            LoadPatientLabels(_patientId);

        }

        private void InitializeCamera()
        {
            cameraToolStrip = new CameraToolStrip(imageToolStrip, imageFlowLayoutPanel);

            // Optional: listen if you want
            cameraToolStrip.ImageAdded += (s, bmp) =>
            {
                Debug.WriteLine("New image added to panel.");
            };
        }
        private void ConsultationControl_Load(object sender, EventArgs e)
        {
            ToolStripInitializer();
            EnableDeleteRow();
            InitializeCamera();

        }
        private void LoadPatientLabels(int patientId)
        {
            try
            {
                fullNameLabel.Text = PatientDataHelper.GetPatientValue(patientId, "full_name");
                addressLabel.Text = PatientDataHelper.GetPatientValue(patientId, "address");
                ageLabel.Text = PatientDataHelper.GetPatientValue(patientId, "age");
                sexLabel.Text = PatientDataHelper.GetPatientValue(patientId, "sex");
                civilStatusLabel.Text = PatientDataHelper.GetPatientValue(patientId, "civil_status");
                patientContactNumberLabel.Text = PatientDataHelper.GetPatientValue(patientId, "patient_contact_number");
                emergencyNameLabel.Text = PatientDataHelper.GetPatientValue(patientId, "emergency_name");
                emergencyContactNumberLabel.Text = PatientDataHelper.GetPatientValue(patientId, "emergency_contact_number");
                emergencyRelationshipLabel.Text = PatientDataHelper.GetPatientValue(patientId, "emergency_relationship");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patient info: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Stop camera safely when needed (e.g., button click)
        private void button1_Click(object sender, EventArgs e)
        {
        }
        private void ToolStripInitializer()
        {
            RichTextBoxBulletHelper.EnableAutoBullets(complaintsRichTextBox);
            RichTextBoxBulletHelper.EnableAutoBullets(illnessHistoryRichTextBox);
            RichTextBoxBulletHelper.EnableAutoBullets(diagnosisRichTextBox);
            RichTextBoxBulletHelper.EnableAutoBullets(noteRichTextBox);
        }

        private void EnableDeleteRow()
        {
            DGVDeleteHelper.EnableDeleteRows(earsDataGridView);
            DGVDeleteHelper.EnableDeleteRows(noseDataGridView);
            DGVDeleteHelper.EnableDeleteRows(throatDataGridView);
            DGVDeleteHelper.EnableDeleteRows(recommendationsDataGridView);
        }




        private void openCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void earsDataGridView_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tableLayoutPanel25_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
