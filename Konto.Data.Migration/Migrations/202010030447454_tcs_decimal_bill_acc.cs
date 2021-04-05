namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tcs_decimal_bill_acc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccOther", "TcsPer", c => c.Decimal(precision: 18, scale: 3));
            AlterColumn("dbo.BillMain", "TcsPer", c => c.Decimal(nullable: false, precision: 18, scale: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BillMain", "TcsPer", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AccOther", "TcsPer", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
