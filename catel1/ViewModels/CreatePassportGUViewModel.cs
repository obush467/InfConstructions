namespace InfConstractions.ViewModels
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Threading.Tasks;
    using System.Windows;

    public class CreatePassportGUViewModel : ViewModelBase
    {
        #region Constractors
        public CreatePassportGUViewModel(Entities context) : this(new CreatePassportGUModel(context))
        { }
        public CreatePassportGUViewModel(CreatePassportGUModel _CreatePassportGUModel)
        {
            createPassportGUModel = _CreatePassportGUModel;
        }
        public CreatePassportGUViewModel()
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
        public CreatePassportGUModel createPassportGUModel
        {
            get ;set; 
        }

        public Entities Context
        {
            get { return createPassportGUModel.Context; }
            set { createPassportGUModel.Context=value; }
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
            //CreartePassportGUViewModel.Refresh();
        }
        public bool CancmRefresh()
        {           
            return true;
        }

        #endregion
    }
}
