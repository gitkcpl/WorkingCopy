namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class template : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TempField",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldName = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TemplateTrans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TemplateId = c.Int(),
                        TempFieldId = c.Int(),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Template", t => t.TemplateId)
                .Index(t => t.TemplateId);
            
            DropColumn("dbo.Template", "TemplateId");
            DropColumn("dbo.Template", "TempFieldId");
            DropColumn("dbo.Template", "ColNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Template", "ColNo", c => c.Int());
            AddColumn("dbo.Template", "TempFieldId", c => c.Int());
            AddColumn("dbo.Template", "TemplateId", c => c.Int());
            DropForeignKey("dbo.TemplateTrans", "TemplateId", "dbo.Template");
            DropIndex("dbo.TemplateTrans", new[] { "TemplateId" });
            DropTable("dbo.TemplateTrans");
            DropTable("dbo.TempField");
        }
    }
}
