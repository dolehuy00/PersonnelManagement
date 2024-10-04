using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.DTO
{
    public class RequestChangePasswordDTO
    {
        [MinLength(8)]
        public required string NewPassword { get; set; }
        public required string CurrentPassword { get; set; }
        [MinLength(8)]
        public required string PasswordConfirm { get; set; }
    }
}
