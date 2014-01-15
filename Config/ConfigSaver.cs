using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LWDock.Config
{
    public interface ConfigSaver
    {
        string getConfigFile();
        void writeConfig(Config config);
        void writeProperties(Dictionary<string, Property> properties);
        void writeProperties(IEnumerable<Property> properties);
    }
}
