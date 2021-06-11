namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NLogLevel",
                c => new
                    {
                        IdLogLevel = c.Int(nullable: false, identity: true),
                        LogName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IdLogLevel);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        IdLog = c.Int(nullable: false, identity: true),
                        LogTitle = c.String(nullable: false, maxLength: 500),
                        LogDescription = c.String(nullable: false, maxLength: 1000),
                        EventDate = c.DateTime(nullable: false),
                        IdLogLevel = c.Int(nullable: false),
                        IDEmployee = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdLog)
                .ForeignKey("dbo.Employees", t => t.IDEmployee, cascadeDelete: true)
                .ForeignKey("dbo.NLogLevel", t => t.IdLogLevel, cascadeDelete: true)
                .Index(t => t.IdLogLevel)
                .Index(t => t.IDEmployee);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "IdLogLevel", "dbo.NLogLevel");
            DropForeignKey("dbo.Logs", "IDEmployee", "dbo.Employees");
            DropIndex("dbo.Logs", new[] { "IDEmployee" });
            DropIndex("dbo.Logs", new[] { "IdLogLevel" });
            DropTable("dbo.Logs");
            DropTable("dbo.NLogLevel");
        }
    }
}
