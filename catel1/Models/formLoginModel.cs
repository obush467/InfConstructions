using System;
using System.Collections.Generic;
using Catel.Data;
using System.Data.Sql;
using System.Data;
using System.Collections.ObjectModel;
using Catel.Logging;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.EntityClient;
using System.Configuration;
using System.Windows;
using InfConstractions.Config;

namespace InfConstractions.Models
{
    public class formLoginModel : ValidatableModelBase
    {
        public Configuration Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public DefaultConnectionConfig _config_connection { get; set; }
        public Config.Logins _config_Logins { get; set; }
        public formLoginModel()
        {
            SuspendValidations(true);
            ConnectionStringBuilder = new SqlConnectionStringBuilder();
            ServersCollection = new ObservableCollection<string>();
            efStringBuilder = new EntityConnectionStringBuilder();
            efConnection = new EntityConnection();
            #region CONFIGURATION
            
            AuthenticationTypes = new List<string>();
            AuthenticationTypes.Add("Проверка подлинности SQl Server");
            AuthenticationTypes.Add("Проверка подлинности Windows");
            AuthenticationTypes.Add("Универсальная проверка подлинности Active Directory");
            AuthenticationTypes.Add("Проверка пароля Active Directory");
            AuthenticationTypes.Add("Аутентификация Active Directory");
            LoadConfig();
            #endregion
            SuspendValidations(false);
            //Validate(true);            
        }

#if NETFX_CORE
[DataMember]
// TODO: use the following line instead of the one above, if you want to ignore the property for (de)serialization
//[IgnoreDataMember]
#endif
        [Required]
        public int AuthenticationType
        {
            get { return GetValue<int>(AuthenticationTypeProperty); }
            set { SetValue(AuthenticationTypeProperty, value); }
        }

        /// <summary>
        /// Register the AuthenticationTypeDefault property so it is known in the class.
        /// </summary>
        public static readonly PropertyData AuthenticationTypeProperty = RegisterProperty("AuthenticationType", typeof(int), null);

#if NETFX_CORE
[DataMember]
// TODO: use the following line instead of the one above, if you want to ignore the property for (de)serialization
//[IgnoreDataMember]
#endif
        public List<string> AuthenticationTypes
        {
            get { return GetValue<List<string>>(AuthenticationTypesProperty); }
            set { SetValue(AuthenticationTypesProperty, value); }
        }
        public static readonly PropertyData AuthenticationTypesProperty = RegisterProperty("AuthenticationTypes", typeof(List<string>), null);
        /// <summary>
            /// Имя сервера.
            /// </summary>
#if NETFX_CORE
[DataMember]
// TODO: use the following line instead of the one above, if you want to ignore the property for (de)serialization
//[IgnoreDataMember]
#endif
       [Required]
       public string ServerName
        {
            get { return ConnectionStringBuilder.DataSource; }
            set { ConnectionStringBuilder.DataSource= value; }
        }
        public static readonly PropertyData ServerNameProperty = RegisterProperty("ServerName", typeof(string), null);

#if NETFX_CORE
[DataMember]
// TODO: use the following line instead of the one above, if you want to ignore the property for (de)serialization
//[IgnoreDataMember]
#endif
        [Required]
        public string DatabaseName
        {
            get { return ConnectionStringBuilder.InitialCatalog; }
            set { ConnectionStringBuilder.InitialCatalog= value; }
        }

        public static readonly PropertyData DatabaseNameProperty = RegisterProperty("DatabaseName", typeof(string), null);
        public SqlConnectionStringBuilder ConnectionStringBuilder
        {
            get { return GetValue<SqlConnectionStringBuilder>(ConnectionStringBuilderProperty); }
            set { SetValue(ConnectionStringBuilderProperty, value); }
        }
        public static readonly PropertyData ConnectionStringBuilderProperty = RegisterProperty("ConnectionStringBuilder", typeof(SqlConnectionStringBuilder), null);

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
#if NETFX_CORE
[DataMember]
// TODO: use the following line instead of the one above, if you want to ignore the property for (de)serialization
//[IgnoreDataMember]
#endif
        public string UserName
        {
            get { return ConnectionStringBuilder.UserID; }
            set { ConnectionStringBuilder.UserID= value;
                RaisePropertyChanged("UserName");
                }
        }

