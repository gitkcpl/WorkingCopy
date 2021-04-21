namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_Refcode_billmain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillMain", "RefCode", c => c.Guid(nullable: false));
            AddColumn("dbo.BillTrans", "RefCode", c => c.Guid(nullable: false));
            AlterColumn("dbo.LedgerTrans", "Remark", c => c.String());
            CreateIndex("dbo.BillMain", "VoucherDate");
            CreateIndex("dbo.BillMain", "BookAcId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BillMain", new[] { "BookAcId" });
            DropIndex("dbo.BillMain", new[] { "VoucherDate" });
            AlterColumn("dbo.LedgerTrans", "Remark", c => c.String(maxLength: 2000));
            DropColumn("dbo.BillTrans", "RefCode");
            DropColumn("dbo.BillMain", "RefCode");
        }
    }
}
