using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TH_Lab5.Data;
using TH_Lab5.Models;

namespace TH_Lab5.Controllers;

public class LearnerController : Controller
{
    private readonly SchoolContext _db;
    public LearnerController(SchoolContext db) => _db = db;

    // Lab5 / Part 1: synchronous filtering; ?mid=1 loads only IT learners.
    [HttpGet]
    public async Task<IActionResult> Index(int? mid)
    {
        ViewBag.AjaxMode = false;
        ViewBag.SelectedMajorID = mid;
        return View(await QueryLearners(mid).ToListAsync());
    }

    // Lab5 / Part 2: AJAX filtering (same page and table, no page reload).
    [HttpGet]
    public async Task<IActionResult> Ajax()
    {
        ViewBag.AjaxMode = true;
        ViewBag.SelectedMajorID = null;
        return View("Index", await QueryLearners(null).ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> LearnerByMajorID(int? mid)
    {
        // Partial HTML; requested by the browser JavaScript fetch().
        var learners = await QueryLearners(mid).ToListAsync();
        return PartialView("LearnerTable", learners);
    }

    private IQueryable<Learner> QueryLearners(int? mid)
    {
        var query = _db.Learners.AsNoTracking().Include(l => l.Major).AsQueryable();
        if (mid.HasValue) query = query.Where(l => l.MajorID == mid.Value);
        return query.OrderBy(l => l.LearnerID);
    }

    [HttpGet]
    public IActionResult Create()
    {
        LoadMajors();
        return View(new Learner { EnrollmentDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
    {
        if (ModelState.IsValid)
        {
            _db.Learners.Add(learner);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã thêm người học thành công.";
            return RedirectToAction(nameof(Index));
        }
        LoadMajors(learner.MajorID);
        return View(learner);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var learner = await _db.Learners.FindAsync(id);
        if (learner is null) return NotFound();
        LoadMajors(learner.MajorID);
        return View(learner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
    {
        if (id != learner.LearnerID) return NotFound();

        if (ModelState.IsValid)
        {
            var current = await _db.Learners.FindAsync(id);
            if (current is null) return NotFound();
            current.FirstMidName = learner.FirstMidName;
            current.LastName = learner.LastName;
            current.MajorID = learner.MajorID;
            current.EnrollmentDate = learner.EnrollmentDate;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _db.Learners.AnyAsync(l => l.LearnerID == id)) return NotFound();
                throw;
            }
            TempData["Success"] = "Đã cập nhật người học.";
            return RedirectToAction(nameof(Index));
        }
        LoadMajors(learner.MajorID);
        return View(learner);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var learner = await _db.Learners.AsNoTracking()
            .Include(l => l.Major)
            .Include(l => l.Enrollments)
            .FirstOrDefaultAsync(l => l.LearnerID == id);
        if (learner is null) return NotFound();
        if (learner.Enrollments.Any())
        {
            TempData["Error"] = "Không thể xóa: người học đã đăng ký môn học.";
            return RedirectToAction(nameof(Index));
        }
        return View(learner);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var learner = await _db.Learners.Include(l => l.Enrollments)
            .FirstOrDefaultAsync(l => l.LearnerID == id);
        if (learner is null) return NotFound();
        if (learner.Enrollments.Any())
        {
            TempData["Error"] = "Không thể xóa: người học đã đăng ký môn học.";
            return RedirectToAction(nameof(Index));
        }
        _db.Learners.Remove(learner);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa người học.";
        return RedirectToAction(nameof(Index));
    }

    private void LoadMajors(int? selected = null)
    {
        ViewBag.MajorID = new SelectList(_db.Majors.OrderBy(m => m.MajorID).ToList(),
            "MajorID", "MajorName", selected);
    }
}
