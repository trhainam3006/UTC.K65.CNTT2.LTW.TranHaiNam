using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TH_Lab6.Data;
using TH_Lab6.Models;

namespace TH_Lab6.Controllers;

public class LearnerController : Controller
{
    private readonly SchoolContext _db;
    private const int PageSize = 3;

    public LearnerController(SchoolContext db) => _db = db;

    // Lab6: main page combines filtering, searching and paging through AJAX.
    // Without JavaScript, its GET links and search form still work (synchronous fallback).
    [HttpGet]
    public async Task<IActionResult> Index(int? mid, string? keyword, int? pageIndex)
    {
        var learners = await GetPageAsync(mid, keyword, pageIndex, ajaxMode: true);
        return View(learners);
    }

    // Preserve Lab5's synchronous mode as a separate page for comparison.
    [HttpGet]
    public async Task<IActionResult> Sync(int? mid, string? keyword, int? pageIndex)
    {
        var learners = await GetPageAsync(mid, keyword, pageIndex, ajaxMode: false);
        return View("Index", learners);
    }

    [HttpGet]
    public IActionResult Ajax() => RedirectToAction(nameof(Index));

    // This action returns the table AND the pagination links, not the full layout.
    [HttpGet]
    public async Task<IActionResult> LearnerFilter(int? mid, string? keyword, int? pageIndex)
    {
        var learners = await GetPageAsync(mid, keyword, pageIndex, ajaxMode: true);
        return PartialView("LearnerTable", learners);
    }

    // Backward-compatible AJAX endpoint from Lab5 (first page of a Major).
    [HttpGet]
    public Task<IActionResult> LearnerByMajorID(int? mid)
        => LearnerFilter(mid, null, 1);

    private async Task<List<Learner>> GetPageAsync(
        int? mid, string? keyword, int? pageIndex, bool ajaxMode)
    {
        var normalizedKeyword = keyword?.Trim() ?? "";
        var learners = _db.Learners.AsNoTracking().AsQueryable();

        if (mid.HasValue)
            learners = learners.Where(l => l.MajorID == mid.Value);

        // Search by both first/middle name and surname. Combine with the Major filter.
        if (normalizedKeyword.Length > 0)
        {
            string text = normalizedKeyword.ToLower();
            learners = learners.Where(l =>
                l.FirstMidName.ToLower().Contains(text) ||
                l.LastName.ToLower().Contains(text));
        }

        int totalItems = await learners.CountAsync();
        int pageNum = (int)Math.Ceiling(totalItems / (double)PageSize);
        int currentPage = Math.Clamp(pageIndex.GetValueOrDefault(1), 1, Math.Max(1, pageNum));

        var result = await learners
            .Include(l => l.Major)
            .OrderBy(l => l.LearnerID) // Stable ordering avoids repeated/skipped records.
            .Skip((currentPage - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        ViewBag.pageNum = pageNum;
        ViewBag.pageIndex = currentPage;
        ViewBag.pageSize = PageSize;
        ViewBag.totalItems = totalItems;
        ViewBag.mid = mid;
        ViewBag.keyword = normalizedKeyword;
        ViewBag.AjaxMode = ajaxMode;
        return result;
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
                if (!await _db.Learners.AnyAsync(l => l.LearnerID == id))
                    return NotFound();
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
        ViewBag.MajorID = new SelectList(
            _db.Majors.OrderBy(m => m.MajorID).ToList(),
            "MajorID", "MajorName", selected);
    }
}
