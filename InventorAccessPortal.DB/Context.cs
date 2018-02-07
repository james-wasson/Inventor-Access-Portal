using System;
using System.Collections.Generic;
using System.Configuration;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.DB;

namespace InventorAccessPortal.DB
{
    public class Context : IDisposable
    {
        private List<Connection> Connections = new List<Connection>();
        public Context()
        {
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                Connections.Add(new Connection(css.ConnectionString, css.Name, css.ProviderName));
            }
        }

        public List<Connection> GetConnections()
        {
            return Connections;
        }

        public void Dispose()
        {
            foreach (var conn in Connections)
            {
                conn.Dispose();
            }
        }
    }

}
