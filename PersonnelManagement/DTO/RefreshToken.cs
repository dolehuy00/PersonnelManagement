namespace PersonnelManagement.DTO
{
    public class RefreshToken
    {
        public required string Token { get; set; }
        public required string UserId { get; set; }
        public required string RoleName { get; set; }
        public string? IpAddress { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

}
