namespace InfConstractionsDX.Modules.ViewModels
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.IO;
    using System.ComponentModel;
    using PassportOperator;
    using InfConstractions.Models;
    using System.Data.Entity.Core.EntityClient;

    public class ucPassportViewModel : ViewModelBase, INotifyCollectionChanged,INotifyPropertyChanged

    {
        #region Services
        public IMessageBoxService MessageService { get { return GetService<IMessageBoxService>(); } }       
        #endregion
        #region Fields
        private Guid passportID;
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
        {
            get
            {
                return from state in Context.GUPassport_States
                       where state.GUPassport_ID == Passport.id
                       orderby state.startdate
                       select state;
            }
        }

        #endregion
        #region Events
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        
        #region Event_Handlers
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
        public void SitesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (GUPassport_Site item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= SitePropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (GUPassport_Site item in e.NewItems)
                {
                    if (PassportSites.Where<GUPassport_Site>
                        (s =>
                                (s.id != item.id)
                                & (s.Block_Number == item.Block_Number)
                                & (s.Row_Number == item.Row_Number)
                                & (s.Site_Number == item.Site_Number)
                         ).Count() > 0)

                        MessageService.ShowMessage("44444444444444444444444444444444444444444444444444444444");
                }
            }
        }



        private void SitePropertyChanged(object sender, PropertyChangedEventArgs e)
        {

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
        #endregion
        #region Constractors
        public static ucPassportViewModel Create()
        {
            return ViewModelSource.Create(() => new ucPassportViewModel());
        }
        public ucPassportViewModel(EntityConnection connection, Guid _passportID):this(connection,new Entities(connection),_passportID)
        {
        }
        public ucPassportViewModel(EntityConnection connection, Entities _context,Guid _passportID)
        {
            efConnection = connection;
            passportID = _passportID;
            Context = _context;
            //Context.GUPassport_Sites.Load();
            Refresh();
        }

        public ucPassportViewModel()
        {
        }
        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            if (parameter is Entities)
            {
                //... 
            }
            else
            {
                efConnection = parameter.GetType().GetProperty("efConnection").GetValue(parameter) as EntityConnection;
                Context = parameter.GetType().GetProperty("Context").GetValue(parameter) as Entities;
                //LoadConfig();
                //Context.proverkaGUs.Load();
                passportID = (Guid)parameter.GetType().GetProperty("passportID").GetValue(parameter);

                Refresh();
                Caption = "Паспорт "+Passport.UNOM;
            }
        }

        public static ucPassportViewModel Create(EntityConnection connection, Entities context,Guid passportID) 
        { return ViewModelSource.Create(() => new ucPassportViewModel(connection,context,passportID)); }
        public static ucPassportViewModel Create(EntityConnection connection, Guid passportID)
        { return ViewModelSource.Create(() => new ucPassportViewModel(connection,passportID)); }
        #endregion
        #region Properties 
        public ObservableCollection<ALLAddressObjectsFull> treeAddress { get; set; }
        ucAddressAssignViewModel AddressAssignViewModel { get; set; }
        protected EntityConnection efConnection { get; set; }
        public bool EnableEdit { get
            {
                switch (PassportStates.Where(s => (s.NextID == null)).FirstOrDefault().State)
                {
                    case "согласован":
                        return false;
                        //break;
                    case "исключён":
                        return false;
                        //break;
                    case "аннулирован":
                        return false;
                        //break;
                    default: return true;
                }
            } private set { } }
        public string Title {
            get { return GetProperty(() => Title); }
            private set {SetProperty(() => Title, value); }
        }
        public Entities Context
        {
            get { return GetProperty(() => Context); }
            private set { SetProperty(() => Context, value); }
        }
        public GUPassport Passport
        {
            get { return GetProperty(() => Passport); }
            private set { SetProperty(() => Passport, value); }
        }

        public Nullable<Guid> AddressID
        {
            get { return Passport.HouseID; }
            set { Passport.HouseID= value; }
        }
        public string Address
        {
            get { return GetProperty(() => Address); }
            private set { SetProperty(() => Address, value); }
        }
        public ObservableCollection<GUPassport_Site> PassportSites {
            get { return GetProperty(() => PassportSites); }
            private set { SetProperty(() => PassportSites, value); }
        }
        public ObservableCollection<GUPassport_State> PassportStates {
            get { return GetProperty(() => PassportStates); }
            private set { SetProperty(() => PassportStates, value); }
        }
        public ObservableCollection<Ground_Type> Ground_Types {
            get { return GetProperty(() => Ground_Types); }
            private set { SetProperty(() => Ground_Types, value); }
        }
        public ObservableCollection<Program> Programs {
            get { return GetProperty(() => Programs); }
            private set { SetProperty(() => Programs, value); }
        }
        public object NullDate { get { return null;} }
        public AdminArea Okrug {
            get { return GetProperty(() => Okrug); }
            private set { SetProperty(() => Okrug, value); }
        }
        public AdminArea Raion {
            get { return GetProperty(() => Raion); }
            private set { SetProperty(() => Raion, value); }
        }

        public string Caption { get; private set; }
        #endregion
        #region Methods
        internal void Refresh()
        {
            
            Passport = Context.GUPassports.Where<GUPassport>(p => (p.id == passportID)).First<GUPassport>();
            Address = Context.ObjectFullAddress4(Passport.HouseID, " ", true, true, true).First().fullAdress;
            Raion = Context.AdminAreas.Where(a => a.ID == Passport.AdmidArea_ID).First();
            Okrug = Context.AdminAreas.Where(a => a.ID == Raion.Parent_ID).First();
            Programs = new ObservableCollection<Program>(Context.Programs.ToList());
            PassportSites = new ObservableCollection<GUPassport_Site>(Context.GetGUPassport_Sites(Passport.id));
            PassportStates = new ObservableCollection<GUPassport_State>(queryPassportStates.ToList());
            Ground_Types = new ObservableCollection<Ground_Type>(Context.Ground_Types.ToList());
            PassportSites.CollectionChanged += passportSitesChanged;
            PassportStates.CollectionChanged += passportStatesChanged;
            CollectionChanged += rrrrr;
            PropertyChanged += yuyu;
            PassportSites.CollectionChanged += CollectionChanged;
            PassportSites.CollectionChanged += SitesCollectionChanged;
            treeAddress = new ObservableCollection<ALLAddressObjectsFull>(Context.ALLAddressObjectsFulls.ToList());
            //AddressID = Passport.HouseID;
        }

        private void yuyu(object sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void rrrrr(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void RollBackChandes()
        {
            foreach (DbEntityEntry entry in Context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }
        #endregion
        #region Commands
        [Command(CanExecuteMethodName = "CancmClose",
            Name = "CloseCommand",
            UseCommandManager = true)]
        public void cmClose()
        {
        }

        public bool CancmClose()
        {
            return true;
        }

        [Command(CanExecuteMethodName = "CancmSaveChanges",
            Name = "SaveChangesCommand",
            UseCommandManager = true)]
        public void cmSaveChanges ()
        {Context.SaveChanges();}

        public bool CancmSaveChanges()
        {
            return Context.ChangeTracker.HasChanges();
        }
        [Command(CanExecuteMethodName = "CancmCancel",
           Name = "CancelCommand",
           UseCommandManager = true)]
        public void cmCancel()
        { RollBackChandes(); }

        public bool CancmCancel()
        {
            return Context.ChangeTracker.HasChanges();
        }

        [Command(CanExecuteMethodName = "CancmRefresh",
            Name = "RefreshCommand",
            UseCommandManager = true)]
        public void cmRefresh()
        {
            Refresh();
        }
        public bool CancmRefresh()
        {           
            return true;
        }

        [Command(CanExecuteMethodName = "CanGenerateDOCX",
       Name = "GenerateDOCXCommand",
       UseCommandManager = true)]
        public void GenerateDOCX()
        {
            MATCPassportExporter _exporter = new MATCPassportExporter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "паспортГУ1.dotx"));
            MATCPassport _expassport = new PassportOperator.MATCPassport();
            _expassport.UNIU = Passport.UNOM;
            _expassport.CreateDate = Passport.startdate.ToShortDateString();
            _expassport.Okrug = Okrug.shortName;
            _expassport.Raion = Raion.shortName;
            _expassport.Address = Address;
            _expassport.Visibility = Passport.Visibility;
            _expassport.Sidewalk_width = Passport.Sidewalk_width;
            _expassport.Traffic = Passport.Patency;
            _expassport.State = Passport.Condition;
            if ( String.IsNullOrWhiteSpace(Passport.Electricity_connection))
                _expassport.Electricity_connection = "Нет";
            else _expassport.Electricity_connection = Passport.Electricity_connection;
            _expassport.Closed_loop = Passport.Closed_loop;
            _expassport.Reconstruction = Passport.Reconstruction;
            _expassport.Type_of_surface = Ground_Types.FirstOrDefault(p => p.id == Passport.Ground_Type_ID).GroundName;
            if (Passport.Foto!=null) _expassport.Foto = new MemoryStream(Passport.Foto);
            if (Passport.Plan != null) _expassport.Plan = new MemoryStream(Passport.Plan);
            foreach (GUPassport_Site s in PassportSites)
            {
                MATCPassport_InformationField f = new MATCPassport_InformationField();
                f.Name = s.Content_Text;
                f.Transliteration = s.Content_Transliteration;
                f.Arrow = "блок " + s.Block_Number + " сторона " + s.Site_Number + " ряд " + s.Row_Number + " стрелка: "+s.Content_Direction;
                _expassport.Information_fields.Add(f);
            }
            //var d=_exporter.ExportToDOCX( ConvertToWord(_expassport);
        }
        public bool CanGenerateDOCX()
        {
            return true;
        }



        [Command(CanExecuteMethodName = "CancmCancelChangeAddress",
            Name = "CancelChangeAddressCommand",
            UseCommandManager = true)]
        public void cmCancelChangeAddress()
        {
            
        }
        public bool CancmCancelChangeAddress()
        {
            return true;
        }

        [Command(CanExecuteMethodName = "CancmChangeAddress",
        Name = "ChangeAddressCommand",
        UseCommandManager = true)]
        public void cmChangeAddress(object parameter)
        {
            if (parameter is ucAddressAssignViewModel)
            { Passport.HouseID = ((ucAddressAssignViewModel)parameter).SelectedAddress.ID; }
            if (parameter is ucPassportViewModel)
            { Passport.HouseID = ((ucPassportViewModel)parameter).AddressAssignViewModel.SelectedAddress.ID; }
            if (parameter is ALLAddressObjectsFull)
            { AddressID = ((ALLAddressObjectsFull)parameter).ID;}

        }
        public bool CancmChangeAddress(object parameter)
        {
            return true;
        }

        #endregion
    }
}
