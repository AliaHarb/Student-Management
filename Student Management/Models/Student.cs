using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Management.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(3)]
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Address { get; set; }
        [Required,EmailAddress]
        [UniqueEmail(msg = "You Must But Unique Email")]
        public string Email {  get; set; }
        [Range(18,60)]
        public int Age { get; set; }
        public int CourseId { get; set; }   // Foreign Key

        [ForeignKey("CourseId")]
        public Course Course { get; set; }  // Navigation property

    }
}
