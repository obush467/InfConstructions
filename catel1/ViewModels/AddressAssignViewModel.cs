using Catel.MVVM;
using System.Threading.Tasks;
using InfConstractions.Models;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using Xceed.Wpf.AvalonDock.Themes;
using Xceed.Wpf.AvalonDock;
using Catel.Data;
using Catel.Logging;

namespace InfConstractions.ViewModels
{
    public class AssignAddressViewModel : ViewModelBase
    {
        #region Fields
        public Entities Context = App._mainDBContext;

        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignAddressViewModel"/> class.
        /// </summary>
        public AssignAddressViewModel()
        {
            Context.Configuration.AutoDetectChangesEnabled = false;
            //SetValue(Probe1Property, Sel1());
            Context.Configuration.AutoDetectChangesEnabled = true;
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ObservableCollection<object> Probe1
        {
            get { return GetValue<ObservableCollection<object>>(Probe1Property); }
        }
        
        /// <summary>
        /// Register the Probe1 property so it is known in the class.
        /// </summary>
        public static readonly PropertyData Probe1Property = RegisterProperty("Probe1", typeof(ObservableCollection<object>));
        /// <summary>
        /// Gets the title of the view model.
        /// </summary>
        /// <value>The title.</value>
        public override string Title { get { return "View model title"; } }

        
        #endregion

        #region Commands
        
        #endregion

        #region Methods
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


            #endregion
        }
    }

