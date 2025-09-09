using ENT_Clinic_Receptionist.CustomUI;
using ENT_Clinic_Receptionist.Helpers;
using ENT_Clinic_System.CustomUI;
using ENT_Clinic_System.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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



        }

        private void InitializeCamera()
        {
            // Initialize the camera toolbar
            cameraToolStrip = new CameraToolStrip(imageToolStrip, imageFlowLayoutPanel);

            flowHelper = new FlowLayoutHelper(imageFlowLayoutPanel);
        }

        private void ConsultationControl_Load(object sender, EventArgs e)
        {
            ToolStripInitializer();
            EnableDeleteRow();
            InitializeCamera();

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
    }
}
