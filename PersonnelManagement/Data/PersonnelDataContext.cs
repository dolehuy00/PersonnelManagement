using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Model;

namespace PersonnelManagement.Data
{
    public class PersonnelDataContext : DbContext
    {
        public PersonnelDataContext(DbContextOptions<PersonnelDataContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentStatus> AssignmentStatuses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeStatus> EmployeeStatuses { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<ProjectTeamDetail> ProjectTeamDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SalaryHistory> SalaryHistories { get; set; }
        public DbSet<SalaryHistoryStatus> SalaryHistoryStatuses { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamStatus> TeamStatuses { get; set; }
    }
}
