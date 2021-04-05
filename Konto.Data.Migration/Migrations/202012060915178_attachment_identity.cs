namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attachment_identity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Attachment");
            DropColumn("dbo.Attachment", "id");
            AddColumn("dbo.Attachment", "id",c=>c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Attachment", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Attachment", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Attachment");
            AlterColumn("dbo.Attachment", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Attachment", "Id");
        }
    }
}
