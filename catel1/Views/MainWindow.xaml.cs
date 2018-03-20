using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Themes;
using System.Windows.Data;
using InfConstractions.ViewModels;
using Catel.Logging;

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
            //string title = "New1";
            var doc = new LayoutDocument();
            DOcPane1.Children.Add(doc);
            ucLoggerViewModel VM = new ucLoggerViewModel();
            Binding myBinding = new Binding("Text");
            myBinding.Source=mainWindow.text2;
            myBinding.Mode = BindingMode.TwoWay;
            //mainWindow.la_vLogger.SetBinding(LayoutAnchorable.TitleProperty, myBinding);
            //BindingOperations.SetBinding(mainWindow.la_vLogger, LayoutAnchorable.TitleProperty, myBinding);
            BindingOperations.SetBinding(doc, LayoutDocument.TitleProperty, myBinding);
            ViewModels.AssignAddressViewModel vm2 = new AssignAddressViewModel();
            vm2.Sel1();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewDocument();
            App.Log.Info("Открыт новый документ");

        }

        private void mainWindiw_Drop(object sender, DragEventArgs e)
        {

        }
    }

}
