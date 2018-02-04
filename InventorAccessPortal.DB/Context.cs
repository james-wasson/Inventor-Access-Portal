using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using InventorAccessPortal.DB.Objects;

namespace InventorAccessPortal.Web.DB
{
    public class Context : System.IDisposable
    {
        /// <summary>
        /// The List containing the connection to the DB
        /// </summary>
        protected List<Connection> DBConnectionsList = new List<Connection>();

        /// <summary>
        /// Defult connection of database
        /// </summary>
        public Context()
        {
            InitContext();
        }

        protected void InitContext() {
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                DBConnectionsList.Add(new Connection(css.ConnectionString, css.Name, css.ProviderName));
            }
        }

        public List<Connection> GetConnections() {
            return DBConnectionsList;
        }

        public void Dispose() {
            foreach (var conn in DBConnectionsList) {
                conn.Dispose();
            }
        }
    }
}