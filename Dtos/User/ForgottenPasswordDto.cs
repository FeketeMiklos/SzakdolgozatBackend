using System.ComponentModel.DataAnnotations;

namespace SzakdolgozatBackend.Dtos.User
{
    public class ForgottenPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; }
    }
}
