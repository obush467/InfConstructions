using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InfConstractions.Models;
using System.Data.Entity.Infrastructure;
using InfConstractions.ViewModels;
using DevExpress.Xpf.Grid;
using System.Data.Entity;
using InfConstractions.Converters;

namespace InfConstractions.Views
{
    /// <summary>
    /// Логика взаимодействия для ucPassport.xaml
    /// </summary>
    public partial class ucPassport : UserControl
    {
        #region Constructors
        public ucPassport()
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
                DbChangeTracker tr = ((ucPassportViewModel)DataContext).Context.ChangeTracker;
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
                        ((ucPassportViewModel)DataContext).Context.Entry<GUPassport>(t).Reload();
                        ((ucPassportViewModel)DataContext).Context.GUPassports.Attach(t);
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
    }
}

