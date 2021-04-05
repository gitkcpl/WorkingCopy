namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_salerate_billtrans : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillTrans", "SaleRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BillTrans", "SaleRate");
        }
    }
}
