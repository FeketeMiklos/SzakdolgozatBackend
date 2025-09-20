using System.ComponentModel.DataAnnotations;

namespace SzakdolgozatBackend.Entities
{
    public class Signature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SignatureBase64 { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        public string StudentNeptunCode { get; set; }
        public Student Student { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
