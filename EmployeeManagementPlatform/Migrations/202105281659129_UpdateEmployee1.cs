namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployee1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NQuestion",
                c => new
                    {
                        IDQuestion = c.Int(nullable: false, identity: true),
                        NameQuestion = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IDQuestion);
            
            AddColumn("dbo.Employees", "IDQuestion", c => c.Int());
            AddColumn("dbo.Employees", "Response", c => c.String(maxLength: 50));
            CreateIndex("dbo.Employees", "IDQuestion");
            AddForeignKey("dbo.Employees", "IDQuestion", "dbo.NQuestion", "IDQuestion");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "IDQuestion", "dbo.NQuestion");
            DropIndex("dbo.Employees", new[] { "IDQuestion" });
            DropColumn("dbo.Employees", "Response");
            DropColumn("dbo.Employees", "IDQuestion");
            DropTable("dbo.NQuestion");
        }
    }
}
