using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Model;

namespace PersonnelManagement.Data
{
    public class PersonnelDataContext : DbContext
    {
        public PersonnelDataContext(DbContextOptions<PersonnelDataContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SalaryHistory> SalaryHistories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DeptAssignment> DeptAssignments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalaryHistory>()
                .HasOne(sh => sh.Employee)
                .WithMany(em => em.SalaryHistories)
                .HasForeignKey(sh => sh.EmployeeId);

            modelBuilder.Entity<Account>()
                .HasOne(acc => acc.Role)
                .WithMany(role => role.Accounts)
                .HasForeignKey(acc => acc.RoleId);

            modelBuilder.Entity<Account>()
                .HasOne(acc => acc.Employee)
                .WithOne(em => em.Account)
                .HasForeignKey<Account>(acc => acc.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(em => em.Assignments)
                .WithOne(asm => asm.ResponsiblePeson)
                .HasForeignKey(asm => asm.ResponsiblePesonId);

            modelBuilder.Entity<Employee>()
               .HasOne(em => em.Department)
               .WithMany(dept => dept.Employees)
               .HasForeignKey(dept => dept.DepartmentId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Assignment>()
                .HasOne(asm => asm.DeptAssignment)
                .WithMany(stt => stt.Assignments)
                .HasForeignKey(asm => asm.DeptAssignmentId);

            modelBuilder.Entity<Department>()
               .HasOne(t => t.Leader)
               .WithMany(em => em.LeaderOfDepartments)
               .HasForeignKey(t => t.LeaderId)
               .OnDelete(DeleteBehavior.SetNull)
               .IsRequired(false);


            modelBuilder.Entity<Project>()
                .HasMany(p => p.DeptAssignments)
                .WithOne(da => da.Project)
                .HasForeignKey(da => da.ProjectId);

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
