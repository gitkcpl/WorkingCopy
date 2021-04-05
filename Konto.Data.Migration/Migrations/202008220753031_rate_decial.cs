namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rate_decial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BillTrans", "Qty", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            AlterColumn("dbo.BillTrans", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.ChallanTrans", "Qty", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            AlterColumn("dbo.ChallanTrans", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChallanTrans", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ChallanTrans", "Qty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BillTrans", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BillTrans", "Qty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
