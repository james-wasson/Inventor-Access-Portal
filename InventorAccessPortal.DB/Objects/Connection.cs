using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace InventorAccessPortal.DB.Objects
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

        /// <summary>
        /// execute SQL command which don't have a feedback result
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>the sgin of whether succeed</returns>
        public bool ExeSQL(string strSQL)
        {
            bool resultState = false;

            DBConnection.Open();
            OleDbTransaction myTrans = DBConnection.BeginTransaction();
            OleDbCommand command = new OleDbCommand(strSQL, DBConnection, myTrans);

            try
            {
                command.ExecuteNonQuery();
                myTrans.Commit();
                resultState = true;
            }
            catch
            {
                myTrans.Rollback();
                resultState = false;
            }
            finally
            {
                DBConnection.Close();
            }
            return resultState;
        }
        /// <summary>
        /// execute SQL command and send result back to DataReader
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>dataReader</returns>
        private OleDbDataReader ReturnDataReader(string strSQL)
        {
            DBConnection.Open();
            OleDbCommand command = new OleDbCommand(strSQL, DBConnection);
            OleDbDataReader dataReader = command.ExecuteReader();
            DBConnection.Close();

            return dataReader;
        }

        /// <summary>
        /// execute SQL command and send result back to DataSet
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>DataSet</returns>
        public DataSet ReturnDataSet(string strSQL)
        {
            DBConnection.Open();
            DataSet dataSet = new DataSet();
            OleDbDataAdapter OleDbDA = new OleDbDataAdapter(strSQL, DBConnection);
            OleDbDA.Fill(dataSet, "objDataSet");

            DBConnection.Close();
            return dataSet;
        }

        /// <summary>
        /// Execute a search SQL command and return the number of results
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>sqlResultCount</returns>
        public int ReturnSqlResultCount(string strSQL)
        {
            int sqlResultCount = 0;

            try
            {
                DBConnection.Open();
                OleDbCommand command = new OleDbCommand(strSQL, DBConnection);
                OleDbDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    sqlResultCount++;
                }
                dataReader.Close();
            }
            catch
            {
                sqlResultCount = 1;
            }
            finally
            {
                DBConnection.Close();
            }
            return sqlResultCount;
        }



        public void Dispose()
        {
            DBConnection.Dispose();
        }
    }
}