using Microsoft.AspNetCore.Mvc;
using MyWebApp.Data;

namespace MyWebApp.Controllers
{
    public class CourseController : Controller
    {
        private SchoolContext db;
        public CourseController(SchoolContext context) {
            db = context;
        }
        public IActionResult Index()
        {
            var course = db.Courses.ToList();
            return View(course);
        }
        public IActionResult Create() {

            return View();
        }
    }
}
