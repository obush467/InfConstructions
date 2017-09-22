namespace InfConstractions.Views
{
    using Catel.Windows;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using ViewModels;
    using System.Data.SqlClient;
    using Catel.MVVM;
    using Catel.Collections;

    public partial class formLogin
    {
        public formLogin()
            : this(new formLoginViewModel())
        { }

        public formLogin(formLoginViewModel viewModel)
            : base(viewModel,DataWindowMode.Close) 
        {
            formLoginViewModel wVM = (formLoginViewModel)ViewModel;
            Command connectedCommand = wVM.cmConnectionStringConstruct;
            DataWindowButton b1 = new DataWindowButton("Соединить", connectedCommand);
            b1.IsDefault = true;
            AddCustomButton(b1);
            InitializeComponent();
            CommandBindings.AddRange(wVM.CommandBindings);
            cbServerName.CommandBindings.AddRange(wVM.CommandBindings);
            CommandBindings.AddRange(wVM.CommandBindings);          
        }
    }
}
