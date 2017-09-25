using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catel.Data;
using System.Data.Sql;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.EntityClient;
using System.Configuration;
using System.Windows;
using System.Data.Entity;
using InfConstractions.Models;
using DevExpress.Mvvm;

namespace InfConstractions.Models
{
#if NET
[Serializable]
#endif
    public class MainWindowModel
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


        public SqlConnection sqlConnection { get; set;}
        private void OnsqlConnectionChanged()
        {
            // TODO: Implement logic
        }

        private EntityConnection _efConnection;
        public EntityConnection efConnection
        {
            get { return _efConnection; }
            set { 
                if ((mainContext != null) && efConnection.State!=ConnectionState.Closed)
                { mainContext.SaveChanges();}
                mainContext = new Entities(value);
                _efConnection=value;
                
            }
        }
        private void OnefConnectionChanged()
        {
            // TODO: Implement logic
        }
        public Entities mainContext { get ;set;}      
        #endregion

        #region Methods

        #endregion
    }

}
