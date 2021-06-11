namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedLogs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Logs", "IDEmployee", "dbo.Employees");
            DropIndex("dbo.Logs", new[] { "IDEmployee" });
            AddColumn("dbo.Logs", "Author", c => c.String());
            DropColumn("dbo.Logs", "IDEmployee");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "IDEmployee", c => c.Int(nullable: false));
            DropColumn("dbo.Logs", "Author");
            CreateIndex("dbo.Logs", "IDEmployee");
            AddForeignKey("dbo.Logs", "IDEmployee", "dbo.Employees", "IDEmployee", cascadeDelete: true);
        }
    }
}
