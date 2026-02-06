using Microsoft.EntityFrameworkCore;

namespace Student_Management.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public ApplicationContext()
        {
        }

       
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData
                (
                new Course { CourseId = 1, CourseName = "Math 101", CourseCode = "MATH101", Credits = 3, Description = "Basic Math" },
                new Course { CourseId = 2, CourseName = "CS 101", CourseCode = "CS101", Credits = 4, Description = "Intro to Programming" }
            );
            modelBuilder.Entity<Student>().HasData(
        new Student
        {
            Id = 1,
            Name = "Ahmed",
            Address = "Cairo",
            ImageURL = "1.png",
            CourseId = 1,
            Email = "ahmed@test.com",
            Age = 20
        },
        new Student
        {
            Id = 2,
            Name = "Alia",
            Address = "Giza",
            ImageURL = "2.png",
            CourseId = 2,
            Email = "Alia@test.com",
            Age = 22
        }
            );
      
        }
    }
}
