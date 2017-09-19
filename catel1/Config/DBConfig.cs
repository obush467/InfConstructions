using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace InfConstractions.Config
{
    public sealed class Config : ConfigurationSection
    {
        [ConfigurationProperty("DefaultConnection")]
        public DefaultConnectionConfig defaultConnection
        { get { return (DefaultConnectionConfig)this["DefaultConnection"]; } }
    }
}
