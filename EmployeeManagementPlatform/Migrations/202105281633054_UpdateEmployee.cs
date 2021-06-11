namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Password", c => c.String(maxLength: 100));
            AddColumn("dbo.Employees", "AccountStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "AccountStatus");
            DropColumn("dbo.Employees", "Password");
        }
    }
}
