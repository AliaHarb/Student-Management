using System.ComponentModel.DataAnnotations;

namespace Student_Management.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        [Required]
        [StringLength(20)]
        public string CourseCode { get; set; }
        public int Credits { get; set; }
        public string Description { get; set; }
        public ICollection<Student> Students { get; set; } // Navigation property


    }
}
