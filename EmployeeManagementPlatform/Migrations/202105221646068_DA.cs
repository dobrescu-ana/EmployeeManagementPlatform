namespace EmployeeManagementPlatform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DA : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NDepartment",
                c => new
                    {
                        IdDepartment = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IdDepartment);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        IDEmployee = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false),
                        IntRight = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        DateOfJoin = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 500),
                        PhoneNo = c.String(nullable: false, maxLength: 20),
                        Gender = c.Boolean(nullable: false),
                        IdDepartment = c.Int(nullable: false),
                        Function = c.String(nullable: false),
                        Skills = c.String(maxLength: 500),
                        Education = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.IDEmployee)
                .ForeignKey("dbo.NDepartment", t => t.IdDepartment, cascadeDelete: true)
                .Index(t => t.IdDepartment);
            
            CreateTable(
                "dbo.NLeave",
                c => new
                    {
                        IDTypeLeave = c.Int(nullable: false, identity: true),
                        NameTypeLeave = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IDTypeLeave);
            
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        IdLeaves = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Reason = c.String(nullable: false, maxLength: 1000),
                        Status = c.String(maxLength: 20),
                        IDEmployee = c.Int(nullable: false),
                        IDTypeLeave = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdLeaves)
                .ForeignKey("dbo.Employees", t => t.IDEmployee, cascadeDelete: true)
                .ForeignKey("dbo.NLeave", t => t.IDTypeLeave, cascadeDelete: true)
                .Index(t => t.IDEmployee)
                .Index(t => t.IDTypeLeave);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        IDProject = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 1000),
                        Deadline = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IDProject);
            
            CreateTable(
                "dbo.Wages",
                c => new
                    {
                        IdSalary = c.Int(nullable: false, identity: true),
                        EmploymentType = c.String(nullable: false, maxLength: 20),
                        BasicSalary = c.Single(nullable: false),
                        MedicalAllowance = c.Single(nullable: false),
                        SpecialAllowance = c.Single(nullable: false),
                        FuelAllowance = c.Single(nullable: false),
                        ProvidedFund = c.Single(nullable: false),
                        TaxDeduction = c.Single(nullable: false),
                        OtherDeduction = c.Single(nullable: false),
                        TotalDeduction = c.Single(nullable: false),
                        GrossSalary = c.Single(nullable: false),
                        NetSalary = c.Single(nullable: false),
                        IDEmployee = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSalary);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "IDTypeLeave", "dbo.NLeave");
            DropForeignKey("dbo.Leaves", "IDEmployee", "dbo.Employees");
            DropForeignKey("dbo.Employees", "IdDepartment", "dbo.NDepartment");
            DropIndex("dbo.Leaves", new[] { "IDTypeLeave" });
            DropIndex("dbo.Leaves", new[] { "IDEmployee" });
            DropIndex("dbo.Employees", new[] { "IdDepartment" });
            DropTable("dbo.Wages");
            DropTable("dbo.Projects");
            DropTable("dbo.Leaves");
            DropTable("dbo.NLeave");
            DropTable("dbo.Employees");
            DropTable("dbo.NDepartment");
        }
    }
}
