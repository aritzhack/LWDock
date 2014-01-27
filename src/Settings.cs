using Ini;
using LWDock.Config;
using System;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Reflection;
using System.Text;
using System.IO;

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
            if (INSTANCE == null) INSTANCE = new Settings(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini"));
            return INSTANCE;
        }

        public const bool FOLDER_FIRST_DEFAULT = true, TRY_ONE_LINE_DEFAULT = true, KEEP_ON_TOP_DEFAULT = false, SIMPLE_ICONS_DEFAULT = false, RUN_WITH_WINDOWS_DEFAULT = false, PRELOAD_POPUPS_DEFAULT = false;
        public const int MAX_POPUPS_DEFAULT = -1, ICON_QUALITY_DEFAULT = 2;
        public static readonly string PATH_DEFAULT = "";

        public bool foldersFirst, tryOneLine, keepOnTop, runWithWindows, preloadPopups;
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
            this.iconQuality = Util.parseInt(this.config.getValue("iconQuality"), ICON_QUALITY_DEFAULT);
            this.keepOnTop = Util.parseBool(this.config.getValue("keepOnTop"), KEEP_ON_TOP_DEFAULT);
            this.tryOneLine = Util.parseBool(this.config.getValue("tryOneLine"), TRY_ONE_LINE_DEFAULT);
            this.foldersFirst = Util.parseBool(this.config.getValue("foldersFirst"), FOLDER_FIRST_DEFAULT);
            this.runWithWindows = Util.parseBool(this.config.getValue("runWithWindows"), RUN_WITH_WINDOWS_DEFAULT);
            this.preloadPopups = Util.parseBool(this.config.getValue("preloadPopups"), PRELOAD_POPUPS_DEFAULT);
            this.path = this.config.getValue("path");
            if (this.path == null || this.path.Length == 0) this.path = PATH_DEFAULT;
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
            this.iconQuality = ICON_QUALITY_DEFAULT;
            this.runWithWindows = RUN_WITH_WINDOWS_DEFAULT;
            this.preloadPopups = PRELOAD_POPUPS_DEFAULT;
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
            this.config.setProperty("preloadPopups", this.preloadPopups);
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

            int i = 0;
            string key2 = key + i;
            while (rk.GetValue(key2) != null && !rk.GetValue(key2).Equals(value)) 
                key2 = key + (++i);

            if (this.runWithWindows) rk.SetValue(key2, value);
            else rk.DeleteValue(key2, false);
        }

        public string getSettings()
        {
            StringBuilder ret = new StringBuilder("Settings:\n").Append(Path.GetFullPath(this.saver.getConfigFile())).Append("\n{\n");
            
            foreach(String key in this.config.getProperties().Keys)
            {
                ret.Append("\t").Append(key).Append(" = ").Append(this.config.getProperty(key).value).Append("\n");
            }
            ret.Append("}");
            return ret.ToString();
        }

        public bool areDefault()
        {
            return 
                this.maxPopups == MAX_POPUPS_DEFAULT &&
                this.path == PATH_DEFAULT &&
                this.runWithWindows == RUN_WITH_WINDOWS_DEFAULT &&
                this.tryOneLine == TRY_ONE_LINE_DEFAULT &&
                this.keepOnTop == KEEP_ON_TOP_DEFAULT &&
                this.foldersFirst == FOLDER_FIRST_DEFAULT &&
                this.preloadPopups == PRELOAD_POPUPS_DEFAULT;
        }
    }
}
