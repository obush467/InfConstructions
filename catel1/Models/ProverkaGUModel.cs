using System;
using System.Collections.Generic;
using Catel.Data;
using System.Data.Sql;
using System.Data;
using System.Collections.ObjectModel;
using Catel.Logging;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.EntityClient;
using System.Configuration;
using System.Windows;
using Catel.MVVM;
using System.Data.Entity;
using System.Linq;
using Catel.Collections;
using Catel.IoC;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Collections.Specialized;

namespace InfConstractions.Models
{
#if NET

#endif
    [ServiceLocatorRegistration(typeof(ProverkaGUModel))]
    [Model]
    public class ProverkaGUModel : ValidatableModelBase
    {
        public Configuration Config ;
        public Config.DefaultConnectionConfig _config_connection { get; set; }
        #region Constructors

        [InjectionConstructor]
        public ProverkaGUModel(Entities _context):this()
        {
            Context = _context;
            Context.proverkaGU.Load();
            ProverkaGU = new ObservableCollection<proverkaGU>(Context.proverkaGU);     
            SuspendValidations(false);
            Validate(true);            
        }

        [InjectionConstructor]
        public ProverkaGUModel()
        {
            SuspendValidations(true);
            #region CONFIGURATION
            LoadConfig();
            #endregion
        }

        #if NET
            protected ProverkaGUModel(SerializationInfo info, StreamingContext context)
                : base(info, context) { /* required for serialization */ }
        #endif
        #endregion
        #region PROPERTIES
        public ObservableCollection<proverkaGU> ProverkaGU
        {
            get { return GetValue<ObservableCollection<proverkaGU>>(ProverkaGUProperty); }
            set { SetValue(ProverkaGUProperty, value); }
        }

        public static readonly PropertyData ProverkaGUProperty = RegisterProperty(nameof(ProverkaGU), typeof(ObservableCollection<proverkaGU>), null);
        public Entities Context
        {
            get { return GetValue<Entities>(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }
        public static readonly PropertyData ContextProperty = RegisterProperty(nameof(Context), typeof(Entities), null);

        #endregion
        #region Methods

        public void LoadConfig()
        {
            Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
        
        public void SaveConfig()
        {
            Config.Save(ConfigurationSaveMode.Full);
        }
        public void Refresh()
        {
            Context.SaveChanges();
            var ctx = ((IObjectContextAdapter)Context).ObjectContext;
            ctx.Refresh(RefreshMode.StoreWins, (from o in Context.proverkaGU select o));
            //ProverkaGU = new ObservableCollection<proverkaGU>(from o in Context.proverkaGU select o);
        }
        #endregion
    }
}


