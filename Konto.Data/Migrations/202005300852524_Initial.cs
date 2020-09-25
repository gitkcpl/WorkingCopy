namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccAddress",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccId = c.Int(nullable: false),
                        Address1 = c.String(maxLength: 200),
                        Address2 = c.String(maxLength: 200),
                        CityId = c.Int(),
                        AreaId = c.Int(),
                        PinCode = c.String(maxLength: 15),
                        ContactPerson = c.String(maxLength: 75),
                        MobileNo = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 200),
                        Website = c.String(maxLength: 50),
                        RouteId = c.Int(),
                        IsDefault = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifyUser = c.String(),
                        AddressType = c.String(nullable: false),
                        Others = c.String(),
                        RowId = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Acc", t => t.AccId)
                .ForeignKey("dbo.Area", t => t.AreaId)
                .ForeignKey("dbo.City", t => t.CityId)
                .ForeignKey("dbo.Route", t => t.RouteId)
                .Index(t => t.AccId)
                .Index(t => t.CityId)
                .Index(t => t.AreaId)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.Acc",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccName = c.String(nullable: false, maxLength: 100),
                        PrintName = c.String(nullable: false, maxLength: 100),
                        GroupId = c.Int(),
                        PGroupId = c.Int(),
                        TdsReq = c.String(nullable: false, maxLength: 3),
                        TcsReq = c.String(nullable: false, maxLength: 3),
                        VatTds = c.String(nullable: false, maxLength: 5),
                        IoTax = c.String(nullable: false, maxLength: 6),
                        DeducteeId = c.Int(nullable: false),
                        NopId = c.Int(nullable: false),
                        CrDays = c.Int(nullable: false),
                        CrLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BToB = c.String(nullable: false, maxLength: 3),
                        AgentId = c.Int(nullable: false),
                        TransportId = c.Int(nullable: false),
                        EmpId = c.Int(nullable: false),
                        AadharNo = c.String(maxLength: 12),
                        GstIn = c.String(maxLength: 15),
                        GSTDate = c.DateTime(),
                        PanNo = c.String(maxLength: 10),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
                        Grade = c.String(),
                        CollDay = c.String(),
                        CollById = c.Int(),
                        AddressId = c.Int(nullable: false),
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
                .ForeignKey("dbo.AcGroup", t => t.GroupId)
                .ForeignKey("dbo.Deductee", t => t.DeducteeId)
                .ForeignKey("dbo.Emp", t => t.EmpId)
                .ForeignKey("dbo.Nop", t => t.NopId)
                .ForeignKey("dbo.PartyGroup", t => t.PGroupId)
                .Index(t => t.GroupId)
                .Index(t => t.PGroupId)
                .Index(t => t.DeducteeId)
                .Index(t => t.NopId)
                .Index(t => t.EmpId);
            
            CreateTable(
                "dbo.AccBank",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccId = c.Int(),
                        BankName = c.String(nullable: false, maxLength: 50),
                        BranchName = c.String(maxLength: 50),
                        Address = c.String(maxLength: 100),
                        IfsCode = c.String(maxLength: 15),
                        AccountNo = c.String(nullable: false, maxLength: 50),
                        RowId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifyUser = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Acc", t => t.AccId)
                .Index(t => t.AccId);
            
            CreateTable(
                "dbo.AcGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupCode = c.String(maxLength: 15),
                        GroupName = c.String(maxLength: 50),
                        UnderGroupId = c.Int(nullable: false),
                        OppSideName = c.String(maxLength: 50),
                        Nature = c.String(maxLength: 30),
                        Remark = c.String(),
                        BlSort = c.Int(nullable: false),
                        TbSort = c.Int(nullable: false),
                        TrSort = c.Int(nullable: false),
                        OnlyTotal = c.Boolean(nullable: false),
                        OpBalanceReq = c.Boolean(nullable: false),
                        AgentReq = c.Boolean(nullable: false),
                        TransportReq = c.Boolean(nullable: false),
                        AddressReq = c.Boolean(nullable: false),
                        TaxationReq = c.Boolean(nullable: false),
                        SalesmanReq = c.Boolean(nullable: false),
                        BankDetailReq = c.Boolean(nullable: false),
                        PartyGroupReq = c.Boolean(nullable: false),
                        PriceLevelReq = c.Boolean(nullable: false),
                        CollDaysReq = c.Boolean(nullable: false),
                        IntAccountReq = c.Boolean(nullable: false),
                        DeprAccountReq = c.Boolean(nullable: false),
                        TcsReq = c.Boolean(nullable: false),
                        TdsReq = c.Boolean(nullable: false),
                        TaxTypeReq = c.Boolean(nullable: false),
                        CrLimitReq = c.Boolean(nullable: false),
                        GradeReq = c.Boolean(nullable: false),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.Deductee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descr = c.String(nullable: false, maxLength: 100),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false,defaultValue:true),
                        IsDeleted = c.Boolean(nullable: false,defaultValue:false),
                        CreateDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Emp",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpName = c.String(nullable: false, maxLength: 75),
                        Address1 = c.String(maxLength: 100),
                        Address2 = c.String(maxLength: 100),
                        MobileNo = c.String(maxLength: 15),
                        Email = c.String(maxLength: 50),
                        AadharNo = c.String(maxLength: 15),
                        PanNo = c.String(maxLength: 15),
                        Remark = c.String(),
                        CompId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Company", t => t.CompId)
                .Index(t => t.CompId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompName = c.String(maxLength: 75),
                        PrintName = c.String(maxLength: 75),
                        SortName = c.String(maxLength: 15),
                        Address1 = c.String(maxLength: 100),
                        Address2 = c.String(maxLength: 100),
                        CityId = c.Int(),
                        Pincode = c.String(maxLength: 15),
                        StateId = c.Int(),
                        FAddress1 = c.String(maxLength: 100),
                        FAddress2 = c.String(maxLength: 100),
                        FCityId = c.Int(),
                        FPincode = c.String(maxLength: 15),
                        FStateId = c.Int(),
                        Mobile = c.String(maxLength: 10),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Website = c.String(maxLength: 50),
                        Para = c.String(maxLength: 25),
                        GstIn = c.String(maxLength: 20),
                        PanNo = c.String(maxLength: 20),
                        AadharNo = c.String(maxLength: 20),
                        TdsAcNo = c.String(maxLength: 50),
                        Remark = c.String(),
                        AcNo = c.String(maxLength: 25),
                        BankName = c.String(maxLength: 50),
                        HolyWorld = c.String(maxLength: 500),
                        IfsCode = c.String(maxLength: 50),
                        Insurance = c.String(maxLength: 100),
                        SendFrom = c.String(maxLength: 50),
                        SendPass = c.String(maxLength: 50),
                        LogoPath = c.String(),
                        Extra1 = c.String(),
                        Extra2 = c.String(maxLength: 50),
                        NobId = c.Int(),
                        EmailPass = c.String(maxLength: 50),
                        PromotionalAPI = c.String(),
                        TransactionalAPI = c.String(maxLength: 500),
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
                "dbo.Branch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchCode = c.String(maxLength: 10),
                        BranchName = c.String(maxLength: 75),
                        CompId = c.Int(),
                        Address1 = c.String(maxLength: 100),
                        Address2 = c.String(maxLength: 100),
                        CityId = c.Int(),
                        AreaId = c.Int(),
                        PinCode = c.String(maxLength: 10),
                        GstIn = c.String(maxLength: 25),
                        AadharNo = c.String(maxLength: 25),
                        ContactPerson = c.String(maxLength: 50),
                        MobileNo = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 200),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                .ForeignKey("dbo.Company", t => t.CompId)
                .Index(t => t.CompId);
            
            CreateTable(
                "dbo.Division",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DivisionName = c.String(maxLength: 50),
                        BranchId = c.Int(),
                        Remark = c.String(maxLength: 200),
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
                .ForeignKey("dbo.Branch", t => t.BranchId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Nop",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descr = c.String(nullable: false, maxLength: 300),
                        SecCode = c.String(maxLength: 15),
                        SecNo = c.String(maxLength: 15),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                        CreateDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PartyGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false, maxLength: 75),
                        Address = c.String(maxLength: 200),
                        ContactNo = c.String(maxLength: 25),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.Area",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AreaName = c.String(maxLength: 50),
                        CityId = c.Int(nullable: false),
                        PinCode = c.String(maxLength: 10),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                .ForeignKey("dbo.City", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(maxLength: 50),
                        StateId = c.Int(nullable: false),
                        StdCode = c.String(maxLength: 50),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                .ForeignKey("dbo.State", t => t.StateId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(maxLength: 50),
                        CountryId = c.Int(nullable: false),
                        GstCode = c.String(maxLength: 5),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                .ForeignKey("dbo.Country", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryCode = c.String(maxLength: 15),
                        CountryName = c.String(maxLength: 50),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.Route",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RouteCode = c.String(maxLength: 15),
                        RouteName = c.String(nullable: false, maxLength: 50),
                        CityId = c.Int(nullable: false),
                        AreaId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Area", t => t.AreaId)
                .ForeignKey("dbo.City", t => t.CityId)
                .Index(t => t.CityId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.AccBal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        Bal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpBal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpDebit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpCredit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CompId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        Share = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccRowId = c.Guid(),
                        Address1 = c.String(maxLength: 200),
                        Address2 = c.String(maxLength: 200),
                        CityId = c.Int(),
                        AreaId = c.Int(),
                        PinCode = c.String(maxLength: 15),
                        ContactPerson = c.String(maxLength: 75),
                        MobileNo = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Website = c.String(maxLength: 50),
                        RouteId = c.Int(),
                        Others = c.String(),
                        RowId = c.Guid(nullable: false, identity: true),
                        ModifyUser = c.String(),
                        Audit = c.Boolean(nullable: false,defaultValue:false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Acc", t => t.AccId)
                .ForeignKey("dbo.AcGroup", t => t.GroupId)
                .ForeignKey("dbo.Company", t => t.CompId)
                .ForeignKey("dbo.FinYear", t => t.YearId)
                .Index(t => t.AccId)
                .Index(t => t.GroupId)
                .Index(t => t.CompId)
                .Index(t => t.YearId);
            
            CreateTable(
                "dbo.FinYear",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YearCode = c.String(maxLength: 50),
                        FromDate = c.Int(),
                        ToDate = c.Int(),
                        FDate = c.DateTime(nullable: false),
                        TDate = c.DateTime(nullable: false),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.AccOther",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccId = c.Int(),
                        OpStockAccId = c.Int(),
                        IntAccId = c.Int(),
                        IntTdsAccId = c.Int(),
                        IntPer = c.Decimal(precision: 18, scale: 2),
                        IntTdsPer = c.Decimal(precision: 18, scale: 2),
                        TdsDrCr = c.String(maxLength: 2),
                        TcsAccId = c.Int(),
                        TcsPer = c.Decimal(precision: 18, scale: 2),
                        TdsAccId = c.Int(),
                        TdsPer = c.Decimal(precision: 18, scale: 2),
                        DepAccId = c.Int(),
                        DepPer = c.Decimal(precision: 18, scale: 2),
                        RowId = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ModifyUser = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Acc", t => t.AccId)
                .ForeignKey("dbo.Acc", t => t.DepAccId)
                .ForeignKey("dbo.Acc", t => t.IntAccId)
                .ForeignKey("dbo.Acc", t => t.IntTdsAccId)
                .ForeignKey("dbo.Acc", t => t.OpStockAccId)
                .ForeignKey("dbo.Acc", t => t.TcsAccId)
                .ForeignKey("dbo.Acc", t => t.TdsAccId)
                .Index(t => t.AccId)
                .Index(t => t.OpStockAccId)
                .Index(t => t.IntAccId)
                .Index(t => t.IntTdsAccId)
                .Index(t => t.TcsAccId)
                .Index(t => t.TdsAccId)
                .Index(t => t.DepAccId);
            
            CreateTable(
                "dbo.Attachment",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        RefVoucherId = c.Int(nullable: false),
                        VoucherId = c.Int(nullable: false),
                        FilePath = c.String(nullable: false),
                        FileDescr = c.String(),
                        FileCatId = c.Int(nullable: false),
                        FileSubCatId = c.Int(nullable: false),
                        FileStatus = c.String(maxLength: 50),
                        DeptId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        PublishDate = c.DateTime(),
                        ExpireDate = c.DateTime(),
                        KeyWords = c.String(maxLength: 200),
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
                "dbo.AuditLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EntityName = c.String(maxLength: 50),
                        PropertyName = c.String(maxLength: 50),
                        PrimaryKeyValue = c.Int(),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        DateChanged = c.DateTime(),
                        UserName = c.String(maxLength: 50),
                        EntryMode = c.String(maxLength: 50),
                        MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BillDelv",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        BillId = c.Int(nullable: false),
                        AccId = c.Int(nullable: false),
                        BillDelDate = c.DateTime(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .ForeignKey("dbo.Acc", t => t.AccId)
                .Index(t => t.AccId);
            
            CreateTable(
                "dbo.BillRef",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(),
                        YearId = c.Int(),
                        BillId = c.Int(),
                        BillVoucherId = c.Int(),
                        BillNo = c.String(maxLength: 50),
                        VoucherNo = c.String(maxLength: 50),
                        VoucherDate = c.Int(),
                        BillTransId = c.Int(),
                        GrossAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BillAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TdsAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TcsAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RetAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdjustAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountId = c.Int(),
                        AgentId = c.Int(),
                        ItemId = c.Int(),
                        BranchId = c.Int(),
                        TotalQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        RefType = c.String(),
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
                "dbo.BillMain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        VoucherId = c.Int(nullable: false),
                        VoucherDate = c.Int(nullable: false),
                        VoucherNo = c.String(maxLength: 25),
                        BillNo = c.String(maxLength: 500),
                        Rcm = c.String(maxLength: 25),
                        Itc = c.String(maxLength: 25),
                        RcdDate = c.DateTime(),
                        HasteId = c.Int(),
                        StateId = c.Int(),
                        TypeId = c.Int(nullable: false),
                        Duedays = c.Int(),
                        TransId = c.Int(),
                        RefId = c.Int(),
                        RefVoucherId = c.Int(),
                        VehicleNo = c.String(maxLength: 50),
                        BillType = c.String(maxLength: 50),
                        DocNo = c.String(maxLength: 25),
                        DocDate = c.DateTime(),
                        VDate = c.DateTime(nullable: false),
                        PortCode = c.String(maxLength: 50),
                        ModeofTrans = c.String(maxLength: 50),
                        EwayBillNo = c.String(maxLength: 50),
                        RefNo = c.String(maxLength: 25),
                        BookAcId = c.Int(),
                        AccId = c.Int(nullable: false),
                        EmpId = c.Int(),
                        StoreId = c.Int(),
                        DelvAccId = c.Int(),
                        DelvAdrId = c.Int(),
                        AddrId = c.Int(),
                        DName = c.String(maxLength: 50),
                        Currency = c.String(maxLength: 50),
                        ExchRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AgentId = c.Int(),
                        BranchId = c.Int(),
                        RequireDate = c.DateTime(),
                        DivisionId = c.Int(),
                        Remarks = c.String(),
                        SpecialNotes = c.String(maxLength: 500),
                        TotalPcs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TdsPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TdsAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TcsPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TcsAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OceanFrtP = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OceanFrtA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 500),
                        Auth = c.Boolean(),
                        RoundOff = c.Decimal(precision: 18, scale: 2),
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
                .ForeignKey("dbo.Voucher", t => t.VoucherId)
                .Index(t => t.VoucherId);
            
            CreateTable(
                "dbo.Voucher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherName = c.String(maxLength: 50),
                        SortName = c.String(maxLength: 4),
                        VTypeId = c.Int(nullable: false),
                        RefVoucherId = c.Int(),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
                        BookAcId = c.Int(),
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
                .ForeignKey("dbo.VoucherType", t => t.VTypeId)
                .Index(t => t.VTypeId);
            
            CreateTable(
                "dbo.VchSetup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherId = c.Int(nullable: false),
                        CompId = c.Int(nullable: false),
                        InvoiceHeading = c.String(maxLength: 50),
                        VchWidth = c.Int(nullable: false),
                        PreFillZero = c.Boolean(nullable: false),
                        StartFrom = c.Int(nullable: false),
                        Increment = c.Int(nullable: false),
                        Serial_Mask = c.String(maxLength: 25),
                        Max_Value = c.Int(),
                        Last_Serial = c.Int(nullable: false),
                        FyReset = c.Boolean(nullable: false),
                        PrintAfterSave = c.Boolean(nullable: false),
                        EmailAfterSave = c.Boolean(nullable: false),
                        SmsAfterSave = c.Boolean(nullable: false),
                        BookFix = c.Boolean(nullable: false),
                        AccId = c.Int(),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                .ForeignKey("dbo.Company", t => t.CompId)
                .ForeignKey("dbo.Voucher", t => t.VoucherId)
                .Index(t => t.VoucherId)
                .Index(t => t.CompId);
            
            CreateTable(
                "dbo.VoucherType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(maxLength: 50),
                        Terms = c.String(),
                        SendSms = c.Boolean(),
                        SmsTemplates = c.String(maxLength: 300),
                        SmsToParty = c.Boolean(),
                        SmsToAgent = c.Boolean(),
                        SmsToUser = c.Boolean(),
                        OtherMobile = c.String(maxLength: 100),
                        SendMail = c.Boolean(),
                        EmailSub = c.String(maxLength: 50),
                        EmailBody = c.String(),
                        EmailToParty = c.Boolean(),
                        EmailToAgent = c.Boolean(),
                        EmailToUser = c.Boolean(),
                        OtherEmail = c.String(maxLength: 100),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.BillTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillId = c.Int(),
                        ProductId = c.Int(),
                        HsnCode = c.String(),
                        ColorId = c.Int(),
                        DesignId = c.Int(),
                        GradeId = c.Int(),
                        RefBankId = c.Int(),
                        ToAccId = c.Int(),
                        ChequeNo = c.String(maxLength: 50),
                        ChequeDate = c.DateTime(),
                        BatchId = c.Int(),
                        LotNo = c.String(maxLength: 50),
                        AvgWt = c.Decimal(precision: 18, scale: 2),
                        Cut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Width = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pcs = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UomId = c.Int(),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Disc = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OceanFrt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomD = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FreightRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Freight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherAdd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherLess = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SgstPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CgstPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IgstPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Igst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CessPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cess = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TdsPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TdsAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TcsPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TcsAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(),
                        CommDescr = c.String(maxLength: 200),
                        DeptId = c.Int(),
                        DivisionId = c.Int(),
                        RpType = c.String(maxLength: 50),
                        RefId = c.Int(),
                        RefTransId = c.Int(),
                        RefVoucherId = c.Int(),
                        BankDate = c.Int(),
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
                "dbo.Brand",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandCode = c.String(maxLength: 15),
                        BrandName = c.String(nullable: false, maxLength: 50),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.BtoB",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefCode = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BillClear = c.String(maxLength: 1),
                        TransType = c.String(maxLength: 15),
                        BillVoucherId = c.Int(),
                        CompanyId = c.Int(),
                        BillId = c.Int(),
                        BillTransId = c.Int(),
                        RefId = c.Int(),
                        RefTransId = c.Int(),
                        RefVoucherId = c.Int(nullable: false),
                        BillNo = c.String(maxLength: 50),
                        Adlp1 = c.Decimal(precision: 18, scale: 2),
                        Adla1 = c.Decimal(precision: 18, scale: 2),
                        Adlp2 = c.Decimal(precision: 18, scale: 2),
                        Adla2 = c.Decimal(precision: 18, scale: 2),
                        Adlp3 = c.Decimal(precision: 18, scale: 2),
                        Adla3 = c.Decimal(precision: 18, scale: 2),
                        Adlp4 = c.Decimal(precision: 18, scale: 2),
                        Adla4 = c.Decimal(precision: 18, scale: 2),
                        Adlp5 = c.Decimal(precision: 18, scale: 2),
                        Adla5 = c.Decimal(precision: 18, scale: 2),
                        Adlp6 = c.Decimal(precision: 18, scale: 2),
                        Adla6 = c.Decimal(precision: 18, scale: 2),
                        Adlp7 = c.Decimal(precision: 18, scale: 2),
                        Adla7 = c.Decimal(precision: 18, scale: 2),
                        Adlp8 = c.Decimal(precision: 18, scale: 2),
                        Adla8 = c.Decimal(precision: 18, scale: 2),
                        Adlp9 = c.Decimal(precision: 18, scale: 2),
                        Adla9 = c.Decimal(precision: 18, scale: 2),
                        Adlp10 = c.Decimal(precision: 18, scale: 2),
                        Adla10 = c.Decimal(precision: 18, scale: 2),
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
                "dbo.Catalog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatalogNo = c.String(maxLength: 50),
                        CatalogName = c.String(nullable: false, maxLength: 500),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.PCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatCode = c.String(maxLength: 15),
                        CatName = c.String(nullable: false, maxLength: 50),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.ChallanDelv",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChlId = c.Int(nullable: false),
                        AccId = c.Int(nullable: false),
                        ChlDelDate = c.DateTime(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .ForeignKey("dbo.Acc", t => t.AccId)
                .Index(t => t.AccId);
            
            CreateTable(
                "dbo.Challan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompId = c.Int(),
                        StoreId = c.Int(),
                        VoucherId = c.Int(nullable: false),
                        VoucherNo = c.String(maxLength: 25),
                        VoucherDate = c.Int(nullable: false),
                        ChallanNo = c.String(nullable: false, maxLength: 25),
                        BillNo = c.String(maxLength: 25),
                        RcdDate = c.DateTime(),
                        AccId = c.Int(nullable: false),
                        AgentId = c.Int(),
                        DivId = c.Int(nullable: false),
                        YearId = c.Int(),
                        DocNo = c.String(maxLength: 25),
                        DocDate = c.DateTime(),
                        TransId = c.Int(),
                        ProcessId = c.Int(),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 200),
                        Extra2 = c.String(maxLength: 200),
                        Extra3 = c.String(maxLength: 200),
                        Extra4 = c.String(maxLength: 200),
                        MasterId = c.Int(),
                        ChallanType = c.Int(nullable: false),
                        DelvAccId = c.Int(),
                        TotalQty = c.Decimal(precision: 18, scale: 2),
                        TotalPcs = c.Decimal(precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BillId = c.Int(),
                        VehicleNo = c.String(maxLength: 25),
                        BranchId = c.Int(),
                        EmpId = c.Int(),
                        DName = c.String(maxLength: 250),
                        DelvAdrId = c.Int(),
                        AddrId = c.Int(),
                        TypeId = c.Int(nullable: false),
                        RoundOff = c.Decimal(precision: 18, scale: 2),
                        TdsPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TdsAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookAcId = c.Int(nullable: false),
                        Rcm = c.String(maxLength: 25),
                        Itc = c.String(maxLength: 25),
                        BillType = c.String(maxLength: 50),
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
                "dbo.ChallanTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChallanId = c.Int(),
                        ProductId = c.Int(),
                        NProductId = c.Int(),
                        ColorId = c.Int(),
                        BatchId = c.Int(),
                        LotNo = c.String(maxLength: 50),
                        RefNo = c.String(maxLength: 50),
                        GradeId = c.Int(),
                        DesignId = c.Int(),
                        Cops = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pcs = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UomId = c.Int(),
                        Gross = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Disc = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FreightRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Freight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherAdd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OtherLess = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CgstPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SgstPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IgstPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Igst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(),
                        RefId = c.Int(),
                        RefVoucherId = c.Int(),
                        MiscId = c.Int(),
                        IssueQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IssuePcs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CessPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cess = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .ForeignKey("dbo.Challan", t => t.ChallanId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ChallanId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DesignDate = c.Int(),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        ProductDesc = c.String(nullable: false, maxLength: 500),
                        ProductCode = c.String(maxLength: 25),
                        BarCode = c.String(maxLength: 25),
                        HsnCode = c.String(nullable: false, maxLength: 15),
                        TaxId = c.Int(nullable: false),
                        PTypeId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        SubGroupId = c.Int(nullable: false),
                        SizeId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        StyleId = c.Int(nullable: false),
                        ColorId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        UomId = c.Int(nullable: false),
                        PurUomId = c.Int(nullable: false),
                        PurDisc = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurSpDisc = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleDisc = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleSpDisc = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ActualCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockReq = c.String(nullable: false, maxLength: 5),
                        BatchReq = c.String(nullable: false, maxLength: 5),
                        SerialReq = c.String(nullable: false, maxLength: 5),
                        MinLevel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxLevel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rol = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinOrdQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxOrdQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LeadDays = c.Int(nullable: false),
                        CheckNegative = c.Boolean(nullable: false),
                        Ingr = c.String(maxLength: 500),
                        Remark = c.String(),
                        SaleRateTaxInc = c.Boolean(nullable: false),
                        VendorId = c.Int(),
                        StockAcId = c.Int(),
                        Sales = c.Boolean(nullable: false),
                        Purchase = c.Boolean(nullable: false),
                        Inventory = c.Boolean(nullable: false),
                        FixedAsset = c.Boolean(nullable: false),
                        WorkOrder = c.Boolean(nullable: false),
                        StdWt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemType = c.String(maxLength: 1),
                        AccId = c.Int(),
                        PartyItemId = c.Int(),
                        ParentItemId = c.Int(),
                        FabricTopId = c.Int(),
                        FabricBottomId = c.Int(),
                        FabricDupattaId = c.Int(),
                        CatalogId = c.Int(),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                        StyleModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductType", t => t.PTypeId)
                .ForeignKey("dbo.Style", t => t.StyleModel_Id)
                .Index(t => t.PTypeId)
                .Index(t => t.StyleModel_Id);
            
            CreateTable(
                "dbo.ProductPrice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        BatchNo = c.String(maxLength: 25),
                        MfgDate = c.String(maxLength: 10),
                        ExpDate = c.String(maxLength: 10),
                        DealerPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SaleRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Mrp = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BranchId = c.Int(),
                        IssueQty = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeCode = c.String(maxLength: 15),
                        TypeName = c.String(maxLength: 50),
                        Remark = c.String(),
                        SysType = c.String(maxLength: 50),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.Color",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ColorCode = c.String(maxLength: 15),
                        ColorName = c.String(nullable: false, maxLength: 50),
                        RGB = c.String(maxLength: 50),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.ColorSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        ColorId = c.Int(),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.CompPara",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompId = c.Int(),
                        ParaId = c.Int(),
                        ParaValue = c.String(maxLength: 200),
                        Remark = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompId)
                .ForeignKey("dbo.SysPara", t => t.ParaId)
                .Index(t => t.CompId)
                .Index(t => t.ParaId);
            
            CreateTable(
                "dbo.SysPara",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descr = c.String(maxLength: 200),
                        DefaultValue = c.String(maxLength: 200),
                        ValueDescr = c.String(maxLength: 200),
                        Category = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false,defaultValueSql: "newsequentialid()"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomRep",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RepCode = c.Guid(nullable: false),
                        ReportTypes = c.String(maxLength: 50),
                        RepId = c.Int(),
                        FieldName = c.String(maxLength: 50),
                        Show = c.Boolean(nullable: false),
                        OrderIndex = c.Int(nullable: false),
                        Heading = c.String(maxLength: 50),
                        Width = c.Int(nullable: false),
                        GroupIndex = c.Int(nullable: false),
                        SortIndex = c.Int(nullable: false),
                        Summary = c.Boolean(nullable: false),
                        GroupSummary = c.Boolean(nullable: false),
                        SummaryType = c.String(maxLength: 50),
                        Appearance = c.String(maxLength: 50),
                        HeaderText = c.String(maxLength: 500),
                        FooterText = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbVersion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        UpgradeDate = c.DateTime(nullable: false),
                        Version = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.data_freeze",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromDate = c.Int(),
                        ToDate = c.Int(),
                        VoucherTypeID = c.Long(),
                        Freeze = c.Boolean(),
                        Pass = c.String(maxLength: 10),
                        CompanyID = c.Int(),
                        ModifyDate = c.DateTime(),
                        ModifyUser = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailSmsLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RowId = c.Guid(nullable: false),
                        TransType = c.String(maxLength: 2),
                        EmailTo = c.String(maxLength: 1000),
                        SmsTo = c.String(maxLength: 200),
                        EmailSub = c.String(maxLength: 200),
                        EmailBody = c.String(),
                        EmalHeader = c.String(),
                        EmailFooter = c.String(),
                        SmsMsg = c.String(maxLength: 500),
                        ScheduleTime = c.DateTime(),
                        UserName = c.String(maxLength: 50),
                        CompanyName = c.String(maxLength: 100),
                        Sended = c.Boolean(nullable: false),
                        SendTime = c.DateTime(),
                        ResponseMsg = c.String(),
                        AttachFile = c.String(maxLength: 1000),
                        FromMailId = c.String(maxLength: 100),
                        FromMailPass = c.Binary(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ErpModule",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ParentId = c.Int(),
                        ModuleDesc = c.String(maxLength: 50),
                        OrderIndex = c.Int(),
                        LinkButton = c.String(maxLength: 50),
                        ShortCutKey = c.String(maxLength: 25),
                        PackageId = c.Int(),
                        DefaultReport = c.String(maxLength: 50),
                        DefaultLayout = c.String(maxLength: 50),
                        TableName = c.String(maxLength: 50),
                        AssemblyName = c.String(maxLength: 75),
                        MainAssembly = c.String(maxLength: 50),
                        ListAssembly = c.String(maxLength: 75),
                        MDI = c.Boolean(),
                        Title = c.String(maxLength: 50),
                        Visible = c.Boolean(nullable: false),
                        IconPath = c.String(maxLength: 50),
                        CheckRight = c.Boolean(),
                        VisibleOnDashBoard = c.Boolean(),
                        VisibleOnSideBar = c.Boolean(),
                        IsSeprator = c.Boolean(nullable: false),
                        Offline = c.Boolean(nullable: false),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
                        ImageIndex = c.Int(),
                        RowId = c.Guid(nullable: false, defaultValueSql: "newsequentialid()"),
                        IsActive = c.Boolean(nullable: false,defaultValue:true),
                        IsDeleted = c.Boolean(nullable: false,defaultValue:false),
                        CreateDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Grade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GradeName = c.String(nullable: false, maxLength: 50),
                        Remark = c.String(maxLength: 50),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
                        RefGradeId = c.Int(),
                        StartWt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EndWt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RateDiff = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                "dbo.Haste",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MType = c.String(maxLength: 50),
                        HasteName = c.String(maxLength: 200),
                        Address1 = c.String(maxLength: 100),
                        Address2 = c.String(maxLength: 100),
                        MobileNo = c.String(maxLength: 15),
                        Email = c.String(maxLength: 50),
                        AadharNo = c.String(maxLength: 15),
                        PanNo = c.String(maxLength: 15),
                        Remark = c.String(),
                        CompId = c.Int(),
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
                .ForeignKey("dbo.Company", t => t.CompId)
                .Index(t => t.CompId);
            
            CreateTable(
                "dbo.JobReceipt",
                c => new
                    {
                        Id = c.Int(nullable: false,identity:true),
                        ChallanId = c.Int(),
                        RefId = c.Int(),
                        ColorId = c.Int(),
                        ProductId = c.Int(),
                        RefTransId = c.Int(),
                        VoucherId = c.Int(),
                        Qty = c.Decimal(precision: 18, scale: 2),
                        Pcs = c.Decimal(precision: 18, scale: 2),
                        PendingQty = c.Decimal(precision: 18, scale: 2),
                        PendingPcs = c.Decimal(precision: 18, scale: 2),
                        IssueQty = c.Decimal(precision: 18, scale: 2),
                        IssuePcs = c.Decimal(precision: 18, scale: 2),
                        IsClear = c.Boolean(nullable: false),
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
                .ForeignKey("dbo.Challan", t => t.ChallanId)
                .Index(t => t.ChallanId);
            
            CreateTable(
                "dbo.LedgerTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefId = c.Guid(nullable: false),
                        TransType = c.Int(),
                        CompanyId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        VoucherId = c.Int(nullable: false),
                        VoucherDate = c.Int(nullable: false),
                        TransDate = c.DateTime(nullable: false),
                        BillNo = c.String(maxLength: 30),
                        VoucherNo = c.String(maxLength: 30),
                        AccountId = c.Int(nullable: false),
                        RefAccountId = c.Int(),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BilllAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChqDate = c.DateTime(),
                        ChqNo = c.String(maxLength: 50),
                        AgentId = c.Int(),
                        TableName = c.String(maxLength: 50),
                        KeyFldValue = c.Int(),
                        Narration = c.String(),
                        Remark = c.String(maxLength: 2000),
                        LrDate = c.DateTime(),
                        LrNo = c.String(),
                        ReconDate = c.Int(),
                        Audit = c.Boolean(),
                        AdjustAmount = c.Decimal(precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransCode = c.Guid(),
                        RetAmount = c.Decimal(precision: 18, scale: 2),
                        TdsAmount = c.Decimal(precision: 18, scale: 2),
                        OpBill = c.String(maxLength: 1),
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
                "dbo.ListPage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descr = c.String(maxLength: 200),
                        SpName = c.String(maxLength: 50),
                        LayoutFile = c.String(maxLength: 200),
                        VTypeId = c.Int(),
                        GroupCol = c.String(maxLength: 200),
                        SumCol = c.String(maxLength: 200),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menu_Package",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageId = c.Int(),
                        MenuId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Nob",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusinessType = c.String(maxLength: 100),
                        Extra1 = c.String(),
                        Extra2 = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrdDelv",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrdId = c.Int(nullable: false),
                        AccId = c.Int(nullable: false),
                        OrdDelvDate = c.DateTime(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .ForeignKey("dbo.Acc", t => t.AccId)
                .Index(t => t.AccId);
            
            CreateTable(
                "dbo.Ord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        VoucherId = c.Int(nullable: false),
                        VoucherDate = c.Int(nullable: false),
                        VoucherNo = c.String(nullable: false, maxLength: 25),
                        RefNo = c.String(maxLength: 25),
                        AccId = c.Int(nullable: false),
                        OrderStatusId = c.Int(nullable: false),
                        EmpId = c.Int(nullable: false),
                        CheckerId = c.Int(nullable: false),
                        CheckRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CheckUomId = c.Int(nullable: false),
                        EmpRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EmpUomId = c.Int(nullable: false),
                        CheckerOutId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                        Currency = c.String(maxLength: 50),
                        ExchRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AgentId = c.Int(nullable: false),
                        TransportId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        RequireDate = c.DateTime(nullable: false),
                        OrderType = c.String(maxLength: 25),
                        DivisionId = c.Int(nullable: false),
                        PGroupId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Remarks = c.String(),
                        SpecialNotes = c.String(maxLength: 500),
                        PayTermsId = c.Int(nullable: false),
                        TotalPcs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
                        Auth = c.Boolean(nullable: false),
                        RefId = c.Int(nullable: false),
                        RefVoucherId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Acc", t => t.AccId)
                .ForeignKey("dbo.Company", t => t.CompId)
                .ForeignKey("dbo.Emp", t => t.EmpId)
                .ForeignKey("dbo.Voucher", t => t.VoucherId)
                .Index(t => t.CompId)
                .Index(t => t.VoucherId)
                .Index(t => t.AccId)
                .Index(t => t.EmpId);
            
            CreateTable(
                "dbo.OrdTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrdId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ColorId = c.Int(nullable: false),
                        DesignId = c.Int(nullable: false),
                        GradeId = c.Int(nullable: false),
                        AvgWt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Width = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NoOfLot = c.Int(nullable: false),
                        LotPcs = c.Int(nullable: false),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UomId = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Disc = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SgstAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CgstAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Igst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IgstAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cess = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CessAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(maxLength: 200),
                        CommDescr = c.String(maxLength: 200),
                        DeptId = c.Int(nullable: false),
                        DivisionId = c.Int(nullable: false),
                        Priority = c.String(maxLength: 10),
                        OrdStatus = c.String(maxLength: 25),
                        RefId = c.Int(nullable: false),
                        RefVoucherId = c.Int(nullable: false),
                        WarpItemId = c.Int(),
                        CancelReason = c.String(maxLength: 200),
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
                .ForeignKey("dbo.Ord", t => t.OrdId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.OrdId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PermissionDescription = c.String(maxLength: 100),
                        RowId = c.Guid(nullable: false),
                        ModuleId = c.Int(nullable: false),
                        PermissionType = c.String(maxLength: 50),
                        PermissionTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role_Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        RowId = c.Guid(nullable: false),
                        PermissionType = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Permissions", t => t.PermissionId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 50),
                        IsSysAdmin = c.Boolean(nullable: false),
                        RoleDescription = c.String(),
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
                "dbo.UserMaster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50),
                        UserPass = c.String(maxLength: 50),
                        LastLoginDate = c.DateTime(),
                        RoleId = c.Int(nullable: false),
                        EmpId = c.Int(),
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
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PFormula",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        DescType = c.Int(),
                        Qty = c.Decimal(precision: 18, scale: 2),
                        cut = c.Decimal(precision: 18, scale: 2),
                        ColorId = c.Int(),
                        Rate = c.Decimal(precision: 18, scale: 2),
                        Total = c.Decimal(precision: 18, scale: 2),
                        Remark = c.String(maxLength: 500),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.PGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupCode = c.String(maxLength: 15),
                        GroupName = c.String(nullable: false, maxLength: 50),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.PSubGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubCode = c.String(maxLength: 15),
                        SubName = c.String(nullable: false, maxLength: 50),
                        PGroupId = c.Int(nullable: false),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                .ForeignKey("dbo.PGroup", t => t.PGroupId)
                .Index(t => t.PGroupId);
            
            CreateTable(
                "dbo.PImage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Category = c.String(maxLength: 1),
                        ImagePath = c.String(maxLength: 500),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.Process",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessName = c.String(nullable: false, maxLength: 50),
                        HsnCode = c.String(maxLength: 15),
                        Priority = c.Int(nullable: false),
                        TaxId = c.Int(),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                .ForeignKey("dbo.TaxMaster", t => t.TaxId)
                .Index(t => t.TaxId);
            
            CreateTable(
                "dbo.TaxMaster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaxName = c.String(maxLength: 50),
                        TaxType = c.String(maxLength: 25),
                        Sgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Igst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CessType = c.String(maxLength: 25),
                        Cess = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CessRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(maxLength: 100),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.ProdOut",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdId = c.Int(),
                        TransId = c.Int(),
                        SrNo = c.Int(),
                        ProductId = c.Int(),
                        GradeId = c.Int(),
                        RefId = c.Int(),
                        ColorId = c.Int(),
                        CompId = c.Int(),
                        YearId = c.Int(),
                        VoucherId = c.Int(),
                        VoucherNo = c.String(maxLength: 25),
                        GrayMtr = c.Decimal(precision: 18, scale: 2),
                        GrayPcs = c.Decimal(precision: 18, scale: 2),
                        FinMrt = c.Decimal(precision: 18, scale: 2),
                        TP1 = c.Decimal(precision: 18, scale: 2),
                        TP2 = c.Decimal(precision: 18, scale: 2),
                        TP3 = c.Decimal(precision: 18, scale: 2),
                        TP4 = c.Decimal(precision: 18, scale: 2),
                        TP5 = c.Decimal(precision: 18, scale: 2),
                        ShMtr = c.Decimal(precision: 18, scale: 2),
                        ShPer = c.Decimal(precision: 18, scale: 2),
                        Qty = c.Decimal(precision: 18, scale: 2),
                        IsOk = c.Boolean(),
                        Remark = c.String(),
                        VTypeId = c.Int(nullable: false),
                        TakaStatus = c.String(),
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
                .ForeignKey("dbo.Challan", t => t.RefId)
                .ForeignKey("dbo.ChallanTrans", t => t.TransId)
                .ForeignKey("dbo.Voucher", t => t.VoucherId)
                .Index(t => t.TransId)
                .Index(t => t.RefId)
                .Index(t => t.VoucherId);
            
            CreateTable(
                "dbo.Prod",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransId = c.Int(),
                        SrNo = c.Int(),
                        ProductId = c.Int(),
                        GradeId = c.Int(),
                        BatchId = c.Int(),
                        ColorId = c.Int(),
                        PackId = c.Int(),
                        MacId = c.Int(),
                        SubGradeId = c.Int(),
                        TwistType = c.String(maxLength: 50),
                        CompId = c.Int(),
                        YearId = c.Int(),
                        VoucherId = c.Int(),
                        VoucherDate = c.Int(nullable: false),
                        VoucherNo = c.String(nullable: false, maxLength: 25),
                        RefId = c.Int(),
                        RefSCId = c.Int(),
                        Ply = c.Int(nullable: false),
                        Cops = c.Int(nullable: false),
                        CopsWt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BoxWt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CartnWt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossWt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TareWt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetWt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DivId = c.Int(),
                        BranchId = c.Int(),
                        JobId = c.Int(),
                        CopsProductId = c.Int(),
                        CopsRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BoxProductId = c.Int(),
                        BoxRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackEmpId = c.Int(),
                        CheckEmpId = c.Int(),
                        PalletProductId = c.Int(),
                        PlyProductId = c.Int(),
                        DrawingDate = c.DateTime(),
                        LoadingDate = c.DateTime(),
                        WarpingDate = c.DateTime(),
                        CloseDate = c.DateTime(),
                        ProdStatus = c.String(maxLength: 50),
                        Tops = c.Int(nullable: false),
                        Pallet = c.Int(nullable: false),
                        CurrQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IssueRefId = c.Int(),
                        IssueRefVoucherId = c.Int(),
                        Remark = c.String(),
                        LotNo = c.String(),
                        IsClose = c.Boolean(nullable: false),
                        IsOk = c.Boolean(nullable: false),
                        IsMultiple = c.Boolean(nullable: false),
                        VTypeId = c.Int(nullable: false),
                        Extra1 = c.String(maxLength: 200),
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
                .ForeignKey("dbo.Challan", t => t.RefId)
                .ForeignKey("dbo.ChallanTrans", t => t.TransId)
                .ForeignKey("dbo.Color", t => t.ColorId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.TransId)
                .Index(t => t.ProductId)
                .Index(t => t.ColorId)
                .Index(t => t.RefId);
            
            CreateTable(
                "dbo.RefBank",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 200),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.ReportPara",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RowId = c.Guid(nullable: false, identity: true),
                        ReportId = c.Int(nullable: false),
                        ParameterName = c.String(maxLength: 50),
                        ParameterValue = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        IPAddress = c.String(),
                        ModifyUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReportType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RowId = c.Guid(nullable: false, identity: true),
                        ReportName = c.String(maxLength: 100),
                        ReportTypes = c.String(maxLength: 500),
                        VoucherTypeId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Remarks = c.String(maxLength: 500),
                        FileName = c.String(maxLength: 500),
                        CreateUser = c.String(),
                        ModifyUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifyDate = c.DateTime(),
                        SpName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecpaySetting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecPay = c.String(nullable: false, maxLength: 50),
                        Field = c.String(nullable: false, maxLength: 100),
                        PlusMinus = c.String(nullable: false, maxLength: 50),
                        PerCap = c.String(maxLength: 10),
                        AmtCap = c.String(nullable: false, maxLength: 10),
                        AccountId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        CalcOn = c.String(maxLength: 200),
                        Remark = c.String(maxLength: 200),
                        HsnCode = c.String(maxLength: 200),
                        Drcr = c.String(),
                        TaxId = c.Int(),
                        VoucherId = c.Int(),
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
                "dbo.SerialNumbersShelf",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherId = c.Int(nullable: false),
                        Serial_Value = c.Int(nullable: false),
                        Is_Hold = c.Boolean(nullable: false),
                        YearId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PSize",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SizeCode = c.String(maxLength: 15),
                        SizeName = c.String(nullable: false, maxLength: 50),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.SPCollection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Section = c.String(nullable: false, maxLength: 200),
                        Remark = c.String(maxLength: 500),
                        RowId = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SpPara",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpName = c.String(maxLength: 200),
                        ParaName = c.String(maxLength: 200),
                        ParaType = c.String(nullable: false, maxLength: 50),
                        DefaultValue = c.String(maxLength: 200),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductBal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        DivId = c.Int(),
                        ProductId = c.Int(nullable: false),
                        GodownId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        ItemCode = c.Guid(),
                        OpNos = c.Int(nullable: false),
                        OpQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IssueNo = c.Int(nullable: false),
                        IssueQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RcptNos = c.Int(nullable: false),
                        RcptQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BalNos = c.Int(nullable: false),
                        BalQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LotNo = c.String(maxLength: 50),
                        CreateDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                        RowId = c.Guid(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.CompanyId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.StockTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Short(nullable: false),
                        BranchId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        DivId = c.Int(),
                        GodownId = c.Int(nullable: false),
                        RefId = c.Guid(nullable: false),
                        MasterRefId = c.Guid(),
                        VoucherDate = c.Int(nullable: false),
                        TransDate = c.DateTime(),
                        VoucherId = c.Int(nullable: false),
                        BillNo = c.String(maxLength: 25),
                        VoucherNo = c.String(maxLength: 25),
                        AccountId = c.Int(),
                        ItemId = c.Int(nullable: false),
                        RcptNos = c.Int(nullable: false),
                        RcptQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IssueNos = c.Int(nullable: false),
                        IssueQty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AgentId = c.Int(nullable: false),
                        TableName = c.String(maxLength: 25),
                        KeyFldValue = c.Int(),
                        Narration = c.String(),
                        TransDateTime = c.DateTime(),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ColorId = c.Int(),
                        BatchId = c.Int(),
                        GradeId = c.Int(),
                        DesignId = c.Int(),
                        ChallanType = c.Int(),
                        LotNo = c.String(maxLength: 50),
                        Cut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pcs = c.Int(nullable: false),
                        UomId = c.Int(),
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
                "dbo.Store",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreName = c.String(nullable: false, maxLength: 50),
                        BranchId = c.Int(nullable: false),
                        Address = c.String(),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                .ForeignKey("dbo.Branch", t => t.BranchId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Style",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StyleCode = c.String(maxLength: 15),
                        StyleName = c.String(nullable: false, maxLength: 50),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.Template",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TemplateId = c.Int(),
                        VTypeId = c.Int(),
                        Descriptions = c.String(maxLength: 500),
                        VoucherId = c.Int(),
                        StartRowNo = c.Int(),
                        TempFieldId = c.Int(),
                        AccId = c.Int(),
                        ColNo = c.Int(),
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
                "dbo.PayTerms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PayDescr = c.String(maxLength: 100),
                        Days = c.Int(nullable: false),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.TransType",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TypeName = c.String(maxLength: 100),
                        Category = c.String(maxLength: 100),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.Uom",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UnitCode = c.String(nullable: false, maxLength: 3),
                        UnitName = c.String(nullable: false, maxLength: 50),
                        Nod = c.Int(nullable: false),
                        RateOn = c.String(nullable: false, maxLength: 1),
                        GSTUnit = c.String(nullable: false, maxLength: 50),
                        Remark = c.String(maxLength: 100),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.Voucher_Book",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherTypeId = c.Int(),
                        GroupId = c.Int(nullable: false),
                        Remark = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false,defaultValue:true),
                        IsDeleted = c.Boolean(nullable: false,defaultValue:false),
                        CreateDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcGroup", t => t.GroupId)
                .ForeignKey("dbo.VoucherType", t => t.VoucherTypeId)
                .Index(t => t.VoucherTypeId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Voucher_Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherTypeId = c.Int(),
                        PTypeId = c.Int(nullable: false),
                        Remark = c.String(maxLength: 50),
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
                .ForeignKey("dbo.ProductType", t => t.PTypeId)
                .ForeignKey("dbo.VoucherType", t => t.VoucherTypeId)
                .Index(t => t.VoucherTypeId)
                .Index(t => t.PTypeId);
            
            CreateTable(
                "dbo.Voucher_Party",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherTypeId = c.Int(),
                        GroupId = c.Int(nullable: false),
                        Remark = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false,defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false,defaultValue:false),
                        CreateDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcGroup", t => t.GroupId)
                .ForeignKey("dbo.VoucherType", t => t.VoucherTypeId)
                .Index(t => t.VoucherTypeId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.WarpItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false, maxLength: 50),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                "dbo.WeftItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Denier = c.Decimal(precision: 18, scale: 2),
                        VoucherDate = c.Int(),
                        VDate = c.DateTime(nullable: false),
                        Change = c.Decimal(precision: 18, scale: 2),
                        Qty = c.Decimal(precision: 18, scale: 2),
                        ColorId = c.Int(),
                        MColorId = c.Int(),
                        PI = c.Decimal(precision: 18, scale: 2),
                        RS = c.Decimal(precision: 18, scale: 2),
                        Ends = c.Decimal(precision: 18, scale: 2),
                        Mtr = c.Decimal(precision: 18, scale: 2),
                        Totcard = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotPick = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tar = c.Int(nullable: false),
                        Costing = c.Decimal(nullable: false, precision: 18, scale: 2),
                        JobCharge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Wasteper = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefId = c.Int(),
                        AccId = c.Int(),
                        ItemId = c.Int(),
                        Remark = c.String(),
                        IType = c.String(),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Card = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Panno = c.Int(nullable: false),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
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
                .ForeignKey("dbo.Product", t => t.RefId)
                .Index(t => t.RefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeftItem", "RefId", "dbo.Product");
            DropForeignKey("dbo.Voucher_Party", "VoucherTypeId", "dbo.VoucherType");
            DropForeignKey("dbo.Voucher_Party", "GroupId", "dbo.AcGroup");
            DropForeignKey("dbo.Voucher_Item", "VoucherTypeId", "dbo.VoucherType");
            DropForeignKey("dbo.Voucher_Item", "PTypeId", "dbo.ProductType");
            DropForeignKey("dbo.Voucher_Book", "VoucherTypeId", "dbo.VoucherType");
            DropForeignKey("dbo.Voucher_Book", "GroupId", "dbo.AcGroup");
            DropForeignKey("dbo.Product", "StyleModel_Id", "dbo.Style");
            DropForeignKey("dbo.Store", "BranchId", "dbo.Branch");
            DropForeignKey("dbo.ProductBal", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductBal", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Prod", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Prod", "ColorId", "dbo.Color");
            DropForeignKey("dbo.Prod", "TransId", "dbo.ChallanTrans");
            DropForeignKey("dbo.Prod", "RefId", "dbo.Challan");
            DropForeignKey("dbo.ProdOut", "VoucherId", "dbo.Voucher");
            DropForeignKey("dbo.ProdOut", "TransId", "dbo.ChallanTrans");
            DropForeignKey("dbo.ProdOut", "RefId", "dbo.Challan");
            DropForeignKey("dbo.Process", "TaxId", "dbo.TaxMaster");
            DropForeignKey("dbo.PSubGroup", "PGroupId", "dbo.PGroup");
            DropForeignKey("dbo.UserMaster", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Role_Permissions", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Role_Permissions", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.OrdTrans", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrdTrans", "OrdId", "dbo.Ord");
            DropForeignKey("dbo.Ord", "VoucherId", "dbo.Voucher");
            DropForeignKey("dbo.Ord", "EmpId", "dbo.Emp");
            DropForeignKey("dbo.Ord", "CompId", "dbo.Company");
            DropForeignKey("dbo.Ord", "AccId", "dbo.Acc");
            DropForeignKey("dbo.OrdDelv", "AccId", "dbo.Acc");
            DropForeignKey("dbo.JobReceipt", "ChallanId", "dbo.Challan");
            DropForeignKey("dbo.Haste", "CompId", "dbo.Company");
            DropForeignKey("dbo.CompPara", "ParaId", "dbo.SysPara");
            DropForeignKey("dbo.CompPara", "CompId", "dbo.Company");
            DropForeignKey("dbo.ChallanTrans", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "PTypeId", "dbo.ProductType");
            DropForeignKey("dbo.ProductPrice", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ChallanTrans", "ChallanId", "dbo.Challan");
            DropForeignKey("dbo.ChallanDelv", "AccId", "dbo.Acc");
            DropForeignKey("dbo.BillMain", "VoucherId", "dbo.Voucher");
            DropForeignKey("dbo.Voucher", "VTypeId", "dbo.VoucherType");
            DropForeignKey("dbo.VchSetup", "VoucherId", "dbo.Voucher");
            DropForeignKey("dbo.VchSetup", "CompId", "dbo.Company");
            DropForeignKey("dbo.BillDelv", "AccId", "dbo.Acc");
            DropForeignKey("dbo.AccOther", "TdsAccId", "dbo.Acc");
            DropForeignKey("dbo.AccOther", "TcsAccId", "dbo.Acc");
            DropForeignKey("dbo.AccOther", "OpStockAccId", "dbo.Acc");
            DropForeignKey("dbo.AccOther", "IntTdsAccId", "dbo.Acc");
            DropForeignKey("dbo.AccOther", "IntAccId", "dbo.Acc");
            DropForeignKey("dbo.AccOther", "DepAccId", "dbo.Acc");
            DropForeignKey("dbo.AccOther", "AccId", "dbo.Acc");
            DropForeignKey("dbo.AccBal", "YearId", "dbo.FinYear");
            DropForeignKey("dbo.AccBal", "CompId", "dbo.Company");
            DropForeignKey("dbo.AccBal", "GroupId", "dbo.AcGroup");
            DropForeignKey("dbo.AccBal", "AccId", "dbo.Acc");
            DropForeignKey("dbo.Route", "CityId", "dbo.City");
            DropForeignKey("dbo.Route", "AreaId", "dbo.Area");
            DropForeignKey("dbo.AccAddress", "RouteId", "dbo.Route");
            DropForeignKey("dbo.AccAddress", "CityId", "dbo.City");
            DropForeignKey("dbo.AccAddress", "AreaId", "dbo.Area");
            DropForeignKey("dbo.State", "CountryId", "dbo.Country");
            DropForeignKey("dbo.City", "StateId", "dbo.State");
            DropForeignKey("dbo.Area", "CityId", "dbo.City");
            DropForeignKey("dbo.Acc", "PGroupId", "dbo.PartyGroup");
            DropForeignKey("dbo.Acc", "NopId", "dbo.Nop");
            DropForeignKey("dbo.Acc", "EmpId", "dbo.Emp");
            DropForeignKey("dbo.Emp", "CompId", "dbo.Company");
            DropForeignKey("dbo.Division", "BranchId", "dbo.Branch");
            DropForeignKey("dbo.Branch", "CompId", "dbo.Company");
            DropForeignKey("dbo.Acc", "DeducteeId", "dbo.Deductee");
            DropForeignKey("dbo.Acc", "GroupId", "dbo.AcGroup");
            DropForeignKey("dbo.AccBank", "AccId", "dbo.Acc");
            DropForeignKey("dbo.AccAddress", "AccId", "dbo.Acc");
            DropIndex("dbo.WeftItem", new[] { "RefId" });
            DropIndex("dbo.Voucher_Party", new[] { "GroupId" });
            DropIndex("dbo.Voucher_Party", new[] { "VoucherTypeId" });
            DropIndex("dbo.Voucher_Item", new[] { "PTypeId" });
            DropIndex("dbo.Voucher_Item", new[] { "VoucherTypeId" });
            DropIndex("dbo.Voucher_Book", new[] { "GroupId" });
            DropIndex("dbo.Voucher_Book", new[] { "VoucherTypeId" });
            DropIndex("dbo.Store", new[] { "BranchId" });
            DropIndex("dbo.ProductBal", new[] { "ProductId" });
            DropIndex("dbo.ProductBal", new[] { "CompanyId" });
            DropIndex("dbo.Prod", new[] { "RefId" });
            DropIndex("dbo.Prod", new[] { "ColorId" });
            DropIndex("dbo.Prod", new[] { "ProductId" });
            DropIndex("dbo.Prod", new[] { "TransId" });
            DropIndex("dbo.ProdOut", new[] { "VoucherId" });
            DropIndex("dbo.ProdOut", new[] { "RefId" });
            DropIndex("dbo.ProdOut", new[] { "TransId" });
            DropIndex("dbo.Process", new[] { "TaxId" });
            DropIndex("dbo.PSubGroup", new[] { "PGroupId" });
            DropIndex("dbo.UserMaster", new[] { "RoleId" });
            DropIndex("dbo.Role_Permissions", new[] { "PermissionId" });
            DropIndex("dbo.Role_Permissions", new[] { "RoleId" });
            DropIndex("dbo.OrdTrans", new[] { "ProductId" });
            DropIndex("dbo.OrdTrans", new[] { "OrdId" });
            DropIndex("dbo.Ord", new[] { "EmpId" });
            DropIndex("dbo.Ord", new[] { "AccId" });
            DropIndex("dbo.Ord", new[] { "VoucherId" });
            DropIndex("dbo.Ord", new[] { "CompId" });
            DropIndex("dbo.OrdDelv", new[] { "AccId" });
            DropIndex("dbo.JobReceipt", new[] { "ChallanId" });
            DropIndex("dbo.Haste", new[] { "CompId" });
            DropIndex("dbo.CompPara", new[] { "ParaId" });
            DropIndex("dbo.CompPara", new[] { "CompId" });
            DropIndex("dbo.ProductPrice", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "StyleModel_Id" });
            DropIndex("dbo.Product", new[] { "PTypeId" });
            DropIndex("dbo.ChallanTrans", new[] { "ProductId" });
            DropIndex("dbo.ChallanTrans", new[] { "ChallanId" });
            DropIndex("dbo.ChallanDelv", new[] { "AccId" });
            DropIndex("dbo.VchSetup", new[] { "CompId" });
            DropIndex("dbo.VchSetup", new[] { "VoucherId" });
            DropIndex("dbo.Voucher", new[] { "VTypeId" });
            DropIndex("dbo.BillMain", new[] { "VoucherId" });
            DropIndex("dbo.BillDelv", new[] { "AccId" });
            DropIndex("dbo.AccOther", new[] { "DepAccId" });
            DropIndex("dbo.AccOther", new[] { "TdsAccId" });
            DropIndex("dbo.AccOther", new[] { "TcsAccId" });
            DropIndex("dbo.AccOther", new[] { "IntTdsAccId" });
            DropIndex("dbo.AccOther", new[] { "IntAccId" });
            DropIndex("dbo.AccOther", new[] { "OpStockAccId" });
            DropIndex("dbo.AccOther", new[] { "AccId" });
            DropIndex("dbo.AccBal", new[] { "YearId" });
            DropIndex("dbo.AccBal", new[] { "CompId" });
            DropIndex("dbo.AccBal", new[] { "GroupId" });
            DropIndex("dbo.AccBal", new[] { "AccId" });
            DropIndex("dbo.Route", new[] { "AreaId" });
            DropIndex("dbo.Route", new[] { "CityId" });
            DropIndex("dbo.State", new[] { "CountryId" });
            DropIndex("dbo.City", new[] { "StateId" });
            DropIndex("dbo.Area", new[] { "CityId" });
            DropIndex("dbo.Division", new[] { "BranchId" });
            DropIndex("dbo.Branch", new[] { "CompId" });
            DropIndex("dbo.Emp", new[] { "CompId" });
            DropIndex("dbo.AccBank", new[] { "AccId" });
            DropIndex("dbo.Acc", new[] { "EmpId" });
            DropIndex("dbo.Acc", new[] { "NopId" });
            DropIndex("dbo.Acc", new[] { "DeducteeId" });
            DropIndex("dbo.Acc", new[] { "PGroupId" });
            DropIndex("dbo.Acc", new[] { "GroupId" });
            DropIndex("dbo.AccAddress", new[] { "RouteId" });
            DropIndex("dbo.AccAddress", new[] { "AreaId" });
            DropIndex("dbo.AccAddress", new[] { "CityId" });
            DropIndex("dbo.AccAddress", new[] { "AccId" });
            DropTable("dbo.WeftItem");
            DropTable("dbo.WarpItem");
            DropTable("dbo.Voucher_Party");
            DropTable("dbo.Voucher_Item");
            DropTable("dbo.Voucher_Book");
            DropTable("dbo.Uom");
            DropTable("dbo.TransType");
            DropTable("dbo.PayTerms");
            DropTable("dbo.Template");
            DropTable("dbo.Style");
            DropTable("dbo.Store");
            DropTable("dbo.StockTrans");
            DropTable("dbo.ProductBal");
            DropTable("dbo.SpPara");
            DropTable("dbo.SPCollection");
            DropTable("dbo.PSize");
            DropTable("dbo.SerialNumbersShelf");
            DropTable("dbo.RecpaySetting");
            DropTable("dbo.ReportType");
            DropTable("dbo.ReportPara");
            DropTable("dbo.RefBank");
            DropTable("dbo.Prod");
            DropTable("dbo.ProdOut");
            DropTable("dbo.TaxMaster");
            DropTable("dbo.Process");
            DropTable("dbo.PImage");
            DropTable("dbo.PSubGroup");
            DropTable("dbo.PGroup");
            DropTable("dbo.PFormula");
            DropTable("dbo.UserMaster");
            DropTable("dbo.Roles");
            DropTable("dbo.Role_Permissions");
            DropTable("dbo.Permissions");
            DropTable("dbo.OrdTrans");
            DropTable("dbo.Ord");
            DropTable("dbo.OrdDelv");
            DropTable("dbo.Nob");
            DropTable("dbo.Menu_Package");
            DropTable("dbo.ListPage");
            DropTable("dbo.LedgerTrans");
            DropTable("dbo.JobReceipt");
            DropTable("dbo.Haste");
            DropTable("dbo.Grade");
            DropTable("dbo.ErpModule");
            DropTable("dbo.EmailSmsLog");
            DropTable("dbo.data_freeze");
            DropTable("dbo.DbVersion");
            DropTable("dbo.CustomRep");
            DropTable("dbo.SysPara");
            DropTable("dbo.CompPara");
            DropTable("dbo.ColorSet");
            DropTable("dbo.Color");
            DropTable("dbo.ProductType");
            DropTable("dbo.ProductPrice");
            DropTable("dbo.Product");
            DropTable("dbo.ChallanTrans");
            DropTable("dbo.Challan");
            DropTable("dbo.ChallanDelv");
            DropTable("dbo.PCategory");
            DropTable("dbo.Catalog");
            DropTable("dbo.BtoB");
            DropTable("dbo.Brand");
            DropTable("dbo.BillTrans");
            DropTable("dbo.VoucherType");
            DropTable("dbo.VchSetup");
            DropTable("dbo.Voucher");
            DropTable("dbo.BillMain");
            DropTable("dbo.BillRef");
            DropTable("dbo.BillDelv");
            DropTable("dbo.AuditLog");
            DropTable("dbo.Attachment");
            DropTable("dbo.AccOther");
            DropTable("dbo.FinYear");
            DropTable("dbo.AccBal");
            DropTable("dbo.Route");
            DropTable("dbo.Country");
            DropTable("dbo.State");
            DropTable("dbo.City");
            DropTable("dbo.Area");
            DropTable("dbo.PartyGroup");
            DropTable("dbo.Nop");
            DropTable("dbo.Division");
            DropTable("dbo.Branch");
            DropTable("dbo.Company");
            DropTable("dbo.Emp");
            DropTable("dbo.Deductee");
            DropTable("dbo.AcGroup");
            DropTable("dbo.AccBank");
            DropTable("dbo.Acc");
            DropTable("dbo.AccAddress");
        }
    }
}
