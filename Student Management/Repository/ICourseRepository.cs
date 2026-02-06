using Student_Management.Models;

namespace Student_Management.Repository
{
    public interface ICourseRepository
    {
        public void Add(Course course);
        public void Update(Course course);
        public void Delete(int id);
        public Course GetById(int id);
        public List<Course> GetAll();
        public void Save();

    }
}
