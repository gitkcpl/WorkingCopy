namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taka_beam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TakaBeam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BeamId = c.Int(),
                        ProdId = c.Int(),
                        Per = c.Decimal(precision: 18, scale: 2),
                        Qty = c.Decimal(precision: 18, scale: 2),
                        Mtr = c.Decimal(precision: 18, scale: 2),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                        ModifyDate = c.DateTime(),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TakaBeam");
        }
    }
}
