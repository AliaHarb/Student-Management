using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Management.Models;
using System.Text.Json;
using Student_Management.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Student_Management.Controllers
{
    [Authorize] // يمنع دخول غير المسجلين تماماً
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepo;
        private readonly ICourseRepository _courseRepo;

        public StudentController(IStudentRepository studentRepo, ICourseRepository courseRepo)
        {
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
        }

        // GET: Student/Index
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
                    if (stt != null) // تصحيح: التحقق من stt وليس st
                    {
                        recentStudentsList.Add(stt);
                    }
                }
            }
            ViewBag.RecentStudents = recentStudentsList;
            return View("Index", st);
        }

        // GET: Student/Details/5
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

        // GET: Student/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Courses = new SelectList(_courseRepo.GetAll(), "CourseId", "CourseName");
            return View();
        }

        // POST: Student/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                // Bonus: إضافة إيميل الشخص الذي قام بالإشاء
                // student.CreatedBy = User.Identity.Name; 

                _studentRepo.Add(student);
                _studentRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Courses = new SelectList(_courseRepo.GetAll(), "CourseId", "CourseName");
            return View(student);
        }

        // GET: Student/Edit/5
        [Authorize(Roles = "Admin")] // تأمين الـ GET أيضاً
        public IActionResult Edit(int id)
        {
            var student = _studentRepo.GetById(id);
            if (student == null) return NotFound();

            ViewBag.Courses = new SelectList(_courseRepo.GetAll(), "CourseId", "CourseName", student.CourseId);
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

        // GET: Student/Delete/5
        [Authorize(Roles = "Admin")] // تأمين الـ GET أيضاً
        public IActionResult Delete(int id)
        {
            var student = _studentRepo.GetById(id);
            if (student == null) return NotFound();
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // يفضل دائماً إضافتها مع الـ Post
        [Authorize(Roles = "Admin")]
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