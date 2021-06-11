namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskTableAdd1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        IDTask = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 1500),
                        Deadline = c.DateTime(nullable: false),
                        IDEmployee = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IDTask)
                .ForeignKey("dbo.Employees", t => t.IDEmployee, cascadeDelete: true)
                .Index(t => t.IDEmployee);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "IDEmployee", "dbo.Employees");
            DropIndex("dbo.Tasks", new[] { "IDEmployee" });
            DropTable("dbo.Tasks");
        }
    }
}
