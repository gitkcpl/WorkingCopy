using Konto.Data;
using Konto.Data.Models.Admin;
using Microsoft.SqlServer.Dac;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KontoWin
{
    public class DacpacService
    {
        public List<string> MessageList { get; set; }
        public bool EqualVersion { get; set; }

        public DacpacService()
        {
            MessageList = new List<string>();
        }
        public bool ProcessDacPac(string connectionString,
                                    string databaseName,
                                    string dacpacName)
        {
            bool success = true;

            EqualVersion = false;
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            connectionStringBuilder.InitialCatalog = "Master";

            MessageList.Add("*** Start of processing for " +
                             databaseName);

            var dacOptions = new DacDeployOptions();
            dacOptions.BlockOnPossibleDataLoss = false;
            dacOptions.BackupDatabaseBeforeChanges = true;




            var dacServiceInstance = new DacServices(connectionStringBuilder.ConnectionString);

            dacServiceInstance.ProgressChanged +=
              new EventHandler<DacProgressEventArgs>((s, e) =>
                            MessageList.Add(e.Message)
                            
                            );

            dacServiceInstance.Message +=
              new EventHandler<DacMessageEventArgs>((s, e) =>
                            MessageList.Add(e.Message.Message));

            try
            {


                using (DacPackage dacpac = DacPackage.Load(dacpacName))
                {
                    Version dacversion = dacpac.Version;
                    // Version _dbv = null;


                    dacServiceInstance.Deploy(dacpac, databaseName,
                                            upgradeExisting: true,
                                            options: dacOptions);



                    using (var db = new KontoContext())
                    {
                        var upg = db.DbVersions.FirstOrDefault();

                        if (upg == null)
                        {
                            upg = new DbVersion();
                            upg.CreateDate = DateTime.Now;

                        }
                        upg.Version = dacpac.Version.ToString();
                        if (upg.Id > 0)
                            upg.UpgradeDate = DateTime.Now;
                        if (upg.Id == 0)
                        {
                            //upg.Id = 1;
                            db.DbVersions.Add(upg);
                        }
                        db.SaveChanges();
                    }
                    MessageList.Add("Database Updated Successfuly...");

                }



            }
            catch (Exception ex)
            {
                success = false;
                MessageList.Add(ex.Message);
                foreach (var item in MessageList)
                {
                    Log.Error(item);
                }
            }

            foreach (var item in MessageList)
            {
                Log.Information(item);
            }
            return success;
        }


        public static bool CheckDatabaseExists(string connectionString, string databaseName)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand($"SELECT db_id('{databaseName}')", connection))
                    {
                        connection.Open();
                        return (command.ExecuteScalar() != DBNull.Value);
                    }
                }

            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public static bool CreateDatabase(string connectionString, string databaseName)
        {

            var dbpath = System.Windows.Forms.Application.StartupPath +  "\\data\\" + databaseName + "_data.mdf";
            var logpath = System.Windows.Forms.Application.StartupPath + "\\data\\" + databaseName + "_log.ldf";

            string sql = "CREATE DATABASE " + databaseName + " ON PRIMARY"
                + "(Name=" + databaseName + "_data, filename ='" + dbpath + "'"
                + ")log on"
                + "(name=" + databaseName +"_log, filename='" + logpath + "')";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    return (command.ExecuteScalar() != DBNull.Value);
                }
            }
        }

        /// <summary>
        /// Gets the database version.
        /// </summary>
        /// <param name="connectionString">The connection string of database to query.</param>
        /// <param name="isAzure">True if we are querying an Azure database.</param>
        /// <returns>DAC pack version</returns>
        private static string GetDatabaseVersion(string connectionString, bool isAzure, string databasename)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            string instanceName = connectionStringBuilder.InitialCatalog;
            string databaseToQuery = "msdb";
            if (isAzure)
            {
                // On Azure we must be connected to the master database to query sysdac_instances
                connectionStringBuilder.InitialCatalog = "Master";
                databaseToQuery = databasename;
            }

            string query = String.Format("select type_version from {0}.dbo.sysdac_instances WHERE instance_name = '{1}'", databaseToQuery, instanceName);
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandTimeout = 15;
                command.CommandType = CommandType.Text;
                var version = (string)command.ExecuteScalar();
                return version;
            }
        }
    }
}
