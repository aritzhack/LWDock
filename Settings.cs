using Ini;
using LWDock.Config;
using System;

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

        public const bool FOLDER_FIRST_DEFAULT = true, TRY_ONE_LINE_DEFAULT = true, KEEP_ON_TOP_DEFAULT = false, SIMPLE_ICONS_DEFAULT = false;
        public const int MAX_POPUPS_DEFAULT = -1, DEFAULT_ICON_QUALITY = 2;
        public const string PATH_DEFAULT = "";

        public bool foldersFirst, tryOneLine, keepOnTop;
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
            this.path = this.config.getValue("path");
            this.saver.writeConfig(this.config);
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
            // this.path = PATH_DEFAULT;
        }

        public void saveAs(ConfigSaver saver)
        {
            this.config.setProperty("maxPopups", this.maxPopups);
            this.config.setProperty("keepOnTop", this.keepOnTop);
            this.config.setProperty("tryOneLine", this.tryOneLine);
            this.config.setProperty("foldersFirst", this.foldersFirst);
            this.config.setProperty("path", this.path);
            this.config.setProperty("iconQuality", this.iconQuality);
            saver.writeConfig(config);
        }

        public void OnChanged(){
            if(this.Changed != null){
                this.Changed(this, EventArgs.Empty);
            }
        }
    }
}
