namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class index_on_BillRef : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.BillRef", "VoucherDate");
            CreateIndex("dbo.BillRef", "AccountId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BillRef", new[] { "AccountId" });
            DropIndex("dbo.BillRef", new[] { "VoucherDate" });
        }
    }
}
