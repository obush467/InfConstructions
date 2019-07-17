using System.Configuration;

namespace commonB1.Config
{
    public class StandartName : ConfigurationElement

    {
        [ConfigurationProperty("Name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return ((string)(base["Name"])); }
            set { base["Name"] = value; }
        }
    }
}