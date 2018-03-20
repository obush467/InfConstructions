namespace InfConstractions.ViewModels
{
    using Catel.Logging;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;
    using System.Windows;

    public class ProverkaGUViewModel : ViewModelBase
    {
        IMessageBoxService MessageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } }
        ILog Log = LogManager.GetCurrentClassLogger();
        #region Constractors
        public ProverkaGUViewModel(Entities context) : this(new ProverkaGUModel(context))
        { }
        public ProverkaGUViewModel(ProverkaGUModel _proverkaGUModel)
        {
            proverkaGUModel = _proverkaGUModel;
        }
        public ProverkaGUViewModel()
        {
        }
        public static ProverkaGUViewModel Create()
        { return ViewModelSource.Create(() => new ProverkaGUViewModel()); }

        public static ProverkaGUViewModel Create(Entities context) 
        { return ViewModelSource.Create(() => new ProverkaGUViewModel(context)); }
        public static ProverkaGUViewModel Create(ProverkaGUModel _proverkaGUModel)
        { return ViewModelSource.Create(() => new ProverkaGUViewModel(_proverkaGUModel)); }

        #endregion

        #region Properties
        public string Title { get { return "Проверка ГУ"; } }
        public ProverkaGUModel proverkaGUModel
        {
            get ;set; 
        }

        public Entities Context
        {
            get { return proverkaGUModel.Context; }
            set { proverkaGUModel.Context=value; }
        }

        public ObservableCollection<proverkaGU> ProverkaGU
        {
            get { return proverkaGUModel.ProverkaGU;}
        }
        #endregion

        #region Commands

        [Command(CanExecuteMethodName = "CancmSaveChanges",
            Name = "SaveChangesCommand",
            UseCommandManager = true)]
        public void cmSaveChanges()
        {
            try
            { Context.SaveChanges(); }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка в cmSaveChanges", null);
            }
            finally
            {            }
        }
        public bool CancmSaveChanges()
        {
            return true;
        }
        [Command(CanExecuteMethodName = "CancmClose",
            Name = "CloseCommand",
            UseCommandManager = true)]
        public void cmClose()
        {
            cmSaveChanges();
        }
        public bool CancmClose()
        {
            return true;
        }

        [Command(CanExecuteMethodName = "CancmRefresh",
            Name = "RefreshCommand",
            UseCommandManager = true)]
        public void cmRefresh()
        {
            proverkaGUModel.Refresh();
        }
        public bool CancmRefresh()
        {           
            return true;
        }


        [Command(CanExecuteMethodName = "CancmProverkaGUChange",
        Name = "ProverkaGUChangeCommand",
        UseCommandManager = true)]

        public void cmProverkaGUChange()
        {
            try
            {
                List<proverkaGU> listModified = new List<proverkaGU>();
                DbChangeTracker tracker = Context.ChangeTracker;
                tracker.DetectChanges();
                if (tracker.HasChanges())
                {
                    //вычисление списка изменённых строк в ProvercaGU и присвоение дат изменения
                    foreach (DbEntityEntry entryProvercaGU in tracker.Entries())
                    {
                        if (entryProvercaGU.State == System.Data.Entity.EntityState.Modified)
                        {
                            proverkaGU modifiedProvrkaGU = (proverkaGU)entryProvercaGU.Entity;
                            modifiedProvrkaGU.updated = DateTimeOffset.Now;
                            listModified.Add(modifiedProvrkaGU);
                        }
                    }
                    //сохранение
                    cmSaveChanges();
                    //перезагрузка изменённых строк в ProvercaGU
                    foreach (proverkaGU t in listModified)
                    {
                        Context.Entry<proverkaGU>(t).Reload();
                        Context.proverkaGU.Attach(t);
                        LogData logd = new LogData();
                        logd.Add("Num", t.Num);
                        logd.Add("Okrug", t.Okrug);
                        logd.Add("Raion", t.Raion);
                        logd.Add("Street", t.Street);
                        logd.Add("Dom", t.Dom);
                        string s = string.Join(", ", logd.Values);
                        Log.Info("Внесены изменения в запись {0}", s);
                    }
                }
            }
            catch (Exception ex)
            { Log.Error(ex, "Ошибка в TableView_PropertyChanged", null); }
            finally
            { }
        }
        public bool CancmProverkaGUChange()
        {
            return true;
        }

        #endregion

    }
}
