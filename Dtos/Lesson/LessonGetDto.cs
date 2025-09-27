using System.ComponentModel.DataAnnotations;

namespace SzakdolgozatBackend.Dtos.Lesson
{
    public class LessonGetDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Reoccuring { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public int NumberOfAttendees { get; set; }
        public int UserId { get; set; }
    }
}
