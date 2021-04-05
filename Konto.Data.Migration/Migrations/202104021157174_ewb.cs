namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ewb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.api_bal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Qty = c.Int(nullable: false),
                        ApiType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.einvs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefId = c.Int(nullable: false),
                        RefVoucherId = c.Int(nullable: false),
                        AckNo = c.String(),
                        AckDt = c.String(),
                        Irn = c.String(),
                        SignedInvoice = c.String(),
                        SignedQrCode = c.String(),
                        Status = c.String(),
                        EwbNo = c.String(),
                        EwbDt = c.String(),
                        EwbValidTill = c.String(),
                        QrCodeImage = c.Binary(),
                        JwtIssuer = c.String(),
                        RefRowId = c.Guid(nullable: false),
                        TransType = c.String(),
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
                .Index(t => t.RefId)
                .Index(t => t.RefVoucherId)
                .Index(t => t.RefRowId);
            
            CreateTable(
                "dbo.ewbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefId = c.Int(nullable: false),
                        RefVoucherId = c.Int(nullable: false),
                        DespatchFromId = c.Int(nullable: false),
                        ModeOfTrans = c.String(maxLength: 3),
                        SubType = c.String(maxLength: 3),
                        ShiptToId = c.Int(nullable: false),
                        ShipToPin = c.String(maxLength: 10),
                        DocType = c.String(maxLength: 5),
                        Distance = c.Int(nullable: false),
                        VehicleNo = c.String(maxLength: 10),
                        VehicleType = c.String(maxLength: 3),
                        EwbNo = c.String(maxLength: 50),
                        TransId = c.Int(),
                        DocNo = c.String(maxLength: 50),
                        DocDate = c.String(maxLength: 50),
                        TransactionType = c.String(maxLength: 3),
                        RefRowId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.State", t => t.DespatchFromId)
                .ForeignKey("dbo.Acc", t => t.TransId)
                .Index(t => t.RefId)
                .Index(t => t.RefVoucherId)
                .Index(t => t.DespatchFromId)
                .Index(t => t.TransId)
                .Index(t => t.RefRowId);
            
            AddColumn("dbo.Company", "AppKey", c => c.String(maxLength: 50));
            AddColumn("dbo.Company", "AuthToken", c => c.String(maxLength: 50));
            AddColumn("dbo.Company", "SEK", c => c.String(maxLength: 50));
            AddColumn("dbo.Company", "TokenExp", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ewbs", "TransId", "dbo.Acc");
            DropForeignKey("dbo.ewbs", "DespatchFromId", "dbo.State");
            DropIndex("dbo.ewbs", new[] { "RefRowId" });
            DropIndex("dbo.ewbs", new[] { "TransId" });
            DropIndex("dbo.ewbs", new[] { "DespatchFromId" });
            DropIndex("dbo.ewbs", new[] { "RefVoucherId" });
            DropIndex("dbo.ewbs", new[] { "RefId" });
            DropIndex("dbo.einvs", new[] { "RefRowId" });
            DropIndex("dbo.einvs", new[] { "RefVoucherId" });
            DropIndex("dbo.einvs", new[] { "RefId" });
            DropColumn("dbo.Company", "TokenExp");
            DropColumn("dbo.Company", "SEK");
            DropColumn("dbo.Company", "AuthToken");
            DropColumn("dbo.Company", "AppKey");
            DropTable("dbo.ewbs");
            DropTable("dbo.einvs");
            DropTable("dbo.api_bal");
        }
    }
}
