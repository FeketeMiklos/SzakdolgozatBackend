using System.ComponentModel.DataAnnotations;
using SzakdolgozatBackend.Entities;

namespace SzakdolgozatBackend.Dtos.User
{
    public class UserGetDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public UserTypeEnum Type { get; set; }
    }
}
