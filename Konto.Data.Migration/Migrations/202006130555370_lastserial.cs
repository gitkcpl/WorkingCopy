namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastserial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LastSerial",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchId = c.Int(nullable: false),
                        BranchCode = c.String(maxLength: 50),
                        VoucherId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        CompId = c.Int(nullable: false),
                        Last_Serial = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LastSerial");
        }
    }
}
