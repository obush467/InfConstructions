using System;
using System.Windows;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using InfConstractions.ViewModels;
using System.Data.Entity.Core.EntityClient;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Collections.Generic;
using InfConstractions.Models;

namespace InfConstractions.ViewModels
{

    public class floginViewModel:ViewModelBase
    {
        private string _userName;

        protected IMessageBoxService MessageService { get { return this.GetService<IMessageBoxService>(); } }
        protected ICurrentDialogService DialogService { get { return this.GetService<ICurrentDialogService>(); } }
        #region Constructors
        public floginViewModel(SqlConnection connection)
        {
            Model = new formLoginModel();
            Model.Connection = connection;          
            //ValidateModelsOnInitialization = false;
            //Создание и регистрация команд 
            RefreshServersList = new DelegateCommand(cmRefreshServersList, CanRefreshServersList, false);
            ConnectionStringConstruct = new DelegateCommand(cmConnectionStringConstruct, true);
            Close = new DelegateCommand(cmClose, CancmClose, false);
        }

        public floginViewModel(EntityConnection connection)
        {
            efConnection = connection;
            Model.Connection = (SqlConnection)efConnection.StoreConnection;
        }

        
        public static floginViewModel Create(SqlConnection connection)
        { return ViewModelSource.Create<floginViewModel>(() => new floginViewModel(connection)); }
        #endregion
        #region Properties
        public EntityConnectionStringBuilder efStringBuilder { get { return Model.efStringBuilder; }}
        public int AuthenticationType
        {
            get { return Model.AuthenticationType; }
            set { Model.AuthenticationType = value; }
        }
        public List<string> AuthenticationTypes { get { return Model.AuthenticationTypes; } }

        protected formLoginModel Model { get; set; }

        public ObservableCollection<string> ServersCollection { get { return Model.ServersCollection; }}

        public SqlConnection Connection { get { return Model.Connection; }}

        /// <summary>
        /// Register the Connection property so it is known in the class.
        /// </summary>
        public string ServerName
        {
            get { return Model.ServerName; }
            set { Model.ServerName = value; }
        }
        public string userName
        { get { return Model.UserName;} set { Model.UserName = value; }}
        public string Password
        { get { return Model.Password; } set { Model.Password = value; } }
        public string DatabaseName
        {
            get { return Model.DatabaseName; }
            set { Model.DatabaseName = value; }
        }

        public SqlConnectionStringBuilder ConnectionStringBuilder { get { return Model.ConnectionStringBuilder; } }

        public EntityConnection efConnection { get { return Model.efConnection; } set { Model.efConnection = value; } }

        /// <summary>
        /// Register the efConnection property so it is known in the class.
        /// </summary>
        public ObservableCollection<string> Logins { get { return Model.Logins; }}

        #endregion

        #region Commands

        public DelegateCommand RefreshServersList { get; private set; }
        public DelegateCommand ConnectionStringConstruct { get; private set; }
        public DelegateCommand Close { get; private set; }
        #endregion

        #region Metods

        private bool CancmClose()
        {
            return true;
        }

        private void cmClose()
        {
            //IDialogService f;
            //f.ShowDialog()
            DialogService.Close();
        }
        protected void cmRefreshServersList()
        {
            // TODO: Handle command logic here
            if (ServersCollection.Count == 0)
            { Model.RefreshServers(); }
        }
        public bool CanRefreshServersList()
        { return true; }

        public void cmConnectionStringConstruct()
        {          
                try
                {
                    ConnectionStringBuilder.PersistSecurityInfo = true;
                    ConnectionStringBuilder.ConnectTimeout = 30;
                    ConnectionStringBuilder.MultipleActiveResultSets = true;
                    ConnectionStringBuilder.ApplicationName = Application.Current.MainWindow.Title;
                    ConnectionStringBuilder.UserID = userName;
                    ConnectionStringBuilder.Password = Password;
                    Connection.ConnectionString = ConnectionStringBuilder.ConnectionString;
                    Connection.Open();
                    efStringBuilder.Provider = "System.Data.SqlClient";
                    efStringBuilder.Metadata = @"res://*/Models.mainModel.csdl|res://*/Models.mainModel.ssdl|res://*/Models.mainModel.msl";
                    efStringBuilder.ProviderConnectionString = Connection.ConnectionString;
                    efConnection = new EntityConnection(efStringBuilder.ToString());
                    efConnection.Open();
                    SaveConfig();
                    Close.Execute(null);
                }
                catch (SqlException e)
                {
                    MessageResult r = MessageService.ShowMessage("Неудачная попытка соединения с сервером. Попробовать повторно?" + e.Message, "Ошибка", MessageButton.YesNo);
                    if (r.HasFlag(MessageResult.No))
                    {
                        //await CloseViewModelAsync(false); 
                    }
                }
                finally
                {
                    //App.uiVisualizerService.Unregister(typeof(formLoginViewModel));
                }
            
        }
        protected bool CancmConnectionStringConstruct()
        { return true; }
        

        
        public void SaveConfig()
        {
            Model.SaveConfig();
        }


        #endregion
    }
}