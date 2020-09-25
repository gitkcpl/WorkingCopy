namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;

    public partial class sp_lookup : DbMigration
    {
        public override void Up()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceNames =
                    assembly.GetManifestResourceNames().Where(str => str.EndsWith(".sql")).OrderBy(x=>x);

                foreach (string resourceName in resourceNames)
                {
                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string sql = reader.ReadToEnd();
                      //  Console.WriteLine(resourceName);
                      //  Console.WriteLine(sql);
                       if (!string.IsNullOrEmpty(sql))
                        this.Sql(sql);
                        //this.SqlResource(resourceName);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
           

        }
        
        public override void Down()
        {

        }
    }
}
