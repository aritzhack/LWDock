using Ini;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LWDock.Config
{
    public class IniSaver : ConfigSaver, ConfigReader
    {

        private string configFile;

        public IniSaver(string configFile)
        {
            this.configFile = configFile;
        }

        public string getConfigFile()
        {
            return this.configFile;
        }

        public void writeConfig(Config config)
        {
            this.writeProperties(config.getPropertyList());
        }

        public void writeProperties(Dictionary<string, Property> properties)
        {
            this.writeProperties(properties.Values);
        }

        public void writeProperties(IEnumerable<Property> properties)
        {
            IniFile file = new IniFile(this.configFile);

            foreach (Property p in properties)
            {
                file.IniWriteValue("General", p.key, p.value);
            }
        }

        public Config readFile()
        {
            return IniSaver.readFile(this.configFile);
        }

        public static Config readFile(string path)
        {
            IniFile file = new IniFile(path);
            Config cfg = new Config();

            
            try
            {
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    if (line.Contains('='))
                    {
                        string key = line.Split(new char[] { '=' })[0].Trim();
                        cfg.addProperty(key, file.IniReadValue("General", key));
                    }
                }
            }
            catch (IOException) {}

            return cfg;
        }
    }
}
