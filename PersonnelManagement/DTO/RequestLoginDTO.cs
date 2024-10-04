using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.DTO
{
    public class RequestLoginDTO
    {
        [MinLength(1)]
        public required string Email { get; set; }
        [MinLength(1)]
        public required string Password { get; set; }
    }
}
