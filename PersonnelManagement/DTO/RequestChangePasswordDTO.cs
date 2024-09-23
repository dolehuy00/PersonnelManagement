namespace MovieAppApi.DTO
{
    public class RequestChangePasswordDTO
    {
        public required string Email { get; set; }
        public required string NewPassword { get; set; }
        public required string OldPassword { get; set; }
        public required string PasswordConfirm { get; set; }
    }
}
