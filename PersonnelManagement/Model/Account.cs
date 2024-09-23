namespace PersonnelManagement.Model
{
    public class Account
    {
        public long Id { get; set; }
        public string? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Role Role { get; set; }
        public required AccountStatus Status { get; set; }
        public required Employee Employee { get; set; }
    }
}
