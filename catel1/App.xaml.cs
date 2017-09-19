﻿namespace InfConstractions
{
    using System.Windows;
    using Models;
    using ViewModels;
    using System.Data.SqlClient;
    using Catel.Logging;
    using Catel.Services;
    using Catel.IoC;
    using Catel.ApiCop;
    using Catel.ApiCop.Listeners;



    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    /// 

    public partial class App : Application
    {
        public static readonly ILog Log = LogManager.GetCurrentClassLogger();
        
        public static SqlConnection Connection = new SqlConnection();
        public static IUIVisualizerService uiVisualizerService;
        public static IMessageService MessageService;
        public static formLoginViewModel vm = new formLoginViewModel(Connection);
        public static Entities _mainDBContext;
        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            Catel.Logging.LogManager.AddDebugListener();           
#endif
            MessageService = this.GetDependencyResolver().Resolve<IMessageService>();
            //new Orc.Controls.Logging.LogViewerLogListener();
            //LogManager. AddListener(Orc.Controls.Logging.LogViewerLogListener);
            //var t = LogManager.GetListeners();
            Log.Info((string)Current.FindResource("startMessage"));           
            uiVisualizerService = this.GetDependencyResolver().Resolve<IUIVisualizerService>();
           

            // To force the loading of all assemblies at startup, uncomment the lines below:

           // Log.Debug((string)Current.FindResource("Preloading_assemblies"));
            //AppDomain.CurrentDomain.PreloadAssemblies();


            // Want to improve performance? Uncomment the lines below. Note though that this will disable
            // some features. 
            //
            // For more information, see https://catelproject.atlassian.net/wiki/display/CTL/Performance+considerations

            //Log.Info("Improving performance");
            // Catel.Data.ModelBase.DefaultSuspendValidationValue = true;
            // Catel.Windows.Controls.UserControl.DefaultCreateWarningAndErrorValidatorForViewModelValue = false;
            // Catel.Windows.Controls.UserControl.DefaultSkipSearchingForInfoBarMessageControlValue = true;


            // TODO: Register custom types in the ServiceLocator
            //Log.Info("Registering custom types");
            //var serviceLocator = ServiceLocator.Default;
            //serviceLocator.RegisterType<IMyInterface, IMyClass>();

            //StyleHelper.CreateStyleForwardersForDefaultStyles();

            base.OnStartup(e);
            Log.Debug((string)Current.FindResource("App_base_OnStartup"));          
        }
        public void Login(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            uiVisualizerService.ShowDialogAsync(vm, completeLogin);          
        }

        private void completeLogin(object sender, UICompletedEventArgs e)
        {
            if (e.Result == true)
            {
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                _mainDBContext = new Entities(vm.efConnection);
                uiVisualizerService.ShowOrActivateAsync<MainWindowViewModel>(_mainDBContext, null, null);
            }
            else {Current.Shutdown(-1); }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Get advisory report in console
            ApiCopManager.AddListener(new ConsoleApiCopListener());
            ApiCopManager.WriteResults();
            base.OnExit(e);
        }
    }
}