using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;

namespace LWDock
{
    public abstract class DockElementContainerFrame : BorderlessFrame
    {

        protected Size squareAmount;
        protected List<DockElement> elements;
        public int maxNesting;

        public DockElementContainerFrame()
        {

        }

        public DockElementContainerFrame(string baseFolder, int maxNesting)
        {
            this.maxNesting = maxNesting;
            this.init(new List<string>(Directory.EnumerateFileSystemEntries(baseFolder)));
        }

        public DockElementContainerFrame(List<string> paths, int maxNesting)
        {
            this.maxNesting = maxNesting;
            this.init(paths);
        }

        protected virtual void init(string baseFolder)
        {
            this.init(new List<string>(Directory.EnumerateFileSystemEntries(baseFolder)));
        }

        protected virtual void init(List<string> paths)
        {
            DockElementContainerFrame.cleanList(ref paths);

            this.Controls.Clear();
            this.squareAmount = Util.getSquarest(paths.Count, false, this.isPopup());
            this.squareAmount = Util.improveSquares(this.squareAmount);

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
        }

        public static void cleanList(ref List<string> paths)
        {
            List<string> ret = new List<string>();
            foreach (string path in paths)
            {
                try
                {
                    if(Directory.Exists(path)) Directory.EnumerateFileSystemEntries(path);
                    ret.Add(path);
                }
                catch { continue; }
                
            }
            paths.Clear();
            paths.AddRange(ret);
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

        protected abstract bool isPopup();
    }
}
