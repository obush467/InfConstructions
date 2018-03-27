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

namespace InfConstractions.Models
{
    public partial class Entities
    {

        public Entities(EntityConnection connection):base(connection,false)
        {
        }
        

        
    }
}
