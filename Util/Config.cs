using Ini;
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
            this.file = file;
            this.reload();
        }

        public void reload(){
            IniFile ini = new IniFile(this.file);
            this.maxPopups = Util.parseInt(ini.IniReadValue(CATEGORY, "maxPopups"), MAX_POPUPS_DEFAULT);
            this.keepOnTop = Util.parseBool(ini.IniReadValue(CATEGORY, "keepOnTop"), KEEP_ON_TOP_DEFAULT);
            this.tryOneLine = Util.parseBool(ini.IniReadValue(CATEGORY, "tryOneLine"), TRY_ONE_LINE_DEFAULT);
            this.foldersFirst = Util.parseBool(ini.IniReadValue(CATEGORY, "foldersFirst"), FOLDER_FIRST_DEFAULT);
            this.path = ini.IniReadValue(CATEGORY, "path");
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
            this.path = PATH_DEFAULT;
        }

        public void saveAs(string file)
        {
            IniFile ini = new IniFile(file);
            ini.IniWriteValue(CATEGORY, "maxPopups", this.maxPopups.ToString());
            ini.IniWriteValue(CATEGORY, "keepOnTop", this.keepOnTop.ToString());
            ini.IniWriteValue(CATEGORY, "tryOneLine", this.tryOneLine.ToString());
            ini.IniWriteValue(CATEGORY, "foldersFirst", this.foldersFirst.ToString());
            ini.IniWriteValue(CATEGORY, "path", this.path);
        }

        public void OnChanged(){
            if(this.Changed != null){
                this.Changed(this, EventArgs.Empty);
            }
        }
    }
}
