namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class weave_update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.emp_rates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmpId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
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
                .ForeignKey("dbo.Emp", t => t.EmpId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.EmpId)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Division", "IsQc", c => c.Boolean(nullable: false));
            AddColumn("dbo.Division", "IsQcOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.Division", "IsOutward", c => c.Boolean(nullable: false));
            AddColumn("dbo.Division", "IsFinishWareHouse", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.emp_rates", "ProductId", "dbo.Product");
            DropForeignKey("dbo.emp_rates", "EmpId", "dbo.Emp");
            DropIndex("dbo.emp_rates", new[] { "ProductId" });
            DropIndex("dbo.emp_rates", new[] { "EmpId" });
            DropColumn("dbo.Division", "IsFinishWareHouse");
            DropColumn("dbo.Division", "IsOutward");
            DropColumn("dbo.Division", "IsQcOut");
            DropColumn("dbo.Division", "IsQc");
            DropTable("dbo.emp_rates");
        }
    }
}
