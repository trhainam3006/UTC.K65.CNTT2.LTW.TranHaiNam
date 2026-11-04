using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TH_Lab5.Data;

namespace TH_Lab5.ViewComponents;

public class MajorViewComponent : ViewComponent
{
    private readonly SchoolContext _db;

    public MajorViewComponent(SchoolContext db) => _db = db;

    public async Task<IViewComponentResult> InvokeAsync(
        bool ajaxMode = false, int? selectedMajorId = null)
    {
        ViewData["AjaxMode"] = ajaxMode;
        ViewData["SelectedMajorID"] = selectedMajorId;
        var majors = await _db.Majors.AsNoTracking()
            .OrderBy(m => m.MajorID).ToListAsync();
        return View("RenderMajor", majors);
    }
}
