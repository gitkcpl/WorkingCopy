namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rpset_compid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecpaySetting", "CompId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecpaySetting", "CompId");
        }
    }
}
