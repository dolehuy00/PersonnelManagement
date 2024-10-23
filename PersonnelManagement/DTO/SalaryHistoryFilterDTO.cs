namespace PersonnelManagement.DTO
{
    public class SalaryHistoryFilterDTO
    {
        public string? SortByDate { get; set; }
        public string? Status { get; set; }
        public long? EmployeeId { get; set; }
        public required int Page { get; set; }
        public required int PageSize { get; set; }
    }
}
