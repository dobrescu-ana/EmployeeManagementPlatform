namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "IDEmployee", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "IDEmployee");
            AddForeignKey("dbo.Projects", "IDEmployee", "dbo.Employees", "IDEmployee", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "IDEmployee", "dbo.Employees");
            DropIndex("dbo.Projects", new[] { "IDEmployee" });
            DropColumn("dbo.Projects", "IDEmployee");
        }
    }
}
