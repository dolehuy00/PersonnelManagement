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
        public DbSet<DepartmentStatus> DepartmentStatuses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DeptAssignment> DeptAssignments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalaryHistory>()
                .HasOne(sh => sh.Status)
                .WithMany(stt => stt.SalaryHistories)
                .HasForeignKey(sh => sh.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalaryHistory>()
                .HasOne(sh => sh.Employee)
                .WithMany(em => em.SalaryHistories)
                .HasForeignKey(sh => sh.EmployeeId);

            modelBuilder.Entity<Account>()
                .HasOne(acc => acc.Role)
                .WithMany(role => role.Accounts)
                .HasForeignKey(acc => acc.RoleId);

            modelBuilder.Entity<Account>()
                .HasOne(acc => acc.Status)
                .WithMany(stt => stt.Accounts)
                .HasForeignKey(acc => acc.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(emp => emp.Account)
                .WithOne(acc => acc.Employee)
                .HasForeignKey<Account>(acc => acc.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(em => em.Assignments)
                .WithOne(asm => asm.ResponsiblePeson)
                .HasForeignKey(asm => asm.ResponsiblePesonId);

            modelBuilder.Entity<Employee>()
                .HasOne(emp => emp.Status)
                .WithMany(stt => stt.Employees)
                .HasForeignKey(acc => acc.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
               .HasOne(em => em.Team)
               .WithMany(t => t.Employees)
               .HasForeignKey(t => t.TeamId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Assignment>()
                .HasOne(asm => asm.Status)
                .WithMany(stt => stt.Assignments)
                .HasForeignKey(asm => asm.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(asm => asm.DeptAssignment)
                .WithMany(stt => stt.Assignments)
                .HasForeignKey(asm => asm.DeptAssignmentId);

            modelBuilder.Entity<Team>()
               .HasOne(t => t.Leader)
               .WithMany(em => em.LeaderOfTeams)
               .HasForeignKey(t => t.LeaderId)
               .OnDelete(DeleteBehavior.SetNull)
               .IsRequired(false);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Status)
                .WithMany(stt => stt.Teams)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectTeamDetail>()
                .HasKey(ptd => new { ptd.TeamId, ptd.ProjectId });
            modelBuilder.Entity<ProjectTeamDetail>()
                .HasOne(ptd => ptd.Team)
                .WithMany(t => t.ProjectTeamDetails)
                .HasForeignKey(ptd => ptd.TeamId);
            modelBuilder.Entity<ProjectTeamDetail>()
                .HasOne(ptd => ptd.Project)
                .WithMany(p => p.ProjectTeamDetails)
                .HasForeignKey(ptd => ptd.ProjectId);

            modelBuilder.Entity<Project>()
               .HasOne(p => p.Status)
               .WithMany(stt => stt.Projects)
               .HasForeignKey(t => t.StatusId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.DeptAssignments)
                .WithOne(da => da.Project)
                .HasForeignKey(da => da.ProjectId);

            modelBuilder.Entity<Department>()
               .HasOne(d => d.Status)
               .WithMany(stt => stt.Departments)
               .HasForeignKey(t => t.StatusId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.DeptAssignments)
                .WithOne(da => da.Department)
                .HasForeignKey(da => da.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Leader)
                .WithMany(em => em.LeaderOfDepartments)
                .HasForeignKey(d => d.LeaderId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
