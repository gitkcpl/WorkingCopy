namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_tcs_challan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Challan", "TcsPer", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Challan", "TcsAmt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Challan", "TcsAmt");
            DropColumn("dbo.Challan", "TcsPer");
        }
    }
}
