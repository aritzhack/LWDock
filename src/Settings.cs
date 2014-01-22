using Ini;
using LWDock.Config;
using System;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Reflection;

namespace LWDock
{

    public delegate void ConfigChangedListener(object sender, EventArgs args);

    public class Settings
    {
        private IniSaver saver;
        private Config.Config config;

        private const string CATEGORY = "General";

        public event ConfigChangedListener Changed;

        private static Settings INSTANCE;

        public static Settings getInstance()
        {
            if (INSTANCE == null) INSTANCE = new Settings(@".\config.ini");
            return INSTANCE;
        }

        public const bool FOLDER_FIRST_DEFAULT = true, TRY_ONE_LINE_DEFAULT = true, KEEP_ON_TOP_DEFAULT = false, SIMPLE_ICONS_DEFAULT = false, RUN_WITH_WINDOWS_DEFAULT = false;
        public const int MAX_POPUPS_DEFAULT = -1, DEFAULT_ICON_QUALITY = 2;
        public const string PATH_DEFAULT = "";

        public bool foldersFirst, tryOneLine, keepOnTop, runWithWindows;
        public int maxPopups, iconQuality;
        public string path;

        private readonly string file;

        private Settings(string file)
        {
            this.saver = new IniSaver(file);
            this.file = file;
            this.reload();
        }

        public void reload(){
            IniFile ini = new IniFile(this.file);
            this.config = this.saver.readFile();
            this.maxPopups = Util.parseInt(this.config.getValue("maxPopups"), MAX_POPUPS_DEFAULT);
            this.iconQuality = Util.parseInt(this.config.getValue("iconQuality"), DEFAULT_ICON_QUALITY);
            this.keepOnTop = Util.parseBool(this.config.getValue("keepOnTop"), KEEP_ON_TOP_DEFAULT);
            this.tryOneLine = Util.parseBool(this.config.getValue("tryOneLine"), TRY_ONE_LINE_DEFAULT);
            this.foldersFirst = Util.parseBool(this.config.getValue("foldersFirst"), FOLDER_FIRST_DEFAULT);
            this.runWithWindows = Util.parseBool(this.config.getValue("runWithWindows"), RUN_WITH_WINDOWS_DEFAULT);
            this.path = this.config.getValue("path");
            this.save();
        }

        public void save(){
            this.saveAs(this.saver);
        }

        public void resetDefaults()
        {
            this.maxPopups = MAX_POPUPS_DEFAULT;
            this.keepOnTop = KEEP_ON_TOP_DEFAULT;
            this.tryOneLine = TRY_ONE_LINE_DEFAULT;
            this.foldersFirst = FOLDER_FIRST_DEFAULT;
            this.iconQuality = DEFAULT_ICON_QUALITY;
            this.runWithWindows = RUN_WITH_WINDOWS_DEFAULT;
            // this.path = PATH_DEFAULT;
        }

        public void saveAs(ConfigSaver saver)
        {
            this.config.setProperty("maxPopups", this.maxPopups);
            this.config.setProperty("keepOnTop", this.keepOnTop);
            this.config.setProperty("tryOneLine", this.tryOneLine);
            this.config.setProperty("foldersFirst", this.foldersFirst);
            this.config.setProperty("runWithWindows", this.runWithWindows);
            this.config.setProperty("path", this.path);
            this.config.setProperty("iconQuality", this.iconQuality);
            this.OnChanged();
            saver.writeConfig(config);
        }

        public void OnChanged(){
            if(this.Changed != null){
                this.Changed(this, EventArgs.Empty);
            }

            RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            string key = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string value = Assembly.GetEntryAssembly().Location;

            if (this.runWithWindows)
            {
                int i = 0;
                string key2 = key + i;
                while(rk.GetValue(key2) != null && !rk.GetValue(key2).Equals(value))
                {
                    key2 = key + (++i);
                }
                rk.SetValue(key2, value);
            }
            else
            {
                int i = 0;
                string key2 = key + i;
                while (rk.GetValue(key2) != null && !rk.GetValue(key2).Equals(value))
                {
                    key2 = key + (++i);
                }
                rk.DeleteValue(key2, false);
            }
        }
    }
}
