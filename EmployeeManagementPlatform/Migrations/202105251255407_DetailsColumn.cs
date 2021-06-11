namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DetailsColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NDepartment", "Details", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NDepartment", "Details");
        }
    }
}
