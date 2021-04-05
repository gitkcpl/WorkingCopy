namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_weaves : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobCard",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherId = c.Int(),
                        VoucherNo = c.String(maxLength: 50),
                        VoucherDate = c.Int(nullable: false),
                        OrdDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DivId = c.Int(),
                        CompanyId = c.Int(),
                        Type = c.String(maxLength: 50),
                        MachineId = c.Int(),
                        RPUIId = c.Int(),
                        DyeingType = c.String(maxLength: 50),
                        CarrierNo = c.String(maxLength: 50),
                        ProgramNo = c.String(maxLength: 50),
                        OrderId = c.Int(),
                        AccountId = c.Int(),
                        ColorId = c.Int(),
                        ProductId = c.Int(),
                        OrderQty = c.Decimal(precision: 18, scale: 2),
                        GrossWt = c.Decimal(precision: 18, scale: 2),
                        CarrierWt = c.Decimal(precision: 18, scale: 2),
                        NoOfCones = c.Int(),
                        SpringWt = c.Decimal(precision: 18, scale: 2),
                        SpringId = c.Int(),
                        GrayItemId = c.Int(),
                        LotNo = c.String(maxLength: 50),
                        GradeId = c.Int(),
                        BatchId = c.Int(),
                        Qty = c.Decimal(precision: 18, scale: 2),
                        ChallanNo = c.String(maxLength: 50),
                        Rate = c.Decimal(precision: 18, scale: 2),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Remark = c.String(),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobCardTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobCardId = c.Int(),
                        Description = c.String(maxLength: 50),
                        ColorPer = c.Decimal(precision: 18, scale: 2),
                        Meter = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GMeter = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumeQty = c.Decimal(precision: 18, scale: 2),
                        Rate = c.Decimal(precision: 18, scale: 2),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        ColorId = c.Int(),
                        ItemId = c.Int(),
                        Per = c.Decimal(precision: 18, scale: 2),
                        Ply = c.Int(),
                        RefId = c.Int(),
                        DesignId = c.Int(),
                        LotNo = c.String(maxLength: 50),
                        Remark = c.String(),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoadingTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdId = c.Int(nullable: false),
                        LoadingDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DivId = c.Int(),
                        MacId = c.Int(),
                        BeamPotion = c.String(maxLength: 50),
                        ProductId = c.Int(),
                        ProdStatus = c.String(maxLength: 50),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MachineMaster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MachineName = c.String(maxLength: 25),
                        Remark = c.String(),
                        CompanyID = c.Int(nullable: false),
                        DivId = c.Int(),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prod_Emp",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdId = c.Int(),
                        VoucherId = c.Int(),
                        LoadingTransId = c.Int(),
                        ProdDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EmpId = c.Int(nullable: false),
                        NightMtrs = c.Decimal(precision: 18, scale: 2),
                        DayMtrs = c.Decimal(precision: 18, scale: 2),
                        TotalMtrs = c.Decimal(precision: 18, scale: 2),
                        Rate = c.Decimal(precision: 18, scale: 2),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Prod_Weft",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdId = c.Int(),
                        ProductId = c.Int(nullable: false),
                        Denier = c.Decimal(precision: 18, scale: 2),
                        PI = c.Decimal(precision: 18, scale: 2),
                        Qty = c.Decimal(precision: 18, scale: 2),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Prod_Weft");
            DropTable("dbo.Prod_Emp");
            DropTable("dbo.MachineMaster");
            DropTable("dbo.LoadingTrans");
            DropTable("dbo.JobCardTrans");
            DropTable("dbo.JobCard");
        }
    }
}
