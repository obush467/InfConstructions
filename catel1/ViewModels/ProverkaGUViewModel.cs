namespace InfConstractions.ViewModels
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using Models;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;

    public class ProverkaGUViewModel : ViewModelBase
    {
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

        #endregion

    }
}
