using System.Collections.Generic;

namespace LWDock.Config
{
    public class Config
    {
        private Dictionary<string, Property> properties = new Dictionary<string, Property>();

        public void addProperty(string key, string value)
        {
            this.properties.Add(key, new Property(key, value));
        }

        public void setProperty(string key, object value)
        {
            this.setProperty(key, value.ToString());
        }

        public void setProperty(string key, string value)
        {
            this.properties.Remove(key);
            this.addProperty(key, value);
        }

        public IEnumerable<Property> getPropertyList()
        {
            return this.properties.Values;
        }

        public Dictionary<string, Property> getProperties()
        {
            return this.properties;
        }

        public string getValue(string key)
        {
            try
            {
                return this.properties[key].value;
            }
            catch (KeyNotFoundException)
            {
                return "";
            }
        }

        public Property getProperty(string key)
        {
            return this.properties[key];
        }
    }
}
