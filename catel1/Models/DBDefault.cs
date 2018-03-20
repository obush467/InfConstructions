using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InfConstractions.Models
{

    public class DBDefault
    {
        [JsonProperty]
        public string DefaultDBName { get; set; }
        [JsonProperty]
        public string DefaultDBAuthenticationType { get; set; }
        [JsonProperty]
        public string DefaultServerName { get; set; }
    }
}
