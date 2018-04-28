using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace InfConstractionsDX.Config
{
    public sealed class Config : ConfigurationSection
    {
        [ConfigurationProperty("DefaultConnection")]
        public DefaultConnectionConfig defaultConnection
        { get { return (DefaultConnectionConfig)this["DefaultConnection"]; }
        }
        [ConfigurationProperty("Logins", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(Logins), AddItemName = "add",ClearItemsName = "clear",RemoveItemName = "remove")]
        public Logins Logins
        {
            get { return (Logins)base["Logins"]; }
        }
    }
}
