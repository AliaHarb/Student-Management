using Student_Management.Models;

namespace Student_Management.Repository
{
    public interface IStudentRepository
    {
        public void Add(Student student);
        public void Update(Student student);
        public void Delete(int id);
        public Student GetById(int id);
        public List<Student> GetAll();
        public void Save();
    }
}
