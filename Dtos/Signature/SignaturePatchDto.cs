namespace SzakdolgozatBackend.Dtos.Signature
{
    public class SignaturePatchDto
    {
        public string? SignatureBase64 { get; set; }
        public DateOnly? Date { get; set; }
        public string? StudentNeptunCode { get; set; }
        public int? LessonId { get; set; }
    }
}
