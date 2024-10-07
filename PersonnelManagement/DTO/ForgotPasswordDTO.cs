using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.DTO
{
    public class ForgotPasswordDTO
    {
        [MinLength(1)]
        public required string Email { get; set; }
        public int Code { get; set; }
    }
}
