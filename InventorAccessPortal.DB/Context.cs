using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

        // returns void, accepts Connection c
        public delegate void Job(Connection c);
        public void RunVoidJob(Job job) // spins off threads to run faster
        {
            foreach (var c in GetConnections())
            {
                Task.Run(() =>
                {
                    job(c);
                });
            }
        }

        public async void RunVoidAwaitJob(Job job) // spins off threads to run faster
        {
            var tasks = new List<Task>();
            foreach (var c in GetConnections())
            {
                tasks.Add(Task.Run(() =>
                {
                    job(c);
                }));
            }
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// This function will let us loop through the connection strings faster
        /// </summary>
        /// <param name="job">a lambda expression, taking Connection and returning _DB_Dataset</param>
        /// <returns>a list of Datasets</returns>
        public async Task<List<_DB_Dataset>> RunDatasetJob(Func<Connection, _DB_Dataset> job)
        {
            var tasks = new List<Task<_DB_Dataset>>();
            foreach (var c in GetConnections())
            {
                tasks.Add(Task.Run(() =>
                {
                    return job(c);
                }));
            }
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }

        /// <summary>
        /// This function will let us loop through the connection strings faster
        /// </summary>
        /// <param name="job">a lambda expression, taking Connection and returning List of DataRows</param>
        /// <returns>a list of List of a list of data rows</returns>
        public async Task<List<List<DataRow>>> RunDatarowsJob(Func<Connection, List<DataRow>> job)
        {
            var tasks = new List<Task<List<DataRow>>>();
            foreach (var c in GetConnections())
            {
                tasks.Add(Task.Run(() =>
                {
                    return job(c);
                }));
            }
            var results = await Task.WhenAll(tasks);
            return results.ToList();
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
