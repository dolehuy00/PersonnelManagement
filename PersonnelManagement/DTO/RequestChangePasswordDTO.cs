namespace MovieAppApi.DTO
{
    public class RequestChangePasswordDTO
    {
        public required string NewPassword { get; set; }
        public required string CurrentPassword { get; set; }
        public required string PasswordConfirm { get; set; }
    }
}
