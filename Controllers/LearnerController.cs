using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Core;
using MyWebApp.Data;
using MyWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MyWebApp.Controllers
{
	public class LearnerController : Controller
    {
        private SchoolContext db;
        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        public IActionResult Index(int? mid)
        {
            if(mid == null)
            {
				var Learners = db.Learners.Include(m => m.Major).ToList();
				return View(Learners);
            }
            else
            {
				var Learners = db.Learners.Where(l => l.MajorID == mid).Include(m => m.Major).ToList();
				return View(Learners);
			}
           
        }

		// //Load dữ liệu không đồng bộ sử dụng AJAX
        public IActionResult LearnerByMajorID(int mid)
        {
			var Learners = db.Learners.Where(l => l.MajorID == mid).Include(m => m.Major).ToList();
            return PartialView("LearnerTable", Learners);
		}

		[HttpGet]
		//thêm 2 action create
		public IActionResult Create()
		{
            // Cách để tạo SelectList Major hiển thị select option
			ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName"); //cách 2
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] 
	Learner learner)
		{
			if (ModelState.IsValid)
			{
				db.Learners.Add(learner);
				db.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			//lại dùng 1 trong 2 cách tạo SelectList gửi về View để hiển thị danh sách Majors
			ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
			return View();
		}

		public IActionResult Delete(int id)
		{
			var learner = db.Learners.Include(l => l.Major).Include(e => e.Enrollments)
                .FirstOrDefault(n => n.LearnerID == id);
			if (learner == null)
			{
				return NotFound();
			}
            if(learner.Enrollments.Count() > 0)
            {
                return Content("This learner has some enrollments, cant't delete");
            }

            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName",learner.MajorID);
            return View(learner);
		}
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		public IActionResult DeleteConfirm(int id) {
            var learner = db.Learners.FirstOrDefault(n => n.LearnerID == id);
            if (learner == null)
            {
                return NotFound();
            }

			var result = db.Learners.Remove(learner);
			db.SaveChanges();
            return RedirectToAction(nameof(Index));
		}

        public IActionResult Edit(int id)
        {
            var learner = db.Learners.FirstOrDefault(n => n.LearnerID == id);
            if (learner == null)
            {
                return NotFound();
            }

            // Tạo SelectList để hiển thị danh sách chuyên ngành (Majors)
            // val thứ 4 là để ngầm định chọn chính xác id của cái cần edit
            var majors = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            ViewBag.MajorID = majors;

            return View(learner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learners)
        {
            if (id != learners.LearnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(learners);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Tạo SelectList để hiển thị danh sách Majors
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View(learners);
        }



    }
}
