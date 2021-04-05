namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_column_jobcard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobCard", "TolPer", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.JobCardTrans", "TransType", c => c.Int(nullable: false));
            CreateIndex("dbo.JobCardTrans", "RefId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.JobCardTrans", new[] { "RefId" });
            DropColumn("dbo.JobCardTrans", "TransType");
            DropColumn("dbo.JobCard", "TolPer");
        }
    }
}
