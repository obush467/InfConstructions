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

namespace InfConstractions.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
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
            var doc = new DocumentPanel();          
            doc.Caption="ProverkaGU";
            ProverkaGUView n = new ProverkaGUView();
            doc.Content = n;
            DockManager.DockController.AddDocumentPanel(docPanel).Content= n;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewProverkaGU();
            App.Log.Info("Открыт новый документ");

        }

        private void mainWindiw_Drop(object sender, DragEventArgs e)
        {

        }

        private void mainWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           // NewProverkaGU();
        }
    }

}
