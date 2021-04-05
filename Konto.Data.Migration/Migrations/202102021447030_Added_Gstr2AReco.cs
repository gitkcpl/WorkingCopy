namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Gstr2AReco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.gstr2a_dump",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Billid = c.Int(nullable: false),
                        AccId = c.Int(nullable: false),
                        GstIn = c.String(maxLength: 20),
                        InvoiceNo = c.String(maxLength: 50),
                        Pos = c.String(maxLength: 50),
                        InvoiceDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        InvoiceValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FilePeriod = c.String(maxLength: 50),
                        FileDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Cgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Igst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cess = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Taxable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransType = c.String(),
                        FPrd = c.String(),
                        CompId = c.Int(nullable: false),
                        YearId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Billid);
            
            CreateTable(
                "dbo.gstr2a_trans_dump",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainId = c.Int(nullable: false),
                        Cgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sgst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Igst = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cess = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Taxable = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.gstr2a_dump", t => t.MainId,cascadeDelete: true)
                .Index(t => t.MainId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.gstr2a_trans_dump", "MainId", "dbo.gstr2a_dump");
            DropIndex("dbo.gstr2a_trans_dump", new[] { "MainId" });
            DropIndex("dbo.gstr2a_dump", new[] { "Billid" });
            DropTable("dbo.gstr2a_trans_dump");
            DropTable("dbo.gstr2a_dump");
        }
    }
}
