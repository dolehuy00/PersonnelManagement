namespace PersonnelManagement.Model
{
    public class EmployeeStatus
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}