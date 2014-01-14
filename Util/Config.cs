using Ini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LWDock
{

    public delegate void ConfigChangedListener(object sender, EventArgs args);

    public class Config
    {

        private const string CATEGORY = "General";

        public event ConfigChangedListener Changed;

        private static Config INSTANCE;

        public static Config getInstance()
        {
            if (INSTANCE == null) INSTANCE = new Config(@".\config.ini");
            return INSTANCE;
        }

        private int _maxPopups;
        private bool _keepOnTop;
        private string _path;
        private bool _tryOneLine;

        public bool tryOneLine
        {
            get
            {
                return this._tryOneLine;
            }
            set
            {
                this._tryOneLine = value;
                this.OnChanged();
            }
        }

        public int maxPopups
        {
            get
            {
                return this._maxPopups;
            }
            set {
                this._maxPopups = value;
                this.OnChanged();
            }
            
        }

        public bool keepOnTop
        {
            get
            {
                return this._keepOnTop;
            }
            set
            {
                this._keepOnTop = value;
                this.OnChanged();
            }
            
        }
        public string path
        {
            get
            {
                return this._path; ;
            }
            set
            {
                this._path = value;
                this.OnChanged();
            }
        }

        private readonly string file;

        private Config(string file)
        {
            this.file = file;
            this.reload();
        }

        public void reload(){
            IniFile ini = new IniFile(this.file);
            this._maxPopups = Util.parseInt(ini.IniReadValue(CATEGORY, "maxPopups"), -1);
            this._keepOnTop = Util.parseBool(ini.IniReadValue(CATEGORY, "keepOnTop"), false);
            this._tryOneLine = Util.parseBool(ini.IniReadValue(CATEGORY, "tryOneLine"), true);
            this._path = ini.IniReadValue(CATEGORY, "path");
            this.save();
        }

        public void save(){
            this.saveAs(this.file);
        }

        public void saveAs(string file)
        {
            IniFile ini = new IniFile(file);
            ini.IniWriteValue(CATEGORY, "maxPopups", this._maxPopups.ToString());
            ini.IniWriteValue(CATEGORY, "keepOnTop", this._keepOnTop.ToString());
            ini.IniWriteValue(CATEGORY, "tryOneLine", this._tryOneLine.ToString());
            ini.IniWriteValue(CATEGORY, "path", this._path);
        }

        private void OnChanged(){
            if(this.Changed != null){
                this.Changed(this, EventArgs.Empty);
            }
        }
    }
}
