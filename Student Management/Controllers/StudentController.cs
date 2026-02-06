using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Management.Models;
using System.Text.Json;
using Student_Management.Repository;

namespace Student_Management.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentRepository _studentRepo;
        private readonly ICourseRepository _courseRepo; 
        public StudentController(IStudentRepository studentRepo, ICourseRepository courseRepo)
        {
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
        }
        //Student/Index
        public IActionResult Index()
        {
            var st = _studentRepo.GetAll();
            List<Student> recentStudentsList = new List<Student>();
            string sessionData = HttpContext.Session.GetString("RecentIds");
            if (!string.IsNullOrEmpty(sessionData))
            {
                var recentIds = JsonSerializer.Deserialize<List<int>>(sessionData);
                foreach (var id in recentIds)
                {
                    var stt = _studentRepo.GetById(id);
                    if (st != null)
                    {
                        recentStudentsList.Add(stt);
                    }
                }
            }
            ViewBag.RecentStudents = recentStudentsList;
            return View("Index", st);
        }
        //Student/Details
            public IActionResult Details(int id)
            {
                var student = _studentRepo.GetById(id);
                if (student == null) return NotFound();
                List<int> idsList = new List<int>();

                string sessionData = HttpContext.Session.GetString("RecentIds");
            if (!string.IsNullOrEmpty(sessionData))
            {
                idsList = JsonSerializer.Deserialize<List<int>>(sessionData);
            }
                if (idsList.Contains(id))
                {
                    idsList.Remove(id);
                }
                idsList.Insert(0, id);

                if (idsList.Count > 3)
                {
                    idsList = idsList.Take(3).ToList();
                }
                string newData = JsonSerializer.Serialize(idsList);
                HttpContext.Session.SetString("RecentIds", newData);



                ViewBag.Msg = "Student Profile";
                return View(student);
            }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Courses = new SelectList(_courseRepo.GetAll(), "CourseId", "CourseName");
            return View();

        }
        // POST: Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _studentRepo.Add(student);
                _studentRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Courses = new SelectList(_courseRepo.GetAll(), "CourseId", "CourseName");
            return View(student);
        }
        // GET: Edit
        public IActionResult Edit(int id)
        {

            var student= _studentRepo.GetById(id);
            ViewBag.Courses = new SelectList(_courseRepo.GetAll(), "CourseId", "CourseName", student.CourseId);
            return View(student);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            if (ModelState.IsValid) 
            {
                _studentRepo.Update(student);
                _studentRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Courses = new SelectList(_courseRepo.GetAll(), "CourseId", "CourseName", student.CourseId);
            return View(student);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var student = _studentRepo.GetById(id);

            if (student == null) return NotFound();

            return View(student);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _studentRepo.GetById(id);
            if (student != null) 
            {
                _studentRepo.Delete(id);
                _studentRepo.Save();
            }
            return RedirectToAction(nameof(Index));
        }



    }
}
