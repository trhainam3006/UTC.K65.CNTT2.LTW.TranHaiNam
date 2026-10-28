
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TH_Lab4.Data;
using TH_Lab4.Models;

namespace TH_Lab4.Controllers
{
    public class LearnerController : Controller
    {
        private readonly SchoolContext db;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var learners = db.Learners
                .Include(l => l.Major)
                .ToList();

            return View(learners);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MajorID = new SelectList(
                db.Majors, "MajorID", "MajorName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("FirstMidName,LastName,MajorID,EnrollmentDate")]
            Learner learner)
        {
            ModelState.Remove(nameof(Learner.Enrollments));

            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.MajorID = new SelectList(
                db.Majors, "MajorID", "MajorName",
                learner.MajorID);

            return View(learner);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var learner = db.Learners.Find(id);

            if (learner == null)
            {
                return NotFound();
            }

            ViewBag.MajorID = new SelectList(
                db.Majors, "MajorID", "MajorName",
                learner.MajorID);

            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")]
            Learner learner)
        {
            if (id != learner.LearnerID)
            {
                return NotFound();
            }

            ModelState.Remove(nameof(Learner.Enrollments));

            if (ModelState.IsValid)
            {
                var existing = db.Learners.Find(id);

                if (existing == null)
                {
                    return NotFound();
                }

                existing.FirstMidName = learner.FirstMidName;
                existing.LastName = learner.LastName;
                existing.MajorID = learner.MajorID;
                existing.EnrollmentDate = learner.EnrollmentDate;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.MajorID = new SelectList(
                db.Majors, "MajorID", "MajorName",
                learner.MajorID);

            return View(learner);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var learner = db.Learners
                .Include(l => l.Major)
                .Include(l => l.Enrollments)
                .FirstOrDefault(l => l.LearnerID == id);

            if (learner == null)
            {
                return NotFound();
            }

            if (learner.Enrollments.Any())
            {
                return Content(
                    "This learner has some enrollments, can't delete!");
            }

            return View(learner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var learner = db.Learners
                .Include(l => l.Enrollments)
                .FirstOrDefault(l => l.LearnerID == id);

            if (learner == null)
            {
                return NotFound();
            }

            if (learner.Enrollments.Any())
            {
                return Content(
                    "This learner has some enrollments, can't delete!");
            }

            db.Learners.Remove(learner);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool LearnerExists(int id)
        {
            return db.Learners.Any(
                l => l.LearnerID == id);
        }
    }
}
