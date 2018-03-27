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
    using Services;

    public class MainWindowViewModel : ViewModelBase
    {
        IPassportService PassportService { get { return GetService<IPassportService>(); } }
        formLoginViewModel vm = new formLoginViewModel(new SqlConnection());
        protected IDocumentManagerService DocumentManagerService { get { return GetService<IDocumentManagerService>(); } }
        #region Constructors
        public MainWindowViewModel()
        {
            try
            {
                vmVisibility = Visibility.Hidden;
                mainWindowModel = new MainWindowModel();
                var u = this.GetDependencyResolver().Resolve<IUIVisualizerService>();
                u.ShowDialogAsync(vm, completeLogin);
                #region CommandsCreate

                #endregion
            }
            catch (Exception)
            { }
        }
        #endregion
        #region Properties
        public string Title { get { return "InfConstractions"; } }
        public Entities mainContext
        {
            get { return mainWindowModel.mainContext; }       
            set { mainWindowModel.mainContext=value; }
        }
        public MainWindowModel mainWindowModel { get; set;}
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
        #endregion
        #region Methods
        void Update_mainContext()
        {
            RaisePropertyChanged(() => mainContext);
        }
        private void completeLogin(object sender, UICompletedEventArgs e)
        {
            if (e.Result == true)
            {
                try
                {
                    mainWindowModel.sqlConnection = vm.Connection;
                    mainWindowModel.efConnection = vm.efConnection;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                finally
                { vmVisibility = Visibility.Visible; }

            }
            else { Application.Current.Shutdown(-1); }
        }
        #endregion
        #region Commands
        public void Close()
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
        public void ProverkaGU()
        {
                IDocument doc1;
                doc1 = DocumentManagerService.CreateDocument("ProverkaGUView", ViewModelSource.Create(() => new ProverkaGUViewModel(mainContext, DocumentManagerService)));
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
