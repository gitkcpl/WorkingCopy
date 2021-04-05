namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BOM",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DivisionId = c.Int(nullable: false),
                        VoucherId = c.Int(nullable: false),
                        VoucherNo = c.String(),
                        VoucherDate = c.Int(nullable: false),
                        Description = c.String(),
                        ProductId = c.Int(nullable: false),
                        TargetQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(),
                        CompId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Voucher", t => t.VoucherId)
                .Index(t => t.VoucherId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.BOMTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ColorId = c.Int(nullable: false),
                        AccId = c.Int(nullable: false),
                        BaseQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UomId = c.Int(nullable: false),
                        RequireQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShortQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BOMId = c.Int(nullable: false),
                        OrderTransId = c.Int(nullable: false),
                        TransType = c.Int(nullable: false),
                        Remark1 = c.String(),
                        Remark2 = c.String(),
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
                .ForeignKey("dbo.BOM", t => t.BOMId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.BOMId)
                .Index(t => t.OrderTransId);
            
            AddColumn("dbo.PFormula", "RefProductId", c => c.Int(nullable: false));
            AddColumn("dbo.PFormula", "UomId", c => c.Int(nullable: false));
            AlterColumn("dbo.PFormula", "DescType", c => c.Int(nullable: false));
            AlterColumn("dbo.PFormula", "Qty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PFormula", "Cut", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PFormula", "ColorId", c => c.Int(nullable: false));
            AlterColumn("dbo.PFormula", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PFormula", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.PFormula", "ProductId");
            CreateIndex("dbo.PFormula", "RefProductId");
            AddForeignKey("dbo.PFormula", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.PFormula", "RefProductId", "dbo.Product", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PFormula", "RefProductId", "dbo.Product");
            DropForeignKey("dbo.PFormula", "ProductId", "dbo.Product");
            DropForeignKey("dbo.BOMTrans", "ProductId", "dbo.Product");
            DropForeignKey("dbo.BOMTrans", "BOMId", "dbo.BOM");
            DropForeignKey("dbo.BOM", "VoucherId", "dbo.Voucher");
            DropForeignKey("dbo.BOM", "ProductId", "dbo.Product");
            DropIndex("dbo.PFormula", new[] { "RefProductId" });
            DropIndex("dbo.PFormula", new[] { "ProductId" });
            DropIndex("dbo.BOMTrans", new[] { "OrderTransId" });
            DropIndex("dbo.BOMTrans", new[] { "BOMId" });
            DropIndex("dbo.BOMTrans", new[] { "ProductId" });
            DropIndex("dbo.BOM", new[] { "ProductId" });
            DropIndex("dbo.BOM", new[] { "VoucherId" });
            AlterColumn("dbo.PFormula", "Total", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PFormula", "Rate", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PFormula", "ColorId", c => c.Int());
            AlterColumn("dbo.PFormula", "Cut", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PFormula", "Qty", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.PFormula", "DescType", c => c.Int());
            DropColumn("dbo.PFormula", "UomId");
            DropColumn("dbo.PFormula", "RefProductId");
            DropTable("dbo.BOMTrans");
            DropTable("dbo.BOM");
        }
    }
}
