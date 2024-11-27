using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.DTO
{
    public class ForgotPasswordChangeDTO
    {
        [MinLength(1)]
        public required string Email { get; set; }
        public required int Code { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirm { get; set; }
    }
}
