using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace InventorAccessPortal.Web.DB.Objects
{
    /// <summary>
    /// A touple storing the connection string, Ole connection, and name
    /// </summary>
    public class Connection : System.IDisposable
    {
        public string ConnectionString
        {
            get;
            private set;
        }
        public string Name
        {
            get;
            private set;
        }
        public string ProviderName
        {
            get;
            private set;
        }
        public OleDbConnection DBConnection
        {
            get;
            private set;
        }
        // removes default constructor
        private Connection() { }
        // public constructor
        public Connection(string connString, string name, string providerName = "")
        {
            try
            {
                if (connString == null || connString == "")
                    throw new Exception("No Connection String Provided");
                // if not provider is specifed in the string
                if (!connString.ToLower().Contains("provider="))
                {
                    // use the one in the config file if it exists
                    if (providerName != null && providerName != "")
                    {
                        connString += ";Provider=" + providerName;
                    }
                    else // if no provider name can be found throw an error
                    {
                        throw new Exception("No Provider Name is in either connection string or in settings");
                    }
                }
                // sets the object variables
                DBConnection = new OleDbConnection(connString);
                ConnectionString = connString;
                Name = name;
                ProviderName = providerName;
            }
            catch (Exception ex)
            {
                throw new Exception("DataBase Connection failed" +
                    Environment.NewLine +
                    "Connection String: " + (connString != null ? connString : "Error: Can't Read") +
                    Environment.NewLine +
                    "Name: " + (name != null ? name : "Error: Can't Read") +
                    Environment.NewLine +
                    "Provider Name: " + (providerName != null ? providerName : "Error: Can't Read") +
                    Environment.NewLine +
                    "Exception: " + ex.Message);
            }
        }

        public void Dispose()
        {
            DBConnection.Dispose();
        }
    }
}