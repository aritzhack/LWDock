using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

namespace LWDock
{
    public abstract class DockElementContainerFrame : BorderlessFrame
    {

        protected Size squareAmount;
        protected List<DockElement> elements;
        protected DockElement currPopupElement;
        protected bool initialized = false;
        protected readonly bool isPopup;
        public int maxNesting;

        public DockElementContainerFrame(string baseFolder, int maxNesting, bool popup)
            : this(new List<string>(Directory.EnumerateFileSystemEntries(baseFolder)), maxNesting, popup)
        {

        }

        public DockElementContainerFrame(List<string> paths, int maxNesting, bool popup)
        {
            this.maxNesting = maxNesting;
            this.isPopup = popup;
            this.init(paths);
        }

        protected virtual void init(string baseFolder)
        {
            this.init(new List<string>(Directory.EnumerateFileSystemEntries(baseFolder)));
        }

        protected virtual void init(List<string> paths)
        {
            if (this.initialized) this.Visible = false;
            DockElementContainerFrame.cleanList(ref paths);

            this.Controls.Clear();
            this.squareAmount = Util.getSquarest(paths.Count, false, this.isPopup);
            if (!this.isPopup) this.squareAmount = Util.improveSquares(this.squareAmount);

            this.elements = new List<DockElement>(squareAmount.Width * squareAmount.Height);

            for (int y = 0; y < squareAmount.Height; y++)
            {
                for (int x = 0; x < squareAmount.Width; x++)
                {
                    if (x + y * squareAmount.Width >= paths.Count)
                    {
                        y = squareAmount.Height;
                        break;
                    }
                    string path = paths.ElementAt(x + y * squareAmount.Width);

                    DockElement element = new DockElement(this, path, x, y, maxNesting);
                    this.Controls.Add(element.button);
                    this.Controls.Add(element.label);
                    element.postInit();
                    this.elements.Add(element);
                }
            }

            this.Size = DockElement.getFinalSize(this.squareAmount);
            if(this.initialized) this.Visible = true;
        }

        public static void cleanList(ref List<string> paths)
        {
            List<string> ret = new List<string>();
            List<string> files = new List<string>();
            foreach (string path in paths)
            {
                try
                {
                    if(Directory.Exists(path))
                    {
                        Directory.EnumerateFileSystemEntries(path);
                        ret.Add(path);
                    }
                    else if (File.Exists(path) && Settings.getInstance().foldersFirst) files.Add(path);
                    else ret.Add(path);
                }
                catch { continue; }
                
            }
            paths.Clear();
            paths.AddRange(ret);
            paths.AddRange(files);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.initialized = true;
            this.BackColor = Color.LightGray;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            foreach (DockElement element in this.elements)
            {
                element.closeChild();
            }
        }

        public virtual void close()
        {
        }

        public void OnPopupOpening(DockElement popupElement)
        {
            if (this.currPopupElement != null && this.currPopupElement != popupElement)
            {
                this.currPopupElement.closePopup();
            }

            this.currPopupElement = popupElement;

        }
    }
}
