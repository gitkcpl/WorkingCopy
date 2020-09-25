namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class batch_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Batch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompId = c.Int(),
                        DivId = c.Int(),
                        VoucherId = c.Int(),
                        YearId = c.Int(),
                        BranchId = c.Int(),
                        VoucherNo = c.String(maxLength: 25),
                        VoucherDate = c.Int(nullable: false),
                        ItemId = c.Int(),
                        ShadeId = c.Int(),
                        Cross_Section = c.String(maxLength: 50),
                        Remark = c.String(),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BatchTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchId = c.Int(nullable: false),
                        ItemId = c.Int(),
                        Per = c.Decimal(precision: 18, scale: 2),
                        ItemType = c.String(maxLength: 50),
                        Ply = c.Int(),
                        RefId = c.Int(),
                        RefTransId = c.Int(),
                        LotNo = c.String(maxLength: 50),
                        Remark = c.String(),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Batch", t => t.BatchId)
                .Index(t => t.BatchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BatchTrans", "BatchId", "dbo.Batch");
            DropIndex("dbo.BatchTrans", new[] { "BatchId" });
            DropTable("dbo.BatchTrans");
            DropTable("dbo.Batch");
        }
    }
}
