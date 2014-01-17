
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
