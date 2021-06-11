namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployee2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "IDQuestion", "dbo.NQuestion");
            DropIndex("dbo.Employees", new[] { "IDQuestion" });
            AlterColumn("dbo.Employees", "IDQuestion", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "Response", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Employees", "IDQuestion");
            AddForeignKey("dbo.Employees", "IDQuestion", "dbo.NQuestion", "IDQuestion", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "IDQuestion", "dbo.NQuestion");
            DropIndex("dbo.Employees", new[] { "IDQuestion" });
            AlterColumn("dbo.Employees", "Response", c => c.String(maxLength: 50));
            AlterColumn("dbo.Employees", "IDQuestion", c => c.Int());
            CreateIndex("dbo.Employees", "IDQuestion");
            AddForeignKey("dbo.Employees", "IDQuestion", "dbo.NQuestion", "IDQuestion");
        }
    }
}
