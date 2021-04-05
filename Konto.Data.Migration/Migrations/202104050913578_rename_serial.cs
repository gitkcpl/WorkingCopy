namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename_serial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.serial_batches", newName: "item_batches");
            DropIndex("dbo.item_batches", new[] { "ProductId" });
            AddColumn("dbo.item_batches", "AvalQty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.item_batches", "BulkRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.item_batches", "SemiBulkRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ReportPara", "ParameterName", c => c.String());
            CreateIndex("dbo.item_batches", new[] { "ProductId", "BatchNo" }, unique: true);
            CreateIndex("dbo.item_batches", "SerialNo");
        }
        
        public override void Down()
        {
            DropIndex("dbo.item_batches", new[] { "SerialNo" });
            DropIndex("dbo.item_batches", new[] { "ProductId", "BatchNo" });
            AlterColumn("dbo.ReportPara", "ParameterName", c => c.String(maxLength: 50));
            DropColumn("dbo.item_batches", "SemiBulkRate");
            DropColumn("dbo.item_batches", "BulkRate");
            DropColumn("dbo.item_batches", "AvalQty");
            CreateIndex("dbo.item_batches", "ProductId");
            RenameTable(name: "dbo.item_batches", newName: "serial_batches");
        }
    }
}
