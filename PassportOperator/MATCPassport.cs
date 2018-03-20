using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassportOperator
{
    public class MATCPassport
    {
        public Guid ID { get; set; }
        public string UNIU { get; set; }
        public string CreateDate { get; set; }
        public string Address { get; set; }
        public string Okrug { get; set; }
        public string Raion { get; set; }
        public string GU { get; set; }
        public string Closed_loop { get; set; }
        public string Reconstruction { get; set; }
        public string Electricity_connection { get; set; }
        public string Visibility { get; set; }
        public string Type_of_surface { get; set; }
        public string Sidewalk_width { get; set; }
        public string Traffic { get; set; }
        public string State { get; set; }
        public Stream Foto { get; set; }
        public Stream Plan { get; set; }
        public List<MATCPassport_InformationField> Information_fields { get; set; } = new List<MATCPassport_InformationField>();


    }
    public class MATCPassport_InformationField
    {
        public string Name { get; set; }
        public string Transliteration { get; set; }
        public string Arrow { get; set; }
    }

}