        /// <summary>
        /// Register the UserName property so it is known in the class.
        /// </summary>
        public static readonly PropertyData UserNameProperty = RegisterProperty("UserName", typeof(string), null);
#if NETFX_CORE
[DataMember]
// TODO: use the following line instead of the one above, if you want to ignore the property for (de)serialization
//[IgnoreDataMember]
#endif
        public string Password
        {
            get { return ConnectionStringBuilder.Password; }
            set {
                    ConnectionStringBuilder.Password= value;
                    RaisePropertyChanged("Password");
                }
        }
        public static readonly PropertyData PasswordProperty = RegisterProperty("Password", typeof(string), null);
        public ObservableCollection<string> ServersCollection
        {
            get { return GetValue<ObservableCollection<string>>(ServersCollectionProperty); }
            private set { SetValue(ServersCollectionProperty, value); }        
        }
        public static readonly PropertyData ServersCollectionProperty = RegisterProperty("ServersCollection", typeof(ObservableCollection<string>), null);

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        /// 


        public ObservableCollection<string> Logins
        {
            get { return GetValue<ObservableCollection<string>>(LoginsProperty); }
            private set { SetValue(LoginsProperty, value); }
        }

        public static readonly PropertyData LoginsProperty = RegisterProperty(nameof(Logins), typeof(ObservableCollection<string>), null);
#if NETFX_CORE
[DataMember]
// TODO: use the following line instead of the one above, if you want to ignore the property for (de)serialization
//[IgnoreDataMember]
#endif
        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
#if NETFX_CORE
[DataMember]
// TODO: use the following line instead of the one above, if you want to ignore the property for (de)serialization
//[IgnoreDataMember]
#endif
        public EntityConnection efConnection
        {
            get { return GetValue<EntityConnection>(efConnectionProperty); }
            set { SetValue(efConnectionProperty, value); }
        }

        /// <summary>
        /// Register the efConnection property so it is known in the class.
        /// </summary>
        public static readonly PropertyData efConnectionProperty = RegisterProperty("efConnection", typeof(EntityConnection), null);
        public SqlConnection Connection
        {
            get { return GetValue<SqlConnection>(ConnectionProperty); }
            set { SetValue(ConnectionProperty, value); }
        }

        /// <summary>
        /// Register the Connection property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ConnectionProperty = RegisterProperty("Connection", typeof(SqlConnection), null);

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        public EntityConnectionStringBuilder efStringBuilder
        {
            get { return GetValue<EntityConnectionStringBuilder>(efStringBuilderProperty); }
            set { SetValue(efStringBuilderProperty, value); }
        }
        /// <summary>
        /// Register the ecStringBuild property so it is known in the class.
        /// </summary>
        public static readonly PropertyData efStringBuilderProperty = RegisterProperty("efStringBuilder", typeof(EntityConnectionStringBuilder), null);
        protected override void ValidateFields(List<IFieldValidationResult> validationResults)
        {
            if (string.IsNullOrWhiteSpace(DatabaseName))
            {
                validationResults.Add(FieldValidationResult.CreateError(DatabaseNameProperty, "Требуется имя базы данных"));
            }
            if (string.IsNullOrWhiteSpace(ServerName))
            {
                validationResults.Add(FieldValidationResult.CreateError(ServerNameProperty,  "Требуется имя сервера"));
            }
            if ((AuthenticationType == 0))
            {
                if (string.IsNullOrWhiteSpace(UserName))
                {validationResults.Add(FieldValidationResult.CreateError(UserNameProperty, "Требуется имя входа"));}
                if (string.IsNullOrWhiteSpace(Password))
                {validationResults.Add(FieldValidationResult.CreateError(PasswordProperty, "Требуется пароль")); }
            }
        }
       
        protected override void OnPropertyChanged(Catel.Data.AdvancedPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            string a = e.PropertyName;
            if (a != null)
                if (a == "AuthenticationType") 
                {
                    if ((int)e.NewValue == 0)
                    {
                        //base.RaisePropertyChanged("AuthenticationType");
                        ConnectionStringBuilder.IntegratedSecurity = false;
                        ConnectionStringBuilder.Remove("Integrated Security");
                    }
                    else
                    {
                        //base.RaisePropertyChanged("AuthenticationType");
                        ConnectionStringBuilder.IntegratedSecurity = true;
                        ConnectionStringBuilder.Remove("User ID");
                        ConnectionStringBuilder.Remove("Password");
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
                App.Log.Error(e,e.Message);
            }
        }
        public void LoadConfig()
        {
            var _config = ((Config.Config)Config.Sections["Config"]);
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
    }
}


