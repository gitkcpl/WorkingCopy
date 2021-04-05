namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class apparel_inward : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.barcode",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        BarcodeNo = c.String(maxLength: 31),
                        CompId = c.Int(nullable: false),
                        ReportId = c.Int(nullable: false),
                        RackNo = c.String(maxLength: 30),
                        EmpId = c.Int(),
                        Qty = c.Int(nullable: false),
                        AccId = c.Int(),
                        RefBarcodeId = c.Int(nullable: false),
                        OrderTransId = c.Int(nullable: false),
                        PcsNo = c.Int(nullable: false),
                        RefId = c.Int(nullable: false),
                        IsLayer = c.Boolean(nullable: false),
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
                .ForeignKey("dbo.Acc", t => t.AccId)
                .ForeignKey("dbo.Emp", t => t.EmpId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.BarcodeNo, unique: true)
                .Index(t => t.ReportId)
                .Index(t => t.EmpId)
                .Index(t => t.AccId)
                .Index(t => t.RefBarcodeId)
                .Index(t => t.OrderTransId);
            
            CreateTable(
                "dbo.barcode_stock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        VoucherDate = c.Int(nullable: false),
                        DivId = c.Int(),
                        EmpId = c.Int(),
                        ProductId = c.Int(),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BarcodeNo = c.String(maxLength: 50),
                        BarcodeId = c.Int(nullable: false),
                        RefId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Division", t => t.DivId)
                .ForeignKey("dbo.Emp", t => t.EmpId)
                .Index(t => t.DivId)
                .Index(t => t.EmpId)
                .Index(t => t.BarcodeNo)
                .Index(t => t.BarcodeId);
            
            CreateTable(
                "dbo.barcode_trans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        VoucherDate = c.Int(nullable: false),
                        DivId = c.Int(),
                        Remarks = c.String(maxLength: 200),
                        EmpId = c.Int(),
                        ProductId = c.Int(),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransType = c.Int(nullable: false),
                        QcPassed = c.Boolean(nullable: false),
                        BarcodeNo = c.String(maxLength: 31),
                        BarcodeId = c.Int(),
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
                .ForeignKey("dbo.barcode", t => t.BarcodeId)
                .ForeignKey("dbo.Division", t => t.DivId)
                .ForeignKey("dbo.Emp", t => t.EmpId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.DivId)
                .Index(t => t.EmpId)
                .Index(t => t.ProductId)
                .Index(t => t.BarcodeNo)
                .Index(t => t.BarcodeId);
            
            AddColumn("dbo.Division", "Priority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.barcode_trans", "ProductId", "dbo.Product");
            DropForeignKey("dbo.barcode_trans", "EmpId", "dbo.Emp");
            DropForeignKey("dbo.barcode_trans", "DivId", "dbo.Division");
            DropForeignKey("dbo.barcode_trans", "BarcodeId", "dbo.barcode");
            DropForeignKey("dbo.barcode_stock", "EmpId", "dbo.Emp");
            DropForeignKey("dbo.barcode_stock", "DivId", "dbo.Division");
            DropForeignKey("dbo.barcode", "ProductId", "dbo.Product");
            DropForeignKey("dbo.barcode", "EmpId", "dbo.Emp");
            DropForeignKey("dbo.barcode", "AccId", "dbo.Acc");
            DropIndex("dbo.barcode_trans", new[] { "BarcodeId" });
            DropIndex("dbo.barcode_trans", new[] { "BarcodeNo" });
            DropIndex("dbo.barcode_trans", new[] { "ProductId" });
            DropIndex("dbo.barcode_trans", new[] { "EmpId" });
            DropIndex("dbo.barcode_trans", new[] { "DivId" });
            DropIndex("dbo.barcode_stock", new[] { "BarcodeId" });
            DropIndex("dbo.barcode_stock", new[] { "BarcodeNo" });
            DropIndex("dbo.barcode_stock", new[] { "EmpId" });
            DropIndex("dbo.barcode_stock", new[] { "DivId" });
            DropIndex("dbo.barcode", new[] { "OrderTransId" });
            DropIndex("dbo.barcode", new[] { "RefBarcodeId" });
            DropIndex("dbo.barcode", new[] { "AccId" });
            DropIndex("dbo.barcode", new[] { "EmpId" });
            DropIndex("dbo.barcode", new[] { "ReportId" });
            DropIndex("dbo.barcode", new[] { "BarcodeNo" });
            DropIndex("dbo.barcode", new[] { "ProductId" });
            DropColumn("dbo.Division", "Priority");
            DropTable("dbo.barcode_trans");
            DropTable("dbo.barcode_stock");
            DropTable("dbo.barcode");
        }
    }
}
