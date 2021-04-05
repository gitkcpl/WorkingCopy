namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class item_serials_new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.item_serials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SerialNo = c.String(maxLength: 50),
                        Extra1 = c.String(maxLength: 50),
                        Extra2 = c.String(maxLength: 50),
                        BranchId = c.Int(),
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
                .Index(t => new { t.ProductId, t.SerialNo }, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.item_serials", "ProductId", "dbo.Product");
            DropIndex("dbo.item_serials", new[] { "ProductId", "SerialNo" });
            DropTable("dbo.item_serials");
        }
    }
}
