namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_costhead_billmain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillMain", "CostHeadId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BillMain", "CostHeadId");
        }
    }
}
