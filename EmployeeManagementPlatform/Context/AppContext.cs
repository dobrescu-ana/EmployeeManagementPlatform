using EmployeeManagementPlatform.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EmployeeManagementPlatform.Context
{
    public class AppContext : DbContext
    {

        public AppContext() : base("name=AppConnection")
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Salary> Salaries { get; set; }

        public DbSet<Leave> Leaves { get; set; }

        public DbSet<LeaveCategory> LeaveCategories { get; set; }

        public System.Data.Entity.DbSet<EmployeeManagementPlatform.Models.Projects> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<LogLevel> LogLevels { get; set; }

        public DbSet<Logs> Logs { get; set; }
        public DbSet<Question> Questions { get; set; }

    }
}