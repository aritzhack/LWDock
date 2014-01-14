using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LWDock
{
    class DockElementButton : Button
    {
        public DockElementButton(Icon icon, Size size, Point location, DockElement parent)
        {
            this.FlatStyle = FlatStyle.Flat;
            this.TabStop = false;
            this.FlatAppearance.BorderSize = 0;
            this.Image = Util.scale(icon.ToBitmap(), 1.3f);
            this.Size = size;
            this.Location = location;
            this.MouseUp += parent.button_Click;
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.Transparent;
            this.UseVisualStyleBackColor = false;
        }
    }
}
