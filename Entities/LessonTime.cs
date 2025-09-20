using System.ComponentModel.DataAnnotations;

namespace SzakdolgozatBackend.Entities
{
    public class LessonTime
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
