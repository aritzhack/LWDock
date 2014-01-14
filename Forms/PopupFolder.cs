using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LWDock
{
    public partial class PopupFolder : DockElementContainerFrame
    {
        private DockElement parentContainer;

        public PopupFolder(DockElement parent, List<string> paths, int maxNesting) : base(paths, maxNesting)
        {
            this.parentContainer = parent;
            this.InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.init(paths);
            this.FormClosed += this.closing;
        }

        protected override void init(List<string> paths)
        {
 	        base.init(paths);
            this.Size = DockElement.getFinalSize(this.squareAmount);
            this.Size = new Size(this.Size.Width + (this.Width - this.ClientSize.Width) / 2, this.Size.Height + (this.Height - this.ClientSize.Height));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Location = new Point(this.parentContainer.frame.DesktopLocation.X + this.parentContainer.button.Location.X, this.parentContainer.frame.Location.Y - this.Height);
        }

        public void closing(object sender, EventArgs args){
            this.parentContainer.onChildClosed();
        }

        public override void close()
        {
            base.close();
            this.Close();
            this.parentContainer.frame.close();
        }

        protected override bool isPopup()
        {
            return true;
        }
    }
}
