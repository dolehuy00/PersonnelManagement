namespace PersonnelManagement.DTO
{
    public class EmployeeFilterDTO
    {
        public string? NameOrId { get; set; }
        public string? Address { get; set; }
        public DateTime? FromDoB { get; set; }
        public DateTime? ToDoB { get; set; }
        public double? FromSalary { get; set; }
        public double? ToSalary { get; set; }
        public string? Position { get; set; }
        public DateTime? FromStartDate { get; set; }
        public DateTime? ToStartDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? StatusId { get; set; }
        public string? SortBy { get; set; }
        public required int Page { get; set; }
        public required int PageSize { get; set; }
    }
}
