using AppModules = InfConstractionsDX.Common.Modules;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.Navigation;
using DevExpress.Xpf.Core;
using InfConstractions.Models;
using InfConstractionsDX.Common;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using InfConstractionsDX.Modules.Views;
using DevExpress.Mvvm.UI;
using InfConstractionsDX.Config;
using DevExpress.Mvvm.ViewModel;

namespace InfConstractionsDX.Modules.ViewModels
{
    public class ProverkaGUViewModel : ViewModelBase, ISupportServices,IDocumentModule, ISupportState<ProverkaGUViewModel.Info>
    {
        #region Services
        protected IDocumentManagerService DocumentManagerService { get; set; }
        protected ISplashScreenService SplashScreenServiceLoad { get { return this.GetService<ISplashScreenService>(); } } 
        #endregion
        #region Constractors
        public ProverkaGUViewModel(EntityConnection connection, Entities context) :this()
        {
            Parameter = new { connection, context };
        }
        public ProverkaGUViewModel()
        {
            
        }
        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            if (IsInDesignMode)
            {
                //... 
            }
            else
            {
                efConnection = parameter.GetType().GetProperty("efConnection").GetValue(parameter) as EntityConnection;
                Context = parameter.GetType().GetProperty("mainContext").GetValue(parameter) as Entities;
                LoadConfig();
                Context.proverkaGUs.Load();
                ProverkaGU = new ObservableCollection<proverkaGU>(Context.proverkaGUs);
                Caption = "Проверка ГУ";
            }
        }
        public ProverkaGUViewModel(EntityConnection connection, Entities context, IDocumentManagerService documentManagerService) : this(connection, context)
        {
            DocumentManagerService = documentManagerService;
        }
        public ProverkaGUViewModel(EntityConnection connection, Entities context, IDocumentManagerService documentManagerService,ViewModelBase parentViewModel) : this(connection, context,documentManagerService)
        {
            this.SetParentViewModel(parentViewModel);
        }
        public static ProverkaGUViewModel Create()
        { return ViewModelSource.Create(() => new ProverkaGUViewModel()); }
        public static ProverkaGUViewModel Create(EntityConnection connection, Entities context) 
        { return ViewModelSource.Create(() => new ProverkaGUViewModel(connection, context)); }
        public static ProverkaGUViewModel Create(EntityConnection connection, Entities context, IDocumentManagerService documentManagerService)
        { return ViewModelSource.Create(() => new ProverkaGUViewModel(connection, context, documentManagerService)); }
        #endregion
        #region Properties
        protected IModuleManager Manager { get { return ModuleManager.DefaultManager; } }
        protected EntityConnection efConnection { get; set; }
        public string Title {
            get { return GetProperty<string>(() => Title); }
            protected set { SetProperty<string>(() => Title, value); }
        }
        public Configuration Config {
            get { return GetProperty<Configuration>(() => Config); }
            protected set { SetProperty<Configuration>(() => Config, value); }
        }
        public DefaultConnectionConfig Config_connection {
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

        public string Caption
        {
            get { return GetProperty<string>(() => Caption); }
            protected set { SetProperty<string>(() => Caption, value); }
        }
        public virtual bool IsActive { get; set; }


        #endregion
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
                Entities newContext = new Entities(efConnection);
                string _ID = _passport.id.ToString().Replace("-", "");
                Module f;
                Manager.RegisterOrInjectOrNavigate("Documents", new Module(_ID, () => ucPassportViewModel.Create(), typeof(ucPassport)), new { efConnection, Context = newContext, passportID = _passport.id });
                Manager.RegisterOrInjectOrNavigate(Regions.Navigation, new Module(_ID, () => new NavigationItem(_ID)));
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
        #region Serialization
        [Serializable]
        public class Info
        {
           public string Caption { get; set; }
        }
        Info ISupportState<Info>.SaveState()
        {
            return new Info()
            {
                Caption = this.Caption,
            };
        }
        void ISupportState<Info>.RestoreState(Info state)
        {
            this.Caption = state.Caption;
        }
        #endregion
    }

}
