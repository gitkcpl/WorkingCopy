namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class job_receipt : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.JobReceipt");
            AlterColumn("dbo.JobReceipt", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.JobReceipt", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.JobReceipt");
            AlterColumn("dbo.JobReceipt", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.JobReceipt", "Id");
        }
    }
}
