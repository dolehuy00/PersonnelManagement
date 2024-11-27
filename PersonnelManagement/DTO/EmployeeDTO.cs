namespace PersonnelManagement.DTO
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        public string? Address { get; set; }
        public double BasicSalary { get; set; }
        public string? Image { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Fullname { get; set; }
        public string? Position { get; set; }
        public DateTime StartDate { get; set; }
        public long? AccountId { get; set; }
        public string? Status { get; set; }
        public long? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
    }
}
