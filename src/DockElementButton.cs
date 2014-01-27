using System.Drawing;
using System.Windows.Forms;

namespace LWDock
{
    class DockElementButton : Button
    {
        public DockElementButton(Bitmap icon, Size size, Point location, DockElement parent)
        {
            this.FlatStyle = FlatStyle.Flat;
            this.UseVisualStyleBackColor = false;
            this.TabStop = false;
            this.MouseUp += parent.button_Click;
            this.FlatAppearance.BorderSize = 0;
            this.Image = Util.resize(icon, DockElement.BUTTON_SIZE, DockElement.BUTTON_SIZE);
            this.Size = size;
            this.Location = location;
        }
    }
}
