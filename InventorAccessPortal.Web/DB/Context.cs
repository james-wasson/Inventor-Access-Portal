using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace InventorAccessPortal.Web.DB
{
    public class Context
    {
        /// <summary>
        /// A touple storing the connection string, Ole connection, and name
        /// </summary>
        public class DBConnection {
            public string ConnectionString
            {
                get;
            }
            public string Name
            {
                get;
            }
            public string ProviderName
            {
                get;
            }
            public OleDbConnection Connection
            {
                get;
            }
            // public constructor
            public DBConnection(OleDbConnection conn, string connString, string name, string providerName = "") {
                ConnectionString = connString;
                Name = name;
                ProviderName = providerName;
                Connection = conn;
            }
        }

        private List<DBConnection> DBConnectionsList = new List<DBConnection>();

        /// <summary>
        /// Defult connection of database
        /// </summary>
        public Context()
        {
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                if (css.ConnectionString != null && css.ConnectionString != "")
                {
                    try
                    {
                        // copies the connectionstring so we can modify it
                        string connString = css.ConnectionString;
                        // if not provider is specifed in the string
                        if (!connString.ToLower().Contains("provider="))
                        {
                            // use the one in the config file if it exists
                            if (css.ProviderName != null && css.ProviderName != "")
                            {
                                connString = css.ConnectionString + ";Provider=" + css.ProviderName;
                            }
                            else // if no provider name can be found throw an error
                            {
                                throw new Exception("No Provider Name is in either connection string or in settings");
                            }
                        }
                        // try the connection
                        var connection = new DBConnection(
                            new OleDbConnection(connString),
                            connString,
                            css.Name,
                            css.ProviderName
                        );
                        // adds it to the connection string list
                        DBConnectionsList.Add(connection);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("DataBase Connection failed" +
                            Environment.NewLine +
                            "Connection String: " + (css.ConnectionString != null ? css.ConnectionString : "Error: Can't Read") +
                            Environment.NewLine +
                            "Name: " + (css.Name != null ? css.Name : "Error: Can't Read") +
                            Environment.NewLine +
                            "Provider Name: " + (css.ProviderName != null ? css.ProviderName : "Error: Can't Read") +
                            Environment.NewLine +
                            "Exception: " + ex.Message);
                    }
                }
            }
        }

        public List<DBConnection> GetConnections() {
            return DBConnectionsList;
        }

        /**************************************

        /// <summary>
        /// execute SQL command which don't have a feedback result
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>the sgin of whether succeed</returns>
        public bool ExeSQL(string strSQL)
        {
            bool resultState = false;

            Connection.Open();
            OleDbTransaction myTrans = Connection.BeginTransaction();
            OleDbCommand command = new OleDbCommand(strSQL, Connection, myTrans);

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
                Connection.Close();
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
            Connection.Open();
            OleDbCommand command = new OleDbCommand(strSQL, Connection);
            OleDbDataReader dataReader = command.ExecuteReader();
            Connection.Close();

            return dataReader;
        }

        /// <summary>
        /// execute SQL command and send result back to DataSet
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns>DataSet</returns>
        public DataSet ReturnDataSet(string strSQL)
        {
            Connection.Open();
            DataSet dataSet = new DataSet();
            OleDbDataAdapter OleDbDA = new OleDbDataAdapter(strSQL, Connection);
            OleDbDA.Fill(dataSet, "objDataSet");

            Connection.Close();
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
                Connection.Open();
                OleDbCommand command = new OleDbCommand(strSQL, Connection);
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
                Connection.Close();
            }
            return sqlResultCount;
        }

        *****************************************************/
    }
}