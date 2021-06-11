namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DestionationColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "Destination", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leaves", "Destination");
        }
    }
}
