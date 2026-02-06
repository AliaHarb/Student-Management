using Student_Management.Models;

namespace Student_Management.Repository
{
    public class CourseRepository:ICourseRepository
    {

        private readonly ApplicationContext _context;
        public CourseRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void Add(Course course)
        {
            _context.Add(course);
        }
        public void Update(Course course)
        {
            _context.Update(course);
        }
        public Course GetById(int id)
        {
            return _context.Courses.FirstOrDefault(c => c.CourseId == id);
        }
        public void Delete(int id)
        {
            var course = GetById(id);
            if (course != null)
            {
                _context.Remove(course);
            }
        }

        public List<Course> GetAll()
        {
            return _context.Courses.ToList();
        }
        public void Save()
        {
            _context.SaveChanges();
        }






    }
}
