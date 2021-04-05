namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gate_entry : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ord", new[] { "AccId" });
            AlterColumn("dbo.Ord", "AccId", c => c.Int());
            CreateIndex("dbo.Ord", "AccId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ord", new[] { "AccId" });
            AlterColumn("dbo.Ord", "AccId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ord", "AccId");
        }
    }
}
