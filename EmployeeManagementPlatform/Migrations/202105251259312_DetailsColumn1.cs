namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DetailsColumn1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NDepartment", "Details", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NDepartment", "Details", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
