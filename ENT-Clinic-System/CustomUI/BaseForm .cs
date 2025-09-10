using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENT_Clinic_System.CustomUI
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            try
            {
                string iconPath = System.IO.Path.Combine(Application.StartupPath, "assets", "icon", "ent_logo.ico");
                this.Icon = new Icon(iconPath);
            }
            catch
            {
                this.Icon = SystemIcons.Application; // fallback
            }
        }
    }
}
