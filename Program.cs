using System;
using System.Windows.Forms;

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

            string folder = Settings.getInstance().path;
            if (folder.Length == 0 && (args == null || args.Length == 0))
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    folder = dialog.SelectedPath;
                    Settings.getInstance().path = folder;
                    Settings.getInstance().save();
                }
                else return;
            }
            else if (folder.Length == 0) folder = args[0];

            Settings.getInstance().reload();

            DockFrame form = new DockFrame(folder);
            WAPI.SetForegroundWindow(form.Handle);
            Application.Run(form);
        }
    }
}
