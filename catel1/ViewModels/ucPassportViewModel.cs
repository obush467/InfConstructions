namespace InfConstractions.ViewModels
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Utils.MVVM.Services;
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using Microsoft.Office.Interop.Word;
    using System.IO;
    using PassportOperator;

    public class ucPassportViewModel : ViewModelBase,INotifyCollectionChanged
    {
        private Guid passportID;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        IPassportService PassportService { get { return GetService<IPassportService>(); } }
        #region Constractors
        public ucPassportViewModel(Entities context) : this(new ucPassportModel(context))
        { }
        public ucPassportViewModel(ucPassportModel _ucPassportModel)
        {
            Model = _ucPassportModel;
        }
        public ucPassportViewModel()
        {
        }

        public ucPassportModel Model { get; set; }

        public ucPassportViewModel(Entities context,Guid passportID)
        {
            this.passportID = passportID;
            Model = new ucPassportModel(context,passportID);
            Model.PassportSites.CollectionChanged+= CollectionChanged;
            Context = context;
        }
        

        public static ucPassportViewModel Create()
        { return ViewModelSource.Create(() => new ucPassportViewModel()); }

        public static ucPassportViewModel Create(Entities context) 
        { return ViewModelSource.Create(() => new ucPassportViewModel(context)); }
        public static ucPassportViewModel Create(ucPassportModel _ucPassportModel)
        { return ViewModelSource.Create(() => new ucPassportViewModel(_ucPassportModel)); }

        #endregion
        #region Properties  
        public string Title {
            get { return GetProperty(() => Title); }
            private set {SetProperty(() => Title, value); }
        }
        public Entities Context
        {
            get { return GetProperty(() => Context); }
            private set { SetProperty(() => Context, value); }
            /*get { return Model.Context; }
            set { Model.Context=value; }*/
        }
        public GUPassport Passport
        {
            get { return Model.Passport;}
        }
        public string Address
        {
            get { return Model.Address; }
        }
        public ObservableCollection<GUPassport_Site> PassportSites { get { return Model.PassportSites; } }
        public ObservableCollection<GUPassport_State> PassportStates { get { return Model.PassportStates; } }
        public ObservableCollection<Ground_Type> Ground_Types { get { return Model.Ground_Types; } }
        public object NullDate { get { return null;} }
        public AdminArea Okrug { get { return Model.Okrug; } }
        public AdminArea Raion { get { return Model.Raion; } }
        #endregion
        #region Methods
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
        {  }

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
            Model.Refresh();
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
            PassportOperator.MATCPassportExporter _exporter = new MATCPassportExporter(Path.Combine("G:\\projects\\InfConstractions\\catel1\\Templates", "паспортГУ.dotx"));
            PassportOperator.MATCPassport _expassport = new PassportOperator.MATCPassport();
            _expassport.UNIU = Passport.UNOM;
            _expassport.CreateDate = Passport.startdate.ToShortDateString();
            _expassport.Okrug = Okrug.shortName;
            _expassport.Raion = Raion.shortName;
            _expassport.Address = Address;
            _expassport.Visibility = Passport.Visibility;
            _expassport.Sidewalk_width = Passport.Sidewalk_width;
            _expassport.Traffic = Passport.Patency;
            _expassport.State = Passport.Condition;
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
            Document d=_exporter.ConvertToWord(_expassport);
        }
        public bool CanGenerateDOCX()
        {
            return true;
        }

        #endregion
    }
}
