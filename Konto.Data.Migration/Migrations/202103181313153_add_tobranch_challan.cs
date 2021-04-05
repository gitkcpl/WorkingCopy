namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tobranch_challan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Challan", "ToBranchId", c => c.Int());
            CreateIndex("dbo.Challan", "VoucherId");
            CreateIndex("dbo.Challan", "BranchId");
            CreateIndex("dbo.Challan", "ToBranchId");
           
            AddForeignKey("dbo.Challan", "BranchId", "dbo.Branch", "Id");
            AddForeignKey("dbo.Challan", "ToBranchId", "dbo.Branch", "Id");
            AddForeignKey("dbo.Challan", "VoucherId", "dbo.Voucher", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Challan", "VoucherId", "dbo.Voucher");
            DropForeignKey("dbo.Challan", "ToBranchId", "dbo.Branch");
            DropForeignKey("dbo.Challan", "BranchId", "dbo.Branch");
           
            DropIndex("dbo.Challan", new[] { "ToBranchId" });
            DropIndex("dbo.Challan", new[] { "BranchId" });
            DropIndex("dbo.Challan", new[] { "VoucherId" });
            
        }
    }
}
