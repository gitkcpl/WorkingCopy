namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prod_out_lotNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProdOut", "LotNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProdOut", "LotNo");
        }
    }
}
