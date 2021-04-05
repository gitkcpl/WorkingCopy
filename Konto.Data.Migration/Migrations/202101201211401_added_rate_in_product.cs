namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_rate_in_product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductPrice", "Rate1", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProductPrice", "Rate2", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductPrice", "Rate2");
            DropColumn("dbo.ProductPrice", "Rate1");
        }
    }
}
