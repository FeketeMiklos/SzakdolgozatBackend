namespace SzakdolgozatBackend.Dtos.LessonTime
{
    public class LessonTimeGetDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int LessonId { get; set; }
    }
}
