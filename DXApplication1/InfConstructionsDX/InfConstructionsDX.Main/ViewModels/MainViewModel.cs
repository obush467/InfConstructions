using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.ModuleInjection;
using DevExpress.Mvvm.POCO;
using InfConstractions.Models;
using InfConstractionsDX.Common;
using InfConstractionsDX.Modules.ViewModels;
using InfConstractionsDX.Modules.Views;
using InfConstractionsDX.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Windows;
using AppModules = InfConstractionsDX.Common.Modules;

namespace InfConstractionsDX.Main.ViewModels
{
    public class MainViewModel : ViewModelBase, IDialogService
    {
        protected IModuleManager Manager { get { return ModuleManager.DefaultManager; } }
        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }
        #region Fields
        protected Guid proverkaGU_key = new Guid("99B84C49-D814-4028-8889-6ED5E7023FF5");
        #endregion
        #region Services
        IOpenFileDialogService OpenFileDialogService { get { return GetService<IOpenFileDialogService>(); } }
        ISaveFileDialogService SaveFileDialogService { get { return GetService<ISaveFileDialogService>(); } }
        IDialogService DialogService { get { return GetService<IDialogService>(); } }
        IMessageBoxService MessageService { get { return GetService<IMessageBoxService>(); } }
        public IDocumentManagerService DocumentManagerService { get { return GetService<IDocumentManagerService>(); } }
        #endregion
        #region Constructors
        public MainViewModel():base()
        {
            try
            {
                sqlConnection = new SqlConnection();
                efConnection = new EntityConnection();
            }
            catch (Exception e)
            { MessageService.ShowMessage(e.Message); }
        }
        #endregion
        #region Properties
        public string Title { get { return "InfConstractions"; } }
        public Entities mainContext
        {
            get { return GetProperty<Entities>(() => mainContext); }
            private set { SetProperty<Entities>(() => mainContext, value); }
        }
        public SqlConnection sqlConnection
        {
            get { return GetProperty<SqlConnection>(() => sqlConnection); }
            private set { SetProperty<SqlConnection>(() => sqlConnection, value); }
        }
        public EntityConnection efConnection
        {
            get { return GetProperty<EntityConnection>(() => efConnection); }
            private set { SetProperty<EntityConnection>(() => efConnection, value); }
        }
        public Visibility vmVisibility
        {
            get { return GetProperty(() => vmVisibility); }
            set { SetProperty<Visibility>(() => vmVisibility, value); }
        }
        #endregion
        #region Methods
        void Update_mainContext()
        {
            RaisePropertyChanged(() => mainContext);
        }
        #endregion
        #region Commands

        [Command(CanExecuteMethodName = "CancmShowLoginForm",
            Name = "cmShowLoginForm",
            UseCommandManager = true)]
        public void ShowLoginForm()
        {
            dxwLoginViewModel LoginViewModel = null;
            if (LoginViewModel == null)
                LoginViewModel = ViewModelSource.Create(() => new dxwLoginViewModel());
            UICommand registerCommand = new UICommand()
            {
                Caption = "Вход",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>(
                    x => { LoginViewModel.ConnectionStringConstructExecute(); },
                    x => LoginViewModel.CancmConnectionStringConstructExecute(), true),
            };
            UICommand cancelCommand = new UICommand()
            {
                Id = DevExpress.Mvvm.MessageResult.Cancel,
                Caption = "Отмена",
                IsCancel = true,
                IsDefault = false,
            };
            UICommand result = DialogService.ShowDialog(
                dialogCommands: new List<UICommand>() { registerCommand, cancelCommand },
                title: "Соединение с сервером",
                viewModel: LoginViewModel);
            if (result == registerCommand)
            {
                efConnection = LoginViewModel.efConnection;
                sqlConnection = LoginViewModel.sqlConnection;
                mainContext = new Entities(efConnection);
            }
        }

        public bool CancmShowLoginForm()
        {
            if (efConnection != null && efConnection.State == ConnectionState.Open) return false;
            else return true;
        }
        [Command(CanExecuteMethodName = "CancmExit",
            Name = "cmExit",
            UseCommandManager = true)]
        public void Exit()
        {
            Application.Current.MainWindow.Close();
        }
        public bool CancmExit()
        {
            return true;
        }

        [Command(CanExecuteMethodName = "CancmProverkaGU",
            Name = "cmProverkaGU",
            UseCommandManager = true)]
        public void ProverkaGU()
        {
            Manager.InjectOrNavigate("Documents", "ProverkaGU", new {efConnection,mainContext});
        }
        public bool CancmProverkaGU()
        {
            if (efConnection != null && efConnection.State != System.Data.ConnectionState.Closed)
            { return true; }
            else
            { return false; }
        }
        [Command(CanExecuteMethodName = "CancmOpenRichEdit",
            Name = "cmOpenRichEdit",
            UseCommandManager = true)]
        /*public void OpenRichEdit()
        {
            DialogService.ShowDialog(null, "dsfdsfds", ViewModelSource.Create(() => new dxwLoginViewModel()));
            DocumentManagerService.FindDocumentByIdOrCreate(proverkaGU_key, (ds) =>
            {
                IDocument doc1 = ds.CreateDocument("ucWord", ViewModelSource.Create(() => new ucWordViewModel()));
                doc1.Id = proverkaGU_key.ToString();
                doc1.Title = "Документ Word";
                return doc1;
            }).Show();
        }*/
        public bool CancmOpenRichEdit()
        {
            if (efConnection != null && efConnection.State != System.Data.ConnectionState.Closed)
            { return true; }
            else
            { return false; }
        }

        public UICommand ShowDialog(IEnumerable<UICommand> dialogCommands, string title, string documentType, object viewModel, object parameter, object parentViewModel)
        {
            return DialogService.ShowDialog(dialogCommands, title, documentType, viewModel, parameter, parentViewModel);
        }

        #endregion
    }
}

