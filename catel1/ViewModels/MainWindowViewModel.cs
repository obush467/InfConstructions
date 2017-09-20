namespace InfConstractions.ViewModels
{
    using Catel.Data;
    using Catel.MVVM;
    using Catel.Services;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Windows;
    using Models;
    using System.Data.Entity.Core.EntityClient;
    using Catel.IoC;

    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationService;
        formLoginViewModel vm = new formLoginViewModel(new SqlConnection());
        public MainWindowViewModel()
        {
            //InitializeAsync();
            var dependencyResolver = this.GetDependencyResolver();
            _uiVisualizerService = dependencyResolver.Resolve<IUIVisualizerService>();
            _pleaseWaitService = dependencyResolver.Resolve<IPleaseWaitService>();
            _messageService = dependencyResolver.Resolve<IMessageService>();
            _navigationService = dependencyResolver.Resolve<INavigationService>();
            mainWindowModel = new MainWindowModel();
            _uiVisualizerService.ShowDialogAsync(vm, completeLogin);
            #region Commands
            cmClose = new Command(OncmCloseExecute, OncmCloseCanExecute);
            #endregion
        }
        public MainWindowViewModel(IUIVisualizerService uiVisualizerService, PleaseWaitService pleaseWaitService, IMessageService messageService)
        {
            _uiVisualizerService = uiVisualizerService;
            _pleaseWaitService = pleaseWaitService;
            _messageService = messageService;
        } 

        public override string Title { get { return "InfConstractions"; } }

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            
            // TODO: subscribe to events here
        }

        private void completeLogin(object sender, UICompletedEventArgs e)
        {
            if (e.Result == true)
            {
                mainWindowModel.sqlConnection = vm.Connection;
                mainWindowModel.efConnection= vm.efConnection;
            }
            else { App.Current.Shutdown(-1); }
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [ViewModelToModel("mainWindowModel")]
        public Entities mainContext
        {
            get { return GetValue<Entities>(mainContextProperty); }
            set { SetValue(mainContextProperty, value); }
        }

        /// <summary>
        /// Register the TTTTT property so it is known in the class.
        /// </summary>
        public static readonly PropertyData mainContextProperty = RegisterProperty("mainContext", typeof(Entities), null);


        [Model]
        public MainWindowModel mainWindowModel
        {
            get { return GetValue<MainWindowModel>(mainWindowModelProperty); }
            set { SetValue(mainWindowModelProperty, value); }
        }

        public static readonly PropertyData mainWindowModelProperty = RegisterProperty(nameof(mainWindowModel), typeof(MainWindowModel));

        [ViewModelToModel("mainWindowModel")]
        public SqlConnection sqlConnection
        {
            get { return GetValue<SqlConnection>(sqlConnectionProperty); }
            private set { SetValue(sqlConnectionProperty, value); }
        }

        public static readonly PropertyData sqlConnectionProperty = RegisterProperty(nameof(sqlConnection), typeof(SqlConnection));

        [ViewModelToModel("mainWindowModel")]
        public EntityConnection efConnection
        {
            get { return GetValue<EntityConnection>(efConnectionProperty); }
            set { SetValue(efConnectionProperty, value); }
        }

        public static readonly PropertyData efConnectionProperty = RegisterProperty(nameof(efConnection), typeof(EntityConnection));


        #region Commands

        public Command cmClose { get; private set; }



        private bool OncmCloseCanExecute()
        {
            return true;
        }

        private async void OncmCloseExecute()
        {
            await this.SaveAndCloseViewModelAsync();
        }

        #endregion
    }
}
