using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNS.Models
{
    public partial class Entities
    {

        public Entities(EntityConnection connection) : base(connection, false)
        {

        }
        public DbQuery<GUPassport_Site> GetGUPassport_Sites(Guid PassportID)
        {
            return (DbQuery<GUPassport_Site>)(from site in GUPassport_Sites
                                              where site.GUPassport_ID == PassportID
                                              orderby site.Block_Number, site.Site_Number, site.Row_Number
                                              select site);
        }

    }
}
