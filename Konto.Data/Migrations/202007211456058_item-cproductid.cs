namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemcproductid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prod", "CProductId", c => c.Int());
            CreateIndex("dbo.Prod", "CProductId");
            AddForeignKey("dbo.Prod", "CProductId", "dbo.Product", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prod", "CProductId", "dbo.Product");
            DropIndex("dbo.Prod", new[] { "CProductId" });
            DropColumn("dbo.Prod", "CProductId");
        }
    }
}
