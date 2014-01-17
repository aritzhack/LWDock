using System.Collections.Generic;

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
