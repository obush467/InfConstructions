namespace InfConstractions
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
    using Catel;



    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    /// 

    public partial class App : Application
    {
        public static readonly ILog Log = LogManager.GetCurrentClassLogger();      
        public static IUIVisualizerService uiVisualizerService;
        public static IMessageService MessageService;
        public static formLoginViewModel vm;
        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            LogManager.AddDebugListener();
#endif

            //new Orc.Controls.Logging.LogViewerLogListener();
            //LogManager. AddListener(Orc.Controls.Logging.LogViewerLogListener);
            //var t = LogManager.GetListeners();
            Log.Info((string)Current.FindResource("startMessage"));
            uiVisualizerService = this.GetDependencyResolver().Resolve<IUIVisualizerService>();
            MessageService = this.GetDependencyResolver().Resolve<IMessageService>();

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
            //uiVisualizerService.Register<MainWindowViewModel, Views.MainWindow>();
            base.OnStartup(e);
            Log.Debug((string)Current.FindResource("App_base_OnStartup"));
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
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