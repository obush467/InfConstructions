using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfConstractionsDX.Config
{
    public sealed class Logins : ConfigurationElementCollection, IEnumerable<Login>
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
        {
            if (BaseGet(Name) == null)
                Add(new Login(Name));
        }
        public new IEnumerator<Login> GetEnumerator()
        {
            foreach (var key in this.BaseGetAllKeys())
            {
                yield return (Login)BaseGet(key);
            }
        }
        public string Cast<T>() { return ToString(); }

    }
}
