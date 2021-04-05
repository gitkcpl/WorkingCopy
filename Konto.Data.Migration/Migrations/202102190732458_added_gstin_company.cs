namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_gstin_company : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Company", "GstInUserId", c => c.String(maxLength: 50));
            AddColumn("dbo.Company", "EwayBillUserId", c => c.String(maxLength: 50));
            AddColumn("dbo.Company", "EwayBillPassword", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Company", "EwayBillPassword");
            DropColumn("dbo.Company", "EwayBillUserId");
            DropColumn("dbo.Company", "GstInUserId");
        }
    }
}
