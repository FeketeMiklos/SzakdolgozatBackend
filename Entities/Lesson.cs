using System.ComponentModel.DataAnnotations;

namespace SzakdolgozatBackend.Entities
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool Reoccuring { get; set; }

        //May need to be split into latitude and longitude later
        [Required]
        public string Location { get; set; } 

        [Required]
        public string Room { get; set; }

        [Required]
        public int NumberOfAttendees { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<LessonTime> LessonTimes { get; set; }
        public ICollection<Signature> Signatures { get; set; }
    }
}
