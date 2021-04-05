namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pos_serial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bill_pays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PayDate = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                        DiscAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pay1Id = c.Int(),
                        Pay2Id = c.Int(),
                        Pay3Id = c.Int(),
                        Pay1Amt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pay2Amt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pay3Amt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChangeAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefNo1 = c.String(maxLength: 50),
                        RefNo2 = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BillMain", t => t.BillId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.serial_batches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        BatchNo = c.String(maxLength: 50),
                        SerialNo = c.String(maxLength: 50),
                        MfgDate = c.String(maxLength: 10),
                        ExpDate = c.String(maxLength: 10),
                        DealerPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Mrp = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefId = c.Int(nullable: false),
                        RefTransId = c.Int(nullable: false),
                        RefVoucherId = c.Int(nullable: false),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
                        BranchId = c.Int(),
                        IssueQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.RefId)
                .Index(t => t.RefTransId)
                .Index(t => t.RefVoucherId);
            
            AddColumn("dbo.Haste", "AccId", c => c.Int());
            CreateIndex("dbo.Haste", "AccId");
            AddForeignKey("dbo.Haste", "AccId", "dbo.Acc", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.serial_batches", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Haste", "AccId", "dbo.Acc");
            DropForeignKey("dbo.bill_pays", "BillId", "dbo.BillMain");
            DropIndex("dbo.serial_batches", new[] { "RefVoucherId" });
            DropIndex("dbo.serial_batches", new[] { "RefTransId" });
            DropIndex("dbo.serial_batches", new[] { "RefId" });
            DropIndex("dbo.serial_batches", new[] { "ProductId" });
            DropIndex("dbo.Haste", new[] { "AccId" });
            DropIndex("dbo.bill_pays", new[] { "BillId" });
            DropColumn("dbo.Haste", "AccId");
            DropTable("dbo.serial_batches");
            DropTable("dbo.bill_pays");
        }
    }
}
