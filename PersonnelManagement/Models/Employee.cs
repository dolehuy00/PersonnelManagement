namespace PersonnelManagement.Model
{
    public class Employee
    {
        public long Id { get; set; }
        public required string Address { get; set; }
        public string? Image { get; set; }
        public double BasicSalary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Fullname { get; set; }
        public required string Position { get; set; }
        public DateTime StartDate { get; set; }
        public Account? Account { get; set; }
        public required string Status { get; set; }
        public long? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<SalaryHistory>? SalaryHistories { get; set; }
        public ICollection<Assignment>? Assignments { get; set; }
        public ICollection<Department>? LeaderOfDepartments { get; set; }
    }
}