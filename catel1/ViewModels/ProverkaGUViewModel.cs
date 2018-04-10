namespace InfConstractions.ViewModels
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Docking;
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using InfConstractions.Config;

    public class ProverkaGUViewModel : ViewModelBase,ISupportServices
    {
        protected IDocumentManagerService DocumentManagerService { get; set; }
        protected ISplashScreenService SplashScreenServiceLoad { get { return this.GetService<ISplashScreenService>(); } }

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
        public ProverkaGUViewModel():this(new Entities(App.mainConnection))
        {
        }
        public ProverkaGUViewModel(Entities context, IDocumentManagerService documentManagerService) : this(context)
        {
            DocumentManagerService = documentManagerService;
        }
        public ProverkaGUViewModel(Entities context, IDocumentManagerService documentManagerService,ViewModelBase parentViewModel) : this(context,documentManagerService)
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
        public string Title { get; set; }
        /*-public ProverkaGUModel proverkaGUModel
        {
            get ;set; 
        }*/
        public Configuration Config { get; protected set; }
        public DefaultConnectionConfig _config_connection { get; set; }
        public Entities Context
        {
            get; protected set;
            /*get { return proverkaGUModel.Context; }
            set { proverkaGUModel.Context=value; }*/
        }

        public ObservableCollection<proverkaGU> ProverkaGU
        {
            get; protected set;
            /*get { return proverkaGUModel.ProverkaGU;}*/
        }

        #region Commands

        [Command(CanExecuteMethodName = "CancmSaveChanges",
            Name = "SaveChangesCommand",
            UseCommandManager = true)]
        public void cmSaveChanges ()
        {Context.SaveChanges();}

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
        public void ucPassport(object UNOM)
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
                    IDocument _docCreated = ds.CreateDocument("ucPassport", ViewModelSource.Create(() => new ucPassportViewModel(_passport.id)));
                    _docCreated.Id = _passport.id.ToString();
                    _docCreated.Title = "Паспорт " + _passport.UNOM;
                    return _docCreated;
                });
                _doc.Show();
                SplashScreenServiceLoad.HideSplashScreen();
            }
        }
        public bool CanucPassport(object UNOM)
        {
            IQueryable<GUPassport> passportID =
        from passport
        in Context.GUPassports
        where (passport.UNOM == UNOM)
        orderby passport.startdate descending
        select passport;
            if ((passportID!=null) & ( passportID.Count() > 0)) return true; else return false;
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
