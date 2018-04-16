using System;
using DevExpress.Mvvm;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Input;


namespace InfConstractions.ViewModels
{
    using Config;
    using DevExpress.Mvvm.DataAnnotations;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Sql;
    using System.Runtime.InteropServices;
    using System.Security;

    public class dxwLoginViewModel : ViewModelBase

    {
        public IMessageBoxService MessageService { get { return GetService<IMessageBoxService>(); } }
        public CommandBindingCollection CommandBindings = new CommandBindingCollection();
        public Configuration Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public DefaultConnectionConfig _config_connection { get; set; }
        public Logins _config_Logins { get; set; }
        public dxwLoginViewModel()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            efConnectionStringBuilder = new EntityConnectionStringBuilder();
            ServersCollection = new ObservableCollection<string>();
            sqlConnection = new SqlConnection();
            efConnection = new EntityConnection();

            AuthenticationTypes = new List<string>();
            AuthenticationTypes.Add("Проверка подлинности SQl Server");
            AuthenticationTypes.Add("Проверка подлинности Windows");
            AuthenticationTypes.Add("Универсальная проверка подлинности Active Directory");
            AuthenticationTypes.Add("Проверка пароля Active Directory");
            AuthenticationTypes.Add("Аутентификация Active Directory");
            PropertyChanged += OnPropertyChanged1;
            LoadConfig();
        }

        public EntityConnectionStringBuilder efConnectionStringBuilder
        {
            get { return GetProperty<EntityConnectionStringBuilder>(() => efConnectionStringBuilder); }
            protected set { SetProperty<EntityConnectionStringBuilder>(() => efConnectionStringBuilder, value); }
        }
        public string Password
        {
            get { return GetProperty(() => Password); }
            protected set { SetProperty(() => Password,value); RaisePropertyChanged("Password");
                sqlConnectionStringBuilder.Password = Password;
            }
        }
        public SqlConnectionStringBuilder sqlConnectionStringBuilder;
               public SqlConnection sqlConnection
        {
            get { return GetProperty<SqlConnection>(() => sqlConnection); }
            protected set { SetProperty<SqlConnection>(() => sqlConnection, value); }
        }
        public EntityConnection efConnection
        {
            get { return GetProperty<EntityConnection>(() => efConnection); }
            protected set { SetProperty<EntityConnection>(() => efConnection, value); }
        }
        public ObservableCollection<string> ServersCollection {
            get { return GetProperty<ObservableCollection<string>>(() => ServersCollection); }
            protected set { SetProperty<ObservableCollection<string>>(() => ServersCollection, value); }
        }

        public List<string> AuthenticationTypes {
            get { return GetProperty< List <string >> (() => AuthenticationTypes); }
            protected set { SetProperty<List<string>>(() => AuthenticationTypes, value); }
        }

        public int AuthenticationType
        {
            get { return GetProperty<int>(() => AuthenticationType); }
            set { SetProperty<int>(() => AuthenticationType, value); }
        }

        public ObservableCollection<string> Logins
        {
            get { return GetProperty<ObservableCollection<string>>(() => Logins); }
            protected set { SetProperty<ObservableCollection<string>>(() => Logins, value); }
        }
        public string UserName
        {
            get { return sqlConnectionStringBuilder.UserID; }
            set
            {
                sqlConnectionStringBuilder.UserID = value;
                //RaisePropertyChanged("UserName");
            }
        }
        public string ServerName
        {
            get { return sqlConnectionStringBuilder.DataSource; }
            set { sqlConnectionStringBuilder.DataSource = value; }
        }
        public string DatabaseName
        {
            get { return sqlConnectionStringBuilder.InitialCatalog; }
            set { sqlConnectionStringBuilder.InitialCatalog = value; }
        }



        #region Methods

