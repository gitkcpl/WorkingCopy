namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_customer_master : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 75),
                        Address = c.String(maxLength: 200),
                        AreaId = c.Int(),
                        CityId = c.Int(),
                        MobileNo = c.String(maxLength: 10),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Dob = c.DateTime(precision: 7, storeType: "datetime2"),
                        AnniDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        GstNo = c.String(maxLength: 20),
                        MemberNo = c.String(maxLength: 25),
                        MemberDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        OpBillAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpPoint = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpPointUsed = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                .ForeignKey("dbo.Area", t => t.AreaId)
                .ForeignKey("dbo.City", t => t.CityId)
                .Index(t => t.AreaId)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.customers", "CityId", "dbo.City");
            DropForeignKey("dbo.customers", "AreaId", "dbo.Area");
            DropIndex("dbo.customers", new[] { "CityId" });
            DropIndex("dbo.customers", new[] { "AreaId" });
            DropTable("dbo.customers");
        }
    }
}
