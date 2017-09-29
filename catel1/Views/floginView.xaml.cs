using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InfConstractions.ViewModels;

namespace InfConstractions.Views
{
    /// <summary>
    /// Interaction logic for floginView.xaml
    /// </summary>
    public partial class floginView : UserControl
    {
        public floginView()
        {
            InitializeComponent();
        }

        private void cbServerName_DropDownOpened(object sender, EventArgs e)
        {
            if (((floginViewModel)DataContext).ServersCollection.Count() == 0)
                ((floginViewModel)DataContext).RefreshServersList.Execute(null);
        }
    }
}
