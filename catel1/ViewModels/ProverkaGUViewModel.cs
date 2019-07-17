namespace InfConstractions.ViewModels
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Docking;
    using InfConstractions.Config;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;
    using UNS.Models;

    public class ProverkaGUViewModel : ViewModelBase, ISupportServices
    {
        #region Services
        protected IDocumentManagerService DocumentManagerService { get; set; }
        protected ISplashScreenService SplashScreenServiceLoad { get { return this.GetService<ISplashScreenService>(); } }
        #endregion
        #region Constractors
        public ProverkaGUViewModel(Entities context)
        {

            //proverkaGUModel = new ProverkaGUModel(context);
            LoadConfig();
            Context = context;
            Context.proverkaGUs.Load();
            ProverkaGU = new ObservableCollection<proverkaGU>(Context.proverkaGUs);
            Title = "Проверка ГУ";

        }
        public ProverkaGUViewModel()
        {
            Context = (Entities)((object[])Parameter)[1];
        }
        public ProverkaGUViewModel(object parameter)
        {
            Context = (Entities)((object[])Parameter)[1];
        }
        public ProverkaGUViewModel(Entities context, IDocumentManagerService documentManagerService) : this(context)
        {
            DocumentManagerService = documentManagerService;
        }
        public ProverkaGUViewModel(Entities context, IDocumentManagerService documentManagerService, ViewModelBase parentViewModel) : this(context, documentManagerService)
        {
            this.SetParentViewModel(parentViewModel);
        }
        public static ProverkaGUViewModel Create()
        { return ViewModelSource.Create(() => new ProverkaGUViewModel()); }

        public static ProverkaGUViewModel Create(Entities context)
        { return ViewModelSource.Create(() => new ProverkaGUViewModel(context)); }
        public static ProverkaGUViewModel Create(Entities context, IDocumentManagerService documentManagerService)
        { return ViewModelSource.Create(() => new ProverkaGUViewModel(context, documentManagerService)); }
        #endregion
        #region Properties
        public string Title
        {
            get { return GetProperty<string>(() => Title); }
            protected set { SetProperty<string>(() => Title, value); }
        }
        public Configuration Config
        {
            get { return GetProperty<Configuration>(() => Config); }
            protected set { SetProperty<Configuration>(() => Config, value); }
        }
        public DefaultConnectionConfig Config_connection
        {
            get { return GetProperty<DefaultConnectionConfig>(() => Config_connection); }
            protected set { SetProperty<DefaultConnectionConfig>(() => Config_connection, value); }
        }
        public Entities Context
        {
            get { return GetProperty<Entities>(() => Context); }
            protected set { SetProperty<Entities>(() => Context, value); }
        }
        public ObservableCollection<proverkaGU> ProverkaGU
        {
            get { return GetProperty<ObservableCollection<proverkaGU>>(() => ProverkaGU); }
            protected set { SetProperty<ObservableCollection<proverkaGU>>(() => ProverkaGU, value); }
        }
        #endregion
        #region Commands

        [Command(CanExecuteMethodName = "CancmSaveChanges",
            Name = "SaveChangesCommand",
            UseCommandManager = true)]
        public void cmSaveChanges()
        { Context.SaveChanges(); }

        public bool CancmSaveChanges()
        {
            return true;
        }


        [Command(CanExecuteMethodName = "CancmRefresh",
            Name = "RefreshCommand",
            UseCommandManager = true)]
        public void cmRefresh()
        {
            // proverkaGUModel.Refresh();
            Context.SaveChanges();
            Context.proverkaGUs.Load();
            ProverkaGU = new ObservableCollection<proverkaGU>(Context.proverkaGUs);
        }
        public bool CancmRefresh()
        {
            return Context.ChangeTracker.HasChanges();
        }

        [Command(CanExecuteMethodName = "CanucPassport",
        Name = "ucPassportCommand",
        UseCommandManager = true)]
        public void ucPassport(string UNOM)
        {
            SplashScreenServiceLoad.ShowSplashScreen("mainSplash");
            IQueryable<GUPassport> passportQuery =
                    from passport
                    in Context.GUPassports
                    where (passport.UNOM == UNOM.ToString())
                    orderby passport.startdate descending
                    select passport;
            if (passportQuery.Count() > 0)
            {
                GUPassport _passport = passportQuery.FirstOrDefault<GUPassport>();
                IDocument _doc =
                DocumentManagerService.FindDocumentByIdOrCreate(_passport.id.ToString(), (ds) =>
                {
                    IDocument _docCreated = ds.CreateDocument("ucPassport", ViewModelSource.Create(() => new UcPassportViewModel(_passport.id)));
                    _docCreated.Id = _passport.id.ToString();
                    _docCreated.Title = "Паспорт " + _passport.UNOM;
                    return _docCreated;
                });
                _doc.Show();
                SplashScreenServiceLoad.HideSplashScreen();
            }
        }
        public bool CanucPassport(string UNOM)
        {
            IQueryable<GUPassport> passportID =
        from passport
        in Context.GUPassports
        where (passport.UNOM == UNOM)
        orderby passport.startdate descending
        select passport;
            if ((passportID != null) & (passportID.Count() > 0)) return true; else return false;
        }
        #endregion
        #region Methods
        public void LoadConfig()
        {
            Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        public void SaveConfig()
        {
            Config.Save(ConfigurationSaveMode.Full);
        }
        #endregion
    }
}
