using Catel.Windows.Controls;
namespace InfConstractions.Views
{
    using Catel.Windows;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using InfConstractions.ViewModels;
    using System.Data.SqlClient;
    using Catel.IoC;
    using Models;
    [ServiceLocatorRegistration(typeof(ProverkaGUView))]
    public partial class ProverkaGUView
    {
        [InjectionConstructor]
        public ProverkaGUView(Entities context):base(new ProverkaGUViewModel(context))
        {
            InitializeComponent();
            this.CloseViewModelOnUnloaded = false;
        }

        private void rrrButt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ProverkaGUViewModel vm = (ProverkaGUViewModel)ViewModel;
            vm.Context.SaveChanges();
        }

        private void TableView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ((ProverkaGUViewModel)ViewModel).cmSaveChanges.Execute();
        }
    }

                
}
