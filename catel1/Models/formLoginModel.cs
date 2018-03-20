using System;
using System.Collections.Generic;
using System.Text;
using Catel.Data;
using System.Data.Sql;
using System.Data;
using System.Collections.ObjectModel;
using Catel.Logging;
using System.IO;
using Newtonsoft.Json;
using Catel;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.EntityClient;

namespace InfConstractions.Models

{

    public class formLoginModel : ModelBase
    {
        public DBDefault DBDefault { get; set; }
        private static readonly JsonSerializerSettings JSSettings = new JsonSerializerSettings();
        const string ConfigPath = "e:\\App.config.json";


        public formLoginModel()
        {
           
            SuspendValidation = true;
            ConnectionStringBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            ServersCollection = new ObservableCollection<string>();
            efStringBuilder = new EntityConnectionStringBuilder();
            efConnection = new EntityConnection();
            #region CONFIGURATION
            JsonSerializer JSS = new JsonSerializer();
            JSSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            JSSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            AuthenticationTypes = new List<string>();
            AuthenticationTypes.Add("Проверка подлинности SQl Server");
            AuthenticationTypes.Add("Проверка подлинности Windows");
            AuthenticationTypes.Add("Универсальная проверка подлинности Active Directory");
            AuthenticationTypes.Add("Проверка пароля Active Directory");
            AuthenticationTypes.Add("Аутентификация Active Directory");

            LoadConfig(ConfigPath);

            #endregion
            SuspendValidation = false;
            Validate(true, false);            
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
        public System.Data.SqlClient.SqlConnectionStringBuilder ConnectionStringBuilder
        {
            get { return GetValue<System.Data.SqlClient.SqlConnectionStringBuilder>(ConnectionStringBuilderProperty); }
            set { SetValue(ConnectionStringBuilderProperty, value); }
        }
        public static readonly PropertyData ConnectionStringBuilderProperty = RegisterProperty("ConnectionStringBuilder", typeof(System.Data.SqlClient.SqlConnectionStringBuilder), null);

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
            set { ConnectionStringBuilder.Password= value;
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
        public void RefreshServers()
        {
            App.Log.Info("SqlEnumerationHelper:SqlEnumerateRemote", "Starting");
            ServersCollection.Clear();
            try
            {
                //Make an initial call to the sql browser service to wake it up
                SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
                System.Data.DataTable dt = instance.GetDataSources();
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
                App.Log.Info("SqlEnumerationHelper:SqlEnumerateRemote", "Ending");
            }
            catch (Exception e)
            {
                App.Log.Info("SqlEnumerationHelper:SqlEnumerateRemote", "SqlDataSourceEnumerator.GetDataSources and assoc processing failed" + e.Message);
            }
        }

        public void SaveConfig(string path)
            
        {
            DBDefault.DefaultServerName = ServerName;
            DBDefault.DefaultDBName = DatabaseName;
            DBDefault.DefaultDBAuthenticationType = AuthenticationTypes[AuthenticationType];
            var str = JsonConvert.SerializeObject(DBDefault, JSSettings).ToString();
            StreamWriter strw = new StreamWriter(Path.GetFullPath(path), false, Encoding.UTF8);
            strw.WriteLine(str);
            strw.Close();
        }

        public void LoadConfig(string path)
        {
            if (File.Exists(path))
            {
                DBDefault = JsonConvert.DeserializeObject<DBDefault>((new StreamReader(path, Encoding.UTF8)).ReadToEnd());
                if (DBDefault != null)
                {
                    if (!string.IsNullOrWhiteSpace(DBDefault.DefaultDBName))
                    { DatabaseName = DBDefault.DefaultDBName; }
                    if (!string.IsNullOrWhiteSpace(DBDefault.DefaultServerName))
                    { ServerName = DBDefault.DefaultServerName; }
                    if (!string.IsNullOrWhiteSpace(DBDefault.DefaultDBAuthenticationType))
                    { AuthenticationType = AuthenticationTypes.IndexOf(DBDefault.DefaultDBAuthenticationType); }
                }
                else { DBDefault = new DBDefault(); }
            }
        }
    }

}
