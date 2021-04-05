namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter_prod_emp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prod_Emp", "NightMtrs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prod_Emp", "DayMtrs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prod_Emp", "TotalMtrs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prod_Emp", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Prod_Emp", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prod_Emp", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Prod_Emp", "Rate", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Prod_Emp", "TotalMtrs", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Prod_Emp", "DayMtrs", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Prod_Emp", "NightMtrs", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
