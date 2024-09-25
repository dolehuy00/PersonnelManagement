namespace PersonnelManagement.Model
{
    public class DepartmentStatus
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Department>? Departments { get; set; }
    }
}