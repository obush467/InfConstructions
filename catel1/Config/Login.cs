using System.Configuration;

namespace InfConstractions.Config
{
    public class Login : ConfigurationElement
    {
        [ConfigurationProperty("Name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return ((string)(base["Name"])); }
            set { base["Name"] = value; }
        }
        public Login(string name)
        { Name = name; }
        public Login() : base() { }
    }
}
