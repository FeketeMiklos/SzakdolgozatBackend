namespace SzakdolgozatBackend.Dtos.Signature
{
    public class SignatureCreateDto
    {
        public string SignatureBase64 { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string StudentNeptunCode { get; set; }
        public int LessonId { get; set; }
    }
}
