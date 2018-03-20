namespace InfConstractions.Views
{
    using Catel.Windows;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using ViewModels;
    using System.Data.SqlClient;
    using Catel.MVVM;

    public partial class formLogin
    {
        public formLogin()
            : this(new formLoginViewModel())
        { }

        public formLogin(SqlConnection connection)
            : this(new formLoginViewModel())
        { ((formLoginViewModel)ViewModel).Connection = connection; }

        public formLogin(formLoginViewModel viewModel)
            : base(viewModel,DataWindowMode.Close) 
        {
            formLoginViewModel wVM = (formLoginViewModel)ViewModel;
            var yyy = wVM.ServersCollection;
            var cc = wVM.ConnectionStringBuilder;
            Command connectedCommand = ((formLoginViewModel)ViewModel).cmConnectionStringConstruct;
            DataWindowButton b1 = new DataWindowButton("Соединить", connectedCommand);
            b1.IsDefault = true;
            AddCustomButton(b1);
            InitializeComponent();
            //cbServerName.ItemsSource = viewModel.ServersCollection;
            this.CommandBindings.AddRange(wVM.CommandBindings);
            this.cbServerName.CommandBindings.AddRange(wVM.CommandBindings);
            this.CommandBindings.AddRange(wVM.CommandBindings);
           
        }

        public ObservableCollection<string> ServersColl1
        { get { return ((formLoginViewModel)ViewModel).ServersCollection; } }
    }
}
