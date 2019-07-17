namespace InfConstractions.ViewModels
{
    using DevExpress.Mvvm;
    using System.Windows;

    public class UcLoggerViewModel : ViewModelBase
    {
        public UcLoggerViewModel()
        {
            Title = (string)Application.Current.FindResource("ucLoggerTitle");
            // Log = App.Log;
        }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        public string Title
        {
            get { return GetProperty<string>(() => Title); }
            set { SetProperty(() => Title, value); }
        }
        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        /*public ILog Log
        {
            get { return GetValue<ILog>(LogProperty); }
            set { SetValue(LogProperty, value); }
        }*/

        /// <summary>
        /// Register the Log property so it is known in the class.
        /// </summary>
        //  public static readonly PropertyData LogProperty = RegisterProperty("Log", typeof(ILog), null);

        /// <summary>
        /// Register the Title property so it is known in the class.
        /// </summary>
        // public static readonly PropertyData TitleProperty = RegisterProperty("Title", typeof(string), null);

        /* protected override async Task InitializeAsync()
         {
             await base.InitializeAsync();

             // TODO: subscribe to events here
         }*/

        /* protected override async Task CloseAsync()
         {
             // TODO: unsubscribe from events here

             await base.CloseAsync();
         }*/
    }
}
