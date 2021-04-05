namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_qty_item_serial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.item_serials", "Qty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.item_serials", "Qty");
        }
    }
}
