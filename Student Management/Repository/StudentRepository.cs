using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Student_Management.Models;

namespace Student_Management.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationContext _context;

        public StudentRepository(ApplicationContext context)
        {
            _context = context;
        }
        public void Add(Student student)
        {
            _context.Students.Add(student);
        }
        public Student GetById(int id)
        {
           
            return _context.Students.Include(s => s.Course).FirstOrDefault(st => st.Id == id);
        }

        public List<Student> GetAll()
        {
            
            return _context.Students.Include(s => s.Course).ToList();
        }
        public void Update(Student student)
        {
            _context.Update(student);
        }
        public void Delete(int id)
        {
            var student = GetById(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
        }
      
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
