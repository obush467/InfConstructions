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
    using Views;
    using System;
    using DevExpress.Xpf.Docking;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.ViewModel;
    using DevExpress.Mvvm.POCO;

    public class MainWindowViewModel : Catel.MVVM.ViewModelBase
    {
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IMessageService _messageService;
        private readonly Catel.Services.INavigationService _navigationService;
        formLoginViewModel vm = new formLoginViewModel(new SqlConnection());
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }
        public MainWindowViewModel()
        {
            //InitializeAsync();
            vmVisibility = Visibility.Hidden;
            var dependencyResolver = this.GetDependencyResolver();
            _uiVisualizerService = dependencyResolver.Resolve<IUIVisualizerService>();
            _pleaseWaitService = dependencyResolver.Resolve<IPleaseWaitService>();
            _messageService = dependencyResolver.Resolve<IMessageService>();
            _navigationService = dependencyResolver.Resolve<Catel.Services.INavigationService>();
            mainWindowModel = new MainWindowModel();
            _uiVisualizerService.ShowDialogAsync(vm, completeLogin);
            
            #region CommandsCreate
            cmClose = new Command(OncmCloseExecute, OncmCloseCanExecute);
            cmProverkaGU = new Command(OncmProverkaGUExecute, OncmProverkaGUCanExecute);
            cmP = new Command(OncmPExecute);
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
                vmVisibility = Visibility.Visible;
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



        public Visibility vmVisibility
        {
            get { return GetValue<System.Windows.Visibility>(vmVisibilityProperty); }
            set { SetValue(vmVisibilityProperty, value); }
        }

        public static readonly PropertyData vmVisibilityProperty = RegisterProperty(nameof(vmVisibility), typeof(Visibility), null);

        #region Commands

        public Command cmClose { get; private set; }
        private bool OncmCloseCanExecute()
        {
            return true;
        }

        private async void OncmCloseExecute()
        {
            await this.SaveAndCloseViewModelAsync();
            _navigationService.CloseApplication();
        }


        public Command cmProverkaGU { get; private set; }

        // TODO: Move code below to constructor
        
        // TODO: Move code above to constructor

        private bool OncmProverkaGUCanExecute()
        {
            if (efConnection.State != System.Data.ConnectionState.Closed)
            { return true; }
            else
            { return false; }
        }

        private async void OncmProverkaGUExecute()
        {
            await App.MessageService.ShowAsync("Неудачная попытка соединения с сервером. Попробовать повторно?" , "Ошибка", Catel.Services.MessageButton.YesNo);
            var s = Guid.NewGuid().ToString();
            ProverkaGUView n = ServiceLocator.Default.ResolveTypeUsingParameters<ProverkaGUView>(new object[] { mainContext }, null);
            var dependencyResolver = this.GetDependencyResolver();
            MainWindow mw = (MainWindow)App.Current.MainWindow;
            DocumentPanel doc = (mw).dockManager_main.DockController.AddDocumentPanel(mw.docPanel);
            doc.Caption = "Проверка ГУ";
            doc.Content = n;
            IDocument doc1;
            doc1 = DocumentManagerService.CreateDocument("Page1", this);
            doc1.Id = DocumentManagerService.Documents.Count<IDocument>();
            doc1.Show();
            doc.IsActive = true;
            
        }


        public Command cmP { get; private set; }


        private void OncmPExecute()
        {
            Title="xvsddsfsfsdf";
            _messageService.ShowAsync("xvsddsfsfsdf");
            IDocument doc;
                doc = DocumentManagerService.CreateDocument("PageView", this);
                doc.Id = DocumentManagerService.Documents.Count<IDocument>();
                doc.Show();
        }

        #endregion
    }
}
