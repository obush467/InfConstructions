﻿using System.Threading.Tasks;
using InfConstractions.Models;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Xpf.Layout.Core;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using DevExpress.Xpo;
using System.Linq.Expressions;

namespace InfConstractions.ViewModels
{
    public class AddressTreeItem
    {
        public Nullable<Guid> ID { get; set; }
        public Nullable<Guid> PARENTGUID { get; set; }
        public string Name { get; set; }
    }
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
            //SetValue(Probe1Property, Sel1());
        }
        public ucAddressAssignViewModel(Entities context)
        {
            Context = context;
            //AddressObjects = new ObservableCollection<Models.Object>(Context.Objects.ToList());
            //FullAddressObject = new ObservableCollection<fullAddress_Result>(Context.fullAddress().ToList());
            

            DbQuery<AddressTreeItem> treeHousesQuery = (DbQuery<AddressTreeItem>)from house in Context.Houses
                             join esst in Context.EstateStatuses on house.ESTSTATUS equals esst.ESTSTATID
                             join strst in Context.StructureStatuses on house.STRSTATUS equals strst.STRSTATID
                             where house.STARTDATE <= DateTime.Now & house.ENDDATE >= DateTime.Now
                             select new AddressTreeItem { ID = house.HOUSEGUID, PARENTGUID = house.AOGUID ,Name = esst.NAME+house.HOUSENUM+house.BUILDNUM+strst.NAME+house.STRUCNUM };
            /*DbQuery<AddressTreeItem> treeObjectsQuery = (DbQuery<AddressTreeItem>)(from ao in Context.Objects
                                                                                   join aotype in Context.AddressObjectTypes
                                                                                   on new { LEVEL = ao.AOLEVEL, NAME = ao.SHORTNAME } equals new { LEVEL = aotype.LEVEL, NAME = aotype.SCNAME }
                                                                                   where ao.ACTSTATUS == 1 & ao.STARTDATE <= DateTime.Now & ao.ENDDATE >= DateTime.Now
                                                                                   orderby ao.FORMALNAME
                                                                                   select new AddressTreeItem { ID = ao.AOGUID, PARENTGUID = ao.PARENTGUID, Name = ao.FORMALNAME+" "+aotype.SOCRNAME }).Concat<AddressTreeItem>(treeHousesQuery.OrderBy(p=>p.Name));*/
            var housesQuery = from house in Context.Houses
                              join esst in Context.EstateStatuses on house.ESTSTATUS equals esst.ESTSTATID
                              join strst in Context.StructureStatuses on house.STRSTATUS equals strst.STRSTATID
                              where house.STARTDATE <= DateTime.Now & house.ENDDATE >= DateTime.Now
                              select new { house.HOUSEID, house.HOUSEGUID, Dom = Dom(esst.NAME, house.HOUSENUM, house.BUILDNUM, strst.NAME, house.STRUCNUM, ", ") };
            //fullHouses = housesQuery.ToList();
            //treeObjectsQuery.Load();
           
           treeAddress = new ObservableCollection<AllAddressObjects_Result>(Context.AllAddressObjects().ToList());
            //treeAddress = new ObservableCollection<AddressTreeItem>(treeObjectsQuery.ToList());
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ObservableCollection<InfConstractions.Models.Object> AddressObjects {get;set;}
        public ObservableCollection<fullAddress_Result> FullAddressObject { get; set; }
        public ObservableCollection<AllAddressObjects_Result> treeAddress { get; set; }
        public IEnumerable fullHouses { get; set; }
        public string Title { get { return "View model title"; } }


        #endregion

        #region Commands

        #endregion

        #region Methods
        public  Expression<Func<string,string, string, string, string, string,string>> Dom(string STYPE, string HOUSENUM, string BUILDNUM, string STRTYPE, string STRUCNUM,string separator )
        {
            Func<string, string, string, string, string, string, string> p = (string _STYPE, string _HOUSENUM, string _BUILDNUM, string _STRTYPE, string _STRUCNUM, string _separator) =>
             {
                 List<string> l = new List<string>();
                 if ((_HOUSENUM != null)) l.Add(_STYPE + " " + _HOUSENUM);
                 if (_BUILDNUM != null) l.Add("корпус " + _BUILDNUM);
                 if (_STRUCNUM != null) l.Add(_STRTYPE + " " + _STRUCNUM);
                 return String.Join(_separator, l);
             };
            return (_STYPE, _HOUSENUM, _BUILDNUM, _STRTYPE, _STRUCNUM, _separator) =>p (_STYPE, _HOUSENUM, _BUILDNUM, _STRTYPE, _STRUCNUM, _separator);
        }

        public string DomString(string STYPE, string HOUSENUM, string BUILDNUM, string STRTYPE, string STRUCNUM, string separator)
        {
            List<string> l = new List<string>();
            if ((HOUSENUM != null)) l.Add(STYPE + " " + HOUSENUM);
            if (BUILDNUM != null) l.Add("корпус " + BUILDNUM);
            if (STRUCNUM != null) l.Add(STRTYPE + " " + STRUCNUM);
            string res = String.Join(separator, l);
            return res;
        }
        protected Expression<Func<string, string, string>> AddressName(string addressName, string addressType)
        {
            Func<string, string, string> t = (string _addressName, string _addressType) => { return String.Concat(_addressName, _addressType); };
            return (_addressName, _addressType) => t(_addressName, _addressType); }
        protected string AddressNameString(string addressName, string addressType)
        {
            return String.Concat(addressName, " ",addressType);
        }
    #endregion
}
    }
