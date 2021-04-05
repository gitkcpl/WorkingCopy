namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_col_tdsacid_bill_trans : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BillTrans", "TdsAcId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BillTrans", "TdsAcId");
        }
    }
}
