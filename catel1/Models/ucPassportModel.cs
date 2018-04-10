using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfConstractions.Models;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.Utils;
using System.Collections.Specialized;

namespace InfConstractions.Models
{
    public class ucPassportModel
    {
        #region Fields
        protected IQueryable<GUPassport_Site> queryPassportSites
        {
            get
            {
                return from site in Context.GUPassport_Sites
                       where site.GUPassport_ID == Passport.id
                       orderby site.Block_Number, site.Site_Number, site.Row_Number
                       select site;
            }
        }
        protected IQueryable<GUPassport_State> queryPassportStates
        { get
            {
                return from state in Context.GUPassport_States
                       where state.GUPassport_ID == Passport.id
                       orderby state.startdate
                       select state;
            }
        }

        #endregion
        #region Constructors
        public ucPassportModel(Entities context)
        {
            Context = context;
            Context.GUPassport_Sites.Load();
        }

        public ucPassportModel(Entities context, Guid passportID) : this(context)
        {
            Passport = context.GUPassports.Where<GUPassport>(p => (p.id == passportID)).First<GUPassport>();
            Refresh();
            PassportSites.CollectionChanged += passportSitesChanged;
            PassportStates.CollectionChanged += passportStatesChanged;
            
        }

        private void passportSitesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (GUPassport_Site newItem in e.NewItems)
                {
                    newItem.id = Guid.NewGuid();
                    newItem.GUPassport_ID = Passport.id;
                    Context.GUPassport_Sites.Add(newItem);
                }
            }
            if (e.OldItems != null)
            {
                foreach (GUPassport_Site oldItem in e.OldItems)
                {
                    Context.GUPassport_Sites.Remove(oldItem);
                }
            }
        }

        private void passportStatesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (GUPassport_State newItem in e.NewItems)
                {
                    newItem.id = Guid.NewGuid();
                    newItem.GUPassport_ID = Passport.id;
                    Context.GUPassport_States.Add(newItem);
                }
            }
            if (e.OldItems != null)
            {
                foreach (GUPassport_State oldItem in e.OldItems)
                {
                    Context.GUPassport_States.Remove(oldItem);
                }
            }
        }

        public ucPassportModel(Entities context,GUPassport passport):this(context)
        {
            Passport = passport;
            Refresh();
        }
        #endregion
        #region Properties 
        public Entities Context { get; set; }
        public GUPassport Passport { get; set; }
        public string Address { get { return Context.ObjectFullAddress4(Passport.HouseID, " ", true, true, true).First().fullAdress; ; } }
        public AdminArea Okrug { get { return Context.AdminAreas.Where(a=> a.ID==Raion.Parent_ID).First();;}}
        public AdminArea Raion { get { return Context.AdminAreas.Where(a => a.ID == Passport.AdmidArea_ID).First();;}}
        public ObservableCollection<GUPassport_Site> PassportSites { get; protected set; }
        public ObservableCollection<GUPassport_State> PassportStates { get; protected set; }
        public ObservableCollection<Ground_Type> Ground_Types { get; protected set; }
        public ObservableCollection<Program> Programs { get; protected set; }
        #endregion
        #region Metods
        internal void Refresh()
        {
            Passport = Context.GUPassports.Where<GUPassport>(p => (p.id == Passport.id)).First<GUPassport>();
            Programs = new ObservableCollection<Program>(Context.Programs.ToList());
            //PassportSites = new ObservableCollection<GUPassport_Site>(queryPassportSites.ToList());
            PassportSites = new ObservableCollection<GUPassport_Site>(Context.GetGUPassport_Sites(Passport.id));
            PassportStates = new ObservableCollection<GUPassport_State>(queryPassportStates.ToList());
            Ground_Types = new ObservableCollection<Ground_Type>(Context.Ground_Types.ToList());
        }
        #endregion
    }
}
