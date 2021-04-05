namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_col_gate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Challan", "RefId", c => c.Int(nullable: false));
            AddColumn("dbo.Challan", "RefVoucherId", c => c.Int(nullable: false));
            AddColumn("dbo.OrdTrans", "RequireDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.LedgerTrans", "VoucherDate");
            CreateIndex("dbo.LedgerTrans", "AccountId");
            CreateIndex("dbo.StockTrans", "MasterRefId");
            CreateIndex("dbo.StockTrans", "VoucherDate");
            CreateIndex("dbo.StockTrans", "AccountId");
            CreateIndex("dbo.StockTrans", "ItemId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StockTrans", new[] { "ItemId" });
            DropIndex("dbo.StockTrans", new[] { "AccountId" });
            DropIndex("dbo.StockTrans", new[] { "VoucherDate" });
            DropIndex("dbo.StockTrans", new[] { "MasterRefId" });
            DropIndex("dbo.LedgerTrans", new[] { "AccountId" });
            DropIndex("dbo.LedgerTrans", new[] { "VoucherDate" });
            DropColumn("dbo.OrdTrans", "RequireDate");
            DropColumn("dbo.Challan", "RefVoucherId");
            DropColumn("dbo.Challan", "RefId");
        }
    }
}
