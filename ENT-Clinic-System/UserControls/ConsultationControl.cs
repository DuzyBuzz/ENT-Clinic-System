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
        private int _patientId; // store the patient_id passed in
        public ConsultationControl(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            Debug.WriteLine(patientId);
        }
    }
}
