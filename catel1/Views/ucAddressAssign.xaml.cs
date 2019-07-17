namespace InfConstractions.Views
{
    using DevExpress.Data;
    using System.Data;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public partial class ucAddressAssign
    {
        public ucAddressAssign()
        {
            //if (DataContext == null) DataContext = new ucAddressAssign();
        }

        private void trlAddressView1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataRow)))

                e.Effects = DragDropEffects.Move;

            else

                e.Effects = DragDropEffects.None;
        }


        private void textBlock_Drop(object sender, DragEventArgs e)
        {

        }

        private void textBlock_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(object)))
            {
                TextBox textBlock = (TextBox)sender;
                textBlock.Background = Brushes.SkyBlue;
                e.Handled = true;
            }
        }

        private void TextEdit_DragOver(object sender, DragEventArgs e)
        {

        }

        private void TextEdit_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void TextEdit_PreviewDragEnter(object sender, DragEventArgs e)
        {

        }

        private void TextEdit_Drop(object sender, DragEventArgs e)
        {

        }

        private void TreeListDragDropManager_StartDrag(object sender, DevExpress.Xpf.Grid.DragDrop.TreeListStartDragEventArgs e)
        {

        }

        private void AddressAssign_Loaded(object sender, RoutedEventArgs e)
        {
            trlAddress.Columns["itemAddress"].SortOrder = ColumnSortOrder.Ascending;
        }
    }
}
