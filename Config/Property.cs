using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LWDock.Config
{
    public class Property
    {

        public string key { get; private set; }
        public string value { get; private set; }

        public Property(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
