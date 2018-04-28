using InfConstractionsDX.Main.ViewModels;
using System.Windows;
using System.Windows.Controls;
using InfConstractionsDX.Modules;
namespace InfConstractionsDX.Main.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void DockManagerDocumentClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Точно закрыть?", "Закрытие", MessageBoxButton.YesNo) == MessageBoxResult.No)
            { e.Cancel = true; }
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region вставить_Адреса
            //TODO:Добавление паннели АДресов
            //ucAddressAssign ap = new ucAddressAssign();
            //AddressPanel.Visibility = Visibility.Visible;
            //leftCenterPanel.Visibility = Visibility.Visible;
            //dockManager_main.LayoutController.Activate(leftCenterPanel);
            //dockManager_main.LayoutController.Activate(AddressPanel);
            //AddressPanel.Content = new Uri("Views\\ucAddressAssign.xaml", UriKind.Relative);
            //ap.Visibility = Visibility.Visible;
            #endregion
            if (((MainViewModel)DataContext).CancmShowLoginForm())
                ((MainViewModel)DataContext).ShowLoginForm();

        }
    }
}
