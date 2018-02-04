﻿using System;
using InventorAccessPortal.DB._DB_DataSetTableAdapters;
using System.Collections.Generic;
using System.Configuration;
using InventorAccessPortal.DB.Objects;
using InventorAccessPortal.DB;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace InventorAccessPortal.DB
{
    public class Context : IDisposable
    {
        List<Connection> Connections = new List<Connection>();
        public Context()
        {
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                Connections.Add(new Connection(css.ConnectionString, css.Name, css.ProviderName));
            }
            foreach (var conn in Connections)
            {
                var a = conn.AllInvestigatorsTableAdapter.GetData().Rows;
            }
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
