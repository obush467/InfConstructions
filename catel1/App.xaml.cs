namespace InfConstractions
{
    using System.Data.Entity.Core.EntityClient;
    using System.Windows;



    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    /// 

    public partial class App : Application
    {
        public static EntityConnection mainConnection;
        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            // LogManager.AddDebugListener();
#endif

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
            //Catel.Windows.StyleHelper.CreateStyleForwardersForDefaultStyles("Office2016Black");
            //var serviceLocator = ServiceLocator.Default;
            //serviceLocator.AutoRegisterTypesViaAttributes = true;
            //Log.Info((string)Current.FindResource("startMessage"));
            //uiVisualizerService = this.GetDependencyResolver().Resolve<IUIVisualizerService>();
            //MessageService = this.GetDependencyResolver().Resolve<IMessageService>();
            base.OnStartup(e);
            //Log.Debug((string)Current.FindResource("App_base_OnStartup"));
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }


        protected override void OnExit(ExitEventArgs e)
        {
            // Get advisory report in console
            //ApiCopManager.AddListener(new ConsoleApiCopListener());
            //ApiCopManager.WriteResults();
            base.OnExit(e);
        }
    }
}