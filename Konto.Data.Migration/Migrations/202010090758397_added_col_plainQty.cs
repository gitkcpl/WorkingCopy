namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_col_plainQty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChallanTrans", "PlainPcs", c => c.Int(nullable: false));
            AddColumn("dbo.ChallanTrans", "PlainQty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProdOut", "PlainQty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProdOut", "PlainQty");
            DropColumn("dbo.ChallanTrans", "PlainQty");
            DropColumn("dbo.ChallanTrans", "PlainPcs");
        }
    }
}
