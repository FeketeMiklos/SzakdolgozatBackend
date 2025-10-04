using System.ComponentModel.DataAnnotations;

namespace SzakdolgozatBackend.Dtos.Lesson
{
    public class LessonCreateDto
    {
        [StringLength(10)]
        public string Code { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public bool Reoccuring { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public int NumberOfAttendees { get; set; }
        public int UserId { get; set; }
    }
}
