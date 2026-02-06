namespace Student_Management.Models
{
    public class StudentBL
    {
        List<Student> students;
        public StudentBL()
        {
            students = new List<Student>();
            students.Add(new Student() { Id = 1, Name = "Ahmed", Address = "Cairo", ImageURL = "1.png" });
            students.Add(new Student() { Id = 2, Name = "Ali", Address = "Cairo", ImageURL = "1.png" });
            students.Add(new Student() { Id = 3, Name = "Alia", Address = "Cairo", ImageURL = "1.png" });
            students.Add(new Student() { Id = 4, Name = "yara", Address = "Cairo", ImageURL = "1.png" });
        }
        public List<Student> GetAll()
        {
            return students;
        }
        public Student GetById(int id)
        {
            return students.FirstOrDefault(st=>st.Id==id);
        }
    }
}

