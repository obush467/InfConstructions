using System;
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
    [POCOViewModel]
    public class floginViewModel
    {
        protected IMessageBoxService MessageService { get { return this.GetService<IMessageBoxService>(); } }
        #region Constructors
        public floginViewModel(SqlConnection connection): this()
        {
            Model.Connection = connection;
        }
        public floginViewModel(formLoginModel _formLoginModel)
        {
            Model = _formLoginModel;
        }
        protected floginViewModel():this(new formLoginModel())
        {
            //ValidateModelsOnInitialization = false;
            //Создание и регистрация команд 
            RefreshServersList = new DelegateCommand(cmRefreshServersList, CanRefreshServersList, false);                   
        }

        public floginViewModel(EntityConnection connection)
        {
            efConnection = connection;
            Model.Connection = (SqlConnection)efConnection.StoreConnection;
        }

        
        public static floginViewModel Create()
        { return ViewModelSource.Create<floginViewModel>(() => new floginViewModel()); }
        public static floginViewModel Create(SqlConnection connection)
        { return ViewModelSource.Create<floginViewModel>(() => new floginViewModel(connection)); }
        public static floginViewModel Create(formLoginModel _formLoginModel)
        { return ViewModelSource.Create<floginViewModel>(() => new floginViewModel(_formLoginModel)); }
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
        public string UserName
        {
            get { return Model.UserName; }
            set { Model.UserName = value; }
        }
        public string Password
        {
            get { return Model.Password; }
            set { Model.Password = value; }
        }
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
        protected void cmRefreshServersList()
        {
            // TODO: Handle command logic here
            if (ServersCollection.Count == 0)
            { Model.RefreshServers(); }
        }
        public bool CanRefreshServersList()
        { return true; }

        public async void cmConnectionStringConstruct()
        {
            ConnectionStringBuilder.PersistSecurityInfo = true;
            ConnectionStringBuilder.ConnectTimeout = 30;
            ConnectionStringBuilder.MultipleActiveResultSets = true;
            ConnectionStringBuilder.ApplicationName = App.Current.MainWindow.Title;
            //Validate(true);
            //if (!HasErrors)
            {
                if (Connection.State != System.Data.ConnectionState.Closed)
                { Connection.Close(); }
                try
                {
                    Connection.ConnectionString = ConnectionStringBuilder.ConnectionString;
                    Connection.Open();
                    efStringBuilder.Provider = "System.Data.SqlClient";
                    efStringBuilder.Metadata = @"res://*/Models.mainModel.csdl|res://*/Models.mainModel.ssdl|res://*/Models.mainModel.msl";
                    efStringBuilder.ProviderConnectionString = Connection.ConnectionString;
                    efConnection = new EntityConnection(efStringBuilder.ToString());
                    efConnection.Open();
                    SaveConfig();
                    //await this.SaveAndCloseViewModelAsync();
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
        }
        #endregion

        #region Metods
        public void SaveConfig()
        {
            Model.SaveConfig();
        }


        #endregion
    }
}