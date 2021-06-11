namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateLog : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logs", "IdLogLevel", "dbo.NLogLevel");
            DropIndex("dbo.Logs", new[] { "IdLogLevel" });
            AddColumn("dbo.Logs", "LogLevel", c => c.String());
            DropColumn("dbo.Logs", "IdLogLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "IdLogLevel", c => c.Int(nullable: false));
            DropColumn("dbo.Logs", "LogLevel");
            CreateIndex("dbo.Logs", "IdLogLevel");
            AddForeignKey("dbo.Logs", "IdLogLevel", "dbo.NLogLevel", "IdLogLevel", cascadeDelete: true);
        }
    }
}
