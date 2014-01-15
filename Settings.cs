using Ini;
using LWDock.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public const bool FOLDER_FIRST_DEFAULT = true, TRY_ONE_LINE_DEFAULT = true, KEEP_ON_TOP_DEFAULT = false;
        public const int MAX_POPUPS_DEFAULT = -1;
        public const string PATH_DEFAULT = "";

        public bool foldersFirst, tryOneLine, keepOnTop;
        public int maxPopups;
        public string path;

        private readonly string file;

        private Settings(string file)
        {
            this.saver = new IniSaver(file);
            
            this.reload();
        }

        public void reload(){
            IniFile ini = new IniFile(this.file);
            this.config = this.saver.readFile();
            this.maxPopups = Util.parseInt(this.config.getValue("maxPopups"), MAX_POPUPS_DEFAULT);
            this.keepOnTop = Util.parseBool(this.config.getValue("keepOnTop"), KEEP_ON_TOP_DEFAULT);
            this.tryOneLine = Util.parseBool(this.config.getValue("tryOneLine"), TRY_ONE_LINE_DEFAULT);
            this.foldersFirst = Util.parseBool(this.config.getValue("foldersFirst"), FOLDER_FIRST_DEFAULT);
            this.path = this.config.getValue("path");
            this.saver.writeConfig(this.config);
            this.save();
        }

        public void save(){
            this.saveAs(this.file);
        }

        public void resetDefaults()
        {
            this.maxPopups = MAX_POPUPS_DEFAULT;
            this.keepOnTop = KEEP_ON_TOP_DEFAULT;
            this.tryOneLine = TRY_ONE_LINE_DEFAULT;
            this.foldersFirst = FOLDER_FIRST_DEFAULT;
            // this.path = PATH_DEFAULT;
        }

        public void saveAs(string file)
        {
            this.config.setProperty("maxPopups", this.maxPopups);
            this.config.setProperty("keepOnTop", this.keepOnTop);
            this.config.setProperty("tryOneLine", this.tryOneLine);
            this.config.setProperty("foldersFirst", this.foldersFirst);
            this.config.setProperty("path", this.path);
            this.saver.writeConfig(config);
        }

        public void OnChanged(){
            if(this.Changed != null){
                this.Changed(this, EventArgs.Empty);
            }
        }
    }
}
