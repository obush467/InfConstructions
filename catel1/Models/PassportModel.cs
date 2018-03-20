using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfConstractions.Models;

namespace InfConstractions.Models
{
    public class PassportModel
    {
        public PassportModel(Entities context)
        {
        }
        public PassportModel(Entities context,GUPassport passport)
        {
            Passport = passport;
            Context = context;
            PassportSites = context.GUPassport_Sites.Where<GUPassport_Site>(site => site.id == passport.id).ToList<GUPassport_Site>();
            PassportStates = context.GUPassport_States.Where<GUPassport_State>(site => site.id == passport.id).ToList<GUPassport_State>();
        }
        private Entities Context { get; set; }
        public GUPassport Passport { get; set; }
        public List<GUPassport_Site> PassportSites { get; protected set; }
        public List<GUPassport_State> PassportStates { get; protected set; }
    }
}
