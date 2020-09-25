namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public partial class sp18 : DbMigration
    {
        public override void Up()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames =
                assembly.GetManifestResourceNames().Where(str => str.EndsWith(".sql")).OrderBy(x => x);

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
        
        public override void Down()
        {
        }
    }
}