        public void LoadConfig()
        {
            var _config = ((Config)Config.Sections["Config"]);
            _config_connection = _config.defaultConnection;
            _config_Logins = _config.Logins;
            if (!string.IsNullOrWhiteSpace(_config_connection.DefaultDBName))
            { DatabaseName = _config_connection.DefaultDBName; }
            if (!string.IsNullOrWhiteSpace(_config_connection.DefaultServerName))
            { ServerName = _config_connection.DefaultServerName; }
            if (!string.IsNullOrWhiteSpace(_config_connection.DefaultDBAuthenticationType))
            { AuthenticationType = AuthenticationTypes.IndexOf(_config_connection.DefaultDBAuthenticationType); }
            Logins = new ObservableCollection<string>();
            foreach (Login l in _config_Logins)
            { Logins.Add(l.Name); }
        }
        public void SaveConfig()
        {
            _config_connection.DefaultServerName = ServerName;
            _config_connection.DefaultDBName = DatabaseName;
            _config_connection.DefaultDBAuthenticationType = AuthenticationTypes[AuthenticationType];
            _config_Logins.Add(UserName);
            Config.Save(ConfigurationSaveMode.Full);
        }

        /// <summary>
        /// Обновляет список доступных в сети SQL-серверов
        /// </summary>
        public void RefreshServers()
        {
            ServersCollection.Clear();
            try
            {
                //Make an initial call to the sql browser service to wake it up
                SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                DataTable dt = instance.GetDataSources();
                DataRow[] rows = dt.Select(string.Empty, "ServerName asc");
                if (rows.Length > 0)
                {
                    foreach (DataRow dr in rows)
                    {
                        string serverName = dr["ServerName"].ToString();
                        string instanceName = dr["InstanceName"].ToString();
                        if (instanceName.Length > 0)
                            serverName += "\\" + instanceName;
                        ServersCollection.Add(serverName);
                    }
                }
               
            }
            catch (Exception e)
            {
                
            }
        }

        private void OnPropertyChanged1(object sender, PropertyChangedEventArgs e)
        {
            string a = e.PropertyName;
            if (a != null)
                if (a == "AuthenticationType")
                {
                    if (AuthenticationType == 0)
                    {
                        //base.RaisePropertyChanged("AuthenticationType");
                        sqlConnectionStringBuilder.IntegratedSecurity = false;
                        sqlConnectionStringBuilder.Remove("Integrated Security");
                    }
                    else
                    {
                        //base.RaisePropertyChanged("AuthenticationType");
                        sqlConnectionStringBuilder.IntegratedSecurity = true;
                        sqlConnectionStringBuilder.Remove("User ID");
                        sqlConnectionStringBuilder.Remove("Password");
                    }
                }
        }
        #endregion

        #region Commands

        [Command(CanExecuteMethodName = "CancmConnectionStringConstructExecute",
        Name = "cmConnectionStringConstruct",
        UseCommandManager = true)]
        public void ConnectionStringConstructExecute()
        {
            sqlConnectionStringBuilder.UserID = UserName;
            sqlConnectionStringBuilder.PersistSecurityInfo = true;
            sqlConnectionStringBuilder.ConnectTimeout = 30;
            sqlConnectionStringBuilder.MultipleActiveResultSets = true;
            sqlConnectionStringBuilder.ApplicationName = App.Current.MainWindow.Title;
            if (sqlConnection.State != ConnectionState.Closed)
                { sqlConnection.Close(); }
                try
                {
                    sqlConnection.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
                    sqlConnection.Open();
                    efConnectionStringBuilder.Provider = "System.Data.SqlClient";
                    efConnectionStringBuilder.Metadata = @"res://*/Models.mainModel.csdl|res://*/Models.mainModel.ssdl|res://*/Models.mainModel.msl";
                    efConnectionStringBuilder.ProviderConnectionString = sqlConnection.ConnectionString;
                    efConnection = new EntityConnection(efConnectionStringBuilder.ToString());
                    efConnection.Open();
                    //App.mainConnection = efConnection;
                    SaveConfig();
                    //await this.SaveAndCloseViewModelAsync();
                }
                catch (SqlException e)
                {
                    MessageResult r = MessageService.ShowMessage( "Неудачная попытка соединения с сервером. Попробовать повторно?" + e.Message, "Ошибка", MessageButton.YesNo);
                    if (r.HasFlag(MessageResult.No)) { this.CommandBindings[1].Command.Execute(null); }
                }
                finally
                {
                    //App.uiVisualizerService.Unregister(typeof(formLoginViewModel));
                }
            }
        public bool CancmConnectionStringConstructExecute()
        {
            return true;
        }
        #endregion
    }
}