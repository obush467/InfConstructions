using System.Threading.Tasks;
using InfConstractions.Models;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Xpf.Layout.Core;
using System.Collections;
using System.Collections.Generic;

namespace InfConstractions.ViewModels
{
    public class ucAddressAssignViewModel : ViewModelBase
    {
        #region Fields
        public Entities Context { get; set; }

        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignAddressViewModel"/> class.
        /// </summary>
        public ucAddressAssignViewModel():this(new Entities(App.mainConnection))
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            //SetValue(Probe1Property, Sel1());
            Context.Configuration.AutoDetectChangesEnabled = true;
        }
        public ucAddressAssignViewModel(Entities context)
        {
            Context = context;
            AddressObjects = new ObservableCollection<Models.Object>(Context.Objects.ToList());
            FullAddressObject = new ObservableCollection<fullAddress_Result>(Context.fullAddress().ToList());
            var housesQuery = from house in Context.Houses
                              join esst in Context.EstateStatuses on house.ESTSTATUS equals esst.ESTSTATID
                              join strst in Context.StructureStatuses on house.STRSTATUS equals strst.STRSTATID
                              where house.STARTDATE <= DateTime.Now & house.ENDDATE >= DateTime.Now
                              select new { house.HOUSEID,house.HOUSEGUID, Dom = Dom(esst.NAME, house.HOUSENUM,house.BUILDNUM, strst.NAME, house.STRUCNUM,", ")};
            fullHouses = housesQuery.ToList();
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ObservableCollection<InfConstractions.Models.Object> AddressObjects {get;set;}
        public ObservableCollection<fullAddress_Result> FullAddressObject { get; set; }
        public IEnumerable fullHouses { get; set; }
        public string Title { get { return "View model title"; } }


        #endregion

        #region Commands

        #endregion

        #region Methods
        protected string Dom(string STYPE, string HOUSENUM, string BUILDNUM, string STRTYPE, string STRUCNUM,string separator )
        {
            List<string> l = new List<string>();
            if ((HOUSENUM != null)) l.Add(STYPE + " " + HOUSENUM);
            if (BUILDNUM != null) l.Add("корпус "+BUILDNUM);
            if (STRUCNUM != null) l.Add(STRTYPE + " " + STRUCNUM);
            return String.Join(separator, l);
        }
        #endregion
        }
    }

