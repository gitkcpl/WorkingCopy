namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_discper_in_account : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Acc", "DiscPer", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Acc", "DiscPer");
        }
    }
}
