using Konto.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KontoWin.Db
{
    public partial class DbUpgrade : KontoForm
    {
        public string filename { get; set; }
        public DbUpgrade()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string dapack = filename;
            var _dc = new DacpacService();
            string constring = ConfigurationManager.ConnectionStrings["KontoContext"].ConnectionString;

            var sqlconbuilder = new SqlConnectionStringBuilder(constring);

            _dc.ProcessDacPac(constring, sqlconbuilder.InitialCatalog, dapack);
            
           listBox1.DataSource = _dc.MessageList;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
