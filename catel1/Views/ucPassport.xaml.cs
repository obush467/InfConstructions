using DevExpress.Xpf.Grid;
using InfConstractions.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Windows;
using System.Windows.Controls;
using UNS.Models;

namespace InfConstractions.Views
{
    /// <summary>
    /// Логика взаимодействия для ucPassport.xaml
    /// </summary>
    public partial class UcPassport : UserControl
    {
        #region Constructors
        public UcPassport()
        {
            InitializeComponent();
        }
        #endregion
        #region EventHandlers
        private void Passport_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (DataContext != null)
            {
                List<GUPassport> l = new List<GUPassport>();
                DbChangeTracker tr = ((UcPassportViewModel)DataContext).Context.ChangeTracker;
                tr.DetectChanges();
                if (tr.HasChanges())
                {
                    foreach (DbEntityEntry p in tr.Entries())
                    {
                        if (p.State == System.Data.Entity.EntityState.Modified)
                        {
                            ((GUPassport)p.Entity).upserted = DateTimeOffset.Now;
                            l.Add((GUPassport)p.Entity);
                        }
                    }
                ((ProverkaGUViewModel)DataContext).cmSaveChanges();
                    foreach (GUPassport t in l)
                    {
                        ((UcPassportViewModel)DataContext).Context.Entry<GUPassport>(t).Reload();
                        ((UcPassportViewModel)DataContext).Context.GUPassports.Attach(t);
                        /*LogData logd = new LogData();
                        logd.Add("Num", t.Num);
                        logd.Add("Okrug", t.Okrug);
                        logd.Add("Raion", t.Raion);
                        logd.Add("Street", t.Street);
                        logd.Add("Dom", t.Dom);
                        string s = string.Join(", ", logd.Values);*/
                        //App.Log.Info("Внесены изменения в запись {0}",s);
                    }
                }
            }
        }
        private void TableView_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e)
        {

            //var i = new GUPassport_Site();
            // i.GUPasssport_ID= ((ucPassportViewModel)DataContext).Passport.id;
            //i.id = Guid.NewGuid();
            // ((ucPassportViewModel)DataContext).PassportSites.Add(i);
            //((ucPassportViewModel)DataContext).Context.SaveChanges();
            /*var table = sitesGrid.View as TableView;
            TableViewHitInfo nfo = table.CalcHitInfo(e.OriginalSource as DependencyObject);
            if (nfo.HitTest == TableViewHitTest.DataArea)
            {
                table.AddNewRow();
                //table.FocusedRowHandle = 0;
            }
            sitesGrid.SetCellValue(e.RowHandle, "id", Guid.NewGuid().ToString());
            sitesGrid.SetCellValue(e.RowHandle, "GUPassport_ID", ((ucPassportViewModel)DataContext).Passport.id);
           // sitesGrid.SetCellValue(e.RowHandle, "GUPassports", ((ucPassportViewModel)DataContext).Passport.id);
            sitesGrid.SetCellValue(e.RowHandle, "Row_Number", "2");
            sitesGrid.SetCellValue(e.RowHandle, "Block_Number", "2");
            sitesGrid.SetCellValue(e.RowHandle, "Site_Number", "w");
            sitesGrid.SetCellValue(e.RowHandle, "Content_Text", "vzzxc");
            sitesGrid.SetCellValue(e.RowHandle, "Content_Transliteration", "vzzxc");*/
        }
        private void TableView_ValidateRow(object sender, GridRowValidationEventArgs e)
        {

        }
        private void TableView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {

        }
        #endregion

        private void TextEdit_Drop(object sender, DragEventArgs e)
        {


        }

        private void TextEdit_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeListView)))
            {
                e.Effects = DragDropEffects.Copy;
                e.Handled = true;
            }
        }

        private void TextEdit_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void TextEdit_DragEnter(object sender, DragEventArgs e)
        {

        }
    }
}

