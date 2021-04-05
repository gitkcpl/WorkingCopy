namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class billmain_Addless : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillMain", "AddLess", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BillMain", "AddLess");
        }
    }
}
