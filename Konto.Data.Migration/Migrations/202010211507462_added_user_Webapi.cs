namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_user_Webapi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefreshToken",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        Expires = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedByIp = c.String(),
                        Revoked = c.DateTime(precision: 7, storeType: "datetime2"),
                        RevokedByIp = c.String(),
                        ReplacedByToken = c.String(),
                        UserMasterModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserMaster", t => t.UserMasterModel_Id)
                .Index(t => t.UserMasterModel_Id);
            
            AddColumn("dbo.UserMaster", "BranchId", c => c.Int());
            AddColumn("dbo.UserMaster", "Email", c => c.String());
            AddColumn("dbo.UserMaster", "PasswordHash", c => c.String());
            AddColumn("dbo.UserMaster", "AcceptTerms", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserMaster", "VerificationToken", c => c.String());
            AddColumn("dbo.UserMaster", "Verified", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.UserMaster", "ResetToken", c => c.String());
            AddColumn("dbo.UserMaster", "ResetTokenExpires", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.UserMaster", "PasswordReset", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.UserMaster", "BranchId");
            AddForeignKey("dbo.UserMaster", "BranchId", "dbo.Branch", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefreshToken", "UserMasterModel_Id", "dbo.UserMaster");
            DropForeignKey("dbo.UserMaster", "BranchId", "dbo.Branch");
            DropIndex("dbo.RefreshToken", new[] { "UserMasterModel_Id" });
            DropIndex("dbo.UserMaster", new[] { "BranchId" });
            DropColumn("dbo.UserMaster", "PasswordReset");
            DropColumn("dbo.UserMaster", "ResetTokenExpires");
            DropColumn("dbo.UserMaster", "ResetToken");
            DropColumn("dbo.UserMaster", "Verified");
            DropColumn("dbo.UserMaster", "VerificationToken");
            DropColumn("dbo.UserMaster", "AcceptTerms");
            DropColumn("dbo.UserMaster", "PasswordHash");
            DropColumn("dbo.UserMaster", "Email");
            DropColumn("dbo.UserMaster", "BranchId");
            DropTable("dbo.RefreshToken");
        }
    }
}
