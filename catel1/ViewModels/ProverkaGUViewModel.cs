namespace InfConstractions.ViewModels
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Docking;
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    public class ProverkaGUViewModel : ViewModelBase,ISupportServices
    {
        protected IPassportService PassportService { get { return GetService<IPassportService>("PassportService"); } }
        protected IDocumentManagerService DocumentManagerService { get; set; }

        
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

        public ProverkaGUViewModel(Entities context, IDocumentManagerService documentManagerService) : this(context)
        {
            DocumentManagerService = documentManagerService;
        }

        public static ProverkaGUViewModel Create()
        { return ViewModelSource.Create(() => new ProverkaGUViewModel()); }

        public static ProverkaGUViewModel Create(Entities context) 
        { return ViewModelSource.Create(() => new ProverkaGUViewModel(context)); }
        public static ProverkaGUViewModel Create(ProverkaGUModel _proverkaGUModel)
        { return ViewModelSource.Create(() => new ProverkaGUViewModel(_proverkaGUModel)); }

        #endregion
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

        #region Commands

        [Command(CanExecuteMethodName = "CancmSaveChanges",
            Name = "SaveChangesCommand",
            UseCommandManager = true)]
        public void cmSaveChanges ()
        {Context.SaveChanges();}

        public bool CancmSaveChanges()
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

       [Command(CanExecuteMethodName = "CanucPassport",
       Name = "ucPassportCommand",
       UseCommandManager = true)]
        public void ucPassport(object UNOM)
        {
            IQueryable<GUPassport> passportID =
                    from passport
                    in Context.GUPassports
                    where (passport.UNOM == UNOM.ToString())
                    orderby passport.startdate descending
                    select passport;
           if (passportID.Count()>0)
            PassportService.Show(passportID.FirstOrDefault<GUPassport>().id, DocumentManagerService);
        }
        public bool CanucPassport(object UNOM)
        {
            IQueryable<GUPassport> passportID =
        from passport
        in Context.GUPassports
        where (passport.UNOM == UNOM.ToString())
        orderby passport.startdate descending
        select passport;
            if (passportID.Count() > 0) return true; else return false;
        }



        #endregion
    }
}
