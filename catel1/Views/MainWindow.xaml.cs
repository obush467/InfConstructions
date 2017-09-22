using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Data;
using InfConstractions.ViewModels;
using Catel.Logging;
using Catel.Services;
using DevExpress.Xpf.Layout;
using DevExpress.Xpf.Docking;
using System.Data.SqlClient;
using InfConstractions.Models;
using Catel.IoC;
using Catel.Windows;
using Orchestra.Services;
using Orchestra;

namespace InfConstractions.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            //Orchestra.StyleHelper.CreateStyleForwardersForDefaultStyles("Office2016Black");
           // Catel.ThemeHelper.EnsureCatelMvvmThemeIsLoaded();

            //var serviceLocator = ServiceLocator.Default;

           // IThemeService themeService = serviceLocator.ResolveType<IThemeService>();
           // ThemeHelper.EnsureApplicationThemes(GetType().Assembly, themeService.ShouldCreateStyleForwarders());
           // Orchestra.ApplicationExtensions.ApplyTheme(App.Current);

            InitializeComponent();
            Visibility = Visibility.Hidden;        
        }




        private void DockManagerDocumentClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Close this tab?", ".Net Notepad", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {e.Cancel = true;}
        }
        private void NewDocument()
        {
            //Title = "New1";
            //var doc = new LayoutDocument();
            //DOcPane1.Children.Add(doc);
            //ucLoggerViewModel VM = new ucLoggerViewModel();
            //Binding myBinding = new Binding("Text");
            //myBinding.Source=mainWindow.text2;
            //myBinding.Mode = BindingMode.TwoWay;
            //mainWindow.la_vLogger.SetBinding(LayoutAnchorable.TitleProperty, myBinding);
            //BindingOperations.SetBinding(mainWindow.la_vLogger, LayoutAnchorable.TitleProperty, myBinding);
            //BindingOperations.SetBinding(doc, LayoutDocument.TitleProperty, myBinding);
            //ViewModels.AssignAddressViewModel vm2 = new AssignAddressViewModel();
            //vm2.Sel1();
        }

        private void NewProverkaGU()
        {
            ProverkaGUView n = new ProverkaGUView(((MainWindowViewModel)DataContext).mainContext);
            var doc = dockManager_main.DockController.AddDocumentPanel(docPanel); 
            doc.Caption="ProverkaGU";
            doc.Content = n;    
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewProverkaGU();
            App.Log.Info("Открыт новый документ");

        }

        private void mainWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           // NewProverkaGU();
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           if (mainWindowviewModel.cmProverkaGU.CanExecute()) mainWindowviewModel.cmProverkaGU.Execute();        }
    }

}
