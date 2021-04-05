namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;


    public partial class accid_challan : DbMigration
    {
        public override void Up()
        {
            
        }

        public override void Down()
        {
           // AddForeignKey("dbo.Challan", "AccId", "dbo.Acc", "Id");
        }
    }
}
