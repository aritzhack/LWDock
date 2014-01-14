using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LWDock
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string folder = Config.getInstance().path;
            if (folder.Length == 0 && (args == null || args.Length == 0))
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    folder = dialog.SelectedPath;
                    Config.getInstance().path = folder;
                    Config.getInstance().save();
                }
                else return;
            }
            else if (folder.Length == 0) folder = args[0];

            Config.getInstance().reload();

            DockFrame form = new DockFrame(folder);
            WAPI.SetForegroundWindow(form.Handle);
            Application.Run(form);
        }
    }
}
