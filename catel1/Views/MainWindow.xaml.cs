using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Data;
using InfConstractions.ViewModels;
using Catel.Logging;
using DevExpress.Xpf.Layout;
using DevExpress.Xpf.Docking;
using DevExpress.Mvvm;
using System.Data.SqlClient;
using InfConstractions.Models;
using Catel.IoC;
using Catel.Windows;

namespace InfConstractions.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Visibility = Visibility.Hidden;
            }
            catch (Exception e)
            { MessageBox.Show("Ошибка при старте"); }
        }

        private void DockManagerDocumentClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Точно закрыть?", "Закрытие", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {e.Cancel = true;}
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           if (((MainWindowViewModel)DataContext).CanProverkaGU()) ((MainWindowViewModel)DataContext).ProverkaGU();
        }

        private void WebBrowser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {

        }
    }

}
