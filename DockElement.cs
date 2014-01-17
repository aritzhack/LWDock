using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LWDock
{
    public class DockElement
    {
        public readonly Icon icon;
        public readonly string path;
        public Button button;
        public Label label;
        private readonly bool isNonEmptyFolder;
        private PopupFolder popup;
        public DockElementContainerFrame frame;
        private int x, y;

        public const int BUTTON_SIZE = 52;
        public const int HMARGIN_BETWEEN_BUTTONS = 20;
        public const int VMARGIN_BETWEEN_BUTTONS = 10;
        public const int HBUTTON_SIZE = BUTTON_SIZE + HMARGIN_BETWEEN_BUTTONS;
        public const int TOP_BUTTON_MARGIN = 10;
        public const int VBUTTON_SIZE = BUTTON_SIZE + VMARGIN_BETWEEN_BUTTONS + LABEL_HEIGHT;

        public const int LABEL_HEIGHT = 23;
        public int maxNesting;

        public DockElement(DockElementContainerFrame frame, string path, int x, int y, int maxNesting)
        {
            this.maxNesting = maxNesting;
            this.frame = frame;
            this.x = x;
            this.y = y;
            if (!File.Exists(path) && !Directory.Exists(path)) throw new ArgumentException("File or directory " + path + " does not exist!");

            this.isNonEmptyFolder = Directory.Exists(path) && !File.Exists(path) && Directory.EnumerateFileSystemEntries(path).Any();

            this.path = path;

            Size size = new Size(BUTTON_SIZE, BUTTON_SIZE);
            Point location = new Point(x * HBUTTON_SIZE + HMARGIN_BETWEEN_BUTTONS, TOP_BUTTON_MARGIN + y * VBUTTON_SIZE);

            this.button = new DockElementButton(Util.getIcon(path), size, location, this);

            if (File.Exists(path) && File.GetAttributes(path).HasFlag(FileAttributes.Hidden)) this.button.ForeColor = Color.FromArgb(100, Color.Red);
            else if ((new DirectoryInfo(this.path).Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) this.button.ForeColor = Color.FromArgb(100, Color.Red);

            this.label = new Label();
            this.label.MaximumSize = new Size(BUTTON_SIZE + HMARGIN_BETWEEN_BUTTONS, LABEL_HEIGHT);
            this.label.AutoSize = true;
            this.label.Text = Path.GetFileNameWithoutExtension(path);
            if (this.label.Text == null || this.label.Text.Equals("")) this.label.Text = Path.GetFileName(path);
            this.label.TextAlign = ContentAlignment.MiddleCenter;;

            string filename = Path.GetFileName(path);
            
            ToolTip tp = new ToolTip();
            tp.SetToolTip(this.button, filename);
            tp.SetToolTip(this.label, filename);

            //if (index != 1) return;
            this.label.BackColor = Color.Transparent;
        }

        public void postInit()
        {
            int centerX = this.button.Location.X + this.button.Size.Width / 2;
            int x = centerX - this.label.Size.Width / 2;

            this.label.Location = new Point(x, this.button.Location.Y + BUTTON_SIZE);
        }


        public void button_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

            }
            else if (!this.isNonEmptyFolder || e.Button == MouseButtons.Middle || this.maxNesting == 0)
            {
                this.openFileFolder();
            }
            else if (this.popup == null || !this.popup.Visible)
            {
                this.openPopup();
            }
            else
            {
                this.closePopup();
            }
        }

        private void openPopup()
        {
            this.popup = new PopupFolder(this, new List<string>(Directory.EnumerateFileSystemEntries(path)), this.maxNesting-1);
            this.popup.Show();
            this.button.BackColor = Color.Red;
        }

        public void closePopup()
        {
            if (this.popup == null) return;
            this.popup.Close();
            this.popup = null;
            this.button.BackColor = Color.Transparent;
        }

        private void openFileFolder()
        {
            System.Diagnostics.Process.Start(this.path);
            if (!(this.frame is DockFrame)) this.frame.close();
        }

        public void closeChild()
        {
            if (this.popup != null) this.popup.Close();
        }

        public void onChildClosed() {
            this.popup = null;
            this.button.BackColor = Color.Transparent;
            this.frame.Focus();
        }

        public static Size getFinalSize(Size squareAmount)
        {
            return new Size(squareAmount.Width * DockElement.HBUTTON_SIZE + DockElement.HMARGIN_BETWEEN_BUTTONS, (DockElement.VBUTTON_SIZE) * squareAmount.Height + DockElement.TOP_BUTTON_MARGIN);
        }
    }
}
