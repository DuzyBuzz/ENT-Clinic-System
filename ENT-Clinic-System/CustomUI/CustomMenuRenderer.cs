using System.Drawing;
using System.Windows.Forms;

namespace ENT_Clinic_System
{
    internal class CustomMenuRenderer : ToolStripProfessionalRenderer
    {
        private Color hoverBackColor = Color.Olive; // hover background
        private Color hoverForeColor = Color.Black;                 // hover text
        private Color defaultBackColor = Color.White;               // default background
        private Color defaultForeColor = Color.Black;               // default text

        public CustomMenuRenderer() : base(new ProfessionalColorTable())
        {
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected) // hover state
            {
                e.Graphics.FillRectangle(new SolidBrush(hoverBackColor), e.Item.ContentRectangle);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(defaultBackColor), e.Item.ContentRectangle);
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = e.Item.Selected ? hoverForeColor : defaultForeColor;
            base.OnRenderItemText(e);
        }
    }
}
