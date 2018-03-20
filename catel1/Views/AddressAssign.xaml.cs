namespace InfConstractions.Views
{
    using Catel.Windows;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using InfConstractions.ViewModels;
    using System.Data.SqlClient;
    using Catel.MVVM;
    public partial class AddressAssign
    {
        public AddressAssign():base(new AssignAddressViewModel())
        {
            InitializeComponent();
            this.CloseViewModelOnUnloaded = false;
        }

        private void rrrButt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AssignAddressViewModel vm = (AssignAddressViewModel)ViewModel;
            vm.Context.SaveChanges();
        }
    }
}
