namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refresh_token : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RefreshToken", new[] { "UserMasterModel_Id" });
            RenameColumn(table: "dbo.RefreshToken", name: "UserMasterModel_Id", newName: "UserId");
            AlterColumn("dbo.RefreshToken", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.RefreshToken", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RefreshToken", new[] { "UserId" });
            AlterColumn("dbo.RefreshToken", "UserId", c => c.Int());
            RenameColumn(table: "dbo.RefreshToken", name: "UserId", newName: "UserMasterModel_Id");
            CreateIndex("dbo.RefreshToken", "UserMasterModel_Id");
        }
    }
}
