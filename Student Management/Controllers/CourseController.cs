using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Management.Models;
using Student_Management.Repository;

namespace Student_Management.Controllers
{
    [Authorize] 
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepo;
        public CourseController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

       
        public IActionResult Index()
        {
            var courses = _courseRepo.GetAll();
            return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = _courseRepo.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseRepo.Add(course);
                _courseRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var course = _courseRepo.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseRepo.Update(course);
                _courseRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var course = _courseRepo.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _courseRepo.Delete(id);
            _courseRepo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}