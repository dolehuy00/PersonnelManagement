namespace PersonnelManagement.DTO
{
    public class ForgotPasswordDTO
    {
        public required string Email { get; set; }
        public int Code { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
    }
}
