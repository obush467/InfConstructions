using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel;
using Catel.MVVM;
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
using System.Data.Entity;
using Catel.Collections;
using InfConstractions.Models;
using Catel.IoC;

namespace InfConstractions.Models
{
#if NET
[Serializable]
#endif
    public class MainWindowModel : ValidatableModelBase
    {
        public MainWindowModel(SqlConnection _sqlConnection,EntityConnection _efConnection)
        {
            sqlConnection = _sqlConnection;
            efConnection = _efConnection;
            mainContext = new Entities(efConnection);
        }

        public MainWindowModel() : this(new SqlConnection(), new EntityConnection())
        { }

#if NET
    protected MainWindowModel(SerializationInfo info, StreamingContext context)
        : base(info, context) { /* required for serialization */ }
#endif
        #region Properties


        public SqlConnection sqlConnection
        {
            get { return GetValue<SqlConnection>(sqlConnectionProperty); }
            set { SetValue(sqlConnectionProperty, value); }
        }

        public static readonly PropertyData sqlConnectionProperty = RegisterProperty(nameof(sqlConnection), typeof(SqlConnection), null, (sender, e) => ((MainWindowModel)sender).OnsqlConnectionChanged());

        private void OnsqlConnectionChanged()
        {
            // TODO: Implement logic
        }
        public EntityConnection efConnection
        {
            get { return GetValue<EntityConnection>(efConnectionProperty); }
            set { 
                if ((mainContext != null) && efConnection.State!=ConnectionState.Closed)
                { mainContext.SaveChanges();}
                mainContext = new Entities(value);
                SetValue(efConnectionProperty, value);
                
            }
        }
        public static readonly PropertyData efConnectionProperty = RegisterProperty(nameof(efConnection), typeof(EntityConnection), null, (sender, e) => ((MainWindowModel)sender).OnefConnectionChanged());
        private void OnefConnectionChanged()
        {
            // TODO: Implement logic
        }

        public Entities mainContext
        {
            get { return GetValue<Entities>(mainContextProperty); }
            set { SetValue(mainContextProperty, value); }
        }

        public static readonly PropertyData mainContextProperty = RegisterProperty(nameof(mainContext), typeof(Entities), null);
        #endregion

        #region Methods

        #endregion
    }

}
