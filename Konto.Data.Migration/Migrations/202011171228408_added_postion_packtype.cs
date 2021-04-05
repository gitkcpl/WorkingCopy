namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_postion_packtype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PackingType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false, maxLength: 50),
                        Remark = c.String(),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PositionName = c.String(maxLength: 50),
                        Remark = c.String(maxLength: 500),
                        Extra1 = c.String(maxLength: 100),
                        Extra2 = c.String(maxLength: 50),
                        RowId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ModifyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreateUser = c.String(maxLength: 50),
                        ModifyUser = c.String(maxLength: 50),
                        IpAddress = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.JobCardTrans", "RefTransId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobCardTrans", "RefTransId");
            DropTable("dbo.Position");
            DropTable("dbo.PackingType");
        }
    }
}
