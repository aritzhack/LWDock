using System.Windows.Forms;

namespace LWDock
{
    public class BorderlessFrame : Form
    {

        public BorderlessFrame()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.MouseDown += this.Form1_MouseDown;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WAPI.dragWindow(this.Handle);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WAPI.WS_EX_TOOLWINDOW;
                return cp;
            }
        }
    }
}
