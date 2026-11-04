using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TH_Lab6.Data;

namespace TH_Lab6.ViewComponents;

public class MajorViewComponent : ViewComponent
{
    private readonly SchoolContext _db;

    public MajorViewComponent(SchoolContext db) => _db = db;

    public async Task<IViewComponentResult> InvokeAsync(
        bool ajaxMode = true, int? selectedMajorId = null, string? keyword = null)
    {
        ViewData["AjaxMode"] = ajaxMode;
        ViewData["SelectedMajorID"] = selectedMajorId;
        ViewData["Keyword"] = keyword?.Trim() ?? "";
        var majors = await _db.Majors.AsNoTracking()
            .OrderBy(m => m.MajorID)
            .ToListAsync();
        return View("RenderMajor", majors);
    }
}
