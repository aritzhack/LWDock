using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LWDock.Forms;
using System.Drawing.Drawing2D;

namespace LWDock
{
    public partial class DockFrame : DockElementContainerFrame
    {
        private string folder;
        private int showTimeout, maxX, centerX, yPos;
        private Timer timer;
        public bool alwaysOnTop;

        public DockFrame(String folder) : base(folder, Settings.getInstance().maxPopups, false)
        {
            InitializeComponent();
            Settings.getInstance().path = folder;
            this.folder = folder;
            this.init();
            Settings.getInstance().Changed += this.setttingsUpdated;
        }

        private static IEnumerable<string> getSubFileFolders(String folder)
        {
            return Directory.GetFiles(folder).Union(Directory.GetDirectories(folder));
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (WAPI.getCursorPos().Y > Screen.PrimaryScreen.Bounds.Height - 5)
            {
                if (showTimeout < 25) showTimeout++;
                else
                {
                    showTimeout = 0;
                    WAPI.SetForegroundWindow(this.Handle);
                    this.setForegrounds();
                }
            }
            else showTimeout = 0;
        }

        private void setForegrounds()
        {
            DockElement e = this.currPopupElement;
            while (e != null && e.popup != null)
            {
                WAPI.SetForegroundWindow(e.popup.Handle);
                e = e.popup.currPopupElement;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(e.X + this.Location.X, e.Y + this.Location.Y);
            }
        }

        private void init()
        {
            base.init(this.folder);
            if (this.timer != null) this.timer.Stop();
            this.timer = new Timer();
            this.timer.Interval = 1;
            this.timer.Tick += new EventHandler(timer_Tick);
            this.timer.Start();
            this.updatePositions();
            this.Location = new Point(centerX, yPos);
            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            const int circleSize = 50;
            path.AddEllipse(0, 0, circleSize, circleSize);
            path.AddEllipse(this.Width - circleSize, 0, circleSize, circleSize);

            path.AddRectangle(new Rectangle(circleSize / 2, 0, this.Width - circleSize, this.Height));
            path.AddRectangle(new Rectangle(0, circleSize / 2, this.Width, this.Height - circleSize / 2));

            this.Region = new Region(path);
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.init();
        }

        private void chooseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.folder = dialog.SelectedPath;
                Settings.getInstance().path = this.folder;
                Settings.getInstance().save();
                this.init();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.init();
        }

        private void move()
        {
            int x = this.Location.X;

            if (Math.Abs(centerX - x) < 30) x = centerX;
            else if (maxX - x < 20) x = maxX;
            else if (x < 20) x = 0;
            else x = Math.Min(Math.Max(this.Location.X, 0), maxX);

            this.Location = new Point(x, this.yPos);
        }

        protected override void OnMove(EventArgs e)
        {
            this.move();

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.updatePositions();
        }

        private void updatePositions()
        {
            this.maxX = Screen.PrimaryScreen.WorkingArea.Width - this.Size.Width;
            this.centerX = Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Size.Width / 2;
            this.yPos = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
        }

        protected override void WndProc(ref Message m)
        {
            
            if (m.Msg == WAPI.WM_MOVING)
            {
                WAPI.RECTAPI rc = (WAPI.RECTAPI)Marshal.PtrToStructure(m.LParam, typeof(WAPI.RECTAPI));
                Screen scr = Screen.PrimaryScreen;

                Rectangle rect = Rectangle.FromLTRB(rc.left, rc.top, rc.right, rc.bottom);

                rect.X = Math.Min(Math.Max(rect.X, 0), maxX);

                rc.left = rect.Left;
                rc.right = rect.Right;
                rc.bottom = scr.WorkingArea.Bottom;
                rc.top = scr.WorkingArea.Bottom - this.Height;

                Marshal.StructureToPtr(rc, m.LParam, false);
            }
            base.WndProc(ref m);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm();
            form.ShowDialog(this);
        }

        private void setttingsUpdated(object sender, EventArgs args)
        {
            this.TopMost = Settings.getInstance().keepOnTop;
            this.maxNesting = Settings.getInstance().maxPopups;
            this.init();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Settings.getInstance().save();
        }

        public override void OnButtonRun()
        {
            base.OnButtonRun();
            // Do nothing
        }
    }
}
