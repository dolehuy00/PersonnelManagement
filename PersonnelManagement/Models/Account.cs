namespace PersonnelManagement.Model
{
    public class Account
    {
        public long Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int RoleId { get; set; }
        public required Role Role { get; set; }
        public required string Status { get; set; }
        public long EmployeeId { get; set; }
        public required Employee Employee { get; set; }
    }
}
