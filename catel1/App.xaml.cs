namespace InfConstractions
{
    using System.Windows;

    using Models;
    using ViewModels;
    using System;
    using Views;
    using System.Data.SqlClient;
    using System.IO;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Text;
    using System.Runtime.Serialization;
    using Catel;
    using Catel.Logging;
    using Catel.Services;
    using Catel.IoC;
    using Catel.ApiCop;
    using Catel.ApiCop.Listeners;
    using System.Data.Entity.Core.EntityClient;


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    /// 

    public partial class App : Application
    {
        public static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static Entities _mainDBContext;
        public static SqlConnection Connection;

        public DBDefault DBDefault  { get; set; }
        private static readonly JsonSerializerSettings JSSettings = new JsonSerializerSettings();

        public static IUIVisualizerService uiVisualizerService;
        public static IMessageService MessageService;

        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            LogManager.AddDebugListener();
#endif

            Log.Info("Starting application");
            uiVisualizerService = this.GetDependencyResolver().Resolve<IUIVisualizerService>();
            MessageService = this.GetDependencyResolver().Resolve<IMessageService>();
            

            // To force the loading of all assemblies at startup, uncomment the lines below:

            //Log.Info("Preloading assemblies");
            //AppDomain.CurrentDomain.PreloadAssemblies();


            // Want to improve performance? Uncomment the lines below. Note though that this will disable
            // some features. 
            //
            // For more information, see https://catelproject.atlassian.net/wiki/display/CTL/Performance+considerations

            // Log.Info("Improving performance");
            // Catel.Data.ModelBase.DefaultSuspendValidationValue = true;
            // Catel.Windows.Controls.UserControl.DefaultCreateWarningAndErrorValidatorForViewModelValue = false;
            // Catel.Windows.Controls.UserControl.DefaultSkipSearchingForInfoBarMessageControlValue = true;


            // TODO: Register custom types in the ServiceLocator
            //Log.Info("Registering custom types");
            //var serviceLocator = ServiceLocator.Default;
            //serviceLocator.RegisterType<IMyInterface, IMyClass>();

            //StyleHelper.CreateStyleForwardersForDefaultStyles();

            base.OnStartup(e);

            Log.Info("Calling base.OnStartup");          
        }
        public void Login(object sender, StartupEventArgs e)
        {
            Connection = new SqlConnection();
            
            var vm = new formLoginViewModel(Connection);
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            if (uiVisualizerService.ShowDialog(vm) == true)            
            {
                //Re-enable normal shutdown mode.
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                    _mainDBContext = new Entities(vm.efConnection);
                }
                else
                {                    
                    MessageBox.Show("Сбой при подключении к серверу.", "Ошибка", MessageBoxButton.OK);
                }
                    //Current.Shutdown(-1);
                }
            else
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                { Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                    _mainDBContext = new Entities(vm.efConnection);
                    Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                }
                else { Current.Shutdown(-1); }
                
            }
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