namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_refid_serials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.item_serials", "RefId", c => c.Int(nullable: false));
            AddColumn("dbo.item_serials", "RefTransId", c => c.Int(nullable: false));
            AddColumn("dbo.item_serials", "RefVoucherId", c => c.Int(nullable: false));
            AddColumn("dbo.item_serials", "BatchId", c => c.Int(nullable: false));
            CreateIndex("dbo.item_serials", "RefId");
            CreateIndex("dbo.item_serials", "RefTransId");
            CreateIndex("dbo.item_serials", "RefVoucherId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.item_serials", new[] { "RefVoucherId" });
            DropIndex("dbo.item_serials", new[] { "RefTransId" });
            DropIndex("dbo.item_serials", new[] { "RefId" });
            DropColumn("dbo.item_serials", "BatchId");
            DropColumn("dbo.item_serials", "RefVoucherId");
            DropColumn("dbo.item_serials", "RefTransId");
            DropColumn("dbo.item_serials", "RefId");
        }
    }
}
