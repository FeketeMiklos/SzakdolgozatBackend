using System.ComponentModel.DataAnnotations;

namespace SzakdolgozatBackend.Entities
{
    public class Student
    {
        [Key]
        [Required]
        [StringLength(6)]
        public string NeptunCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public ICollection<Signature> Signatures { get; set; }
    }
}
