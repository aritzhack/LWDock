﻿using System;
using System.Windows.Forms;
using System.Diagnostics;

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

#if DEBUG
            CHelloWorld.sayHello();

            string message;
            if (args.Length != 0) MessageBox.Show("{\n\t" + String.Join(",\n\t", args) + "\n}", "Arguments", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if(!Settings.getInstance().areDefault()) MessageBox.Show(Settings.getInstance().getSettings(), "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif

            Settings.getInstance().getSettings();
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
