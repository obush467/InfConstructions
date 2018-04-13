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
    public class dxwLoginViewModel : ViewModelBase
    {
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
            LoadConfig();
        }
    

        public EntityConnectionStringBuilder efConnectionStringBuilder
        {
            get { return GetProperty<EntityConnectionStringBuilder>(() => efConnectionStringBuilder); }
            protected set { SetProperty<EntityConnectionStringBuilder>(() => efConnectionStringBuilder, value); }
        }
        public string Password
        {
            get { return sqlConnectionStringBuilder.Password; }
            set
            {
                sqlConnectionStringBuilder.Password = value;
                RaisePropertyChanged("Password");
            }
        }
        public SqlConnectionStringBuilder sqlConnectionStringBuilder
        {
            get { return GetProperty<SqlConnectionStringBuilder>(() => sqlConnectionStringBuilder); }
            protected set { SetProperty<SqlConnectionStringBuilder>(() => sqlConnectionStringBuilder, value); }
        }
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
            protected set { SetProperty<int>(() => AuthenticationType, value); }
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
                RaisePropertyChanged("UserName");
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
        #endregion
    }
}