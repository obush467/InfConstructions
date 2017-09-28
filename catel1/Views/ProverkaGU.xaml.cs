using Catel.Windows.Controls;
namespace InfConstractions.Views
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using InfConstractions.ViewModels;
    using System.Data.SqlClient;
    using Models;
    using System.Data.Entity.Infrastructure;
    using System;
    using System.Collections.Generic;

    public partial class  ProverkaGUView
    {
        public ProverkaGUView(Entities context):this(new ProverkaGUViewModel(context))
        {        
            InitializeComponent();
        }
        public ProverkaGUView() 
        {
            InitializeComponent();
        }
        public ProverkaGUView(ProverkaGUViewModel viewmodel)
        {
            DataContext = viewmodel;
            InitializeComponent();
        }

        private void TableView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                ((ProverkaGUViewModel)DataContext).cmProverkaGUChange();
            }
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            gridMain.RefreshData();
        }

        private void ucProverkaGU_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((ProverkaGUViewModel)DataContext).cmClose();
            }
        }
    }               
}
