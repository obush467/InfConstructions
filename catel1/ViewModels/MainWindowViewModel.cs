namespace InfConstractions.ViewModels
{
    using Catel.Data;
    using Catel.MVVM;
    using Catel.Services;
    using System.Threading.Tasks;
    using System.Data.Entity;
    using System.Linq;

    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IMessageService _messageService;
        public MainWindowViewModel()
        {
            TTTTT = App._mainDBContext;
        }
        public MainWindowViewModel(IUIVisualizerService uiVisualizerService, PleaseWaitService pleaseWaitService, IMessageService messageService)
        {
            _messageService.ShowAsync("Старт2");
            _uiVisualizerService = uiVisualizerService;
            _pleaseWaitService = pleaseWaitService;
            _messageService = messageService;
        } 

        public override string Title { get { return "InfConstractions"; } }

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
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

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        public DbContext TTTTT
        {
            get { return GetValue<DbContext>(TTTTTProperty); }
            set { SetValue(TTTTTProperty, value); }
        }

        /// <summary>
        /// Register the TTTTT property so it is known in the class.
        /// </summary>
        public static readonly PropertyData TTTTTProperty = RegisterProperty("TTTTT", typeof(DbContext), null);
    }
}
