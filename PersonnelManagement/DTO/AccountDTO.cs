namespace PersonnelManagement.DTO
{
    public class AccountDTO
    {
        public long Id { get; set; }
        public required string Email { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public required string Status { get; set; }
        public string? StatusName { get; set; }
        public long EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
    }
}
