using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LWDock.Config
{
    interface ConfigSaver
    {

        public void writeConfig(Config config);
        public void writeProperties(Map map)
    }
}
