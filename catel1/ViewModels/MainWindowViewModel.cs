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
    using System.Collections.Generic;
    using System.Collections;
    using Catel.Logging;

    [POCOViewModel]
    public class MainWindowViewModel : ViewModelBase
    {
        formLoginViewModel vm = new formLoginViewModel(new SqlConnection());
        IDictionary Documents = new Dictionary<string, IDocument>();
        ILog Log = LogManager.GetCurrentClassLogger();
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }
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
            catch (Exception ex)
            { MessageBox.Show("MainWindowViewModel - " + ex.Message); }
        }
        public string Title { get { return "InfConstractions"; } }

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
                { MessageBox.Show("MainWindowViewModel.completeLogin - " + ex.Message); }
                finally
                { vmVisibility = Visibility.Visible; }
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
            get { return GetProperty(()=> mainWindowModel.sqlConnection); }
            private set { SetProperty(()=> mainWindowModel.sqlConnection, value); }
        }

        public EntityConnection efConnection
        {
            get { return mainWindowModel.efConnection; }
            set { mainWindowModel.efConnection= value; }
        }

        public Visibility vmVisibility
        {
            get { return GetProperty(() => vmVisibility); }
            set { SetProperty(() => vmVisibility, value); }
        }

        #region Commands
        [Command(CanExecuteMethodName = "CanClose",
            Name = "CloseCommand",
            UseCommandManager = true)]
        public void Close ()
        {
            var l=DocumentManagerService.Documents.ToList();
            foreach(IDocument doc in l)
                {
                    Log.Info("Закрытие документа {0}",doc.Title);
                    doc.Close();
                };
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
