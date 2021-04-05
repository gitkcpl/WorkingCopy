namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_prevyearid_finyear : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FinYear", "PrevYearId", c => c.Int(nullable: false,defaultValue:0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FinYear", "PrevYearId");
        }
    }
}
