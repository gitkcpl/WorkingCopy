using Konto.App.Shared;
using Konto.Core.Shared;
using Konto.Data;
using Konto.Data.Models.Admin;
using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Dac.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KontoWin.Db
{
    public partial class DbSetting : KontoForm
    {
        private bool isTestConnection;
        public List<string> MessageList { get; set; }

        SqlConnectionStringBuilder connectionStringBuilder = null;
        public DbSetting()
        {
            InitializeComponent();
            connectionStringBuilder = new SqlConnectionStringBuilder();
            MessageList = new List<string>();
            this.databseTextEdit.Text = KontoGlobals.DbName;
            this.databseTextEdit.Enabled = false;
            this.ActiveControl = serverTextEdit;
            
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            isTestConnection = false;

            connectionStringBuilder.InitialCatalog = "Master";
            connectionStringBuilder.UserID = userTextEdit.Text;
            connectionStringBuilder.Password = passTextEdit.Text;
            connectionStringBuilder.DataSource = serverTextEdit.Text;

            if (string.IsNullOrEmpty(userTextEdit.Text))
            {
                connectionStringBuilder.Remove("User ID");
                connectionStringBuilder.Remove("Password");

                // Turn on integrated security:
                connectionStringBuilder.IntegratedSecurity = true;
            }

            try
            {
                var conn = new SqlConnection(connectionStringBuilder.ConnectionString);
                conn.Open();
                isTestConnection = true;
                MessageBox.Show("Connection Test Successfull");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                isTestConnection = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

            connectionStringBuilder.InitialCatalog = KontoGlobals.DbName;

            connectionStringsSection.ConnectionStrings["KontoContext"].ConnectionString = connectionStringBuilder.ConnectionString;
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
            KontoGlobals.sqlConnectionString = connectionStringBuilder;
            MessageBox.Show("Connection Saved Successfully");
            this.Close();
        }
       

     

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
                
        }
    }
}
