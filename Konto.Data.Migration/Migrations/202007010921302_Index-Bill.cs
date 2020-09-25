namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndexBill : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BillRef", "BillId");
            CreateIndex("dbo.BillRef", "BillTransId");
            CreateIndex("dbo.BillMain", "AccId");
            CreateIndex("dbo.BillTrans", "BillId");
            CreateIndex("dbo.BillTrans", "ProductId");
            CreateIndex("dbo.BillTrans", "ToAccId");
            CreateIndex("dbo.BtoB", "RefId");
            CreateIndex("dbo.Challan", "AccId");
            CreateIndex("dbo.LedgerTrans", "RefId");
            AddForeignKey("dbo.BillTrans", "BillId", "dbo.BillMain", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BillTrans", "BillId", "dbo.BillMain");
            DropIndex("dbo.LedgerTrans", new[] { "RefId" });
            DropIndex("dbo.Challan", new[] { "AccId" });
            DropIndex("dbo.BtoB", new[] { "RefId" });
            DropIndex("dbo.BillTrans", new[] { "ToAccId" });
            DropIndex("dbo.BillTrans", new[] { "ProductId" });
            DropIndex("dbo.BillTrans", new[] { "BillId" });
            DropIndex("dbo.BillMain", new[] { "AccId" });
            DropIndex("dbo.BillRef", new[] { "BillTransId" });
            DropIndex("dbo.BillRef", new[] { "BillId" });
        }
    }
}
