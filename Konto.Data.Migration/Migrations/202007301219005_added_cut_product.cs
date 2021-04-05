namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_cut_product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Cut", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Cut");
        }
    }
}
