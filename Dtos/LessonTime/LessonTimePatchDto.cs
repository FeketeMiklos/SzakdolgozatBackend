namespace SzakdolgozatBackend.Dtos.LessonTime
{
    public class LessonTimePatchDto
    {
        public DateOnly? Date { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public int? LessonId { get; set; }
    }
}
