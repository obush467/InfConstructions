namespace InfConstractions.ViewModels
{
    using Catel;
    using Catel.Collections;
    using Catel.Data;
    using Catel.IoC;
    using Catel.MVVM;
    using Models;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    [ServiceLocatorRegistration(typeof(ProverkaGUViewModel))]
    public class ProverkaGUViewModel : ViewModelBase
    {
        #region Constractors
        public ProverkaGUViewModel(Entities context):this(new ProverkaGUModel(context))
        {      }
        public ProverkaGUViewModel(ProverkaGUModel _proverkaGUModel)
        {
            Argument.IsNotNull("proverkaGUModel", _proverkaGUModel);
            proverkaGUModel = _proverkaGUModel;
            Initialize();
        }
        public void Initialize()
        {
            cmSaveChanges = new Command(OncmSaveChangesExecute, OncmSaveChangesCanExecute);
            cmRefresh = new Command(OncmRefreshExecute, OncmRefreshCanExecute);
        }
        #endregion

        public override string Title { get { return "View model title"; } }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }

        [Model]
        public ProverkaGUModel proverkaGUModel
        {
            get { return GetValue<ProverkaGUModel>(proverkaGUModelProperty); }
            private set { SetValue(proverkaGUModelProperty, value); }
        }

        public static readonly PropertyData proverkaGUModelProperty = RegisterProperty(nameof(proverkaGUModel), typeof(ProverkaGUModel));

        [ViewModelToModel("proverkaGUModel")]
        public Entities Context
        {
            get { return GetValue<Entities>(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }

        public static readonly PropertyData ContextProperty = RegisterProperty(nameof(Context), typeof(Entities));


        [ViewModelToModel("proverkaGUModel")]
        public FastObservableCollection<proverkaGU> ProverkaGU
        {
            get { return GetValue<FastObservableCollection<proverkaGU>>(ProverkaGUProperty); }
            set { SetValue(ProverkaGUProperty, value); }
        }

        public static readonly PropertyData ProverkaGUProperty = RegisterProperty(nameof(ProverkaGU), typeof(FastObservableCollection<proverkaGU>), null);

       #region Commands

        public Command cmSaveChanges { get; private set; }

        private bool OncmSaveChangesCanExecute()
        {
            return true;
        }

        private void OncmSaveChangesExecute()
        {
            Context.SaveChanges();
        }


        public Command cmRefresh { get; private set; }
        private bool OncmRefreshCanExecute()
        {
            
            return true;
        }

        private void OncmRefreshExecute()
        {
            Context.SaveChanges();
            proverkaGUModel.Refresh();
        }
        #endregion

    }
}
