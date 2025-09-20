using System.ComponentModel.DataAnnotations;

namespace SzakdolgozatBackend.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public UserTypeEnum Type { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}
