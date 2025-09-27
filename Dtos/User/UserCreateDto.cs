using System.ComponentModel.DataAnnotations;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Dtos.User
{
    public class UserCreateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string Password { get; set; }

        public UserTypeEnum Type { get; set; }
    }
}
