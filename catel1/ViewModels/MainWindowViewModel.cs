namespace InfConstractions.ViewModels
{
    using System.Threading.Tasks;
    using System.Data.Entity;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Windows;
    using Models;
    using System.Data.Entity.Core.EntityClient;
    using System;
    using DevExpress.Xpf.Docking;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.ViewModel;
    using DevExpress.Mvvm.POCO;

    using Catel.Services;
    using Catel.IoC;
    using DevExpress.Mvvm.DataAnnotations;

    [POCOViewModel]
    public class MainWindowViewModel : DevExpress.Mvvm.ViewModelBase
    {
        formLoginViewModel vm = new formLoginViewModel(new SqlConnection());
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }
        public MainWindowViewModel()
        {
            vmVisibility = Visibility.Hidden;
            mainWindowModel = new MainWindowModel();
            var u = this.GetDependencyResolver().Resolve<IUIVisualizerService>();
            u.ShowDialogAsync(vm, completeLogin);      
            #region CommandsCreate

            #endregion
        }
        public string Title { get { return "InfConstractions"; } }

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

        public Entities mainContext
        {
            get { return mainWindowModel.mainContext; }
          
            set { mainWindowModel.mainContext=value; }
        }

        void Update_mainContext()
        {
            RaisePropertyChanged(() => mainContext);
        }

        public MainWindowModel mainWindowModel;

        public SqlConnection sqlConnection
        {
            get { return GetProperty<SqlConnection>(()=> mainWindowModel.sqlConnection); }
            private set { SetProperty<SqlConnection>(()=> mainWindowModel.sqlConnection, value); }
        }

        public EntityConnection efConnection
        {
            get { return mainWindowModel.efConnection; }
            set { mainWindowModel.efConnection= value; }
        }

        public Visibility vmVisibility
        {
            get { return GetProperty<System.Windows.Visibility>(() => vmVisibility); }
            set { SetProperty<Visibility>(() => vmVisibility, value); }
        }

        #region Commands
        public  async void Close ()
        {
            //_navigationService.CloseApplication();
        }
        public bool CanClose()
        {
            return true;
        }

        [Command(CanExecuteMethodName = "CanProverkaGU",
            Name = "ProverkaGUCommand",
            UseCommandManager = true)]
        public async void ProverkaGU()
        {
                IDocument doc1;
                doc1 = DocumentManagerService.CreateDocument("ProverkaGUView", ViewModelSource.Create(() => new ProverkaGUViewModel(mainContext)));
                doc1.Id = DocumentManagerService.Documents.Count<IDocument>();
                doc1.Title = "Проверка ГУ";
                doc1.Show();
        }
        public bool CanProverkaGU()
        {
            if (efConnection.State != System.Data.ConnectionState.Closed)
            { return true; }
            else
            { return false; }
        }
        #endregion
    }
}
