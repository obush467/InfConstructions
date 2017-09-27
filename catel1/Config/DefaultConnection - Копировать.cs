using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Login():base() { }
    }

    public sealed class Logins : ConfigurationElementCollection,IEnumerable<Login>
    {
        protected override ConfigurationElement CreateNewElement()
        {
           return new Login();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Login)element).Name;
        }

        public void Add(Login element)
        { BaseAdd(element); }

        public void Add(string Name)
        { if (BaseGet(Name)==null)
            Add(new Login(Name)); }

        public new IEnumerator<Login> GetEnumerator()
        {
            foreach (var key in this.BaseGetAllKeys())
            {
                yield return (Login)BaseGet(key);
            }
        }
        public string Cast<T>() { return this.ToString(); }

    }
}
