using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
namespace InfConstractions.Models
{
    [DataContract]
    class JSONconfig
    {
        [DataMember]
        public string ServerName { get; set; }
        [DataMember]
        public string AutenticationType { get; set; }
    }
}
