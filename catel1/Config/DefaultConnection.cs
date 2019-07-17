using System.Configuration;

namespace InfConstractions.Config
{
    public class DefaultConnectionConfig : ConfigurationElement
    {
        [ConfigurationProperty("Name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return ((string)(base["Name"])); }
            set { base["Name"] = value; }
        }
        [ConfigurationProperty("DefaultServerName", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string DefaultServerName
        {
            get { return ((string)(base["DefaultServerName"])); }
            set { base["DefaultServerName"] = value; }
        }
        [ConfigurationProperty("DefaultDBName", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string DefaultDBName
        {
            get { return ((string)(base["DefaultDBName"])); }
            set { base["DefaultDBName"] = value; }
        }

        [ConfigurationProperty("DefaultDBAuthenticationType", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string DefaultDBAuthenticationType
        {
            get { return ((string)(base["DefaultDBAuthenticationType"])); }
            set { base["DefaultDBAuthenticationType"] = value; }
        }


    }
}
