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

namespace InfConstractions.Models
{
#if NET

#endif
    [Model]
    public class ProverkaGUModel : ModelBase
    {
        public Configuration Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public Config.DefaultConnectionConfig _config_connection { get; set; }
        public ProverkaGUModel()
        {
            //SuspendValidations(true);
            #region CONFIGURATION
            LoadConfig();
            #endregion
            Context = App._mainDBContext;
            Context.proverkaGU.Load();
            ProverkaGU = new FastObservableCollection<proverkaGU>(from o in Context.proverkaGU select o);
            
            //SuspendValidations(false);
            //Validate(true);            
        }

#if NET
    protected ProverkaGUModel(SerializationInfo info, StreamingContext context)
        : base(info, context) { /* required for serialization */ }
#endif

        #region PROPERTIES

        public Entities Context
        {
            get { return GetValue<Entities>(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }

        public static readonly PropertyData ContextProperty = RegisterProperty(nameof(Context), typeof(Entities), null);

        public ObservableCollection<proverkaGU> ProverkaGU
        {
            get { return GetValue<ObservableCollection<proverkaGU>>(ProverkaGUProperty); }
            set { SetValue(ProverkaGUProperty, value); }
        }

        public static readonly PropertyData ProverkaGUProperty = RegisterProperty(nameof(ProverkaGU), typeof(ObservableCollection<proverkaGU>), null);

        #endregion

        public void LoadConfig()
        {           
            
               
        }
        
        public void SaveConfig()
        {
            Config.Save(ConfigurationSaveMode.Full);
        }
        public void Refresh()
        { ProverkaGU.ToList();}
    }
}


