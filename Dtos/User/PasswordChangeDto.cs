using System.ComponentModel.DataAnnotations;

namespace SzakdolgozatBackend.Dtos.User
{
    public class PasswordChangeDto
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; }
    }
}
