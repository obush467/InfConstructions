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

namespace InfConstractions.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
           // Visibility = Visibility.Visible;        
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
            ProverkaGUView n = new ProverkaGUView(((MainWindowViewModel)ViewModel).mainContext);
            var doc = dockManager_main.DockController.AddDocumentPanel(docPanel); 
            doc.Caption="ProverkaGU";
            doc.Content = n;    
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewProverkaGU();
            App.Log.Info("Открыт новый документ");

        }

        private void mainWindiw_Drop(object sender, DragEventArgs e)
        {
            Close();
        }

        private void mainWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           // NewProverkaGU();
        }

        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Close();
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            var dependencyResolver = this.GetDependencyResolver();
            var navigationService = dependencyResolver.Resolve<INavigationService>();
            //navigationService.CloseApplication();


        }
    }

}
