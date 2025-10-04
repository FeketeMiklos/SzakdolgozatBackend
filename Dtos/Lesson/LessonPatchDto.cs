namespace SzakdolgozatBackend.Dtos.Lesson
{
    public class LessonPatchDto
    {
        public string? Name { get; set; }
        public bool? Reoccuring { get; set; }
        public string? Location { get; set; }
        public string? Room { get; set; }
        public int? NumberOfAttendees { get; set; }
    }
}
