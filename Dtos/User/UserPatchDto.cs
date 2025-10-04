using System.ComponentModel.DataAnnotations;

namespace SzakdolgozatBackend.Dtos.User
{
    public class UserPatchDto
    {
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? Name { get; set; }
    }
}
