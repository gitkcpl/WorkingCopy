namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_series_vouchersetup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VchSetup", "ManualSeries", c => c.Boolean(nullable: false,defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VchSetup", "ManualSeries");
        }
    }
}
