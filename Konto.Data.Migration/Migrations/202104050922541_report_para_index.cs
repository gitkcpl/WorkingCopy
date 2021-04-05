namespace Konto.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class report_para_index : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReportPara", "ParameterName", c => c.String(maxLength: 50));
            CreateIndex("dbo.ReportPara", "ReportId");
            CreateIndex("dbo.ReportPara", "ParameterName");
            CreateIndex("dbo.ReportPara", "ParameterValue");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ReportPara", new[] { "ParameterValue" });
            DropIndex("dbo.ReportPara", new[] { "ParameterName" });
            DropIndex("dbo.ReportPara", new[] { "ReportId" });
            AlterColumn("dbo.ReportPara", "ParameterName", c => c.String());
        }
    }
}
