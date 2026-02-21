using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Management.Models;
using Student_Management.Repository;

namespace Student_Management.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepo;
        public CourseController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }
        [Authorize]
        public IActionResult Index()
        {
            var courses = _courseRepo.GetAll();
            return View(courses);
        }

        // GET: /Course/Details/5
        public IActionResult Details(int id)
        {
            var course = _courseRepo.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        // GET: /Course/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: /Course/Edit/5
        public IActionResult Edit(int id)
        {
            var course = _courseRepo.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        // POST: /Course/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: /Course/Delete/5
        public IActionResult Delete(int id)
        { 
            var course = _courseRepo.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        // POST: /Course/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _courseRepo.Delete(id);
            _courseRepo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}