namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cost_head : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cost_heads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HeadName = c.String(maxLength: 50),
                        BranchId = c.Int(),
                        Remark = c.String(maxLength: 200),
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
                .ForeignKey("dbo.Branch", t => t.BranchId)
                .Index(t => t.BranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.cost_heads", "BranchId", "dbo.Branch");
            DropIndex("dbo.cost_heads", new[] { "BranchId" });
            DropTable("dbo.cost_heads");
        }
    }
}
