using System.Collections.Generic;
using Catel.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Configuration;

namespace InfConstractions.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;
    using Models;
    using Catel.Services;
    using Catel;
    using System.Data.Entity.Core.EntityClient;
    using Config;
    using System.Data.Sql;
    using System.Data;
    using Catel.Logging;
    using System.Windows;
    using System;

    public class formLoginViewModel : ViewModelBase
    {
        public CommandBindingCollection CommandBindings = new CommandBindingCollection();
        public Configuration Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public DefaultConnectionConfig _config_connection { get; set; }
        public Logins _config_Logins { get; set; }
        #region Constructors
        public formLoginViewModel ()
        {
            //ValidateModelsOnInitialization = false;
            SuspendValidations(true);
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            efConnectionStringBuilder = new EntityConnectionStringBuilder();
            ServersCollection = new ObservableCollection<string>();
            sqlConnection = new SqlConnection();
            efConnection = new EntityConnection();
            #region CONFIGURATION
            AuthenticationTypes = new List<string>();
            AuthenticationTypes.Add("Проверка подлинности SQl Server");
            AuthenticationTypes.Add("Проверка подлинности Windows");
            AuthenticationTypes.Add("Универсальная проверка подлинности Active Directory");
            AuthenticationTypes.Add("Проверка пароля Active Directory");
            AuthenticationTypes.Add("Аутентификация Active Directory");
            LoadConfig();
            //Создание и регистрация команд
            ICommandManager cManager = new CommandManager();
            cmRefreshServersList = new Command(OncmRefreshServersListExecute, OncmRefreshServersListCanExecute);
            cManager.CreateCommand("cmRefreshServersList");
            cManager.RegisterCommand("cmRefreshServersList", cmRefreshServersList,this);
            cmConnectionStringConstruct = new Command(OncmConnectionStringConstructExecute);
            cManager.CreateCommand("cmConnectionStringConstruct");
            cManager.RegisterCommand("cmConnectionStringConstruct", cmConnectionStringConstruct, this);
            #endregion
            //Заполнение CommandBindings
            CommandBindings.Add(new CommandBinding(cmRefreshServersList));
            CommandBindings.Add(new CommandBinding(cmConnectionStringConstruct));
        }

        public formLoginViewModel(EntityConnection connection)
        {
            this.connection = connection;
        }
        #endregion
        #region Commands
        public Command cmRefreshServersList { get; private set; }
        private bool OncmRefreshServersListCanExecute()
        {return true;}
        private void OncmRefreshServersListExecute()
        {
            if (ServersCollection.Count == 0)
            { RefreshServers();}
        }
        public Command cmConnectionStringConstruct { get; set; }
        private async void OncmConnectionStringConstructExecute()
       {
            sqlConnectionStringBuilder.PersistSecurityInfo = true; 
            sqlConnectionStringBuilder.ConnectTimeout = 30;           
            sqlConnectionStringBuilder.MultipleActiveResultSets = true;
            sqlConnectionStringBuilder.ApplicationName = App.Current.MainWindow.Title;
            Validate(true);
            if (!HasErrors)
            {
                if (sqlConnection.State != ConnectionState.Closed)
                { sqlConnection.Close(); }
                try
                {
                    sqlConnection.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
                    sqlConnection.Open();
                    efConnectionStringBuilder.Provider = "System.Data.SqlClient";
                    efConnectionStringBuilder.Metadata = @"res://*/Models.mainModel.csdl|res://*/Models.mainModel.ssdl|res://*/Models.mainModel.msl";
                    efConnectionStringBuilder.ProviderConnectionString = sqlConnection.ConnectionString;                   
                    efConnection=new EntityConnection (efConnectionStringBuilder.ToString());
                    efConnection.Open();
                    App.mainConnection = efConnection;
                    SaveConfig();
                    await this.SaveAndCloseViewModelAsync();
                }
                catch (SqlException e)
                { MessageResult r =await App.MessageService.ShowAsync("Неудачная попытка соединения с сервером. Попробовать повторно?" + e.Message,"Ошибка",MessageButton.YesNo);
                    if (r.HasFlag(MessageResult.No)) {await CloseViewModelAsync(false); } }
                finally
                {
                    //App.uiVisualizerService.Unregister(typeof(formLoginViewModel));
                }
            }
        }
        #endregion

        #region Properties
        public string DatabaseName
        {
            get { return sqlConnectionStringBuilder.InitialCatalog; }
            set { sqlConnectionStringBuilder.InitialCatalog = value; }
        }

        public static readonly PropertyData DatabaseNameProperty = RegisterProperty("DatabaseName", typeof(string), null);
        public string ServerName
        {
            get { return sqlConnectionStringBuilder.DataSource; }
            set { sqlConnectionStringBuilder.DataSource = value; }
        }
        public static readonly PropertyData ServerNameProperty = RegisterProperty("ServerName", typeof(string), null);
        public int AuthenticationType
        {
            get { return GetValue<int>(AuthenticationTypeProperty); }
            set { SetValue(AuthenticationTypeProperty, value); }
        }

        public static readonly PropertyData AuthenticationTypeProperty = RegisterProperty("AuthenticationType", typeof(int), null);
        public List<string> AuthenticationTypes
        {
            get { return GetValue<List<string>>(AuthenticationTypesProperty); }
            set { SetValue(AuthenticationTypesProperty, value); }
        }

        public static readonly PropertyData AuthenticationTypesProperty = RegisterProperty("AuthenticationTypes", typeof(List<string>), null);
        public ObservableCollection<string> Logins
        {
            get { return GetValue<ObservableCollection<string>>(LoginsProperty); }
            private set { SetValue(LoginsProperty, value); }
        }

        public static readonly PropertyData LoginsProperty = RegisterProperty(nameof(Logins), typeof(ObservableCollection<string>), null);

        public EntityConnectionStringBuilder efConnectionStringBuilder
        {
            get { return GetValue<EntityConnectionStringBuilder>(efConnectionStringBuilderProperty); }
            set { SetValue(efConnectionStringBuilderProperty, value); }
        }
        /// <summary>
        /// Register the ecStringBuild property so it is known in the class.
        /// </summary>
        public static readonly PropertyData efConnectionStringBuilderProperty = RegisterProperty("efConnectionStringBuilder", typeof(EntityConnectionStringBuilder), null);

        public SqlConnection sqlConnection
        {
            get { return GetValue<SqlConnection>(sqlConnectionProperty); }
            set { SetValue(sqlConnectionProperty, value); }
        }

        /// <summary>
        /// Register the Connection property so it is known in the class.
        /// </summary>
        public static readonly PropertyData sqlConnectionProperty = RegisterProperty("sqlConnection", typeof(SqlConnection), null);
        public string Password
        {
            get { return sqlConnectionStringBuilder.Password; }
            set
            {
                sqlConnectionStringBuilder.Password = value;
                RaisePropertyChanged("Password");
            }
        }
        public static readonly PropertyData PasswordProperty = RegisterProperty("Password", typeof(string), null);

        public SqlConnectionStringBuilder sqlConnectionStringBuilder
        {
            get { return GetValue<SqlConnectionStringBuilder>(sqlConnectionStringBuilderProperty); }
            set { SetValue(sqlConnectionStringBuilderProperty, value); }
        }
        public static readonly PropertyData sqlConnectionStringBuilderProperty = RegisterProperty("sqlConnectionStringBuilder", typeof(SqlConnectionStringBuilder), null);

        public string UserName
        {
            get { return sqlConnectionStringBuilder.UserID; }
            set
            {
                sqlConnectionStringBuilder.UserID = value;
                RaisePropertyChanged("UserName");
            }
        }

        public static readonly PropertyData UserNameProperty = RegisterProperty("UserName", typeof(string), null);


        public ObservableCollection<string> ServersCollection
        {
            get { return GetValue<ObservableCollection<string>>(ServersCollectionProperty); }
            private set { SetValue(ServersCollectionProperty, value); }
        }
        public static readonly PropertyData ServersCollectionProperty = RegisterProperty("ServersCollection", typeof(ObservableCollection<string>), null);

        public EntityConnection efConnection
        {
            get { return GetValue<EntityConnection>(efConnectionProperty); }
            set { SetValue(efConnectionProperty, value); }
        }

        /// <summary>
        /// Register the efConnection property so it is known in the class.
        /// </summary>
        public static readonly PropertyData efConnectionProperty = RegisterProperty("efConnection", typeof(EntityConnection), null);

        private EntityConnection connection;
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>

        #endregion

        #region Metods
        protected override void ValidateFields(List<IFieldValidationResult> validationResults)
        {
            if (string.IsNullOrWhiteSpace(DatabaseName))
            {
                validationResults.Add(FieldValidationResult.CreateError(DatabaseNameProperty, "Требуется имя базы данных"));
            }
            if (string.IsNullOrWhiteSpace(ServerName))
            {
                validationResults.Add(FieldValidationResult.CreateError(ServerNameProperty, "Требуется имя сервера"));
            }
            if ((AuthenticationType == 0))
            {
                if (string.IsNullOrWhiteSpace(UserName))
                { validationResults.Add(FieldValidationResult.CreateError(UserNameProperty, "Требуется имя входа")); }
                if (string.IsNullOrWhiteSpace(Password))
                { validationResults.Add(FieldValidationResult.CreateError(PasswordProperty, "Требуется пароль")); }
            }
        }

        protected override void OnPropertyChanged(AdvancedPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            string a = e.PropertyName;
            if (a != null)
                if (a == "AuthenticationType")
                {
                    if ((int)e.NewValue == 0)
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
                App.Log.Debug((string)Application.Current.FindResource("refreshServers"));
            }
            catch (Exception e)
            {
                App.Log.Error(e, e.Message);
            }
        }
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
        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }
        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here
            await base.CloseAsync();
        }
        #endregion

    }
}

    

