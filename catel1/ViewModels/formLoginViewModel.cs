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
    using Views;

    public class formLoginViewModel : ViewModelBase
    {
        public CommandBindingCollection CommandBindings = new CommandBindingCollection();
        #region Constructors
        public formLoginViewModel(SqlConnection connection) : this()
        {
            Argument.IsNotNull(()=> connection);
            Connection = connection;
        }
        public formLoginViewModel(formLoginModel _formLoginModel)
        {
            Argument.IsNotNull("Model", _formLoginModel);
            Model = _formLoginModel;
        }
        public formLoginViewModel ():this(new formLoginModel())
        {
            //ValidateModelsOnInitialization = false;

            //Создание и регистрация команд
            ICommandManager cManager = new CommandManager();
            cmRefreshServersList = new Command(OncmRefreshServersListExecute, OncmRefreshServersListCanExecute);
            cManager.CreateCommand("cmRefreshServersList");
            cManager.RegisterCommand("cmRefreshServersList", cmRefreshServersList,this);
            cmConnectionStringConstruct = new Command(OncmConnectionStringConstructExecute);
            cManager.CreateCommand("cmConnectionStringConstruct");
            cManager.RegisterCommand("cmConnectionStringConstruct", cmConnectionStringConstruct, this);
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
            // TODO: Handle command logic here
            if (ServersCollection.Count == 0)
            { Model.RefreshServers();}
        }
        public Command cmConnectionStringConstruct { get; set; }
        private async void OncmConnectionStringConstructExecute()
       {
            //ConnectionStringBuilder.Clear();
            ConnectionStringBuilder.PersistSecurityInfo = true; 
            //ConnectionStringBuilder.DataSource = ServerName;
            //ConnectionStringBuilder.InitialCatalog = DatabaseName;
            //ConnectionStringBuilder.UserID =UserName;
            //Password = "Password";
            ConnectionStringBuilder.ConnectTimeout = 30;           
            ConnectionStringBuilder.MultipleActiveResultSets = true;
            ConnectionStringBuilder.ApplicationName = App.Current.MainWindow.Title;
            Validate(true);
            if (!HasErrors)
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
                    efConnection=new EntityConnection (efStringBuilder.ToString());
                    efConnection.Open();
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
        [ViewModelToModel("Model")]
        public EntityConnectionStringBuilder efStringBuilder
        {
            get { return GetValue<EntityConnectionStringBuilder>(efStringBuilderProperty); }
            set { SetValue(efStringBuilderProperty, value); }
        }

        /// <summary>
        /// Register the efStringBuilder property so it is known in the class.
        /// </summary>
        public static readonly PropertyData efStringBuilderProperty = RegisterProperty("efStringBuilder", typeof(EntityConnectionStringBuilder), null);
        [ViewModelToModel("Model")]
        public int AuthenticationType
        {
            get { return GetValue<int>(AuthenticationTypeProperty); }
            set { SetValue(AuthenticationTypeProperty, value); }
        }
        public static readonly PropertyData AuthenticationTypeProperty = RegisterProperty("AuthenticationType", typeof(int), null);

        [ViewModelToModel("Model")]
        public List<string> AuthenticationTypes
        {
            get { return GetValue<List<string>>(AuthenticationTypesProperty); }
            set { SetValue(AuthenticationTypesProperty, value); }
        }

        public static readonly PropertyData AuthenticationTypesProperty = RegisterProperty("AuthenticationTypes", typeof(List<string>), null);
              
        [Model]
        public formLoginModel Model
        {
            get { return GetValue<formLoginModel>(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }
        public static readonly PropertyData ModelProperty = RegisterProperty("Model", typeof(formLoginModel), null);        

        [ViewModelToModel("Model")]
        public ObservableCollection<string> ServersCollection
        {
            get { return GetValue<ObservableCollection<string>>(ServersCollectionProperty); }
            private set { SetValue(ServersCollectionProperty, value); }
        }

        public static readonly PropertyData ServersCollectionProperty = RegisterProperty("ServersCollection", typeof(ObservableCollection<string>));


        [ViewModelToModel("Model")]
        public SqlConnection Connection
        {
            get { return GetValue<SqlConnection>(ConnectionProperty); }
            set { SetValue(ConnectionProperty, value); }
        }

        /// <summary>
        /// Register the Connection property so it is known in the class.
        /// </summary>
        public static readonly PropertyData ConnectionProperty = RegisterProperty("Connection", typeof(SqlConnection));

        [ViewModelToModel("Model")]
        public string ServerName
        {
            get { return GetValue<string>(ServerNameProperty); }
            set { SetValue(ServerNameProperty, value); }
        }

        public static readonly PropertyData ServerNameProperty = RegisterProperty("ServerName", typeof(string), null);

        [ViewModelToModel("Model")]
        public string UserName
        {
            get { return GetValue<string>(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly PropertyData UserNameProperty = RegisterProperty("UserName", typeof(string), null);

        [ViewModelToModel("Model")]
        public string Password
        {
            get { return GetValue<string>(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly PropertyData PasswordProperty = RegisterProperty("Password", typeof(string), null);
        [ViewModelToModel("Model")]
        public string DatabaseName
        {
            get { return GetValue<string>(DatabaseNameProperty); }
            set { SetValue(DatabaseNameProperty,value); }
        }
        
        public static readonly PropertyData DatabaseNameProperty = RegisterProperty("DatabaseName", typeof(string), null);
        [ViewModelToModel("Model")]
        public SqlConnectionStringBuilder ConnectionStringBuilder
        {
            get { return GetValue<SqlConnectionStringBuilder>(ConnectionStringBuilderProperty); }
            private set { SetValue(ConnectionStringBuilderProperty, value); }
        }

        public static readonly PropertyData ConnectionStringBuilderProperty = RegisterProperty("ConnectionStringBuilder", typeof(SqlConnectionStringBuilder));
        private EntityConnection connection;
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>

        [ViewModelToModel("Model")]
        public EntityConnection efConnection
        {
            get { return GetValue<EntityConnection>(efConnectionProperty); }
            set { SetValue(efConnectionProperty, value); }
        }

        /// <summary>
        /// Register the efConnection property so it is known in the class.
        /// </summary>
        public static readonly PropertyData efConnectionProperty = RegisterProperty("efConnection", typeof(EntityConnection), null);

        [ViewModelToModel("Model")]
        public ObservableCollection<string> Logins
        {
            get { return GetValue<ObservableCollection<string>>(LoginsProperty); }
            set { SetValue(LoginsProperty, value); }
        }

        public static readonly PropertyData LoginsProperty = RegisterProperty(nameof(Logins), typeof(ObservableCollection<string>), null);
        #endregion

        #region Metods
        public void SaveConfig()
        {
            // Model.SaveConfig(ConfigPath);
            Model.SaveConfig();
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

    

