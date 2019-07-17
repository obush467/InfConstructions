using InfConstractions.ViewModels;
using System;
using System.Windows;

namespace InfConstractions.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                // Visibility = Visibility.Hidden;
            }
            catch (Exception e)
            { MessageBox.Show("Ошибка при старте " + e.Message); }
        }

        private void DockManagerDocumentClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Точно закрыть?", "Закрытие", MessageBoxButton.YesNo) == MessageBoxResult.No)
            { e.Cancel = true; }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
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
            ((MainWindowViewModel)DataContext).ShowLoginForm();

        }
    }

}
